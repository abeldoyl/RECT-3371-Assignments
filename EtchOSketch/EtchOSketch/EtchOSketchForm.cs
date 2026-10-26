/*
Abel Doyle
RCET 2265
Spring Semester 2026
EtchOSketch
https://github.com/abeldoyl/EtchOSketch.git
*/
namespace EtchOSketch
{
    public partial class EtchOSketchForm : Form
    {
        public EtchOSketchForm()
        {
            InitializeComponent();
        }

        int oldX = 0, oldY = 0;
        Color PenColor = Color.Black;

        void UpdatePenColor()
        {
            ColorDialog PenColorDialog = new ColorDialog();
            PenColorDialog.ShowDialog();
            this.PenColor = PenColorDialog.Color;
        }


        void DrawLineSegment(int newX, int newY)
        {
            //create a Graphics object named g that draws on the picture box
            Graphics g = DisplayPictureBox.CreateGraphics();
            // create a pen to draw with
            Pen thePen = new Pen(this.PenColor);
            thePen.Width = 2;
            //draw the line here
            g.DrawLine(thePen, oldX, oldY, newX, newY);

            //free up resources
            g.Dispose();
            thePen.Dispose();
        }
        void DrawSineWave()
        {
            //create a Graphics object named g that draws on the picture box
            Graphics g = DisplayPictureBox.CreateGraphics();
            // create a pen to draw with
            Pen thePen = new Pen(Color.OrangeRed);
            thePen.Width = 2;
            int lastX = 0, lastY = 0, currentY = 0;
            float scaleX = DisplayPictureBox.Width / 360F;
            float scaleY = (DisplayPictureBox.Height / 200F) * -1;

            g.TranslateTransform(0, DisplayPictureBox.Height / 2);
            g.ScaleTransform(scaleX, scaleY);
            for (int currentX = 0; currentX < 360; currentX++)
            {
                currentY = (int)Math.Round(100 * Math.Sin((Math.PI / 180) * currentX));
                g.DrawLine(thePen, lastX, lastY, currentX, currentY);
                lastX = currentX;
                lastY = currentY;
            }
            //free up resources
            g.Dispose();
            thePen.Dispose();
        }
        void DrawCosineWave()
        {
            //create a graphics object named g that draws on the picture box
            Graphics g = DisplayPictureBox.CreateGraphics();
            //create a pen to draw with
            Pen thePen = new Pen(Color.Green);
            thePen.Width = 2;
            int lastX = 0, lastY = 0, currentY = 0;
            float scaleX = DisplayPictureBox.Width / 360f;
            float scaleY = (DisplayPictureBox.Height / 200f) * -1;
            g.TranslateTransform(0, DisplayPictureBox.Height / 2);
            g.ScaleTransform(scaleX, scaleY);
            for (int currentX = 0; currentX < 360; currentX++)
            {
                currentY = (int)(Math.Round(100 * Math.Cos((Math.PI / 180) * currentX)));
                g.DrawLine(thePen, lastX, lastY, currentX, currentY);
                lastX = currentX;
                lastY = currentY;
            }
            //free up resorces
            g.Dispose();
            thePen.Dispose();
        }
        void DrawTangentWave()
        {
            //create a graphics object named g that draws on the picture box
            Graphics g = DisplayPictureBox.CreateGraphics();
            //create a pen to draw with
            Pen thePen = new Pen(Color.Purple);
            thePen.Width = 2;
            int lastX = 0, lastY = 0, currentY = 0;
            float scaleX = DisplayPictureBox.Width / 360f;
            float scaleY = (DisplayPictureBox.Height / 200f) * -1;
            g.TranslateTransform(0, DisplayPictureBox.Height / 2);
            g.ScaleTransform(scaleX, scaleY);
            for (int currentX = 0; currentX < 360; currentX++)
            {
                currentY = (int)(Math.Round(100 * Math.Tan((Math.PI / 180) * currentX)));
                if (Math.Abs(currentY) < DisplayPictureBox.Height / 2)
                {
                    g.DrawLine(thePen, lastX, lastY, currentX, currentY);
                }
                lastX = currentX;
                lastY = currentY;
            }
            //free up resorces
            g.Dispose();
            thePen.Dispose();
        }
        void DrawGrid()
        {
            DisplayPictureBox.Refresh();
            Color oldColor = this.PenColor;
            this.PenColor = Color.Gray;
            int xDiv = DisplayPictureBox.Width / 10;
            int yDiv = DisplayPictureBox.Height / 8;

            this.oldX = 0;
            this.oldY = 0;
            for (int x = 0; x < DisplayPictureBox.Width; x += xDiv)
            {
                oldX = x;
                DrawLineSegment(x, DisplayPictureBox.Height);
            }
            this.oldX = 0;
            this.oldY = 0;
            for (int y = 0; y < DisplayPictureBox.Height; y += yDiv)
            {
                oldY = y;
                DrawLineSegment(DisplayPictureBox.Width, y);
            }
            this.PenColor = oldColor;
        }
        private void ShakeEtchOSketch()
        {
            Random random = new Random();
            int shakeAmount = 15; // Adjust this value to increase/decrease the shake intensity
            int originalX = this.Location.X;
            int originalY = this.Location.Y;

            for (int i = 0; i < 10; i++)
            {
                int offsetX = random.Next(-shakeAmount, shakeAmount + 1);
                int offsetY = random.Next(-shakeAmount, shakeAmount + 1);
                System.Threading.Thread.Sleep(50);
                this.Location = new Point(originalX + offsetX, originalY + offsetY);
            }
            this.Location = new Point(originalX, originalY); // Return to original position after shaking
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DrawWaveformsButton_Click(object sender, EventArgs e)
        {
            DrawGrid();
            DrawSineWave();
            DrawCosineWave();
            DrawTangentWave();
        }
        private void DisplayPictureBox_MouseMove(object? sender, MouseEventArgs e)
        {
            this.Text = $"({e.X},{e.Y})";
            switch (e.Button)
            {
                case MouseButtons.Left:
                    DrawLineSegment(e.X, e.Y);
                    break;
                case MouseButtons.Right:
                    //Save for context menu
                    break;
                case MouseButtons.Middle:
                    UpdatePenColor();
                    break;
                default:
                    //MessageBox.Show($"{e.Button}");
                    break;
            }
            this.oldX = e.X;
            this.oldY = e.Y;
        }
        private void DisplayPictureBox_MouseDown(object? sender, MouseEventArgs e)
        {
            this.Text += $"{e.Button}";
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            DisplayPictureBox.Refresh();
            ShakeEtchOSketch();
        }

        private void SelectColorButton_Click(object sender, EventArgs e)
        {
            UpdatePenColor();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void selectColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatePenColor();
        }

        private void drawWaveformsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawWaveformsButton_Click(sender, e);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearButton_Click(sender, e);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is an EtchOSketch program, with basic drawing functions. \n" +
               "You are able draw lines with your mouse, select different colors, and clear the screen. \n" +
               "There is also a Sine, Cosine, and Tangent waves function that draws each wave in different colors.");
        }
    }
}