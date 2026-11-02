namespace Datalogger
{
    partial class DataloggerForm
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
            TopMenuStrip = new MenuStrip();
            FileMenuItem = new ToolStripMenuItem();
            OpenLogMenuItem = new ToolStripMenuItem();
            SaveLogAsMenuItem = new ToolStripMenuItem();
            FileMenuSeparator = new ToolStripSeparator();
            ExitMenuItem = new ToolStripMenuItem();
            SettingsMenuItem = new ToolStripMenuItem();
            ComPortMenuItem = new ToolStripMenuItem();
            ExitButton = new Button();
            SaveButton = new Button();
            StopButton = new Button();
            StartButton = new Button();
            StatusStrip = new StatusStrip();
            FileStatusLabel = new ToolStripStatusLabel();
            RateStatusLabel = new ToolStripStatusLabel();
            PortStatusLabel = new ToolStripStatusLabel();
            DisplayPictureBox = new PictureBox();
            SampleRatesComboBox = new ComboBox();
            SampleRateLabel = new Label();
            DisplayDataLabel = new Label();
            DisplayComboBox = new ComboBox();
            DisplayContextMenuStrip = new ContextMenuStrip(components);
            ContextStartMenuItem = new ToolStripMenuItem();
            ContextStopMenuItem = new ToolStripMenuItem();
            ContextSaveMenuItem = new ToolStripMenuItem();
            ButtonToolTip = new ToolTip(components);
            TopMenuStrip.SuspendLayout();
            StatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DisplayPictureBox).BeginInit();
            DisplayContextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // TopMenuStrip
            // 
            TopMenuStrip.Items.AddRange(new ToolStripItem[] { FileMenuItem, SettingsMenuItem });
            TopMenuStrip.Location = new Point(0, 0);
            TopMenuStrip.Name = "TopMenuStrip";
            TopMenuStrip.Size = new Size(800, 24);
            TopMenuStrip.TabIndex = 8;
            TopMenuStrip.Text = "TopMenuStrip";
            // 
            // FileMenuItem
            // 
            FileMenuItem.DropDownItems.AddRange(new ToolStripItem[] { OpenLogMenuItem, SaveLogAsMenuItem, FileMenuSeparator, ExitMenuItem });
            FileMenuItem.Name = "FileMenuItem";
            FileMenuItem.Size = new Size(37, 20);
            FileMenuItem.Text = "&File";
            // 
            // OpenLogMenuItem
            // 
            OpenLogMenuItem.Name = "OpenLogMenuItem";
            OpenLogMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            OpenLogMenuItem.Size = new Size(190, 22);
            OpenLogMenuItem.Text = "&Open Log...";
            OpenLogMenuItem.Click += OpenLogMenuItem_Click;
            // 
            // SaveLogAsMenuItem
            // 
            SaveLogAsMenuItem.Name = "SaveLogAsMenuItem";
            SaveLogAsMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            SaveLogAsMenuItem.Size = new Size(190, 22);
            SaveLogAsMenuItem.Text = "Save Log &As...";
            SaveLogAsMenuItem.Click += SaveLogAsMenuItem_Click;
            // 
            // FileMenuSeparator
            // 
            FileMenuSeparator.Name = "FileMenuSeparator";
            FileMenuSeparator.Size = new Size(187, 6);
            // 
            // ExitMenuItem
            // 
            ExitMenuItem.Name = "ExitMenuItem";
            ExitMenuItem.Size = new Size(190, 22);
            ExitMenuItem.Text = "E&xit";
            ExitMenuItem.Click += ExitButton_Click;
            // 
            // SettingsMenuItem
            // 
            SettingsMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ComPortMenuItem });
            SettingsMenuItem.Name = "SettingsMenuItem";
            SettingsMenuItem.Size = new Size(61, 20);
            SettingsMenuItem.Text = "&Settings";
            // 
            // ComPortMenuItem
            // 
            ComPortMenuItem.Name = "ComPortMenuItem";
            ComPortMenuItem.Size = new Size(124, 22);
            ComPortMenuItem.Text = "&COM Port";
            // 
            // ExitButton
            // 
            ExitButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ExitButton.Location = new Point(699, 388);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(89, 37);
            ExitButton.TabIndex = 7;
            ExitButton.Text = "E&xit";
            ButtonToolTip.SetToolTip(ExitButton, "Close the application (Alt+X)");
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            SaveButton.Location = new Point(604, 388);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(89, 37);
            SaveButton.TabIndex = 6;
            SaveButton.Text = "S&ave";
            ButtonToolTip.SetToolTip(SaveButton, "Save the sampled data to a .log file next to the application (Alt+A)");
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // StopButton
            // 
            StopButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            StopButton.Location = new Point(509, 388);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(89, 37);
            StopButton.TabIndex = 5;
            StopButton.Text = "S&top";
            ButtonToolTip.SetToolTip(StopButton, "Stop sampling (Alt+T)");
            StopButton.UseVisualStyleBackColor = true;
            StopButton.Click += StopButton_Click;
            // 
            // StartButton
            // 
            StartButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            StartButton.Location = new Point(414, 388);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(89, 37);
            StartButton.TabIndex = 4;
            StartButton.Text = "&Start";
            ButtonToolTip.SetToolTip(StartButton, "Start sampling from the PIC (Alt+S)");
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // StatusStrip
            // 
            StatusStrip.Items.AddRange(new ToolStripItem[] { FileStatusLabel, RateStatusLabel, PortStatusLabel });
            StatusStrip.Location = new Point(0, 428);
            StatusStrip.Name = "StatusStrip";
            StatusStrip.Size = new Size(800, 22);
            StatusStrip.TabIndex = 9;
            StatusStrip.Text = "StatusStrip";
            // 
            // FileStatusLabel
            // 
            FileStatusLabel.Name = "FileStatusLabel";
            FileStatusLabel.Size = new Size(500, 17);
            FileStatusLabel.Spring = true;
            FileStatusLabel.Text = "Log file:";
            FileStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // RateStatusLabel
            // 
            RateStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.Left;
            RateStatusLabel.Name = "RateStatusLabel";
            RateStatusLabel.Size = new Size(120, 17);
            RateStatusLabel.Text = "Rate:";
            // 
            // PortStatusLabel
            // 
            PortStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.Left;
            PortStatusLabel.Name = "PortStatusLabel";
            PortStatusLabel.Size = new Size(160, 17);
            PortStatusLabel.Text = "Port:";
            // 
            // DisplayPictureBox
            // 
            DisplayPictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DisplayPictureBox.BackColor = Color.White;
            DisplayPictureBox.BorderStyle = BorderStyle.FixedSingle;
            DisplayPictureBox.ContextMenuStrip = DisplayContextMenuStrip;
            DisplayPictureBox.Location = new Point(222, 33);
            DisplayPictureBox.Name = "DisplayPictureBox";
            DisplayPictureBox.Size = new Size(566, 349);
            DisplayPictureBox.TabIndex = 15;
            DisplayPictureBox.TabStop = false;
            // 
            // SampleRatesComboBox
            // 
            SampleRatesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            SampleRatesComboBox.FormattingEnabled = true;
            SampleRatesComboBox.Location = new Point(12, 56);
            SampleRatesComboBox.Name = "SampleRatesComboBox";
            SampleRatesComboBox.Size = new Size(121, 23);
            SampleRatesComboBox.TabIndex = 1;
            ButtonToolTip.SetToolTip(SampleRatesComboBox, "Choose how often the PIC is sampled");
            // 
            // SampleRateLabel
            // 
            SampleRateLabel.AutoSize = true;
            SampleRateLabel.Location = new Point(12, 38);
            SampleRateLabel.Name = "SampleRateLabel";
            SampleRateLabel.Size = new Size(77, 15);
            SampleRateLabel.TabIndex = 0;
            SampleRateLabel.Text = "Sample &Rates";
            // 
            // DisplayDataLabel
            // 
            DisplayDataLabel.AutoSize = true;
            DisplayDataLabel.Location = new Point(12, 82);
            DisplayDataLabel.Name = "DisplayDataLabel";
            DisplayDataLabel.Size = new Size(72, 15);
            DisplayDataLabel.TabIndex = 2;
            DisplayDataLabel.Text = "&Display Data";
            // 
            // DisplayComboBox
            // 
            DisplayComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            DisplayComboBox.FormattingEnabled = true;
            DisplayComboBox.Location = new Point(12, 100);
            DisplayComboBox.Name = "DisplayComboBox";
            DisplayComboBox.Size = new Size(121, 23);
            DisplayComboBox.TabIndex = 3;
            ButtonToolTip.SetToolTip(DisplayComboBox, "Show all data or only the last 30 seconds");
            // 
            // DisplayContextMenuStrip
            // 
            DisplayContextMenuStrip.Items.AddRange(new ToolStripItem[] { ContextStartMenuItem, ContextStopMenuItem, ContextSaveMenuItem });
            DisplayContextMenuStrip.Name = "DisplayContextMenuStrip";
            DisplayContextMenuStrip.Size = new Size(100, 70);
            // 
            // ContextStartMenuItem
            // 
            ContextStartMenuItem.Name = "ContextStartMenuItem";
            ContextStartMenuItem.Size = new Size(99, 22);
            ContextStartMenuItem.Text = "&Start";
            ContextStartMenuItem.Click += StartButton_Click;
            // 
            // ContextStopMenuItem
            // 
            ContextStopMenuItem.Name = "ContextStopMenuItem";
            ContextStopMenuItem.Size = new Size(99, 22);
            ContextStopMenuItem.Text = "S&top";
            ContextStopMenuItem.Click += StopButton_Click;
            // 
            // ContextSaveMenuItem
            // 
            ContextSaveMenuItem.Name = "ContextSaveMenuItem";
            ContextSaveMenuItem.Size = new Size(99, 22);
            ContextSaveMenuItem.Text = "S&ave";
            ContextSaveMenuItem.Click += SaveButton_Click;
            // 
            // DataloggerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            ContextMenuStrip = DisplayContextMenuStrip;
            Controls.Add(DisplayComboBox);
            Controls.Add(DisplayDataLabel);
            Controls.Add(SampleRateLabel);
            Controls.Add(SampleRatesComboBox);
            Controls.Add(DisplayPictureBox);
            Controls.Add(StatusStrip);
            Controls.Add(StartButton);
            Controls.Add(StopButton);
            Controls.Add(SaveButton);
            Controls.Add(ExitButton);
            Controls.Add(TopMenuStrip);
            MainMenuStrip = TopMenuStrip;
            MinimumSize = new Size(640, 400);
            Name = "DataloggerForm";
            Text = "PIC Data Logger";
            TopMenuStrip.ResumeLayout(false);
            TopMenuStrip.PerformLayout();
            StatusStrip.ResumeLayout(false);
            StatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DisplayPictureBox).EndInit();
            DisplayContextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip TopMenuStrip;
        private ToolStripMenuItem FileMenuItem;
        private ToolStripMenuItem OpenLogMenuItem;
        private ToolStripMenuItem SaveLogAsMenuItem;
        private ToolStripSeparator FileMenuSeparator;
        private ToolStripMenuItem ExitMenuItem;
        private ToolStripMenuItem SettingsMenuItem;
        private ToolStripMenuItem ComPortMenuItem;
        private Button ExitButton;
        private Button SaveButton;
        private Button StopButton;
        private Button StartButton;
        private StatusStrip StatusStrip;
        private ToolStripStatusLabel FileStatusLabel;
        private ToolStripStatusLabel RateStatusLabel;
        private ToolStripStatusLabel PortStatusLabel;
        private PictureBox DisplayPictureBox;
        private ComboBox SampleRatesComboBox;
        private Label SampleRateLabel;
        private Label DisplayDataLabel;
        private ComboBox DisplayComboBox;
        private ContextMenuStrip DisplayContextMenuStrip;
        private ToolStripMenuItem ContextStartMenuItem;
        private ToolStripMenuItem ContextStopMenuItem;
        private ToolStripMenuItem ContextSaveMenuItem;
        private ToolTip ButtonToolTip;
    }
}