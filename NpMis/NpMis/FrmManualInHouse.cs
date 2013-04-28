namespace NpMis
{
    using Common;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class FrmManualInHouse : Form
    {
        private Button btnExit;
        private Button btnInHouse;
        private CheckBox cbx_Select;
        private IContainer components;
        private ColumnHeader HandlePerson;
        private Label labelInfo;
        private ListView listVwTaskID;
        private ColumnHeader Operator;
        private ColumnHeader OperTime;
        private ColumnHeader TaskID;

        public FrmManualInHouse()
        {
            this.InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Dispose();
        }

        private void btnInHouse_Click(object sender, EventArgs e)
        {
            if (this.listVwTaskID.Items.Count == 0)
            {
                MessageBox.Show("没有需要入库的箱！");
                base.Dispose();
            }
            if ((this.listVwTaskID.CheckedItems.Count == 0) && (this.listVwTaskID.Items.Count > 0))
            {
                MessageBox.Show("请先选择要入库的箱号！");
            }
            else
            {
                try
                {
                    for (int i = 0; i < this.listVwTaskID.Items.Count; i++)
                    {
                        if (this.listVwTaskID.Items[i].Checked)
                        {
                            this.InsertToDBWarehouse(this.listVwTaskID.Items[i].Text, "入库", DateTime.Now, User.UserName, User.UserName);
                        }
                    }
                    this.listVwTaskID.Items.Clear();
                    this.LoadBoxToInHouse();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("手工入库失败！原因 " + exception.Message);
                }
                MessageBox.Show("手工入库成功！");
            }
        }

        private void cbx_Select_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listVwTaskID.Items.Count; i++)
            {
                this.listVwTaskID.Items[i].Checked = this.cbx_Select.Checked;
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

        private void FrmManualInHouse_Load(object sender, EventArgs e)
        {
            this.LoadBoxToInHouse();
        }

        private void InitializeComponent()
        {
            this.cbx_Select = new CheckBox();
            this.listVwTaskID = new ListView();
            this.TaskID = new ColumnHeader();
            this.OperTime = new ColumnHeader();
            this.HandlePerson = new ColumnHeader();
            this.Operator = new ColumnHeader();
            this.btnInHouse = new Button();
            this.btnExit = new Button();
            this.labelInfo = new Label();
            base.SuspendLayout();
            this.cbx_Select.AutoSize = true;
            this.cbx_Select.Checked = true;
            this.cbx_Select.CheckState = CheckState.Checked;
            this.cbx_Select.Location = new Point(0x33, 340);
            this.cbx_Select.Name = "cbx_Select";
            this.cbx_Select.Size = new Size(0x4e, 0x10);
            this.cbx_Select.TabIndex = 11;
            this.cbx_Select.Text = "全选/不选";
            this.cbx_Select.UseVisualStyleBackColor = true;
            this.cbx_Select.CheckedChanged += new EventHandler(this.cbx_Select_CheckedChanged);
            this.listVwTaskID.CheckBoxes = true;
            this.listVwTaskID.Columns.AddRange(new ColumnHeader[] { this.TaskID, this.OperTime, this.HandlePerson, this.Operator });
            this.listVwTaskID.Location = new Point(0x33, 0x13);
            this.listVwTaskID.Name = "listVwTaskID";
            this.listVwTaskID.Size = new Size(0x1bc, 0x125);
            this.listVwTaskID.TabIndex = 10;
            this.listVwTaskID.UseCompatibleStateImageBehavior = false;
            this.listVwTaskID.View = View.Details;
            this.listVwTaskID.ItemCheck += new ItemCheckEventHandler(this.listVwTaskID_ItemCheck);
            this.TaskID.Text = "箱号";
            this.TaskID.Width = 140;
            this.OperTime.Text = "出库时间";
            this.OperTime.Width = 140;
            this.HandlePerson.Text = "领出人";
            this.HandlePerson.Width = 80;
            this.Operator.Text = "操作人";
            this.Operator.Width = 80;
            this.btnInHouse.Location = new Point(0x11e, 0x14d);
            this.btnInHouse.Name = "btnInHouse";
            this.btnInHouse.Size = new Size(0x54, 0x1c);
            this.btnInHouse.TabIndex = 12;
            this.btnInHouse.Text = "入库";
            this.btnInHouse.UseVisualStyleBackColor = true;
            this.btnInHouse.Click += new EventHandler(this.btnInHouse_Click);
            this.btnExit.Location = new Point(0x19b, 0x14d);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(0x54, 0x1c);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new Point(0x87, 0x157);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new Size(0x11, 12);
            this.labelInfo.TabIndex = 13;
            this.labelInfo.Text = "共";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x231, 0x187);
            base.Controls.Add(this.labelInfo);
            base.Controls.Add(this.btnExit);
            base.Controls.Add(this.btnInHouse);
            base.Controls.Add(this.cbx_Select);
            base.Controls.Add(this.listVwTaskID);
            base.Name = "FrmManualInHouse";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "手工入库";
            base.Load += new EventHandler(this.FrmManualInHouse_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public bool InsertToDBWarehouse(string TaskID, string OperateType, DateTime OperateTime, string HandlePerson, string OperationPerson)
        {
            string str;
            bool flag = false;
            if (((TaskID.Trim().Length < 1) || (OperateType.Trim().Length < 1)) || (OperationPerson.Trim().Length < 1))
            {
                return false;
            }
            if (TaskID.Substring(0, 1).CompareTo("T") == 0)
            {
                if (OperateType.CompareTo("入库") == 0)
                {
                    str = "update T_Task set IsInWarehouse=1 where taskid='" + TaskID + "'";
                }
                else
                {
                    str = "update T_Task set IsInWarehouse=0 where taskid='" + TaskID + "'";
                }
            }
            else if (OperateType.CompareTo("入库") == 0)
            {
                str = "update T_PanTaskInfo set IsInWarehouse=1 where Pantaskid='" + TaskID + "'";
            }
            else
            {
                str = "update T_PanTaskInfo set IsInWarehouse=0 where Pantaskid='" + TaskID + "'";
            }
            Sql2KDataAccess access = new Sql2KDataAccess();
            DataSet set = new DataSet();
            set = access.Run_SqlText(str);
            str = string.Concat(new object[] { "insert into T_WarehouseOperation(TaskID , OperateType , OperateTime ,HandlePerson , OperatePerson ) values('", TaskID, "','", OperateType, "','", OperateTime, "','", HandlePerson, "','", OperationPerson, "')" });
            if (access.Run_SqlText(str) != null)
            {
                flag = true;
            }
            access = null;
            return flag;
        }

        private void listVwTaskID_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                this.labelInfo.Text = string.Concat(new object[] { "已选中", Convert.ToString((int) (this.listVwTaskID.CheckedItems.Count + 1)), "箱  共", this.listVwTaskID.Items.Count, "箱" });
            }
            else
            {
                this.labelInfo.Text = string.Concat(new object[] { "已选中", Convert.ToString((int) (this.listVwTaskID.CheckedItems.Count - 1)), "箱  共", this.listVwTaskID.Items.Count, "箱" });
            }
        }

        private void LoadBoxToInHouse()
        {
            string s = DateTime.Today.ToString();
            DateTime time = new DateTime();
            time = DateTime.Parse(s);
            Sql2KDataAccess access = new Sql2KDataAccess();
            DataSet set = access.Run_SqlText("select * from d_code where codetype=8 and code='WorkEndTime'");
            try
            {
                DateTime time2 = DateTime.Parse(set.Tables[0].Rows[0]["Codename"].ToString());
                time = time.AddHours((double) time2.Hour);
                time = time.AddMinutes((double) time2.Minute);
            }
            catch
            {
                time = time.AddHours(17.0).AddMinutes(30.0);
                string queryStr = "insert into d_code(code,codetype,codename,description) values('WorkEndTime',8,'" + time.ToLongTimeString() + "','下班时间')";
                access.Run_SqlText(queryStr);
            }
            s = string.Concat(new object[] { "SELECT w1.TaskID, w1.OperateTime, u1.TrueName AS name1,  u2.TrueName AS name2  FROM T_WarehouseOperation w1 INNER JOIN  T_User u1 ON w1.HandlePerson = u1.UserName INNER JOIN  T_User u2 ON w1.OperatePerson = u2.UserName INNER JOIN  T_task  ON w1.taskid = t_task.taskid  WHERE t_task.sendtime is null and (DATEDIFF([day], w1.OperateTime,'", time, "') > 0 OR  DATEDIFF([minute], GETDATE(),'", time, "') < 0)AND (w1.OperateType = '出库')   AND (w1.OperateTime IN  (SELECT MAX(w2.OperateTime)  FROM T_WarehouseOperation w2  WHERE w1.taskid = w2.taskid)) order by w1.taskid " });
            set = access.Run_SqlText(s);
            if (set != null)
            {
                this.listVwTaskID.Items.Clear();
                for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem {
                        Text = set.Tables[0].Rows[i]["TaskID"].ToString()
                    };
                    item.SubItems.Add(set.Tables[0].Rows[i]["OperateTime"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[i]["name1"].ToString());
                    item.SubItems.Add(set.Tables[0].Rows[i]["name2"].ToString());
                    item.Checked = true;
                    this.listVwTaskID.Items.Add(item);
                }
            }
            s = string.Concat(new object[] { "SELECT w1.TaskID, w1.OperateTime, u1.TrueName AS name1,  u2.TrueName AS name2  FROM T_WarehouseOperation w1 INNER JOIN  T_User u1 ON w1.HandlePerson = u1.UserName INNER JOIN  T_User u2 ON w1.OperatePerson = u2.UserName INNER JOIN  T_PantaskInfo  ON w1.taskid = t_PantaskInfo.pantaskid  WHERE t_Pantaskinfo.pancount>0 and (DATEDIFF([day], w1.OperateTime,'", time, "') > 0 OR  DATEDIFF([minute], GETDATE(),'", time, "')< 0)AND (w1.OperateType = '出库')   AND (w1.OperateTime IN  (SELECT MAX(w2.OperateTime)  FROM T_WarehouseOperation w2  WHERE w1.taskid = w2.taskid)) order by w1.taskid " });
            set = new Sql2KDataAccess().Run_SqlText(s);
            if (set != null)
            {
                for (int j = 0; j < set.Tables[0].Rows.Count; j++)
                {
                    ListViewItem item2 = new ListViewItem {
                        Text = set.Tables[0].Rows[j]["TaskID"].ToString()
                    };
                    item2.SubItems.Add(set.Tables[0].Rows[j]["OperateTime"].ToString());
                    item2.SubItems.Add(set.Tables[0].Rows[j]["name1"].ToString());
                    item2.SubItems.Add(set.Tables[0].Rows[j]["name2"].ToString());
                    item2.Checked = true;
                    this.listVwTaskID.Items.Add(item2);
                }
            }
            this.cbx_Select.Checked = true;
            this.labelInfo.Text = string.Concat(new object[] { "已选中", this.listVwTaskID.Items.Count, "箱共", this.listVwTaskID.Items.Count, "箱" });
        }
    }
}

