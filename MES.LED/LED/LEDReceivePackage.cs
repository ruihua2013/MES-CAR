using System;
using System.Collections.Generic;
using System.Text;

namespace MES.LED.LED
{
    public class LEDReceivePackage : LEDPackege
    {
        private readonly static int FirstHeadByte = 0xaf;
        private readonly static int LastFootByte = 0x5f;

        public LEDReceiveResult Result { get; set; }

        public LEDReceivePackage(byte[] bytes)
        {
            if (bytes == null || bytes.Length != 52)
            {
                throw new Exception("Receive package is error 1");
            }
            if (bytes[0] == BitConverter.GetBytes(FirstHeadByte)[0]
                && bytes[bytes.Length - 1] == BitConverter.GetBytes(LastFootByte)[0])
            {
                byte cmdByte = bytes[13];
                int cmdInt = (int)cmdByte;
                if (Enum.IsDefined(typeof(LEDReceiveResult), cmdInt))
                    Result = (LEDReceiveResult)cmdInt;
                else
                    throw new Exception(" Receive result is not defined");
            }
        }
    }
}
