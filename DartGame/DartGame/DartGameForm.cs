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
            ReviewListBox.SelectedIndexChanged += ReviewListBox_SelectedIndexChanged;
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

        // Loads all saved rounds from the log file into the ListBox
        void LoadRounds()
        {
            savedRounds.Clear();
            ReviewListBox.Items.Clear();

            try
            {
                if (!File.Exists(logFile))
                {
                    File.WriteAllText(logFile, "");
                }

                string[] lines = File.ReadAllLines(logFile);
                List<int> roundNumbers = new List<int>();

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length == 7)
                    {
                        int[] numbers = new int[7];
                        bool valid = true;

                        for (int i = 0; i < 7; i++)
                        {
                            if (!int.TryParse(parts[i], out numbers[i]) || numbers[i] < 0)
                            {
                                valid = false;
                                break;
                            }
                        }

                        int roundNum = numbers[0];
                        if (valid && roundNum != 0 && !roundNumbers.Contains(roundNum))
                        {
                            roundNumbers.Add(roundNum);
                            savedRounds.Add(line);
                            ReviewListBox.Items.Add($"Round {roundNum}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading rounds: " + ex.Message);
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

            try
            {
                // Check that the round number has not been saved
                if (roundNumber < GetNextRound())
                {
                    MessageBox.Show("This round is already saved.");
                    roundStarted = false;
                    return;
                }

                File.AppendAllText(logFile, roundData + Environment.NewLine);

                roundStarted = false;

                MessageBox.Show($"Round {roundNumber} saved. Start a new round to keep playing.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving round: " + ex.Message +
                    "\nPress Spacebar to try saving again.");
            }
        }

        // Returns the next round number based on the log file
        int GetNextRound()
        {
            int lastRound = 0;

            if (!File.Exists(logFile))
            {
                File.WriteAllText(logFile, "");
            }

            string[] lines = File.ReadAllLines(logFile);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length == 7)
                {
                    int savedNumber;

                    if (int.TryParse(parts[0], out savedNumber))
                    {
                        if (savedNumber > lastRound)
                        {
                            lastRound = savedNumber;
                        }
                    }
                }
            }

            return lastRound + 1;
        }

        // Puts everything into Play mode and clears the PictureBox
        void SetPlayMode()
        {
            reviewMode = false;
            roundStarted = false;
            throwCount = 0;

            ReviewListBox.Visible = false;
            ModeLabel.Text = "Mode: Play";
            ReviewButton.Text = "&Review";
            RoundNumberLabel.Text = "Round: #";
            DartCountLabel.Text = "Darts Thrown: 0 / 3";

            DartboardPictureBox.Invalidate();
        }

        // Puts everything into Review mode and clears the PictureBox
        void SetReviewMode()
        {
            reviewMode = true;
            roundStarted = false;
            throwCount = 0;

            ReviewListBox.Visible = true;
            ModeLabel.Text = "Mode: Review";
            ReviewButton.Text = "&Play";
            RoundNumberLabel.Text = "Round: #";
            DartCountLabel.Text = "Darts Thrown: 0 / 3";

            LoadRounds();
            DartboardPictureBox.Invalidate();
        }


        // Event handlers here ------------------------------------------------------------
        private void QuitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StartRoundButton_Click(object sender, EventArgs e)
        {
            // Leave Review mode first so the two modes never mix
            if (reviewMode)
            {
                SetPlayMode();
            }

            if (!roundStarted)
            {
                try
                {
                    roundNumber = GetNextRound();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred while starting the round: " + ex.Message);
                    return;
                }

                throwCount = 0;
                roundStarted = true;

                RoundNumberLabel.Text = $"Round #{roundNumber}";
                DartCountLabel.Text = $"Darts Thrown: {throwCount} / 3";
                ModeLabel.Text = "Mode: Play";

                // Clears the old darts
                DartboardPictureBox.Invalidate();

                // Prevent focused buttons from receiving Spacebar
                this.ActiveControl = null;
            }
            else
            {
                MessageBox.Show("Finish the current round first.");
            }
        }

        private void ReviewButton_Click(object sender, EventArgs e)
        {
            if (roundStarted)
            {
                DialogResult result = MessageBox.Show(
                    "Discard the current round?",
                    "Confirm",
                    MessageBoxButtons.YesNo);

                if (result == DialogResult.No)
                {
                    return;
                }
            }

            if (reviewMode)
            {
                SetPlayMode();
            }
            else
            {
                SetReviewMode();
            }
        }

        private void ReviewListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!reviewMode || ReviewListBox.SelectedIndex == -1)
            {
                return;
            }

            try
            {
                string roundData = savedRounds[ReviewListBox.SelectedIndex];
                string[] parts = roundData.Split(',');

                roundNumber = int.Parse(parts[0]);

                for (int i = 0; i < 3; i++)
                {
                    int x = int.Parse(parts[i * 2 + 1]);
                    int y = int.Parse(parts[i * 2 + 2]);

                    dartPositions[i] = new Point(x, y);
                }

                throwCount = 3;

                RoundNumberLabel.Text = $"Round #{roundNumber}";
                DartCountLabel.Text = $"Darts Thrown: {throwCount} / 3";

                DartboardPictureBox.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not show that round: " + ex.Message);
            }
        }
    }
}