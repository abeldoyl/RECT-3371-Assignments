/*
    Abel Doyle
    RCET 3371
    Fall Semester 2026
    EtchOSketch
    https://github.com/abeldoyl/RECT-3371-Assignments/tree/main/HVACHomeController
*/
using System.Diagnostics;
using System.Globalization;
using System.IO.Ports;

namespace HVACHomeController
{
    public partial class HVACForm : Form
    {
        public HVACForm()
        {
            InitializeComponent();
            ApplyTheme();
            WireEvents();
        }

        // =====================================================================
        //  Fields and constants
        // =====================================================================

        // ISU palette
        private readonly Color GrowlGreyLight = Color.FromArgb(230, 231, 232);
        private readonly Color GrowlGreyMed = Color.FromArgb(167, 167, 167);
        private readonly Color GrowlGrey = Color.FromArgb(130, 130, 130);
        private readonly Color Roarange = Color.FromArgb(244, 121, 32);
        private readonly Color RoarangeL = Color.FromArgb(246, 146, 64);
        private readonly Color BengalBlack = Color.FromArgb(0, 0, 0);

        // HVAC limits and timing
        private const double MinSetpoint = 50.0;
        private const double MaxSetpoint = 90.0;
        private const double SetpointStep = 0.5;
        private const double Hysteresis = 2.0;                       // degrees F
        private static readonly TimeSpan FanLeadTime = TimeSpan.FromSeconds(5);
        private static readonly TimeSpan FanRunOn = TimeSpan.FromSeconds(5);
        private static readonly TimeSpan TempCheckInterval = TimeSpan.FromSeconds(30);
        private static readonly TimeSpan PressureCheckInterval = TimeSpan.FromMinutes(2);
        private static readonly TimeSpan ReconnectInterval = TimeSpan.FromSeconds(10);

        // QY@ board commands
        private const byte CmdIdentify = 0xF0;      // from the serial example (64 byte reply)
        private const byte CmdDigitalWrite = 0x20;  // from the serial example: 0x20, <pins>
        private const byte CmdAN1Read = 0x51;       // from the serial example
        private const byte CmdAN2Read = 0x52;       // TODO: ASSUMED - confirm
        private const byte CmdDigitalRead = 0x30;   // TODO: ASSUMED - confirm
        private const int ReplyTimeoutMs = 150;

        private enum Mode { Off, Heat, Cool }
        private sealed record Inputs(double RoomTemp, double UnitTemp, bool Safety,
            bool HeatSw, bool FanOnlySw, bool Pressure, bool CoolSw);

        // Settings
        private double heatSetpoint = 68.0;     // low  (heating)
        private double coolSetpoint = 76.0;     // high (A/C)
        private string savedPort = "";
        private string lastSavedSettings = "";
        private bool editingHeat = true;        // which setpoint the up/down buttons change
        private Mode lastMode = Mode.Off;

        // Simulation (room temperature is simulated, no up/down boxes needed)
        private const double SimAmbient = 70.0;         // where the room drifts when idle
        private const double SimHeatRate = 0.25;        // degrees F per second with heat on
        private const double SimCoolRate = 0.25;        // degrees F per second with cooling on
        private const double SimDriftRate = 0.03;       // degrees F per second drift to ambient
        private double simRoomTemp = SimAmbient;
        private double simUnitTemp = SimAmbient;
        private DateTime lastSimUpdate = DateTime.Now;

        // HVAC state
        private bool heatDemand, coolDemand, elementOn, wasDemand, fanWasOn;
        private bool tempFault, pressureFault, pressureChecked, unsafeLogged;
        private DateTime fanStart, lastTempCheck, lastPressureCheck;
        private DateTime runOnUntil = DateTime.MinValue;

        // Communication state
        private readonly SerialPort _serialPort = new();
        private Inputs? latestInputs;           // newest board reading (null = no data)
        private byte outputByte;                // outputs to send to the board
        private bool scanning, userDisconnected;
        private int failCount;
        private DateTime lastScan = DateTime.MinValue;

