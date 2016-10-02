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
    public partial class MainForm : Form
    {
        private RfidMe rfid;

        public MainForm()
        {
            InitializeComponent();
            rfid = new RfidMe(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            rfid.init();
        }
    }
}
