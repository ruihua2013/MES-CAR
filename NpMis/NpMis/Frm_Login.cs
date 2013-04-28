namespace NpMis
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Frm_Login : Form
    {
        private Button btn_Max;
        internal Button Cancel;
        private IContainer components;
        internal Button OK;
        internal TextBox tbxPassword;
        internal TextBox tbxUserName;

        public Frm_Login()
        {
            this.InitializeComponent();
        }

        private void btn_Max_Click(object sender, EventArgs e)
        {
            base.Close();
            base.Dispose();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            base.Close();
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(Frm_Login));
            this.Cancel = new Button();
            this.OK = new Button();
            this.tbxPassword = new TextBox();
            this.tbxUserName = new TextBox();
            this.btn_Max = new Button();
            base.SuspendLayout();
            this.Cancel.BackColor = Color.Transparent;
            this.Cancel.BackgroundImage = Rs.取消正常;
            this.Cancel.BackgroundImageLayout = ImageLayout.Center;
            this.Cancel.DialogResult = DialogResult.Cancel;
            this.Cancel.FlatAppearance.BorderSize = 0;
            this.Cancel.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.Cancel.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.Cancel.FlatStyle = FlatStyle.Flat;
            this.Cancel.Location = new Point(0x163, 0xd6);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new Size(0x4b, 0x47);
            this.Cancel.TabIndex = 11;
            this.Cancel.UseVisualStyleBackColor = false;
            this.Cancel.Click += new EventHandler(this.Cancel_Click);
            this.OK.BackColor = Color.Transparent;
            this.OK.BackgroundImage = Rs.确定正常;
            this.OK.BackgroundImageLayout = ImageLayout.Center;
            this.OK.DialogResult = DialogResult.OK;
            this.OK.FlatAppearance.BorderSize = 0;
            this.OK.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.OK.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.OK.FlatStyle = FlatStyle.Flat;
            this.OK.Location = new Point(0x109, 0xd6);
            this.OK.Name = "OK";
            this.OK.Size = new Size(0x4b, 0x47);
            this.OK.TabIndex = 2;
            this.OK.UseVisualStyleBackColor = false;
            this.OK.Click += new EventHandler(this.OK_Click);
            this.tbxPassword.BackColor = Color.WhiteSmoke;
            this.tbxPassword.Location = new Point(0x63, 0xab);
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.PasswordChar = '*';
            this.tbxPassword.Size = new Size(0x11d, 0x15);
            this.tbxPassword.TabIndex = 1;
            this.tbxPassword.KeyPress += new KeyPressEventHandler(this.tbxPassword_KeyPress);
            this.tbxUserName.BackColor = Color.WhiteSmoke;
            this.tbxUserName.Location = new Point(0x63, 0x90);
            this.tbxUserName.Name = "tbxUserName";
            this.tbxUserName.Size = new Size(0x11d, 0x15);
            this.tbxUserName.TabIndex = 0;
            this.btn_Max.BackColor = Color.Transparent;
            this.btn_Max.BackgroundImage = Rs.关闭正常;
            this.btn_Max.BackgroundImageLayout = ImageLayout.Center;
            this.btn_Max.FlatAppearance.BorderSize = 0;
            this.btn_Max.FlatStyle = FlatStyle.Flat;
            this.btn_Max.Location = new Point(0x1a0, 0x12);
            this.btn_Max.Name = "btn_Max";
            this.btn_Max.Size = new Size(0x16, 0x17);
            this.btn_Max.TabIndex = 12;
            this.btn_Max.UseVisualStyleBackColor = false;
            this.btn_Max.Click += new EventHandler(this.btn_Max_Click);
            base.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = Color.Red;
            this.BackgroundImage = Rs.登陆界面底图无文字2;
            base.ClientSize = new Size(0x1c4, 0x153);
            base.ControlBox = false;
            base.Controls.Add(this.btn_Max);
            base.Controls.Add(this.Cancel);
            base.Controls.Add(this.OK);
            base.Controls.Add(this.tbxPassword);
            base.Controls.Add(this.tbxUserName);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.Name = "Frm_Login";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "系统登录";
            base.TransparencyKey = Color.Red;
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            User user = new User();
            if ((this.tbxUserName.Text.Trim() == "") || (this.tbxPassword.Text.Trim() == ""))
            {
                MessageBox.Show("请输入用户的完整信息", "系统登录", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                switch (user.UserLogin(this.tbxUserName.Text.Trim(), this.tbxPassword.Text.Trim()))
                {
                    case -2:
                        MessageBox.Show("用户不存在,请检查后重新输入", "系统登录", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.tbxUserName.Focus();
                        return;

                    case -1:
                        MessageBox.Show("用户密码错误", "系统登录", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.tbxPassword.Focus();
                        return;
                }
                base.Hide();
                new Frm_Main().Show();
            }
        }

        private void tbxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.OK_Click(this, e);
            }
        }
    }
}

