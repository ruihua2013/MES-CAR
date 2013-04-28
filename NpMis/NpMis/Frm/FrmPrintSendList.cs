namespace NpMis.Frm
{
    using Common;
    using NpMis;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class FrmPrintSendList : Form
    {
        private Button btnExit;
        private Button btnPrint;
        public ComboBox combPlanDepart;
        private IContainer components;
        public DataGridView GridVwSendInfo;
        private GroupBox groupBox1;
        public bool IsPreview;
        private Label label1;
        private Label label2;
        private Label label3;
        public Label labelMakeperson;
        public Label labelMakeTime;
        private Label labelPlanDepart;
        private Label labelPlanID;
        private Label labelReceiveDepart;
        private Label labelReceivePerson;
        private Label labelSendID;
        private Label labelSendPerson;
        public Label labelTotal;
        private DataGridViewTextBoxColumn NPCount;
        private DataGridViewComboBoxColumn NPType;
        private PageSetupDialog pageSetupDialog1;
        public TextBox textPlanID;
        public TextBox textRemark;
        public TextBox textSendID;
        public TextBox textSendPerson;
        private DataGridViewTextBoxColumn XU;

        public FrmPrintSendList()
        {
            this.InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Dispose();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument document = new PrintDocument();
            document.PrintPage += new PrintPageEventHandler(this.Pd_PrintPageSend);
            try
            {
                PrintPreviewDialog dialog = new PrintPreviewDialog {
                    Document = document
                };
                PaperSize size = new PaperSize {
                    Height = 0x21f,
                    Width = 0x33b
                };
                Sql2KDataAccess access = new Sql2KDataAccess();
                DataSet set = access.Run_SqlText("select * from d_code where codetype=8 and code='SendListOff'");
                try
                {
                    size.Height = int.Parse(set.Tables[0].Rows[0]["codename"].ToString());
                }
                catch
                {
                    size.Height = 0x229;
                    string queryStr = "insert into d_code(code,codetype,codename,description) values('SendListOff',8,'" + size.Height + "','打印纸高度')";
                    access.Run_SqlText(queryStr);
                }
                document.DefaultPageSettings.PaperSize = size;
                document.PrinterSettings.PrinterName = Invoice.GetSendPrinter();
                dialog.WindowState = FormWindowState.Maximized;
                if (this.IsPreview)
                {
                    dialog.ShowDialog();
                }
                else
                {
                    document.Print();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("打印时发生错误" + '\n' + exception.Message, "打印送货单", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FrmPrintSendList_Load(object sender, EventArgs e)
        {
            List<User.PersonInfo> personInfoByUserName = User.GetPersonInfoByUserName(User.UserName);
            if (personInfoByUserName.Count > 0)
            {
                this.labelMakeperson.Text = "制表人 " + personInfoByUserName[0].TrueName;
            }
            this.labelMakeTime.Text = "制表时间 " + DateTime.Now;
            Sql2KDataAccess access = new Sql2KDataAccess();
            DataSet set = new DataSet();
            set = access.Run_SqlText("select * from d_code where codetype=8 and code='SendPerson'");
            try
            {
                this.textSendPerson.Text = set.Tables[0].Rows[0]["codename"].ToString();
            }
            catch
            {
                this.textSendPerson.Text = "程道民";
                string queryStr = "insert into d_code(code,codetype,codename,description) values('SendPerson',8,'" + this.textSendPerson.Text + "','送货人')";
                access.Run_SqlText(queryStr);
            }
            this.IsPreview = false;
        }

        private void GridVwSendInfo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 1)
                {
                    this.GridVwSendInfo.Rows[e.RowIndex].Cells[0].Value = Convert.ToString((int) (int.Parse(e.RowIndex.ToString()) + 1));
                }
                if (e.ColumnIndex == 2)
                {
                    int num = 0;
                    int num2 = 0;
                    for (int i = 0; i < (this.GridVwSendInfo.RowCount - 1); i++)
                    {
                        try
                        {
                            num = int.Parse(this.GridVwSendInfo.Rows[i].Cells[2].Value.ToString());
                        }
                        catch
                        {
                            this.GridVwSendInfo.Rows[i].Cells[2].Value = "0";
                            num = 0;
                        }
                        num2 += num;
                    }
                    this.labelTotal.Text = "总计 " + num2.ToString();
                }
            }
        }

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.label2 = new Label();
            this.labelPlanID = new Label();
            this.labelPlanDepart = new Label();
            this.labelSendID = new Label();
            this.GridVwSendInfo = new DataGridView();
            this.XU = new DataGridViewTextBoxColumn();
            this.NPType = new DataGridViewComboBoxColumn();
            this.NPCount = new DataGridViewTextBoxColumn();
            this.labelTotal = new Label();
            this.labelMakeperson = new Label();
            this.labelMakeTime = new Label();
            this.labelSendPerson = new Label();
            this.labelReceivePerson = new Label();
            this.labelReceiveDepart = new Label();
            this.btnExit = new Button();
            this.btnPrint = new Button();
            this.combPlanDepart = new ComboBox();
            this.textPlanID = new TextBox();
            this.textSendPerson = new TextBox();
            this.textSendID = new TextBox();
            this.label3 = new Label();
            this.textRemark = new TextBox();
            this.groupBox1 = new GroupBox();
            this.pageSetupDialog1 = new PageSetupDialog();
            ((ISupportInitialize) this.GridVwSendInfo).BeginInit();
            this.groupBox1.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Font = new Font("宋体", 21.75f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.label1.Location = new Point(0xce, 0x1b);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xfd, 0x1d);
            this.label1.TabIndex = 0;
            this.label1.Text = "机动车号牌送货单";
            this.label2.AutoSize = true;
            this.label2.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            this.label2.Location = new Point(0xf3, 0x3d);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0xa8, 0x10);
            this.label2.TabIndex = 0;
            this.label2.Text = "瑞华交通工程有限公司";
            this.labelPlanID.AutoSize = true;
            this.labelPlanID.Location = new Point(40, 0x62);
            this.labelPlanID.Name = "labelPlanID";
            this.labelPlanID.Size = new Size(0x35, 12);
            this.labelPlanID.TabIndex = 0;
            this.labelPlanID.Text = "计划单号";
            this.labelPlanDepart.AutoSize = true;
            this.labelPlanDepart.Location = new Point(0xfd, 0x63);
            this.labelPlanDepart.Name = "labelPlanDepart";
            this.labelPlanDepart.Size = new Size(0x35, 12);
            this.labelPlanDepart.TabIndex = 0;
            this.labelPlanDepart.Text = "计划单位";
            this.labelSendID.AutoSize = true;
            this.labelSendID.Location = new Point(0x1d1, 0x63);
            this.labelSendID.Name = "labelSendID";
            this.labelSendID.Size = new Size(0x35, 12);
            this.labelSendID.TabIndex = 0;
            this.labelSendID.Text = "送货单号";
            this.GridVwSendInfo.BackgroundColor = SystemColors.Control;
            this.GridVwSendInfo.BorderStyle = BorderStyle.Fixed3D;
            this.GridVwSendInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridVwSendInfo.Columns.AddRange(new DataGridViewColumn[] { this.XU, this.NPType, this.NPCount });
            this.GridVwSendInfo.Location = new Point(4, 15);
            this.GridVwSendInfo.Name = "GridVwSendInfo";
            this.GridVwSendInfo.RowTemplate.Height = 0x17;
            this.GridVwSendInfo.Size = new Size(0x25b, 150);
            this.GridVwSendInfo.TabIndex = 1;
            this.GridVwSendInfo.CellValueChanged += new DataGridViewCellEventHandler(this.GridVwSendInfo_CellValueChanged);
            this.XU.HeaderText = "序号";
            this.XU.Name = "XU";
            this.NPType.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.NPType.HeaderText = "机动车号牌类型";
            this.NPType.Name = "NPType";
            this.NPType.Resizable = DataGridViewTriState.True;
            this.NPType.SortMode = DataGridViewColumnSortMode.Automatic;
            this.NPCount.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.NPCount.HeaderText = "数量";
            this.NPCount.Name = "NPCount";
            this.labelTotal.AutoSize = true;
            this.labelTotal.Location = new Point(6, 0xa8);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new Size(0x1d, 12);
            this.labelTotal.TabIndex = 0;
            this.labelTotal.Text = "总计";
            this.labelMakeperson.AutoSize = true;
            this.labelMakeperson.Location = new Point(0x3e, 0x16d);
            this.labelMakeperson.Name = "labelMakeperson";
            this.labelMakeperson.Size = new Size(0x29, 12);
            this.labelMakeperson.TabIndex = 0;
            this.labelMakeperson.Text = "制表人";
            this.labelMakeTime.AutoSize = true;
            this.labelMakeTime.Location = new Point(0x113, 0x16d);
            this.labelMakeTime.Name = "labelMakeTime";
            this.labelMakeTime.Size = new Size(0x35, 12);
            this.labelMakeTime.TabIndex = 0;
            this.labelMakeTime.Text = "制表时间";
            this.labelSendPerson.AutoSize = true;
            this.labelSendPerson.Location = new Point(0x3e, 0x18a);
            this.labelSendPerson.Name = "labelSendPerson";
            this.labelSendPerson.Size = new Size(0x29, 12);
            this.labelSendPerson.TabIndex = 0;
            this.labelSendPerson.Text = "送货人";
            this.labelReceivePerson.AutoSize = true;
            this.labelReceivePerson.Location = new Point(0x113, 0x18a);
            this.labelReceivePerson.Name = "labelReceivePerson";
            this.labelReceivePerson.Size = new Size(0x29, 12);
            this.labelReceivePerson.TabIndex = 0;
            this.labelReceivePerson.Text = "签收人";
            this.labelReceiveDepart.AutoSize = true;
            this.labelReceiveDepart.Location = new Point(0x1e7, 0x18a);
            this.labelReceiveDepart.Name = "labelReceiveDepart";
            this.labelReceiveDepart.Size = new Size(0x35, 12);
            this.labelReceiveDepart.TabIndex = 0;
            this.labelReceiveDepart.Text = "签收单位";
            this.btnExit.Location = new Point(0x180, 0x1af);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(0x4b, 0x17);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.btnPrint.Location = new Point(0xf6, 0x1af);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new Size(0x4b, 0x17);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
            this.combPlanDepart.FormattingEnabled = true;
            this.combPlanDepart.Location = new Point(0x138, 0x60);
            this.combPlanDepart.Name = "combPlanDepart";
            this.combPlanDepart.Size = new Size(0x79, 20);
            this.combPlanDepart.TabIndex = 3;
            this.textPlanID.Location = new Point(0x63, 0x5f);
            this.textPlanID.Name = "textPlanID";
            this.textPlanID.Size = new Size(0x80, 0x15);
            this.textPlanID.TabIndex = 4;
            this.textSendPerson.BorderStyle = BorderStyle.None;
            this.textSendPerson.Location = new Point(0x6d, 0x187);
            this.textSendPerson.Name = "textSendPerson";
            this.textSendPerson.Size = new Size(0x80, 14);
            this.textSendPerson.TabIndex = 5;
            this.textSendPerson.Text = "程道民";
            this.textSendID.Location = new Point(0x205, 0x5f);
            this.textSendID.Name = "textSendID";
            this.textSendID.Size = new Size(0x80, 0x15);
            this.textSendID.TabIndex = 4;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(6, 0xc0);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "备注";
            this.textRemark.Location = new Point(0x2d, 0xbd);
            this.textRemark.Multiline = true;
            this.textRemark.Name = "textRemark";
            this.textRemark.Size = new Size(0x22e, 0x2f);
            this.textRemark.TabIndex = 6;
            this.groupBox1.Controls.Add(this.GridVwSendInfo);
            this.groupBox1.Controls.Add(this.textRemark);
            this.groupBox1.Controls.Add(this.labelTotal);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new Point(0x2a, 0x71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x265, 0xf7);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2a6, 0x1db);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.textSendPerson);
            base.Controls.Add(this.textSendID);
            base.Controls.Add(this.textPlanID);
            base.Controls.Add(this.combPlanDepart);
            base.Controls.Add(this.btnPrint);
            base.Controls.Add(this.btnExit);
            base.Controls.Add(this.labelSendID);
            base.Controls.Add(this.labelReceiveDepart);
            base.Controls.Add(this.labelReceivePerson);
            base.Controls.Add(this.labelSendPerson);
            base.Controls.Add(this.labelMakeTime);
            base.Controls.Add(this.labelMakeperson);
            base.Controls.Add(this.labelPlanDepart);
            base.Controls.Add(this.labelPlanID);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Name = "FrmPrintSendList";
            this.Text = "手工打印送货单";
            base.Load += new EventHandler(this.FrmPrintSendList_Load);
            ((ISupportInitialize) this.GridVwSendInfo).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void Pd_PrintPageSend(object sender, PrintPageEventArgs e)
        {
            new Font("隶书", 7.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            string s = "机动车号牌送货单";
            string str2 = "瑞华交通工程有限公司";
            Font font = new Font("黑体", 20f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            Font font2 = new Font("黑体", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            Font font3 = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            Font font4 = new Font("宋体", 12f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            Font font5 = new Font("宋体", 10f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            e.Graphics.DrawString(s, font, Brushes.Black, new PointF(300f, 90f));
            e.Graphics.DrawString(str2, font2, Brushes.Black, new PointF(330f, 60f));
            e.Graphics.DrawString(this.labelPlanID.Text + " " + this.textPlanID.Text.Trim(), font5, Brushes.Black, new PointF(70f, 130f));
            e.Graphics.DrawString(this.labelPlanDepart.Text + " " + this.combPlanDepart.Text.Trim(), font5, Brushes.Black, new PointF(300f, 130f));
            e.Graphics.DrawString(this.labelSendID.Text + " " + this.textSendID.Text.Trim(), font5, Brushes.Black, new PointF(550f, 130f));
            e.Graphics.DrawString(this.labelMakeperson.Text, font5, Brushes.Black, new PointF(70f, 420f));
            e.Graphics.DrawString(this.labelMakeTime.Text, font5, Brushes.Black, new PointF(300f, 420f));
            e.Graphics.DrawString(this.labelSendPerson.Text + " " + this.textSendPerson.Text.Trim(), font5, Brushes.Black, new PointF(70f, 450f));
            e.Graphics.DrawString(this.labelReceivePerson.Text, font5, Brushes.Black, new PointF(300f, 450f));
            e.Graphics.DrawString(this.labelReceiveDepart.Text, font5, Brushes.Black, new PointF(550f, 450f));
            Pen pen = new Pen(Brushes.Black) {
                Width = 1.2f
            };
            int num = 0x19;
            int num2 = 160;
            e.Graphics.DrawLine(pen, 70, num2, 770, num2);
            e.Graphics.DrawString("序号", font4, Brushes.Black, new PointF(150f, (float) (num2 + 3)));
            e.Graphics.DrawString("机动车号牌类型", font4, Brushes.Black, new PointF(300f, (float) (num2 + 3)));
            e.Graphics.DrawString("数量", font4, Brushes.Black, new PointF(550f, (float) (num2 + 3)));
            for (int i = 0; i < (this.GridVwSendInfo.RowCount - 1); i++)
            {
                num2 = 190 + (i * num);
                e.Graphics.DrawLine(pen, 70, num2, 770, num2);
                if (this.GridVwSendInfo.Rows[i].Cells[0].Value != null)
                {
                    e.Graphics.DrawString(this.GridVwSendInfo.Rows[i].Cells[0].Value.ToString(), font3, Brushes.Black, new PointF(150f, (float) (num2 + 3)));
                }
                if (this.GridVwSendInfo.Rows[i].Cells[1].FormattedValue != null)
                {
                    e.Graphics.DrawString(this.GridVwSendInfo.Rows[i].Cells[1].FormattedValue.ToString(), font3, Brushes.Black, new PointF(300f, (float) (num2 + 3)));
                }
                if (this.GridVwSendInfo.Rows[i].Cells[2].Value != null)
                {
                    e.Graphics.DrawString(this.GridVwSendInfo.Rows[i].Cells[2].Value.ToString(), font3, Brushes.Black, new PointF(550f, (float) (num2 + 3)));
                }
            }
            num2 += num;
            e.Graphics.DrawLine(pen, 70, num2, 770, num2);
            e.Graphics.DrawString("总计", font4, Brushes.Black, new PointF(300f, (float) (num2 + 3)));
            e.Graphics.DrawString(this.labelTotal.Text.Substring(2, this.labelTotal.Text.Length - 2), font4, Brushes.Black, new PointF(550f, (float) (num2 + 3)));
            num2 += num;
            e.Graphics.DrawLine(pen, 70, num2, 770, num2);
            num2 += 3;
            this.textRemark.Text = this.textRemark.Text.Replace("\r\n", "\r\n       ");
            e.Graphics.DrawString("备注 ：" + this.textRemark.Text, font5, Brushes.Black, new PointF(70f, (float) num2));
            this.textRemark.Text = this.textRemark.Text.Replace("\r\n       ", "\r\n");
            pen.Dispose();
        }
    }
}

