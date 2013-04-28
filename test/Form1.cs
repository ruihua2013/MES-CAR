using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using WinControls;
using PlanInput;
using NpMis;
using MES_Car;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MES_Car.Fm_Login fm = new MES_Car.Fm_Login();
            fm.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Frm_Input.Instance.Parent = this;

           // Frm_Input.Instance.MdiParent = this;
            //Frm_Input.Instance.Show();
            //Frm_Input.Instance.Focus();
            //Frm_Input.Instance.UserId = User.UserName;
            //Frm_Input.Instance.TextShowNPNum.Focus();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            UC_TaskList uc = new UC_TaskList();
            FmDemo fm = new FmDemo(uc);
            fm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UC_DeviceExe uc = new UC_DeviceExe();
            FmDemo fm = new FmDemo(uc);
            fm.ShowDialog();
        }
    }
}
