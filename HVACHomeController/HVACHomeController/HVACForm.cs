namespace HVACHomeController
{
    public partial class HVACForm : Form
    {
        public HVACForm()
        {
            InitializeComponent();
        }

       private void TempUpButton_Click(object sender, EventArgs e)
        {
            // Handle the temperature up button click event
            // You can add your logic here to increase the temperature
            MessageBox.Show("Temperature Up button clicked!");
        }
        private void TempDownButton_Click(object sender, EventArgs e)
        {
            // Handle the temperature down button click event
            // You can add your logic here to decrease the temperature
            MessageBox.Show("Temperature Down button clicked!");
        }
    }
}
