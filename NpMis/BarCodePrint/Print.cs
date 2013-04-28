namespace BarCodePrint
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Print
    {
        [DllImport("tsclib.dll")]
        private static extern void barcode(string X, string Y, string CodeType, string Height, string Readable, string rotation, string Narrow, string Wide, string Code);
        [DllImport("tsclib.dll")]
        private static extern void clearbuffer();
        [DllImport("tsclib.dll")]
        private static extern void closeport();
        [DllImport("tsclib.dll")]
        private static extern void downloadpcx(string FileName, string ImageName);
        [DllImport("tsclib.dll")]
        private static extern void formfeed();
        public void LabelPrintPackNP(string[] NPNum)
        {
            openport("TSC TTP-243E Plus");
            setup("100", "120", "3", "10", "0", "0", "0");
            clearbuffer();
            int num = 0;
            int lowerBound = NPNum.GetLowerBound(0);
            while (lowerBound < (NPNum.GetUpperBound(0) + 1))
            {
                for (int i = 0; i < 0x11; i++)
                {
                    int x = 60 + (num * 240);
                    int y = 30 + (i * 50);
                    windowsfont(x, y, 50, 0, 0, 0, "黑体", NPNum[lowerBound]);
                    lowerBound++;
                    if (lowerBound > NPNum.GetUpperBound(0))
                    {
                        break;
                    }
                }
                num++;
            }
            windowsfont(640, 880, 30, 0, 0, 0, "黑体", "共计" + lowerBound + "副");
            windowsfont(620, 910, 30, 0, 0, 0, "黑体", DateTime.Now.ToString("yyyy-MM-dd"));
            printlabel("1", "1");
            closeport();
        }

        public void LabelPrintSingleNP(NPBarCode[] NPNum)
        {
            try
            {
                openport("TSC TTP-243E Plus");
                sendcommand("SIZE 100 mm,35 mm");
                sendcommand("GAP 2 mm,0");
                sendcommand("SPEED 4");
                sendcommand("DENSITY 7");
                sendcommand("DIRECTION 0");
                sendcommand("OFFSET 0.00");
                sendcommand("REFERENCE 0,0");
                sendcommand("SET PEEL OFF");
                sendcommand("SET CUTTER OFF");
                sendcommand("HOME");
                clearbuffer();
                downloadpcx("Name.pcx", "NAME.PCX");
                for (int i = NPNum.GetLowerBound(0); i < (NPNum.GetUpperBound(0) + 1); i++)
                {
                    int num3;
                    clearbuffer();
                    string s = "";
                    string str2 = "";
                    if (NPNum[i].bFrontPiece)
                    {
                        int num2;
                        num3 = 1;
                        while (num3 < 7)
                        {
                            s = NPNum[i].strNPNum.Substring(num3, 1);
                            if (Encoding.Default.GetBytes(s).Length > 1)
                            {
                                str2 = str2 + "I";
                                windowsfont(70 + (num3 * 0x23), 0x94, 0x2d, 0, 0, 0, "黑体", s);
                            }
                            else
                            {
                                str2 = str2 + s;
                                num2 = 70 + (num3 * 0x23);
                                printerfont(num2.ToString(), "145", "5", "0", "1", "1", s);
                            }
                            num3++;
                        }
                        barcode("100", "50", "128", "99", "0", "0", "2", "4", NPNum[i].strNPTypeCode + str2);
                        windowsfont(20, 20, 20, 0, 0, 0, "黑体", NPNum[i].strNPTypeName);
                        windowsfont(320, 20, 20, 0, 0, 0, "黑体", NPNum[i].strNPTypeCode + "前");
                        num2 = 15 + (NPNum[i].strNPTypeName.Length * 0x17);
                        sendcommand("BOX 15,15," + num2.ToString() + ",45,3");
                        windowsfont(50, 140, 0x2d, 0, 0, 0, "黑体", NPNum[i].strNPNum.Substring(0, 1));
                        sendcommand("PUTPCX 25,210,\"NAME.PCX\"");
                    }
                    s = "";
                    str2 = "";
                    if (NPNum[i].bBackPiece)
                    {
                        for (num3 = 1; num3 < 7; num3++)
                        {
                            s = NPNum[i].strNPNum.Substring(num3, 1);
                            if (Encoding.Default.GetBytes(s).Length > 1)
                            {
                                str2 = str2 + "I";
                                windowsfont(480 + (num3 * 0x23), 0x94, 0x2d, 0, 0, 0, "黑体", s);
                            }
                            else
                            {
                                str2 = str2 + s;
                                printerfont((480 + (num3 * 0x23)).ToString(), "145", "5", "0", "1", "1", s);
                            }
                        }
                        barcode("520", "50", "128", "99", "0", "0", "2", "4", NPNum[i].strNPTypeCode + str2);
                        windowsfont(440, 20, 20, 0, 0, 0, "黑体", NPNum[i].strNPTypeName);
                        windowsfont(740, 20, 20, 0, 0, 0, "黑体", NPNum[i].strNPTypeCode + "后");
                        sendcommand("BOX 435,15," + ((0x1b3 + (NPNum[i].strNPTypeName.Length * 0x17))).ToString() + ",45,3");
                        windowsfont(470, 140, 0x2d, 0, 0, 0, "黑体", NPNum[i].strNPNum.Substring(0, 1));
                        sendcommand("PUTPCX 445,210,\"NAME.PCX\"");
                    }
                    printlabel("1", "1");
                }
                closeport();
            }
            catch
            {
            }
        }

        public void LabelPrintTaskNP(string TaskID, NPBarCode[] NPNum)
        {
            try
            {
                openport("TSC TTP-243E Plus");
                sendcommand("SIZE 100 mm,35 mm");
                sendcommand("GAP 2 mm,0");
                sendcommand("SPEED 4");
                sendcommand("DENSITY 7");
                sendcommand("DIRECTION 0");
                sendcommand("OFFSET 0.00");
                sendcommand("REFERENCE 0,0");
                sendcommand("SET PEEL OFF");
                sendcommand("SET CUTTER OFF");
                sendcommand("HOME");
                clearbuffer();
                downloadpcx("Name.pcx", "NAME.PCX");
                sendcommand("BOX 15,20,380,250,6");
                barcode("75", "50", "128", "110", "0", "0", "2", "4", TaskID);
                printerfont("40", "160", "4", "0", "1", "1", TaskID);
                sendcommand("PUTPCX 25,200,\"NAME.PCX\"");
                sendcommand("BOX 425,20,790,250,6");
                barcode("485", "50", "128", "110", "0", "0", "2", "4", TaskID);
                printerfont("450", "160", "4", "0", "1", "1", TaskID);
                sendcommand("PUTPCX 435,200,\"NAME.PCX\"");
                printlabel("1", "1");
                for (int i = NPNum.GetLowerBound(0); i < (NPNum.GetUpperBound(0) + 1); i++)
                {
                    int num3;
                    clearbuffer();
                    string s = "";
                    string str2 = "";
                    if (NPNum[i].bFrontPiece)
                    {
                        int num2;
                        num3 = 1;
                        while (num3 < 7)
                        {
                            s = NPNum[i].strNPNum.Substring(num3, 1);
                            if (Encoding.Default.GetBytes(s).Length > 1)
                            {
                                str2 = str2 + "I";
                                windowsfont(70 + (num3 * 0x23), 0x94, 0x2d, 0, 0, 0, "黑体", s);
                            }
                            else
                            {
                                str2 = str2 + s;
                                num2 = 70 + (num3 * 0x23);
                                printerfont(num2.ToString(), "145", "5", "0", "1", "1", s);
                            }
                            num3++;
                        }
                        barcode("100", "50", "128", "99", "0", "0", "2", "4", NPNum[i].strNPTypeCode + str2);
                        windowsfont(20, 20, 20, 0, 0, 0, "黑体", NPNum[i].strNPTypeName);
                        windowsfont(320, 20, 20, 0, 0, 0, "黑体", NPNum[i].strNPTypeCode + "前");
                        num2 = 15 + (NPNum[i].strNPTypeName.Length * 0x17);
                        sendcommand("BOX 15,15," + num2.ToString() + ",45,3");
                        windowsfont(50, 140, 0x2d, 0, 0, 0, "黑体", NPNum[i].strNPNum.Substring(0, 1));
                        sendcommand("PUTPCX 25,210,\"NAME.PCX\"");
                    }
                    s = "";
                    str2 = "";
                    if (NPNum[i].bBackPiece)
                    {
                        for (num3 = 1; num3 < 7; num3++)
                        {
                            s = NPNum[i].strNPNum.Substring(num3, 1);
                            if (Encoding.Default.GetBytes(s).Length > 1)
                            {
                                str2 = str2 + "I";
                                windowsfont(480 + (num3 * 0x23), 0x94, 0x2d, 0, 0, 0, "黑体", s);
                            }
                            else
                            {
                                str2 = str2 + s;
                                printerfont((480 + (num3 * 0x23)).ToString(), "145", "5", "0", "1", "1", s);
                            }
                        }
                        barcode("520", "50", "128", "99", "0", "0", "2", "4", NPNum[i].strNPTypeCode + str2);
                        windowsfont(440, 20, 20, 0, 0, 0, "黑体", NPNum[i].strNPTypeName);
                        windowsfont(740, 20, 20, 0, 0, 0, "黑体", NPNum[i].strNPTypeCode + "后");
                        sendcommand("BOX 435,15," + ((0x1b3 + (NPNum[i].strNPTypeName.Length * 0x17))).ToString() + ",45,3");
                        windowsfont(470, 140, 0x2d, 0, 0, 0, "黑体", NPNum[i].strNPNum.Substring(0, 1));
                        sendcommand("PUTPCX 445,210,\"NAME.PCX\"");
                    }
                    printlabel("1", "1");
                }
                closeport();
            }
            catch
            {
            }
        }

        public void LabelPrintTaskPan(PanBarCode[] PanTaskID)
        {
            try
            {
                openport("TSC TTP-243E Plus");
                sendcommand("SIZE 100 mm,35 mm");
                sendcommand("GAP 2 mm,0");
                sendcommand("SPEED 4");
                sendcommand("DENSITY 7");
                sendcommand("DIRECTION 0");
                sendcommand("OFFSET 0.00");
                sendcommand("REFERENCE 0,0");
                sendcommand("SET PEEL OFF");
                sendcommand("SET CUTTER OFF");
                sendcommand("HOME");
                clearbuffer();
                downloadpcx("Name.pcx", "NAME.PCX");
                for (int i = PanTaskID.GetLowerBound(0); i <= PanTaskID.GetUpperBound(0); i += 2)
                {
                    clearbuffer();
                    sendcommand("BOX 15,20,380,250,2");
                    windowsfont(0x2d, 0x19, 0x19, 0, 0, 0, "黑体", PanTaskID[i].strPanType);
                    barcode("75", "60", "128", "100", "0", "0", "2", "4", PanTaskID[i].strTaskID);
                    printerfont("45", "160", "4", "0", "1", "1", PanTaskID[i].strTaskID);
                    sendcommand("PUTPCX 25,200,\"NAME.PCX\"");
                    if ((i + 1) <= PanTaskID.GetUpperBound(0))
                    {
                        sendcommand("BOX 425,20,790,250,2");
                        windowsfont(0x1c7, 0x19, 0x19, 0, 0, 0, "黑体", PanTaskID[i + 1].strPanType);
                        barcode("485", "50", "128", "110", "0", "0", "2", "4", PanTaskID[i + 1].strTaskID);
                        printerfont("455", "160", "4", "0", "1", "1", PanTaskID[i + 1].strTaskID);
                        sendcommand("PUTPCX 435,200,\"NAME.PCX\"");
                    }
                    printlabel("1", "1");
                }
                closeport();
            }
            catch
            {
            }
        }

        public void LabelPrintTaskSingle(string[] TaskID)
        {
            try
            {
                openport("TSC TTP-243E Plus");
                sendcommand("SIZE 100 mm,35 mm");
                sendcommand("GAP 2 mm,0");
                sendcommand("SPEED 4");
                sendcommand("DENSITY 7");
                sendcommand("DIRECTION 0");
                sendcommand("OFFSET 0.00");
                sendcommand("REFERENCE 0,0");
                sendcommand("SET PEEL OFF");
                sendcommand("SET CUTTER OFF");
                sendcommand("HOME");
                clearbuffer();
                downloadpcx("Name.pcx", "NAME.PCX");
                for (int i = TaskID.GetLowerBound(0); i <= TaskID.GetUpperBound(0); i += 2)
                {
                    clearbuffer();
                    sendcommand("BOX 15,20,380,250,6");
                    barcode("75", "50", "128", "110", "0", "0", "2", "4", TaskID[i]);
                    printerfont("40", "160", "4", "0", "1", "1", TaskID[i]);
                    sendcommand("PUTPCX 25,200,\"NAME.PCX\"");
                    if ((i + 1) <= TaskID.GetUpperBound(0))
                    {
                        sendcommand("BOX 425,20,790,250,6");
                        barcode("485", "50", "128", "110", "0", "0", "2", "4", TaskID[i + 1]);
                        printerfont("450", "160", "4", "0", "1", "1", TaskID[i + 1]);
                        sendcommand("PUTPCX 435,200,\"NAME.PCX\"");
                    }
                    printlabel("1", "1");
                }
                closeport();
            }
            catch
            {
            }
        }

        [DllImport("tsclib.dll")]
        private static extern void openport(string PrinterName);
        [DllImport("tsclib.dll")]
        private static extern void printerfont(string X, string Y, string FontName, string rotation, string Xmul, string Ymul, string Content);
        [DllImport("tsclib.dll")]
        private static extern void printlabel(string NumberOfSet, string NumberOfCopy);
        [DllImport("tsclib.dll")]
        private static extern void sendcommand(string command);
        [DllImport("tsclib.dll")]
        private static extern void setup(string LableWidth, string LabelHeight, string Speed, string Density, string Sensor, string Vertical, string Offset);
        public void TestOnly()
        {
            openport("TSC TTP-243E Plus");
            clearbuffer();
            sendcommand("SIZE 100 mm,35 mm");
            sendcommand("GAP 2 mm,0");
            sendcommand("SPEED 4");
            sendcommand("DENSITY 7");
            sendcommand("DIRECTION 0");
            sendcommand("OFFSET 0.00");
            sendcommand("REFERENCE 0,0");
            sendcommand("SET PEEL OFF");
            sendcommand("SET CUTTER OFF");
            sendcommand("SET COUNTER @0 +1");
            sendcommand("@0=\"000001\"");
            sendcommand("HOME");
            sendcommand("CLS");
            sendcommand("BOX 1,1,360,65,12");
            sendcommand("TEXT 25,25,\"3\",0,1,1,\"FORMFEED COMMAND TEST\"");
            sendcommand("TEXT 25,80,\"3\",0,1,1,@0");
            sendcommand("BOX 401,1,760,65,12");
            sendcommand("TEXT 425,25,\"3\",0,1,1,\"FORMFEED COMMAND TEST\"");
            sendcommand("TEXT 425,80,\"3\",0,1,1,@0");
            sendcommand("PRINT 5,1");
            closeport();
        }

        [DllImport("tsclib.dll")]
        private static extern void windowsfont(int X, int Y, int fontheight, int rotation, int fontstyle, int fontunderline, string FaceName, string TextContent);
    }
}

