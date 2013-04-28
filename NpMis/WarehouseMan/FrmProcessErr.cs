namespace WarehouseMan
{
    using Common;
    using NpMis;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class FrmProcessErr : Form
    {
        private Button btnCancel;
        private Button btnClear;
        private Button btnNPOK;
        private Button btnOK;
        private CheckBox checkBackPiece;
        private CheckBox checkFrontPiece;
        private ComboBox combNPType;
        private ComboBox combProcess;
        private IContainer components;
        private DataSet ds = new DataSet();
        public PersonInfo G_Operater;
        private ColumnHeader id;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private ListView listVwNPDetails;
        private Sql2KDataAccess MyDB = new Sql2KDataAccess();
        private ColumnHeader NPNum;
        private RadioButton radioDestory;
        private RadioButton radioMis;
        private TextBox textCount;
        private TextBox textNPNum;
        private TextBox textRemarks;
        private TextBox textTaskID;
        private ColumnHeader WhichPiece;

        public FrmProcessErr()
        {
            this.InitializeComponent();
            this.ds = this.MyDB.Run_SqlText("select description,code from V_NPType");
            this.combNPType.DataSource = this.ds.Tables[0].DefaultView;
            this.combNPType.DisplayMember = "description";
            this.combNPType.ValueMember = "code";
            this.combNPType.SelectedIndex = 2;
            this.combNPType.Refresh();
            this.ds = this.MyDB.Run_SqlText("select codename,code from V_processall");
            this.combProcess.DataSource = this.ds.Tables[0].DefaultView;
            this.combProcess.DisplayMember = "codename";
            this.combProcess.ValueMember = "code";
            this.combProcess.SelectedIndex = 2;
            this.combProcess.Refresh();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Dispose();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.clear();
        }

        private void btnNPOK_Click(object sender, EventArgs e)
        {
            if (this.textNPNum.Text.Trim().Substring(0, 1).CompareTo("鲁") != 0)
            {
                this.textNPNum.Text = "鲁" + this.textNPNum.Text.Trim();
            }
            if ((this.textNPNum.Text.Length > 0) && this.IsNPInTask(this.textTaskID.Text.Trim(), this.textNPNum.Text.Trim()))
            {
                if (this.checkFrontPiece.Checked)
                {
                    ListViewItem item = new ListViewItem {
                        Text = this.listVwNPDetails.Items.Count.ToString()
                    };
                    item.SubItems.Add(this.textNPNum.Text.Trim());
                    item.SubItems.Add("前片");
                    this.listVwNPDetails.Items.Add(item);
                }
                if (this.checkBackPiece.Checked)
                {
                    ListViewItem item2 = new ListViewItem {
                        Text = this.listVwNPDetails.Items.Count.ToString()
                    };
                    item2.SubItems.Add(this.textNPNum.Text.Trim());
                    item2.SubItems.Add("后片");
                    this.listVwNPDetails.Items.Add(item2);
                }
            }
            else
            {
                this.textNPNum.Text = "鲁";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.VerfyData() != 0)
            {
                string str;
                int num = int.Parse(this.textCount.Text.Trim());
                if (this.radioDestory.Checked)
                {
                    str = string.Concat(new object[] { "insert into t_ProcessErr(taskid,errcount,findprocess,DestoryorMis,remark,recordU,recordTime)values('", this.textTaskID.Text.Trim(), "',", num, ",'", this.combProcess.SelectedValue, "', '损','", this.textRemarks.Text.Trim(), "','", User.UserName, "','", DateTime.Now, "')" });
                }
                else
                {
                    str = string.Concat(new object[] { "insert into t_ProcessErr(taskid,errcount,findprocess,remark,recordU,recordTime)values('", this.textTaskID.Text.Trim(), "',", num, ",'", this.combProcess.SelectedValue, "','丢','", this.textRemarks.Text.Trim(), "','", User.UserName, "','", DateTime.Now, "')" });
                }
                this.ds = this.MyDB.Run_SqlText(str);
                num = int.Parse(this.textCount.Text.Trim());
                if (this.textTaskID.Text.Substring(0, 1).CompareTo("M") == 0)
                {
                    str = string.Concat(new object[] { "update t_pantaskinfo set pancount=pancount -", num, "where pantaskid='", this.textTaskID.Text.Trim(), "'" });
                    this.ds = this.MyDB.Run_SqlText(str);
                }
                for (int i = 0; i < this.listVwNPDetails.Items.Count; i++)
                {
                    ListViewItem item = this.listVwNPDetails.Items[i];
                    if (item.SubItems[2].Text.CompareTo("前片") == 0)
                    {
                        str = "update t_np  set frontpiece=0  where taskid='" + this.textTaskID.Text.Trim() + "' and npno='" + this.textNPNum.Text.Trim() + "'";
                    }
                    else
                    {
                        str = "update t_np  set backpiece=0  where taskid='" + this.textTaskID.Text.Trim() + "' and npno='" + this.textNPNum.Text.Trim() + "'";
                    }
                    this.ds = this.MyDB.Run_SqlText(str);
                }
                if (this.ds != null)
                {
                    MessageBox.Show(this, this.textTaskID.Text.Trim() + "损坏提交成功！", "损毁登记", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private void clear()
        {
            this.textTaskID.Text = "";
            this.textCount.Text = "";
            this.textRemarks.Text = "";
            this.listVwNPDetails.Items.Clear();
            this.textNPNum.Text = "";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.textTaskID = new TextBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.textRemarks = new TextBox();
            this.label3 = new Label();
            this.textCount = new TextBox();
            this.listVwNPDetails = new ListView();
            this.id = new ColumnHeader();
            this.NPNum = new ColumnHeader();
            this.WhichPiece = new ColumnHeader();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.textNPNum = new TextBox();
            this.label4 = new Label();
            this.checkFrontPiece = new CheckBox();
            this.checkBackPiece = new CheckBox();
            this.btnNPOK = new Button();
            this.combNPType = new ComboBox();
            this.label5 = new Label();
            this.label6 = new Label();
            this.combProcess = new ComboBox();
            this.btnClear = new Button();
            this.radioDestory = new RadioButton();
            this.radioMis = new RadioButton();
            base.SuspendLayout();
            this.textTaskID.Location = new Point(0x53, 0x12);
            this.textTaskID.Name = "textTaskID";
            this.textTaskID.Size = new Size(0x8e, 0x15);
            this.textTaskID.TabIndex = 0;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x30, 0x12);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1d, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "箱号";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x30, 0x99);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1d, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "说明";
            this.textRemarks.Location = new Point(0x53, 0x99);
            this.textRemarks.Multiline = true;
            this.textRemarks.Name = "textRemarks";
            this.textRemarks.Size = new Size(0x8e, 120);
            this.textRemarks.TabIndex = 3;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x30, 0x3b);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x1d, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "数量";
            this.textCount.Location = new Point(0x53, 0x3b);
            this.textCount.Name = "textCount";
            this.textCount.Size = new Size(0x8e, 0x15);
            this.textCount.TabIndex = 5;
            this.listVwNPDetails.Columns.AddRange(new ColumnHeader[] { this.id, this.NPNum, this.WhichPiece });
            this.listVwNPDetails.Location = new Point(0xf7, 0x56);
            this.listVwNPDetails.Name = "listVwNPDetails";
            this.listVwNPDetails.Size = new Size(0x10c, 0xbb);
            this.listVwNPDetails.TabIndex = 6;
            this.listVwNPDetails.UseCompatibleStateImageBehavior = false;
            this.listVwNPDetails.View = View.Details;
            this.id.Text = "序号";
            this.NPNum.Text = "车牌号码";
            this.NPNum.Width = 0x7d;
            this.WhichPiece.Text = "前片/后片";
            this.WhichPiece.Width = 0x4a;
            this.btnOK.Location = new Point(0xa9, 0x124);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Location = new Point(0x179, 0x124);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.textNPNum.Location = new Point(0x12f, 0x35);
            this.textNPNum.Name = "textNPNum";
            this.textNPNum.Size = new Size(0x5f, 0x15);
            this.textNPNum.TabIndex = 9;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0xf5, 0x3b);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x29, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "车牌号";
            this.checkFrontPiece.AutoSize = true;
            this.checkFrontPiece.Location = new Point(0x194, 0x13);
            this.checkFrontPiece.Name = "checkFrontPiece";
            this.checkFrontPiece.Size = new Size(0x30, 0x10);
            this.checkFrontPiece.TabIndex = 11;
            this.checkFrontPiece.Text = "前片";
            this.checkFrontPiece.UseVisualStyleBackColor = true;
            this.checkBackPiece.AutoSize = true;
            this.checkBackPiece.Location = new Point(0x1c1, 0x12);
            this.checkBackPiece.Name = "checkBackPiece";
            this.checkBackPiece.Size = new Size(0x30, 0x10);
            this.checkBackPiece.TabIndex = 12;
            this.checkBackPiece.Text = "后片";
            this.checkBackPiece.UseVisualStyleBackColor = true;
            this.btnNPOK.Location = new Point(0x194, 0x33);
            this.btnNPOK.Name = "btnNPOK";
            this.btnNPOK.Size = new Size(80, 0x17);
            this.btnNPOK.TabIndex = 13;
            this.btnNPOK.Text = "确定";
            this.btnNPOK.UseVisualStyleBackColor = true;
            this.btnNPOK.Click += new EventHandler(this.btnNPOK_Click);
            this.combNPType.FormattingEnabled = true;
            this.combNPType.Location = new Point(0x12f, 0x10);
            this.combNPType.Name = "combNPType";
            this.combNPType.Size = new Size(0x5f, 20);
            this.combNPType.TabIndex = 14;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0xf5, 0x16);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "车牌类型";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x30, 100);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x1d, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "工序";
            this.combProcess.FormattingEnabled = true;
            this.combProcess.Location = new Point(0x53, 100);
            this.combProcess.Name = "combProcess";
            this.combProcess.Size = new Size(0x8e, 20);
            this.combProcess.TabIndex = 0x10;
            this.btnClear.Location = new Point(0x112, 0x124);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(0x4b, 0x17);
            this.btnClear.TabIndex = 0x11;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.radioDestory.AutoSize = true;
            this.radioDestory.Location = new Point(0x53, 0x83);
            this.radioDestory.Name = "radioDestory";
            this.radioDestory.Size = new Size(0x2f, 0x10);
            this.radioDestory.TabIndex = 0x12;
            this.radioDestory.TabStop = true;
            this.radioDestory.Text = "损坏";
            this.radioDestory.UseVisualStyleBackColor = true;
            this.radioMis.AutoSize = true;
            this.radioMis.Location = new Point(0x95, 0x83);
            this.radioMis.Name = "radioMis";
            this.radioMis.Size = new Size(0x2f, 0x10);
            this.radioMis.TabIndex = 0x12;
            this.radioMis.TabStop = true;
            this.radioMis.Text = "丢失";
            this.radioMis.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x246, 0x14e);
            base.Controls.Add(this.radioMis);
            base.Controls.Add(this.radioDestory);
            base.Controls.Add(this.btnClear);
            base.Controls.Add(this.combProcess);
            base.Controls.Add(this.label5);
            base.Controls.Add(this.combNPType);
            base.Controls.Add(this.btnNPOK);
            base.Controls.Add(this.checkBackPiece);
            base.Controls.Add(this.checkFrontPiece);
            base.Controls.Add(this.label4);
            base.Controls.Add(this.textNPNum);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.listVwNPDetails);
            base.Controls.Add(this.textCount);
            base.Controls.Add(this.label6);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.textRemarks);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.textTaskID);
            base.Name = "FrmProcessErr";
            this.Text = "损毁登记";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private bool IsNPInTask(string TaskID, string NPNum)
        {
            string queryStr = string.Concat(new object[] { "select * from T_NP where TaskID='", TaskID, "' and NPNo='", NPNum, "' and nptype='", this.combNPType.SelectedValue, "'" });
            this.ds = this.MyDB.Run_SqlText(queryStr);
            return ((this.ds != null) && (this.ds.Tables[0].Rows.Count > 0));
        }

        private bool IsNPTask(string TaskID)
        {
            string queryStr = "select * from t_task where taskId='" + TaskID.Trim() + "'";
            this.ds = this.MyDB.Run_SqlText(queryStr);
            return ((this.ds != null) && (this.ds.Tables[0].Rows.Count == 1));
        }

        private bool IsPanTask(string TaskID)
        {
            string queryStr = "select * from t_pantaskinfo where barcode='" + TaskID.Trim() + "'";
            this.ds = this.MyDB.Run_SqlText(queryStr);
            return ((this.ds != null) && (this.ds.Tables[0].Rows.Count == 1));
        }

        private int VerfyData()
        {
            if (!this.IsNPTask(this.textTaskID.Text.Trim()) && !this.IsPanTask(this.textTaskID.Text.Trim()))
            {
                this.textTaskID.Text = "";
            }
            if (this.textTaskID.Text.Length == 0)
            {
                this.textTaskID.Focus();
                return 0;
            }
            try
            {
                if (this.textCount.Text.Length == 0)
                {
                    this.textCount.Focus();
                    return 0;
                }
                int.Parse(this.textCount.Text.Trim());
            }
            catch
            {
                this.textCount.Text = "";
                this.textCount.Focus();
                return 0;
            }
            if (this.textRemarks.Text.Length == 0)
            {
                this.textRemarks.Focus();
                return 0;
            }
            return 1;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PersonInfo
        {
            public string BarCode;
            public string UserName;
            public string TrueName;
        }
    }
}

