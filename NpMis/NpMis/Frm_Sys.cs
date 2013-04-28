namespace NpMis
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class Frm_Sys : Form
    {
        private Button btn_AddUser;
        private Button btn_Cancel;
        private Button btn_Delete;
        private Button btn_Modi;
        private Button btn_ModiPwd;
        private Button btn_SaveSet;
        private Button btn_SetAuthority;
        private ComboBox cbx_SendListPrinter;
        private ComboBox cbx_SendPrinter;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private IContainer components;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private ListBox lbx_User;
        private ListView lvw_Authority;
        private SortedListView Lvw_User;
        private static Frm_Sys m_Instance;
        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TextBox tbx_BarCode;
        private TextBox tbx_BoxAmount;
        private TextBox tbx_ConfirmNewPwd;
        private TextBox tbx_ConfirmPwd;
        private TextBox tbx_Duty;
        private TextBox tbx_NewPwd;
        private TextBox tbx_OldPwd;
        private TextBox tbx_Pwd;
        private TextBox tbx_TrueName;
        private TextBox tbx_UName;
        private TextBox tbx_UserName;
        private TextBox textBox2;

        private Frm_Sys()
        {
            this.InitializeComponent();
        }

        private void btn_AddUser_Click(object sender, EventArgs e)
        {
            if (((this.tbx_UserName.Text == "") || (this.tbx_Pwd.Text == "")) || (((this.tbx_ConfirmPwd.Text == "") || (this.tbx_TrueName.Text == "")) || (this.tbx_Duty.Text == "")))
            {
                MessageBox.Show("请填写完整用户信息", "添加用户", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (this.tbx_Pwd.Text != this.tbx_ConfirmPwd.Text)
            {
                MessageBox.Show("两次密码输入不一致", "添加用户", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                User user = new User();
                if (user.AddUser(this.tbx_UserName.Text.Trim(), this.tbx_Pwd.Text.Trim(), this.tbx_TrueName.Text.Trim(), this.tbx_Duty.Text.Trim(), this.tbx_BarCode.Text.Trim()))
                {
                    MessageBox.Show("用户添加成功", "添加用户", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.tbx_UserName.Text = "";
                    this.tbx_Pwd.Text = "";
                    this.tbx_ConfirmPwd.Text = "";
                    this.tbx_TrueName.Text = "";
                    this.tbx_Duty.Text = "";
                    this.Frm_Data_Load(this, new EventArgs());
                }
                else
                {
                    MessageBox.Show("用户已存在", "添加用户", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            base.Close();
            base.Dispose();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (this.Lvw_User.SelectedItems.Count > 0)
            {
                if (this.Lvw_User.SelectedItems[0].Text.Trim().ToUpper() == "ADMIN")
                {
                    MessageBox.Show("系统管理员不能删除", "删除用户", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                User user = new User();
                if (MessageBox.Show("确定删除此用户" + this.Lvw_User.SelectedItems[0].Text.Trim() + "吗?", "删除用户", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (user.DeleteUser(this.Lvw_User.SelectedItems[0].Text.Trim()))
                    {
                        MessageBox.Show("用户删除成功", "删除用户", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("用户删除失败", "删除用户", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            this.Frm_Data_Load(this, new EventArgs());
        }

        private void btn_Modi_Click(object sender, EventArgs e)
        {
            if (((this.tbx_UserName.Text == "") || (this.tbx_Pwd.Text == "")) || (this.tbx_ConfirmPwd.Text == ""))
            {
                MessageBox.Show("请填写用户名和密码", "修改用户", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (this.tbx_Pwd.Text != this.tbx_ConfirmPwd.Text)
            {
                MessageBox.Show("两次密码输入不一致", "添加用户", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                User user = new User();
                if (user.ModiUser(this.tbx_UserName.Text.Trim(), this.tbx_Pwd.Text.Trim(), this.tbx_TrueName.Text.Trim(), this.tbx_Duty.Text.Trim(), this.tbx_BarCode.Text.Trim()))
                {
                    MessageBox.Show("用户修改成功", "修改用户", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.tbx_UserName.Text = "";
                    this.tbx_Pwd.Text = "";
                    this.tbx_ConfirmPwd.Text = "";
                    this.tbx_TrueName.Text = "";
                    this.tbx_Duty.Text = "";
                    this.Frm_Data_Load(this, new EventArgs());
                }
                else
                {
                    MessageBox.Show("用户修改失败", "修改用户", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void btn_ModiPwd_Click(object sender, EventArgs e)
        {
            if (this.tbx_OldPwd.Text.Trim() == "")
            {
                MessageBox.Show("请输入用户原密码", "修改密码", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (this.tbx_NewPwd.Text.Trim() == "")
            {
                MessageBox.Show("请输入新密码", "修改密码", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (this.tbx_NewPwd.Text.Trim() != this.tbx_ConfirmNewPwd.Text.Trim())
            {
                MessageBox.Show("两次密码输入不一致", "修改密码", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                User user = new User();
                if (user.UserLogin(User.UserName, this.tbx_OldPwd.Text.Trim()) == 1)
                {
                    if (user.ModiPwd(this.tbx_NewPwd.Text.Trim()))
                    {
                        MessageBox.Show("密码修改成功", "修改密码", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                else
                {
                    MessageBox.Show("用户原密码错误", "修改密码", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btn_SaveSet_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(this.tbx_BoxAmount.Text.Trim()) <= 0)
                {
                    MessageBox.Show("底板箱的容量必须>0", "保存设置", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("输入有误", "保存设置", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            new MbTask().SaveBoxCapability(int.Parse(this.tbx_BoxAmount.Text.Trim()));
            Invoice.SaveSendPrinter(this.cbx_SendPrinter.Text.Trim());
            Invoice.SaveSendListPrinter(this.cbx_SendListPrinter.Text.Trim());
            MessageBox.Show("保存成功", "保存设置", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void btn_SetAuthority_Click(object sender, EventArgs e)
        {
            User user = new User();
            if (this.lbx_User.SelectedItem.ToString().ToUpper() == "ADMIN")
            {
                MessageBox.Show("系统管理员无法更改其权限", "设置权限", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                foreach (ListViewItem item in this.lvw_Authority.Items)
                {
                    user.UpdateUserAuthority(this.lbx_User.SelectedItem.ToString(), item.Tag.ToString(), !item.Checked);
                }
                MessageBox.Show("权限设置成功", "设置权限", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        private void Frm_Data_Load(object sender, EventArgs e)
        {
            User user = new User();
            DataSet allUser = user.GetAllUser();
            this.Lvw_User.Items.Clear();
            this.lbx_User.Items.Clear();
            for (int i = 0; i < allUser.Tables[0].Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem {
                    Text = allUser.Tables[0].Rows[i]["UserName"].ToString()
                };
                item.SubItems.Add(allUser.Tables[0].Rows[i]["TrueName"].ToString());
                item.SubItems.Add(allUser.Tables[0].Rows[i]["Duty"].ToString());
                item.SubItems.Add(allUser.Tables[0].Rows[i]["BarCode"].ToString());
                this.Lvw_User.Items.Add(item);
                this.lbx_User.Items.Add(item.Text.Trim());
            }
            allUser = user.GetAllAuthority();
            this.lvw_Authority.Items.Clear();
            for (int j = 0; j < allUser.Tables[0].Rows.Count; j++)
            {
                ListViewItem item2 = new ListViewItem {
                    Text = allUser.Tables[0].Rows[j]["CodeName"].ToString(),
                    Tag = allUser.Tables[0].Rows[j]["Code"].ToString()
                };
                this.lvw_Authority.Items.Add(item2);
            }
            foreach (string str in PrinterSettings.InstalledPrinters)
            {
                this.cbx_SendListPrinter.Items.Add(str);
                this.cbx_SendPrinter.Items.Add(str);
            }
            this.cbx_SendPrinter.Text = Invoice.GetSendPrinter();
            this.cbx_SendListPrinter.Text = Invoice.GetSendListPrinter();
            this.tbx_UName.Text = User.UserName;
            if (!User.IsHaveAuthority("用户管理"))
            {
                this.tabControl.TabPages.Remove(this.tabPage1);
            }
            if (!User.IsHaveAuthority("权限设定"))
            {
                this.tabControl.TabPages.Remove(this.tabPage2);
            }
            if (!User.IsHaveAuthority("修改密码"))
            {
                this.tabControl.TabPages.Remove(this.tabPage3);
            }
            if (!User.IsHaveAuthority("选项设置"))
            {
                this.tabControl.TabPages.Remove(this.tabPage4);
            }
            MbTask task = new MbTask();
            this.tbx_BoxAmount.Text = task.GetBoxCapability().ToString();
        }

        private void Frm_Sys_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_Instance = null;
        }

        private void InitializeComponent()
        {
            this.tabControl = new TabControl();
            this.tabPage1 = new TabPage();
            this.btn_Delete = new Button();
            this.btn_Modi = new Button();
            this.btn_AddUser = new Button();
            this.groupBox2 = new GroupBox();
            this.Lvw_User = new SortedListView();
            this.columnHeader1 = new ColumnHeader();
            this.columnHeader2 = new ColumnHeader();
            this.columnHeader3 = new ColumnHeader();
            this.columnHeader4 = new ColumnHeader();
            this.groupBox1 = new GroupBox();
            this.tbx_BarCode = new TextBox();
            this.label6 = new Label();
            this.label5 = new Label();
            this.tbx_ConfirmPwd = new TextBox();
            this.tbx_Duty = new TextBox();
            this.label4 = new Label();
            this.tbx_TrueName = new TextBox();
            this.label3 = new Label();
            this.tbx_Pwd = new TextBox();
            this.label2 = new Label();
            this.tbx_UserName = new TextBox();
            this.label1 = new Label();
            this.tabPage2 = new TabPage();
            this.btn_SetAuthority = new Button();
            this.groupBox4 = new GroupBox();
            this.lvw_Authority = new ListView();
            this.groupBox3 = new GroupBox();
            this.lbx_User = new ListBox();
            this.tabPage3 = new TabPage();
            this.btn_ModiPwd = new Button();
            this.groupBox5 = new GroupBox();
            this.label8 = new Label();
            this.tbx_NewPwd = new TextBox();
            this.tbx_ConfirmNewPwd = new TextBox();
            this.label10 = new Label();
            this.tbx_OldPwd = new TextBox();
            this.label11 = new Label();
            this.tbx_UName = new TextBox();
            this.label12 = new Label();
            this.tabPage4 = new TabPage();
            this.btn_SaveSet = new Button();
            this.groupBox6 = new GroupBox();
            this.cbx_SendPrinter = new ComboBox();
            this.cbx_SendListPrinter = new ComboBox();
            this.label7 = new Label();
            this.textBox2 = new TextBox();
            this.label9 = new Label();
            this.label13 = new Label();
            this.tbx_BoxAmount = new TextBox();
            this.label14 = new Label();
            this.tabPage5 = new TabPage();
            this.btn_Cancel = new Button();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            base.SuspendLayout();
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage5);
            this.tabControl.Location = new Point(11, 11);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new Size(0x22b, 0x144);
            this.tabControl.TabIndex = 0;
            this.tabPage1.Controls.Add(this.btn_Delete);
            this.tabPage1.Controls.Add(this.btn_Modi);
            this.tabPage1.Controls.Add(this.btn_AddUser);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new Point(4, 0x15);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(5);
            this.tabPage1.Size = new Size(0x223, 0x12b);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "用户管理";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.btn_Delete.Location = new Point(0x1cd, 0xa6);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new Size(0x4b, 0x17);
            this.btn_Delete.TabIndex = 4;
            this.btn_Delete.Text = "删除用户";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new EventHandler(this.btn_Delete_Click);
            this.btn_Modi.Location = new Point(0x1cd, 0x3f);
            this.btn_Modi.Name = "btn_Modi";
            this.btn_Modi.Size = new Size(0x4b, 0x17);
            this.btn_Modi.TabIndex = 3;
            this.btn_Modi.Text = "修改用户";
            this.btn_Modi.UseVisualStyleBackColor = true;
            this.btn_Modi.Click += new EventHandler(this.btn_Modi_Click);
            this.btn_AddUser.Location = new Point(0x1cd, 0x1d);
            this.btn_AddUser.Name = "btn_AddUser";
            this.btn_AddUser.Size = new Size(0x4b, 0x17);
            this.btn_AddUser.TabIndex = 2;
            this.btn_AddUser.Text = "添加用户";
            this.btn_AddUser.UseVisualStyleBackColor = true;
            this.btn_AddUser.Click += new EventHandler(this.btn_AddUser_Click);
            this.groupBox2.Controls.Add(this.Lvw_User);
            this.groupBox2.Location = new Point(8, 0x9c);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(0x1b5, 0x88);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "用户信息";
            this.Lvw_User.AllowColumnReorder = true;
            this.Lvw_User.Columns.AddRange(new ColumnHeader[] { this.columnHeader1, this.columnHeader2, this.columnHeader3, this.columnHeader4 });
            this.Lvw_User.Dock = DockStyle.Fill;
            this.Lvw_User.FullRowSelect = true;
            this.Lvw_User.Location = new Point(3, 0x11);
            this.Lvw_User.Name = "Lvw_User";
            this.Lvw_User.Order = SortOrder.Descending;
            this.Lvw_User.Size = new Size(0x1af, 0x74);
            this.Lvw_User.SortColumn = 0;
            this.Lvw_User.TabIndex = 0;
            this.Lvw_User.UseCompatibleStateImageBehavior = false;
            this.Lvw_User.View = View.Details;
            this.Lvw_User.Click += new EventHandler(this.Lvw_User_Click);
            this.columnHeader1.Text = "用户名";
            this.columnHeader1.Width = 0x9a;
            this.columnHeader2.Text = "真实姓名";
            this.columnHeader2.Width = 0xa9;
            this.columnHeader3.Text = "职责";
            this.columnHeader3.Width = 0x88;
            this.columnHeader4.Text = "条码";
            this.columnHeader4.Width = 120;
            this.groupBox1.Controls.Add(this.tbx_BarCode);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tbx_ConfirmPwd);
            this.groupBox1.Controls.Add(this.tbx_Duty);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbx_TrueName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbx_Pwd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbx_UserName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x1b5, 0x8f);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "添加用户";
            this.tbx_BarCode.Location = new Point(0x106, 0x70);
            this.tbx_BarCode.Name = "tbx_BarCode";
            this.tbx_BarCode.Size = new Size(0x9a, 0x15);
            this.tbx_BarCode.TabIndex = 11;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0xdf, 0x74);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x23, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "条码:";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x13, 70);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x3b, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "重复密码:";
            this.tbx_ConfirmPwd.Location = new Point(0x53, 0x42);
            this.tbx_ConfirmPwd.Name = "tbx_ConfirmPwd";
            this.tbx_ConfirmPwd.PasswordChar = '*';
            this.tbx_ConfirmPwd.Size = new Size(0x14d, 0x15);
            this.tbx_ConfirmPwd.TabIndex = 2;
            this.tbx_Duty.Location = new Point(0x53, 0x70);
            this.tbx_Duty.Name = "tbx_Duty";
            this.tbx_Duty.Size = new Size(0x7f, 0x15);
            this.tbx_Duty.TabIndex = 4;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x13, 0x73);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x3b, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "职    责:";
            this.tbx_TrueName.Location = new Point(0x53, 0x59);
            this.tbx_TrueName.Name = "tbx_TrueName";
            this.tbx_TrueName.Size = new Size(0x14d, 0x15);
            this.tbx_TrueName.TabIndex = 3;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x13, 0x5d);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x3b, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "真实姓名:";
            this.tbx_Pwd.Location = new Point(0x53, 0x2b);
            this.tbx_Pwd.Name = "tbx_Pwd";
            this.tbx_Pwd.PasswordChar = '*';
            this.tbx_Pwd.Size = new Size(0x14d, 0x15);
            this.tbx_Pwd.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x13, 0x2e);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x3b, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "密    码:";
            this.tbx_UserName.Location = new Point(0x53, 20);
            this.tbx_UserName.Name = "tbx_UserName";
            this.tbx_UserName.Size = new Size(0x14d, 0x15);
            this.tbx_UserName.TabIndex = 0;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x13, 0x17);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3b, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用 户 名:";
            this.tabPage2.Controls.Add(this.btn_SetAuthority);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new Point(4, 0x15);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(0x223, 0x12b);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "权限设定";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.btn_SetAuthority.Location = new Point(0x1cd, 0x1d);
            this.btn_SetAuthority.Name = "btn_SetAuthority";
            this.btn_SetAuthority.Size = new Size(0x4b, 0x17);
            this.btn_SetAuthority.TabIndex = 2;
            this.btn_SetAuthority.Text = "重设权限";
            this.btn_SetAuthority.UseVisualStyleBackColor = true;
            this.btn_SetAuthority.Click += new EventHandler(this.btn_SetAuthority_Click);
            this.groupBox4.Controls.Add(this.lvw_Authority);
            this.groupBox4.Location = new Point(0x99, 7);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new Padding(5);
            this.groupBox4.Size = new Size(0x124, 0x11e);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "权限";
            this.lvw_Authority.CheckBoxes = true;
            this.lvw_Authority.Dock = DockStyle.Fill;
            this.lvw_Authority.Location = new Point(5, 0x13);
            this.lvw_Authority.Name = "lvw_Authority";
            this.lvw_Authority.Size = new Size(0x11a, 0x106);
            this.lvw_Authority.TabIndex = 0;
            this.lvw_Authority.UseCompatibleStateImageBehavior = false;
            this.lvw_Authority.View = View.List;
            this.groupBox3.Controls.Add(this.lbx_User);
            this.groupBox3.Location = new Point(8, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new Padding(5);
            this.groupBox3.Size = new Size(0x8b, 0x11e);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "用户";
            this.lbx_User.Dock = DockStyle.Fill;
            this.lbx_User.FormattingEnabled = true;
            this.lbx_User.ItemHeight = 12;
            this.lbx_User.Location = new Point(5, 0x13);
            this.lbx_User.Name = "lbx_User";
            this.lbx_User.Size = new Size(0x81, 0x100);
            this.lbx_User.TabIndex = 0;
            this.lbx_User.SelectedIndexChanged += new EventHandler(this.lbx_User_SelectedIndexChanged);
            this.tabPage3.Controls.Add(this.btn_ModiPwd);
            this.tabPage3.Controls.Add(this.groupBox5);
            this.tabPage3.Location = new Point(4, 0x15);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new Padding(3);
            this.tabPage3.Size = new Size(0x223, 0x12b);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "修改密码";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.btn_ModiPwd.Location = new Point(0x1cd, 0x1d);
            this.btn_ModiPwd.Name = "btn_ModiPwd";
            this.btn_ModiPwd.Size = new Size(0x4b, 0x17);
            this.btn_ModiPwd.TabIndex = 3;
            this.btn_ModiPwd.Text = "修改密码";
            this.btn_ModiPwd.UseVisualStyleBackColor = true;
            this.btn_ModiPwd.Click += new EventHandler(this.btn_ModiPwd_Click);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.tbx_NewPwd);
            this.groupBox5.Controls.Add(this.tbx_ConfirmNewPwd);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.tbx_OldPwd);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.tbx_UName);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Location = new Point(8, 8);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0x1b5, 0x8f);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "修改密码";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x13, 0x71);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x3b, 12);
            this.label8.TabIndex = 9;
            this.label8.Text = "重复密码:";
            this.tbx_NewPwd.Location = new Point(0x53, 80);
            this.tbx_NewPwd.Name = "tbx_NewPwd";
            this.tbx_NewPwd.PasswordChar = '*';
            this.tbx_NewPwd.Size = new Size(0x14d, 0x15);
            this.tbx_NewPwd.TabIndex = 2;
            this.tbx_ConfirmNewPwd.Location = new Point(0x53, 110);
            this.tbx_ConfirmNewPwd.Name = "tbx_ConfirmNewPwd";
            this.tbx_ConfirmNewPwd.PasswordChar = '*';
            this.tbx_ConfirmNewPwd.Size = new Size(0x14d, 0x15);
            this.tbx_ConfirmNewPwd.TabIndex = 3;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x13, 0x53);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x3b, 12);
            this.label10.TabIndex = 4;
            this.label10.Text = "新 密 码:";
            this.tbx_OldPwd.Location = new Point(0x53, 50);
            this.tbx_OldPwd.Name = "tbx_OldPwd";
            this.tbx_OldPwd.PasswordChar = '*';
            this.tbx_OldPwd.Size = new Size(0x14d, 0x15);
            this.tbx_OldPwd.TabIndex = 1;
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0x13, 0x35);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x3b, 12);
            this.label11.TabIndex = 2;
            this.label11.Text = "原 密 码:";
            this.tbx_UName.Location = new Point(0x53, 20);
            this.tbx_UName.Name = "tbx_UName";
            this.tbx_UName.ReadOnly = true;
            this.tbx_UName.Size = new Size(0x14d, 0x15);
            this.tbx_UName.TabIndex = 0;
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0x13, 0x17);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x3b, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "用 户 名:";
            this.tabPage4.Controls.Add(this.btn_SaveSet);
            this.tabPage4.Controls.Add(this.groupBox6);
            this.tabPage4.Location = new Point(4, 0x15);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new Padding(3);
            this.tabPage4.Size = new Size(0x223, 0x12b);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "选项设置";
            this.tabPage4.UseVisualStyleBackColor = true;
            this.btn_SaveSet.Location = new Point(0x1cd, 0x1d);
            this.btn_SaveSet.Name = "btn_SaveSet";
            this.btn_SaveSet.Size = new Size(0x4b, 0x17);
            this.btn_SaveSet.TabIndex = 5;
            this.btn_SaveSet.Text = "保存设置";
            this.btn_SaveSet.UseVisualStyleBackColor = true;
            this.btn_SaveSet.Click += new EventHandler(this.btn_SaveSet_Click);
            this.groupBox6.Controls.Add(this.cbx_SendPrinter);
            this.groupBox6.Controls.Add(this.cbx_SendListPrinter);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.textBox2);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.tbx_BoxAmount);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Location = new Point(8, 8);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(0x1b5, 0x8f);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "设置";
            this.cbx_SendPrinter.FormattingEnabled = true;
            this.cbx_SendPrinter.Location = new Point(0x92, 0x33);
            this.cbx_SendPrinter.Name = "cbx_SendPrinter";
            this.cbx_SendPrinter.Size = new Size(270, 20);
            this.cbx_SendPrinter.TabIndex = 11;
            this.cbx_SendListPrinter.FormattingEnabled = true;
            this.cbx_SendListPrinter.Location = new Point(0x92, 0x4f);
            this.cbx_SendListPrinter.Name = "cbx_SendListPrinter";
            this.cbx_SendListPrinter.Size = new Size(270, 20);
            this.cbx_SendListPrinter.TabIndex = 10;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x13, 0x71);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x23, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "XXXX:";
            this.label7.Click += new EventHandler(this.label7_Click);
            this.textBox2.Location = new Point(0x92, 110);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new Size(270, 0x15);
            this.textBox2.TabIndex = 3;
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x13, 0x53);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x5f, 12);
            this.label9.TabIndex = 4;
            this.label9.Text = "发货明细打印机:";
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0x13, 0x35);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x53, 12);
            this.label13.TabIndex = 2;
            this.label13.Text = "发货单打印机:";
            this.tbx_BoxAmount.Location = new Point(0x92, 20);
            this.tbx_BoxAmount.Name = "tbx_BoxAmount";
            this.tbx_BoxAmount.Size = new Size(270, 0x15);
            this.tbx_BoxAmount.TabIndex = 0;
            this.label14.AutoSize = true;
            this.label14.Location = new Point(0x13, 0x17);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x47, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "底板箱容量:";
            this.tabPage5.Location = new Point(4, 0x15);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new Padding(3);
            this.tabPage5.Size = new Size(0x223, 0x12b);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "工位系数";
            this.tabPage5.UseVisualStyleBackColor = true;
            this.btn_Cancel.Location = new Point(0x1e7, 0x155);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new Size(0x4b, 0x17);
            this.btn_Cancel.TabIndex = 2;
            this.btn_Cancel.Text = "退出";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new EventHandler(this.btn_Cancel_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x241, 0x18b);
            base.Controls.Add(this.btn_Cancel);
            base.Controls.Add(this.tabControl);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "Frm_Sys";
            base.Padding = new Padding(8);
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "系统管理";
            base.FormClosed += new FormClosedEventHandler(this.Frm_Sys_FormClosed);
            base.Load += new EventHandler(this.Frm_Data_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            base.ResumeLayout(false);
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void lbx_User_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet userAllAuthority = new User().GetUserAllAuthority(this.lbx_User.SelectedItem.ToString());
            foreach (ListViewItem item in this.lvw_Authority.Items)
            {
                item.Checked = false;
                for (int i = 0; i < userAllAuthority.Tables[0].Rows.Count; i++)
                {
                    if (item.Tag.ToString() == userAllAuthority.Tables[0].Rows[i]["Code"].ToString())
                    {
                        item.Checked = true;
                    }
                }
            }
        }

        private void Lvw_User_Click(object sender, EventArgs e)
        {
            this.tbx_UserName.Text = this.Lvw_User.SelectedItems[0].Text.Trim();
            this.tbx_TrueName.Text = this.Lvw_User.SelectedItems[0].SubItems[1].Text.Trim();
            this.tbx_Duty.Text = this.Lvw_User.SelectedItems[0].SubItems[2].Text.Trim();
            this.tbx_BarCode.Text = this.Lvw_User.SelectedItems[0].SubItems[3].Text.Trim();
        }

        public static Frm_Sys Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Frm_Sys();
                }
                return m_Instance;
            }
        }
    }
}

