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
            TopMenuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            ExitButton = new Button();
            SaveButton = new Button();
            StopButton = new Button();
            StartButton = new Button();
            StatusStrip = new StatusStrip();
            StatusStripLabel = new ToolStripStatusLabel();
            TenMinRadioButton = new RadioButton();
            FiveMinRradioButton = new RadioButton();
            ThirtySecRadioButton = new RadioButton();
            OneMinRadioButton = new RadioButton();
            FiveHundMilRadioButton = new RadioButton();
            OneSecRadioButton = new RadioButton();
            FiveSecRadioButton = new RadioButton();
            TenSecRadioButton = new RadioButton();
            OneHundMilRadioButton = new RadioButton();
            TwoHundMilRadioButton = new RadioButton();
            DisplayPictureBox = new PictureBox();
            TopMenuStrip.SuspendLayout();
            StatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DisplayPictureBox).BeginInit();
            SuspendLayout();
            // 
            // TopMenuStrip
            // 
            TopMenuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, settingsToolStripMenuItem });
            TopMenuStrip.Location = new Point(0, 0);
            TopMenuStrip.Name = "TopMenuStrip";
            TopMenuStrip.Size = new Size(800, 24);
            TopMenuStrip.TabIndex = 1;
            TopMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(61, 20);
            settingsToolStripMenuItem.Text = "Settings";
            // 
            // ExitButton
            // 
            ExitButton.Location = new Point(699, 388);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(89, 37);
            ExitButton.TabIndex = 3;
            ExitButton.Text = "E&xit";
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(604, 388);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(89, 37);
            SaveButton.TabIndex = 2;
            SaveButton.Text = "S&ave";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Click += SaveButton_Click;
            // 
            // StopButton
            // 
            StopButton.Location = new Point(509, 388);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(89, 37);
            StopButton.TabIndex = 1;
            StopButton.Text = "S&top";
            StopButton.UseVisualStyleBackColor = true;
            StopButton.Click += StopButton_Click;
            // 
            // StartButton
            // 
            StartButton.Location = new Point(414, 388);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(89, 37);
            StartButton.TabIndex = 0;
            StartButton.Text = "&Start";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click;
            // 
            // StatusStrip
            // 
            StatusStrip.Items.AddRange(new ToolStripItem[] { StatusStripLabel });
            StatusStrip.Location = new Point(0, 428);
            StatusStrip.Name = "StatusStrip";
            StatusStrip.Size = new Size(800, 22);
            StatusStrip.TabIndex = 4;
            StatusStrip.Text = "StatusStrip";
            // 
            // StatusStripLabel
            // 
            StatusStripLabel.Name = "StatusStripLabel";
            StatusStripLabel.Size = new Size(91, 17);
            StatusStripLabel.Text = "StatusStripLabel";
            // 
            // TenMinRadioButton
            // 
            TenMinRadioButton.AutoSize = true;
            TenMinRadioButton.Location = new Point(12, 43);
            TenMinRadioButton.Name = "TenMinRadioButton";
            TenMinRadioButton.Size = new Size(83, 19);
            TenMinRadioButton.TabIndex = 5;
            TenMinRadioButton.TabStop = true;
            TenMinRadioButton.Text = "10 minutes";
            TenMinRadioButton.UseVisualStyleBackColor = true;
            TenMinRadioButton.CheckedChanged += TenMinRadioButton_CheckedChanged;
            // 
            // FiveMinRradioButton
            // 
            FiveMinRradioButton.AutoSize = true;
            FiveMinRradioButton.Location = new Point(12, 68);
            FiveMinRradioButton.Name = "FiveMinRradioButton";
            FiveMinRradioButton.Size = new Size(77, 19);
            FiveMinRradioButton.TabIndex = 6;
            FiveMinRradioButton.TabStop = true;
            FiveMinRradioButton.Text = "5 minutes";
            FiveMinRradioButton.UseVisualStyleBackColor = true;
            FiveMinRradioButton.CheckedChanged += FiveMinRradioButton_CheckedChanged;
            // 
            // ThirtySecRadioButton
            // 
            ThirtySecRadioButton.AutoSize = true;
            ThirtySecRadioButton.Location = new Point(12, 117);
            ThirtySecRadioButton.Name = "ThirtySecRadioButton";
            ThirtySecRadioButton.Size = new Size(84, 19);
            ThirtySecRadioButton.TabIndex = 8;
            ThirtySecRadioButton.TabStop = true;
            ThirtySecRadioButton.Text = "30 Seconds";
            ThirtySecRadioButton.UseVisualStyleBackColor = true;
            ThirtySecRadioButton.CheckedChanged += ThirtySecRadioButton_CheckedChanged;
            // 
            // OneMinRadioButton
            // 
            OneMinRadioButton.AutoSize = true;
            OneMinRadioButton.Location = new Point(12, 93);
            OneMinRadioButton.Name = "OneMinRadioButton";
            OneMinRadioButton.Size = new Size(72, 19);
            OneMinRadioButton.TabIndex = 7;
            OneMinRadioButton.TabStop = true;
            OneMinRadioButton.Text = "1 minute";
            OneMinRadioButton.UseVisualStyleBackColor = true;
            OneMinRadioButton.CheckedChanged += OneMinRadioButton_CheckedChanged;
            // 
            // FiveHundMilRadioButton
            // 
            FiveHundMilRadioButton.AutoSize = true;
            FiveHundMilRadioButton.Location = new Point(12, 214);
            FiveHundMilRadioButton.Name = "FiveHundMilRadioButton";
            FiveHundMilRadioButton.Size = new Size(112, 19);
            FiveHundMilRadioButton.TabIndex = 12;
            FiveHundMilRadioButton.TabStop = true;
            FiveHundMilRadioButton.Text = "500 milliseconds";
            FiveHundMilRadioButton.UseVisualStyleBackColor = true;
            FiveHundMilRadioButton.CheckedChanged += FiveHundMilRadioButton_CheckedChanged;
            // 
            // OneSecRadioButton
            // 
            OneSecRadioButton.AutoSize = true;
            OneSecRadioButton.Location = new Point(12, 189);
            OneSecRadioButton.Name = "OneSecRadioButton";
            OneSecRadioButton.Size = new Size(73, 19);
            OneSecRadioButton.TabIndex = 11;
            OneSecRadioButton.TabStop = true;
            OneSecRadioButton.Text = "1 Second";
            OneSecRadioButton.UseVisualStyleBackColor = true;
            OneSecRadioButton.CheckedChanged += OneSecRadioButton_CheckedChanged;
            // 
            // FiveSecRadioButton
            // 
            FiveSecRadioButton.AutoSize = true;
            FiveSecRadioButton.Location = new Point(12, 165);
            FiveSecRadioButton.Name = "FiveSecRadioButton";
            FiveSecRadioButton.Size = new Size(78, 19);
            FiveSecRadioButton.TabIndex = 10;
            FiveSecRadioButton.TabStop = true;
            FiveSecRadioButton.Text = "5 Seconds";
            FiveSecRadioButton.UseVisualStyleBackColor = true;
            FiveSecRadioButton.CheckedChanged += FiveSecRadioButton_CheckedChanged;
            // 
            // TenSecRadioButton
            // 
            TenSecRadioButton.AutoSize = true;
            TenSecRadioButton.Location = new Point(12, 140);
            TenSecRadioButton.Name = "TenSecRadioButton";
            TenSecRadioButton.Size = new Size(84, 19);
            TenSecRadioButton.TabIndex = 9;
            TenSecRadioButton.TabStop = true;
            TenSecRadioButton.Text = "10 Seconds";
            TenSecRadioButton.UseVisualStyleBackColor = true;
            TenSecRadioButton.CheckedChanged += TenSecRadioButton_CheckedChanged;
            // 
            // OneHundMilRadioButton
            // 
            OneHundMilRadioButton.AutoSize = true;
            OneHundMilRadioButton.Location = new Point(12, 264);
            OneHundMilRadioButton.Name = "OneHundMilRadioButton";
            OneHundMilRadioButton.Size = new Size(112, 19);
            OneHundMilRadioButton.TabIndex = 14;
            OneHundMilRadioButton.TabStop = true;
            OneHundMilRadioButton.Text = "100 Milliseconds";
            OneHundMilRadioButton.UseVisualStyleBackColor = true;
            OneHundMilRadioButton.CheckedChanged += OneHundMilRadioButton_CheckedChanged;
            // 
            // TwoHundMilRadioButton
            // 
            TwoHundMilRadioButton.AutoSize = true;
            TwoHundMilRadioButton.Location = new Point(12, 239);
            TwoHundMilRadioButton.Name = "TwoHundMilRadioButton";
            TwoHundMilRadioButton.Size = new Size(112, 19);
            TwoHundMilRadioButton.TabIndex = 13;
            TwoHundMilRadioButton.TabStop = true;
            TwoHundMilRadioButton.Text = "200 Milliseconds";
            TwoHundMilRadioButton.UseVisualStyleBackColor = true;
            TwoHundMilRadioButton.CheckedChanged += TwoHundMilRadioButton_CheckedChanged;
            // 
            // DisplayPictureBox
            // 
            DisplayPictureBox.Location = new Point(222, 33);
            DisplayPictureBox.Name = "DisplayPictureBox";
            DisplayPictureBox.Size = new Size(566, 349);
            DisplayPictureBox.TabIndex = 15;
            DisplayPictureBox.TabStop = false;
            // 
            // DataloggerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DisplayPictureBox);
            Controls.Add(OneHundMilRadioButton);
            Controls.Add(TwoHundMilRadioButton);
            Controls.Add(FiveHundMilRadioButton);
            Controls.Add(OneSecRadioButton);
            Controls.Add(FiveSecRadioButton);
            Controls.Add(TenSecRadioButton);
            Controls.Add(ThirtySecRadioButton);
            Controls.Add(OneMinRadioButton);
            Controls.Add(FiveMinRradioButton);
            Controls.Add(TenMinRadioButton);
            Controls.Add(StatusStrip);
            Controls.Add(StartButton);
            Controls.Add(StopButton);
            Controls.Add(SaveButton);
            Controls.Add(ExitButton);
            Controls.Add(TopMenuStrip);
            MainMenuStrip = TopMenuStrip;
            Name = "DataloggerForm";
            Text = "Form1";
            TopMenuStrip.ResumeLayout(false);
            TopMenuStrip.PerformLayout();
            StatusStrip.ResumeLayout(false);
            StatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DisplayPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip TopMenuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private Button ExitButton;
        private Button SaveButton;
        private Button StopButton;
        private Button StartButton;
        private StatusStrip StatusStrip;
        private ToolStripStatusLabel StatusStripLabel;
        private RadioButton TenMinRadioButton;
        private RadioButton FiveMinRradioButton;
        private RadioButton ThirtySecRadioButton;
        private RadioButton OneMinRadioButton;
        private RadioButton FiveHundMilRadioButton;
        private RadioButton OneSecRadioButton;
        private RadioButton FiveSecRadioButton;
        private RadioButton TenSecRadioButton;
        private RadioButton OneHundMilRadioButton;
        private RadioButton TwoHundMilRadioButton;
        private PictureBox DisplayPictureBox;
    }
}
