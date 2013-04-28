namespace NpMis
{
    using BarCodePrint;
    using Common;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    public class FrmSingleBarcode : Form
    {
        private Button btnCancel;
        private Button btnClear;
        private Button btnNPOK;
        private Button btnOK;
        private Button buttonClearPanSingle;
        private Button buttonClearTaskSingle;
        private Button buttonPanSingleOK;
        private Button buttonPrintPanSingle;
        private Button buttonPrintTaskSingle;
        private Button buttonTaskSingleOK;
        private CheckBox checkBackPiece;
        private CheckBox checkFrontPiece;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ComboBox combNPType;
        private IContainer components;
        private DataSet ds = new DataSet();
        public PersonInfo G_Operater;
        private Hashtable htNPType;
        private ColumnHeader id;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ListView listVwNPDetails;
        private ListView listVwPrintPanID;
        private ListView listVwPrintTaskID;
        private Sql2KDataAccess MyDB = new Sql2KDataAccess();
        private ArrayList NPList;
        private ColumnHeader NPNum;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TextBox textNPNum;
        private TextBox textPanIDSingle;
        private TextBox textTaskID;
        private TextBox textTaskIDSingle;
        private ColumnHeader WhichPiece;

        public FrmSingleBarcode()
        {
            this.InitializeComponent();
            this.htNPType = new Hashtable();
            this.ds = this.MyDB.Run_SqlText("select * from V_NPType");
            this.combNPType.DataSource = this.ds.Tables[0];
            this.combNPType.DisplayMember = "Description";
            this.combNPType.ValueMember = "Code";
            this.combNPType.SelectedIndex = 2;
            this.combNPType.Refresh();
            for (int i = 0; i < this.ds.Tables[0].Rows.Count; i++)
            {
                this.htNPType.Add(this.ds.Tables[0].Rows[i]["code"].ToString(), this.ds.Tables[0].Rows[i]["codename"].ToString());
            }
            this.NPList = new ArrayList();
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
            NPBarCode code = new NPBarCode {
                strNPNum = this.textNPNum.Text.Trim(),
                strNPTypeCode = this.combNPType.SelectedValue.ToString(),
                strNPTypeDescription = this.combNPType.Text,
                strNPTypeName = this.htNPType[this.combNPType.SelectedValue].ToString()
            };
            if (!this.IsNPInTask(this.textTaskID.Text.Trim(), this.textNPNum.Text.Trim()))
            {
                MessageBox.Show(this, "该车牌不在该任务单内！", "输入错误！");
            }
            else
            {
                if (this.checkFrontPiece.Checked)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = (this.listVwNPDetails.Items.Count + 1).ToString();
                    item.SubItems.Add(this.textNPNum.Text.Trim());
                    item.SubItems.Add("前片");
                    this.listVwNPDetails.Items.Add(item);
                    code.bFrontPiece = true;
                }
                else
                {
                    code.bFrontPiece = false;
                }
                if (this.checkBackPiece.Checked)
                {
                    ListViewItem item2 = new ListViewItem();
                    item2.Text = (this.listVwNPDetails.Items.Count + 1).ToString();
                    item2.SubItems.Add(this.textNPNum.Text.Trim());
                    item2.SubItems.Add("后片");
                    this.listVwNPDetails.Items.Add(item2);
                    code.bBackPiece = true;
                }
                else
                {
                    code.bBackPiece = false;
                }
                this.NPList.Add(code);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            NPBarCode[] nPNum = new NPBarCode[this.NPList.Count];
            Print print = new Print();
            IEnumerator enumerator = this.NPList.GetEnumerator();
            for (int i = 0; enumerator.MoveNext(); i++)
            {
                nPNum[i] = (NPBarCode) enumerator.Current;
            }
            print.LabelPrintSingleNP(nPNum);
            this.clear();
        }

        private void buttonClearPanSingle_Click(object sender, EventArgs e)
        {
            this.textPanIDSingle.Text = "M";
            this.listVwPrintPanID.Items.Clear();
        }

        private void buttonClearTaskSingle_Click(object sender, EventArgs e)
        {
            this.textTaskIDSingle.Text = "T";
            this.listVwPrintTaskID.Items.Clear();
        }

        private void buttonPanSingleOK_Click(object sender, EventArgs e)
        {
            string queryStr = "select * from V_pantaskinfo where pantaskid='" + this.textPanIDSingle.Text.Trim() + "'";
            DataSet set = new Sql2KDataAccess().Run_SqlText(queryStr);
            if ((set != null) && (set.Tables[0].Rows.Count > 0))
            {
                for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = (this.listVwPrintPanID.Items.Count + 1).ToString();
                    item.SubItems.Add(set.Tables[0].Rows[i]["pantaskid"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[i]["codename"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[i]["pancount"].ToString());
                    this.listVwPrintPanID.Items.Add(item);
                }
                this.textPanIDSingle.Text = "M";
            }
            else
            {
                MessageBox.Show(this, "您输入的底板单号错误！", "底板条码补打", MessageBoxButtons.OK);
            }
        }

        private void buttonPrintPanSingle_Click(object sender, EventArgs e)
        {
            if (this.listVwPrintPanID.Items.Count > 0)
            {
                PanBarCode[] panTaskID = new PanBarCode[this.listVwPrintPanID.Items.Count];
                for (int i = 0; i < this.listVwPrintPanID.Items.Count; i++)
                {
                    panTaskID[i].strTaskID = this.listVwPrintPanID.Items[i].SubItems[1].Text;
                    panTaskID[i].strPanType = this.listVwPrintPanID.Items[i].SubItems[2].Text;
                    panTaskID[i].strCount = this.listVwPrintPanID.Items[i].SubItems[3].Text;
                }
                new Print().LabelPrintTaskPan(panTaskID);
                this.textPanIDSingle.Text = "M";
                this.listVwPrintPanID.Items.Clear();
            }
        }

        private void buttonPrintTaskSingle_Click(object sender, EventArgs e)
        {
            if (this.listVwPrintTaskID.Items.Count > 0)
            {
                string[] taskID = new string[this.listVwPrintTaskID.Items.Count];
                for (int i = 0; i < this.listVwPrintTaskID.Items.Count; i++)
                {
                    taskID[i] = this.listVwPrintTaskID.Items[i].SubItems[1].Text;
                }
                new Print().LabelPrintTaskSingle(taskID);
                this.textTaskIDSingle.Text = "T";
                this.listVwPrintTaskID.Items.Clear();
            }
        }

        private void buttonTaskSingleOK_Click(object sender, EventArgs e)
        {
            ListViewItem item = new ListViewItem();
            item.Text = (this.listVwPrintTaskID.Items.Count + 1).ToString();
            item.SubItems.Add(this.textTaskIDSingle.Text.Trim());
            this.listVwPrintTaskID.Items.Add(item);
            this.textTaskIDSingle.Text = "T";
        }

        private void clear()
        {
            this.listVwNPDetails.Items.Clear();
            this.textNPNum.Text = "";
            this.textTaskID.Text = "T";
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
            this.btnClear = new Button();
            this.label1 = new Label();
            this.textTaskID = new TextBox();
            this.textTaskIDSingle = new TextBox();
            this.label2 = new Label();
            this.textPanIDSingle = new TextBox();
            this.label3 = new Label();
            this.buttonPrintTaskSingle = new Button();
            this.buttonClearTaskSingle = new Button();
            this.buttonPrintPanSingle = new Button();
            this.buttonClearPanSingle = new Button();
            this.buttonTaskSingleOK = new Button();
            this.buttonPanSingleOK = new Button();
            this.listVwPrintTaskID = new ListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.listVwPrintPanID = new ListView();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.columnHeader5 = new ColumnHeader();
            this.columnHeader6 = new ColumnHeader();
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.tabPage2 = new TabPage();
            this.tabPage3 = new TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            base.SuspendLayout();
            this.listVwNPDetails.Columns.AddRange(new ColumnHeader[] { this.id, this.NPNum, this.WhichPiece });
            this.listVwNPDetails.Location = new Point(0x2d, 0x84);
            this.listVwNPDetails.Name = "listVwNPDetails";
            this.listVwNPDetails.Size = new Size(0x108, 0xd0);
            this.listVwNPDetails.TabIndex = 6;
            this.listVwNPDetails.UseCompatibleStateImageBehavior = false;
            this.listVwNPDetails.View = View.Details;
            this.id.Text = "序号";
            this.NPNum.Text = "车牌号码";
            this.NPNum.Width = 0x7d;
            this.WhichPiece.Text = "前片/后片";
            this.WhichPiece.Width = 0x4a;
            this.btnOK.Location = new Point(90, 350);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "打印";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.Location = new Point(0x11a, 0x1b5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "退出";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
            this.textNPNum.Font = new Font("宋体", 24f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.textNPNum.Location = new Point(0x5c, 80);
            this.textNPNum.Name = "textNPNum";
            this.textNPNum.Size = new Size(160, 0x2c);
            this.textNPNum.TabIndex = 9;
            this.textNPNum.Text = "鲁B";
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x27, 0x60);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x29, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "车牌号";
            this.checkFrontPiece.AutoSize = true;
            this.checkFrontPiece.Location = new Point(0xcb, 0x13);
            this.checkFrontPiece.Name = "checkFrontPiece";
            this.checkFrontPiece.Size = new Size(0x30, 0x10);
            this.checkFrontPiece.TabIndex = 11;
            this.checkFrontPiece.Text = "前片";
            this.checkFrontPiece.UseVisualStyleBackColor = true;
            this.checkBackPiece.AutoSize = true;
            this.checkBackPiece.Location = new Point(0x100, 0x13);
            this.checkBackPiece.Name = "checkBackPiece";
            this.checkBackPiece.Size = new Size(0x30, 0x10);
            this.checkBackPiece.TabIndex = 12;
            this.checkBackPiece.Text = "后片";
            this.checkBackPiece.UseVisualStyleBackColor = true;
            this.btnNPOK.Location = new Point(0x100, 0x60);
            this.btnNPOK.Name = "btnNPOK";
            this.btnNPOK.Size = new Size(0x35, 0x17);
            this.btnNPOK.TabIndex = 13;
            this.btnNPOK.Text = "确定";
            this.btnNPOK.UseVisualStyleBackColor = true;
            this.btnNPOK.Click += new EventHandler(this.btnNPOK_Click);
            this.combNPType.FormattingEnabled = true;
            this.combNPType.Location = new Point(0x5d, 15);
            this.combNPType.Name = "combNPType";
            this.combNPType.Size = new Size(0x6a, 20);
            this.combNPType.TabIndex = 14;
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x27, 0x18);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x35, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "车牌类型";
            this.btnClear.Location = new Point(0xc2, 350);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(0x4b, 0x17);
            this.btnClear.TabIndex = 0x11;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x27, 0x34);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "任务单号";
            this.textTaskID.Location = new Point(0x5c, 0x31);
            this.textTaskID.Name = "textTaskID";
            this.textTaskID.Size = new Size(160, 0x15);
            this.textTaskID.TabIndex = 9;
            this.textTaskID.Text = "T";
            this.textTaskIDSingle.Location = new Point(0x71, 0x2c);
            this.textTaskIDSingle.Name = "textTaskIDSingle";
            this.textTaskIDSingle.Size = new Size(0x68, 0x15);
            this.textTaskIDSingle.TabIndex = 9;
            this.textTaskIDSingle.Text = "T";
            this.textTaskIDSingle.TextChanged += new EventHandler(this.textTaskIDSingle_TextChanged);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(60, 0x2f);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x35, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "任务单号";
            this.textPanIDSingle.Location = new Point(0x71, 0x2c);
            this.textPanIDSingle.Name = "textPanIDSingle";
            this.textPanIDSingle.Size = new Size(0x68, 0x15);
            this.textPanIDSingle.TabIndex = 9;
            this.textPanIDSingle.Text = "M";
            this.textPanIDSingle.TextChanged += new EventHandler(this.textPanIDSingle_TextChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(60, 0x2f);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "底板单号";
            this.buttonPrintTaskSingle.Location = new Point(90, 350);
            this.buttonPrintTaskSingle.Name = "buttonPrintTaskSingle";
            this.buttonPrintTaskSingle.Size = new Size(0x4b, 0x17);
            this.buttonPrintTaskSingle.TabIndex = 7;
            this.buttonPrintTaskSingle.Text = "打印";
            this.buttonPrintTaskSingle.UseVisualStyleBackColor = true;
            this.buttonPrintTaskSingle.Click += new EventHandler(this.buttonPrintTaskSingle_Click);
            this.buttonClearTaskSingle.Location = new Point(0xc2, 350);
            this.buttonClearTaskSingle.Name = "buttonClearTaskSingle";
            this.buttonClearTaskSingle.Size = new Size(0x4b, 0x17);
            this.buttonClearTaskSingle.TabIndex = 0x11;
            this.buttonClearTaskSingle.Text = "清除";
            this.buttonClearTaskSingle.UseVisualStyleBackColor = true;
            this.buttonClearTaskSingle.Click += new EventHandler(this.buttonClearTaskSingle_Click);
            this.buttonPrintPanSingle.Location = new Point(90, 350);
            this.buttonPrintPanSingle.Name = "buttonPrintPanSingle";
            this.buttonPrintPanSingle.Size = new Size(0x4b, 0x17);
            this.buttonPrintPanSingle.TabIndex = 7;
            this.buttonPrintPanSingle.Text = "打印";
            this.buttonPrintPanSingle.UseVisualStyleBackColor = true;
            this.buttonPrintPanSingle.Click += new EventHandler(this.buttonPrintPanSingle_Click);
            this.buttonClearPanSingle.Location = new Point(0xc2, 350);
            this.buttonClearPanSingle.Name = "buttonClearPanSingle";
            this.buttonClearPanSingle.Size = new Size(0x4b, 0x17);
            this.buttonClearPanSingle.TabIndex = 0x11;
            this.buttonClearPanSingle.Text = "清除";
            this.buttonClearPanSingle.UseVisualStyleBackColor = true;
            this.buttonClearPanSingle.Click += new EventHandler(this.buttonClearPanSingle_Click);
            this.buttonTaskSingleOK.Location = new Point(0xf3, 0x2a);
            this.buttonTaskSingleOK.Name = "buttonTaskSingleOK";
            this.buttonTaskSingleOK.Size = new Size(0x35, 0x17);
            this.buttonTaskSingleOK.TabIndex = 13;
            this.buttonTaskSingleOK.Text = "确定";
            this.buttonTaskSingleOK.UseVisualStyleBackColor = true;
            this.buttonTaskSingleOK.Click += new EventHandler(this.buttonTaskSingleOK_Click);
            this.buttonPanSingleOK.Location = new Point(0xf3, 0x2a);
            this.buttonPanSingleOK.Name = "buttonPanSingleOK";
            this.buttonPanSingleOK.Size = new Size(0x35, 0x17);
            this.buttonPanSingleOK.TabIndex = 13;
            this.buttonPanSingleOK.Text = "确定";
            this.buttonPanSingleOK.UseVisualStyleBackColor = true;
            this.buttonPanSingleOK.Click += new EventHandler(this.buttonPanSingleOK_Click);
            this.listVwPrintTaskID.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2 });
            this.listVwPrintTaskID.Location = new Point(0x3e, 0x5e);
            this.listVwPrintTaskID.Name = "listVwPrintTaskID";
            this.listVwPrintTaskID.Size = new Size(0xee, 220);
            this.listVwPrintTaskID.TabIndex = 0x12;
            this.listVwPrintTaskID.UseCompatibleStateImageBehavior = false;
            this.listVwPrintTaskID.View = View.Details;
            this.columnHeader1.Text = "序号";
            this.columnHeader2.Text = "任务单号";
            this.columnHeader2.Width = 170;
            this.listVwPrintPanID.Columns.AddRange(new ColumnHeader[] { this.columnHeader3, this.columnHeader4, this.columnHeader5, this.columnHeader6 });
            this.listVwPrintPanID.Location = new Point(0x3e, 0x5e);
            this.listVwPrintPanID.Name = "listVwPrintPanID";
            this.listVwPrintPanID.Size = new Size(250, 220);
            this.listVwPrintPanID.TabIndex = 0x12;
            this.listVwPrintPanID.UseCompatibleStateImageBehavior = false;
            this.listVwPrintPanID.View = View.Details;
            this.columnHeader3.Text = "序号";
            this.columnHeader3.Width = 40;
            this.columnHeader4.Text = "底板单号";
            this.columnHeader4.Width = 100;
            this.columnHeader5.Text = "底板类型";
            this.columnHeader5.Width = 0x41;
            this.columnHeader6.Text = "数量";
            this.columnHeader6.Width = 40;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new Point(0, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x174, 420);
            this.tabControl1.TabIndex = 20;
            this.tabPage1.Controls.Add(this.listVwNPDetails);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.btnClear);
            this.tabPage1.Controls.Add(this.textNPNum);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textTaskID);
            this.tabPage1.Controls.Add(this.btnNPOK);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.checkFrontPiece);
            this.tabPage1.Controls.Add(this.combNPType);
            this.tabPage1.Controls.Add(this.btnOK);
            this.tabPage1.Controls.Add(this.checkBackPiece);
            this.tabPage1.Location = new Point(4, 0x15);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x16c, 0x18b);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "车牌条码补打";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage2.Controls.Add(this.listVwPrintTaskID);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.textTaskIDSingle);
            this.tabPage2.Controls.Add(this.buttonTaskSingleOK);
            this.tabPage2.Controls.Add(this.buttonClearTaskSingle);
            this.tabPage2.Controls.Add(this.buttonPrintTaskSingle);
            this.tabPage2.Location = new Point(4, 0x15);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(0x16c, 0x18b);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "任务单条码补打";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage3.Controls.Add(this.listVwPrintPanID);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.textPanIDSingle);
            this.tabPage3.Controls.Add(this.buttonPanSingleOK);
            this.tabPage3.Controls.Add(this.buttonClearPanSingle);
            this.tabPage3.Controls.Add(this.buttonPrintPanSingle);
            this.tabPage3.Location = new Point(4, 0x15);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new Padding(3);
            this.tabPage3.Size = new Size(0x16c, 0x18b);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "底板条码补打";
            this.tabPage3.UseVisualStyleBackColor = true;
            base.ClientSize = new Size(0x171, 0x1d8);
            base.Controls.Add(this.tabControl1);
            base.Controls.Add(this.btnCancel);
            base.Name = "FrmSingleBarcode";
            this.Text = "单个条码补打";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            base.ResumeLayout(false);
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

        private void textNPNum_TextChanged(object sender, EventArgs e)
        {
            this.textNPNum.Text = this.VeryfyOthersNP(this.textNPNum.Text);
            this.textNPNum.SelectionStart = this.textNPNum.Text.Length;
        }

        private void textPanIDSingle_TextChanged(object sender, EventArgs e)
        {
            this.textPanIDSingle.Text = this.textPanIDSingle.Text.ToUpper();
        }

        private void textTaskIDSingle_TextChanged(object sender, EventArgs e)
        {
            this.textTaskIDSingle.Text = this.textTaskIDSingle.Text.ToUpper();
        }

        private string VeryfycharAfer(string charAfer)
        {
            string str = "";
            if (charAfer.Length < 1)
            {
                return "";
            }
            charAfer = charAfer.ToUpper();
            for (int i = 0; i < charAfer.Length; i++)
            {
                byte[] bytes = Encoding.Default.GetBytes(charAfer.Substring(i, 1));
                int num = bytes[0];
                if (bytes.Length > 1)
                {
                    str = str + charAfer.Substring(i, 1);
                }
                else if ((((num <= 0) || (num >= 0x30)) && ((num <= 0x39) || (num >= 0x41))) && (((num <= 90) || (num >= 0x61)) && (num <= 0x84)))
                {
                    str = str + charAfer.Substring(i, 1);
                }
            }
            return str;
        }

        private string VeryfyOthersNP(string NpStr)
        {
            NpStr = NpStr.ToUpper();
            return this.VeryfycharAfer(NpStr);
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

