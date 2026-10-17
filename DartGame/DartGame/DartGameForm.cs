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
        // (Random.Next excludes the upper number, so we add 1 to include max)
        int GetNumberFrom(int max, int min = 0)
        {
            return randy.Next(min, max + 1);
        }

        // Draws one dart: a circle with a center mark
        void DrawDart(Graphics g, int x, int y)
        {
            Pen thePen = new Pen(Color.Green, 2);
            int size = 10;

            // The circle, centered on x,y (subtract half the size to find the top-left corner)
            g.DrawEllipse(thePen, x - size / 2, y - size / 2, size, size);

            // The horizontal and vertical lines that make the cross in the middle
            g.DrawLine(thePen, x - 10, y, x + 10, y);
            g.DrawLine(thePen, x, y - 10, x, y + 10);

            // Pens use system resources, so free it when done
            thePen.Dispose();
        }

        // Throws a dart at a random position on the PictureBox
        void ThrowDart()
        {
            // Ignore Spacebar if no round is active or if we are reviewing old rounds
            if (!roundStarted || reviewMode)
            {
                return;
            }

            // Save again if the previous save failed
            // (the round is still active but all 3 darts are already thrown)
            if (throwCount == 3)
            {
                SaveRound();
                return;
            }

            // Pick a random spot anywhere inside the PictureBox
            int x = GetNumberFrom(DartboardPictureBox.ClientSize.Width);
            int y = GetNumberFrom(DartboardPictureBox.ClientSize.Height);

            // Store the dart, then count it
            dartPositions[throwCount] = new Point(x, y);
            throwCount++;
            DartCountLabel.Text = $"Darts Thrown: {throwCount} / 3";

            // Redraw right away so the new dart shows up
            DartboardPictureBox.Refresh();

            // The third dart ends the round, so save it
            if (throwCount == 3)
            {
                SaveRound();
            }
        }

        // Draws a simple dartboard: rings with a bullseye in the middle
        void DrawBoard(Graphics g)
        {
            // Find the center of the PictureBox
            int centerX = DartboardPictureBox.ClientSize.Width / 2;
            int centerY = DartboardPictureBox.ClientSize.Height / 2;

            // The board radius is the shorter half-side, minus 5 for a small margin
            int maxRadius = Math.Min(centerX, centerY) - 5;

            // If the PictureBox is too small to draw in, skip drawing
            if (maxRadius <= 0)
            {
                return;
            }

            // Ring colors, from the outside ring to the bullseye
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
                // Each ring is a little smaller than the one before it
                int radius = maxRadius - (i * maxRadius / ringColors.Length);

                Brush theBrush = new SolidBrush(ringColors[i]);

                // FillEllipse wants the top-left corner and the width/height,
                // so subtract the radius from the center and use the diameter
                g.FillEllipse(theBrush,
                    centerX - radius, centerY - radius,
                    radius * 2, radius * 2);

                theBrush.Dispose();
            }
        }

        // Paint event handler for the PictureBox
        // Windows calls this whenever the PictureBox needs to be redrawn
        private void DrawPictureBox_Paint(object sender, PaintEventArgs e)
        {
            // Board first, so the darts are drawn on top of it
            DrawBoard(e.Graphics);

            // Don't draw darts if the PictureBox is too small
            if (DartboardPictureBox.ClientSize.Width <= 20 || DartboardPictureBox.ClientSize.Height <= 20)
            {
                return;
            }

            // Draw only the darts that have been thrown so far
            for (int i = 0; i < throwCount; i++)
            {
                int x = dartPositions[i].X;
                int y = dartPositions[i].Y;

                // Keep each dart at least 10 pixels from the edge so it is not cut off
                // (this also pulls darts back in if the window was resized)
                x = Math.Max(10, Math.Min(x, DartboardPictureBox.Width - 10));
                y = Math.Max(10, Math.Min(y, DartboardPictureBox.Height - 10));
                DrawDart(e.Graphics, x, y);
            }
        }

        // Loads all saved rounds from the log file into the ListBox
        void LoadRounds()
        {
            // Start fresh so rounds are not listed twice
            savedRounds.Clear();
            ReviewListBox.Items.Clear();

            try
            {
                // Create an empty log file if there isn't one yet
                if (!File.Exists(logFile))
                {
                    File.WriteAllText(logFile, "");
                }

                string[] lines = File.ReadAllLines(logFile);

                // Round numbers already loaded, used to skip duplicates
                List<int> roundNumbers = new List<int>();

                foreach (string line in lines)
                {
                    // Each line looks like: round,x1,y1,x2,y2,x3,y3
                    string[] parts = line.Split(',');

                    // A good line has exactly 7 values
                    if (parts.Length == 7)
                    {
                        int[] numbers = new int[7];
                        bool valid = true;

                        // Check that every value is a whole number that is not negative
                        for (int i = 0; i < 7; i++)
                        {
                            if (!int.TryParse(parts[i], out numbers[i]) || numbers[i] < 0)
                            {
                                valid = false;
                                break;
                            }
                        }

                        // Only keep the line if it is valid, the round number is not 0,
                        // and that round number has not been seen already
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
            // Only save a finished round that is still active
            if (throwCount != 3 || !roundStarted)
            {
                return;
            }

            // Build one line of text: round,x1,y1,x2,y2,x3,y3
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

                // Add the new line to the end of the file
                File.AppendAllText(logFile, roundData + Environment.NewLine);

                // The round is done, so the player must click Start Round again
                roundStarted = false;

                MessageBox.Show($"Round {roundNumber} saved. Start a new round to keep playing.");
            }
            catch (Exception ex)
            {
                // roundStarted stays true here, so Spacebar will call SaveRound again
                MessageBox.Show("Error saving round: " + ex.Message +
                    "\nPress Spacebar to try saving again.");
            }
        }

        // Returns the next round number based on the log file
        int GetNextRound()
        {
            // Find the highest round number in the file, then add 1
            int lastRound = 0;

            // Create an empty log file if there isn't one yet
            if (!File.Exists(logFile))
            {
                File.WriteAllText(logFile, "");
            }

            string[] lines = File.ReadAllLines(logFile);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                // Only look at lines that have the right number of values
                if (parts.Length == 7)
                {
                    int savedNumber;

                    // The round number is the first value on the line
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
            // Reset the game state
            reviewMode = false;
            roundStarted = false;
            throwCount = 0;

            // Hide the review list and update the labels and button text
            // (the & in the button text makes the next letter an Alt+key shortcut)
            ReviewListBox.Visible = false;
            ModeLabel.Text = "Mode: Play";
            ReviewButton.Text = "&Review";
            RoundNumberLabel.Text = "Round: #";
            DartCountLabel.Text = "Darts Thrown: 0 / 3";

            // Redraw so the old darts disappear
            DartboardPictureBox.Invalidate();
        }

        // Puts everything into Review mode and clears the PictureBox
        void SetReviewMode()
        {
            // Reset the game state
            reviewMode = true;
            roundStarted = false;
            throwCount = 0;

            // Show the review list and update the labels and button text
            ReviewListBox.Visible = true;
            ModeLabel.Text = "Mode: Review";
            ReviewButton.Text = "&Play";
            RoundNumberLabel.Text = "Round: #";
            DartCountLabel.Text = "Darts Thrown: 0 / 3";

            // Fill the list with the saved rounds from the log file
            LoadRounds();

            // Redraw so the old darts disappear
            DartboardPictureBox.Invalidate();
        }


        // Event handlers here ------------------------------------------------------------

        // Closes the form
        private void QuitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Starts a new round
        private void StartRoundButton_Click(object sender, EventArgs e)
        {
            // Leave Review mode first so the two modes never mix
            if (reviewMode)
            {
                SetPlayMode();
            }

            // Only start a new round if one is not already going
            if (!roundStarted)
            {
                try
                {
                    // The new round number comes from the log file
                    roundNumber = GetNextRound();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occurred while starting the round: " + ex.Message);
                    return;
                }

                // Reset for the new round
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

        // Switches between Play mode and Review mode
        private void ReviewButton_Click(object sender, EventArgs e)
        {
            // If a round is in progress, make sure the player is OK losing it
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

            // The same button toggles between the two modes
            if (reviewMode)
            {
                SetPlayMode();
            }
            else
            {
                SetReviewMode();
            }
        }

        // Shows the darts for whichever round is clicked in the ListBox
        private void ReviewListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Do nothing outside Review mode or when nothing is selected
            if (!reviewMode || ReviewListBox.SelectedIndex == -1)
            {
                return;
            }

            try
            {
                // Get the saved line that matches the selected item
                string roundData = savedRounds[ReviewListBox.SelectedIndex];
                string[] parts = roundData.Split(',');

                // The first value is the round number
                roundNumber = int.Parse(parts[0]);

                // The next 6 values are x,y pairs: dart 0 is at parts 1 and 2,
                // dart 1 is at parts 3 and 4, dart 2 is at parts 5 and 6
                for (int i = 0; i < 3; i++)
                {
                    int x = int.Parse(parts[i * 2 + 1]);
                    int y = int.Parse(parts[i * 2 + 2]);

                    dartPositions[i] = new Point(x, y);
                }

                // A saved round always has all 3 darts
                throwCount = 3;

                RoundNumberLabel.Text = $"Round #{roundNumber}";
                DartCountLabel.Text = $"Darts Thrown: {throwCount} / 3";

                // Redraw to show the darts from this round
                DartboardPictureBox.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not show that round: " + ex.Message);
            }
        }
    }
}