        private static readonly string ProjectFolder = FindProjectFolder();
        private static string SettingsPath => Path.Combine(ProjectFolder, "HVAC Settings.txt");

        // =====================================================================
        //  Serial helpers
        // =====================================================================
        void SerialPortSetup(string portName)
        {
            _serialPort.Close();
            _serialPort.PortName = portName;
            _serialPort.BaudRate = 9600;
            _serialPort.DataBits = 8;
            _serialPort.Parity = Parity.None;
            _serialPort.StopBits = StopBits.One;
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;
        }

        void SerialConnect()
        {
            _serialPort.Close();
            _serialPort.Open();
        }

        void SerialSend(byte[] data)
        {
            _serialPort.Write(data, 0, data.Length);
        }

        // Send a command and wait for a fixed-length reply (throws TimeoutException)
        byte[] SerialQuery(byte[] command, int replyLength)
        {
            _serialPort.DiscardInBuffer();
            SerialSend(command);

            Stopwatch timer = Stopwatch.StartNew();
            while (_serialPort.BytesToRead < replyLength)
            {
                if (timer.ElapsedMilliseconds > ReplyTimeoutMs)
                    throw new TimeoutException("No reply from QY@ board");
                Thread.Sleep(2);
            }

            byte[] reply = new byte[replyLength];
            _serialPort.Read(reply, 0, replyLength);
            return reply;
        }

        string[] GetSerialPorts()
        {
            return SerialPort.GetPortNames();
        }

        // Checks one port for a QY@ board. Uses its own SerialPort so it can run
        // on a background thread while the main port stays untouched.
        bool IsQyAtBoard(string portName)
        {
            try
            {
                using SerialPort sp = new(portName, 9600, Parity.None, 8, StopBits.One)
                {
                    ReadTimeout = 300,
                    WriteTimeout = 300
                };
                sp.Open();
                sp.DiscardInBuffer();                          // flush rx buffer
                sp.Write(new byte[] { CmdIdentify }, 0, 1);    // request settings
                Thread.Sleep(100);                             // wait for reply

                byte[] input = new byte[sp.BytesToRead];
                sp.Read(input, 0, input.Length);
                sp.Close();

                // reply is 64 bytes with "Qy@" at bytes 58-60
                return input.Length == 64 && input[58] == 81 && input[59] == 121 && input[60] == 64;
            }
            catch (Exception)
            {
                return false;   // busy, missing or not a QY@ board
            }
        }

        // Fills both port lists with the QY@ boards found, then connects (saved port first)
        async void UpdatePortSelection()
        {
            if (scanning) return;
            scanning = true;
            lastScan = DateTime.Now;

            string[] found = await Task.Run(() => GetSerialPorts().Where(IsQyAtBoard).ToArray());

            scanning = false;
            lastScan = DateTime.Now;
            if (IsDisposed) return;

            PortsComboBox.Items.Clear();
            PortsStripComboBox.Items.Clear();
            PortsComboBox.Items.AddRange(found);
            PortsStripComboBox.Items.AddRange(found);

            if (found.Length == 0 || userDisconnected || _serialPort.IsOpen) return;
            OpenPort(found.Contains(savedPort) ? savedPort : found[0]);
        }

        void OpenPort(string portName)
        {
            try
            {
                SerialPortSetup(portName);
                SerialConnect();
                failCount = 0;
                savedPort = portName;
                PortsComboBox.Text = portName;
                PortsStripComboBox.Text = portName;
            }
            catch (Exception)
            {
                _serialPort.Close();
            }
        }

        void ClosePort()
        {
            try { if (_serialPort.IsOpen) _serialPort.Close(); } catch (Exception) { }
            latestInputs = null;
        }

