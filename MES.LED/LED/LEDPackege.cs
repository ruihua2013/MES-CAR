using System;
using System.Collections.Generic;
using System.Text;

namespace MES.LED.LED
{
    public class LEDPackege
    {
        /// <summary>
        /// 包头
        /// </summary>
        public byte[] Head { get; set; }
        /// <summary>
        /// 指令参数
        /// </summary>
        public byte[] CommandArgs { get; set; }
        /// <summary>
        /// 包尾
        /// </summary>
        public byte[] Foot { get; set; }
        public List<byte> Package { get; set; }
        public virtual byte[] GetPackage()
        {
            return null;
        }

        public byte[] ToLEDUnicodeBytes(string str)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(str);
            int ch;
            for (int i = 0; i < str.Length; i++)
            {
                ch = (int)str[i];
                if (ch >= 0x80)
                {
                    if (ch <= 0xFF)
                        ch -= 0x80;
                    else if (ch >= 0x2000 && ch <= 0x266F)
                        ch = ch - 0x2000 + 128;
                    else if (ch >= 0x3000 && ch <= 0x33FF)
                        ch = ch - 0x3000 + 1648 + 128;
                    else if (ch >= 0x4E00 && ch <= 0x9FA5)
                        ch = ch - 0x4E00 + 1648 + 1024 + 128;
                    else if (ch >= 0xF900 && ch <= 0xFFFF)
                        ch = ch - 0xF900 + 1648 + 1024 + 20902 + 128;
                    ch += 128;
                    CopyToArray(ch, bytes, i * 2, 2);
                }
            }
            return bytes;
        }
        /// <summary>
        /// 将int拷贝到byte[]指定位置
        /// </summary>
        /// <param name="ival">int值</param>
        /// <param name="array">byte[]</param>
        /// <param name="arrayIndex">byte[]中的起始位置</param>
        /// <param name="byteLength">拷贝的byte数</param>
        public void CopyToArray(int ival, byte[] array, int arrayIndex, int byteCount)
        {
            byte[] temp = BitConverter.GetBytes(ival);
            for (int i = 0; i < byteCount; i++)
            {
                array[arrayIndex + i] = temp[i];
            }
        }

    }
    /// <summary>
    /// 指令
    /// </summary>
    public enum LEDCommandType
    {
        /// <summary>
        /// 追加信息
        /// </summary>
        Append = 0x02,
        /// <summary>
        /// 发送信息
        /// </summary>
        Send = 0x03,
        /// <summary>
        /// 清除所有信息
        /// </summary>
        Clear = 0x06,
        /// <summary>
        /// 设置显示屏亮度
        /// </summary>
        SetLight = 0x10,
        /// <summary>
        /// 读取亮度设置
        /// </summary>
        GetLight = 0x13,
        /// <summary>
        /// 临时设置屏幕显示状态
        /// </summary>
        TempSetDisplayStatus = 0x19,
        /// <summary>
        /// 时间校正指令
        /// </summary>
        TimeCheck = 0x21,
        /// <summary>
        /// 时间查询
        /// </summary>
        TimeQuery = 0x22,
        /// <summary>
        /// 显示屏硬件设置
        /// </summary>
        DisplayOptions = 0x23,
        /// <summary>
        /// 读取硬件设置
        /// </summary>
        GetOptions = 0x24,
        /// <summary>
        /// 设置控制卡自动开关机
        /// </summary>
        SetAutOpenShut = 0x26,
        /// <summary>
        /// 读取屏幕开关机设定
        /// </summary>
        GetAutoOpenShut = 0x27,
        /// <summary>
        /// 设置分区指令
        /// </summary>
        SetRegion = 0x29,
        /// <summary>
        /// 读取分区指令
        /// </summary>
        GetRegion = 0x2A,
        /// <summary>
        /// 获取控制卡固件版本
        /// </summary>
        GetFirewareVersion = 0x35
    }
    public enum TempSetDisplayStatusType
    {
        Shutdown = 0x00,
        Start = 0x01
    }
    /// <summary>
    /// 信息动画方式
    /// </summary>
    public enum LEDContentAnimation
    {
        /// <summary>
        /// 立刻显示
        /// </summary>
        Display = 0x00,
        /// <summary>
        /// 左移
        /// </summary>
		Left = 0x01,
        /// <summary>
        /// 右移
        /// </summary>
        Right = 0x02,
        /// <summary>
        /// 上移
        /// </summary>
        Up = 0x03,
        /// <summary>
        /// 下移
        /// </summary>
        Down = 0x04,
        /// <summary>
        /// 左拉幕
        /// </summary>
		ScreenLeft = 0x05,
        /// <summary>
        /// 下拉幕
        /// </summary>
        ScreenDown = 0x06,
        /// <summary>
        /// 右拉幕
        /// </summary>
        ScreenRight = 0x07,
        /// <summary>
        /// 阿拉伯右移
        /// </summary>
        Arabic = 0x08,
        /// <summary>
        /// 水平百叶窗
        /// </summary>
        HorizontalPersiennes = 0x09,
        /// <summary>
        /// 堆积木
        /// </summary>
		BuildingBlock = 0x0A,
        /// <summary>
        /// 垂直百叶窗
        /// </summary>
        VerticalPersiennes = 0x0B,
        /// <summary>
        /// 中合
        /// </summary>
		CloseToCenter = 0x0C,
        /// <summary>
        /// 中开
        /// </summary>
		OpenToCenter = 0x0D,
        /// <summary>
        /// 上下合
        /// </summary>
        CloseToUpDown = 0x0E,
        /// <summary>
        /// 上下开
        /// </summary>
        OpenToUpDown = 0x0F,
        /// <summary>
        /// 水平穿插
        /// </summary>
        HorizontalCross = 0x10,
        /// <summary>
        /// 上下交错
        /// </summary>
        VerticalCross = 0x11,
        /// <summary>
        /// 随机
        /// </summary>
        Random = 0x12

    }
    /// <summary>
    /// 停留方式
    /// </summary>
    public enum LEDContentStand
    {
        /// <summary>
        /// 无环绕闪烁
        /// </summary>
        NoCircle = 0x00,
        /// <summary>
        /// 红4点
        /// </summary>
        FourRed = 0x10,
        /// <summary>
        /// 绿4点
        /// </summary>
        FourGreen = 0x20,
        /// <summary>
        /// 黄4点
        /// </summary>
        FourYellow = 0x30,
        /// <summary>
        /// 红1点
        /// </summary>
        OneRed = 0x40,
        /// <summary>
        /// 绿1点
        /// </summary>
        OneGreen = 0x50,
        /// <summary>
        /// 黄1点
        /// </summary>
        OneYeelow = 0x60,
        /// <summary>
        /// 红单线闪烁
        /// </summary>
        RedLine = 0x70,
        /// <summary>
        /// 绿单线闪烁
        /// </summary>
        GreenLine = 0x80,
        /// <summary>
        /// 黄单线闪烁
        /// </summary>
        YellowLine = 0x90,
        /// <summary>
        /// 红单线环绕
        /// </summary>
        RedLineCircle = 0xA0,
        /// <summary>
        /// 绿单线环绕
        /// </summary>
        GreenLineCircle = 0xB0,
        /// <summary>
        /// 黄单线环绕
        /// </summary>
        YellowLineCircle = 0xC0,
        /// <summary>
        /// 红双线环绕
        /// </summary>
        RedTwoLinesCircle = 0xD0,
        /// <summary>
        /// 绿双线环绕
        /// </summary>
        GreenTwoLinesCircle = 0xE0,
        /// <summary>
        /// 黄双线环绕
        /// </summary>
        YellowTwoLinesCircle = 0xF0
    }
    /// <summary>
    /// 文字颜色（单色直接0x01）
    /// </summary>
    public enum LEDTextColor
    {
        Red = 1,
        Green = 2,
        Yellow = 3
    }
    /// <summary>
    /// 内容类型，低4位代表内容类型，高4位代表播放方式，0x0代表普通信息
    /// </summary>
    public enum LEDContentType
    {
        /// <summary>
        /// 文本
        /// </summary>
        Text = 0x00,
        /// <summary>
        /// RTF
        /// </summary>
        RTF = 0x01,
        /// <summary>
        /// 图片
        /// </summary>
        Image = 0x2,
        /// <summary>
        /// 温度
        /// </summary>
        Temperature = 0x04,
        /// <summary>
        /// 时间
        /// </summary>
        DateTime = 0x05,
        /// <summary>
        /// 倒计时
        /// </summary>
        CountDown = 0x06
    }
    /// <summary>
    /// 返回包结果
    /// </summary>
    public enum LEDReceiveResult
    {
        //如果收到的包格式不正确，终端会丢弃该包，不会发出回馈信息
        /// <summary>
        /// 收到的包格式正确，但是校验和验证错误。错误码为 1
        /// </summary>
        ValidateSumErr = 1,
        /// <summary>
        /// 收到的包验证正确，但是指令无法识别。错误码为2.
        /// </summary>
        CommandInvalid =2,
        /// <summary>
        /// 收到的包验证正确，但是指令尚未实现或者控制卡不支持。错误码为3.
        /// </summary>
        CommandNotSupported = 3,
        /// <summary>
        /// 包中参数错误，如发送信息时指定的ID大于控制卡允许的ID上限、设置硬件参数时指定的屏幕高宽超出最大范围。错误码为4.
        /// </summary>
        ArgumentsErr =4,
        /// <summary>
        /// 返回0代表执行成功
        /// </summary>
        Sucess = 0

    }
}
