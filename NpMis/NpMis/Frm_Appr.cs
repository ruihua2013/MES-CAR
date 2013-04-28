namespace NpMis
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class Frm_Appr : Form
    {
        private AutoRefresh Ar;
        private BackgroundWorker backgroundWorker;
        private Button btn_NotPass;
        private Button btn_Pass;
        private Button btn_Refresh;
        private CheckBox cbx_AutoRefresh;
        private CheckBox cbx_Select;
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
        public static bool G_IsRemarkAppr;
        private DataSet G_NPTypeDS;
        private Hashtable G_SelectNPCount;
        private int G_SelectPlan;
        private int G_SelectTotal;
        private int G_Total;
        private Hashtable G_TotalNPCount;
        private GroupBox gbx_NpCount;
        private GroupBox groupBox2;
        private ImageList ilt_Icon;
        private Label labelSelectCount;
        private Label labRemark;
        private ListViewClear Lc;
        private SortedListView Lvw_Audited;
        private SortedListView Lvw_License;
        private SortedListView Lvw_Plan;
        private static Frm_Appr m_Instance;
        private Panel panel1;
        private SplitContainer splitContainer1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;

        private Frm_Appr()
        {
            this.InitializeComponent();
            this.G_SelectNPCount = new Hashtable();
            this.G_TotalNPCount = new Hashtable();
            this.G_NPTypeDS = Plan.GetNpTypeDS();
        }

        private void AddList(ListViewItem Lvi)
        {
            foreach (ListViewItem item in this.Lvw_Plan.Items)
            {
                if (item.Tag.ToString() == Lvi.Tag.ToString())
                {
                    return;
                }
            }
            this.Lvw_Plan.Items.Add(Lvi);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (this.cbx_AutoRefresh.Checked)
            {
                DataSet unApprPlan = new Plan().GetUnApprPlan(User.IsHaveAuthority("特殊审批"), G_IsRemarkAppr);
                if (unApprPlan.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < unApprPlan.Tables[0].Rows.Count; i++)
                    {
                        ListViewItem item = new ListViewItem {
                            Tag = unApprPlan.Tables[0].Rows[i]["PlanId"].ToString(),
                            ImageKey = "Plan"
                        };
                        item.SubItems.Add(unApprPlan.Tables[0].Rows[i]["PlanId"].ToString());
                        item.SubItems.Add(unApprPlan.Tables[0].Rows[i]["PlanKind"].ToString());
                        item.SubItems.Add(unApprPlan.Tables[0].Rows[i]["PlanDepart"].ToString());
                        item.SubItems.Add(unApprPlan.Tables[0].Rows[i]["PlanTime"].ToString());
                        item.SubItems.Add(unApprPlan.Tables[0].Rows[i]["InputUser"].ToString());
                        item.SubItems.Add(unApprPlan.Tables[0].Rows[i]["InPutTime"].ToString());
                        item.SubItems.Add(unApprPlan.Tables[0].Rows[i]["TotalCount"].ToString());
                        if (this.Lvw_Plan != null)
                        {
                            this.Lvw_Plan.Invoke(this.Ar, new object[] { item });
                        }
                    }
                }
                Thread.Sleep(0x7d0);
            }
            if (this.backgroundWorker.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private void btn_NotPass_Click(object sender, EventArgs e)
        {
            if (this.Lvw_Plan.CheckedItems.Count != 0)
            {
                Plan plan = new Plan();
                foreach (ListViewItem item in this.Lvw_Plan.Items)
                {
                    if (item.Checked && plan.SmtAudiResult(item.SubItems[1].Text.Trim(), false))
                    {
                        ListViewItem item2 = (ListViewItem) item.Clone();
                        item2.ImageKey = "NotPass";
                        this.Lvw_Audited.Items.Add(item2);
                    }
                }
                this.btn_Refresh_Click(this, new EventArgs());
            }
        }

        private void btn_Pass_Click(object sender, EventArgs e)
        {
            if (this.Lvw_Plan.CheckedItems.Count != 0)
            {
                Plan plan = new Plan();
                foreach (ListViewItem item in this.Lvw_Plan.Items)
                {
                    if (item.Checked && plan.SmtAudiResult(item.SubItems[1].Text.Trim(), true))
                    {
                        ListViewItem item2 = (ListViewItem) item.Clone();
                        item2.ImageKey = "Ok";
                        this.Lvw_Audited.Items.Add(item2);
                    }
                }
                this.btn_Refresh_Click(this, new EventArgs());
            }
        }

        public void btn_Refresh_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Plan plan = new Plan();
            DataSet unApprPlan = plan.GetUnApprPlan(User.IsHaveAuthority("特殊审批"), G_IsRemarkAppr);
            this.Lvw_Plan.Items.Clear();
            for (int i = 0; i < unApprPlan.Tables[0].Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem {
                    Checked = this.cbx_Select.Checked,
                    ImageKey = "Plan",
                    Tag = unApprPlan.Tables[0].Rows[i]["PlanId"].ToString()
                };
                item.SubItems.Add(unApprPlan.Tables[0].Rows[i]["PlanId"].ToString());
                item.SubItems.Add(unApprPlan.Tables[0].Rows[i]["PlanKind"].ToString());
                item.SubItems.Add(unApprPlan.Tables[0].Rows[i]["TotalCount"].ToString());
                item.SubItems.Add(unApprPlan.Tables[0].Rows[i]["PlanDepart"].ToString());
                item.SubItems.Add(unApprPlan.Tables[0].Rows[i]["PlanTime"].ToString());
                item.SubItems.Add(unApprPlan.Tables[0].Rows[i]["InputUser"].ToString());
                item.SubItems.Add(unApprPlan.Tables[0].Rows[i]["InPutTime"].ToString());
                this.Lvw_Plan.Items.Add(item);
            }
            unApprPlan = plan.GetAuditedPlan(User.IsHaveAuthority("特殊审批"), G_IsRemarkAppr);
            this.Lvw_Audited.Items.Clear();
            for (int j = 0; j < unApprPlan.Tables[0].Rows.Count; j++)
            {
                ListViewItem item2 = new ListViewItem {
                    ImageKey = (unApprPlan.Tables[0].Rows[j]["IsPass"].ToString() == "1") ? "Ok" : "NotPass"
                };
                item2.SubItems.Add(unApprPlan.Tables[0].Rows[j]["PlanId"].ToString());
                item2.SubItems.Add(unApprPlan.Tables[0].Rows[j]["PlanKind"].ToString());
                item2.SubItems.Add(unApprPlan.Tables[0].Rows[j]["PlanDepart"].ToString());
                item2.SubItems.Add(unApprPlan.Tables[0].Rows[j]["PlanTime"].ToString());
                item2.SubItems.Add(unApprPlan.Tables[0].Rows[j]["InputUser"].ToString());
                item2.SubItems.Add(unApprPlan.Tables[0].Rows[j]["InPutTime"].ToString());
                item2.SubItems.Add(unApprPlan.Tables[0].Rows[j]["TotalCount"].ToString());
                this.Lvw_Audited.Items.Add(item2);
            }
            this.Lvw_License.Items.Clear();
            this.labRemark.Text = "";
            this.gbx_NpCount.Text = "车牌信息";
            this.CountAppDataDetails();
            this.ShowCountInLabel();
            this.Cursor = Cursors.Default;
        }

        private void cbx_AutoRefresh_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.cbx_AutoRefresh.Checked)
            {
                if (!this.backgroundWorker.IsBusy)
                {
                    this.backgroundWorker.RunWorkerAsync();
                }
            }
            else
            {
                this.backgroundWorker.CancelAsync();
            }
        }

        private void cbx_Select_CheckStateChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.Lvw_Plan.Items)
            {
                item.Checked = this.cbx_Select.Checked;
            }
        }

        private void CountAppData()
        {
            Plan plan = new Plan();
            int num = 0;
            int num2 = 0;
            for (int i = 0; i < this.Lvw_Plan.Items.Count; i++)
            {
                DataSet npByPlanId = plan.GetNpByPlanId(this.Lvw_Plan.Items[i].SubItems[1].Text);
                num2 += npByPlanId.Tables[0].Rows.Count;
                if (this.Lvw_Plan.Items[i].Checked)
                {
                    num += npByPlanId.Tables[0].Rows.Count;
                }
            }
            this.labelSelectCount.Text = string.Concat(new object[] { "共有", this.Lvw_Plan.Items.Count, "个计划单", num2.ToString(), "副车牌等待审批！当前选取", num.ToString(), "副" });
        }

        private void CountAppDataDetails()
        {
            Plan plan = new Plan();
            this.G_SelectPlan = 0;
            this.G_SelectTotal = 0;
            this.G_Total = 0;
            this.G_TotalNPCount.Clear();
            this.G_SelectNPCount.Clear();
            for (int i = 0; i < this.Lvw_Plan.Items.Count; i++)
            {
                DataSet npCountByPlanID = plan.GetNpCountByPlanID(this.Lvw_Plan.Items[i].SubItems[1].Text);
                if ((npCountByPlanID != null) && (npCountByPlanID.Tables[0].Rows.Count > 0))
                {
                    for (int j = 0; j < npCountByPlanID.Tables[0].Rows.Count; j++)
                    {
                        string key = npCountByPlanID.Tables[0].Rows[j]["NPType"].ToString();
                        if (this.G_TotalNPCount.ContainsKey(npCountByPlanID.Tables[0].Rows[j]["NPType"].ToString()))
                        {
                            this.G_TotalNPCount[key] = Convert.ToString((int) (int.Parse(this.G_TotalNPCount[key].ToString()) + int.Parse(npCountByPlanID.Tables[0].Rows[j]["Count"].ToString())));
                        }
                        else
                        {
                            this.G_TotalNPCount.Add(key, 0);
                            this.G_TotalNPCount[key] = Convert.ToString((int) (int.Parse(this.G_TotalNPCount[key].ToString()) + int.Parse(npCountByPlanID.Tables[0].Rows[j]["Count"].ToString())));
                        }
                        this.G_Total += int.Parse(npCountByPlanID.Tables[0].Rows[j]["Count"].ToString());
                        if (this.Lvw_Plan.Items[i].Checked)
                        {
                            if (this.G_SelectNPCount.ContainsKey(npCountByPlanID.Tables[0].Rows[j]["NPType"].ToString()))
                            {
                                this.G_SelectNPCount[key] = Convert.ToString((int) (int.Parse(this.G_SelectNPCount[key].ToString()) + int.Parse(npCountByPlanID.Tables[0].Rows[j]["Count"].ToString())));
                            }
                            else
                            {
                                this.G_SelectNPCount.Add(key, 0);
                                this.G_SelectNPCount[key] = Convert.ToString((int) (int.Parse(this.G_SelectNPCount[key].ToString()) + int.Parse(npCountByPlanID.Tables[0].Rows[j]["Count"].ToString())));
                            }
                            this.G_SelectTotal += int.Parse(npCountByPlanID.Tables[0].Rows[j]["Count"].ToString());
                            this.G_SelectPlan++;
                        }
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

        private void Frm_Appr_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_Instance = null;
            this.cbx_AutoRefresh.Checked = false;
            this.backgroundWorker.CancelAsync();
        }

        private void Frm_Appr_Load(object sender, EventArgs e)
        {
            this.Ar = (AutoRefresh) Delegate.Combine(this.Ar, new AutoRefresh(this.AddList));
            this.Lc = (ListViewClear) Delegate.Combine(this.Lc, new ListViewClear(this.ListClear));
            try
            {
                base.Size = base.Parent.ClientSize;
            }
            catch (Exception)
            {
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Frm_Appr));
            this.ilt_Icon = new ImageList(this.components);
            this.backgroundWorker = new BackgroundWorker();
            this.splitContainer1 = new SplitContainer();
            this.gbx_NpCount = new GroupBox();
            this.Lvw_License = new SortedListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader19 = new ColumnHeader();
            this.columnHeader20 = new ColumnHeader();
            this.columnHeader21 = new ColumnHeader();
            this.groupBox2 = new GroupBox();
            this.labRemark = new Label();
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.Lvw_Plan = new SortedListView();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.columnHeader17 = new ColumnHeader();
            this.columnHeader5 = new ColumnHeader();
            this.columnHeader6 = new ColumnHeader();
            this.columnHeader9 = new ColumnHeader();
            this.columnHeader7 = new ColumnHeader();
            this.columnHeader8 = new ColumnHeader();
            this.panel1 = new Panel();
            this.labelSelectCount = new Label();
            this.cbx_AutoRefresh = new CheckBox();
            this.btn_NotPass = new Button();
            this.btn_Pass = new Button();
            this.btn_Refresh = new Button();
            this.cbx_Select = new CheckBox();
            this.tabPage2 = new TabPage();
            this.Lvw_Audited = new SortedListView();
            this.columnHeader10 = new ColumnHeader();
            this.columnHeader11 = new ColumnHeader();
            this.columnHeader18 = new ColumnHeader();
            this.columnHeader12 = new ColumnHeader();
            this.columnHeader13 = new ColumnHeader();
            this.columnHeader14 = new ColumnHeader();
            this.columnHeader15 = new ColumnHeader();
            this.columnHeader16 = new ColumnHeader();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbx_NpCount.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            base.SuspendLayout();
            this.ilt_Icon.ImageStream = (ImageListStreamer) manager.GetObject("ilt_Icon.ImageStream");
            this.ilt_Icon.TransparentColor = Color.Transparent;
            this.ilt_Icon.Images.SetKeyName(0, "Ok");
            this.ilt_Icon.Images.SetKeyName(1, "NotPass");
            this.ilt_Icon.Images.SetKeyName(2, "NP");
            this.ilt_Icon.Images.SetKeyName(3, "Plan");
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.gbx_NpCount);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Padding = new Padding(5);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Padding = new Padding(5);
            this.splitContainer1.Size = new Size(740, 0x1b7);
            this.splitContainer1.SplitterDistance = 0xf6;
            this.splitContainer1.TabIndex = 0;
            this.gbx_NpCount.Controls.Add(this.Lvw_License);
            this.gbx_NpCount.Dock = DockStyle.Fill;
            this.gbx_NpCount.Location = new Point(5, 5);
            this.gbx_NpCount.Name = "gbx_NpCount";
            this.gbx_NpCount.Padding = new Padding(8);
            this.gbx_NpCount.Size = new Size(0xec, 0x128);
            this.gbx_NpCount.TabIndex = 2;
            this.gbx_NpCount.TabStop = false;
            this.gbx_NpCount.Text = "车牌信息";
            this.Lvw_License.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader19, this.columnHeader20, this.columnHeader21 });
            this.Lvw_License.Dock = DockStyle.Fill;
            this.Lvw_License.FullRowSelect = true;
            this.Lvw_License.GridLines = true;
            this.Lvw_License.LabelEdit = true;
            this.Lvw_License.Location = new Point(8, 0x16);
            this.Lvw_License.Name = "Lvw_License";
            this.Lvw_License.Order = SortOrder.Descending;
            this.Lvw_License.Size = new Size(220, 0x10a);
            this.Lvw_License.SmallImageList = this.ilt_Icon;
            this.Lvw_License.SortColumn = 0;
            this.Lvw_License.TabIndex = 0;
            this.Lvw_License.UseCompatibleStateImageBehavior = false;
            this.Lvw_License.View = View.Details;
            this.Lvw_License.MouseDoubleClick += new MouseEventHandler(this.Lvw_License_MouseDoubleClick);
            this.Lvw_License.ItemCheck += new ItemCheckEventHandler(this.Lvw_License_ItemCheck);
            this.Lvw_License.BeforeLabelEdit += new LabelEditEventHandler(this.Lvw_License_BeforeLabelEdit);
            this.columnHeader1.Text = "车牌号码";
            this.columnHeader1.Width = 0x7e;
            this.columnHeader2.Text = "车牌种类";
            this.columnHeader2.Width = 100;
            this.columnHeader19.Text = "前片";
            this.columnHeader20.Text = "后片";
            this.columnHeader21.Text = "邮寄";
            this.groupBox2.Controls.Add(this.labRemark);
            this.groupBox2.Dock = DockStyle.Bottom;
            this.groupBox2.Location = new Point(5, 0x12d);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new Padding(8);
            this.groupBox2.Size = new Size(0xec, 0x85);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "备注信息";
            this.labRemark.Dock = DockStyle.Fill;
            this.labRemark.Location = new Point(8, 0x16);
            this.labRemark.Name = "labRemark";
            this.labRemark.Size = new Size(220, 0x67);
            this.labRemark.TabIndex = 0;
            this.labRemark.TextAlign = ContentAlignment.MiddleCenter;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new Point(5, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(480, 0x1ad);
            this.tabControl1.TabIndex = 0;
            this.tabPage1.Controls.Add(this.Lvw_Plan);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new Point(4, 0x15);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(0x1d8, 0x194);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "未审批";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.Lvw_Plan.AllowColumnReorder = true;
            this.Lvw_Plan.CheckBoxes = true;
            this.Lvw_Plan.Columns.AddRange(new ColumnHeader[] { this.columnHeader3, this.columnHeader4, this.columnHeader17, this.columnHeader8, this.columnHeader5, this.columnHeader6, this.columnHeader9, this.columnHeader7 });
            this.Lvw_Plan.Dock = DockStyle.Fill;
            this.Lvw_Plan.FullRowSelect = true;
            this.Lvw_Plan.Location = new Point(3, 0x85);
            this.Lvw_Plan.Name = "Lvw_Plan";
            this.Lvw_Plan.Order = SortOrder.Descending;
            this.Lvw_Plan.Size = new Size(0x1d2, 0x10c);
            this.Lvw_Plan.SmallImageList = this.ilt_Icon;
            this.Lvw_Plan.SortColumn = 0;
            this.Lvw_Plan.Sorting = SortOrder.Descending;
            this.Lvw_Plan.TabIndex = 1;
            this.Lvw_Plan.UseCompatibleStateImageBehavior = false;
            this.Lvw_Plan.View = View.Details;
            this.Lvw_Plan.SelectedIndexChanged += new EventHandler(this.Lvw_Plan_SelectedIndexChanged);
            this.Lvw_Plan.ItemCheck += new ItemCheckEventHandler(this.Lvw_Plan_ItemCheck);
            this.columnHeader3.Text = "";
            this.columnHeader3.Width = 0x27;
            this.columnHeader4.Text = "计划单号";
            this.columnHeader4.Width = 150;
            this.columnHeader17.Text = "计划单类型";
            this.columnHeader17.Width = 0x69;
            this.columnHeader5.Text = "计划部门";
            this.columnHeader5.Width = 150;
            this.columnHeader6.Text = "上报时间";
            this.columnHeader6.Width = 130;
            this.columnHeader9.Text = "录入人员";
            this.columnHeader9.Width = 80;
            this.columnHeader7.Text = "录入时间";
            this.columnHeader7.Width = 130;
            this.columnHeader8.Text = "总数";
            this.columnHeader8.Width = 80;
            this.panel1.Controls.Add(this.labelSelectCount);
            this.panel1.Controls.Add(this.cbx_AutoRefresh);
            this.panel1.Controls.Add(this.btn_NotPass);
            this.panel1.Controls.Add(this.btn_Pass);
            this.panel1.Controls.Add(this.btn_Refresh);
            this.panel1.Controls.Add(this.cbx_Select);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1d2, 130);
            this.panel1.TabIndex = 0;
            this.labelSelectCount.AutoSize = true;
            this.labelSelectCount.Location = new Point(12, 6);
            this.labelSelectCount.Name = "labelSelectCount";
            this.labelSelectCount.Size = new Size(11, 12);
            this.labelSelectCount.TabIndex = 5;
            this.labelSelectCount.Text = "0";
            this.cbx_AutoRefresh.AutoSize = true;
            this.cbx_AutoRefresh.Location = new Point(0x5f, 0x49);
            this.cbx_AutoRefresh.Name = "cbx_AutoRefresh";
            this.cbx_AutoRefresh.Size = new Size(0x48, 0x10);
            this.cbx_AutoRefresh.TabIndex = 4;
            this.cbx_AutoRefresh.Text = "自动刷新";
            this.cbx_AutoRefresh.UseVisualStyleBackColor = true;
            this.cbx_AutoRefresh.CheckStateChanged += new EventHandler(this.cbx_AutoRefresh_CheckStateChanged);
            this.btn_NotPass.ForeColor = Color.Red;
            this.btn_NotPass.Location = new Point(0x10d, 0x45);
            this.btn_NotPass.Name = "btn_NotPass";
            this.btn_NotPass.Size = new Size(0x4b, 0x17);
            this.btn_NotPass.TabIndex = 3;
            this.btn_NotPass.Text = "审核不通过";
            this.btn_NotPass.UseVisualStyleBackColor = true;
            this.btn_NotPass.Click += new EventHandler(this.btn_NotPass_Click);
            this.btn_Pass.ForeColor = Color.LimeGreen;
            this.btn_Pass.Location = new Point(0xb3, 0x45);
            this.btn_Pass.Name = "btn_Pass";
            this.btn_Pass.Size = new Size(0x4b, 0x17);
            this.btn_Pass.TabIndex = 2;
            this.btn_Pass.Text = "审核通过";
            this.btn_Pass.UseVisualStyleBackColor = true;
            this.btn_Pass.Click += new EventHandler(this.btn_Pass_Click);
            this.btn_Refresh.Location = new Point(14, 0x45);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new Size(0x4b, 0x17);
            this.btn_Refresh.TabIndex = 1;
            this.btn_Refresh.Text = "刷新";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new EventHandler(this.btn_Refresh_Click);
            this.cbx_Select.AutoSize = true;
            this.cbx_Select.Location = new Point(14, 0x6c);
            this.cbx_Select.Name = "cbx_Select";
            this.cbx_Select.Size = new Size(90, 0x10);
            this.cbx_Select.TabIndex = 0;
            this.cbx_Select.Text = "全选/全不选";
            this.cbx_Select.UseVisualStyleBackColor = true;
            this.cbx_Select.CheckStateChanged += new EventHandler(this.cbx_Select_CheckStateChanged);
            this.tabPage2.Controls.Add(this.Lvw_Audited);
            this.tabPage2.Location = new Point(4, 0x15);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(0x1d8, 0x194);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "已审批";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.Lvw_Audited.Columns.AddRange(new ColumnHeader[] { this.columnHeader10, this.columnHeader11, this.columnHeader18, this.columnHeader12, this.columnHeader13, this.columnHeader14, this.columnHeader15, this.columnHeader16 });
            this.Lvw_Audited.Dock = DockStyle.Fill;
            this.Lvw_Audited.FullRowSelect = true;
            this.Lvw_Audited.Location = new Point(3, 3);
            this.Lvw_Audited.Name = "Lvw_Audited";
            this.Lvw_Audited.Order = SortOrder.Descending;
            this.Lvw_Audited.Size = new Size(0x1d2, 0x18e);
            this.Lvw_Audited.SmallImageList = this.ilt_Icon;
            this.Lvw_Audited.SortColumn = 0;
            this.Lvw_Audited.TabIndex = 2;
            this.Lvw_Audited.UseCompatibleStateImageBehavior = false;
            this.Lvw_Audited.View = View.Details;
            this.Lvw_Audited.SelectedIndexChanged += new EventHandler(this.Lvw_Plan_SelectedIndexChanged);
            this.columnHeader10.Text = "";
            this.columnHeader10.Width = 20;
            this.columnHeader11.Text = "计划单号";
            this.columnHeader11.Width = 150;
            this.columnHeader18.Text = "计划单类型";
            this.columnHeader18.Width = 0x60;
            this.columnHeader12.Text = "计划部门";
            this.columnHeader12.Width = 150;
            this.columnHeader13.Text = "上报时间";
            this.columnHeader13.Width = 130;
            this.columnHeader14.Text = "录入人员";
            this.columnHeader14.Width = 80;
            this.columnHeader15.Text = "录入时间";
            this.columnHeader15.Width = 130;
            this.columnHeader16.Text = "总数";
            this.columnHeader16.Width = 80;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(740, 0x1b7);
            base.Controls.Add(this.splitContainer1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "Frm_Appr";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "审批管理";
            base.FormClosing += new FormClosingEventHandler(this.Frm_Appr_FormClosing);
            base.Load += new EventHandler(this.Frm_Appr_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.gbx_NpCount.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void ListClear()
        {
            this.Lvw_Plan.Items.Clear();
        }

        private void Lvw_License_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
        }

        private void Lvw_License_ItemCheck(object sender, ItemCheckEventArgs e)
        {
        }

        private void Lvw_License_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void Lvw_Plan_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Plan plan = new Plan();
            this.CountAppDataDetails();
            DataSet npCountByPlanID = plan.GetNpCountByPlanID(this.Lvw_Plan.Items[e.Index].SubItems[1].Text);
            if ((npCountByPlanID != null) && (npCountByPlanID.Tables[0].Rows.Count > 0))
            {
                for (int i = 0; i < npCountByPlanID.Tables[0].Rows.Count; i++)
                {
                    string key = npCountByPlanID.Tables[0].Rows[i]["NPType"].ToString();
                    if (e.NewValue == CheckState.Checked)
                    {
                        if (this.G_SelectNPCount.ContainsKey(npCountByPlanID.Tables[0].Rows[i]["NPType"].ToString()))
                        {
                            this.G_SelectNPCount[key] = Convert.ToString((int) (int.Parse(this.G_SelectNPCount[key].ToString()) + int.Parse(npCountByPlanID.Tables[0].Rows[i]["Count"].ToString())));
                        }
                        else
                        {
                            this.G_SelectNPCount.Add(key, 0);
                            this.G_SelectNPCount[key] = Convert.ToString((int) (int.Parse(this.G_SelectNPCount[key].ToString()) + int.Parse(npCountByPlanID.Tables[0].Rows[i]["Count"].ToString())));
                        }
                        this.G_SelectTotal += int.Parse(npCountByPlanID.Tables[0].Rows[i]["Count"].ToString());
                        this.G_SelectPlan++;
                    }
                    else
                    {
                        if (this.G_SelectNPCount.ContainsKey(npCountByPlanID.Tables[0].Rows[i]["NPType"].ToString()))
                        {
                            this.G_SelectNPCount[key] = Convert.ToString((int) (int.Parse(this.G_SelectNPCount[key].ToString()) - int.Parse(npCountByPlanID.Tables[0].Rows[i]["Count"].ToString())));
                        }
                        this.G_SelectTotal -= int.Parse(npCountByPlanID.Tables[0].Rows[i]["Count"].ToString());
                        this.G_SelectPlan--;
                    }
                }
            }
            this.ShowCountInLabel();
        }

        private void Lvw_Plan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Plan plan = new Plan();
                ListView view = (ListView) sender;
                DataSet npByPlanId = plan.GetNpByPlanId(view.SelectedItems[0].SubItems[1].Text);
                this.Lvw_License.Items.Clear();
                this.labRemark.Text = plan.GetPlanRemark(view.SelectedItems[0].SubItems[1].Text);
                if (npByPlanId.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < npByPlanId.Tables[0].Rows.Count; i++)
                    {
                        ListViewItem item = new ListViewItem {
                            ImageKey = "NP",
                            Text = npByPlanId.Tables[0].Rows[i]["NpNo"].ToString()
                        };
                        item.SubItems.Add(npByPlanId.Tables[0].Rows[i]["CodeName"].ToString());
                        item.SubItems.Add(npByPlanId.Tables[0].Rows[i]["IsFront"].ToString());
                        item.SubItems.Add(npByPlanId.Tables[0].Rows[i]["IsBack"].ToString());
                        item.SubItems.Add(npByPlanId.Tables[0].Rows[i]["IsMail"].ToString());
                        this.Lvw_License.Items.Add(item);
                    }
                }
                this.gbx_NpCount.Text = "车牌信息 数量:" + npByPlanId.Tables[0].Rows.Count.ToString();
            }
            catch (Exception)
            {
            }
        }

        private void ShowCountInLabel()
        {
            this.labelSelectCount.Text = string.Concat(new object[] { "共有", this.Lvw_Plan.Items.Count, "个计划单", this.G_Total.ToString(), "副车牌等待审批！！当前选取", this.G_SelectPlan, "个计划单", this.G_SelectTotal.ToString(), "副\r\n\r\n" });
            if ((this.G_NPTypeDS != null) && (this.G_NPTypeDS.Tables[0].Rows.Count > 0))
            {
                this.labelSelectCount.Text = this.labelSelectCount.Text + "共有";
                string str = "选取";
                for (int i = 0; i < this.G_NPTypeDS.Tables[0].Rows.Count; i++)
                {
                    if (this.G_TotalNPCount.ContainsKey(this.G_NPTypeDS.Tables[0].Rows[i]["Code"].ToString()))
                    {
                        int num2 = int.Parse(this.G_TotalNPCount[this.G_NPTypeDS.Tables[0].Rows[i]["Code"].ToString()].ToString());
                        this.labelSelectCount.Text = this.labelSelectCount.Text + this.G_NPTypeDS.Tables[0].Rows[i]["Description"].ToString() + string.Format("{0,-5}", num2);
                        if (this.G_SelectNPCount.ContainsKey(this.G_NPTypeDS.Tables[0].Rows[i]["Code"].ToString()))
                        {
                            num2 = int.Parse(this.G_SelectNPCount[this.G_NPTypeDS.Tables[0].Rows[i]["Code"].ToString()].ToString());
                            str = str + this.G_NPTypeDS.Tables[0].Rows[i]["Description"].ToString() + string.Format("{0,-5}", num2);
                        }
                        else
                        {
                            str = str + this.G_NPTypeDS.Tables[0].Rows[i]["Description"].ToString() + "0    ";
                        }
                    }
                }
                this.labelSelectCount.Text = this.labelSelectCount.Text + "\r\n" + str;
            }
        }

        public static Frm_Appr Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Frm_Appr();
                }
                return m_Instance;
            }
        }
    }
}

