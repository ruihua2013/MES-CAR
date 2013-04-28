using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MES.LED.LED
{
    public class LEDSendPackage : LEDPackege
    {
        private readonly static int FirstHeadByte = 0xa0;
        private readonly static int LastFootByte = 0x55;
        private readonly static LEDContentAnimation DefaultLEDContentAnimation = LEDContentAnimation.Display;
        private readonly static LEDContentStand DefaultLEDContentStand = LEDContentStand.NoCircle;
        private readonly static int DefaultSpeed = 0x03;//0x00-0xff,数字越大越快
        //比如屏幕可以显示8个字，当前信息有16个字，那么前8个字移动完毕后，可以选择在屏幕上停留一会儿再显示后8个字。
        private readonly static int DefaultDuration = 0x08;//0x00-0xff,描述
        private readonly static LEDTextColor DefaultTextColor = LEDTextColor.Red;
        private readonly static int DefaultScreenID = 0x01;
        private readonly static int DefaultRegion = 0x01;

        /// <summary>
        /// 指令扩展
        /// </summary>
        public byte[] CommandExtend { get; set; }
        /// <summary>
        /// 指令类型
        /// </summary>
        public LEDCommandType CommandType {get;set;}
        /// <summary>
        /// 内容序号
        /// </summary>
        public int ContentOrder { get; set; }
        /// <summary>
        /// 数据包内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 数据颜色
        /// </summary>
        public LEDTextColor ContentColor { get; set; }
        /// <summary>
        /// 所属分区号
        /// </summary>
        public int RegionNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TempSetDisplayStatusType TempSetDisplayStatusType { get; set; }

        public LEDSendPackage(LEDCommandType type)
        {
            CommandType = type;
        }

        public override byte[] GetPackage()
        {
            using(MemoryStream ms = new MemoryStream())
	        {
                //头部
                Head = new byte[16];
                //0固定
                Head[0] = BitConverter.GetBytes(FirstHeadByte)[0];
                //1-2整个信息长度
                //todo:
                //3-4 16bit屏ID
                Head[3] = BitConverter.GetBytes(DefaultScreenID)[0];
                //5-8,9-12保留
                //13指令代码
                Head[13] = BitConverter.GetBytes((int)CommandType)[0];
                //14-15保留

                //指令扩展内容
                CommandArgs = new byte[32];
                if (CommandType == LEDCommandType.Send && !string.IsNullOrEmpty(Content))
                {
                    //包信息内容
                    CommandExtend = ToLEDUnicodeBytes(Content);
                }
                if (CommandType == LEDCommandType.TempSetDisplayStatus)
                {
                    CopyToArray((int)TempSetDisplayStatusType, CommandArgs, 0, 1);
                }
                if (!string.IsNullOrEmpty(Content))//CommandExtend != null && CommandExtend.Length > 0)
                {
                    //指令参数
                    //CommandArgs = new byte[32];
                    //字节0-1：信息序号：低字节在前，高字节在后，从1开始，上限取决于控制板类型，信息的序号就是信息存放的位置。播放时将按照序号从小到大轮流播放。
                    ContentOrder = (ContentOrder > 0) ? ContentOrder : 1;
                    CopyToArray(ContentOrder, CommandArgs, 0, 2);
                    //字节2：动画方式
                    CommandArgs[2] = BitConverter.GetBytes((int)DefaultLEDContentAnimation)[0];
                    //字节3：停留方式，低4位保留，高4位代表信息播放时是否伴随“环绕闪烁”
                    CommandArgs[3] = BitConverter.GetBytes((int)DefaultLEDContentStand)[0];
                    //字节4：移动速度，0x00-0x0F，数字越大越快
                    CommandArgs[4] = BitConverter.GetBytes(DefaultSpeed)[0];
                    //字节5：页面停留时间：0x00-0xFF，秒数。比如屏幕可以显示8个字，当前信息有16个字，那么前8个字移动完毕后，可以选择在屏幕上停留一会儿再显示后8个字。
                    CopyToArray(DefaultDuration, CommandArgs, 5, 1);
                    //字节6：保留 字节7：保留  字节8-15：保留 字节16-23：保留
                    //字节24：文字颜色，bit0=红色开关；bit1=绿色开关；bit2=蓝色开关；（单色屏可直接填充为0x01，其中蓝色开关为保留字段，暂不支持）。
                    ContentColor = ((int)ContentColor > 1) ? ContentColor : DefaultTextColor;
                    CopyToArray((int)ContentColor, CommandArgs, 24, 1);
                    //字节25：低4位代表内容类型，0x0代表文本，0x1代表RTF，0x2代表图片，0x4温度，0x5时间，0x6倒计时；高4位代表播放方式，0x0代表普通信息。 
                    CommandArgs[25] = BitConverter.GetBytes(0x00)[0];
                    //字节26：本信息处于分组中的序号.
                    //字节27：本组信息总最大序号。（字段26、27用于RTF和位图信息，此类信息每个存储位只能保存一幕内容，可以利用此字段将多幕信息合成为一条信息）
                    //字节28-29：“信息内容”的长度，低字节在前，高字节在后。
                    byte[] bytesContentLength = BitConverter.GetBytes(CommandExtend.Length);
                    CommandArgs[28] = bytesContentLength[0];
                    CommandArgs[29] = bytesContentLength[1];
                    //字节30：当前节目所属分区。0x01表示分区1，0x02表示分区2，0x03表示分区3。注意：不分区等同于一个分区，因此不分区时，该选项直接填0x01。 
                    RegionNo = (RegionNo > 0) ? RegionNo : DefaultRegion;
                    CopyToArray(RegionNo, CommandArgs, 30, 1);
                    //字节31：保留，填充为0。
                }
                //尾部
                Foot = new byte[4];
                //数据包总长度,//1-2整个信息长度
                int len = GetTotalLength();
                byte[] lenBytes = BitConverter.GetBytes(len);
                Head[1] = lenBytes[0];
                if (len > 256)
                    Head[2] = lenBytes[1];
                //0-1 16bit检验和，将本字前所有相加，取低16位
                int sum = GetValidateSum();
                byte[] sumBytes = BitConverter.GetBytes(sum);
                Foot[0] = sumBytes[0];
                if (sum > 256)
	                Foot[1] = sumBytes[1];
                //2保留
                //3固定
                Foot[3] = BitConverter.GetBytes(LastFootByte)[0];

                ms.Write(Head, 0, Head.Length);
                ms.Write(CommandArgs, 0, CommandArgs.Length);
                if (CommandExtend != null && CommandExtend.Length > 0)
                {
                    //todo : 
                    ms.Write(CommandExtend, 0, CommandExtend.Length);
                }
                ms.Write(Foot, 0, Foot.Length);

                //return ms.GetBuffer();//返回256
		        return ms.ToArray();
            }
        }
        private int GetTotalLength()
        {
            int len = 0;
            len = Head.Length + CommandArgs.Length + Foot.Length;
            if(!string.IsNullOrEmpty(Content))//CommandExtend != null && CommandExtend.Length > 0)
                len += CommandExtend.Length;
            return len;
        }
        private int GetValidateSum()
        {
            int sum = 0;
            foreach (byte b in Head)
            {
                sum += (int)b;
            }
            foreach (var b in CommandArgs)
            {
                sum += (int)b;
            }
            if (!string.IsNullOrEmpty(Content))//CommandExtend != null && CommandExtend.Length > 0)
            {
                foreach (var b in CommandExtend)
                {
                    sum += (int)b;
                }
            }
            //不包括Foot
            return sum;
        }
    }
}