        void CommLost()
        {
            ClosePort();
            if (!userDisconnected) LogError("Communication lost with QY@ board - Attempting reconnect");
            lastScan = DateTime.Now;        // first retry in 10 s
            ResetCycle();
        }

        byte[] DigitalWriteQyAt(byte pins = 0x00)
        {
            byte[] command = { CmdDigitalWrite, pins };
            return command;
        }

        // Output bits (schematic pins 1-4): bit0 Interlock Warning, bit1 Heat, bit2 Cool, bit3 Fan
        static byte BuildOutputByte(bool warn, bool heat, bool cool, bool fan)
        {
            int b = 0;
            if (warn) b |= 0x01;
            if (heat) b |= 0x02;
            if (cool) b |= 0x04;
            if (fan) b |= 0x08;
            return (byte)b;
        }

        // ADC 1023 = 100 F
        static double AdcToF(int adc) => Math.Clamp(adc, 0, 1023) * 100.0 / 1023.0;

        // Reads ANI1 (room), ANI2 (HVAC unit) and DIG_I_1..5 from the board
        Inputs ReadBoardInputs()
        {
            byte[] an1 = SerialQuery(new[] { CmdAN1Read }, 2);          // high, low
            byte[] an2 = SerialQuery(new[] { CmdAN2Read }, 2);
            byte[] di = SerialQuery(new[] { CmdDigitalRead }, 1);       // bit0 = DIG_I_1 ...

            return new Inputs(
                AdcToF(an1[0] << 8 | an1[1]),
                AdcToF(an2[0] << 8 | an2[1]),
                Safety: (di[0] & 0x01) != 0,    // DIG_I_1 interlock
                HeatSw: (di[0] & 0x02) != 0,    // DIG_I_2 heat enable
                FanOnlySw: (di[0] & 0x04) != 0, // DIG_I_3 fan enable
                Pressure: (di[0] & 0x08) != 0,  // DIG_I_4 pressure
                CoolSw: (di[0] & 0x10) != 0);   // DIG_I_5 cooling enable
        }

        // Simulated room: heating/cooling moves the temperature toward the setpoint over
        // time, and the room slowly drifts back to ambient when nothing is running.
        Inputs ReadSimulation(DateTime now)
        {
            double dt = Math.Clamp((now - lastSimUpdate).TotalSeconds, 0, 1);
            lastSimUpdate = now;

            bool heating = elementOn && heatDemand;
            bool cooling = elementOn && coolDemand;

            if (heating) simRoomTemp += SimHeatRate * dt;
            else if (cooling) simRoomTemp -= SimCoolRate * dt;
            else
            {
                double drift = SimDriftRate * dt;
                if (Math.Abs(SimAmbient - simRoomTemp) <= drift) simRoomTemp = SimAmbient;
                else simRoomTemp += Math.Sign(SimAmbient - simRoomTemp) * drift;
            }
            simRoomTemp = Math.Clamp(simRoomTemp, 40.0, 100.0);

            // unit air temperature: warmer than the room when heating, cooler when cooling
            double unitTarget = heating ? simRoomTemp + 25 : cooling ? simRoomTemp - 15 : simRoomTemp;
            double step = 2.0 * dt;
            if (Math.Abs(unitTarget - simUnitTemp) <= step) simUnitTemp = unitTarget;
            else simUnitTemp += Math.Sign(unitTarget - simUnitTemp) * step;
            simUnitTemp = Math.Clamp(simUnitTemp, 40.0, 100.0);

            // If both heating and cooling are enabled, the radio buttons pick which one runs
            bool heatSw = HeatingCheckBox.Checked;
            bool coolSw = CoolingCheckBox.Checked;
            if (heatSw && coolSw)
            {
                heatSw = HeatRadioButton.Checked;
                coolSw = CoolRadioButton.Checked;
            }

            return new Inputs(simRoomTemp, simUnitTemp,
                SafetyCheckBox.Checked, heatSw, FanOnlyCheckBox.Checked,
                PressureCheckBox.Checked, coolSw);
        }

