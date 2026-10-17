namespace DartGame
{
    partial class DartGameForm
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
            DartboardPictureBox = new PictureBox();
            QuitButton = new Button();
            ReviewButton = new Button();
            StartRoundButton = new Button();
            RoundNumberLabel = new Label();
            DartCountLabel = new Label();
            ReviewListBox = new ListBox();
            ModeLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)DartboardPictureBox).BeginInit();
            SuspendLayout();
            // 
            // DartboardPictureBox
            // 
            DartboardPictureBox.BackColor = SystemColors.Control;
            DartboardPictureBox.Location = new Point(12, 20);
            DartboardPictureBox.Name = "DartboardPictureBox";
            DartboardPictureBox.Size = new Size(375, 375);
            DartboardPictureBox.TabIndex = 0;
            DartboardPictureBox.TabStop = false;
            // 
            // QuitButton
            // 
            QuitButton.Location = new Point(698, 392);
            QuitButton.Name = "QuitButton";
            QuitButton.Size = new Size(90, 46);
            QuitButton.TabIndex = 2;
            QuitButton.Text = "&Quit";
            QuitButton.UseVisualStyleBackColor = true;
            QuitButton.Click += QuitButton_Click;
            // 
            // ReviewButton
            // 
            ReviewButton.Location = new Point(602, 392);
            ReviewButton.Name = "ReviewButton";
            ReviewButton.Size = new Size(90, 46);
            ReviewButton.TabIndex = 1;
            ReviewButton.Text = "&Review";
            ReviewButton.UseVisualStyleBackColor = true;
            ReviewButton.Click += ReviewButton_Click;
            // 
            // StartRoundButton
            // 
            StartRoundButton.Location = new Point(506, 392);
            StartRoundButton.Name = "StartRoundButton";
            StartRoundButton.Size = new Size(90, 46);
            StartRoundButton.TabIndex = 0;
            StartRoundButton.Text = "&Start";
            StartRoundButton.UseVisualStyleBackColor = true;
            StartRoundButton.Click += StartRoundButton_Click;
            // 
            // RoundNumberLabel
            // 
            RoundNumberLabel.AutoSize = true;
            RoundNumberLabel.Font = new Font("Source Code Pro", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            RoundNumberLabel.Location = new Point(12, 423);
            RoundNumberLabel.Name = "RoundNumberLabel";
            RoundNumberLabel.Size = new Size(57, 18);
            RoundNumberLabel.TabIndex = 4;
            RoundNumberLabel.Text = "Round #";
            // 
            // DartCountLabel
            // 
            DartCountLabel.AutoSize = true;
            DartCountLabel.Font = new Font("Source Code Pro", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DartCountLabel.Location = new Point(147, 423);
            DartCountLabel.Name = "DartCountLabel";
            DartCountLabel.Size = new Size(120, 18);
            DartCountLabel.TabIndex = 5;
            DartCountLabel.Text = "Darts thrown #/3";
            // 
            // ReviewListBox
            // 
            ReviewListBox.FormattingEnabled = true;
            ReviewListBox.Location = new Point(427, 12);
            ReviewListBox.Name = "ReviewListBox";
            ReviewListBox.Size = new Size(350, 349);
            ReviewListBox.TabIndex = 6;
            ReviewListBox.TabStop = false;
            // 
            // ModeLabel
            // 
            ModeLabel.AutoSize = true;
            ModeLabel.Font = new Font("Source Code Pro", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ModeLabel.Location = new Point(322, 423);
            ModeLabel.Name = "ModeLabel";
            ModeLabel.Size = new Size(57, 18);
            ModeLabel.TabIndex = 7;
            ModeLabel.Text = "Mode: -";
            // 
            // DartGameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ModeLabel);
            Controls.Add(ReviewListBox);
            Controls.Add(DartCountLabel);
            Controls.Add(RoundNumberLabel);
            Controls.Add(StartRoundButton);
            Controls.Add(ReviewButton);
            Controls.Add(QuitButton);
            Controls.Add(DartboardPictureBox);
            Name = "DartGameForm";
            Text = "Dart Game";
            ((System.ComponentModel.ISupportInitialize)DartboardPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox DartboardPictureBox;
        private Button QuitButton;
        private Button ReviewButton;
        private Button StartRoundButton;
        private Label RoundNumberLabel;
        private Label DartCountLabel;
        private ListBox ReviewListBox;
        private Label ModeLabel;
    }
}
