namespace NpMis
{
    using BarCodePrint;
    using Common;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class Frm_MatherBoard : Form
    {
        private Button btn_Ok;
        private Button btn_PrintBarCode;
        private Button btnPrintWorkPlan;
        private CheckBox cbx_SelectAll;
        private CheckBox cbx_SelectBarCode;
        private ComboBox cbx_Size;
        private ComboBox cbx_WProcedure;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader10;
        private ColumnHeader columnHeader11;
        private ColumnHeader columnHeader12;
        private ColumnHeader columnHeader13;
        private ColumnHeader columnHeader14;
        private ColumnHeader columnHeader15;
        private ColumnHeader columnHeader16;
        private ColumnHeader columnHeader17;
        private ColumnHeader columnHeader18;
        private ColumnHeader columnHeader19;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader20;
        private ColumnHeader columnHeader21;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private IContainer components;
        private DateTimePicker dtp_WorkDay;
        private GroupBox gpb_BarCode;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ImageList ilt_Icon;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label labelWorker;
        private ListView listVwWorkers;
        private SortedListView lvw_BarCode;
        private SortedListView Lvw_MbTask;
        private SortedListView lvw_NPTask;
        private static Frm_MatherBoard m_Instance;
        private ColumnHeader MakePerson;
        private ColumnHeader MakeTime;
        private MbTask mt;
        private Panel panel1;
        private Panel panel2;
        private Panel panel4;
        private ColumnHeader Process;
        private ColumnHeader Remark;
        private TextBox tbx_Amount;
        private TextBox tbx_Remark;
        private ColumnHeader WorkDay;
        private ColumnHeader Worker;
        private ColumnHeader WorkLoad;
        private ColumnHeader WorkPlanID;

        private Frm_MatherBoard()
        {
            this.InitializeComponent();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (((this.tbx_Amount.Text.Trim() == "") || (this.tbx_Amount.Text.Trim() == "0")) && (this.cbx_WProcedure.SelectedValue.ToString() != "3"))
            {
                MessageBox.Show("请输入数量", "工作安排", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tbx_Amount.Focus();
            }
            else if (this.listVwWorkers.CheckedItems.Count == 0)
            {
                MessageBox.Show("请选择工作人员", "工作安排", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (this.cbx_WProcedure.SelectedValue.ToString() != "11")
                {
                    long num;
                    if (this.gpb_BarCode.Tag.ToString() == "0")
                    {
                        MessageBox.Show("没有要下达的任务", "工作安排", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    try
                    {
                        num = long.Parse(this.tbx_Amount.Text.Trim());
                    }
                    catch
                    {
                        num = 0L;
                        this.tbx_Amount.Text = "0";
                    }
                    if (num > long.Parse(this.gpb_BarCode.Tag.ToString()))
                    {
                        MessageBox.Show("要下达任务的数量大于待下达的数量", "工作安排", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                this.SaveWorkPlanToDB();
                this.tbx_Remark.Text = "";
                if (this.mt.NewTask(this.cbx_Size.SelectedValue.ToString().Trim(), long.Parse(this.tbx_Amount.Text.Trim()), this.cbx_WProcedure.SelectedValue.ToString().Trim()) != "")
                {
                    MessageBox.Show("工作安排成功", "工作安排", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.Frm_MatherBoard_Load(this, e);
                }
                else
                {
                    MessageBox.Show("工作安排失败", "工作安排", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btn_PrintBarCode_Click(object sender, EventArgs e)
        {
            int boxCapability = new MbTask().GetBoxCapability();
            try
            {
                foreach (ListViewItem item in this.Lvw_MbTask.CheckedItems)
                {
                    PanBarCode[] codeArray;
                    if (item.SubItems[2].Text.ToString().Trim() != "清洗烘干")
                    {
                        MessageBox.Show("只有清洗烘干工序可以打印条形码", "底板任务管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    int num2 = ((int.Parse(item.SubItems[3].Text.ToString()) % boxCapability) == 0) ? (int.Parse(item.SubItems[3].Text.ToString()) / boxCapability) : ((int.Parse(item.SubItems[3].Text.ToString()) / boxCapability) + 1);
                    string queryStr = "select * from t_pantaskinfo where left(pantaskid,12)='" + item.Text + "'";
                    DataSet set = new Sql2KDataAccess().Run_SqlText(queryStr);
                    if ((set != null) && (set.Tables[0].Rows.Count > 0))
                    {
                        codeArray = new PanBarCode[set.Tables[0].Rows.Count];
                        for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                        {
                            codeArray[i].strCount = set.Tables[0].Rows[i]["pancount"].ToString();
                            codeArray[i].strTaskID = set.Tables[0].Rows[i]["pantaskid"].ToString();
                            codeArray[i].strPanType = item.SubItems[1].Text;
                        }
                    }
                    else
                    {
                        codeArray = new PanBarCode[0];
                    }
                    new Print().LabelPrintTaskPan(codeArray);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("打印条形码时发生错误\n" + exception.Message, "底板任务管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnPrintWorkPlan_Click(object sender, EventArgs e)
        {
            PrintDocument document = new PrintDocument();
            document.PrintPage += new PrintPageEventHandler(this.Pd_PrintPageSend);
            try
            {
                PrintPreviewDialog dialog = new PrintPreviewDialog {
                    Document = document
                };
                PaperSize size = new PaperSize {
                    Height = 0x491,
                    Width = 0x33b
                };
                document.DefaultPageSettings.PaperSize = size;
                dialog.WindowState = FormWindowState.Maximized;
                dialog.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show("打印时发生错误" + '\n' + exception.Message, "打印生产表", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cbx_SelectAll_CheckedChanged(object sender, EventArgs e)
        {
            this.cbx_SelectAll.Cursor = Cursors.WaitCursor;
            foreach (ListViewItem item in this.Lvw_MbTask.Items)
            {
                item.Checked = this.cbx_SelectAll.Checked;
            }
            this.cbx_SelectAll.Cursor = Cursors.Default;
        }

        private void cbx_SelectBarCode_CheckedChanged(object sender, EventArgs e)
        {
            this.cbx_SelectBarCode.Cursor = Cursors.WaitCursor;
            foreach (ListViewItem item in this.lvw_BarCode.Items)
            {
                item.Checked = this.cbx_SelectBarCode.Checked;
            }
            foreach (ListViewItem item2 in this.lvw_NPTask.Items)
            {
                item2.Checked = this.cbx_SelectBarCode.Checked;
            }
            this.cbx_SelectBarCode.Cursor = Cursors.Default;
        }

        private void cbx_WProcedure_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cbx_WProcedure_SelectedIndexChanged_1(object sender, EventArgs e)
        {
        }

        private void cbx_WProcedure_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (this.cbx_WProcedure.SelectedValue.ToString() == "11")
            {
                this.lvw_BarCode.Items.Clear();
                this.gpb_BarCode.Tag = "0".ToString();
                this.tbx_Amount.Text = "";
                this.gpb_BarCode.Text = "条码信息 底板总数:0";
            }
            else
            {
                if (int.Parse(this.cbx_WProcedure.SelectedValue.ToString()) > 10)
                {
                    this.GetPanIntheProcess(int.Parse(this.cbx_WProcedure.SelectedValue.ToString()));
                }
                else
                {
                    this.GetNPIntheProcess(int.Parse(this.cbx_WProcedure.SelectedValue.ToString()));
                }
                if ((this.lvw_BarCode.Items.Count == 0) && (this.lvw_NPTask.Items.Count == 0))
                {
                    this.gpb_BarCode.Tag = "0".ToString();
                    this.tbx_Amount.Text = "";
                    this.gpb_BarCode.Text = "条码信息 总数:0";
                    if ((this.cbx_WProcedure.SelectedValue.ToString().CompareTo("3") == 0) || (this.cbx_WProcedure.SelectedValue.ToString().CompareTo("15") == 0))
                    {
                        this.gpb_BarCode.Tag = "1".ToString();
                    }
                }
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

        private void dtp_WorkDay_ValueChanged(object sender, EventArgs e)
        {
            this.Frm_MatherBoard_Load(this, e);
        }

        private DataTable FormatDT(DataSet ds)
        {
            DataTable table = ds.Tables[0];
            DataTable table2 = table.Copy();
            DataColumn column = new DataColumn {
                ColumnName = "Group"
            };
            table.Columns.Add(column);
            foreach (DataRow row in table.Rows)
            {
                row.BeginEdit();
                foreach (DataRow row2 in table2.Rows)
                {
                    if (((row["WorkPlanID"].ToString() != "") && (row["WorkPlanID"].ToString() == row2["WorkPlanID"].ToString())) && (row["Worker"].ToString() != row2["Worker"].ToString()))
                    {
                        string str = row2["Worker"].ToString();
                        if (row["Group"].ToString().IndexOf(str) < 0)
                        {
                            DataRow row3;
                            (row3 = row)["Group"] = row3["Group"] + str + " ";
                        }
                    }
                }
                row.EndEdit();
            }
            return table;
        }

        private void Frm_MatherBoard_Load(object sender, EventArgs e)
        {
            base.Size = base.Parent.ClientSize;
            DataTable dt = new DataTable();
            Sql2KDataAccess access = new Sql2KDataAccess();
            access.Run_SqlText("select * from V_MbType", ref dt);
            this.cbx_Size.DataSource = dt;
            this.cbx_Size.DisplayMember = "CodeName";
            this.cbx_Size.ValueMember = "Code";
            access.Run_SqlText("select * from V_ProcessAll", ref dt);
            this.cbx_WProcedure.DataSource = dt;
            this.cbx_WProcedure.DisplayMember = "CodeName";
            this.cbx_WProcedure.ValueMember = "Code";
            this.tbx_Amount.Text = "";
            this.mt = new MbTask();
            DataSet todayWork = WorkPlan.GetTodayWork(this.dtp_WorkDay.Value);
            this.Lvw_MbTask.Items.Clear();
            for (int i = 0; i < todayWork.Tables[0].Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem {
                    Checked = this.cbx_SelectAll.Checked,
                    ImageKey = "MBoard",
                    Text = todayWork.Tables[0].Rows[i]["workplanid"].ToString()
                };
                item.SubItems.Add(todayWork.Tables[0].Rows[i]["Worker"].ToString());
                item.SubItems.Add(todayWork.Tables[0].Rows[i]["Process"].ToString());
                item.SubItems.Add(todayWork.Tables[0].Rows[i]["WorkLoad"].ToString());
                item.SubItems.Add(todayWork.Tables[0].Rows[i]["WorkDay"].ToString());
                item.SubItems.Add(todayWork.Tables[0].Rows[i]["Remark"].ToString());
                item.SubItems.Add(todayWork.Tables[0].Rows[i]["MakePersonName"].ToString());
                item.SubItems.Add(todayWork.Tables[0].Rows[i]["MakeTime"].ToString());
                this.Lvw_MbTask.Items.Add(item);
            }
            this.listVwWorkers.Items.Clear();
            todayWork = new Sql2KDataAccess().Run_SqlText("SELECT * FROM T_Attendance  WHERE (DATEDIFF([day], ArriveTime, GETDATE()) = 0) AND (LeaveTime IS NULL)");
            if (todayWork != null)
            {
                for (int j = 0; j < todayWork.Tables[0].Rows.Count; j++)
                {
                    this.listVwWorkers.Items.Add(todayWork.Tables[0].Rows[j]["personid"].ToString());
                }
                this.labelWorker.Text = "当前" + todayWork.Tables[0].Rows.Count.ToString() + "人";
            }
        }

        private void GetNPIntheProcess(int ProcessIndex)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            DataSet set = new DataSet();
            string[] strArray = new string[0x11];
            for (int i = 0; i < 0x10; i++)
            {
                strArray[i] = " 1=2";
            }
            string str = "order by TaskTime";
            strArray[4] = " PressTime is null and withdrawtime is not null" + str;
            strArray[5] = " presstime is not null and EraseTime is null and SmearTime is null and DryingTime is null and ClashTime is  null" + str;
            strArray[6] = " presstime is not null and EraseTime is null and SmearTime is null and DryingTime is null and ClashTime is  null" + str;
            strArray[7] = " presstime is not null and EraseTime is not null and SmearTime is null and DryingTime is null and ClashTime is  null" + str;
            strArray[8] = " presstime is not null  and DryingTime is not null and ClashTime is  null and ProcessPackTime is  null " + str;
            strArray[9] = " (ProcessPackTime is  null) and (presstime is not null and ((erasetime is not null and clashtime is not null) or (smeartime is not null)) and  dryingtime  is not null)" + str;
            string str2 = "";
            str2 = " and " + strArray[ProcessIndex];
            string str3 = "select TaskId as '生产任务单号',TaskTime as '任务下达时间',TaskUser as '任务下达用户',NpNum as '车牌总数',PressTime as '压字时间',PressU as '压字人员',EraseTime as '擦字时间',EraseU as '擦字人员',DryingTime as '烘干时间',DryingU as '烘干人员',SmearTime as '涂印时间',SmearU as '涂印人员',ClashTime as '冲安装孔时间',ClashU as '冲安装孔人员',PackTime as '打包时间',PackU as '打包人员' from V_Task ";
            string queryStr = str3 + "where (Sendtime IS NULL)   " + str2;
            set = access.Run_SqlText(queryStr);
            this.lvw_BarCode.Visible = false;
            this.lvw_BarCode.Items.Clear();
            this.lvw_NPTask.Items.Clear();
            this.lvw_NPTask.Visible = true;
            this.lvw_NPTask.BringToFront();
            this.lvw_NPTask.SortColumn = 2;
            this.lvw_NPTask.Order = SortOrder.Ascending;
            long num2 = 0L;
            if (set != null)
            {
                for (int j = set.Tables[0].Rows.Count - 1; j >= 0; j--)
                {
                    ListViewItem item = new ListViewItem {
                        ImageKey = "Box",
                        Checked = this.cbx_SelectBarCode.Checked,
                        Text = set.Tables[0].Rows[j]["生产任务单号"].ToString()
                    };
                    item.SubItems.Add(set.Tables[0].Rows[j]["车牌总数"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[j]["任务下达时间"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[j]["压字人员"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[j]["压字时间"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[j]["擦字人员"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[j]["擦字时间"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[j]["烘干人员"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[j]["烘干时间"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[j]["涂印人员"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[j]["涂印时间"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[j]["冲安装孔人员"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[j]["冲安装孔时间"].ToString());
                    num2 += int.Parse(set.Tables[0].Rows[j]["车牌总数"].ToString());
                    this.lvw_NPTask.Items.Add(item);
                }
                this.gpb_BarCode.Tag = num2.ToString();
                this.tbx_Amount.Text = num2.ToString();
            }
        }

        private void GetPanIntheProcess(int ProcessIndex)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            DataSet set = new DataSet();
            string[] strArray = new string[0x10];
            string str2 = "SELECT PanTaskID AS 底板箱号, Pantype AS 底板尺寸, PanCount AS 数量, IsInWarehouse AS 是否在库中, CleanU AS 清洗人员, CleanTime AS 清洗时间,FilmU AS 贴膜人员, FilmTime AS 贴膜时间, SilkScreenU AS 丝印人员, SilkScreenTime AS 丝印时间, ClashU AS 冲孔人员, ClashTime AS 冲孔时间  FROM T_PanTaskInfo WHERE pancount>0  ";
            for (int i = 0; i < 0x10; i++)
            {
                strArray[i] = " and  1=2";
            }
            strArray[14] = " and  whiteoryellow='1' and FilmU is not null and ClashU IS NULL order by FilmTime";
            strArray[13] = " and  whiteoryellow='0' and FilmU is not null and (SilkScreenU IS NULL) order by FilmTime";
            strArray[12] = " and  whiteoryellow is null  and cleanU is not null order by cleantime";
            strArray[11] = " and isinwarehouse is null and whiteoryellow is null and cleanU is null  ";
            set = access.Run_SqlText(str2 + strArray[ProcessIndex]);
            this.lvw_NPTask.Visible = false;
            this.lvw_BarCode.Visible = true;
            this.lvw_BarCode.BringToFront();
            this.lvw_BarCode.Items.Clear();
            this.lvw_NPTask.Items.Clear();
            this.lvw_BarCode.Order = SortOrder.Ascending;
            long num2 = 0L;
            if (set != null)
            {
                for (int j = 0; j < set.Tables[0].Rows.Count; j++)
                {
                    ListViewItem item = new ListViewItem {
                        ImageKey = "Box",
                        Checked = this.cbx_SelectBarCode.Checked,
                        Text = set.Tables[0].Rows[j]["底板箱号"].ToString()
                    };
                    item.SubItems.Add(set.Tables[0].Rows[j]["数量"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[j]["清洗人员"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[j]["清洗时间"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[j]["贴膜人员"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[j]["贴膜时间"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[j]["丝印人员"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[j]["丝印时间"].ToString());
                    num2 += int.Parse(set.Tables[0].Rows[j]["数量"].ToString());
                    this.lvw_BarCode.Items.Add(item);
                }
                this.gpb_BarCode.Tag = num2.ToString();
                this.tbx_Amount.Text = num2.ToString();
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.groupBox1 = new GroupBox();
            this.listVwWorkers = new ListView();
            this.label4 = new Label();
            this.panel4 = new Panel();
            this.panel2 = new Panel();
            this.dtp_WorkDay = new DateTimePicker();
            this.btn_Ok = new Button();
            this.cbx_Size = new ComboBox();
            this.label1 = new Label();
            this.label5 = new Label();
            this.label2 = new Label();
            this.btnPrintWorkPlan = new Button();
            this.btn_PrintBarCode = new Button();
            this.cbx_WProcedure = new ComboBox();
            this.label3 = new Label();
            this.tbx_Amount = new TextBox();
            this.tbx_Remark = new TextBox();
            this.panel1 = new Panel();
            this.labelWorker = new Label();
            this.cbx_SelectAll = new CheckBox();
            this.groupBox2 = new GroupBox();
            this.ilt_Icon = new ImageList(this.components);
            this.gpb_BarCode = new GroupBox();
            this.cbx_SelectBarCode = new CheckBox();
            this.lvw_NPTask = new SortedListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader21 = new ColumnHeader();
            this.columnHeader6 = new ColumnHeader();
            this.columnHeader7 = new ColumnHeader();
            this.columnHeader8 = new ColumnHeader();
            this.columnHeader9 = new ColumnHeader();
            this.columnHeader10 = new ColumnHeader();
            this.columnHeader16 = new ColumnHeader();
            this.columnHeader17 = new ColumnHeader();
            this.columnHeader18 = new ColumnHeader();
            this.columnHeader19 = new ColumnHeader();
            this.columnHeader20 = new ColumnHeader();
            this.lvw_BarCode = new SortedListView();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.columnHeader5 = new ColumnHeader();
            this.columnHeader11 = new ColumnHeader();
            this.columnHeader12 = new ColumnHeader();
            this.columnHeader13 = new ColumnHeader();
            this.columnHeader14 = new ColumnHeader();
            this.columnHeader15 = new ColumnHeader();
            this.Lvw_MbTask = new SortedListView();
            this.WorkPlanID = new ColumnHeader();
            this.Worker = new ColumnHeader();
            this.Process = new ColumnHeader();
            this.WorkLoad = new ColumnHeader();
            this.WorkDay = new ColumnHeader();
            this.Remark = new ColumnHeader();
            this.MakePerson = new ColumnHeader();
            this.MakeTime = new ColumnHeader();
            this.groupBox1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gpb_BarCode.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.listVwWorkers);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Controls.Add(this.tbx_Remark);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.labelWorker);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x3f0, 0x79);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "下达底板的生产任务";
            this.listVwWorkers.CheckBoxes = true;
            this.listVwWorkers.Location = new Point(80, 0x11);
            this.listVwWorkers.Name = "listVwWorkers";
            this.listVwWorkers.Size = new Size(0x28b, 0x35);
            this.listVwWorkers.TabIndex = 12;
            this.listVwWorkers.UseCompatibleStateImageBehavior = false;
            this.listVwWorkers.View = View.List;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x2e1, 0x16);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x23, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "备注:";
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Dock = DockStyle.Bottom;
            this.panel4.Location = new Point(3, 0x4c);
            this.panel4.Name = "panel4";
            this.panel4.Size = new Size(0x3cc, 0x2a);
            this.panel4.TabIndex = 0x10;
            this.panel2.Controls.Add(this.dtp_WorkDay);
            this.panel2.Controls.Add(this.btn_Ok);
            this.panel2.Controls.Add(this.cbx_Size);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnPrintWorkPlan);
            this.panel2.Controls.Add(this.btn_PrintBarCode);
            this.panel2.Controls.Add(this.cbx_WProcedure);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.tbx_Amount);
            this.panel2.Location = new Point(11, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x3ae, 0x2c);
            this.panel2.TabIndex = 3;
            this.dtp_WorkDay.Location = new Point(0x51, 12);
            this.dtp_WorkDay.Name = "dtp_WorkDay";
            this.dtp_WorkDay.Size = new Size(0x6f, 0x15);
            this.dtp_WorkDay.TabIndex = 14;
            this.dtp_WorkDay.ValueChanged += new EventHandler(this.dtp_WorkDay_ValueChanged);
            this.btn_Ok.ForeColor = Color.FromArgb(0, 0xc0, 0);
            this.btn_Ok.Location = new Point(690, 13);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new Size(0x4b, 0x17);
            this.btn_Ok.TabIndex = 6;
            this.btn_Ok.Text = "确定";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new EventHandler(this.btn_Ok_Click);
            this.cbx_Size.FormattingEnabled = true;
            this.cbx_Size.Location = new Point(0x1b9, 14);
            this.cbx_Size.Name = "cbx_Size";
            this.cbx_Size.Size = new Size(0x79, 20);
            this.cbx_Size.TabIndex = 0;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x178, 0x11);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3b, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择尺寸:";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(3, 0x10);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x53, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "工作执行日期:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0xcc, 0x12);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x3b, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "选择工序:";
            this.btnPrintWorkPlan.Location = new Point(0x30f, 12);
            this.btnPrintWorkPlan.Name = "btnPrintWorkPlan";
            this.btnPrintWorkPlan.Size = new Size(0x4b, 0x17);
            this.btnPrintWorkPlan.TabIndex = 7;
            this.btnPrintWorkPlan.Text = "打印工作表";
            this.btnPrintWorkPlan.UseVisualStyleBackColor = true;
            this.btnPrintWorkPlan.Click += new EventHandler(this.btnPrintWorkPlan_Click);
            this.btn_PrintBarCode.Location = new Point(0x360, 12);
            this.btn_PrintBarCode.Name = "btn_PrintBarCode";
            this.btn_PrintBarCode.Size = new Size(0x4b, 0x17);
            this.btn_PrintBarCode.TabIndex = 7;
            this.btn_PrintBarCode.Text = "打印条形码";
            this.btn_PrintBarCode.UseVisualStyleBackColor = true;
            this.btn_PrintBarCode.Visible = false;
            this.btn_PrintBarCode.Click += new EventHandler(this.btn_PrintBarCode_Click);
            this.cbx_WProcedure.FormattingEnabled = true;
            this.cbx_WProcedure.Location = new Point(0x10c, 14);
            this.cbx_WProcedure.Name = "cbx_WProcedure";
            this.cbx_WProcedure.Size = new Size(0x5d, 20);
            this.cbx_WProcedure.TabIndex = 3;
            this.cbx_WProcedure.SelectionChangeCommitted += new EventHandler(this.cbx_WProcedure_SelectionChangeCommitted);
            this.cbx_WProcedure.SelectedIndexChanged += new EventHandler(this.cbx_WProcedure_SelectedIndexChanged_1);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x23f, 0x12);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x23, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "数量:";
            this.tbx_Amount.Font = new Font("黑体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.tbx_Amount.Location = new Point(0x266, 12);
            this.tbx_Amount.Name = "tbx_Amount";
            this.tbx_Amount.Size = new Size(70, 0x17);
            this.tbx_Amount.TabIndex = 5;
            this.tbx_Remark.Font = new Font("黑体", 10.5f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.tbx_Remark.Location = new Point(0x30a, 0x11);
            this.tbx_Remark.Multiline = true;
            this.tbx_Remark.Name = "tbx_Remark";
            this.tbx_Remark.Size = new Size(0xb2, 0x35);
            this.tbx_Remark.TabIndex = 5;
            this.panel1.Dock = DockStyle.Right;
            this.panel1.Location = new Point(0x3cf, 0x11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(30, 0x65);
            this.panel1.TabIndex = 14;
            this.labelWorker.AutoSize = true;
            this.labelWorker.Location = new Point(0x15, 0x11);
            this.labelWorker.Name = "labelWorker";
            this.labelWorker.Size = new Size(0x35, 12);
            this.labelWorker.TabIndex = 14;
            this.labelWorker.Text = "当前工人";
            this.cbx_SelectAll.AutoSize = true;
            this.cbx_SelectAll.Checked = true;
            this.cbx_SelectAll.CheckState = CheckState.Checked;
            this.cbx_SelectAll.Dock = DockStyle.Bottom;
            this.cbx_SelectAll.Location = new Point(5, 0xcf);
            this.cbx_SelectAll.Name = "cbx_SelectAll";
            this.cbx_SelectAll.Size = new Size(0x3e6, 0x10);
            this.cbx_SelectAll.TabIndex = 11;
            this.cbx_SelectAll.Text = "全选/不选";
            this.cbx_SelectAll.UseVisualStyleBackColor = true;
            this.cbx_SelectAll.Visible = false;
            this.cbx_SelectAll.CheckedChanged += new EventHandler(this.cbx_SelectAll_CheckedChanged);
            this.groupBox2.Controls.Add(this.Lvw_MbTask);
            this.groupBox2.Controls.Add(this.cbx_SelectAll);
            this.groupBox2.Dock = DockStyle.Top;
            this.groupBox2.Location = new Point(8, 0x81);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new Padding(5);
            this.groupBox2.Size = new Size(0x3f0, 0xe4);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "今天下达的任务";
            this.ilt_Icon.ColorDepth = ColorDepth.Depth8Bit;
            this.ilt_Icon.ImageSize = new Size(0x10, 0x10);
            this.ilt_Icon.TransparentColor = Color.Transparent;
            this.gpb_BarCode.Controls.Add(this.lvw_NPTask);
            this.gpb_BarCode.Controls.Add(this.cbx_SelectBarCode);
            this.gpb_BarCode.Controls.Add(this.lvw_BarCode);
            this.gpb_BarCode.Dock = DockStyle.Fill;
            this.gpb_BarCode.Location = new Point(8, 0x165);
            this.gpb_BarCode.Name = "gpb_BarCode";
            this.gpb_BarCode.Padding = new Padding(5);
            this.gpb_BarCode.Size = new Size(0x3f0, 0x9a);
            this.gpb_BarCode.TabIndex = 2;
            this.gpb_BarCode.TabStop = false;
            this.gpb_BarCode.Text = "条码信息";
            this.cbx_SelectBarCode.AutoSize = true;
            this.cbx_SelectBarCode.Dock = DockStyle.Bottom;
            this.cbx_SelectBarCode.Location = new Point(5, 0x85);
            this.cbx_SelectBarCode.Name = "cbx_SelectBarCode";
            this.cbx_SelectBarCode.Size = new Size(0x3e6, 0x10);
            this.cbx_SelectBarCode.TabIndex = 5;
            this.cbx_SelectBarCode.Text = "全选/不选";
            this.cbx_SelectBarCode.UseVisualStyleBackColor = true;
            this.cbx_SelectBarCode.CheckedChanged += new EventHandler(this.cbx_SelectBarCode_CheckedChanged);
            this.lvw_NPTask.CheckBoxes = true;
            this.lvw_NPTask.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader3, this.columnHeader21, this.columnHeader6, this.columnHeader7, this.columnHeader8, this.columnHeader9, this.columnHeader10, this.columnHeader16, this.columnHeader17, this.columnHeader18, this.columnHeader19, this.columnHeader20 });
            this.lvw_NPTask.Dock = DockStyle.Fill;
            this.lvw_NPTask.FullRowSelect = true;
            this.lvw_NPTask.Location = new Point(5, 0x13);
            this.lvw_NPTask.MultiSelect = false;
            this.lvw_NPTask.Name = "lvw_NPTask";
            this.lvw_NPTask.Order = SortOrder.Descending;
            this.lvw_NPTask.Size = new Size(0x3e6, 0x72);
            this.lvw_NPTask.SortColumn = 0;
            this.lvw_NPTask.Sorting = SortOrder.Descending;
            this.lvw_NPTask.TabIndex = 7;
            this.lvw_NPTask.Tag = "0";
            this.lvw_NPTask.UseCompatibleStateImageBehavior = false;
            this.lvw_NPTask.View = View.Details;
            this.lvw_NPTask.ItemChecked += new ItemCheckedEventHandler(this.lvw_BarCode_ItemChecked);
            this.columnHeader1.Text = "条码ID";
            this.columnHeader1.Width = 0x87;
            this.columnHeader3.Text = "数量";
            this.columnHeader3.Width = 0x87;
            this.columnHeader21.Text = "下达时间";
            this.columnHeader21.Width = 0x87;
            this.columnHeader6.Text = "压字人员";
            this.columnHeader6.Width = 130;
            this.columnHeader7.Text = "压字时间";
            this.columnHeader7.Width = 0x87;
            this.columnHeader8.Text = "擦字人员";
            this.columnHeader8.Width = 0x87;
            this.columnHeader9.Text = "擦字时间";
            this.columnHeader9.Width = 0x87;
            this.columnHeader10.Text = "烘干人员";
            this.columnHeader10.Width = 0x87;
            this.columnHeader16.Text = "烘干时间";
            this.columnHeader16.Width = 0x87;
            this.columnHeader17.Text = "涂印人员";
            this.columnHeader18.Text = "涂印时间";
            this.columnHeader19.Text = "冲孔人员";
            this.columnHeader20.Text = "冲孔时间";
            this.lvw_BarCode.CheckBoxes = true;
            this.lvw_BarCode.Columns.AddRange(new ColumnHeader[] { this.columnHeader2, this.columnHeader4, this.columnHeader5, this.columnHeader11, this.columnHeader12, this.columnHeader13, this.columnHeader14, this.columnHeader15 });
            this.lvw_BarCode.Dock = DockStyle.Fill;
            this.lvw_BarCode.FullRowSelect = true;
            this.lvw_BarCode.Location = new Point(5, 0x13);
            this.lvw_BarCode.MultiSelect = false;
            this.lvw_BarCode.Name = "lvw_BarCode";
            this.lvw_BarCode.Order = SortOrder.Descending;
            this.lvw_BarCode.Size = new Size(0x3e6, 130);
            this.lvw_BarCode.SmallImageList = this.ilt_Icon;
            this.lvw_BarCode.SortColumn = 0;
            this.lvw_BarCode.TabIndex = 7;
            this.lvw_BarCode.Tag = "0";
            this.lvw_BarCode.UseCompatibleStateImageBehavior = false;
            this.lvw_BarCode.View = View.Details;
            this.lvw_BarCode.ItemChecked += new ItemCheckedEventHandler(this.lvw_BarCode_ItemChecked);
            this.columnHeader2.Text = "条码ID";
            this.columnHeader2.Width = 0x87;
            this.columnHeader4.Text = "数量";
            this.columnHeader4.Width = 0x87;
            this.columnHeader5.Text = "清洗烘干人员";
            this.columnHeader5.Width = 130;
            this.columnHeader11.Text = "清洗烘干时间";
            this.columnHeader11.Width = 0x87;
            this.columnHeader12.Text = "贴膜人员";
            this.columnHeader12.Width = 0x87;
            this.columnHeader13.Text = "贴膜时间";
            this.columnHeader13.Width = 0x87;
            this.columnHeader14.Text = "丝印人员";
            this.columnHeader14.Width = 0x87;
            this.columnHeader15.Text = "丝印时间";
            this.columnHeader15.Width = 0x87;
            this.Lvw_MbTask.Columns.AddRange(new ColumnHeader[] { this.WorkPlanID, this.Worker, this.Process, this.WorkLoad, this.WorkDay, this.Remark, this.MakePerson, this.MakeTime });
            this.Lvw_MbTask.Dock = DockStyle.Fill;
            this.Lvw_MbTask.FullRowSelect = true;
            this.Lvw_MbTask.Location = new Point(5, 0x13);
            this.Lvw_MbTask.MultiSelect = false;
            this.Lvw_MbTask.Name = "Lvw_MbTask";
            this.Lvw_MbTask.Order = SortOrder.Descending;
            this.Lvw_MbTask.Size = new Size(0x3e6, 0xbc);
            this.Lvw_MbTask.SmallImageList = this.ilt_Icon;
            this.Lvw_MbTask.SortColumn = 0;
            this.Lvw_MbTask.TabIndex = 3;
            this.Lvw_MbTask.UseCompatibleStateImageBehavior = false;
            this.Lvw_MbTask.View = View.Details;
            this.WorkPlanID.Text = "任务ID";
            this.WorkPlanID.Width = 0x7e;
            this.Worker.Text = "工人";
            this.Worker.Width = 100;
            this.Process.Text = "工序";
            this.Process.Width = 100;
            this.WorkLoad.Text = "数量";
            this.WorkLoad.Width = 100;
            this.WorkDay.Text = "工作日期";
            this.WorkDay.Width = 100;
            this.Remark.Text = "备注";
            this.Remark.Width = 120;
            this.MakePerson.Text = "下达人";
            this.MakePerson.Width = 100;
            this.MakeTime.Text = "下达时间";
            this.MakeTime.Width = 0x87;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x400, 0x207);
            base.ControlBox = false;
            base.Controls.Add(this.gpb_BarCode);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "Frm_MatherBoard";
            base.Padding = new Padding(8);
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "Frm_MatherBoard";
            base.Load += new EventHandler(this.Frm_MatherBoard_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gpb_BarCode.ResumeLayout(false);
            this.gpb_BarCode.PerformLayout();
            base.ResumeLayout(false);
        }

        private void lvw_BarCode_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            long num = 0L;
            ListView view = (ListView) sender;
            foreach (ListViewItem item in view.CheckedItems)
            {
                num += long.Parse(item.SubItems[1].Text.ToString());
            }
            this.gpb_BarCode.Text = "总数:" + num.ToString();
        }

        private void Pd_PrintPageSend(object sender, PrintPageEventArgs e)
        {
            DataSet todayWork = WorkPlan.GetTodayWork(this.dtp_WorkDay.Value);
            if (todayWork != null)
            {
                DataTable table = this.FormatDT(todayWork);
                new Font("隶书", 7.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
                string s = "机动车号牌生产工作安排表";
                string str2 = "瑞华交通工程有限公司";
                string str3 = "执行日期  " + this.dtp_WorkDay.Value.ToShortDateString();
                Font font = new Font("黑体", 20f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
                Font font2 = new Font("黑体", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
                Font font3 = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
                Font font4 = new Font("宋体", 12f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
                new Font("宋体", 10f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
                e.Graphics.DrawString(s, font, Brushes.Black, new PointF(250f, 100f));
                e.Graphics.DrawString(str2, font2, Brushes.Black, new PointF(330f, 70f));
                e.Graphics.DrawString(str3, font2, Brushes.Black, new PointF(330f, 140f));
                Pen pen = new Pen(Brushes.Black) {
                    Width = 1.2f
                };
                int num = 0x19;
                int num2 = 160;
                e.Graphics.DrawLine(pen, 50, num2, 770, num2);
                e.Graphics.DrawString("序号", font4, Brushes.Black, new PointF(55f, (float) (num2 + 3)));
                e.Graphics.DrawString("姓名", font4, Brushes.Black, new PointF(100f, (float) (num2 + 3)));
                e.Graphics.DrawString("工作", font4, Brushes.Black, new PointF(200f, (float) (num2 + 3)));
                e.Graphics.DrawString("工作量", font4, Brushes.Black, new PointF(300f, (float) (num2 + 3)));
                e.Graphics.DrawString("同组人", font4, Brushes.Black, new PointF(400f, (float) (num2 + 3)));
                e.Graphics.DrawString("备注", font4, Brushes.Black, new PointF(600f, (float) (num2 + 3)));
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    num2 = 190 + (i * num);
                    e.Graphics.DrawLine(pen, 50, num2, 770, num2);
                    e.Graphics.DrawString(Convert.ToString((int) (i + 1)), font3, Brushes.Black, new PointF(60f, (float) (num2 + 3)));
                    if (table.Rows[i]["Worker"].ToString() != "")
                    {
                        e.Graphics.DrawString(table.Rows[i]["Worker"].ToString(), font3, Brushes.Black, new PointF(100f, (float) (num2 + 3)));
                    }
                    if (table.Rows[i]["Process"].ToString() != "")
                    {
                        e.Graphics.DrawString(table.Rows[i]["Process"].ToString(), font3, Brushes.Black, new PointF(200f, (float) (num2 + 3)));
                    }
                    if (table.Rows[i]["WorkLoad"].ToString() != "")
                    {
                        e.Graphics.DrawString(table.Rows[i]["WorkLoad"].ToString(), font3, Brushes.Black, new PointF(300f, (float) (num2 + 3)));
                    }
                    if (table.Rows[i]["Group"].ToString() != "")
                    {
                        e.Graphics.DrawString(table.Rows[i]["Group"].ToString(), font3, Brushes.Black, new PointF(400f, (float) (num2 + 3)));
                    }
                    if (table.Rows[i]["Worker"].ToString() != "")
                    {
                        e.Graphics.DrawString(table.Rows[i]["Remark"].ToString(), font3, Brushes.Black, new PointF(600f, (float) (num2 + 3)));
                    }
                }
                num2 += num;
                e.Graphics.DrawLine(pen, 50, num2, 770, num2);
                e.Graphics.DrawString("制表人：系统管理员  制表时间：" + DateTime.Now.ToString(), font3, Brushes.Black, new PointF(55f, (float) (num2 + 20)));
                pen.Dispose();
            }
        }

        private void SaveWorkPlanToDB()
        {
            DataSet workPlanSet = new DataSet();
            DataTable table = new DataTable("WorkPlan");
            table = workPlanSet.Tables.Add();
            DataColumn column = new DataColumn("WorkPlanID", typeof(string));
            table.Columns.Add(column);
            DataColumn column2 = new DataColumn("UserName", typeof(string));
            table.Columns.Add(column2);
            DataColumn column3 = new DataColumn("ProcessID", typeof(string));
            table.Columns.Add(column3);
            DataColumn column4 = new DataColumn("WorkLoad", typeof(int));
            table.Columns.Add(column4);
            DataColumn column5 = new DataColumn("WorkDay", typeof(string));
            table.Columns.Add(column5);
            DataColumn column6 = new DataColumn("MakePeron", typeof(string));
            table.Columns.Add(column6);
            DataColumn column7 = new DataColumn("Remark", typeof(string));
            table.Columns.Add(column7);
            string str = WorkPlan.CreateWorkPlanID();
            for (int i = 0; i < this.listVwWorkers.Items.Count; i++)
            {
                if (this.listVwWorkers.Items[i].Checked)
                {
                    DataRow row = workPlanSet.Tables[0].Rows.Add(new object[0]);
                    row["WorkPlanID"] = str;
                    row["UserName"] = this.listVwWorkers.Items[i].Text;
                    row["ProcessID"] = this.cbx_WProcedure.SelectedValue;
                    row["WorkLoad"] = this.tbx_Amount.Text.Trim();
                    row["WorkDay"] = this.dtp_WorkDay.Value.ToShortDateString();
                    row["MakePeron"] = User.UserName;
                    row["Remark"] = this.tbx_Remark.Text.Trim();
                }
            }
            WorkPlan.InsertWorkPlan(workPlanSet);
            workPlanSet.Tables.Clear();
            table.Clear();
            table = workPlanSet.Tables.Add();
            DataColumn column8 = new DataColumn("WorkPlanID", typeof(string));
            table.Columns.Add(column8);
            DataColumn column9 = new DataColumn("TaskID", typeof(string));
            table.Columns.Add(column9);
            for (int j = 0; j < this.lvw_BarCode.Items.Count; j++)
            {
                if (this.lvw_BarCode.Items[j].Checked)
                {
                    DataRow row2 = workPlanSet.Tables[0].Rows.Add(new object[0]);
                    row2["WorkPlanID"] = str;
                    row2["TaskID"] = this.lvw_BarCode.Items[j].Text;
                }
            }
            WorkPlan.InsertFixedBox(workPlanSet);
        }

        public static Frm_MatherBoard Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Frm_MatherBoard();
                }
                return m_Instance;
            }
        }
    }
}