        // =====================================================================
        //  HVAC control logic
        // =====================================================================
        void RunControl(Inputs i, DateTime now)
        {
            Mode mode = i.HeatSw == i.CoolSw ? Mode.Off : (i.HeatSw ? Mode.Heat : Mode.Cool);
            bool warn = false, heatOut = false, coolOut = false, fanOnlyOut = false, fanCycleOut = false;

            if (!i.Safety)
            {
                // Safety interlock LOW: everything off, warning LED on
                warn = true;
                if (!unsafeLogged)
                {
                    LogError("Safety interlock switch LOW - Heating/Cooling/Fan disabled");
                    unsafeLogged = true;
                }
                ResetCycle();
            }
            else
            {
                unsafeLogged = false;
                if (mode == Mode.Off) tempFault = false;   // switching Off clears a maintenance fault

                UpdateDemand(mode, i.RoomTemp);
                bool demand = (heatDemand || coolDemand) && !tempFault;
                if (wasDemand && !demand) runOnUntil = now + FanRunOn;   // fan keeps running 5 s
                wasDemand = demand;
                bool runOn = !demand && now < runOnUntil;

                fanOnlyOut = i.FanOnlySw;                  // fan-only: runs continuously
                fanCycleOut = demand || runOn;             // fan for the heat/cool cycle
                bool fanOn = fanOnlyOut || fanCycleOut;

                if (fanOn && !fanWasOn) { fanStart = now; pressureChecked = false; }
                if (!fanOn) { pressureChecked = false; pressureFault = false; }
                fanWasOn = fanOn;

                bool leadDone = fanOn && now - fanStart >= FanLeadTime;
                if (leadDone) CheckPressure(i.Pressure, now);

                bool elementAllowed = demand && leadDone && pressureChecked && !pressureFault;
                if (elementAllowed)
                {
                    if (!elementOn) { elementOn = true; lastTempCheck = now; }
                    else if (now - lastTempCheck >= TempCheckInterval)
                    {
                        lastTempCheck = now;
                        bool ok = heatDemand ? i.UnitTemp > i.RoomTemp : i.UnitTemp < i.RoomTemp;
                        if (!ok)
                        {
                            tempFault = true;
                            elementOn = false;
                            LogError("System temperature verification failed - Maintenance required");
                        }
                    }
                }
                else elementOn = false;

                heatOut = elementOn && heatDemand;
                coolOut = elementOn && coolDemand;
            }

            bool fanOut = fanOnlyOut || fanCycleOut;
            outputByte = BuildOutputByte(warn, heatOut, coolOut, fanOut);   // sent by CommunicationTimer
            UpdateDisplay(i, mode, warn, heatOut, coolOut, fanOut);
        }

        // +2 degree hysteresis: heat turns on below its setpoint and off at setpoint + 2;
        // cooling turns on above its setpoint and off at setpoint - 2.
        void UpdateDemand(Mode mode, double room)
        {
            if (mode == Mode.Heat)
            {
                coolDemand = false;
                if (room < heatSetpoint) heatDemand = true;
                else if (room >= heatSetpoint + Hysteresis) heatDemand = false;
            }
            else if (mode == Mode.Cool)
            {
                heatDemand = false;
                if (room > coolSetpoint) coolDemand = true;
                else if (room <= coolSetpoint - Hysteresis) coolDemand = false;
            }
            else heatDemand = coolDemand = false;
        }

        // Checked once after the first 5 s of fan operation, then every 2 minutes.
        // While a fault exists it is re-checked every tick so heating/cooling
        // can resume as soon as pressure returns.
        void CheckPressure(bool pressureOk, DateTime now)
        {
            bool due = !pressureChecked || now - lastPressureCheck >= PressureCheckInterval;
            if (!due && !pressureFault) return;
            if (due) { pressureChecked = true; lastPressureCheck = now; }

            bool low = !pressureOk;
            if (low && !pressureFault)
                LogError("Differential pressure sensor LOW - Heating/Cooling disabled");
            pressureFault = low;
        }

