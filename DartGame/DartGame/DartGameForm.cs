namespace DartGame
{
    public partial class DartGameForm : Form
    {

        // Variables Below Here ------------------------------------------------------------
        Point[] dartPositions = new Point[3];
        List<string> savedRounds = new List<string>();
        int roundNumber = 0;
        int throwCount = 0;
        bool reviewMode = false;
        bool roundStarted = false;

        // One Random for the whole program
        Random randy = new Random();

        // Location of the log file
        string logFile = Path.GetFullPath(Path.Combine(Application.StartupPath, "..", "..", "..", "DartGame.log"));

        public DartGameForm()
        {
            InitializeComponent();

            // Text for labels on startup
            RoundNumberLabel.Text = "Round: -";
            DartCountLabel.Text = "Darts: 0 / 3";
            ModeLabel.Text = "Mode: Play";

            // Spacebar throws a dart, so prevent focused buttons from receiving Spacebar
            this.KeyDown += DartGameForm_KeyDown;
            DartboardPictureBox.Paint += DrawPictureBox_Paint;
           
        }

        // Methods Below Here ------------------------------------------------------------

        // Handles the Spacebar key to throw a dart
        private void DartGameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                // Stops Spacebar from also clicking a focused button
                e.Handled = true;
                e.SuppressKeyPress = true;
                ThrowDart();
            }
        }

        // Returns a random number between min and max, inclusive
        int GetNumberFrom(int max, int min = 0)
        {
            return randy.Next(min, max + 1);
        }

        // Draws one dart: a circle with a center mark
        void DrawDart(Graphics g, int x, int y)
        {
            Pen thePen = new Pen(Color.Green, 2);
            int size = 10;

            g.DrawEllipse(thePen, x - size / 2, y - size / 2, size, size);
            g.DrawLine(thePen, x - 10, y, x + 10, y);
            g.DrawLine(thePen, x, y - 10, x, y + 10);

            thePen.Dispose();
        }

        // Throws a dart at a random position on the PictureBox
        void ThrowDart()
        {
            if (!roundStarted || reviewMode)
            {
                return;
            }

            // Save again if the previous save failed
            if (throwCount == 3)
            {
                SaveRound();
                return;
            }


            int x = GetNumberFrom(DartboardPictureBox.ClientSize.Width);
            int y = GetNumberFrom(DartboardPictureBox.ClientSize.Height);
            dartPositions[throwCount] = new Point(x, y);
            throwCount++;
            DartCountLabel.Text = $"Darts Thrown: {throwCount} / 3";
            DartboardPictureBox.Refresh();

            if (throwCount == 3)
            {
                SaveRound();
            }
        }

        // Draws a simple dartboard: rings with a bullseye in the middle
        void DrawBoard(Graphics g)
        {
            int centerX = DartboardPictureBox.ClientSize.Width / 2;
            int centerY = DartboardPictureBox.ClientSize.Height / 2;
            int maxRadius = Math.Min(centerX, centerY) - 5;

            if (maxRadius <= 0)
            {
                return;
            }

            Color[] ringColors =
            {
                Color.Black,
                Color.DarkOrange,
                Color.White,
                Color.Black,
                Color.DarkOrange
            };

            // Draw the biggest ring first, then smaller rings on top
            for (int i = 0; i < ringColors.Length; i++)
            {
                int radius = maxRadius - (i * maxRadius / ringColors.Length);

                Brush theBrush = new SolidBrush(ringColors[i]);

                g.FillEllipse(theBrush,
                    centerX - radius, centerY - radius,
                    radius * 2, radius * 2);

                theBrush.Dispose();
            }
        }

        // Paint event handler for the PictureBox
        private void DrawPictureBox_Paint(object sender, PaintEventArgs e)
        {
            DrawBoard(e.Graphics);

            if (DartboardPictureBox.ClientSize.Width <= 20 || DartboardPictureBox.ClientSize.Height <= 20)
            {
                return;
            }

            for (int i = 0; i < throwCount; i++)
            {
                int x = dartPositions[i].X;
                int y = dartPositions[i].Y;
                x = Math.Max(10, Math.Min(x, DartboardPictureBox.Width - 10));
                y = Math.Max(10, Math.Min(y, DartboardPictureBox.Height - 10));
                DrawDart(e.Graphics, x, y);
            }
        }

        // Saves the current round to the log file
        void SaveRound()
        {
            if (throwCount != 3 || !roundStarted)
            {
                return;
            }

            string roundData = $"{roundNumber}";

            for (int i = 0; i < 3; i++)
            {
                roundData += $",{dartPositions[i].X},{dartPositions[i].Y}";
            }

        }


        // Event handlers here ------------------------------------------------------------
        private void QuitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        
    }
}