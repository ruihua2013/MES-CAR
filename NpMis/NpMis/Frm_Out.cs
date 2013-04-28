namespace NpMis
{
    using Common;
    using NpMis.Frm;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class Frm_Out : Form
    {
        private Button btn_Out;
        private Button btn_Print;
        private Button btn_Refresh;
        private Button btnNullPrint;
        private CheckBox cbx_PrintSendView;
        private CheckBox cbx_Select;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader10;
        private ColumnHeader columnHeader11;
        private ColumnHeader columnHeader12;
        private ColumnHeader columnHeader13;
        private ColumnHeader columnHeader14;
        private ColumnHeader columnHeader15;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader21;
        private ColumnHeader columnHeader22;
        private ColumnHeader columnHeader23;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private IContainer components;
        private int DetailPrintPage;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private ImageList ilt_Icon;
        private ListViewItem LVICurPrint;
        private SortedListView Lvw_SendList;
        private SortedListView Lvw_Task;
        private SortedListView Lvw_TaskNp;
        private static Frm_Out m_Instance;
        private DataSet SendListDetailsds;
        private DataSet SendListds;
        private SplitContainer splitContainer1;

        private Frm_Out()
        {
            this.InitializeComponent();
        }

        private void btn_Out_Click(object sender, EventArgs e)
        {
            if (this.Lvw_Task.CheckedItems.Count == 0)
            {
                MessageBox.Show("请选择要打印的生产任务", "出库管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Invoice invoice = new Invoice();
                string taskStr = "";
                foreach (ListViewItem item in this.Lvw_Task.Items)
                {
                    if (item.Checked)
                    {
                        taskStr = taskStr + item.Text.Trim() + "-";
                        item.Remove();
                    }
                }
                invoice.NewSend(taskStr);
                MessageBox.Show("出库成功", "出库管理", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Lvw_TaskNp.Items.Clear();
                this.btn_Refresh_Click(this, new EventArgs());
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            if (this.Lvw_SendList.CheckedItems.Count == 0)
            {
                MessageBox.Show("请选择要打印的送货单", "出库管理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                foreach (ListViewItem item in this.Lvw_SendList.Items)
                {
                    if (!item.Checked)
                    {
                        continue;
                    }
                    this.LVICurPrint = item;
                    Sql2KDataAccess access = new Sql2KDataAccess();
                    string queryStr = "select * from V_SendList Where SendId='" + this.LVICurPrint.Text.Trim() + "'";
                    this.SendListds = access.Run_SqlText(queryStr);
                    if ((this.SendListds == null) || (this.SendListds.Tables[0].Rows.Count == 0))
                    {
                        break;
                    }
                    queryStr = "select t_np.npno,v_nptype.codename,taskid from T_NP,v_nptype Where t_np.nptype=v_nptype.code and  SendId='" + this.LVICurPrint.Text.Trim() + "' order by taskid";
                    this.SendListDetailsds = access.Run_SqlText(queryStr);
                    if ((this.SendListDetailsds == null) || (this.SendListDetailsds.Tables[0].Rows.Count == 0))
                    {
                        break;
                    }
                    PrintDocument document = new PrintDocument();
                    PrintPreviewDialog dialog = new PrintPreviewDialog();
                    this.DetailPrintPage = 0;
                    document.PrintPage += new PrintPageEventHandler(this.Pd_PrintPageSendDetails);
                    dialog.Document = document;
                    this.DetailPrintPage = 0;
                    try
                    {
                        dialog.WindowState = FormWindowState.Maximized;
                        document.PrinterSettings.PrinterName = Invoice.GetSendListPrinter();
                        if (this.cbx_PrintSendView.Checked)
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
                        MessageBox.Show("打印时发生错误" + '\n' + exception.Message, "打印送货单名细", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    FrmPrintSendList list = new FrmPrintSendList {
                        IsPreview = this.cbx_PrintSendView.Checked
                    };
                    DataSet set = access.Run_SqlText("select Code,CodeName from V_Depart");
                    list.combPlanDepart.DataSource = set.Tables[0].DefaultView;
                    list.combPlanDepart.DisplayMember = "CodeName";
                    list.combPlanDepart.ValueMember = "CodeName";
                    list.combPlanDepart.Refresh();
                    set = access.Run_SqlText("select Code,CodeName from V_nptype");
                    DataGridViewComboBoxColumn column = (DataGridViewComboBoxColumn) list.GridVwSendInfo.Columns[1];
                    column.DataSource = set.Tables[0].DefaultView;
                    column.DisplayMember = "codename";
                    column.ValueMember = "codename";
                    list.labelMakeperson.Text = "制表人 " + this.SendListds.Tables[0].Rows[0]["truename"].ToString();
                    list.labelMakeTime.Text = "制表时间 " + this.SendListds.Tables[0].Rows[0]["sendtime"].ToString();
                    list.textPlanID.Text = this.SendListds.Tables[0].Rows[0]["planid"].ToString();
                    list.combPlanDepart.Text = this.SendListds.Tables[0].Rows[0]["plandepart"].ToString();
                    list.textSendID.Text = this.SendListds.Tables[0].Rows[0]["sendid"].ToString();
                    queryStr = "select * from V_List Where SendId='" + this.LVICurPrint.Text.Trim() + "'";
                    DataSet set2 = access.Run_SqlText(queryStr);
                    if ((set2 == null) || (set2.Tables[0].Rows.Count == 0))
                    {
                        break;
                    }
                    int num = 0;
                    for (int i = 0; i < set2.Tables[0].Rows.Count; i++)
                    {
                        list.GridVwSendInfo.Rows.Add();
                        list.GridVwSendInfo.Rows[list.GridVwSendInfo.NewRowIndex - 1].Cells[0].Value = Convert.ToString((int) (i + 1));
                        DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell) list.GridVwSendInfo.Rows[list.GridVwSendInfo.NewRowIndex - 1].Cells[1];
                        cell.Value = set2.Tables[0].Rows[i]["codename"].ToString();
                        list.GridVwSendInfo.Rows[list.GridVwSendInfo.NewRowIndex - 1].Cells[2].Value = set2.Tables[0].Rows[i]["totalnum"].ToString();
                        num += int.Parse(set2.Tables[0].Rows[i]["totalnum"].ToString());
                    }
                    list.labelTotal.Text = "总计" + num.ToString();
                    list.ShowDialog();
                }
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            DataSet finishTask = new Task().GetFinishTask();
            this.Lvw_Task.Items.Clear();
            for (int i = 0; i < finishTask.Tables[0].Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem {
                    Checked = this.cbx_Select.Checked,
                    ImageKey = "Task",
                    Text = finishTask.Tables[0].Rows[i]["TaskId"].ToString().Trim()
                };
                item.SubItems.Add(finishTask.Tables[0].Rows[i]["TaskTime"].ToString());
                item.SubItems.Add(finishTask.Tables[0].Rows[i]["PlanDepart"].ToString());
                item.SubItems.Add(finishTask.Tables[0].Rows[i]["NpNum"].ToString());
                item.SubItems.Add(finishTask.Tables[0].Rows[i]["TaskUser"].ToString());
                item.SubItems.Add(finishTask.Tables[0].Rows[i]["PackTime"].ToString());
                item.SubItems.Add(finishTask.Tables[0].Rows[i]["PackU"].ToString());
                this.Lvw_Task.Items.Add(item);
            }
            finishTask = new Invoice().GetSendList();
            this.Lvw_SendList.Items.Clear();
            for (int j = 0; j < finishTask.Tables[0].Rows.Count; j++)
            {
                ListViewItem item2 = new ListViewItem {
                    ImageKey = "Send",
                    Text = finishTask.Tables[0].Rows[j]["SendId"].ToString().Trim()
                };
                item2.SubItems.Add(finishTask.Tables[0].Rows[j]["SendTime"].ToString());
                item2.SubItems.Add(finishTask.Tables[0].Rows[j]["SendUser"].ToString());
                item2.SubItems.Add(finishTask.Tables[0].Rows[j]["PlanId"].ToString());
                this.Lvw_SendList.Items.Add(item2);
            }
        }

        private void btnNullPrint_Click(object sender, EventArgs e)
        {
            FrmPrintSendList list = new FrmPrintSendList();
            Sql2KDataAccess access = new Sql2KDataAccess();
            DataSet set = access.Run_SqlText("select Code,CodeName from V_Depart");
            list.combPlanDepart.DataSource = set.Tables[0].DefaultView;
            list.combPlanDepart.DisplayMember = "CodeName";
            list.combPlanDepart.ValueMember = "CodeName";
            list.combPlanDepart.Refresh();
            set = access.Run_SqlText("select Code,CodeName from V_nptype");
            DataGridViewComboBoxColumn column = (DataGridViewComboBoxColumn) list.GridVwSendInfo.Columns[1];
            column.DataSource = set.Tables[0].DefaultView;
            column.DisplayMember = "codename";
            column.ValueMember = "codename";
            list.IsPreview = this.cbx_PrintSendView.Checked;
            list.ShowDialog();
        }

        private void cbx_Select_CheckStateChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.Lvw_Task.Items)
            {
                item.Checked = this.cbx_Select.Checked;
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

        private void Frm_Out_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_Instance = null;
        }

        private void Frm_Out_Load(object sender, EventArgs e)
        {
            base.Size = base.Parent.ClientSize;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Frm_Out));
            this.splitContainer1 = new SplitContainer();
            this.groupBox1 = new GroupBox();
            this.Lvw_Task = new SortedListView();
            this.columnHeader21 = new ColumnHeader();
            this.columnHeader22 = new ColumnHeader();
            this.columnHeader15 = new ColumnHeader();
            this.columnHeader12 = new ColumnHeader();
            this.columnHeader23 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.columnHeader5 = new ColumnHeader();
            this.ilt_Icon = new ImageList(this.components);
            this.groupBox2 = new GroupBox();
            this.Lvw_TaskNp = new SortedListView();
            this.columnHeader6 = new ColumnHeader();
            this.columnHeader7 = new ColumnHeader();
            this.columnHeader8 = new ColumnHeader();
            this.columnHeader9 = new ColumnHeader();
            this.columnHeader10 = new ColumnHeader();
            this.columnHeader13 = new ColumnHeader();
            this.columnHeader14 = new ColumnHeader();
            this.groupBox4 = new GroupBox();
            this.Lvw_SendList = new SortedListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader11 = new ColumnHeader();
            this.groupBox3 = new GroupBox();
            this.btnNullPrint = new Button();
            this.cbx_PrintSendView = new CheckBox();
            this.btn_Refresh = new Button();
            this.btn_Print = new Button();
            this.btn_Out = new Button();
            this.cbx_Select = new CheckBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            base.SuspendLayout();
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Padding = new Padding(8);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Padding = new Padding(8);
            this.splitContainer1.Size = new Size(740, 0x1be);
            this.splitContainer1.SplitterDistance = 0x213;
            this.splitContainer1.TabIndex = 0;
            this.groupBox1.Controls.Add(this.Lvw_Task);
            this.groupBox1.Dock = DockStyle.Fill;
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x203, 0xbc);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "已制作完成的生产任务单";
            this.Lvw_Task.CheckBoxes = true;
            this.Lvw_Task.Columns.AddRange(new ColumnHeader[] { this.columnHeader21, this.columnHeader22, this.columnHeader15, this.columnHeader12, this.columnHeader23, this.columnHeader4, this.columnHeader5 });
            this.Lvw_Task.Dock = DockStyle.Fill;
            this.Lvw_Task.FullRowSelect = true;
            this.Lvw_Task.Location = new Point(3, 0x11);
            this.Lvw_Task.Name = "Lvw_Task";
            this.Lvw_Task.Order = SortOrder.Descending;
            this.Lvw_Task.Size = new Size(0x1fd, 0xa8);
            this.Lvw_Task.SmallImageList = this.ilt_Icon;
            this.Lvw_Task.SortColumn = 0;
            this.Lvw_Task.TabIndex = 1;
            this.Lvw_Task.UseCompatibleStateImageBehavior = false;
            this.Lvw_Task.View = View.Details;
            this.Lvw_Task.ItemCheck += new ItemCheckEventHandler(this.Lvw_Task_ItemCheck);
            this.Lvw_Task.Click += new EventHandler(this.Lvw_Task_Click);
            this.columnHeader21.Text = "生产任务单号";
            this.columnHeader21.Width = 130;
            this.columnHeader22.Text = "下达时间";
            this.columnHeader22.Width = 130;
            this.columnHeader15.Text = "计划单位";
            this.columnHeader15.Width = 120;
            this.columnHeader12.Text = "车牌总数";
            this.columnHeader12.Width = 80;
            this.columnHeader23.Text = "下达人员";
            this.columnHeader23.Width = 80;
            this.columnHeader4.Text = "装箱时间";
            this.columnHeader4.Width = 130;
            this.columnHeader5.Text = "装箱人员";
            this.columnHeader5.Width = 80;
            this.ilt_Icon.ImageStream = (ImageListStreamer) manager.GetObject("ilt_Icon.ImageStream");
            this.ilt_Icon.TransparentColor = Color.Transparent;
            this.ilt_Icon.Images.SetKeyName(0, "Ok");
            this.ilt_Icon.Images.SetKeyName(1, "NotPass");
            this.ilt_Icon.Images.SetKeyName(2, "NP");
            this.ilt_Icon.Images.SetKeyName(3, "Plan");
            this.ilt_Icon.Images.SetKeyName(4, "Send");
            this.ilt_Icon.Images.SetKeyName(5, "Task");
            this.groupBox2.Controls.Add(this.Lvw_TaskNp);
            this.groupBox2.Dock = DockStyle.Bottom;
            this.groupBox2.Location = new Point(8, 0xc4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x203, 0xf2);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "任务单内的车牌信息";
            this.Lvw_TaskNp.Columns.AddRange(new ColumnHeader[] { this.columnHeader6, this.columnHeader7, this.columnHeader8, this.columnHeader9, this.columnHeader10, this.columnHeader13, this.columnHeader14 });
            this.Lvw_TaskNp.Dock = DockStyle.Fill;
            this.Lvw_TaskNp.FullRowSelect = true;
            this.Lvw_TaskNp.Location = new Point(3, 0x11);
            this.Lvw_TaskNp.Name = "Lvw_TaskNp";
            this.Lvw_TaskNp.Order = SortOrder.Descending;
            this.Lvw_TaskNp.Size = new Size(0x1fd, 0xde);
            this.Lvw_TaskNp.SmallImageList = this.ilt_Icon;
            this.Lvw_TaskNp.SortColumn = 0;
            this.Lvw_TaskNp.TabIndex = 2;
            this.Lvw_TaskNp.UseCompatibleStateImageBehavior = false;
            this.Lvw_TaskNp.View = View.Details;
            this.columnHeader6.Text = "车牌号码";
            this.columnHeader6.Width = 0x7e;
            this.columnHeader7.Text = "车牌种类";
            this.columnHeader7.Width = 120;
            this.columnHeader8.Text = "下达时间";
            this.columnHeader8.Width = 0x87;
            this.columnHeader9.Text = "最后完成期限";
            this.columnHeader9.Width = 130;
            this.columnHeader10.Text = "计划单号";
            this.columnHeader10.Width = 0x77;
            this.columnHeader13.Text = "前片";
            this.columnHeader14.Text = "后片";
            this.groupBox4.Controls.Add(this.Lvw_SendList);
            this.groupBox4.Dock = DockStyle.Fill;
            this.groupBox4.Location = new Point(8, 0xd8);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0xbd, 0xde);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "已生成送货单";
            this.Lvw_SendList.CheckBoxes = true;
            this.Lvw_SendList.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3, this.columnHeader11 });
            this.Lvw_SendList.Dock = DockStyle.Fill;
            this.Lvw_SendList.FullRowSelect = true;
            this.Lvw_SendList.Location = new Point(3, 0x11);
            this.Lvw_SendList.Name = "Lvw_SendList";
            this.Lvw_SendList.Order = SortOrder.Descending;
            this.Lvw_SendList.Size = new Size(0xb7, 0xca);
            this.Lvw_SendList.SmallImageList = this.ilt_Icon;
            this.Lvw_SendList.SortColumn = 0;
            this.Lvw_SendList.TabIndex = 1;
            this.Lvw_SendList.UseCompatibleStateImageBehavior = false;
            this.Lvw_SendList.View = View.Details;
            this.columnHeader1.Text = "生产任务单号";
            this.columnHeader1.Width = 0x62;
            this.columnHeader2.Text = "下达时间";
            this.columnHeader2.Width = 130;
            this.columnHeader3.Text = "下达人员";
            this.columnHeader3.Width = 80;
            this.columnHeader11.Text = "计划指令单号";
            this.columnHeader11.Width = 100;
            this.groupBox3.Controls.Add(this.btnNullPrint);
            this.groupBox3.Controls.Add(this.cbx_PrintSendView);
            this.groupBox3.Controls.Add(this.btn_Refresh);
            this.groupBox3.Controls.Add(this.btn_Print);
            this.groupBox3.Controls.Add(this.btn_Out);
            this.groupBox3.Controls.Add(this.cbx_Select);
            this.groupBox3.Dock = DockStyle.Top;
            this.groupBox3.Location = new Point(8, 8);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0xbd, 0xd0);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "出库操作";
            this.btnNullPrint.Location = new Point(20, 0x79);
            this.btnNullPrint.Name = "btnNullPrint";
            this.btnNullPrint.Size = new Size(0x4b, 0x17);
            this.btnNullPrint.TabIndex = 13;
            this.btnNullPrint.Text = "手工打印";
            this.btnNullPrint.UseVisualStyleBackColor = true;
            this.btnNullPrint.Click += new EventHandler(this.btnNullPrint_Click);
            this.cbx_PrintSendView.AutoSize = true;
            this.cbx_PrintSendView.Checked = true;
            this.cbx_PrintSendView.CheckState = CheckState.Checked;
            this.cbx_PrintSendView.Location = new Point(0x15, 0xb0);
            this.cbx_PrintSendView.Name = "cbx_PrintSendView";
            this.cbx_PrintSendView.Size = new Size(0x48, 0x10);
            this.cbx_PrintSendView.TabIndex = 12;
            this.cbx_PrintSendView.Text = "打印预览";
            this.cbx_PrintSendView.UseVisualStyleBackColor = true;
            this.btn_Refresh.Location = new Point(20, 0x22);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new Size(0x4b, 0x17);
            this.btn_Refresh.TabIndex = 11;
            this.btn_Refresh.Text = "刷新";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new EventHandler(this.btn_Refresh_Click);
            this.btn_Print.Location = new Point(20, 0x93);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new Size(0x4b, 0x17);
            this.btn_Print.TabIndex = 10;
            this.btn_Print.Text = "打印送货单";
            this.btn_Print.UseVisualStyleBackColor = true;
            this.btn_Print.Click += new EventHandler(this.btn_Print_Click);
            this.btn_Out.ForeColor = Color.ForestGreen;
            this.btn_Out.Location = new Point(0x15, 0x55);
            this.btn_Out.Name = "btn_Out";
            this.btn_Out.Size = new Size(0x4b, 0x17);
            this.btn_Out.TabIndex = 9;
            this.btn_Out.Text = "出库";
            this.btn_Out.UseVisualStyleBackColor = true;
            this.btn_Out.Click += new EventHandler(this.btn_Out_Click);
            this.cbx_Select.AutoSize = true;
            this.cbx_Select.Checked = true;
            this.cbx_Select.CheckState = CheckState.Checked;
            this.cbx_Select.Location = new Point(0x15, 0x3f);
            this.cbx_Select.Name = "cbx_Select";
            this.cbx_Select.Size = new Size(0x4e, 0x10);
            this.cbx_Select.TabIndex = 8;
            this.cbx_Select.Text = "全选/不选";
            this.cbx_Select.UseVisualStyleBackColor = true;
            this.cbx_Select.CheckStateChanged += new EventHandler(this.cbx_Select_CheckStateChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(740, 0x1be);
            base.Controls.Add(this.splitContainer1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "Frm_Out";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "出库管理";
            base.FormClosed += new FormClosedEventHandler(this.Frm_Out_FormClosed);
            base.Load += new EventHandler(this.Frm_Out_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            base.ResumeLayout(false);
        }

        private void Lvw_Task_Click(object sender, EventArgs e)
        {
            try
            {
                Task task = new Task();
                ListView view = (ListView) sender;
                DataSet npByTaskId = task.GetNpByTaskId(view.SelectedItems[0].Text.Trim());
                this.Lvw_TaskNp.Items.Clear();
                for (int i = 0; i < npByTaskId.Tables[0].Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem {
                        ImageKey = "NP",
                        Text = npByTaskId.Tables[0].Rows[i]["NpNo"].ToString()
                    };
                    item.SubItems.Add(npByTaskId.Tables[0].Rows[i]["CodeName"].ToString());
                    item.SubItems.Add(npByTaskId.Tables[0].Rows[i]["PlanTime"].ToString());
                    item.SubItems.Add(npByTaskId.Tables[0].Rows[i]["Deadline"].ToString());
                    item.SubItems.Add(npByTaskId.Tables[0].Rows[i]["PlanId"].ToString());
                    item.SubItems.Add(npByTaskId.Tables[0].Rows[i]["IsFront"].ToString());
                    item.SubItems.Add(npByTaskId.Tables[0].Rows[i]["IsBack"].ToString());
                    this.Lvw_TaskNp.Items.Add(item);
                }
            }
            catch (Exception)
            {
            }
        }

        private void Lvw_Task_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (((e.NewValue == CheckState.Checked) && (this.Lvw_SendList.Items.Count > 0)) && (this.Lvw_SendList.SelectedItems.Count == 0))
            {
                this.Lvw_SendList.Items[e.Index].Selected = true;
            }
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
            e.Graphics.DrawString(s, font, Brushes.Black, new PointF(300f, 60f));
            e.Graphics.DrawString(str2, font2, Brushes.Black, new PointF(330f, 100f));
            Sql2KDataAccess access = new Sql2KDataAccess();
            string queryStr = "select * from V_SendList Where SendId='" + this.LVICurPrint.Text.Trim() + "'";
            DataSet set = access.Run_SqlText(queryStr);
            if ((set != null) && (set.Tables[0].Rows.Count != 0))
            {
                e.Graphics.DrawString("计划单号 " + set.Tables[0].Rows[0]["planid"].ToString(), font5, Brushes.Black, new PointF(70f, 130f));
                e.Graphics.DrawString("计划单位 " + set.Tables[0].Rows[0]["plandepart"].ToString(), font5, Brushes.Black, new PointF(300f, 130f));
                e.Graphics.DrawString("送货单号 " + set.Tables[0].Rows[0]["sendid"].ToString(), font5, Brushes.Black, new PointF(550f, 130f));
                e.Graphics.DrawString("制表人 " + set.Tables[0].Rows[0]["truename"].ToString(), font5, Brushes.Black, new PointF(70f, 420f));
                e.Graphics.DrawString("制表时间 " + set.Tables[0].Rows[0]["sendtime"].ToString(), font5, Brushes.Black, new PointF(300f, 420f));
                e.Graphics.DrawString("送货人", font5, Brushes.Black, new PointF(70f, 450f));
                e.Graphics.DrawString("签收人", font5, Brushes.Black, new PointF(300f, 450f));
                e.Graphics.DrawString("签收单位", font5, Brushes.Black, new PointF(550f, 450f));
                Pen pen = new Pen(Brushes.Black) {
                    Width = 1.2f
                };
                int num = 0x19;
                int num2 = 160;
                e.Graphics.DrawLine(pen, 70, num2, 770, num2);
                e.Graphics.DrawString("序号", font4, Brushes.Black, new PointF(150f, (float) (num2 + 3)));
                e.Graphics.DrawString("机动车号牌类型", font4, Brushes.Black, new PointF(300f, (float) (num2 + 3)));
                e.Graphics.DrawString("数量", font4, Brushes.Black, new PointF(550f, (float) (num2 + 3)));
                queryStr = "select * from V_List Where SendId='" + this.LVICurPrint.Text.Trim() + "'";
                DataSet set2 = access.Run_SqlText(queryStr);
                if ((set2 != null) && (set2.Tables[0].Rows.Count != 0))
                {
                    int num3 = 0;
                    for (int i = 0; i < set2.Tables[0].Rows.Count; i++)
                    {
                        num2 = 190 + (i * num);
                        e.Graphics.DrawLine(pen, 70, num2, 770, num2);
                        e.Graphics.DrawString(Convert.ToString((int) (i + 1)), font3, Brushes.Black, new PointF(150f, (float) (num2 + 3)));
                        e.Graphics.DrawString(set2.Tables[0].Rows[i]["codename"].ToString(), font3, Brushes.Black, new PointF(300f, (float) (num2 + 3)));
                        e.Graphics.DrawString(set2.Tables[0].Rows[i]["totalnum"].ToString(), font3, Brushes.Black, new PointF(550f, (float) (num2 + 3)));
                        num3 += int.Parse(set2.Tables[0].Rows[i]["totalnum"].ToString());
                    }
                    num2 += num;
                    e.Graphics.DrawLine(pen, 70, num2, 770, num2);
                    e.Graphics.DrawString("总计", font4, Brushes.Black, new PointF(300f, (float) (num2 + 3)));
                    e.Graphics.DrawString(num3.ToString(), font4, Brushes.Black, new PointF(550f, (float) (num2 + 3)));
                    e.Graphics.DrawLine(pen, 70, num2 + num, 770, num2 + num);
                    pen.Dispose();
                }
            }
        }

        private void Pd_PrintPageSendDetails(object sender, PrintPageEventArgs e)
        {
            this.DetailPrintPage++;
            this.DetailPrintPage = this.DetailPrintPage % ((this.SendListDetailsds.Tables[0].Rows.Count + 0x31) / 50);
            if (this.DetailPrintPage == 0)
            {
                this.DetailPrintPage = (this.SendListDetailsds.Tables[0].Rows.Count + 0x31) / 50;
            }
            new Font("隶书", 7.5f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            string s = "机动车号牌送货单明细";
            string str2 = "瑞华交通工程有限公司";
            Font font = new Font("黑体", 20f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            Font font2 = new Font("黑体", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            Font font3 = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 0x86);
            Font font4 = new Font("宋体", 12f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            Font font5 = new Font("宋体", 10f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            e.Graphics.DrawString(s, font, Brushes.Black, new PointF(270f, 90f));
            e.Graphics.DrawString(str2, font2, Brushes.Black, new PointF(330f, 60f));
            e.Graphics.DrawString("计划单号 " + this.SendListds.Tables[0].Rows[0]["planid"].ToString(), font5, Brushes.Black, new PointF(70f, 140f));
            e.Graphics.DrawString("计划单位 " + this.SendListds.Tables[0].Rows[0]["plandepart"].ToString(), font5, Brushes.Black, new PointF(300f, 140f));
            e.Graphics.DrawString("送货单号 " + this.SendListds.Tables[0].Rows[0]["sendid"].ToString(), font5, Brushes.Black, new PointF(550f, 140f));
            e.Graphics.DrawString("制表人 " + this.SendListds.Tables[0].Rows[0]["truename"].ToString(), font5, Brushes.Black, new PointF(300f, 1040f));
            e.Graphics.DrawString("制表时间 " + this.SendListds.Tables[0].Rows[0]["sendtime"].ToString(), font5, Brushes.Black, new PointF(550f, 1040f));
            Pen pen = new Pen(Brushes.Black) {
                Width = 1f
            };
            int num = 0x21;
            int num2 = 170;
            e.Graphics.DrawLine(pen, 70, num2, 770, num2);
            num2 += 12;
            e.Graphics.DrawString("序号", font4, Brushes.Black, new PointF(72f, (float) num2));
            e.Graphics.DrawString("号牌类型", font4, Brushes.Black, new PointF(112f, (float) num2));
            e.Graphics.DrawString("车牌号", font4, Brushes.Black, new PointF(220f, (float) num2));
            e.Graphics.DrawString("箱号", font4, Brushes.Black, new PointF(300f, (float) num2));
            e.Graphics.DrawString("序号", font4, Brushes.Black, new PointF(427f, (float) num2));
            e.Graphics.DrawString("号牌类型", font4, Brushes.Black, new PointF(466f, (float) num2));
            e.Graphics.DrawString("车牌号", font4, Brushes.Black, new PointF(574f, (float) num2));
            e.Graphics.DrawString("箱号", font4, Brushes.Black, new PointF(654f, (float) num2));
            for (int i = (this.DetailPrintPage - 1) * 50; i < (((this.DetailPrintPage - 1) * 50) + 0x19); i++)
            {
                num2 = 0xcd + ((i - ((this.DetailPrintPage - 1) * 50)) * num);
                e.Graphics.DrawLine(pen, 70, num2, 770, num2);
                num2 += 10;
                if (i < this.SendListDetailsds.Tables[0].Rows.Count)
                {
                    int num4 = i + 1;
                    e.Graphics.DrawString(num4.ToString(), font3, Brushes.Black, new PointF(72f, (float) num2));
                    e.Graphics.DrawString(this.SendListDetailsds.Tables[0].Rows[i]["codename"].ToString(), font3, Brushes.Black, new PointF(112f, (float) num2));
                    e.Graphics.DrawString(this.SendListDetailsds.Tables[0].Rows[i]["NPNo"].ToString(), font3, Brushes.Black, new PointF(220f, (float) num2));
                    e.Graphics.DrawString(this.SendListDetailsds.Tables[0].Rows[i]["taskid"].ToString(), font3, Brushes.Black, new PointF(300f, (float) num2));
                }
                if ((i + 0x19) < this.SendListDetailsds.Tables[0].Rows.Count)
                {
                    int num5 = i + 0x1a;
                    e.Graphics.DrawString(num5.ToString(), font3, Brushes.Black, new PointF(427f, (float) num2));
                    e.Graphics.DrawString(this.SendListDetailsds.Tables[0].Rows[i + 0x19]["codename"].ToString(), font3, Brushes.Black, new PointF(466f, (float) num2));
                    e.Graphics.DrawString(this.SendListDetailsds.Tables[0].Rows[i]["NPNo"].ToString(), font3, Brushes.Black, new PointF(574f, (float) num2));
                    e.Graphics.DrawString(this.SendListDetailsds.Tables[0].Rows[i]["taskid"].ToString(), font3, Brushes.Black, new PointF(654f, (float) num2));
                }
            }
            num2 += num;
            e.Graphics.DrawLine(pen, 70, num2, 770, num2);
            e.Graphics.DrawLine(pen, 110, 170, 110, num2);
            e.Graphics.DrawLine(pen, 0xda, 170, 0xda, num2);
            e.Graphics.DrawLine(pen, 0x12a, 170, 0x12a, num2);
            e.Graphics.DrawLine(pen, 0x1a5, 170, 0x1a5, num2);
            e.Graphics.DrawLine(pen, 0x1d1, 170, 0x1d1, num2);
            e.Graphics.DrawLine(pen, 0x23d, 170, 0x23d, num2);
            e.Graphics.DrawLine(pen, 0x28c, 170, 0x28c, num2);
            e.Graphics.DrawString(string.Concat(new object[] { "第", this.DetailPrintPage, "页 共", Convert.ToString((int) ((this.SendListDetailsds.Tables[0].Rows.Count + 0x31) / 50)), "页" }), font3, Brushes.Black, new PointF(300f, 1100f));
            pen.Dispose();
            if (this.DetailPrintPage < ((this.SendListDetailsds.Tables[0].Rows.Count + 0x31) / 50))
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }

        public static Frm_Out Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Frm_Out();
                }
                return m_Instance;
            }
        }
    }
}