        void ResetCycle()
        {
            heatDemand = coolDemand = elementOn = wasDemand = fanWasOn = false;
            pressureChecked = pressureFault = false;
            runOnUntil = DateTime.MinValue;
            outputByte = 0;
        }

        // =====================================================================
        //  Display helpers
        // =====================================================================
        void UpdateDisplay(Inputs i, Mode mode, bool warn, bool heatOut, bool coolOut, bool fanOut)
        {
            if (mode != lastMode)
            {
                if (mode == Mode.Heat) editingHeat = true;
                else if (mode == Mode.Cool) editingHeat = false;
                lastMode = mode;
                UpdateSetpointLabels();
            }

            TempLabel.Text = $"{i.RoomTemp:F1}°";

            HeatRadioButton.Checked = mode == Mode.Heat;
            CoolRadioButton.Checked = mode == Mode.Cool;
            OffRadioButton.Checked = mode == Mode.Off;
            FanOnCheckBox.Checked = i.FanOnlySw;
            HeatRadioButton.Enabled = CoolRadioButton.Enabled =
                OffRadioButton.Enabled = FanOnCheckBox.Enabled = i.Safety;

            ModeStatusLabel.Text = !i.Safety ? "Mode: INHIBITED" : $"Mode: {mode.ToString().ToUpper()}";
            FanModeLabel.Text = i.FanOnlySw ? "Fan Mode: ON" : "Fan Mode: AUTO";
            FanStatusLabel.Text = fanOut ? "Fan: ON" : "Fan: OFF";
            HeatStatusLabel.Text = heatOut ? "Heating: ON" : "Heating: OFF";
            CoolStatusLabel.Text = coolOut ? "Cooling: ON" : "Cooling: OFF";

            SafetyStatusLabel.Text = i.Safety ? "SAFE" : "UNSAFE";
            SafetyStatusLabel.ForeColor = i.Safety ? Color.DarkGreen : Color.Red;
            PressureStatusLabel.Text = i.Pressure ? "OK" : "LOW";
            PressureStatusLabel.ForeColor = i.Pressure ? Color.DarkGreen : Color.Red;

            bool fault = tempFault || pressureFault || warn;
            FaultStatusLabel.Text = fault ? "FAULT" : "NO FAULT";
            FaultStatusLabel.ForeColor = fault ? Color.Red : Color.DarkGreen;

            if (tempFault || pressureFault)
            {
                MaintenanceLabel.Text = "Maintenance Required";
                MaintenanceLabel.ForeColor = Color.Red;
            }
            else if (warn)
            {
                MaintenanceLabel.Text = "Safety Interlock Open";
                MaintenanceLabel.ForeColor = Color.Red;
            }
            else
            {
                MaintenanceLabel.Text = "System Normal";
                MaintenanceLabel.ForeColor = BengalBlack;
            }
        }

        void ShowNoInputs()
        {
            ResetCycle();
            TempLabel.Text = "--°";
            ModeStatusLabel.Text = "Mode: OFF";
            FanStatusLabel.Text = "Fan: OFF";
            HeatStatusLabel.Text = "Heating: OFF";
            CoolStatusLabel.Text = "Cooling: OFF";
        }

        void UpdateCommStatus()
        {
            if (SimulationCheckBox.Checked) SetComm("Simulation", BengalBlack);
            else if (_serialPort.IsOpen) SetComm($"Connected ({_serialPort.PortName})", Color.DarkGreen);
            else if (scanning) SetComm("Searching...", Roarange);
            else if (userDisconnected || lastScan == DateTime.MinValue) SetComm("Disconnected", Color.Red);
            else SetComm("Comm lost - retrying", Color.Red);
        }

        void SetComm(string text, Color color)
        {
            CommStatusLabel.Text = text;
            CommStatusLabel.ForeColor = color;
        }

