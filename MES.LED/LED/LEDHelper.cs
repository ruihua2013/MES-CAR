using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace MES.LED.LED
{
    class LEDHelper
    {
        //UDP
        //private readonly static int UDPPort = 51883;
        private readonly static string DefaultEncoding = "gb2312";
        private readonly static string FindDeviceMessage = "EASYNET";
        private readonly static string FindeDeviceRetMessage = "NETEASY";
        private List<LEDController> devices = new List<LEDController>();
        private UdpClient sendUdpClient;
        Thread receiveThread;
        public delegate void OnFindDeviceHandler(object sender, EventArgs e);
        public event OnFindDeviceHandler OnFindDevice;

        IPAddress ipAddr;
        TcpClient tcpClient;
        NetworkStream netStream;
        BinaryWriter writer;
        BinaryReader reader;
        //TCP
        private readonly static int TCPPort = 120;
        #region Properties
        public List<LEDController> Devices
        {
            get {
                return devices;
            }
        }
        #endregion

        #region 使用UDP协议查找设备
        /// <summary>
        /// 查找设备
        /// </summary>
        public void FindDevices()
        {
            //先停止上次的挂起的接收
            StopReceive();
            if (receiveThread == null)
            {
                receiveThread = new Thread(ReceiveMessage);
            }
            receiveThread.Start();
        }
        /// <summary>
        /// 停止接收
        /// </summary>
        private void StopReceive()
        {
            if (sendUdpClient != null)
            {
                sendUdpClient.Close();
                sendUdpClient = null;
            }
            if (receiveThread != null && receiveThread.ThreadState == ThreadState.Running)
            {
                receiveThread.Abort();
                receiveThread = null;
            }
        }
        /// <summary>
        /// 发送并接收消息
        /// </summary>
        private void ReceiveMessage()
        {
            //使用UDP协议查找设备
            IPAddress localIp = NetHelper.GetLocalIP();
            IPEndPoint localPoint = new IPEndPoint(localIp, 0);//UDPPort);

            if (sendUdpClient == null)
                sendUdpClient = new UdpClient(localPoint);

            //发送数据
            byte[] sendbytes = Encoding.GetEncoding(DefaultEncoding)
                .GetBytes(FindDeviceMessage);
            sendUdpClient.Send(sendbytes, sendbytes.Length,
                new IPEndPoint(IPAddress.Broadcast, 0));
            
            //接收数据
            IPEndPoint remoteIPPoint = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                if (sendUdpClient.Client == null)
                    break;
                //if (sendUdpClient.Client.Poll(-1, SelectMode.SelectRead))
                //    break;
                try
                {
                    //如果没有数据而执行了Receive，那么当前的线程会被挂起
                    byte[] receiveBytes = sendUdpClient.Receive(ref remoteIPPoint);
                    string receiveMsg = Encoding.GetEncoding(DefaultEncoding)
                        .GetString(receiveBytes);
                    if (!string.IsNullOrEmpty(receiveMsg) &&
                        receiveMsg.Equals(FindeDeviceRetMessage))
                    {
                        devices.Add(new LEDController()
                        {
                            IPAddress = remoteIPPoint.Address
                        });

                        if (OnFindDevice != null)
                        {
                            OnFindDevice(this, EventArgs.Empty);
                        }
                    }
                }
                catch
                {
                    break;
                }
            }
        } 
        #endregion

        #region ??获取mac地址
        
        #endregion

        #region 使用TCP协议发送消息
        public void SendMessageTest(string ip)
        {
            IPAddress ipAddr = IPAddress.Parse(ip);
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(ipAddr, TCPPort);

            if (tcpClient.Connected)
            {
                NetworkStream netStream = tcpClient.GetStream();
                BinaryWriter writer = new BinaryWriter(netStream);
                BinaryReader reader = new BinaryReader(netStream);

                string sendMsg = "a034000100000000000000000019191800000000000000000000000000000000000000000000000000000000000000001f010055";
                byte[] sendBytes = HexStrToByteArray(sendMsg);

                writer.Write(sendBytes);
                writer.Flush();

                byte[] receiveBytes = reader.ReadBytes(52);

                sendMsg = "a0340101000000000000000000030302020001000e03020000000000000000000000000000000000010100000000020000000000000000000000000000000000000004000020000000000c000060000000000c000060000000001f7c3ef8000000000ce66360000000000cc36060000000000cc37060000000000cff3e60000000000cc00760000000000cc00360000000000ce76360000000000f7e3e780000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000ac0f0055";

                sendBytes = HexStrToByteArray(sendMsg);

                writer.Write(sendBytes);
                writer.Flush();

                receiveBytes = reader.ReadBytes(52);

                sendMsg = "a0340101000000000000000000030302020001000e030200000000000000000000000000000000000101000000000200000000000000000000000000000000000000200001030000000060000307000000006000030f00000000fbe1f7db0000000067331b0300000000661b030300000000661b83030000000067f9f3030000000066003b030000000066001b0300000000673b1b03000000007bf1f3c300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000004c0f0055";
                sendBytes = HexStrToByteArray(sendMsg);

                writer.Write(sendBytes);
                writer.Flush();

                receiveBytes = reader.ReadBytes(52);

                sendMsg = "a0340001000000000000000000191918010000000000000000000000000000000000000000000000000000000000000020010055";
                sendBytes = HexStrToByteArray(sendMsg);

                writer.Write(sendBytes);
                writer.Flush();

                receiveBytes = reader.ReadBytes(52);


                if (reader != null)
                {
                    reader.Close();
                }
                if (writer != null)
                {
                    writer.Close();
                }
                if (tcpClient != null)
                {
                    tcpClient.Close();
                }
            }
        }

        public void Open(string ip)
        {
            ipAddr = IPAddress.Parse(ip);
            tcpClient = new TcpClient();
            if (!tcpClient.Connected)
                tcpClient.Connect(ipAddr, TCPPort);
            //if (tcpClient.Connected)

            netStream = tcpClient.GetStream();
            writer = new BinaryWriter(netStream);
            reader = new BinaryReader(netStream);
            byte[] sendBytes = null;
            byte[] receiveBytes = null;
            LEDReceivePackage rPck = null;
            //关屏
            LEDSendPackage spTemp = new LEDSendPackage(LEDCommandType.TempSetDisplayStatus);
            spTemp.TempSetDisplayStatusType = TempSetDisplayStatusType.Shutdown;
            sendBytes = spTemp.GetPackage();
            writer.Write(sendBytes);
            writer.Flush();

            receiveBytes = reader.ReadBytes(52);
            rPck = new LEDReceivePackage(receiveBytes);
            if (rPck.Result != LEDReceiveResult.Sucess)
            {
                throw new Exception("发送失败");
            }
        }
        public void Close()
        {
            byte[] sendBytes = null;
            byte[] receiveBytes = null;
            LEDReceivePackage rPck = null;
            //开屏
            LEDSendPackage spTemp = new LEDSendPackage(LEDCommandType.TempSetDisplayStatus);
            spTemp.TempSetDisplayStatusType = TempSetDisplayStatusType.Start;
            sendBytes = spTemp.GetPackage();
            writer.Write(sendBytes);
            writer.Flush();

            receiveBytes = reader.ReadBytes(52);
            rPck = new LEDReceivePackage(receiveBytes);
            if (rPck.Result != LEDReceiveResult.Sucess)
            {
                throw new Exception("发送失败");
            }
            if (reader != null)
            {
                reader.Close();
            }
            if (writer != null)
            {
                writer.Close();
            }
            if (tcpClient != null)
            {
                tcpClient.Close();
            }
        }
        /// <summary>
        /// 10ms延迟，GPRS 1分钟；串口 200ms
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="package"></param>
        public void SendMessage(LEDSendPackage package)
        {
            if (tcpClient.Connected)
            {
                byte[] sendBytes = null;
                byte[] receiveBytes = null;
                LEDReceivePackage rPck = null;
                //发送信息
                sendBytes = package.GetPackage();
                writer.Write(sendBytes);
                writer.Flush();

                receiveBytes = reader.ReadBytes(52);
                rPck = new LEDReceivePackage(receiveBytes);
                if (rPck.Result != LEDReceiveResult.Sucess)
                {
                    throw new Exception("发送失败");
                }

            }
        }


        // 字节转十六进制
        private static char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        static string ToHexString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                int b = bytes[i];
                chars[i * 2] = hexDigits[b >> 4];
                chars[i * 2 + 1] = hexDigits[b & 0xF];
            }
            return new string(chars);
        } 

        /// <summary>
        /// 十六进制字符串转换成字节数组 
        /// </summary>
        /// <param name="hexString">要转换的字符串</param>
        /// <returns></returns>
        private static byte[] HexStrToByteArray(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                throw new ArgumentException("十六进制字符串长度不对");
            byte[] buffer = new byte[hexString.Length / 2];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Trim(), 0x10);
            }
            return buffer;
        }
        #endregion
    }
}
