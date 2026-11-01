using System.IO.Ports;
namespace Datalogger
{
    public partial class DataloggerForm : Form
    {
        public DataloggerForm()
        {
            InitializeComponent();

            this.Text = "PIC Data Logger";
            StartButton.Enabled = true;
            StopButton.Enabled = false;
        }

        SerialPort picserialPort = new SerialPort();
        void SerialPortSetup(string portname)
        {
            picserialPort.Close();
            picserialPort.PortName = portname;
            picserialPort.BaudRate = 9600;
            picserialPort.Parity = Parity.None;
            picserialPort.DataBits = 8;
            picserialPort.StopBits = StopBits.None;
        }

        private void TenMinRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FiveMinRradioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void OneMinRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ThirtySecRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void TenSecRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FiveSecRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void OneSecRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FiveHundMilRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void TwoHundMilRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void OneHundMilRadioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void StartButton_Click(object sender, EventArgs e)
        {

        }

        private void StopButton_Click(object sender, EventArgs e)
        {

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