        void UpdateSetpointLabels()
        {
            HeatToLabel.Text = $"Heat To {heatSetpoint:F1}°";
            CoolToLabel.Text = $"Cool To {coolSetpoint:F1}°";
            HeatToLabel.ForeColor = editingHeat ? Roarange : BengalBlack;   // orange = being edited
            CoolToLabel.ForeColor = editingHeat ? BengalBlack : Roarange;
            SetupHeatingRadioButton.Checked = editingHeat;
            SetupCoolingRadioButton.Checked = !editingHeat;
        }

        void AdjustSetpoint(double delta)
        {
            if (editingHeat) heatSetpoint = ClampSetpoint(heatSetpoint + delta);
            else coolSetpoint = ClampSetpoint(coolSetpoint + delta);
            UpdateSetpointLabels();
        }

        static double ClampSetpoint(double v) =>
            Math.Clamp(Math.Round(v * 2) / 2, MinSetpoint, MaxSetpoint);

        // =====================================================================
        //  Settings file and error log (both in the project folder)
        // =====================================================================
        // One line per save: "yyMMdd-HHmmss: Port=COM3, HeatSetpoint=68.0, CoolSetpoint=76.0"
        string BuildSettingsText() =>
            $"Port={savedPort}, " +
            $"HeatSetpoint={heatSetpoint.ToString("F1", CultureInfo.InvariantCulture)}, " +
            $"CoolSetpoint={coolSetpoint.ToString("F1", CultureInfo.InvariantCulture)}";

