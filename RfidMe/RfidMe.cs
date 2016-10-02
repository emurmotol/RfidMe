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
        private static int tagSize = 10;
        private Form form;

        public RfidMe(Form form)
        {
            this.form = form;
            this.incommingDelegate = new IncommingDelegate(this.appendData);
        }

        void appendData(string data)
        {
            this.dataReceived = this.dataReceived + data;
            string tag = getTag(this.dataReceived);

            if (tag.Length == tagSize)
            {
                //tag; 
                this.dataReceived = string.Empty;
            }
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

                    if (serialPort.IsOpen)
                    {
                        isOpen = true;
                        break;
                    }
                }

                if (isOpen)
                {
                    this.serialPort.DataReceived += SerialPort_DataReceived;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
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
            form.BeginInvoke(this.incommingDelegate, tempString);
        }

        private string getTag(string dataReceived)
        {
            return dataReceived.TrimEnd().Split(' ').Last().TrimEnd();
        }
    }
}
