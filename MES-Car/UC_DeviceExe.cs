using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using MES_Car.Objects;
using System.IO.Ports;
using ICSharpCode.SharpDevelop.Gui;

namespace MES_Car
{
    public partial class UC_DeviceExe : UserControl, IWindows
    {
        protected SerialPort comp = null;
        MethodInvoker deleg;
        IAsyncResult aResult = null;
        int MessageLenth = 4;
        private int Head = 128;
        byte[] myBuffer;
        int BufferSize = 0;
        public UC_DeviceExe()
        {
            InitializeComponent();
           
            BufferSize=MessageLenth * 2;
            myBuffer = new byte[BufferSize];
        }
        List<ZZ_Device> listdevice;
        Dictionary<string, UC_Device> dicdevice = new Dictionary<string, UC_Device>();
        bool isOpen = false;


        private void UC_DeviceExe_Load(object sender, EventArgs e)
        {
            listdevice = ZZ_Device.GetAllDevice();
            foreach(ZZ_Device device in listdevice)
            {
                UC_Device uc1 = new UC_Device();
                uc1.SetDevice(device);
                dicdevice[device.DNO] = uc1;
                uc1.OnOpenOrCloseDevice += new OnOpenOrCloseDeviceHadle(uc1_OnOpenOrCloseDevice);
                uc1.DoNext();

                this.flowLayoutPanel1.Controls.Add(uc1);
            }
            this.comboBox1.DataSource = listdevice;
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.ValueMember = "DNO";

            if (!DesignMode)
            {
                comp = new SerialPort();
                //string portname = "COM2"; //ICSharpCode.Core.PropertyService.Get(OptionName(), this.comp.PortName);
                string portname = ICSharpCode.Core.PropertyService.Get(OptionName(), this.comp.PortName);

                ICSharpCode.Core.Properties pro = ICSharpCode.Core.PropertyService.Get(portname, new ICSharpCode.Core.Properties());

                //this.comp.PortName = portname;
                //this.comp.BaudRate = 9600;// pro.Get<int>("Baud", this.comp.BaudRate);
                //this.comp.Parity = Parity.None; pro.Get<Parity>("Parity", Parity.None);
                //this.comp.DataBits = 8;// pro.Get<int>("Data", 8);
                //this.comp.StopBits = StopBits.One;// pro.Get<StopBits>("Stop", StopBits.One);
                //this.comp.ReadTimeout = 500;

                this.comp.PortName = portname;
                this.comp.BaudRate =  pro.Get<int>("Baud", this.comp.BaudRate);
                this.comp.Parity = pro.Get<Parity>("Parity", Parity.None);
                this.comp.DataBits =  pro.Get<int>("Data", 8);
                this.comp.StopBits = pro.Get<StopBits>("Stop", StopBits.One);
                this.comp.ReadTimeout = 500;

                this.comp.ReceivedBytesThreshold = MessageLenth;
                comp.DataReceived += new SerialDataReceivedEventHandler(comp_DataReceived);
                this.comp.RtsEnable = true;
                Open();
            }
        }

        void uc1_OnOpenOrCloseDevice(ZZ_Device device, bool isopen)
        {
            if (isopen)
            {
                byte[] data = HexToByte(device.OnCode);
                this.comp.Write(data, 0, data.Length);
            }
            else
            {
                byte[] data = HexToByte(device.OffCode);
                this.comp.Write(data, 0, data.Length);
            }
        }
        protected void comp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!isOpen) return;
            try
            {
                deleg = delegate { AsyncReceive(this.comp); };
                aResult=this.BeginInvoke(deleg);
                System.Threading.Thread.Sleep(1);
            }
            catch
            { }

        }
        int index = 0;
        private void AsyncReceive(SerialPort com)
        {

            if (!com.IsOpen) return;                       
            try
            {
                int len = com.BytesToRead;
                if (len < MessageLenth) return;
                byte c;
                if (index == 0)//从头读
                {
                    for (int pos = 0; pos < len; pos++)
                    {
                        c = (byte)com.ReadByte();
                        if (c == Head)
                        {
                            myBuffer[index++] = c;
                            break;
                        }
                    }
                    len = com.BytesToRead;
                }
                if (index > 0)//已经读到起始符了
                {
                    for (int pos = 0; pos < len; pos++)
                    {
                        c = (byte)com.ReadByte();
                        myBuffer[index++] = c;
                        if (index == 4)
                        {
                            string str = string.Format("{0:X}", myBuffer[1]);
                            System.Array.Clear(myBuffer, 0, BufferSize);
                            index = 0;
                            SerialPort_OnReceive(str);
                        }

                    }
                }

            }
            catch
            { 
            }
        }
        ///// <summary>
        /////  字节转十六进制
        ///// </summary>
        //private static char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        //public static string ToHexString(byte[] bytes)
        //{
        //    char[] chars = new char[bytes.Length * 2];
        //    for (int i = 0; i < bytes.Length; i++)
        //    {
        //        int b = bytes[i];
        //        chars[i * 2] = hexDigits[b >> 4];
        //        chars[i * 2 + 1] = hexDigits[b & 0xF];
        //    }
        //    return new string(chars);
        //} 

        private void SerialPort_OnReceive(string no)
        {
            if (this.InvokeRequired)
            {
                MethodInvoker deleg = delegate { SerialPort_OnReceive(no); };
                this.Invoke(deleg);
                return;
            }
            else
            {
                DoNext(no);
            }

        }
        public void Open()
        {
            if (comp != null && !comp.IsOpen)
            {
                try
                {
                    this.comp.Open();
                    this.comp.DiscardInBuffer();
                    isOpen = true;
                }
                catch //(System.Exception er)
                {
                }
            }
        }

        /// <summary>
        /// 转换十六进制字符串到字节数组
        /// </summary>
        /// <param name="msg">待转换字符串</param>
        /// <returns>字节数组</returns>
        public static byte[] HexToByte(string msg)
        {
            msg = msg.Replace(" ", "");//移除空格

            //create a byte array the length of the
            //divided by 2 (Hex is 2 characters in length)
            byte[] comBuffer = new byte[msg.Length / 2];
            for (int i = 0; i < msg.Length; i += 2)
            {
                //convert each set of 2 characters to a byte and add to the array
                comBuffer[i / 2] = (byte)Convert.ToByte(msg.Substring(i, 2), 16);
            }

            return comBuffer;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DoNext(this.comboBox1.SelectedValue.ToString());
        }
        private void DoNext(string dno)
        {
            UC_Device device = null;
            dicdevice.TryGetValue(dno, out device);
            if (device != null)
            {
                device.DoNext();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Fm_ComSet fm = new Fm_ComSet();
            string tokename = OptionName();
            fm.DefaultComName = ICSharpCode.Core.PropertyService.Get(tokename, this.comp.PortName);
            if (fm.ShowDialog() == DialogResult.OK)
            {
                ICSharpCode.Core.PropertyService.Set(tokename, fm.DefaultComName);
            }
        }
        private string OptionName()
        {
            string tokename = "ComOption_" + this.GetType().ToString();
            return tokename;
        }
        public void Close()
        {
            try
            {
                if (comp != null && isOpen)
                {
                    isOpen = false;
                    this.comp.DiscardInBuffer();
                    Application.DoEvents();
                    try
                    {
                        if (aResult != null)
                        {
                            this.EndInvoke(aResult);
                        }
                    }
                    catch
                    {
                    }
                    comp.Close();
                    System.Threading.Thread.Sleep(1);
                    comp.Dispose();
                    comp = null;
                }
            }
            catch
            {
            }
        }
    }
}