        // Reads every saved line in order, so the newest values win
        void LoadSettings()
        {
            try
            {
                if (!File.Exists(SettingsPath)) return;
                foreach (string line in File.ReadAllLines(SettingsPath))
                {
                    int colon = line.IndexOf(": ");
                    string data = colon >= 0 ? line[(colon + 2)..] : line;
                    foreach (string pair in data.Split(','))
                    {
                        int eq = pair.IndexOf('=');
                        if (eq < 0) continue;
                        string key = pair[..eq].Trim();
                        string val = pair[(eq + 1)..].Trim();
                        switch (key)
                        {
                            case "Port": savedPort = val; break;
                            case "HeatSetpoint":
                                if (double.TryParse(val, NumberStyles.Float, CultureInfo.InvariantCulture, out double h))
                                    heatSetpoint = ClampSetpoint(h);
                                break;
                            case "CoolSetpoint":
                                if (double.TryParse(val, NumberStyles.Float, CultureInfo.InvariantCulture, out double c))
                                    coolSetpoint = ClampSetpoint(c);
                                break;
                        }
                    }
                }
                lastSavedSettings = BuildSettingsText();
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
        }

        // Creates the file the first time, then adds a new line to it
        // (skipped if nothing changed since the last save)
        bool SaveSettings()
        {
            try
            {
                string text = BuildSettingsText();
                if (text == lastSavedSettings && File.Exists(SettingsPath)) return true;

                File.AppendAllText(SettingsPath,
                    $"{DateTime.Now:yyMMdd-HHmmss}: {text}{Environment.NewLine}");
                lastSavedSettings = text;
                return true;
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
            return false;
        }

        static void LogError(string message)
        {
            try
            {
                DateTime now = DateTime.Now;
                string path = Path.Combine(ProjectFolder, $"HVACSystem-{now:yyMMdd}.log");
                File.AppendAllText(path, $"{now:yyMMdd-HHmmss}: {message}{Environment.NewLine}");
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
        }

        // Walks up from bin\Debug\... to the folder that holds the .csproj
        static string FindProjectFolder()
        {
            DirectoryInfo? dir = new(AppContext.BaseDirectory);
            while (dir != null)
            {
                if (dir.GetFiles("*.csproj").Length > 0) return dir.FullName;
                dir = dir.Parent;
            }
            return AppContext.BaseDirectory;
        }

        // =====================================================================
        //  ISU look: palette + Segoe UI Bold, flat controls that blend in
        // =====================================================================
        void ApplyTheme()
        {
            BackColor = GrowlGreyLight;
            ForeColor = BengalBlack;

            foreach (Control c in AllControls(this))
            {
                c.Font = new Font("Segoe UI", c.Font.Size, FontStyle.Bold);
                switch (c)
                {
                    case Button b:
                        b.UseVisualStyleBackColor = false;
                        b.FlatStyle = FlatStyle.Flat;
                        b.FlatAppearance.BorderSize = 0;
                        b.FlatAppearance.MouseOverBackColor = RoarangeL;
                        b.BackColor = Roarange;
                        b.ForeColor = BengalBlack;
                        break;
                    case GroupBox g:
                        g.BackColor = GrowlGreyLight;
                        g.ForeColor = BengalBlack;
                        break;
                    case Label l:
                        l.BackColor = Color.Transparent;
                        break;
                    case ComboBox cb:
                        cb.FlatStyle = FlatStyle.Flat;
                        cb.BackColor = GrowlGreyMed;
                        break;
                    case NumericUpDown n:
                        n.BackColor = GrowlGreyMed;
                        break;
                    case MenuStrip m:
                        m.BackColor = GrowlGreyMed;
                        m.ForeColor = BengalBlack;
                        break;
                }
            }
        }

        static IEnumerable<Control> AllControls(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                yield return c;
                foreach (Control d in AllControls(c)) yield return d;
            }
        }

        // The designer only wires the two setpoint buttons, so the rest is wired here
        void WireEvents()
        {
            // Keep the simulation Fan Only box and the Fan On box in step with each other
            FanOnCheckBox.CheckedChanged += (s, e) =>
            {
                if (SimulationCheckBox.Checked) FanOnlyCheckBox.Checked = FanOnCheckBox.Checked;
            };
            FanOnlyCheckBox.CheckedChanged += (s, e) =>
            {
                if (SimulationCheckBox.Checked) FanOnCheckBox.Checked = FanOnlyCheckBox.Checked;
            };

            // simulated temperatures are automatic now, so hide the number boxes and their label
            foreach (Control c in SimulationGroupBox.Controls)
                if (c is NumericUpDown || c is Label) c.Visible = false;

            HVACTimer.Interval = 250;
            CommunicationTimer.Interval = 250;
            HVACTimer.Tick += HVACTimer_Tick;
            CommunicationTimer.Tick += CommunicationTimer_Tick;

            Load += HVACForm_Load;
            FormClosing += HVACForm_FormClosing;
            ExitButton.Click += ExitButton_Click;
            ExitMenuItem.Click += ExitButton_Click;
            SaveSettingsMenuItem.Click += SaveSettingsMenuItem_Click;
            ConnectButton.Click += ConnectButton_Click;
            ConnectMenuItem.Click += ConnectButton_Click;
            DisconnectButton.Click += DisconnectButton_Click;
            DisconnectMenuItem.Click += DisconnectButton_Click;
            PortsComboBox.SelectedIndexChanged += PortsComboBox_SelectedIndexChanged;
            PortsStripComboBox.SelectedIndexChanged += PortsStripComboBox_SelectedIndexChanged;
            SetupHeatingRadioButton.CheckedChanged += SetupRadioButton_CheckedChanged;
            SetupCoolingRadioButton.CheckedChanged += SetupRadioButton_CheckedChanged;
            HeatToLabel.Click += (s, e) => SetupHeatingRadioButton.Checked = true;
            CoolToLabel.Click += (s, e) => SetupCoolingRadioButton.Checked = true;
            SimulationCheckBox.CheckedChanged += SimulationCheckBox_CheckedChanged;
        }

        // Event Handlers Below here ******************************************
        private void HVACForm_Load(object? sender, EventArgs e)
        {
            LoadSettings();
            SafetyCheckBox.Checked = true;      // simulation defaults: safe, pressure OK
            PressureCheckBox.Checked = true;
            PortsComboBox.Text = savedPort;
            PortsStripComboBox.Text = savedPort;
            UpdateSetpointLabels();
            HVACTimer.Start();
            CommunicationTimer.Start();
            UpdatePortSelection();              // find the QY@ board and connect on startup
        }

        private void HVACForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            HVACTimer.Stop();
            CommunicationTimer.Stop();
            if (_serialPort.IsOpen)
            {
                try { SerialSend(DigitalWriteQyAt(0x00)); } catch (Exception) { }   // all outputs off
                ClosePort();
            }
            SaveSettings();
        }

        private void ExitButton_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveSettingsMenuItem_Click(object? sender, EventArgs e)
        {
            if (SaveSettings()) MessageBox.Show("Settings saved.", "HVAC Controller");
        }

        private void ConnectButton_Click(object? sender, EventArgs e)
        {
            userDisconnected = false;
            if (!string.IsNullOrWhiteSpace(PortsComboBox.Text)) savedPort = PortsComboBox.Text;
            UpdatePortSelection();
        }

        private void DisconnectButton_Click(object? sender, EventArgs e)
        {
            userDisconnected = true;
            if (_serialPort.IsOpen)
            {
                try { SerialSend(DigitalWriteQyAt(0x00)); } catch (Exception) { }
                ClosePort();
            }
        }

        private void PortsComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            PortsStripComboBox.Text = PortsComboBox.Text;
        }

