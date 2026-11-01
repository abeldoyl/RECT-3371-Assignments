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
            DisplayPictureBox = new PictureBox();
            SampleRatesComboBox = new ComboBox();
            SampleRateLabel = new Label();
            DisplayDataLabel = new Label();
            DisplayComboBox = new ComboBox();
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
            // DisplayPictureBox
            // 
            DisplayPictureBox.Location = new Point(222, 33);
            DisplayPictureBox.Name = "DisplayPictureBox";
            DisplayPictureBox.Size = new Size(566, 349);
            DisplayPictureBox.TabIndex = 15;
            DisplayPictureBox.TabStop = false;
            // 
            // SampleRatesComboBox
            // 
            SampleRatesComboBox.FormattingEnabled = true;
            SampleRatesComboBox.Location = new Point(12, 56);
            SampleRatesComboBox.Name = "SampleRatesComboBox";
            SampleRatesComboBox.Size = new Size(121, 23);
            SampleRatesComboBox.TabIndex = 16;
            // 
            // SampleRateLabel
            // 
            SampleRateLabel.AutoSize = true;
            SampleRateLabel.Location = new Point(12, 38);
            SampleRateLabel.Name = "SampleRateLabel";
            SampleRateLabel.Size = new Size(77, 15);
            SampleRateLabel.TabIndex = 17;
            SampleRateLabel.Text = "Sample Rates";
            // 
            // DisplayDataLabel
            // 
            DisplayDataLabel.AutoSize = true;
            DisplayDataLabel.Location = new Point(12, 82);
            DisplayDataLabel.Name = "DisplayDataLabel";
            DisplayDataLabel.Size = new Size(72, 15);
            DisplayDataLabel.TabIndex = 18;
            DisplayDataLabel.Text = "Display Data";
            // 
            // DisplayComboBox
            // 
            DisplayComboBox.FormattingEnabled = true;
            DisplayComboBox.Location = new Point(12, 100);
            DisplayComboBox.Name = "DisplayComboBox";
            DisplayComboBox.Size = new Size(121, 23);
            DisplayComboBox.TabIndex = 19;
            // 
            // DataloggerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
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
        private PictureBox DisplayPictureBox;
        private ComboBox SampleRatesComboBox;
        private Label SampleRateLabel;
        private Label DisplayDataLabel;
        private ComboBox DisplayComboBox;
    }
}
