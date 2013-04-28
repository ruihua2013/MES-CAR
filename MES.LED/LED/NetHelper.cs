using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace MES.LED.LED
{
    class NetHelper
    {
        //获取本机IP
        public static IPAddress GetLocalIP()
        {
            //获得本机名
            string hostname = Dns.GetHostName();
            IPHostEntry localhost = Dns.GetHostEntry(hostname);
            //todo: 多个IP时怎么获取当前局域网的Ip地址
            //todo:如果获取到本机地址失败
            IPAddress localaddr = localhost.AddressList[localhost.AddressList.Length - 1];
            return localaddr;
        }
    }
}
