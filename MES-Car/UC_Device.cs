using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MES_Car.Objects;
using MES.LED;
using MES.LED.LED;

namespace MES_Car
{
    public delegate void OnOpenOrCloseDeviceHadle(ZZ_Device np, bool isopen);

    public partial class UC_Device : UserControl
    {

        public event OnOpenOrCloseDeviceHadle OnOpenOrCloseDevice;


        ZZ_Device devic;
        public UC_Device()
        {
            InitializeComponent();
        }
        YazijiController yaziji=null;
        public void SetDevice(ZZ_Device d)
        {
            devic = d;
            if (devic.IP.Length > 0)
            {
                yaziji = new YazijiController(devic.IP);
            }
            this.lb_deviceName.Text = devic.Name;
        }
        T_Task task = null;
        T_NP np = null;
        T_NP nextnp = null;
        int count = 50;
        int index = 1;
        public void DoNext()
        {
            if(np!=null)
            {
                if (np.SetExeOver() == false) return;
            }
            if (task == null)
            {
                LoadTask();
            }
            LoadNp();
        }
        private void LoadTask()
        {
            task = devic.GetNextTask();
            count = task.GetCountNp();
            index = task.GetCountExeNp()+1;
            DispTask(task);
        }
        private void LoadNp()
        {
            if (task == null) return;

            while (task.TaskID.Trim().Length > 0)
            {
                np = task.GetNextNp();
                index = task.GetCountExeNp() + 1;

                if (np.NpId > 0)
                {
                    nextnp = np.GetNextTwoNp();
                    SendToLED();
                    return;
                }
                else
                {
                    task.SetExeOver();
                    LoadTask();
                }
            }
            SendToLEDtoEnd();
        }

        private void SendToLEDtoEnd()
        {
            this.label1.Text = "无任务";
            this.label4.Text = "";
        }
        private void DispTask(T_Task task)
        {
            this.label2.Text = "计划单号：" + task.PlanID;
            this.label3.Text = "任务单号：" + task.TaskID;
        }
        
        private void SendToLED()
        {
            this.label1.Text = string.Format("{0}/{1}{2} {3}", index, count, np.NPType,np.NpNo);
            this.label4.Text = string.Format("序号：{0}/{1}{2} {3}", index+1, count, nextnp.NPType, nextnp.NpNo);
            if (yaziji != null)
            {
                yaziji.Send(new YazijiModel()
                {
                    Current = np.NpNo,
                    Next = nextnp.NpNo,
                    CurrentIndex = index,
                    Total = count,
                    CurrentTextColor = LEDTextColor.Yellow,
                    NextTextColor = LEDTextColor.Green
                });
            }

        }
        bool isopen = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if(OnOpenOrCloseDevice!=null)
            {
                isopen=!isopen;
                this.button1.Text = isopen ?  "关机":"开机";
                OnOpenOrCloseDevice(devic,isopen);
            }

        }





    }
}
