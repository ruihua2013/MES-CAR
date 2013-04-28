namespace BarCodePrint
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PanBarCode
    {
        public string strTaskID;
        public string strPanType;
        public string strCount;
    }
}

