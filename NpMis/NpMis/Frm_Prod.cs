namespace NpMis
{
    using BarCodePrint;
    using Common;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class Frm_Prod : Form
    {
        private Button btn_AddTask;
        private Button btn_Print;
        private Button btn_Refresh;
        private Button btn_Task;
        private Button btnSinglePrint;
        private CheckBox cbx_BatchTask;
        private CheckBox cbx_PrintCodeBar;
        private CheckBox cbx_PrintView;
        private CheckBox cbx_Select;
        private CheckBox cbx_SelectAll;
        private CheckedListBox clb_NpType;
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
        private ColumnHeader columnHeader22;
        private ColumnHeader columnHeader23;
        private ColumnHeader columnHeader24;
        private ColumnHeader columnHeader25;
        private ColumnHeader columnHeader26;
        private ColumnHeader columnHeader27;
        private ColumnHeader columnHeader28;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private IContainer components;
        private DataSet ds = new DataSet();
        private string G_processType = "";
        private GroupBox gbx_NpNum;
        private GroupBox gbx_TaskNp;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox6;
        private ImageList ilt_Icon;
        private ListView listVwWorkers;
        private SortedListView Lvw_License;
        private SortedListView Lvw_Plan;
        private SortedListView Lvw_Task;
        private SortedListView Lvw_TaskNp;
        private static Frm_Prod m_Instance;
        private Sql2KDataAccess MyDB = new Sql2KDataAccess();
        private Panel panel1;
        private Panel panel2;
        private SplitContainer splitContainer1;
        private ColumnHeader WorkerName;

        private Frm_Prod()
        {
            this.InitializeComponent();
        }

        private void btn_AddTask_Click(object sender, EventArgs e)
        {
            if (this.Lvw_License.CheckedItems.Count == 0)
            {
                MessageBox.Show("请选择要添加到任务的车牌", "添加到任务单", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (this.Lvw_TaskNp.Items.Count >= 50)
            {
                MessageBox.Show("一个任务单制作的车牌数不能超过50个", "添加到任务单", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                foreach (ListViewItem item in this.Lvw_License.Items)
                {
                    if (this.Lvw_TaskNp.Items.Count >= 50)
                    {
                        return;
                    }
                    if (item.Checked)
                    {
                        ListViewItem item2 = (ListViewItem) item.Clone();
                        this.Lvw_TaskNp.Items.Add(item2);
                        item.Remove();
                    }
                    this.gbx_TaskNp.Text = "任务单包含的车牌信息  数量:" + this.Lvw_TaskNp.Items.Count;
                }
                this.gbx_NpNum.Text = string.Concat(new object[] { "车牌明细  数量:", this.Lvw_License.Items.Count, "  已选择数:", this.Lvw_License.CheckedItems.Count });
                if (this.Lvw_License.CheckedItems.Count == 0)
                {
                    this.G_processType = "";
                    CheckedListBox.CheckedIndexCollection checkedIndices = this.clb_NpType.CheckedIndices;
                    int num = 0;
                    foreach (int num2 in checkedIndices)
                    {
                        this.clb_NpType.Items.RemoveAt(num2 - num);
                        num++;
                    }
                }
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            if (this.Lvw_Task.CheckedItems.Count == 0)
            {
                MessageBox.Show("请选择要打印的生产任务单", "生产管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                try
                {
                    foreach (ListViewItem item in this.Lvw_Task.CheckedItems)
                    {
                        DataSet npByTaskId = new Task().GetNpByTaskId(item.Text.Trim());
                        TaskList list = new TaskList {
                            TaskId = item.Text.Trim(),
                            TaskUser = item.SubItems[2].Text.Trim(),
                            TaskTime = item.SubItems[1].Text.Trim(),
                            PrintArray = new string[npByTaskId.Tables[0].Rows.Count, 4]
                        };
                        NPBarCode[] nPNum = new NPBarCode[npByTaskId.Tables[0].Rows.Count];
                        for (int i = 0; i < npByTaskId.Tables[0].Rows.Count; i++)
                        {
                            list.PrintArray[i, 0] = (i + 1).ToString();
                            list.PrintArray[i, 1] = npByTaskId.Tables[0].Rows[i]["Description"].ToString().Trim();
                            list.PrintArray[i, 2] = npByTaskId.Tables[0].Rows[i]["NpNo"].ToString().Trim();
                            list.PrintArray[i, 3] = ((DateTime) npByTaskId.Tables[0].Rows[i]["DeadLine"]).ToShortDateString();
                            nPNum[i].strNPNum = npByTaskId.Tables[0].Rows[i]["NpNo"].ToString().Trim();
                            nPNum[i].strNPTypeCode = npByTaskId.Tables[0].Rows[i]["Code"].ToString().Trim();
                            nPNum[i].strNPTypeDescription = npByTaskId.Tables[0].Rows[i]["Description"].ToString().Trim();
                            nPNum[i].strNPTypeName = npByTaskId.Tables[0].Rows[i]["CodeName"].ToString().Trim();
                            nPNum[i].bFrontPiece = npByTaskId.Tables[0].Rows[i]["IsFront"].ToString().Trim() == "√";
                            nPNum[i].bBackPiece = npByTaskId.Tables[0].Rows[i]["IsBack"].ToString().Trim() == "√";
                        }
                        if (this.cbx_PrintView.Checked)
                        {
                            list.ShowTaskList();
                        }
                        else
                        {
                            list.PrintTaskList();
                        }
                        if (this.cbx_PrintCodeBar.Checked)
                        {
                            new Print().LabelPrintTaskNP(item.Text.Trim(), nPNum);
                        }
                        item.Checked = false;
                        MessageBox.Show("生产任务单:" + item.Text.Trim() + " 打印完毕", "打印条码", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show("打印时发生错误" + '\n' + exception.Message, "打印条码", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            DataSet toDoPlan = new Plan().GetToDoPlan();
            this.Lvw_Plan.Items.Clear();
            this.Lvw_TaskNp.Items.Clear();
            for (int i = 0; i < toDoPlan.Tables[0].Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem {
                    Checked = this.cbx_SelectAll.Checked,
                    ImageKey = "Plan"
                };
                item.SubItems.Add(toDoPlan.Tables[0].Rows[i]["PlanId"].ToString());
                item.SubItems.Add(toDoPlan.Tables[0].Rows[i]["PlanKind"].ToString());
                item.SubItems.Add(toDoPlan.Tables[0].Rows[i]["PlanDepart"].ToString());
                DateTime time = (DateTime) toDoPlan.Tables[0].Rows[i]["Deadline"];
                item.SubItems.Add(time.ToShortDateString());
                item.SubItems.Add(toDoPlan.Tables[0].Rows[i]["MakedNum"].ToString());
                item.SubItems.Add(toDoPlan.Tables[0].Rows[i]["TotalCount"].ToString());
                item.SubItems.Add(toDoPlan.Tables[0].Rows[i]["InputUser"].ToString());
                item.SubItems.Add(toDoPlan.Tables[0].Rows[i]["InPutTime"].ToString());
                item.SubItems.Add(toDoPlan.Tables[0].Rows[i]["AuditingTime"].ToString());
                DateTime time2 = (DateTime) toDoPlan.Tables[0].Rows[i]["PlanTime"];
                item.SubItems.Add(time2.ToShortDateString());
                this.Lvw_Plan.Items.Add(item);
            }
            toDoPlan = new Task().GetNewTask();
            this.Lvw_Task.Items.Clear();
            for (int j = 0; j < toDoPlan.Tables[0].Rows.Count; j++)
            {
                ListViewItem item2 = new ListViewItem {
                    ImageKey = "Task",
                    Text = toDoPlan.Tables[0].Rows[j]["TaskId"].ToString().Trim()
                };
                item2.SubItems.Add(toDoPlan.Tables[0].Rows[j]["TaskTime"].ToString());
                item2.SubItems.Add(toDoPlan.Tables[0].Rows[j]["TaskUser"].ToString());
                this.Lvw_Task.Items.Add(item2);
            }
            this.Lvw_TaskNp.Items.Clear();
            this.gbx_TaskNp.Text = "任务单包含的车牌信息";
            this.listVwWorkers.Items.Clear();
            DataSet set2 = new Sql2KDataAccess().Run_SqlText("SELECT * FROM T_Attendance  WHERE (DATEDIFF([day], ArriveTime, GETDATE()) = 0) AND (LeaveTime IS NULL)");
            if (set2 != null)
            {
                for (int k = 0; k < set2.Tables[0].Rows.Count; k++)
                {
                    this.listVwWorkers.Items.Add(set2.Tables[0].Rows[k]["personid"].ToString());
                }
                this.listVwWorkers.Columns[0].Text = "当前" + set2.Tables[0].Rows.Count.ToString() + "人";
            }
        }

        private void btn_Task_Click(object sender, EventArgs e)
        {
            Task task = new Task();
            if (this.cbx_BatchTask.Checked)
            {
                if (this.Lvw_Plan.Items.Count == 0)
                {
                    MessageBox.Show("请选择生产任务单", "任务单", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (MessageBox.Show(this, "是否批量下达？", "任务单下达", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
                {
                    return;
                }
                foreach (ListViewItem item in this.Lvw_Plan.CheckedItems)
                {
                    task.PlanToTask(item.SubItems[1].Text);
                }
                this.Lvw_TaskNp.Items.Clear();
            }
            else
            {
                if (this.Lvw_TaskNp.Items.Count == 0)
                {
                    MessageBox.Show("请添加指令单要包含的车牌信息", "任务单", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                string taskId = task.NewTask();
                foreach (ListViewItem item2 in this.Lvw_TaskNp.Items)
                {
                    if (task.NpToTask(taskId, item2.Tag.ToString()))
                    {
                        item2.Remove();
                    }
                }
                this.gbx_TaskNp.Text = "任务单包含的车牌信息  数量:" + this.Lvw_TaskNp.Items.Count;
            }
            DataSet newTask = task.GetNewTask();
            DialogResult result = MessageBox.Show("任务单下达成功,是否打印条形码?", "任务单", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            for (int i = 0; i < newTask.Tables[0].Rows.Count; i++)
            {
                bool flag = false;
                foreach (ListViewItem item3 in this.Lvw_Task.Items)
                {
                    if (item3.Text.Trim() == newTask.Tables[0].Rows[i]["TaskId"].ToString().Trim())
                    {
                        flag = true;
                    }
                }
                if (!flag)
                {
                    ListViewItem item4 = new ListViewItem {
                        ImageKey = "Task",
                        Text = newTask.Tables[0].Rows[i]["TaskId"].ToString().Trim()
                    };
                    item4.SubItems.Add(newTask.Tables[0].Rows[i]["TaskTime"].ToString());
                    item4.SubItems.Add(newTask.Tables[0].Rows[i]["TaskUser"].ToString());
                    item4.Checked = true;
                    this.Lvw_Task.Items.Insert(0, item4);
                    if (result == DialogResult.Yes)
                    {
                        this.btn_Print_Click(this, new EventArgs());
                    }
                }
            }
        }

        private void btnSinglePrint_Click(object sender, EventArgs e)
        {
            new FrmSingleBarcode().ShowDialog();
        }

        private void cbx_BatchTask_CheckStateChanged(object sender, EventArgs e)
        {
            this.cbx_SelectAll.Checked = this.cbx_BatchTask.Checked;
            this.btn_AddTask.Enabled = !this.cbx_BatchTask.Checked;
        }

        private void cbx_PrintCodeBar_CheckStateChanged(object sender, EventArgs e)
        {
        }

        private void cbx_Select_CheckStateChanged(object sender, EventArgs e)
        {
            this.cbx_Select.Cursor = Cursors.WaitCursor;
            foreach (ListViewItem item in this.Lvw_License.Items)
            {
                item.Checked = this.cbx_Select.Checked;
            }
            this.gbx_NpNum.Text = string.Concat(new object[] { "车牌明细  数量:", this.Lvw_License.Items.Count, "  已选择数:", this.Lvw_License.CheckedItems.Count });
            this.cbx_Select.Cursor = Cursors.Default;
        }

        private void cbx_SelectAll_CheckedChanged(object sender, EventArgs e)
        {
            this.cbx_SelectAll.Cursor = Cursors.WaitCursor;
            foreach (ListViewItem item in this.Lvw_Plan.Items)
            {
                item.Checked = this.cbx_SelectAll.Checked;
            }
            this.cbx_SelectAll.Cursor = Cursors.Default;
        }

        private void clb_NpType_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.clb_NpType.Cursor = Cursors.WaitCursor;
            if (e.NewValue == CheckState.Checked)
            {
                if (this.G_processType.Length == 0)
                {
                    this.G_processType = this.GetProcessTypeNP(this.clb_NpType.Items[e.Index].ToString());
                    foreach (ListViewItem item in this.Lvw_License.Items)
                    {
                        if (item.SubItems[1].Text.ToString().Trim() == this.clb_NpType.Items[e.Index].ToString())
                        {
                            item.Checked = e.NewValue == CheckState.Checked;
                        }
                    }
                    this.gbx_NpNum.Text = string.Concat(new object[] { "车牌明细  数量:", this.Lvw_License.Items.Count, "  已选择数:", this.Lvw_License.CheckedItems.Count });
                }
                else if (this.G_processType.CompareTo(this.GetProcessTypeNP(this.clb_NpType.Items[e.Index].ToString())) == 0)
                {
                    foreach (ListViewItem item2 in this.Lvw_License.Items)
                    {
                        if (item2.SubItems[1].Text.ToString().Trim() == this.clb_NpType.Items[e.Index].ToString())
                        {
                            item2.Checked = e.NewValue == CheckState.Checked;
                        }
                    }
                    this.gbx_NpNum.Text = string.Concat(new object[] { "车牌明细  数量:", this.Lvw_License.Items.Count, "  已选择数:", this.Lvw_License.CheckedItems.Count });
                }
                else
                {
                    e.NewValue = CheckState.Unchecked;
                }
            }
            else
            {
                foreach (ListViewItem item3 in this.Lvw_License.Items)
                {
                    if (item3.SubItems[1].Text.ToString().Trim() == this.clb_NpType.Items[e.Index].ToString())
                    {
                        item3.Checked = e.NewValue == CheckState.Checked;
                    }
                }
                this.gbx_NpNum.Text = string.Concat(new object[] { "车牌明细  数量:", this.Lvw_License.Items.Count, "  已选择数:", this.Lvw_License.CheckedItems.Count });
                if (this.Lvw_License.CheckedItems.Count == 0)
                {
                    this.G_processType = "";
                }
            }
            this.clb_NpType.Cursor = Cursors.Default;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Frm_Prod_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_Instance = null;
        }

        private void Frm_Prod_Load(object sender, EventArgs e)
        {
            base.Size = base.Parent.ClientSize;
            new Sql2KDataAccess().Run_SqlText("select Code,Description from V_NpType");
        }

        private string GetProcessTypeNP(string NPType)
        {
            string str = "0";
            string queryStr = "SELECT TOP 1 * FROM D_NP2Pan INNER JOIN V_NpType ON D_NP2Pan.NpType = V_NpType.Code where V_NpType.Description ='" + NPType + "'";
            this.ds = this.MyDB.Run_SqlText(queryStr);
            if ((this.ds != null) && (this.ds.Tables[0].Rows.Count > 0))
            {
                string str3 = this.ds.Tables[0].Rows[0]["ProcessType"].ToString();
                if (str3 == null)
                {
                    return str;
                }
                if (!(str3 == "1"))
                {
                    if ((str3 != "2") && (str3 != "3"))
                    {
                        return str;
                    }
                }
                else
                {
                    return "1";
                }
                return "2";
            }
            return "0";
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Frm_Prod));
            this.splitContainer1 = new SplitContainer();
            this.groupBox1 = new GroupBox();
            this.Lvw_Plan = new SortedListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader18 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader20 = new ColumnHeader();
            this.columnHeader24 = new ColumnHeader();
            this.columnHeader17 = new ColumnHeader();
            this.columnHeader5 = new ColumnHeader();
            this.columnHeader16 = new ColumnHeader();
            this.columnHeader19 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.ilt_Icon = new ImageList(this.components);
            this.panel2 = new Panel();
            this.cbx_SelectAll = new CheckBox();
            this.gbx_NpNum = new GroupBox();
            this.Lvw_License = new SortedListView();
            this.columnHeader11 = new ColumnHeader();
            this.columnHeader12 = new ColumnHeader();
            this.columnHeader13 = new ColumnHeader();
            this.columnHeader14 = new ColumnHeader();
            this.columnHeader15 = new ColumnHeader();
            this.columnHeader25 = new ColumnHeader();
            this.columnHeader26 = new ColumnHeader();
            this.panel1 = new Panel();
            this.clb_NpType = new CheckedListBox();
            this.cbx_Select = new CheckBox();
            this.gbx_TaskNp = new GroupBox();
            this.Lvw_TaskNp = new SortedListView();
            this.columnHeader6 = new ColumnHeader();
            this.columnHeader7 = new ColumnHeader();
            this.columnHeader8 = new ColumnHeader();
            this.columnHeader9 = new ColumnHeader();
            this.columnHeader10 = new ColumnHeader();
            this.columnHeader27 = new ColumnHeader();
            this.columnHeader28 = new ColumnHeader();
            this.groupBox6 = new GroupBox();
            this.Lvw_Task = new SortedListView();
            this.columnHeader21 = new ColumnHeader();
            this.columnHeader22 = new ColumnHeader();
            this.columnHeader23 = new ColumnHeader();
            this.groupBox2 = new GroupBox();
            this.listVwWorkers = new ListView();
            this.WorkerName = new ColumnHeader();
            this.btnSinglePrint = new Button();
            this.cbx_PrintCodeBar = new CheckBox();
            this.cbx_PrintView = new CheckBox();
            this.cbx_BatchTask = new CheckBox();
            this.btn_Print = new Button();
            this.btn_AddTask = new Button();
            this.btn_Task = new Button();
            this.btn_Refresh = new Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gbx_NpNum.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbx_TaskNp.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.gbx_NpNum);
            this.splitContainer1.Panel1.Controls.Add(this.gbx_TaskNp);
            this.splitContainer1.Panel1.Padding = new Padding(8);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox6);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Padding = new Padding(8);
            this.splitContainer1.Size = new Size(0x341, 0x169);
            this.splitContainer1.SplitterDistance = 0x253;
            this.splitContainer1.TabIndex = 0;
            this.groupBox1.Controls.Add(this.Lvw_Plan);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Dock = DockStyle.Fill;
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(8);
            this.groupBox1.Size = new Size(0x243, 0x19);
            this.groupBox1.TabIndex = 0x5b;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "计划通知单";
            this.Lvw_Plan.CheckBoxes = true;
            this.Lvw_Plan.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader18, this.columnHeader3, this.columnHeader20, this.columnHeader24, this.columnHeader17, this.columnHeader5, this.columnHeader16, this.columnHeader19, this.columnHeader4 });
            this.Lvw_Plan.Dock = DockStyle.Fill;
            this.Lvw_Plan.FullRowSelect = true;
            this.Lvw_Plan.Location = new Point(8, 0x16);
            this.Lvw_Plan.Name = "Lvw_Plan";
            this.Lvw_Plan.Order = SortOrder.Descending;
            this.Lvw_Plan.Size = new Size(0x233, 0);
            this.Lvw_Plan.SmallImageList = this.ilt_Icon;
            this.Lvw_Plan.SortColumn = 0;
            this.Lvw_Plan.TabIndex = 13;
            this.Lvw_Plan.UseCompatibleStateImageBehavior = false;
            this.Lvw_Plan.View = View.Details;
            this.Lvw_Plan.ItemChecked += new ItemCheckedEventHandler(this.Lvw_Plan_ItemChecked);
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 0x26;
            this.columnHeader2.Text = "计划单号";
            this.columnHeader2.Width = 150;
            this.columnHeader18.Text = "计划单类型";
            this.columnHeader18.Width = 0x60;
            this.columnHeader3.Text = "计划部门";
            this.columnHeader3.Width = 110;
            this.columnHeader20.Text = "完成的最后期限";
            this.columnHeader20.Width = 120;
            this.columnHeader24.Text = "已制作数";
            this.columnHeader24.Width = 100;
            this.columnHeader17.Text = "总数";
            this.columnHeader17.Width = 80;
            this.columnHeader5.Text = "录入人员";
            this.columnHeader5.Width = 80;
            this.columnHeader16.Text = "录入时间";
            this.columnHeader16.Width = 130;
            this.columnHeader19.Text = "审批时间";
            this.columnHeader19.Width = 130;
            this.columnHeader4.Text = "下达时间";
            this.columnHeader4.Width = 130;
            this.ilt_Icon.ImageStream = (ImageListStreamer) manager.GetObject("ilt_Icon.ImageStream");
            this.ilt_Icon.TransparentColor = Color.Transparent;
            this.ilt_Icon.Images.SetKeyName(0, "Ok");
            this.ilt_Icon.Images.SetKeyName(1, "NotPass");
            this.ilt_Icon.Images.SetKeyName(2, "NP");
            this.ilt_Icon.Images.SetKeyName(3, "Plan");
            this.ilt_Icon.Images.SetKeyName(4, "Send");
            this.ilt_Icon.Images.SetKeyName(5, "Task");
            this.panel2.Controls.Add(this.cbx_SelectAll);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new Point(8, 2);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new Padding(2, 2, 6, 2);
            this.panel2.Size = new Size(0x233, 20);
            this.panel2.TabIndex = 12;
            this.cbx_SelectAll.AutoSize = true;
            this.cbx_SelectAll.Checked = true;
            this.cbx_SelectAll.CheckState = CheckState.Checked;
            this.cbx_SelectAll.Dock = DockStyle.Right;
            this.cbx_SelectAll.Location = new Point(0x1df, 2);
            this.cbx_SelectAll.Name = "cbx_SelectAll";
            this.cbx_SelectAll.Size = new Size(0x4e, 0x10);
            this.cbx_SelectAll.TabIndex = 10;
            this.cbx_SelectAll.Text = "全选/不选";
            this.cbx_SelectAll.UseVisualStyleBackColor = true;
            this.cbx_SelectAll.CheckedChanged += new EventHandler(this.cbx_SelectAll_CheckedChanged);
            this.gbx_NpNum.Controls.Add(this.Lvw_License);
            this.gbx_NpNum.Controls.Add(this.panel1);
            this.gbx_NpNum.Dock = DockStyle.Bottom;
            this.gbx_NpNum.Location = new Point(8, 0x21);
            this.gbx_NpNum.Name = "gbx_NpNum";
            this.gbx_NpNum.Padding = new Padding(8);
            this.gbx_NpNum.Size = new Size(0x243, 0x9b);
            this.gbx_NpNum.TabIndex = 3;
            this.gbx_NpNum.TabStop = false;
            this.gbx_NpNum.Text = "车牌明细";
            this.Lvw_License.CheckBoxes = true;
            this.Lvw_License.Columns.AddRange(new ColumnHeader[] { this.columnHeader11, this.columnHeader12, this.columnHeader13, this.columnHeader14, this.columnHeader15, this.columnHeader25, this.columnHeader26 });
            this.Lvw_License.Dock = DockStyle.Fill;
            this.Lvw_License.FullRowSelect = true;
            this.Lvw_License.Location = new Point(8, 0x16);
            this.Lvw_License.Name = "Lvw_License";
            this.Lvw_License.Order = SortOrder.Descending;
            this.Lvw_License.Size = new Size(0x233, 0x5f);
            this.Lvw_License.SmallImageList = this.ilt_Icon;
            this.Lvw_License.SortColumn = 0;
            this.Lvw_License.TabIndex = 12;
            this.Lvw_License.UseCompatibleStateImageBehavior = false;
            this.Lvw_License.View = View.Details;
            this.columnHeader11.Text = "车牌号码";
            this.columnHeader11.Width = 0x7e;
            this.columnHeader12.Text = "车牌种类";
            this.columnHeader12.Width = 120;
            this.columnHeader13.Text = "下达时间";
            this.columnHeader13.Width = 0x88;
            this.columnHeader14.Text = "最后完成期限";
            this.columnHeader14.Width = 0x87;
            this.columnHeader15.Text = "计划单号";
            this.columnHeader15.Width = 0x77;
            this.columnHeader25.Text = "前片";
            this.columnHeader26.Text = "后片";
            this.panel1.Controls.Add(this.clb_NpType);
            this.panel1.Controls.Add(this.cbx_Select);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(8, 0x75);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new Padding(6);
            this.panel1.Size = new Size(0x233, 30);
            this.panel1.TabIndex = 11;
            this.clb_NpType.BackColor = SystemColors.Control;
            this.clb_NpType.BorderStyle = BorderStyle.None;
            this.clb_NpType.ColumnWidth = 50;
            this.clb_NpType.Dock = DockStyle.Fill;
            this.clb_NpType.FormattingEnabled = true;
            this.clb_NpType.Location = new Point(6, 6);
            this.clb_NpType.MultiColumn = true;
            this.clb_NpType.Name = "clb_NpType";
            this.clb_NpType.Size = new Size(0x1d9, 0x10);
            this.clb_NpType.TabIndex = 12;
            this.clb_NpType.ItemCheck += new ItemCheckEventHandler(this.clb_NpType_ItemCheck);
            this.cbx_Select.AutoSize = true;
            this.cbx_Select.Checked = true;
            this.cbx_Select.CheckState = CheckState.Checked;
            this.cbx_Select.Dock = DockStyle.Right;
            this.cbx_Select.Location = new Point(0x1df, 6);
            this.cbx_Select.Name = "cbx_Select";
            this.cbx_Select.Size = new Size(0x4e, 0x12);
            this.cbx_Select.TabIndex = 10;
            this.cbx_Select.Text = "全选/不选";
            this.cbx_Select.UseVisualStyleBackColor = true;
            this.cbx_Select.CheckedChanged += new EventHandler(this.cbx_Select_CheckStateChanged);
            this.gbx_TaskNp.Controls.Add(this.Lvw_TaskNp);
            this.gbx_TaskNp.Dock = DockStyle.Bottom;
            this.gbx_TaskNp.Location = new Point(8, 0xbc);
            this.gbx_TaskNp.Name = "gbx_TaskNp";
            this.gbx_TaskNp.Padding = new Padding(8);
            this.gbx_TaskNp.Size = new Size(0x243, 0xa5);
            this.gbx_TaskNp.TabIndex = 2;
            this.gbx_TaskNp.TabStop = false;
            this.gbx_TaskNp.Tag = "0";
            this.gbx_TaskNp.Text = "任务单包含的车牌信息";
            this.Lvw_TaskNp.Columns.AddRange(new ColumnHeader[] { this.columnHeader6, this.columnHeader7, this.columnHeader8, this.columnHeader9, this.columnHeader10, this.columnHeader27, this.columnHeader28 });
            this.Lvw_TaskNp.Dock = DockStyle.Fill;
            this.Lvw_TaskNp.FullRowSelect = true;
            this.Lvw_TaskNp.Location = new Point(8, 0x16);
            this.Lvw_TaskNp.Name = "Lvw_TaskNp";
            this.Lvw_TaskNp.Order = SortOrder.Descending;
            this.Lvw_TaskNp.Size = new Size(0x233, 0x87);
            this.Lvw_TaskNp.SmallImageList = this.ilt_Icon;
            this.Lvw_TaskNp.SortColumn = 0;
            this.Lvw_TaskNp.TabIndex = 1;
            this.Lvw_TaskNp.Tag = "0";
            this.Lvw_TaskNp.UseCompatibleStateImageBehavior = false;
            this.Lvw_TaskNp.View = View.Details;
            this.columnHeader6.Text = "车牌号码";
            this.columnHeader6.Width = 0x7e;
            this.columnHeader7.Text = "车牌种类";
            this.columnHeader7.Width = 120;
            this.columnHeader8.Text = "下达时间";
            this.columnHeader8.Width = 0x87;
            this.columnHeader9.Text = "最后完成期限";
            this.columnHeader9.Width = 0x87;
            this.columnHeader10.Text = "计划单号";
            this.columnHeader10.Width = 0x77;
            this.columnHeader27.Text = "前片";
            this.columnHeader28.Text = "后片";
            this.groupBox6.Controls.Add(this.Lvw_Task);
            this.groupBox6.Dock = DockStyle.Fill;
            this.groupBox6.Location = new Point(8, 0x115);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new Padding(5);
            this.groupBox6.Size = new Size(0xda, 0x4c);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "已下达的生产任务单";
            this.Lvw_Task.CheckBoxes = true;
            this.Lvw_Task.Columns.AddRange(new ColumnHeader[] { this.columnHeader21, this.columnHeader22, this.columnHeader23 });
            this.Lvw_Task.Dock = DockStyle.Fill;
            this.Lvw_Task.FullRowSelect = true;
            this.Lvw_Task.Location = new Point(5, 0x13);
            this.Lvw_Task.Name = "Lvw_Task";
            this.Lvw_Task.Order = SortOrder.Descending;
            this.Lvw_Task.Size = new Size(0xd0, 0x34);
            this.Lvw_Task.SmallImageList = this.ilt_Icon;
            this.Lvw_Task.SortColumn = 0;
            this.Lvw_Task.TabIndex = 0;
            this.Lvw_Task.UseCompatibleStateImageBehavior = false;
            this.Lvw_Task.View = View.Details;
            this.columnHeader21.Text = "生产指令单号";
            this.columnHeader21.Width = 130;
            this.columnHeader22.Text = "下达时间";
            this.columnHeader22.Width = 130;
            this.columnHeader23.Text = "下达人员";
            this.columnHeader23.Width = 80;
            this.groupBox2.Controls.Add(this.listVwWorkers);
            this.groupBox2.Controls.Add(this.btnSinglePrint);
            this.groupBox2.Controls.Add(this.cbx_PrintCodeBar);
            this.groupBox2.Controls.Add(this.cbx_PrintView);
            this.groupBox2.Controls.Add(this.cbx_BatchTask);
            this.groupBox2.Controls.Add(this.btn_Print);
            this.groupBox2.Controls.Add(this.btn_AddTask);
            this.groupBox2.Controls.Add(this.btn_Task);
            this.groupBox2.Controls.Add(this.btn_Refresh);
            this.groupBox2.Dock = DockStyle.Top;
            this.groupBox2.Location = new Point(8, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0xda, 0x10d);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "生产任务单操作";
            this.listVwWorkers.Columns.AddRange(new ColumnHeader[] { this.WorkerName });
            this.listVwWorkers.Dock = DockStyle.Right;
            this.listVwWorkers.Location = new Point(0x8f, 0x11);
            this.listVwWorkers.Name = "listVwWorkers";
            this.listVwWorkers.Size = new Size(0x48, 0xf9);
            this.listVwWorkers.TabIndex = 13;
            this.listVwWorkers.UseCompatibleStateImageBehavior = false;
            this.listVwWorkers.View = View.Details;
            this.WorkerName.Text = "车间工人";
            this.WorkerName.Width = 0x40;
            this.btnSinglePrint.Location = new Point(0x17, 0xa7);
            this.btnSinglePrint.Name = "btnSinglePrint";
            this.btnSinglePrint.Size = new Size(0x4b, 0x17);
            this.btnSinglePrint.TabIndex = 12;
            this.btnSinglePrint.Text = "条码补打";
            this.btnSinglePrint.UseVisualStyleBackColor = true;
            this.btnSinglePrint.Click += new EventHandler(this.btnSinglePrint_Click);
            this.cbx_PrintCodeBar.AutoSize = true;
            this.cbx_PrintCodeBar.Checked = true;
            this.cbx_PrintCodeBar.CheckState = CheckState.Checked;
            this.cbx_PrintCodeBar.Location = new Point(0x1a, 0xdf);
            this.cbx_PrintCodeBar.Name = "cbx_PrintCodeBar";
            this.cbx_PrintCodeBar.Size = new Size(0x6c, 0x10);
            this.cbx_PrintCodeBar.TabIndex = 11;
            this.cbx_PrintCodeBar.Text = "同时打印条形码";
            this.cbx_PrintCodeBar.UseVisualStyleBackColor = true;
            this.cbx_PrintCodeBar.CheckStateChanged += new EventHandler(this.cbx_PrintCodeBar_CheckStateChanged);
            this.cbx_PrintView.AutoSize = true;
            this.cbx_PrintView.Checked = true;
            this.cbx_PrintView.CheckState = CheckState.Checked;
            this.cbx_PrintView.Location = new Point(0x1a, 0xf5);
            this.cbx_PrintView.Name = "cbx_PrintView";
            this.cbx_PrintView.Size = new Size(0x48, 0x10);
            this.cbx_PrintView.TabIndex = 10;
            this.cbx_PrintView.Text = "打印预览";
            this.cbx_PrintView.UseVisualStyleBackColor = true;
            this.cbx_BatchTask.AutoSize = true;
            this.cbx_BatchTask.Checked = true;
            this.cbx_BatchTask.CheckState = CheckState.Checked;
            this.cbx_BatchTask.Location = new Point(0x1a, 0x93);
            this.cbx_BatchTask.Name = "cbx_BatchTask";
            this.cbx_BatchTask.Size = new Size(0x48, 0x10);
            this.cbx_BatchTask.TabIndex = 9;
            this.cbx_BatchTask.Text = "批量下达";
            this.cbx_BatchTask.UseVisualStyleBackColor = true;
            this.cbx_BatchTask.CheckStateChanged += new EventHandler(this.cbx_BatchTask_CheckStateChanged);
            this.btn_Print.Location = new Point(0x18, 0xc3);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new Size(0x4b, 0x17);
            this.btn_Print.TabIndex = 8;
            this.btn_Print.Text = "打印";
            this.btn_Print.UseVisualStyleBackColor = true;
            this.btn_Print.Click += new EventHandler(this.btn_Print_Click);
            this.btn_AddTask.Enabled = false;
            this.btn_AddTask.Location = new Point(0x18, 0x53);
            this.btn_AddTask.Name = "btn_AddTask";
            this.btn_AddTask.Size = new Size(0x4b, 0x17);
            this.btn_AddTask.TabIndex = 8;
            this.btn_AddTask.Text = "添加到任务";
            this.btn_AddTask.UseVisualStyleBackColor = true;
            this.btn_AddTask.Click += new EventHandler(this.btn_AddTask_Click);
            this.btn_Task.ForeColor = Color.LimeGreen;
            this.btn_Task.Location = new Point(0x18, 0x75);
            this.btn_Task.Name = "btn_Task";
            this.btn_Task.Size = new Size(0x4b, 0x17);
            this.btn_Task.TabIndex = 4;
            this.btn_Task.Text = "下达任务单";
            this.btn_Task.UseVisualStyleBackColor = true;
            this.btn_Task.Click += new EventHandler(this.btn_Task_Click);
            this.btn_Refresh.Location = new Point(0x18, 0x24);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new Size(0x4b, 0x17);
            this.btn_Refresh.TabIndex = 3;
            this.btn_Refresh.Text = "刷新";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new EventHandler(this.btn_Refresh_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x341, 0x169);
            base.Controls.Add(this.splitContainer1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "Frm_Prod";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "生产管理";
            base.FormClosed += new FormClosedEventHandler(this.Frm_Prod_FormClosed);
            base.Load += new EventHandler(this.Frm_Prod_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.gbx_NpNum.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbx_TaskNp.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            base.ResumeLayout(false);
        }

        private void Lvw_Plan_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!this.cbx_BatchTask.Checked)
            {
                this.Lvw_Plan.Cursor = Cursors.WaitCursor;
                if (e.Item.Checked)
                {
                    DataSet toDoNp = new License().GetToDoNp(e.Item.SubItems[1].Text);
                    for (int i = 0; i < toDoNp.Tables[0].Rows.Count; i++)
                    {
                        ListViewItem item = new ListViewItem {
                            Checked = false,
                            ImageKey = "NP",
                            Tag = toDoNp.Tables[0].Rows[i]["NpId"].ToString(),
                            Text = toDoNp.Tables[0].Rows[i]["NpNo"].ToString()
                        };
                        item.SubItems.Add(toDoNp.Tables[0].Rows[i]["Description"].ToString());
                        DateTime time = (DateTime) toDoNp.Tables[0].Rows[i]["PlanTime"];
                        item.SubItems.Add(time.ToShortDateString());
                        DateTime time2 = (DateTime) toDoNp.Tables[0].Rows[i]["Deadline"];
                        item.SubItems.Add(time2.ToShortDateString());
                        item.SubItems.Add(toDoNp.Tables[0].Rows[i]["PlanId"].ToString());
                        item.SubItems.Add(toDoNp.Tables[0].Rows[i]["IsFront"].ToString());
                        item.SubItems.Add(toDoNp.Tables[0].Rows[i]["IsBack"].ToString());
                        this.Lvw_License.Items.Add(item);
                    }
                    this.gbx_NpNum.Text = string.Concat(new object[] { "车牌明细  数量:", this.Lvw_License.Items.Count, "  已选择数:", this.Lvw_License.CheckedItems.Count });
                }
                else
                {
                    foreach (ListViewItem item2 in this.Lvw_License.Items)
                    {
                        if (item2.SubItems[4].Text.ToString() == e.Item.SubItems[1].Text.Trim())
                        {
                            item2.Remove();
                        }
                    }
                    this.gbx_NpNum.Text = string.Concat(new object[] { "车牌明细  数量:", this.Lvw_License.Items.Count, "  已选择数:", this.Lvw_License.CheckedItems.Count });
                }
                Sql2KDataAccess access = new Sql2KDataAccess();
                Hashtable hashtable = new Hashtable();
                this.clb_NpType.Items.Clear();
                hashtable.Clear();
                foreach (ListViewItem item3 in this.Lvw_Plan.CheckedItems)
                {
                    DataSet set2 = access.Run_SqlText("select Distinct Code,Description from V_Np where PlanId='" + item3.SubItems[1].Text.Trim() + "'");
                    for (int j = 0; j < set2.Tables[0].Rows.Count; j++)
                    {
                        if (!hashtable.ContainsKey(set2.Tables[0].Rows[j]["Code"].ToString().Trim()))
                        {
                            hashtable.Add(set2.Tables[0].Rows[j]["Code"].ToString().Trim(), set2.Tables[0].Rows[j]["Description"].ToString().Trim());
                        }
                    }
                }
                IDictionaryEnumerator enumerator = hashtable.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    this.clb_NpType.Items.Add(enumerator.Value, false);
                }
                this.Lvw_Plan.Cursor = Cursors.Default;
            }
        }

        public static Frm_Prod Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Frm_Prod();
                }
                return m_Instance;
            }
        }
    }
}

