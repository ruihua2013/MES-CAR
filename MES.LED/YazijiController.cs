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
        private static readonly string CurrentTotalStepFormat = "{0}-{1}-{2}";
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
                if (string.IsNullOrEmpty(chepai.CurrentStep))
                {
                    pack.Content = string.Format(CurrentTotalStepFormat, chepai.Total, chepai.CurrentIndex, chepai.CurrentStep);
                }
                else
                {
                    pack.Content = string.Format(CurrentTotalFormat, chepai.Total, chepai.CurrentIndex);
                }
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
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Send(string message)
        {
            return Send(
                new YazijiStringModel()
                {
                    Message = message,
                    MessageColor = LEDTextColor.Red,
                    Region = YazijiLEDRegion.Top
                }
                );
        }
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Send(YazijiStringModel message)
        {
            if (message == null || string.IsNullOrEmpty(message.Message))
            {
                throw new ArgumentException("发送内容不能为空");
            }
            bool reVal = false;
            int orderNo = 0;
            //1，发送当前做的车牌
            LEDSendPackage pack = new LEDSendPackage(LEDCommandType.Send);
            pack.Content = message.Message;
            pack.ContentColor = message.MessageColor;
            pack.RegionNo = (message.Region == YazijiLEDRegion.Top) ? 1 : 2;
            pack.ContentOrder = ++orderNo;
            led.Open(YazijiIPAddress);
            led.SendMessage(pack);
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
        /// <summary>
        /// 当前车牌压制步骤
        /// </summary>
        public string CurrentStep { get; set; }
    }


    public class YazijiStringModel
    {
        /// <summary>
        /// 信息内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 信息颜色
        /// </summary>
        public LEDTextColor MessageColor { get; set; }
        /// <summary>
        /// 发送到的分区（上，下）
        /// </summary>
        public YazijiLEDRegion Region { get; set; }
    }

    /// <summary>
    /// 压字机的LED分区
    /// </summary>
    public enum YazijiLEDRegion
    {
        /// <summary>
        /// 上分区
        /// </summary>
        Top = 0,
        /// <summary>
        /// 下分区
        /// </summary>
        Bottom = 1
    }
}
