using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RfidMe
{
    delegate void IncommingDelegate(string text);

    public partial class MainForm : Form
    {
        private IncommingDelegate incommingDelegate;
        private SerialPort serialPort = new SerialPort();
        private String incommingData;
        private int dataSize = 44;

        public MainForm()
        {
            InitializeComponent();
            this.incommingDelegate = new IncommingDelegate(appendData);
        }

        void appendData(string text)
        {
            this.incommingData = this.incommingData + text;
        }

        private void serialPortInit()
        {
            this.serialPort.BaudRate = 9600;
            this.serialPort.DataBits = 8;
            this.serialPort.DiscardNull = false;
            this.serialPort.DtrEnable = false;
            this.serialPort.Handshake = Handshake.None;
            this.serialPort.Parity = Parity.None;
            this.serialPort.ParityReplace = 63;
            this.serialPort.PortName = "";
            this.serialPort.ReadBufferSize = 4096;
            this.serialPort.ReadTimeout = -1;
            this.serialPort.ReceivedBytesThreshold = 1;
            this.serialPort.RtsEnable = true;
            this.serialPort.StopBits = StopBits.One;
            //serialPort.WriteBufferSize = 2048;
            //serialPort.WriteTimeout = -1;

            this.serialPort.Open();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            serialPortInit();
        }
    }
}
