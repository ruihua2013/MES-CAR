namespace BarCodePrint
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct NPBarCode
    {
        public string strNPNum;
        public string strNPTypeCode;
        public string strNPTypeDescription;
        public string strNPTypeName;
        public bool bFrontPiece;
        public bool bBackPiece;
    }
}

