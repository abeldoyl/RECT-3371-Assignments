namespace EtchOSketch
{
    partial class EtchOSketchForm
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
            fToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            selectColorToolStripMenuItem = new ToolStripMenuItem();
            drawWaveformsToolStripMenuItem = new ToolStripMenuItem();
            clearToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            MainContextMenuStrip = new ContextMenuStrip(components);
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem1 = new ToolStripMenuItem();
            editToolStripMenuItem1 = new ToolStripMenuItem();
            selectColorToolStripMenuItem1 = new ToolStripMenuItem();
            drawWaveformsToolStripMenuItem1 = new ToolStripMenuItem();
            clearToolStripMenuItem1 = new ToolStripMenuItem();
            helpToolStripMenuItem1 = new ToolStripMenuItem();
            aboutToolStripMenuItem1 = new ToolStripMenuItem();
            DisplayPictureBox = new PictureBox();
            ExitButton = new Button();
            ClearButton = new Button();
            DrawWaveformsButton = new Button();
            SelectColorButton = new Button();
            MainToolTip = new ToolTip(components);
            TopMenuStrip.SuspendLayout();
            MainContextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DisplayPictureBox).BeginInit();
            SuspendLayout();
            // 
            // TopMenuStrip
            // 
            TopMenuStrip.Items.AddRange(new ToolStripItem[] { fToolStripMenuItem, editToolStripMenuItem, helpToolStripMenuItem });
            TopMenuStrip.Location = new Point(0, 0);
            TopMenuStrip.Name = "TopMenuStrip";
            TopMenuStrip.Size = new Size(800, 24);
            TopMenuStrip.TabIndex = 1;
            TopMenuStrip.Text = "menuStrip1";
            // 
            // fToolStripMenuItem
            // 
            fToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            fToolStripMenuItem.Name = "fToolStripMenuItem";
            fToolStripMenuItem.Size = new Size(37, 20);
            fToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(92, 22);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { selectColorToolStripMenuItem, drawWaveformsToolStripMenuItem, clearToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "&Edit";
            // 
            // selectColorToolStripMenuItem
            // 
            selectColorToolStripMenuItem.Name = "selectColorToolStripMenuItem";
            selectColorToolStripMenuItem.Size = new Size(164, 22);
            selectColorToolStripMenuItem.Text = "&Select Color";
            selectColorToolStripMenuItem.Click += selectColorToolStripMenuItem_Click;
            // 
            // drawWaveformsToolStripMenuItem
            // 
            drawWaveformsToolStripMenuItem.Name = "drawWaveformsToolStripMenuItem";
            drawWaveformsToolStripMenuItem.Size = new Size(164, 22);
            drawWaveformsToolStripMenuItem.Text = "&Draw Waveforms";
            drawWaveformsToolStripMenuItem.Click += drawWaveformsToolStripMenuItem_Click;
            // 
            // clearToolStripMenuItem
            // 
            clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            clearToolStripMenuItem.Size = new Size(164, 22);
            clearToolStripMenuItem.Text = "&Clear";
            clearToolStripMenuItem.Click += clearToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(107, 22);
            aboutToolStripMenuItem.Text = "&About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // MainContextMenuStrip
            // 
            MainContextMenuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem1, helpToolStripMenuItem1 });
            MainContextMenuStrip.Name = "MainContextMenuStrip";
            MainContextMenuStrip.Size = new Size(100, 70);
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem1 });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(99, 22);
            fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem1
            // 
            exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            exitToolStripMenuItem1.Size = new Size(92, 22);
            exitToolStripMenuItem1.Text = "E&xit";
            // 
            // editToolStripMenuItem1
            // 
            editToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { selectColorToolStripMenuItem1, drawWaveformsToolStripMenuItem1, clearToolStripMenuItem1 });
            editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            editToolStripMenuItem1.Size = new Size(99, 22);
            editToolStripMenuItem1.Text = "&Edit";
            // 
            // selectColorToolStripMenuItem1
            // 
            selectColorToolStripMenuItem1.Name = "selectColorToolStripMenuItem1";
            selectColorToolStripMenuItem1.Size = new Size(164, 22);
            selectColorToolStripMenuItem1.Text = "Select Color";
            // 
            // drawWaveformsToolStripMenuItem1
            // 
            drawWaveformsToolStripMenuItem1.Name = "drawWaveformsToolStripMenuItem1";
            drawWaveformsToolStripMenuItem1.Size = new Size(164, 22);
            drawWaveformsToolStripMenuItem1.Text = "Draw Waveforms";
            // 
            // clearToolStripMenuItem1
            // 
            clearToolStripMenuItem1.Name = "clearToolStripMenuItem1";
            clearToolStripMenuItem1.Size = new Size(164, 22);
            clearToolStripMenuItem1.Text = "&Clear";
            // 
            // helpToolStripMenuItem1
            // 
            helpToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem1 });
            helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            helpToolStripMenuItem1.Size = new Size(99, 22);
            helpToolStripMenuItem1.Text = "&Help";
            // 
            // aboutToolStripMenuItem1
            // 
            aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            aboutToolStripMenuItem1.Size = new Size(107, 22);
            aboutToolStripMenuItem1.Text = "&About";
            // 
            // DisplayPictureBox
            // 
            DisplayPictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DisplayPictureBox.BackColor = Color.Gainsboro;
            DisplayPictureBox.Location = new Point(12, 27);
            DisplayPictureBox.Name = "DisplayPictureBox";
            DisplayPictureBox.Size = new Size(776, 360);
            DisplayPictureBox.TabIndex = 3;
            DisplayPictureBox.TabStop = false;
            MainToolTip.SetToolTip(DisplayPictureBox, "Use mouse to draw");
            DisplayPictureBox.MouseMove += DisplayPictureBox_MouseMove;
            // 
            // ExitButton
            // 
            ExitButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ExitButton.Location = new Point(680, 396);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(108, 53);
            ExitButton.TabIndex = 4;
            ExitButton.Text = "E&xit";
            MainToolTip.SetToolTip(ExitButton, "Leave program");
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // ClearButton
            // 
            ClearButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ClearButton.Location = new Point(566, 396);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(108, 53);
            ClearButton.TabIndex = 3;
            ClearButton.Text = "&Clear";
            MainToolTip.SetToolTip(ClearButton, "Clears screen when pressed");
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // DrawWaveformsButton
            // 
            DrawWaveformsButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            DrawWaveformsButton.Location = new Point(452, 396);
            DrawWaveformsButton.Name = "DrawWaveformsButton";
            DrawWaveformsButton.Size = new Size(108, 53);
            DrawWaveformsButton.TabIndex = 2;
            DrawWaveformsButton.Text = "&Draw Waveforms";
            MainToolTip.SetToolTip(DrawWaveformsButton, "Clears and draws a Cosine, Sine, and Tangent waveforms when pressed");
            DrawWaveformsButton.UseVisualStyleBackColor = true;
            DrawWaveformsButton.Click += DrawWaveformsButton_Click;
            // 
            // SelectColorButton
            // 
            SelectColorButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            SelectColorButton.Location = new Point(338, 396);
            SelectColorButton.Name = "SelectColorButton";
            SelectColorButton.Size = new Size(108, 53);
            SelectColorButton.TabIndex = 1;
            SelectColorButton.Text = "&Select Color";
            MainToolTip.SetToolTip(SelectColorButton, "Select the color of the pen you wish to draw with");
            SelectColorButton.UseVisualStyleBackColor = true;
            SelectColorButton.Click += SelectColorButton_Click;
            // 
            // EtchOSketchForm
            // 
            AcceptButton = DrawWaveformsButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = ClearButton;
            ClientSize = new Size(800, 461);
            ContextMenuStrip = MainContextMenuStrip;
            Controls.Add(SelectColorButton);
            Controls.Add(DrawWaveformsButton);
            Controls.Add(ClearButton);
            Controls.Add(ExitButton);
            Controls.Add(DisplayPictureBox);
            Controls.Add(TopMenuStrip);
            MainMenuStrip = TopMenuStrip;
            MinimumSize = new Size(500, 500);
            Name = "EtchOSketchForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Etch-O-Sketch";
            TopMenuStrip.ResumeLayout(false);
            TopMenuStrip.PerformLayout();
            MainContextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DisplayPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip TopMenuStrip;
        private ToolStripMenuItem fToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem selectColorToolStripMenuItem;
        private ToolStripMenuItem drawWaveformsToolStripMenuItem;
        private ToolStripMenuItem clearToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ContextMenuStrip MainContextMenuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem1;
        private ToolStripMenuItem editToolStripMenuItem1;
        private ToolStripMenuItem selectColorToolStripMenuItem1;
        private ToolStripMenuItem drawWaveformsToolStripMenuItem1;
        private ToolStripMenuItem clearToolStripMenuItem1;
        private ToolStripMenuItem helpToolStripMenuItem1;
        private ToolStripMenuItem aboutToolStripMenuItem1;
        private PictureBox DisplayPictureBox;
        private Button ExitButton;
        private Button ClearButton;
        private Button DrawWaveformsButton;
        private Button SelectColorButton;
        private ToolTip MainToolTip;
    }
}
