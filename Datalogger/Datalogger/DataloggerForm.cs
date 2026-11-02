/* Abel Doyle
 * RCET 3371
 * Fall Semester 2026
 * Datalogger
 * https://github.com/abeldoyl/RECT-3371-Assignments/tree/main/Datalogger
 */
using System.Globalization;
using System.IO.Ports;

namespace Datalogger
{
    public partial class DataloggerForm : Form
    {
        // One analog sample: channel 1-4, the two data bytes, and when it was taken.
        private readonly record struct Sample(int Channel, byte HighByte, byte LowByte, DateTime Time)
        {
            public int Counts => (HighByte << 8) | LowByte;
        }

        // ---- Constants -------------------------------------------------------

        // Selectable sample rates: text shown to the user and interval in milliseconds.
        private static readonly (string Text, int Milliseconds)[] SampleRates =
        {
            ("10 minutes", 600000), ("5 minutes", 300000), ("1 minute", 60000),
            ("30 seconds", 30000), ("10 seconds", 10000), ("5 seconds", 5000),
            ("1 second", 1000), ("500 ms", 500), ("200 ms", 200), ("100 ms", 100)
        };

        private const int DefaultRateIndex = 6;          // 1 second
        private const int ChannelCount = 4;              // AN1 - AN4
        private const int PacketLength = 7;              // '$' '$' 'A' 'N' ch high low
        private const int MaxCounts = 1023;              // 10-bit ADC
        private const double ReferenceVolts = 5.0;       // ADC reference voltage
        private const int LastWindowSeconds = 30;
        private const string LogTimestampFormat = "yyMMddHHmmssff";
        private const string AppTitle = "PIC Data Logger";

        private static readonly Color[] ChannelColors =
            { Color.Blue, Color.Red, Color.Green, Color.DarkOrange };

        // ---- Fields ----------------------------------------------------------

        private readonly SerialPort picSerialPort = new SerialPort();
        private readonly System.Windows.Forms.Timer sampleTimer = new System.Windows.Forms.Timer();
        private readonly List<Sample> samples = new List<Sample>();

        // Receive buffer and latest raw value per channel (-1 = nothing new).
        // These are touched by the serial thread, so access is locked.
        private readonly object receiveLock = new object();
        private readonly List<byte> receiveBuffer = new List<byte>();
        private readonly int[] latestRaw = { -1, -1, -1, -1 };

        private string selectedPortName = "";
        private string currentLogPath = "";
        private bool isSampling = false;

        public DataloggerForm()
        {
            InitializeComponent();

            this.Text = AppTitle;

            // Fill the combo boxes.
            foreach ((string text, int _) in SampleRates)
            {
                SampleRatesComboBox.Items.Add(text);
            }
            SampleRatesComboBox.SelectedIndex = DefaultRateIndex;
            DisplayComboBox.Items.Add("Entire history");
            DisplayComboBox.Items.Add("Last 30 seconds");
            DisplayComboBox.SelectedIndex = 0;

            // Wire up events that are not set in the designer.
            sampleTimer.Tick += SampleTimer_Tick;
            picSerialPort.DataReceived += PicSerialPort_DataReceived;
            DisplayPictureBox.Paint += DisplayPictureBox_Paint;
            DisplayPictureBox.Resize += (s, e) => DisplayPictureBox.Invalidate();
            SampleRatesComboBox.SelectedIndexChanged += SampleRatesComboBox_SelectedIndexChanged;
            DisplayComboBox.SelectedIndexChanged += (s, e) => DisplayPictureBox.Invalidate();
            ComPortMenuItem.DropDownOpening += ComPortMenuItem_DropDownOpening;
            FormClosing += (s, e) => StopSampling();

            SetSamplingState(false);
            UpdateStatusStrip();
        }

        // ---- Serial port -----------------------------------------------------

        // Configure the serial port for the PIC (9600 baud, 8 data bits, no parity, 1 stop bit).
        void SerialPortSetup(string portname)
        {
            picSerialPort.Close();
            picSerialPort.PortName = portname;
            picSerialPort.BaudRate = 9600;
            picSerialPort.Parity = Parity.None;
            picSerialPort.DataBits = 8;
            picSerialPort.StopBits = StopBits.One;   // StopBits.None throws an exception
        }

