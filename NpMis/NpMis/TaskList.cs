namespace NpMis
{
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class TaskList
    {
        private string[,] a;
        private PrintDocument Pd = new PrintDocument();
        private string perName;
        private string proId;
        private string tTime;

        public TaskList()
        {
            this.Pd.PrintPage += new PrintPageEventHandler(this.Pd_PrintPage);
        }

        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            new Font("隶书", 7.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            string s = "生产任务单";
            string str2 = "单号：";
            string str3 = "下单人：";
            string str4 = "日期：";
            string str5 = "序号";
            string str6 = "类型";
            string str7 = "车牌号";
            string str8 = "备注";
            Font font = new Font("黑体", 18f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            Font font2 = new Font("宋体", 15f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            Font font3 = new Font("宋体", 13f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            e.Graphics.DrawString(s, font, Brushes.Black, new PointF(350f, 20f));
            e.Graphics.DrawString(str2, font2, Brushes.Black, new PointF(70f, 75f));
            e.Graphics.DrawString(this.proId, font2, Brushes.Black, new PointF(125f, 75f));
            e.Graphics.DrawString(str3, font2, Brushes.Black, new PointF(320f, 75f));
            e.Graphics.DrawString(this.perName, font2, Brushes.Black, new PointF(395f, 75f));
            e.Graphics.DrawString(str4, font2, Brushes.Black, new PointF(530f, 75f));
            e.Graphics.DrawString(this.tTime, font2, Brushes.Black, new PointF(590f, 75f));
            Pen pen = new Pen(Brushes.Black) {
                Width = 2f
            };
            e.Graphics.DrawRectangle(pen, new Rectangle(70, 110, 700, 920));
            int num = 0x23;
            for (int i = 0; i <= 0x19; i++)
            {
                e.Graphics.DrawLine(pen, 70, 110 + (i * num), 770, 110 + (i * num));
                int num3 = i - 1;
                if (num3 >= 0)
                {
                    if (num3 <= this.a.GetUpperBound(0))
                    {
                        e.Graphics.DrawString(this.a.GetValue(num3, 0).ToString(), font2, Brushes.Black, new PointF(72f, (float) (0x75 + (num * (num3 + 1)))));
                        e.Graphics.DrawString(this.a.GetValue(num3, 1).ToString(), font2, Brushes.Black, new PointF(122f, (float) (0x75 + (num * (num3 + 1)))));
                        e.Graphics.DrawString(this.a.GetValue(num3, 2).ToString(), font2, Brushes.Black, new PointF(200f, (float) (0x75 + (num * (num3 + 1)))));
                        if ((num3 + 0x19) <= this.a.GetUpperBound(0))
                        {
                            e.Graphics.DrawString(this.a.GetValue(num3 + 0x19, 0).ToString(), font2, Brushes.Black, new PointF(427f, (float) (0x75 + (num * (num3 + 1)))));
                            e.Graphics.DrawString(this.a.GetValue(num3 + 0x19, 1).ToString(), font2, Brushes.Black, new PointF(476f, (float) (0x75 + (num * (num3 + 1)))));
                            e.Graphics.DrawString(this.a.GetValue(num3 + 0x19, 2).ToString(), font2, Brushes.Black, new PointF(552f, (float) (0x75 + (num * (num3 + 1)))));
                        }
                    }
                }
                else
                {
                    e.Graphics.DrawString(str5, font3, Brushes.Black, new PointF(72f, 117f));
                    e.Graphics.DrawString(str6, font3, Brushes.Black, new PointF(122f, 117f));
                    e.Graphics.DrawString(str7, font3, Brushes.Black, new PointF(222f, 117f));
                    e.Graphics.DrawString(str8, font3, Brushes.Black, new PointF(322f, 117f));
                    e.Graphics.DrawString(str5, font3, Brushes.Black, new PointF(422f, 117f));
                    e.Graphics.DrawString(str6, font3, Brushes.Black, new PointF(472f, 117f));
                    e.Graphics.DrawString(str7, font3, Brushes.Black, new PointF(572f, 117f));
                    e.Graphics.DrawString(str8, font3, Brushes.Black, new PointF(672f, 117f));
                }
            }
            e.Graphics.DrawLine(pen, 120, 110, 120, 0x406);
            e.Graphics.DrawLine(pen, 0xc3, 110, 0xc3, 0x406);
            e.Graphics.DrawLine(pen, 0x13b, 110, 0x13b, 0x406);
            e.Graphics.DrawLine(pen, 0x19f, 110, 0x19f, 0x406);
            e.Graphics.DrawLine(pen, 0x1db, 110, 0x1db, 0x406);
            e.Graphics.DrawLine(pen, 550, 110, 550, 0x406);
            e.Graphics.DrawLine(pen, 670, 110, 670, 0x406);
            e.Graphics.DrawString("共计:", font2, Brushes.Black, new PointF(70f, 1040f));
            e.Graphics.DrawString(Convert.ToString((int) (this.a.GetUpperBound(0) + 1)), font2, Brushes.Black, new PointF(150f, 1040f));
            pen.Dispose();
        }

        public void PrintTaskList()
        {
            try
            {
                new PrintPreviewDialog { Document = this.Pd, WindowState = FormWindowState.Maximized };
                this.Pd.Print();
            }
            catch (Exception exception)
            {
                MessageBox.Show("打印时发生错误" + '\n' + exception.Message, "打印生产任务单", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void ShowTaskList()
        {
            new PrintPreviewDialog { Document = this.Pd, WindowState = FormWindowState.Maximized }.ShowDialog();
        }

        public string[,] PrintArray
        {
            get
            {
                return this.a;
            }
            set
            {
                this.a = value;
            }
        }

        public string TaskId
        {
            get
            {
                return this.proId;
            }
            set
            {
                this.proId = value;
            }
        }

        public string TaskTime
        {
            get
            {
                return this.tTime;
            }
            set
            {
                this.tTime = value;
            }
        }

        public string TaskUser
        {
            get
            {
                return this.perName;
            }
            set
            {
                this.perName = value;
            }
        }
    }
}