        private void PortsStripComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            PortsComboBox.Text = PortsStripComboBox.Text;
        }

        private void SetupRadioButton_CheckedChanged(object? sender, EventArgs e)
        {
            if (SetupHeatingRadioButton.Checked) editingHeat = true;
            else if (SetupCoolingRadioButton.Checked) editingHeat = false;
            UpdateSetpointLabels();
        }

        private void SimulationCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            ResetCycle();
            latestInputs = null;
            lastSimUpdate = DateTime.Now;
        }

        private void TempUpButton_Click(object sender, EventArgs e)
        {
            AdjustSetpoint(+SetpointStep);
        }

        private void TempDownButton_Click(object sender, EventArgs e)
        {
            AdjustSetpoint(-SetpointStep);
        }

        // HVAC logic, clock and display (every 250 ms)
        private void HVACTimer_Tick(object? sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            ClockLabel.Text = now.ToString("hh:mm tt", CultureInfo.InvariantCulture);

            bool sim = SimulationCheckBox.Checked;
            foreach (Control c in SimulationGroupBox.Controls)
                if (c != SimulationCheckBox) c.Enabled = sim;

            // Mode radio buttons and Fan On box are clickable in simulation only;
            // with the real board they just mirror the physical switches
            HeatRadioButton.AutoCheck = CoolRadioButton.AutoCheck =
                OffRadioButton.AutoCheck = FanOnCheckBox.AutoCheck = sim;

            UpdateCommStatus();

            Inputs? inputs = sim ? ReadSimulation(now) : latestInputs;
            if (inputs == null) { ShowNoInputs(); return; }
            RunControl(inputs, now);
        }

        // Board I/O and connection monitoring (every 250 ms)
        private void CommunicationTimer_Tick(object? sender, EventArgs e)
        {
            if (SimulationCheckBox.Checked) return;

            if (!_serialPort.IsOpen)
            {
                latestInputs = null;
                if (!userDisconnected && !scanning && DateTime.Now - lastScan >= ReconnectInterval)
                    UpdatePortSelection();      // retry every 10 s
                return;
            }

            try
            {
                latestInputs = ReadBoardInputs();
                SerialSend(DigitalWriteQyAt(outputByte));
                failCount = 0;
            }
            catch (Exception)
            {
                latestInputs = null;
                if (++failCount >= 3) CommLost();
            }
        }
    }
}