        // Runs on a background thread whenever bytes arrive from the PIC.
        private void PicSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int count = picSerialPort.BytesToRead;
                byte[] incoming = new byte[count];
                int read = picSerialPort.Read(incoming, 0, count);

                lock (receiveLock)
                {
                    for (int i = 0; i < read; i++)
                    {
                        receiveBuffer.Add(incoming[i]);
                    }
                    ParseReceiveBuffer();
                }
            }
            catch (Exception ex) when (ex is IOException || ex is InvalidOperationException
                                       || ex is TimeoutException || ex is UnauthorizedAccessException)
            {
                // The timer handler checks the port and reports a disconnect to the user.
            }
        }

        // Pull complete packets out of the buffer: '$' '$' 'A' 'N' <'1'-'4'> <high> <low>.
        // Bytes that do not start a valid packet are discarded to re-synchronize.
        // Caller must hold receiveLock.
        private void ParseReceiveBuffer()
        {
            while (receiveBuffer.Count >= PacketLength)
            {
                bool validHeader = receiveBuffer[0] == '$' && receiveBuffer[1] == '$'
                                   && receiveBuffer[2] == 'A' && receiveBuffer[3] == 'N'
                                   && receiveBuffer[4] >= '1' && receiveBuffer[4] <= '0' + ChannelCount;
                if (validHeader)
                {
                    int channelIndex = receiveBuffer[4] - '1';
                    latestRaw[channelIndex] = (receiveBuffer[5] << 8) | receiveBuffer[6];
                    receiveBuffer.RemoveRange(0, PacketLength);
                }
                else
                {
                    receiveBuffer.RemoveAt(0);
                }
            }
        }

        // True while the port is open and the device is still reachable.
        private bool PortIsAlive()
        {
            try
            {
                return picSerialPort.IsOpen && picSerialPort.BytesToRead >= 0;
            }
            catch (Exception ex) when (ex is IOException || ex is InvalidOperationException
                                       || ex is UnauthorizedAccessException)
            {
                return false;
            }
        }

        // ---- Sampling --------------------------------------------------------

        // Look up the selected interval; returns false if the selection is invalid.
        private bool TryGetSampleInterval(out int milliseconds)
        {
            int index = SampleRatesComboBox.SelectedIndex;
            if (index >= 0 && index < SampleRates.Length)
            {
                milliseconds = SampleRates[index].Milliseconds;
                return true;
            }
            milliseconds = 0;
            return false;
        }

        private void StartSampling()
        {
            // Validate the sample rate.
            if (!TryGetSampleInterval(out int interval))
            {
                ShowError("Please choose a valid sample rate from the list.");
                return;
            }

            // Use the only available port automatically; otherwise make the user pick one.
            if (string.IsNullOrEmpty(selectedPortName))
            {
                string[] ports = SerialPort.GetPortNames();
                if (ports.Length == 1)
                {
                    selectedPortName = ports[0];
                }
                else
                {
                    ShowError("Choose a COM port first (Settings > COM Port).");
                    return;
                }
            }

            try
            {
                SerialPortSetup(selectedPortName);
                picSerialPort.Open();
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException
                                       || ex is InvalidOperationException || ex is ArgumentException)
            {
                ShowError($"Could not open {selectedPortName}. Check that the PIC is connected "
                          + $"and the port is not in use.\n\n{ex.Message}");
                return;
            }

            lock (receiveLock)
            {
                receiveBuffer.Clear();
                for (int i = 0; i < latestRaw.Length; i++)
                {
                    latestRaw[i] = -1;
                }
            }

            sampleTimer.Interval = interval;
            sampleTimer.Start();
            SetSamplingState(true);
            UpdateStatusStrip();
        }

        private void StopSampling()
        {
            sampleTimer.Stop();
            try
            {
                if (picSerialPort.IsOpen)
                {
                    picSerialPort.Close();
                }
            }
            catch (Exception ex) when (ex is IOException || ex is InvalidOperationException
                                       || ex is UnauthorizedAccessException)
            {
                // Port is already gone (unplugged); nothing more to close.
            }
            SetSamplingState(false);
            UpdateStatusStrip();
        }

        // Each tick records the newest value received for each channel.
        private void SampleTimer_Tick(object? sender, EventArgs e)
        {
            if (!PortIsAlive())
            {
                StopSampling();
                ShowError("The PIC appears to be disconnected. Sampling has been stopped.\n"
                          + "Data collected so far is still on screen - use Save to keep it.");
                return;
            }

            DateTime now = DateTime.Now;
            lock (receiveLock)
            {
                for (int i = 0; i < ChannelCount; i++)
                {
                    if (latestRaw[i] >= 0)
                    {
                        int raw = latestRaw[i];
                        samples.Add(new Sample(i + 1, (byte)(raw >> 8), (byte)(raw & 0xFF), now));
                        latestRaw[i] = -1;   // do not record the same reading twice
                    }
                }
            }
            DisplayPictureBox.Invalidate();
        }

        // Enable/disable controls to match the sampling state.
        private void SetSamplingState(bool running)
        {
            isSampling = running;
            StartButton.Enabled = !running;
            StopButton.Enabled = running;
            ContextStartMenuItem.Enabled = !running;
            ContextStopMenuItem.Enabled = running;
            ComPortMenuItem.Enabled = !running;
        }

        // ---- Graph -----------------------------------------------------------

        private void DisplayPictureBox_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Plot area leaves room for axis labels on the left and bottom.
            Rectangle plot = new Rectangle(52, 12, DisplayPictureBox.ClientSize.Width - 52 - 12,
                                           DisplayPictureBox.ClientSize.Height - 12 - 30);
            if (plot.Width < 60 || plot.Height < 60)
            {
                return;   // too small to draw anything readable
            }

            // Work out the time window to display.
            DateTime windowEnd = DateTime.Now;
            DateTime windowStart = windowEnd.AddSeconds(-LastWindowSeconds);
            Sample[] visible = Array.Empty<Sample>();

            if (samples.Count > 0)
            {
                windowEnd = samples[^1].Time;
                bool lastOnly = DisplayComboBox.SelectedIndex == 1;
                windowStart = lastOnly ? windowEnd.AddSeconds(-LastWindowSeconds) : samples[0].Time;
                if ((windowEnd - windowStart).TotalSeconds < 1)
                {
                    windowStart = windowEnd.AddSeconds(-1);   // avoid a zero-width axis
                }
                DateTime start = windowStart;
                visible = samples.Where(s => s.Time >= start).ToArray();
            }
            double spanMs = (windowEnd - windowStart).TotalMilliseconds;

            DrawAxes(g, plot, windowStart, spanMs);
            if (visible.Length == 0)
            {
                DrawCenteredText(g, plot, "No data - press Start or open a log file");
                return;
            }

            // Draw one line per channel, clipped to the plot area.
            g.SetClip(plot);
            for (int ch = 1; ch <= ChannelCount; ch++)
            {
                PointF[] points = visible.Where(s => s.Channel == ch)
                    .Select(s => new PointF(
                        plot.Left + (float)((s.Time - windowStart).TotalMilliseconds / spanMs * plot.Width),
                        plot.Bottom - (float)s.Counts / MaxCounts * plot.Height))
                    .ToArray();
                if (points.Length == 0)
                {
                    continue;
                }

                using Pen pen = new Pen(ChannelColors[ch - 1], 1.75f);
                if (points.Length >= 2)
                {
                    g.DrawLines(pen, points);
                }
                if (points.Length <= 100)   // show individual samples when sparse
                {
                    using Brush brush = new SolidBrush(ChannelColors[ch - 1]);
                    foreach (PointF p in points)
                    {
                        g.FillEllipse(brush, p.X - 2.5f, p.Y - 2.5f, 5f, 5f);
                    }
                }
            }
            g.ResetClip();

            DrawLegend(g, plot, visible);
        }

        // Grid lines, volt labels (left) and time labels (bottom).
        private void DrawAxes(Graphics g, Rectangle plot, DateTime windowStart, double spanMs)
        {
            using Pen gridPen = new Pen(Color.Gainsboro);
            using Pen axisPen = new Pen(Color.Black);
            using Font labelFont = new Font(this.Font.FontFamily, 8f);
            using StringFormat rightAligned = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
            using StringFormat centered = new StringFormat { Alignment = StringAlignment.Center };

            // Horizontal grid: 0 V to 5 V in 1 V steps.
            for (int volts = 0; volts <= (int)ReferenceVolts; volts++)
            {
                float y = plot.Bottom - (float)(volts / ReferenceVolts) * plot.Height;
                g.DrawLine(gridPen, plot.Left, y, plot.Right, y);
                g.DrawString($"{volts:0.0} V", labelFont, Brushes.Black,
                             new RectangleF(0, y - 8, plot.Left - 4, 16), rightAligned);
            }

            // Vertical grid: time labels, using more detail for short spans.
            string format = spanMs < 10000 ? "HH:mm:ss.f" : spanMs < 3600000 ? "HH:mm:ss" : "HH:mm";
            int divisions = Math.Max(2, Math.Min(6, plot.Width / 90));
            for (int i = 0; i <= divisions; i++)
            {
                float x = plot.Left + (float)i / divisions * plot.Width;
                g.DrawLine(gridPen, x, plot.Top, x, plot.Bottom);
                DateTime t = windowStart.AddMilliseconds(spanMs * i / divisions);
                g.DrawString(t.ToString(format), labelFont, Brushes.Black,
                             new RectangleF(x - 40, plot.Bottom + 4, 80, 16), centered);
            }

            g.DrawRectangle(axisPen, plot);
        }

        // Small color key for the channels that have data.
        private void DrawLegend(Graphics g, Rectangle plot, Sample[] visible)
        {
            using Font font = new Font(this.Font.FontFamily, 8f, FontStyle.Bold);
            float x = plot.Right - 8;
            for (int ch = ChannelCount; ch >= 1; ch--)
            {
                if (!visible.Any(s => s.Channel == ch))
                {
                    continue;
                }
                string label = $"AN{ch}";
                SizeF size = g.MeasureString(label, font);
                x -= size.Width;
                using Brush brush = new SolidBrush(ChannelColors[ch - 1]);
                g.DrawString(label, font, brush, x, plot.Top + 4);
            }
        }

        private void DrawCenteredText(Graphics g, Rectangle area, string text)
        {
            using StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            g.DrawString(text, this.Font, Brushes.Gray, area, format);
        }

        // ---- Log files -------------------------------------------------------

        // Default file name: log_YYMMDDHH.log for the hour containing the given time.
        private static string DefaultLogName(DateTime time)
        {
            return $"log_{time:yyMMddHH}.log";
        }

        // Write samples as: "$$AN1",<HighByte>,<LowByte>,<YYMMDDHHMMSSmm>
        private static void WriteLogFile(string path, IEnumerable<Sample> data)
        {
            IEnumerable<string> lines = data.Select(s =>
                $"\"$$AN{s.Channel}\",{s.HighByte},{s.LowByte},{s.Time.ToString(LogTimestampFormat, CultureInfo.InvariantCulture)}");
            File.WriteAllLines(path, lines);
        }

        // Parse one log line; returns false if it is not a valid sample line.
        private static bool TryParseLogLine(string line, out Sample sample)
        {
            sample = default;
            string[] parts = line.Split(',');
            if (parts.Length != 4)
            {
                return false;
            }

            string name = parts[0].Trim().Trim('"');
            if (name.Length != 5 || !name.StartsWith("$$AN", StringComparison.Ordinal)
                || name[4] < '1' || name[4] > '0' + ChannelCount)
            {
                return false;
            }

            if (!byte.TryParse(parts[1], out byte high) || !byte.TryParse(parts[2], out byte low)
                || !DateTime.TryParseExact(parts[3].Trim(), LogTimestampFormat, CultureInfo.InvariantCulture,
                                           DateTimeStyles.None, out DateTime time))
            {
                return false;
            }

            sample = new Sample(name[4] - '0', high, low, time);
            return true;
        }

        // Save button: one file per hour of data, in the application folder.
        private void SaveSamples()
        {
            if (samples.Count == 0)
            {
                MessageBox.Show("There is no sampled data to save yet.", AppTitle,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int fileCount = 0;
                foreach (IGrouping<DateTime, Sample> hour in samples.GroupBy(s => s.Time.Date.AddHours(s.Time.Hour)))
                {
                    currentLogPath = Path.Combine(AppContext.BaseDirectory, DefaultLogName(hour.Key));
                    WriteLogFile(currentLogPath, hour);
                    fileCount++;
                }
                UpdateStatusStrip();
                MessageBox.Show($"Saved {samples.Count} samples to {fileCount} file(s) in:\n{AppContext.BaseDirectory}",
                                AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                ShowError($"The log file could not be saved.\n\n{ex.Message}");
            }
        }

        private void OpenLogMenuItem_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Log files (*.log)|*.log",
                InitialDirectory = AppContext.BaseDirectory,
                Title = "Open Log File"
            };
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            if (isSampling)
            {
                StopSampling();
            }

            try
            {
                List<Sample> loaded = new List<Sample>();
                int skipped = 0;
                foreach (string line in File.ReadLines(dialog.FileName))
                {
                    if (TryParseLogLine(line, out Sample sample))
                    {
                        loaded.Add(sample);
                    }
                    else if (line.Trim().Length > 0)
                    {
                        skipped++;
                    }
                }

                samples.Clear();
                samples.AddRange(loaded.OrderBy(s => s.Time));
                currentLogPath = dialog.FileName;
                UpdateStatusStrip();
                DisplayPictureBox.Invalidate();

                if (skipped > 0)
                {
                    MessageBox.Show($"{skipped} line(s) were not valid samples and were skipped.", AppTitle,
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                ShowError($"The log file could not be opened.\n\n{ex.Message}");
            }
        }

        private void SaveLogAsMenuItem_Click(object? sender, EventArgs e)
        {
            if (samples.Count == 0)
            {
                MessageBox.Show("There is no sampled data to save yet.", AppTitle,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "Log files (*.log)|*.log",
                InitialDirectory = AppContext.BaseDirectory,
                FileName = DefaultLogName(samples[0].Time),
                Title = "Save Log File"
            };
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                WriteLogFile(dialog.FileName, samples);
                currentLogPath = dialog.FileName;
                UpdateStatusStrip();
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                ShowError($"The log file could not be saved.\n\n{ex.Message}");
            }
        }

        // ---- Menus, status strip, helpers ------------------------------------

        // Build the COM port list each time the submenu opens so new ports show up.
        private void ComPortMenuItem_DropDownOpening(object? sender, EventArgs e)
        {
            ComPortMenuItem.DropDownItems.Clear();
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length == 0)
            {
                ComPortMenuItem.DropDownItems.Add(new ToolStripMenuItem("No ports found") { Enabled = false });
                return;
            }

            foreach (string port in ports.OrderBy(p => p))
            {
                ToolStripMenuItem item = new ToolStripMenuItem(port) { Checked = port == selectedPortName };
                item.Click += (s, args) =>
                {
                    selectedPortName = port;
                    UpdateStatusStrip();
                };
                ComPortMenuItem.DropDownItems.Add(item);
            }
        }

        private void SampleRatesComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Apply a new rate immediately if sampling is already running.
            if (isSampling && TryGetSampleInterval(out int interval))
            {
                sampleTimer.Interval = interval;
            }
            UpdateStatusStrip();
        }

        // Status strip: log file path, sampling rate, COM port state.
        private void UpdateStatusStrip()
        {
            string path = string.IsNullOrEmpty(currentLogPath)
                ? Path.Combine(AppContext.BaseDirectory, DefaultLogName(DateTime.Now))
                : currentLogPath;
            FileStatusLabel.Text = "Log file: " + path;
            RateStatusLabel.Text = "Rate: " + (SampleRatesComboBox.SelectedItem?.ToString() ?? "none");

            string portName = string.IsNullOrEmpty(selectedPortName) ? "none selected" : selectedPortName;
            PortStatusLabel.Text = isSampling && picSerialPort.IsOpen
                ? $"Port: {portName} (open)"
                : $"Port: {portName} (closed)";
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Event handlers below ---------------------------------------------------------

        private void StartButton_Click(object? sender, EventArgs e)
        {
            StartSampling();
        }

        private void StopButton_Click(object? sender, EventArgs e)
        {
            StopSampling();
        }

        private void SaveButton_Click(object? sender, EventArgs e)
        {
            SaveSamples();
        }

        private void ExitButton_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

    }
}
