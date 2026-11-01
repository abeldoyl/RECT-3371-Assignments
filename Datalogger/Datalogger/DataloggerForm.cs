using System.IO.Ports;
using System.Runtime.InteropServices;
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


        // Event handlers below ---------------------------------------------------------

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
