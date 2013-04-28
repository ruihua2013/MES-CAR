using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using ICSharpCode.Core;

namespace MES_Car
{
    [ToolboxItem(false)]
    public partial class Fm_ComSet : Form
    {
        public Fm_ComSet()
        {
            InitializeComponent();
            myInit();
        }

        public string DefaultComName
        {
            get
            {
                return this.comboBox1.Text;
            }
            set
            {
                this.comboBox1.Text = value;
            }
        }
        private void myInit()
        {
            string defaultCom = ICSharpCode.Core.PropertyService.Get("defaultCom", "");
            if ("" == defaultCom)
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    defaultCom = s;
                    break;
                }
            }
            this.comboBox1.SelectedIndex = this.comboBox1.FindString(defaultCom);

            foreach (string s in Enum.GetNames(typeof(Parity)))
            {
                this.comboBox4.Items.Add(s);
            }
            foreach (string s in Enum.GetNames(typeof(StopBits)))
            {
                if (s == "None") continue;
                this.comboBox5.Items.Add(s);
            }
            LoadData();
        }
        private void LoadData()
        {
            ICSharpCode.Core.Properties pro = ICSharpCode.Core.PropertyService.Get(this.comboBox1.Text, new ICSharpCode.Core.Properties());
            this.comboBox2.Text = pro.Get<int> ("Baud", 4800).ToString();
            this.comboBox3.Text = pro.Get<int>("Data", 7).ToString();
            this.comboBox4.Text = pro.Get<Parity>("Parity", Parity.None).ToString();
            this.comboBox5.Text = pro.Get<StopBits>("Stop", StopBits.One).ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ICSharpCode.Core.Properties pro = ICSharpCode.Core.PropertyService.Get(this.comboBox1.Text, new ICSharpCode.Core.Properties());
            int baud = 4800;
            int.TryParse(this.comboBox2.Text, out baud);
            pro.Set<int>("Baud", baud);
            pro.Set<int>("Data",int.Parse(this.comboBox3.Text));
            pro.Set("Parity", (Parity)Enum.Parse(typeof(Parity),  this.comboBox4.Text));
            pro.Set("Stop", (StopBits)Enum.Parse(typeof(StopBits), this.comboBox5.Text));

            ICSharpCode.Core.PropertyService.Set("defaultCom", this.comboBox1.Text);
            ICSharpCode.Core.PropertyService.Save();
            this.DialogResult = DialogResult.OK;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
