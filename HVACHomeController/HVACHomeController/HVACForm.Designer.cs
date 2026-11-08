namespace HVACHomeController
{
    partial class HVACForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TempUpButton = new Button();
            TempDownButton = new Button();
            TempLabel = new Label();
            TempDisplayGroupBox = new GroupBox();
            HeatToLabel = new Label();
            CoolToLabel = new Label();
            PortsComboBox = new ComboBox();
            ExitButton = new Button();
            DisconnectButton = new Button();
            ConnectButton = new Button();
            MaintenanceLabel = new Label();
            CommStatusLabel = new Label();
            FaultStatusLabel = new Label();
            PressureStatusLabel = new Label();
            SafetyStatusLabel = new Label();
            ClockLabel = new Label();
            ControlGroupBox = new GroupBox();
            OperatingModeGroupBox = new GroupBox();
            HeatRadioButton = new RadioButton();
            CoolRadioButton = new RadioButton();
            FanOnCheckBox = new CheckBox();
            OffRadioButton = new RadioButton();
            ModeStatusLabel = new Label();
            FanModeLabel = new Label();
            FanStatusLabel = new Label();
            HeatStatusLabel = new Label();
            CoolStatusLabel = new Label();
            SimulationGroupBox = new GroupBox();
            SimulationCheckBox = new CheckBox();
            SafetyCheckBox = new CheckBox();
            HeatingCheckBox = new CheckBox();
            FanOnlyCheckBox = new CheckBox();
            PressureCheckBox = new CheckBox();
            CoolingCheckBox = new CheckBox();
            MainMenuStrip = new MenuStrip();
            FileStripMenuItem = new ToolStripMenuItem();
            SaveSettingsMenuItem = new ToolStripMenuItem();
            ExitMenuItem = new ToolStripMenuItem();
            SettingsMenuItem = new ToolStripMenuItem();
            CommunicationMenuItem = new ToolStripMenuItem();
            PortsStripComboBox = new ToolStripComboBox();
            ConnectMenuItem = new ToolStripMenuItem();
            DisconnectMenuItem = new ToolStripMenuItem();
            SetupCoolingRadioButton = new RadioButton();
            SetupHeatingRadioButton = new RadioButton();
            HVACTimer = new System.Windows.Forms.Timer(components);
            CommunicationTimer = new System.Windows.Forms.Timer(components);
            LogoPictureBox = new PictureBox();
            TempDisplayGroupBox.SuspendLayout();
            ControlGroupBox.SuspendLayout();
            OperatingModeGroupBox.SuspendLayout();
            SimulationGroupBox.SuspendLayout();
            MainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LogoPictureBox).BeginInit();
            SuspendLayout();
            // 
            // TempUpButton
            // 
            TempUpButton.Font = new Font("Segoe UI", 32.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TempUpButton.Location = new Point(17, 45);
            TempUpButton.Name = "TempUpButton";
            TempUpButton.Size = new Size(76, 51);
            TempUpButton.TabIndex = 0;
            TempUpButton.Text = "^";
            TempUpButton.UseVisualStyleBackColor = true;
            TempUpButton.Click += TempUpButton_Click;
            // 
            // TempDownButton
            // 
            TempDownButton.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TempDownButton.Location = new Point(17, 102);
            TempDownButton.Name = "TempDownButton";
            TempDownButton.Size = new Size(76, 51);
            TempDownButton.TabIndex = 1;
            TempDownButton.Text = "˅";
            TempDownButton.UseVisualStyleBackColor = true;
            TempDownButton.Click += TempDownButton_Click;
            // 
            // TempLabel
            // 
            TempLabel.AutoSize = true;
            TempLabel.Font = new Font("Segoe UI", 48F, FontStyle.Bold, GraphicsUnit.Point, 0);
            TempLabel.Location = new Point(47, 19);
            TempLabel.Name = "TempLabel";
            TempLabel.Size = new Size(81, 86);
            TempLabel.TabIndex = 2;
            TempLabel.Text = "`°";
            // 
            // TempDisplayGroupBox
            // 
            TempDisplayGroupBox.Controls.Add(HeatToLabel);
            TempDisplayGroupBox.Controls.Add(CoolToLabel);
            TempDisplayGroupBox.Controls.Add(TempLabel);
            TempDisplayGroupBox.Location = new Point(117, 39);
            TempDisplayGroupBox.Name = "TempDisplayGroupBox";
            TempDisplayGroupBox.Size = new Size(246, 158);
            TempDisplayGroupBox.TabIndex = 3;
            TempDisplayGroupBox.TabStop = false;
            TempDisplayGroupBox.Text = "Room Temperature";
            // 
            // HeatToLabel
            // 
            HeatToLabel.AutoSize = true;
            HeatToLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            HeatToLabel.Location = new Point(137, 125);
            HeatToLabel.Name = "HeatToLabel";
            HeatToLabel.Size = new Size(83, 21);
            HeatToLabel.TabIndex = 4;
            HeatToLabel.Text = "Heat To `°";
            // 
            // CoolToLabel
            // 
            CoolToLabel.AutoSize = true;
            CoolToLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            CoolToLabel.Location = new Point(6, 125);
            CoolToLabel.Name = "CoolToLabel";
            CoolToLabel.Size = new Size(82, 21);
            CoolToLabel.TabIndex = 3;
            CoolToLabel.Text = "Cool To `°";
            // 
            // PortsComboBox
            // 
            PortsComboBox.FormattingEnabled = true;
            PortsComboBox.Location = new Point(6, 37);
            PortsComboBox.Margin = new Padding(3, 2, 3, 2);
            PortsComboBox.Name = "PortsComboBox";
            PortsComboBox.Size = new Size(258, 23);
            PortsComboBox.TabIndex = 45;
            // 
            // ExitButton
            // 
            ExitButton.Location = new Point(182, 63);
            ExitButton.Margin = new Padding(3, 2, 3, 2);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(82, 29);
            ExitButton.TabIndex = 44;
            ExitButton.Text = "Exit";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // DisconnectButton
            // 
            DisconnectButton.Location = new Point(94, 63);
            DisconnectButton.Margin = new Padding(3, 2, 3, 2);
            DisconnectButton.Name = "DisconnectButton";
            DisconnectButton.Size = new Size(82, 29);
            DisconnectButton.TabIndex = 43;
            DisconnectButton.Text = "Disconnect";
            DisconnectButton.UseVisualStyleBackColor = true;
            DisconnectButton.Click += DisconnectButton_Click;
            // 
            // ConnectButton
            // 
            ConnectButton.Location = new Point(6, 63);
            ConnectButton.Margin = new Padding(3, 2, 3, 2);
            ConnectButton.Name = "ConnectButton";
            ConnectButton.Size = new Size(82, 29);
            ConnectButton.TabIndex = 42;
            ConnectButton.Text = "Connect";
            ConnectButton.UseVisualStyleBackColor = true;
            ConnectButton.Click += ConnectButton_Click;
            // 
            // MaintenanceLabel
            // 
            MaintenanceLabel.AutoSize = true;
            MaintenanceLabel.Location = new Point(269, 77);
            MaintenanceLabel.Name = "MaintenanceLabel";
            MaintenanceLabel.Size = new Size(88, 15);
            MaintenanceLabel.TabIndex = 41;
            MaintenanceLabel.Text = "System Normal";
            // 
            // CommStatusLabel
            // 
            CommStatusLabel.AutoSize = true;
            CommStatusLabel.Location = new Point(6, 20);
            CommStatusLabel.Name = "CommStatusLabel";
            CommStatusLabel.Size = new Size(79, 15);
            CommStatusLabel.TabIndex = 40;
            CommStatusLabel.Text = "Disconnected";
            // 
            // FaultStatusLabel
            // 
            FaultStatusLabel.AutoSize = true;
            FaultStatusLabel.Location = new Point(269, 62);
            FaultStatusLabel.Name = "FaultStatusLabel";
            FaultStatusLabel.Size = new Size(61, 15);
            FaultStatusLabel.TabIndex = 39;
            FaultStatusLabel.Text = "NO FAULT";
            // 
            // PressureStatusLabel
            // 
            PressureStatusLabel.AutoSize = true;
            PressureStatusLabel.Location = new Point(269, 43);
            PressureStatusLabel.Name = "PressureStatusLabel";
            PressureStatusLabel.Size = new Size(23, 15);
            PressureStatusLabel.TabIndex = 38;
            PressureStatusLabel.Text = "OK";
            // 
            // SafetyStatusLabel
            // 
            SafetyStatusLabel.AutoSize = true;
            SafetyStatusLabel.Location = new Point(269, 28);
            SafetyStatusLabel.Name = "SafetyStatusLabel";
            SafetyStatusLabel.Size = new Size(33, 15);
            SafetyStatusLabel.TabIndex = 37;
            SafetyStatusLabel.Text = "SAFE";
            // 
            // ClockLabel
            // 
            ClockLabel.AutoSize = true;
            ClockLabel.Location = new Point(205, 19);
            ClockLabel.Name = "ClockLabel";
            ClockLabel.Size = new Size(34, 15);
            ClockLabel.TabIndex = 36;
            ClockLabel.Text = "12:00";
            // 
            // ControlGroupBox
            // 
            ControlGroupBox.Controls.Add(PortsComboBox);
            ControlGroupBox.Controls.Add(ExitButton);
            ControlGroupBox.Controls.Add(DisconnectButton);
            ControlGroupBox.Controls.Add(ConnectButton);
            ControlGroupBox.Controls.Add(MaintenanceLabel);
            ControlGroupBox.Controls.Add(CommStatusLabel);
            ControlGroupBox.Controls.Add(FaultStatusLabel);
            ControlGroupBox.Controls.Add(PressureStatusLabel);
            ControlGroupBox.Controls.Add(SafetyStatusLabel);
            ControlGroupBox.Controls.Add(ClockLabel);
            ControlGroupBox.Location = new Point(6, 271);
            ControlGroupBox.Name = "ControlGroupBox";
            ControlGroupBox.Size = new Size(496, 98);
            ControlGroupBox.TabIndex = 46;
            ControlGroupBox.TabStop = false;
            ControlGroupBox.Text = "Controls";
            // 
            // OperatingModeGroupBox
            // 
            OperatingModeGroupBox.Controls.Add(HeatRadioButton);
            OperatingModeGroupBox.Controls.Add(CoolRadioButton);
            OperatingModeGroupBox.Controls.Add(FanOnCheckBox);
            OperatingModeGroupBox.Controls.Add(OffRadioButton);
            OperatingModeGroupBox.Controls.Add(ModeStatusLabel);
            OperatingModeGroupBox.Controls.Add(FanModeLabel);
            OperatingModeGroupBox.Controls.Add(FanStatusLabel);
            OperatingModeGroupBox.Controls.Add(HeatStatusLabel);
            OperatingModeGroupBox.Controls.Add(CoolStatusLabel);
            OperatingModeGroupBox.Location = new Point(436, 39);
            OperatingModeGroupBox.Margin = new Padding(3, 2, 3, 2);
            OperatingModeGroupBox.Name = "OperatingModeGroupBox";
            OperatingModeGroupBox.Padding = new Padding(3, 2, 3, 2);
            OperatingModeGroupBox.Size = new Size(128, 228);
            OperatingModeGroupBox.TabIndex = 49;
            OperatingModeGroupBox.TabStop = false;
            OperatingModeGroupBox.Text = "Operating Modes";
            // 
            // HeatRadioButton
            // 
            HeatRadioButton.AutoSize = true;
            HeatRadioButton.Location = new Point(10, 19);
            HeatRadioButton.Margin = new Padding(3, 2, 3, 2);
            HeatRadioButton.Name = "HeatRadioButton";
            HeatRadioButton.Size = new Size(67, 19);
            HeatRadioButton.TabIndex = 31;
            HeatRadioButton.TabStop = true;
            HeatRadioButton.Text = "Heating";
            HeatRadioButton.UseVisualStyleBackColor = true;
            // 
            // CoolRadioButton
            // 
            CoolRadioButton.AutoSize = true;
            CoolRadioButton.Location = new Point(10, 42);
            CoolRadioButton.Margin = new Padding(3, 2, 3, 2);
            CoolRadioButton.Name = "CoolRadioButton";
            CoolRadioButton.Size = new Size(67, 19);
            CoolRadioButton.TabIndex = 32;
            CoolRadioButton.TabStop = true;
            CoolRadioButton.Text = "Cooling";
            CoolRadioButton.UseVisualStyleBackColor = true;
            // 
            // FanOnCheckBox
            // 
            FanOnCheckBox.AutoSize = true;
            FanOnCheckBox.Location = new Point(10, 88);
            FanOnCheckBox.Margin = new Padding(3, 2, 3, 2);
            FanOnCheckBox.Name = "FanOnCheckBox";
            FanOnCheckBox.Size = new Size(64, 19);
            FanOnCheckBox.TabIndex = 34;
            FanOnCheckBox.Text = "Fan On";
            FanOnCheckBox.UseVisualStyleBackColor = true;
            // 
            // OffRadioButton
            // 
            OffRadioButton.AutoSize = true;
            OffRadioButton.Location = new Point(10, 64);
            OffRadioButton.Margin = new Padding(3, 2, 3, 2);
            OffRadioButton.Name = "OffRadioButton";
            OffRadioButton.Size = new Size(42, 19);
            OffRadioButton.TabIndex = 33;
            OffRadioButton.TabStop = true;
            OffRadioButton.Text = "Off";
            OffRadioButton.UseVisualStyleBackColor = true;
            // 
            // ModeStatusLabel
            // 
            ModeStatusLabel.AutoSize = true;
            ModeStatusLabel.Location = new Point(10, 111);
            ModeStatusLabel.Name = "ModeStatusLabel";
            ModeStatusLabel.Size = new Size(65, 15);
            ModeStatusLabel.TabIndex = 11;
            ModeStatusLabel.Text = "Mode: OFF";
            // 
            // FanModeLabel
            // 
            FanModeLabel.AutoSize = true;
            FanModeLabel.Location = new Point(10, 126);
            FanModeLabel.Name = "FanModeLabel";
            FanModeLabel.Size = new Size(97, 15);
            FanModeLabel.TabIndex = 12;
            FanModeLabel.Text = "Fan Mode: AUTO";
            // 
            // FanStatusLabel
            // 
            FanStatusLabel.AutoSize = true;
            FanStatusLabel.Location = new Point(10, 141);
            FanStatusLabel.Name = "FanStatusLabel";
            FanStatusLabel.Size = new Size(53, 15);
            FanStatusLabel.TabIndex = 13;
            FanStatusLabel.Text = "Fan: OFF";
            // 
            // HeatStatusLabel
            // 
            HeatStatusLabel.AutoSize = true;
            HeatStatusLabel.Location = new Point(10, 157);
            HeatStatusLabel.Name = "HeatStatusLabel";
            HeatStatusLabel.Size = new Size(76, 15);
            HeatStatusLabel.TabIndex = 14;
            HeatStatusLabel.Text = "Heating: OFF";
            // 
            // CoolStatusLabel
            // 
            CoolStatusLabel.AutoSize = true;
            CoolStatusLabel.Location = new Point(10, 171);
            CoolStatusLabel.Name = "CoolStatusLabel";
            CoolStatusLabel.Size = new Size(76, 15);
            CoolStatusLabel.TabIndex = 15;
            CoolStatusLabel.Text = "Cooling: OFF";
            // 
            // SimulationGroupBox
            // 
            SimulationGroupBox.Controls.Add(SimulationCheckBox);
            SimulationGroupBox.Controls.Add(SafetyCheckBox);
            SimulationGroupBox.Controls.Add(HeatingCheckBox);
            SimulationGroupBox.Controls.Add(FanOnlyCheckBox);
            SimulationGroupBox.Controls.Add(PressureCheckBox);
            SimulationGroupBox.Controls.Add(CoolingCheckBox);
            SimulationGroupBox.Location = new Point(569, 39);
            SimulationGroupBox.Margin = new Padding(3, 2, 3, 2);
            SimulationGroupBox.Name = "SimulationGroupBox";
            SimulationGroupBox.Padding = new Padding(3, 2, 3, 2);
            SimulationGroupBox.Size = new Size(219, 228);
            SimulationGroupBox.TabIndex = 48;
            SimulationGroupBox.TabStop = false;
            SimulationGroupBox.Text = "Simulation Check Boxes";
            // 
            // SimulationCheckBox
            // 
            SimulationCheckBox.AutoSize = true;
            SimulationCheckBox.Location = new Point(15, 15);
            SimulationCheckBox.Margin = new Padding(3, 2, 3, 2);
            SimulationCheckBox.Name = "SimulationCheckBox";
            SimulationCheckBox.Size = new Size(117, 19);
            SimulationCheckBox.TabIndex = 26;
            SimulationCheckBox.Text = "Simulation Mode";
            SimulationCheckBox.UseVisualStyleBackColor = true;
            // 
            // SafetyCheckBox
            // 
            SafetyCheckBox.AutoSize = true;
            SafetyCheckBox.Location = new Point(15, 38);
            SafetyCheckBox.Margin = new Padding(3, 2, 3, 2);
            SafetyCheckBox.Name = "SafetyCheckBox";
            SafetyCheckBox.Size = new Size(107, 19);
            SafetyCheckBox.TabIndex = 21;
            SafetyCheckBox.Text = "Safety Interlock";
            SafetyCheckBox.UseVisualStyleBackColor = true;
            // 
            // HeatingCheckBox
            // 
            HeatingCheckBox.AutoSize = true;
            HeatingCheckBox.Location = new Point(15, 105);
            HeatingCheckBox.Margin = new Padding(3, 2, 3, 2);
            HeatingCheckBox.Name = "HeatingCheckBox";
            HeatingCheckBox.Size = new Size(113, 19);
            HeatingCheckBox.TabIndex = 22;
            HeatingCheckBox.Text = "Heating Enabled";
            HeatingCheckBox.UseVisualStyleBackColor = true;
            // 
            // FanOnlyCheckBox
            // 
            FanOnlyCheckBox.AutoSize = true;
            FanOnlyCheckBox.Location = new Point(15, 83);
            FanOnlyCheckBox.Margin = new Padding(3, 2, 3, 2);
            FanOnlyCheckBox.Name = "FanOnlyCheckBox";
            FanOnlyCheckBox.Size = new Size(73, 19);
            FanOnlyCheckBox.TabIndex = 23;
            FanOnlyCheckBox.Text = "Fan Only";
            FanOnlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // PressureCheckBox
            // 
            PressureCheckBox.AutoSize = true;
            PressureCheckBox.Location = new Point(15, 61);
            PressureCheckBox.Margin = new Padding(3, 2, 3, 2);
            PressureCheckBox.Name = "PressureCheckBox";
            PressureCheckBox.Size = new Size(89, 19);
            PressureCheckBox.TabIndex = 24;
            PressureCheckBox.Text = "Pressure OK";
            PressureCheckBox.UseVisualStyleBackColor = true;
            // 
            // CoolingCheckBox
            // 
            CoolingCheckBox.AutoSize = true;
            CoolingCheckBox.Location = new Point(15, 128);
            CoolingCheckBox.Margin = new Padding(3, 2, 3, 2);
            CoolingCheckBox.Name = "CoolingCheckBox";
            CoolingCheckBox.Size = new Size(113, 19);
            CoolingCheckBox.TabIndex = 25;
            CoolingCheckBox.Text = "Cooling Enabled";
            CoolingCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainMenuStrip
            // 
            MainMenuStrip.ImageScalingSize = new Size(20, 20);
            MainMenuStrip.Items.AddRange(new ToolStripItem[] { FileStripMenuItem, SettingsMenuItem });
            MainMenuStrip.Location = new Point(0, 0);
            MainMenuStrip.Name = "MainMenuStrip";
            MainMenuStrip.Padding = new Padding(5, 2, 0, 2);
            MainMenuStrip.Size = new Size(800, 24);
            MainMenuStrip.TabIndex = 47;
            MainMenuStrip.Text = "menuStrip1";
            // 
            // FileStripMenuItem
            // 
            FileStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { SaveSettingsMenuItem, ExitMenuItem });
            FileStripMenuItem.Name = "FileStripMenuItem";
            FileStripMenuItem.Size = new Size(37, 20);
            FileStripMenuItem.Text = "File";
            // 
            // SaveSettingsMenuItem
            // 
            SaveSettingsMenuItem.Name = "SaveSettingsMenuItem";
            SaveSettingsMenuItem.Size = new Size(143, 22);
            SaveSettingsMenuItem.Text = "Save Settings";
            // 
            // ExitMenuItem
            // 
            ExitMenuItem.Name = "ExitMenuItem";
            ExitMenuItem.Size = new Size(143, 22);
            ExitMenuItem.Text = "Exit";
            // 
            // SettingsMenuItem
            // 
            SettingsMenuItem.DropDownItems.AddRange(new ToolStripItem[] { CommunicationMenuItem });
            SettingsMenuItem.Name = "SettingsMenuItem";
            SettingsMenuItem.Size = new Size(61, 20);
            SettingsMenuItem.Text = "Settings";
            // 
            // CommunicationMenuItem
            // 
            CommunicationMenuItem.DropDownItems.AddRange(new ToolStripItem[] { PortsStripComboBox, ConnectMenuItem, DisconnectMenuItem });
            CommunicationMenuItem.Name = "CommunicationMenuItem";
            CommunicationMenuItem.Size = new Size(194, 22);
            CommunicationMenuItem.Text = "Communication Setup";
            // 
            // PortsStripComboBox
            // 
            PortsStripComboBox.Name = "PortsStripComboBox";
            PortsStripComboBox.Size = new Size(121, 23);
            // 
            // ConnectMenuItem
            // 
            ConnectMenuItem.Name = "ConnectMenuItem";
            ConnectMenuItem.Size = new Size(181, 22);
            ConnectMenuItem.Text = "Connect";
            // 
            // DisconnectMenuItem
            // 
            DisconnectMenuItem.Name = "DisconnectMenuItem";
            DisconnectMenuItem.Size = new Size(181, 22);
            DisconnectMenuItem.Text = "Disconnect";
            // 
            // SetupCoolingRadioButton
            // 
            SetupCoolingRadioButton.AutoSize = true;
            SetupCoolingRadioButton.Location = new Point(12, 166);
            SetupCoolingRadioButton.Name = "SetupCoolingRadioButton";
            SetupCoolingRadioButton.Size = new Size(100, 19);
            SetupCoolingRadioButton.TabIndex = 50;
            SetupCoolingRadioButton.TabStop = true;
            SetupCoolingRadioButton.Text = "Setup Cooling";
            SetupCoolingRadioButton.UseVisualStyleBackColor = true;
            // 
            // SetupHeatingRadioButton
            // 
            SetupHeatingRadioButton.AutoSize = true;
            SetupHeatingRadioButton.Location = new Point(11, 188);
            SetupHeatingRadioButton.Name = "SetupHeatingRadioButton";
            SetupHeatingRadioButton.Size = new Size(100, 19);
            SetupHeatingRadioButton.TabIndex = 51;
            SetupHeatingRadioButton.TabStop = true;
            SetupHeatingRadioButton.Text = "Setup Heating";
            SetupHeatingRadioButton.UseVisualStyleBackColor = true;
            // 
            // LogoPictureBox
            // 
            LogoPictureBox.Image = Properties.Resources.images;
            LogoPictureBox.InitialImage = Properties.Resources.images;
            LogoPictureBox.Location = new Point(698, 279);
            LogoPictureBox.Name = "LogoPictureBox";
            LogoPictureBox.Size = new Size(90, 90);
            LogoPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            LogoPictureBox.TabIndex = 52;
            LogoPictureBox.TabStop = false;
            // 
            // HVACForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(230, 231, 232);
            ClientSize = new Size(800, 383);
            Controls.Add(LogoPictureBox);
            Controls.Add(SetupHeatingRadioButton);
            Controls.Add(SetupCoolingRadioButton);
            Controls.Add(OperatingModeGroupBox);
            Controls.Add(SimulationGroupBox);
            Controls.Add(MainMenuStrip);
            Controls.Add(ControlGroupBox);
            Controls.Add(TempDisplayGroupBox);
            Controls.Add(TempDownButton);
            Controls.Add(TempUpButton);
            Name = "HVACForm";
            Text = "Form1";
            TempDisplayGroupBox.ResumeLayout(false);
            TempDisplayGroupBox.PerformLayout();
            ControlGroupBox.ResumeLayout(false);
            ControlGroupBox.PerformLayout();
            OperatingModeGroupBox.ResumeLayout(false);
            OperatingModeGroupBox.PerformLayout();
            SimulationGroupBox.ResumeLayout(false);
            SimulationGroupBox.PerformLayout();
            MainMenuStrip.ResumeLayout(false);
            MainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)LogoPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button TempUpButton;
        private Button TempDownButton;
        private Label TempLabel;
        private GroupBox TempDisplayGroupBox;
        private Label HeatToLabel;
        private Label CoolToLabel;
        private ComboBox PortsComboBox;
        private Button ExitButton;
        private Button DisconnectButton;
        private Button ConnectButton;
        private Label MaintenanceLabel;
        private Label CommStatusLabel;
        private Label FaultStatusLabel;
        private Label PressureStatusLabel;
        private Label SafetyStatusLabel;
        private Label ClockLabel;
        private GroupBox ControlGroupBox;
        private GroupBox OperatingModeGroupBox;
        private RadioButton HeatRadioButton;
        private RadioButton CoolRadioButton;
        private CheckBox FanOnCheckBox;
        private RadioButton OffRadioButton;
        private Label ModeStatusLabel;
        private Label FanModeLabel;
        private Label FanStatusLabel;
        private Label HeatStatusLabel;
        private Label CoolStatusLabel;
        private GroupBox SimulationGroupBox;
        private CheckBox SimulationCheckBox;
        private CheckBox SafetyCheckBox;
        private CheckBox HeatingCheckBox;
        private CheckBox FanOnlyCheckBox;
        private CheckBox PressureCheckBox;
        private CheckBox CoolingCheckBox;
        private MenuStrip MainMenuStrip;
        private ToolStripMenuItem FileStripMenuItem;
        private ToolStripMenuItem SaveSettingsMenuItem;
        private ToolStripMenuItem ExitMenuItem;
        private ToolStripMenuItem SettingsMenuItem;
        private ToolStripMenuItem CommunicationMenuItem;
        private ToolStripComboBox PortsStripComboBox;
        private ToolStripMenuItem ConnectMenuItem;
        private ToolStripMenuItem DisconnectMenuItem;
        private RadioButton SetupCoolingRadioButton;
        private RadioButton SetupHeatingRadioButton;
        private System.Windows.Forms.Timer HVACTimer;
        private System.Windows.Forms.Timer CommunicationTimer;
        private PictureBox LogoPictureBox;
    }
}
