using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace ConnectDB
{
	/// <summary>
	/// ConnectServer 的摘要说明。
	/// </summary>
	public class ConnectServer : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cboServers;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
        //SqlConnectionStringBuilder sqlconsb = new SqlConnectionStringBuilder();
		public ConnectServer()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
            this.cboServers.Items.Clear();
            this.comboBox1.Items.Clear();
            this.cboServers.Text = "";
            this.comboBox1.Text = "";
            this.textBox1.Text = "sa";
            this.textBox2.Text = "";



			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}
		public string GetSqlStr()
		{
            //return sqlconsb.ConnectionString;
			return string.Format("server={0};uid={1};pwd={2};database={3}",this.cboServers.Text,this.textBox1.Text,this.textBox2.Text,this.comboBox1.Text);
									   
		}
		private string GetMasterSqlStr()
		{
			return string.Format("server={0};uid={1};pwd={2};database=master",this.cboServers.Text,this.textBox1.Text,this.textBox2.Text);
									   
		}
		public string ServerName
		{
			get
			{
				return this.cboServers.Text;
			}
		}
		public string Uid
		{
			get
			{
				return this.textBox1.Text;
			}
		}
		public string Pwd
		{
			get
			{
				return this.textBox2.Text;
			}
		}
		public string DataBase
		{
			get
			{
				return this.comboBox1.Text;
			}
            set
            {
                this.comboBox1.Text = value;
            }
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboServers = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboServers);
            this.groupBox1.Location = new System.Drawing.Point(8, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 192);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(168, 160);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "测试连接";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Location = new System.Drawing.Point(24, 136);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(224, 20);
            this.comboBox1.TabIndex = 15;
            this.comboBox1.Text = "comboBox1";
            this.comboBox1.DropDown += new System.EventHandler(this.comboBox1_DropDown);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(232, 23);
            this.label4.TabIndex = 14;
            this.label4.Text = "请选择服务器上的数据库：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(104, 88);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(144, 21);
            this.textBox2.TabIndex = 13;
            this.textBox2.Text = "textBox2";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 23);
            this.label3.TabIndex = 12;
            this.label3.Text = "密码：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(104, 64);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(144, 21);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "textBox1";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 23);
            this.label2.TabIndex = 10;
            this.label2.Text = "用户名称：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(232, 23);
            this.label1.TabIndex = 9;
            this.label1.Text = "请选择或输入服务器名称：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboServers
            // 
            this.cboServers.Location = new System.Drawing.Point(16, 40);
            this.cboServers.Name = "cboServers";
            this.cboServers.Size = new System.Drawing.Size(232, 20);
            this.cboServers.TabIndex = 8;
            this.cboServers.Text = "comboBox1";
            this.cboServers.SelectedIndexChanged += new System.EventHandler(this.cboServers_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(112, 200);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "确定";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Location = new System.Drawing.Point(200, 200);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "放弃";
            // 
            // ConnectServer
            // 
            this.AcceptButton = this.button2;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.button3;
            this.ClientSize = new System.Drawing.Size(298, 231);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectServer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据库连接";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ConnectServer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void Init()
		{



			string[] servers = SqlLocator.GetServers();

			if(null!=servers)
			{
				foreach ( string s in servers )
				{
					this.cboServers.Items.Add(s);
				}
			}

			if(this.cboServers.Items.Count > 0) 
				this.cboServers.SelectedIndex = 0; 
			else 
				this.cboServers.Items.Add("(local)"); 

		}
		public static bool isConnection(string constr)
		{
			try
			{
                using (System.Data.SqlClient.SqlConnection sqlcon = new System.Data.SqlClient.SqlConnection(constr))
                {
                    sqlcon.Open();
                    sqlcon.Close();
                    return true;
                }
			}
			catch
			{
				return false;
			}

		}

		private void comboBox1_DropDown(object sender, System.EventArgs e)
		{
			if(isConnection(GetMasterSqlStr()))
			{
				refredb();
			}
			else
			{
				MessageBox.Show("数据库连接失败！","提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		private void cboServers_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.comboBox1.SelectedIndex=-1;
		}
		private void refredb()
		{
			System.Data.DataSet ds=new System.Data.DataSet();
			System.Data.SqlClient.SqlConnection sqlcon=new System.Data.SqlClient.SqlConnection(GetMasterSqlStr());
			sqlcon.Open();
			System.Data.SqlClient.SqlDataAdapter adpter=new System.Data.SqlClient.SqlDataAdapter("select name from sysdatabases",sqlcon);
			adpter.Fill(ds);

			this.comboBox1.Items.Clear();
			this.comboBox1.Text="pdmoa";
			for(int i=0;i<ds.Tables[0].Rows.Count;i++)
			{
				this.comboBox1.Items.Add(ds.Tables[0].Rows[i][0]);
			}
			sqlcon.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if(isConnection(GetSqlStr()))
			{
				MessageBox.Show("数据库连接成功！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("数据库连接失败！","提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			if(this.comboBox1.Text=="")
			{
				MessageBox.Show("请选择服务器上的数据库！","提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			if(isConnection(GetSqlStr()))
			{
				this.DialogResult=DialogResult.OK;
			}
			else
			{
				MessageBox.Show("数据库连接失败！","提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

        private void ConnectServer_Load(object sender, EventArgs e)
        {
            this.Refresh();
            Application.DoEvents();
            MethodInvoker deleg = delegate { Init(); };
            this.BeginInvoke(deleg);
        }
	}
	public class SqlLocator
	{
		[DllImport("odbc32.dll")]
		private static extern short SQLAllocHandle(short hType, IntPtr inputHandle, out IntPtr outputHandle);
		[DllImport("odbc32.dll")]
		private static extern short SQLSetEnvAttr(IntPtr henv, int attribute, IntPtr valuePtr, int strLength);
		[DllImport("odbc32.dll")]
		private static extern short SQLFreeHandle(short hType, IntPtr handle); 
		[DllImport("odbc32.dll",CharSet=CharSet.Ansi)]
		private static extern short SQLBrowseConnect(IntPtr hconn, StringBuilder inString, 
			short inStringLength, StringBuilder outString, short outStringLength,
			out short outLengthNeeded);

		private const short SQL_HANDLE_ENV = 1;
		private const short SQL_HANDLE_DBC = 2;
		private const int SQL_ATTR_ODBC_VERSION = 200;
		private const int SQL_OV_ODBC3 = 3;
		private const short SQL_SUCCESS = 0;
		
		private const short SQL_NEED_DATA = 99;
		private const short DEFAULT_RESULT_SIZE = 1024;
		private const string SQL_DRIVER_STR = "DRIVER=SQL SERVER";
	
		private SqlLocator(){}

        private static string[] servers = null;

		public static string[] GetServers()
		{
            if (servers != null) return servers;

			string[] retval = null;
			string txt = string.Empty;
			IntPtr henv = IntPtr.Zero;
			IntPtr hconn = IntPtr.Zero;
			StringBuilder inString = new StringBuilder(SQL_DRIVER_STR);
			StringBuilder outString = new StringBuilder(DEFAULT_RESULT_SIZE);
			short inStringLength = (short) inString.Length;
			short lenNeeded = 0;

			try
			{
				if (SQL_SUCCESS == SQLAllocHandle(SQL_HANDLE_ENV, henv, out henv))
				{
					if (SQL_SUCCESS == SQLSetEnvAttr(henv,SQL_ATTR_ODBC_VERSION,(IntPtr)SQL_OV_ODBC3,0))
					{
						if (SQL_SUCCESS == SQLAllocHandle(SQL_HANDLE_DBC, henv, out hconn))
						{
							if (SQL_NEED_DATA ==  SQLBrowseConnect(hconn, inString, inStringLength, outString, 
								DEFAULT_RESULT_SIZE, out lenNeeded))
							{
								if (DEFAULT_RESULT_SIZE < lenNeeded)
								{
									outString.Capacity = lenNeeded;
									if (SQL_NEED_DATA != SQLBrowseConnect(hconn, inString, inStringLength, outString, 
										lenNeeded,out lenNeeded))
									{
										throw new ApplicationException("Unabled to aquire SQL Servers from ODBC driver.");
									}	
								}
								txt = outString.ToString();
								int start = txt.IndexOf("{") + 1;
								int len = txt.IndexOf("}") - start;
								if ((start > 0) && (len > 0))
								{
									txt = txt.Substring(start,len);
								}
								else
								{
									txt = string.Empty;
								}
							}						
						}
					}
				}
			}
			catch //(Exception ex)
			{
				//Throw away any error if we are not in debug mode
#if (DEBUG)
				//MessageBox.Show(ex.Message,"Acquire SQL Servier List Error");
#endif 
				txt = string.Empty;
			}
			finally
			{
				if (hconn != IntPtr.Zero)
				{
					SQLFreeHandle(SQL_HANDLE_DBC,hconn);
				}
				if (henv != IntPtr.Zero)
				{
					SQLFreeHandle(SQL_HANDLE_ENV,hconn);
				}
			}
	
			if (txt.Length > 0)
			{
				retval = txt.Split(",".ToCharArray());
                servers = retval;
			}

			return retval;
		}
	}
}
