using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;

using CWData;
using System.Threading;
using ICSharpMain.PublicConfig;
using ICSharpCode.Core;
using ConnectDB;
using AttnObjects;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpMain;
using NpMis;
using MES_Car;

namespace MES_Car
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class Fm_Login : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button LoginButton;

        private Config config = Config.GetInstance();

        private Button SetButton;

        static bool isLogin = false;
        private TextBox txt_Name;

		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Fm_Login()
		{
			//isShowAllUseronLogin=ICSharpCode.Core.PropertyService.Get("isShowAllUseronLogin",true);
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

            this.ExitButton.Text = ICSharpCode.Core.ResourceService.GetString("退出");
            this.LoginButton.Text = ICSharpCode.Core.ResourceService.GetString("登录");
            this.SetButton.Text = ICSharpCode.Core.ResourceService.GetString("设置");

            this.label1.Text = ICSharpCode.Core.ResourceService.GetString("用户");
            this.label2.Text = ICSharpCode.Core.ResourceService.GetString("密码");
            this.Text = ICSharpCode.Core.ResourceService.GetString("系统登录");

            System.Drawing.Bitmap bmp = ICSharpCode.Core.ResourceService.GetBitmap("pictureBox1");
            if (null != bmp)
            {
                this.BackgroundImage = bmp;
                this.Size = new Size(bmp.Size.Width + 4, bmp.Size.Height + 24);
            }
			
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Windows Form Designer generated code
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.ExitButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SetButton = new System.Windows.Forms.Button();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ExitButton
            // 
            this.ExitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ExitButton.Location = new System.Drawing.Point(262, 156);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(63, 23);
            this.ExitButton.TabIndex = 5;
            this.ExitButton.Text = "退出";
            this.ExitButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.textBox1.Location = new System.Drawing.Point(208, 107);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '●';
            this.textBox1.Size = new System.Drawing.Size(152, 22);
            this.textBox1.TabIndex = 3;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxEdit1_KeyPress);
            // 
            // LoginButton
            // 
            this.LoginButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LoginButton.Location = new System.Drawing.Point(170, 156);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(86, 23);
            this.LoginButton.TabIndex = 4;
            this.LoginButton.Text = "登录";
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(112, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(112, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "密码";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SetButton
            // 
            this.SetButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.SetButton.Location = new System.Drawing.Point(331, 156);
            this.SetButton.Name = "SetButton";
            this.SetButton.Size = new System.Drawing.Size(51, 23);
            this.SetButton.TabIndex = 6;
            this.SetButton.Text = "设置";
            this.SetButton.Click += new System.EventHandler(this.SetButton_Click);
            // 
            // txt_Name
            // 
            this.txt_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Name.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txt_Name.Location = new System.Drawing.Point(208, 67);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(152, 22);
            this.txt_Name.TabIndex = 1;
            this.txt_Name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxEdit1_KeyPress);
            // 
            // Fm_Login
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.ExitButton;
            this.ClientSize = new System.Drawing.Size(394, 233);
            this.Controls.Add(this.txt_Name);
            this.Controls.Add(this.SetButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.ExitButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Fm_Login";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统登录";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Fm_Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

        void Fm_Login_Load(object sender, EventArgs e)
        {

            MethodInvoker deleg = delegate { LoadData(); };
            this.BeginInvoke(deleg);
            this.Refresh();
            Application.DoEvents();
            this.txt_Name.Text= PropertyService.Get<string>("PrvLoginName", "");
            
        }
        private void LoadData()
        {
            string constr = PropertyService.Get("sqlconfig", "");
            constr = TbrERPTools.DecodeBase64(constr);
            if (constr == "" || !ConnectServer.isConnection(constr))
            {
                ConnectServer connect = new ConnectServer();
                if (connect.ShowDialog() == DialogResult.OK)
                {
                    constr = connect.GetSqlStr();
                    string jmconstr = TbrERPTools.EncodeBase64(constr);
                    PropertyService.Set("sqlconfig", jmconstr);
                    PropertyService.Save();
                }
                else
                {
                    Application.Exit();
                    return;
                }
            }
            CWData.ServerFactory.GetServer(CWData.ServerType.MasterDB).ConnectionString = constr;
            CWData.ServerFactory.GetServer().ConnectionString = constr;

        }
        private void button1_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }


        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            User user = new  User();
            string userno=this.txt_Name.Text.Trim();
            if (userno == "")
            {
                MessageBox.Show("请输入用户的完整信息", "系统登录", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                switch (user.UserLogin(userno, this.textBox1.Text.Trim()))
                {
                    case -2:
                        MessageBox.Show("用户不存在,请检查后重新输入", "系统登录", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txt_Name.Focus();
                        return;

                    case -1:
                        MessageBox.Show("用户密码错误", "系统登录", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.textBox1.Focus();
                        return;
                }
                PSWD pswd = PSWD.GetById("0000", userno);
                LoginUser lguser = new LoginUser(pswd);
                AttnObjects.PSWD.LoginUser = pswd;
                LoginService.LoginUser = lguser;

                SetQxMod();
                if (isLogin)
                {
                    WorkbenchSingleton.Workbench.RedrawAllComponents();
                }

                this.DialogResult = DialogResult.OK;
                isLogin = true;
                PropertyService.Set<string>("PrvLoginName", userno);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void SetQxMod()
        {
            ICSharpMain.Gui.Pads.IdeModPad.AllMod.Clear();
            foreach (AttnObjects.programtree pt in AttnObjects.PSWD.LoginUser.GetAllMod())
            {
                ICSharpMain.Gui.Pads.IdeModPad.AllMod[pt.QX_BH]= pt;
            }
        }

		private void comboBox1_Leave(object sender, System.EventArgs e)
		{
			if(sender is ComboBox)
			{
				System.Windows.Forms.ComboBox cb=(System.Windows.Forms.ComboBox)sender;
				int iFoundIndex=cb.FindStringExact(cb.Text);
				cb.SelectedIndex=iFoundIndex;
			}
		}
		private bool CheckU()
		{
            string sql = "select getdate()";
            string servertime = DbParams.SqlServer.GetFirstString(sql);
            DateTime dtime = System.DateTime.Parse(servertime);
            if (dtime.Date.CompareTo(System.DateTime.Now.Date)!=0)
            {
                string mess = string.Format("与服务器日期不同步！\n服务器日期：{0}", dtime.Date.ToString("yyyy-MM-dd"));
                MessageBox.Show(mess, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
			
			return true;
		}

		private void comboBoxEdit1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13)
			{
				SelectNextControl(ActiveControl,true,true,true,true);
			}
		}

        //数据库链接设置
        private void SetButton_Click(object sender, EventArgs e)
        {
            ConnectServer connect = new ConnectServer();
            if (connect.ShowDialog() == DialogResult.OK)
            {
                string constr = connect.GetSqlStr();
                string jmconstr = TbrERPTools.EncodeBase64(constr);
                PropertyService.Set("sqlconfig", jmconstr);
                PropertyService.Save();
                CWData.ServerFactory.GetServer().ConnectionString = constr;
            }
        }


	}
}
