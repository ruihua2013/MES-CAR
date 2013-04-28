using System;
using System.Collections.Generic;
using System.Text;
using MES.LED.LED;
using System.Threading;

namespace MES.LED
{
    public class YazijiController
    {
        private static readonly int WaitTime = 500;
        private static readonly string CurrentTotalFormat = "{0}-{1}";
        LEDHelper led;
        public YazijiController(string ip)
        {
            led = new LEDHelper();
            YazijiIPAddress = ip;
            //led.OnFindDevice += new LEDHelper.OnFindDeviceHandler(led_OnFindDevice);
        }

        //void led_OnFindDevice(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
        /// <summary>
        /// 是否连接设备
        /// </summary>
        public bool IsOpen
        {
            get;
            set;
        }
        /// <summary>
        /// 设备的IP地址
        /// </summary>
        public string YazijiIPAddress
        {
            get;
            set;
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public bool Connect(string ipAddress)
        {
            bool reVal = false;
            led.FindDevices();
            Thread.Sleep(WaitTime);//暂停，等待找到设备
            if (led.Devices != null && led.Devices.Count > 0)
            {
                foreach (LEDController c in led.Devices)
                {
                    if (c.IPAddress.ToString().Equals(ipAddress))
                    {
                        reVal = true;
                        YazijiIPAddress = ipAddress;  
                        IsOpen = true;
                    }
                }
            }
            return reVal;
        }

        /// <summary>
        /// 发送车牌
        /// </summary>
        /// <param name="chepai"></param>
        /// <returns></returns>
        public bool Send(YazijiModel chepai)
        {
            if (chepai == null)
            {
                throw new ArgumentNullException("车牌不能为空");
            }
            if (string.IsNullOrEmpty(chepai.Current))
            {
                throw new ArgumentException("车牌不能为空");
            }
            bool reVal = false;
            int orderNo = 0;
            //1，发送当前做的车牌
            LEDSendPackage pack = new LEDSendPackage(LEDCommandType.Send);
            pack.Content = chepai.Current;
            pack.ContentColor = chepai.CurrentTextColor;
            pack.ContentOrder = ++orderNo;
            led.Open(YazijiIPAddress);
            led.SendMessage(pack);
            //2，发送当前计数,格式：m-n,其中m为当前车牌计数,n为当前任务总数
            if (chepai.CurrentIndex > 0 && chepai.Total > 0)
            {
                pack.Content = string.Format(CurrentTotalFormat, chepai.CurrentIndex, chepai.Total);
                pack.ContentColor = chepai.NextTextColor;
                pack.ContentOrder = ++orderNo;
                pack.RegionNo = 2;
                led.SendMessage(pack);
            }
            //3，发送下一个要做的车牌
            if (!string.IsNullOrEmpty(chepai.Next))
            {
                pack.Content = chepai.Next;
                pack.ContentOrder = ++orderNo;
                pack.ContentColor = chepai.NextTextColor;
                pack.RegionNo = 2;
                led.SendMessage(pack);
            }
            led.Close();
            return reVal;
        }
    }

    public class YazijiModel
    {
        private LEDTextColor _currentTextColor = LEDTextColor.Red;
        private LEDTextColor _nextTextColor = LEDTextColor.Red;
        /// <summary>
        /// 当前要做的车牌号
        /// </summary>
        public string Current { get; set; }
        /// <summary>
        /// 当前要做的车牌号颜色
        /// </summary>
        public LEDTextColor CurrentTextColor
        {
            get { return _currentTextColor; }
            set { _currentTextColor = value; }
        }
        /// <summary>
        /// 下一个车牌号
        /// </summary>
        public string Next { get; set; }
        /// <summary>
        /// 下一个车牌号
        /// </summary>
        public LEDTextColor NextTextColor
        {
            get { return _nextTextColor; }
            set { _nextTextColor = value; }
        }
        /// <summary>
        /// 当前车牌的计数
        /// </summary>
        public int CurrentIndex { get; set; }
        /// <summary>
        /// 当前任务车牌总数
        /// </summary>
        public int Total { get; set; }
    }
}
