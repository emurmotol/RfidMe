using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RfidMe
{
    delegate void IncommingDelegate(string data);

    public class RfidMe
    {
        private IncommingDelegate incommingDelegate;
        private SerialPort serialPort = new SerialPort();
        private string dataReceived = string.Empty;
        private int tagSize = 10;
        private Form form;
        private Control control;

        public RfidMe(Form form, Control control = null)
        {
            this.form = form;
            this.log(string.Format("[{0}] Initialized from form: {1}", this.timestamp(), form.Name));

            if (control != null)
            {
                this.control = control;
                this.log(string.Format("[{0}] Display data on control: {1}", this.timestamp(), control.Name));
            }
            else
            {
                this.log(string.Format("[{0}] Control not set.", this.timestamp()));
            }
            this.incommingDelegate = new IncommingDelegate(this.appendData);
        }

        void appendData(string data)
        {
            this.dataReceived = this.dataReceived + data;
            this.lastResort(this.dataReceived);
        }

        private void lastResort(string dataReceived)
        {
            string tag = getTag(dataReceived);

            if (tag.Length == tagSize)
            {
                this.log(string.Format("[{0}] Data received: {1}", this.timestamp(), dataReceived));
                this.log(string.Format("[{0}] Tag: {1}", this.timestamp(), tag));
                this.serialPort.Close();
                this.log(string.Format("[{0}] Port is open: {1}", this.timestamp(), this.serialPort.IsOpen));

                try
                {
                    if (this.control != null)
                    {
                        this.control.ResetText();
                        this.control.Text = tag;
                        this.log(string.Format("[{0}] {1}.Text: {2}", this.timestamp(), this.control.Name, tag));
                    }
                }
                catch (Exception ex)
                {
                    this.log(string.Format("[{0}] Error: {1}", this.timestamp(), ex.Message));
                }
                this.dataReceived = string.Empty;
                this.serialPort.Open();
                this.log(string.Format("[{0}] Port is open: {1}", this.timestamp(), this.serialPort.IsOpen));
            }
        }

        private string timestamp()
        {
            return DateTime.Now.ToString();
        }

        private void log(string text)
        {
            string.Format("\n{0}", text);
        }

        public bool init()
        {
            try
            {
                this.serialPort.BaudRate = 9600;
                this.serialPort.DataBits = 8;
                this.serialPort.DiscardNull = false;
                this.serialPort.DtrEnable = false;
                this.serialPort.Handshake = Handshake.None;
                this.serialPort.Parity = Parity.None;
                this.serialPort.ParityReplace = 63;
                this.serialPort.ReadBufferSize = 4096;
                this.serialPort.ReadTimeout = -1;
                this.serialPort.ReceivedBytesThreshold = 1;
                this.serialPort.RtsEnable = true;
                this.serialPort.StopBits = StopBits.One;
                //serialPort.WriteBufferSize = 2048;
                //serialPort.WriteTimeout = -1;

                bool isOpen = false;

                foreach (var portName in SerialPort.GetPortNames())
                {
                    this.serialPort.PortName = portName;
                    this.serialPort.Open();

                    if (this.serialPort.IsOpen)
                    {
                        isOpen = true;
                        this.log(string.Format("[{0}] RFID reader found at: {1}", this.timestamp(), portName));
                        break;
                    }
                }

                if (isOpen)
                {
                    this.serialPort.DataReceived += SerialPort_DataReceived;
                    this.log(string.Format("[{0}] Port is open: {1}", this.timestamp(), this.serialPort.IsOpen));
                    return true;
                }
                this.log(string.Format("[{0}] Port is close: {1}", this.timestamp(), "RFID reader not found."));
                return false;
            }
            catch (Exception ex)
            {
                this.log(string.Format("[{0}] Error: {1}", this.timestamp(), ex.Message));
                return false;
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var incomming = (SerialPort)sender;
            int incommingBytes = incomming.BytesToRead;

            char[] incommingBuffer = new char[incommingBytes];
            incomming.Read(incommingBuffer, 0, incommingBytes);

            String tempString = new String(incommingBuffer);
            this.form.BeginInvoke(this.incommingDelegate, tempString);
        }

        private string getTag(string dataReceived)
        {
            return dataReceived.TrimEnd().Split(' ').Last().TrimEnd();
        }
    }
}
