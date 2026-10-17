namespace DartGame
{
    public partial class DartGameForm : Form
    {
        public DartGameForm()
        {
            InitializeComponent();
        }




        //Event handlers below

        private void QuitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StartRoundButton_Click(object sender, EventArgs e)
        {
            // Start a new round of the game
            MessageBox.Show("Starting New Round!");
        }


        private void ReviewButton_Click(object sender, EventArgs e)
        {
            // Review the game logic here
            MessageBox.Show("Reviewing Game!");
        }
    }
}
