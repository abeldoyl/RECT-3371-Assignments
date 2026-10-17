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
        }

        // Methods Below Here ------------------------------------------------------------

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


        // Event Handlers Below Here ------------------------------------------------------------
        private void QuitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}