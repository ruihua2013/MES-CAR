namespace PlanInput
{
    using Common;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    public class Frm_Input : Form
    {
        internal Button btn_CheckMode;
        private Button btnCancle;
        private Button btnChar01;
        private Button btnChar02;
        private Button btnChar03;
        private Button btnChar04;
        private Button btnChar05;
        private Button btnChar06;
        private Button btnChar07;
        private Button btnChar08;
        private Button btnChar09;
        private Button btnChar10;
        private Button btnChar11;
        private Button btnChar12;
        private Button btnChar13;
        private Button btnChar14;
        private Button btnChar15;
        private Button btnChar16;
        private Button btnChar17;
        private Button btnChar18;
        private Button btnChar19;
        private Button btnChar20;
        private Button btnChar21;
        private Button btnChar22;
        private Button btnChar23;
        private Button btnChar24;
        private Button btnChar25;
        private Button btnChar26;
        private Button btnChar27;
        private Button btnChar28;
        private Button btnChar29;
        private Button btnChar30;
        private Button btnChar31;
        private Button btnChar32;
        private Button btnChar33;
        internal Button BtnCreatePlan;
        private Button btnFW;
        private Button btnIgnore;
        private Button btnImportFromHtm;
        internal Button btnInputMode;
        internal Button BtnLargInput;
        private Button btnLargInputCacel;
        private Button btnLargInputOK;
        private Button btnLargPlanExit;
        private Button btnLargPlanOK;
        private Button btnNumP1;
        private Button btnNumP2;
        private Button btnNumP3;
        private Button btnNumP4;
        private Button btnNumP5;
        private Button btnNumP6;
        private Button btnNumP7;
        internal Button BtnSaveNP;
        private Button btnSaveNPTemp;
        private Button btnUpdatePlanID;
        private CheckBox checkBackPiece;
        private CheckBox checkBoxMail;
        private CheckBox checkFontPiece;
        private CheckBox checkIsLargPlan;
        private ComboBox combLargPlanNPType;
        internal ComboBox CombNPType;
        internal ComboBox CombPlanDepart;
        internal ComboBox CombPlanType;
        private IContainer components;
        internal DateTimePicker DateDeadLine;
        internal DateTimePicker DatePlanTime;
        private DataSet ds = new DataSet();
        private ArrayList G_FWlist;
        private string G_FWPos;
        private int G_GridNPInfoLastIndex;
        private Hashtable G_htNPType;
        private bool G_IsCheck;
        private Hashtable G_LargPlanCount;
        private int G_NPBegin;
        private int G_NPCount;
        private int G_NPEnd;
        private string[] G_NPWord;
        private int G_PlanID = 1;
        private int G_Pos;
        private int G_SubPlan;
        private string G_UserID;
        internal DataGridView GridNPInfo;
        private DataGridView GridSameNPInfo;
        internal GroupBox GroupBox1;
        internal GroupBox GroupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupChar;
        private DataSet InputDS = new DataSet();
        internal Label Label1;
        internal Label Label12;
        internal Label Label13;
        internal Label label14;
        internal Label label15;
        private Label label16;
        internal Label Label2;
        internal Label label21;
        internal Label label22;
        internal Label Label3;
        internal Label Label4;
        internal Label Label5;
        internal Label Label6;
        internal Label label7;
        private Label label8;
        private Label label9;
        private Label labLargPlanTotal;
        private static Frm_Input m_Instance;
        private Sql2KDataAccess MyDB = new Sql2KDataAccess();
        private bool[,] NPArr;
        private string[] NPChar;
        private Panel palLargInput;
        private Panel palNPAllInfo;
        internal Panel PalNpNum;
        private ArrayList PCNPNum;
        private RichTextBox rTextAddition;
        private SplitContainer splitContainer1;
        private TabControl tabControl1;
        private TabPage tabLargInput;
        private TabPage tabNPInfo;
        private TabPage tabShow;
        internal TextBox textFWBegin;
        private TextBox textFWDefine;
        internal TextBox textFWEnd;
        private TextBox textLargPlanCount;
        private TextBox textNPTemplate;
        private TextBox textNpWord;
        internal TextBox textPCNPNum;
        internal TextBox TextPlanID;
        public TextBox TextShowNPNum;
        internal TextBox TextTotalCount;

        private Frm_Input()
        {
            this.InitializeComponent();
            this.NPArr = new bool[7, 0x24];
            this.NPChar = new string[0x24];
            this.G_NPWord = new string[6];
            this.G_LargPlanCount = new Hashtable();
            this.PCNPNum = new ArrayList();
            this.G_FWlist = new ArrayList();
        }

        private void AddOneNP()
        {
            this.G_NPCount++;
            DataRow row = this.InputDS.Tables[0].Rows.Add(new object[0]);
            row["XH"] = this.G_NPCount;
            row["NPType"] = this.CombNPType.Text;
            row["NPNum"] = this.TextShowNPNum.Text;
            row["NPTypeID"] = this.CombNPType.SelectedValue;
            row["FrontPiece"] = Convert.ToInt32(this.checkFontPiece.Checked).ToString();
            row["BackPiece"] = Convert.ToInt32(this.checkBackPiece.Checked).ToString();
            row["Mail"] = Convert.ToInt32(this.checkBoxMail.Checked).ToString();
            row["PlanID"] = this.TextPlanID.Text;
            row["PlanDepart"] = this.CombPlanDepart.Text;
            row["PlanTime"] = this.DatePlanTime.Value;
            row["DeadLine"] = this.DateDeadLine.Value;
            if (this.GridNPInfo.RowCount > 12)
            {
                this.GridNPInfo.FirstDisplayedScrollingRowIndex = this.GridNPInfo.RowCount - 12;
            }
            this.TextShowNPNum.Text = this.TextShowNPNum.Text.Substring(0, 2);
        }

        private void AllCombShowInit()
        {
            this.NPChar[0] = "0";
            this.NPChar[1] = "1";
            this.NPChar[2] = "2";
            this.NPChar[3] = "3";
            this.NPChar[4] = "4";
            this.NPChar[5] = "5";
            this.NPChar[6] = "6";
            this.NPChar[7] = "7";
            this.NPChar[8] = "8";
            this.NPChar[9] = "9";
            this.NPChar[10] = "A";
            this.NPChar[11] = "B";
            this.NPChar[12] = "C";
            this.NPChar[13] = "D";
            this.NPChar[14] = "E";
            this.NPChar[15] = "F";
            this.NPChar[0x10] = "G";
            this.NPChar[0x11] = "H";
            this.NPChar[0x12] = "J";
            this.NPChar[0x13] = "K";
            this.NPChar[20] = "L";
            this.NPChar[0x15] = "M";
            this.NPChar[0x16] = "N";
            this.NPChar[0x17] = "P";
            this.NPChar[0x18] = "Q";
            this.NPChar[0x19] = "R";
            this.NPChar[0x1a] = "S";
            this.NPChar[0x1b] = "T";
            this.NPChar[0x1c] = "U";
            this.NPChar[0x1d] = "V";
            this.NPChar[30] = "W";
            this.NPChar[0x1f] = "X";
            this.NPChar[0x20] = "Y";
            this.NPChar[0x21] = "";
            this.NPChar[0x22] = "";
            this.NPChar[0x23] = "";
            foreach (Control control in this.groupChar.Controls)
            {
                if (control is Button)
                {
                    int num = Convert.ToInt32(control.Name.Substring(7, 2));
                    if (num < 0x22)
                    {
                        control.Text = this.NPChar[num - 1];
                    }
                }
            }
            this.ds = this.MyDB.Run_SqlText("select description,code from V_NPType");
            this.CombNPType.DataSource = this.ds.Tables[0].DefaultView;
            this.CombNPType.DisplayMember = "description";
            this.CombNPType.ValueMember = "code";
            this.CombNPType.SelectedIndex = 1;
            this.CombNPType.Refresh();
            this.G_htNPType = new Hashtable();
            for (int i = 0; i < this.ds.Tables[0].Rows.Count; i++)
            {
                this.G_htNPType.Add(this.ds.Tables[0].Rows[i]["code"].ToString(), this.ds.Tables[0].Rows[i]["description"].ToString());
            }
            this.combLargPlanNPType.DataSource = this.ds.Tables[0].DefaultView;
            this.combLargPlanNPType.DisplayMember = "description";
            this.combLargPlanNPType.ValueMember = "code";
            this.combLargPlanNPType.SelectedIndex = 1;
            this.combLargPlanNPType.Refresh();
            this.ds = this.MyDB.Run_SqlText("select Code,CodeName from V_PlanKind");
            this.CombPlanType.DataSource = this.ds.Tables[0].DefaultView;
            this.CombPlanType.DisplayMember = "CodeName";
            this.CombPlanType.ValueMember = "Code";
            this.CombPlanType.SelectedIndex = 2;
            this.CombPlanType.Refresh();
            this.ds = this.MyDB.Run_SqlText("select Code,CodeName from V_Depart");
            this.CombPlanDepart.DataSource = this.ds.Tables[0].DefaultView;
            this.CombPlanDepart.DisplayMember = "CodeName";
            this.CombPlanDepart.ValueMember = "Code";
            this.CombPlanDepart.SelectedIndex = 0;
            this.CombPlanDepart.Refresh();
            DataTable table = new DataTable("NPInfo");
            table = this.InputDS.Tables.Add();
            DataColumn column = new DataColumn("XH", typeof(int));
            table.Columns.Add(column);
            DataColumn column2 = new DataColumn("NPType", typeof(string));
            table.Columns.Add(column2);
            DataColumn column3 = new DataColumn("NPNum", typeof(string));
            table.Columns.Add(column3);
            DataColumn column4 = new DataColumn("NPTypeID", typeof(string));
            table.Columns.Add(column4);
            DataColumn column5 = new DataColumn("FrontPiece", typeof(string));
            table.Columns.Add(column5);
            DataColumn column6 = new DataColumn("BackPiece", typeof(string));
            table.Columns.Add(column6);
            DataColumn column7 = new DataColumn("Mail", typeof(string));
            table.Columns.Add(column7);
            DataColumn column8 = new DataColumn("PlanID", typeof(string));
            table.Columns.Add(column8);
            DataColumn column9 = new DataColumn("PlanDepart", typeof(string));
            table.Columns.Add(column9);
            DataColumn column10 = new DataColumn("PlanTime", typeof(string));
            table.Columns.Add(column10);
            DataColumn column11 = new DataColumn("DeadLine", typeof(string));
            table.Columns.Add(column11);
            this.GridNPInfo.DataSource = this.InputDS.Tables[0].DefaultView;
            this.GridNPInfo.Columns["XH"].HeaderText = "序号";
            this.GridNPInfo.Columns["XH"].Width = 30;
            this.GridNPInfo.Columns["NPType"].HeaderText = "车牌类型";
            this.GridNPInfo.Columns["NPType"].Width = 60;
            this.GridNPInfo.Columns["NPNum"].HeaderText = "车牌号码";
            this.GridNPInfo.Columns["NPTypeID"].Visible = false;
            this.GridNPInfo.Columns["FrontPiece"].HeaderText = "前片";
            this.GridNPInfo.Columns["FrontPiece"].Width = 30;
            this.GridNPInfo.Columns["BackPiece"].HeaderText = "后片";
            this.GridNPInfo.Columns["BackPiece"].Width = 30;
            this.GridNPInfo.Columns["Mail"].HeaderText = "邮寄";
            this.GridNPInfo.Columns["Mail"].Width = 30;
            this.GridNPInfo.Columns["PlanID"].HeaderText = "计划单号";
            this.GridNPInfo.Columns["PlanDepart"].HeaderText = "计划单位";
            this.GridNPInfo.Columns["PlanTime"].HeaderText = "计划时间";
            this.GridNPInfo.Columns["PlanTime"].Width = 150;
            this.GridNPInfo.Columns["DeadLine"].HeaderText = "最后期限";
            this.GridNPInfo.Columns["DeadLine"].Width = 150;
        }

        private bool AutoFindNP(string StrPlan, DateTime PlanTime, string PlanDepart)
        {
            this.G_NPCount = 0;
            this.InputDS.Tables[0].Rows.Clear();
            for (int i = StrPlan.IndexOf("class=\"f1\">"); i > 0; i = StrPlan.IndexOf("class=\"f1\">"))
            {
                this.G_NPCount++;
                int index = StrPlan.IndexOf("</td>", i);
                string str = StrPlan.Substring(i + 11, (index - i) - 11);
                DataRow row = this.InputDS.Tables[0].Rows.Add(new object[0]);
                row["XH"] = this.G_NPCount;
                row["NPType"] = "大蓝";
                row["NPNum"] = str;
                row["NPTypeID"] = "B";
                row["FrontPiece"] = 1;
                row["BackPiece"] = 1;
                row["Mail"] = 0;
                row["PlanID"] = this.TextPlanID.Text.Substring(0, 12) + "-" + this.G_SubPlan.ToString("d2");
                row["PlanDepart"] = PlanDepart;
                row["PlanTime"] = PlanTime;
                row["DeadLine"] = PlanTime.AddDays(3.0);
                StrPlan = StrPlan.Substring(index);
            }
            return true;
        }

        private bool AutoSaveNP()
        {
            for (int i = 0; i < this.InputDS.Tables[0].Rows.Count; i++)
            {
                string queryStr = string.Concat(new object[] { "Insert into T_NP(PlanID,NPNo,NPType,frontPiece,backPiece,Mail) values('", this.InputDS.Tables[0].Rows[i][7], "','", this.InputDS.Tables[0].Rows[i][2], "','", this.InputDS.Tables[0].Rows[i][3], "','", this.InputDS.Tables[0].Rows[i]["FrontPiece"], "','", this.InputDS.Tables[0].Rows[i]["BackPiece"], "','", this.InputDS.Tables[0].Rows[i]["Mail"], "')" });
                this.ds = this.MyDB.Run_SqlText(queryStr);
                if (this.ds == null)
                {
                    MessageBox.Show("计划单：" + this.TextPlanID.Text + "不存在！请先保存计划单！");
                    this.Cursor = Cursors.Default;
                    return false;
                }
            }
            this.InputDS.Tables[0].Rows.Clear();
            return true;
        }

        private bool AutoSavePlan(DateTime PlanTime, int count, string PlanDepart)
        {
            string str;
            Hashtable hashtable = new Hashtable();
            DataSet set = this.MyDB.Run_SqlText("select * from v_depart");
            if (set != null)
            {
                for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                {
                    hashtable.Add(set.Tables[0].Rows[i]["description"].ToString(), set.Tables[0].Rows[i]["Code"].ToString());
                }
            }
            try
            {
                str = hashtable[PlanDepart].ToString();
            }
            catch (Exception)
            {
                int num2 = 0;
                while (hashtable.ContainsValue("3702" + num2.ToString("D2")) || (num2 > 0x63))
                {
                    num2++;
                }
                this.MyDB.Run_SqlText("insert into d_code(code,codetype,codename,description) values('3702" + num2.ToString("D2") + "','2','" + PlanDepart + "发放点','" + PlanDepart + "')");
                hashtable.Add(PlanDepart, "3702" + num2.ToString("D2"));
                str = hashtable[PlanDepart].ToString();
            }
            string queryStr = string.Concat(new object[] { 
                "INSERT INTO T_Plan(PlanID, PlanType, PlanTime, PlanDepart, TotalCount, DeadLine,InputUser,InPutTime,ReMark) VALUES( '", this.TextPlanID.Text.Substring(0, 12), "-", this.G_SubPlan.ToString("d2"), "','2','", PlanTime, "','", str, "',", count, ",'", PlanTime.AddDays(3.0), "','", this.G_UserID, "','", DateTime.Now, 
                "','')"
             });
            return (this.MyDB.Run_SqlText(queryStr) != null);
        }

        private void Btn_CheckMode_Click(object sender, EventArgs e)
        {
            if (!this.G_IsCheck && (this.GridNPInfo.Rows.Count > 1))
            {
                this.G_IsCheck = true;
                this.TextShowNPNum.Text = this.TextShowNPNum.Text.Substring(0, 2);
                this.GridNPInfo.Focus();
                this.GridNPInfo.CurrentCell = this.GridNPInfo.Rows[0].Cells[0];
                this.GridNPInfo.Rows[0].Selected = true;
                this.TextShowNPNum.Text = this.GridNPInfo.Rows[0].Cells[2].Value.ToString();
                this.GridNPInfo.AllowUserToDeleteRows = true;
                this.tabControl1.SelectedIndex = 0;
            }
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.NpWaringtoInputMode();
        }

        private void btnChar_Click(object sender, MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Left) && (this.textNpWord.Text == ""))
            {
                Button button = sender as Button;
                int num = Convert.ToInt16(button.Name.Substring(7, 2));
                if (!this.NPArr[this.G_Pos - 1, num - 1])
                {
                    this.NPArr[this.G_Pos - 1, num - 1] = true;
                    button.BackColor = Color.LightGreen;
                    switch (this.G_Pos)
                    {
                        case 1:
                            this.btnNumP1.Text = button.Text;
                            this.G_NPWord[0] = "";
                            return;

                        case 2:
                            this.btnNumP2.Text = button.Text;
                            this.G_NPWord[1] = "";
                            return;

                        case 3:
                            this.btnNumP3.Text = button.Text;
                            this.G_NPWord[2] = "";
                            return;

                        case 4:
                            this.btnNumP4.Text = button.Text;
                            this.G_NPWord[3] = "";
                            return;

                        case 5:
                            this.btnNumP5.Text = button.Text;
                            this.G_NPWord[4] = "";
                            return;
                    }
                }
                else
                {
                    this.NPArr[this.G_Pos - 1, num - 1] = false;
                    button.BackColor = Color.Red;
                    switch (this.G_Pos)
                    {
                        case 1:
                            this.btnNumP1.Text = "";
                            return;

                        case 2:
                            this.btnNumP2.Text = "";
                            return;

                        case 3:
                            this.btnNumP3.Text = "";
                            return;

                        case 4:
                            this.btnNumP4.Text = "";
                            return;

                        case 5:
                            this.btnNumP5.Text = "";
                            return;
                    }
                }
            }
        }

        private void BtnCreatePlan_Click(object sender, EventArgs e)
        {
            int num;
            try
            {
                if (this.TextTotalCount.Text == "")
                {
                    this.TextTotalCount.Focus();
                    return;
                }
                num = int.Parse(this.TextTotalCount.Text);
            }
            catch
            {
                this.TextTotalCount.Text = "";
                this.TextTotalCount.Focus();
                return;
            }
            if (!(this.TextPlanID.BackColor == Color.White))
            {
                string queryStr = string.Concat(new object[] { 
                    "INSERT INTO T_Plan(PlanID, PlanType, PlanTime, PlanDepart, TotalCount, DeadLine,InputUser,InPutTime,ReMark) VALUES( '", this.TextPlanID.Text, "','", this.CombPlanType.SelectedValue, "','", this.DatePlanTime.Value.ToShortDateString(), "','", this.CombPlanDepart.SelectedValue, "',", num, ",'", this.DateDeadLine.Value.ToShortDateString(), "','", this.G_UserID, "','", DateTime.Now, 
                    "','", this.rTextAddition.Text, "')"
                 });
                DataSet set = new DataSet();
                set = this.MyDB.Run_SqlText(queryStr);
                if (set == null)
                {
                    this.CountPlanID();
                    if (this.checkIsLargPlan.Checked)
                    {
                        this.TextPlanID.Text = "P" + DateTime.Now.ToString("yyyyMMdd") + this.G_PlanID.ToString("D3") + "-" + this.G_SubPlan.ToString("D2");
                    }
                    else
                    {
                        this.TextPlanID.Text = "P" + DateTime.Now.ToString("yyyyMMdd") + this.G_PlanID.ToString("D3");
                    }
                    queryStr = string.Concat(new object[] { 
                        "INSERT INTO T_Plan(PlanID, PlanType, PlanTime, PlanDepart, TotalCount, DeadLine,InputUser,InPutTime,ReMark) VALUES( '", this.TextPlanID.Text, "','", this.CombPlanType.SelectedValue, "','", this.DatePlanTime.Value, "','", this.CombPlanDepart.SelectedValue, "',", num, ",'", this.DateDeadLine.Value, "','", this.G_UserID, "','", DateTime.Now, 
                        "','", this.rTextAddition.Text, "')"
                     });
                    set = this.MyDB.Run_SqlText(queryStr);
                }
                if (set != null)
                {
                    this.TextPlanID.BackColor = Color.White;
                    MessageBox.Show(this, "计划单添加成功！", "计划单输入", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show(this, "计划单添加不成功！", "计划单输入", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            else
            {
                MessageBox.Show(this, "该计划单已保存！", "计划单输入", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnFW_Click(object sender, EventArgs e)
        {
            int[] numArray = new int[6];
            int index = 0;
            string text = this.textFWDefine.Text;
            while ((text != "") && (index < 7))
            {
                int length = text.IndexOf(",");
                if (length > 0)
                {
                    index++;
                    switch (text.Substring(0, length))
                    {
                        case "个位":
                            numArray[index] = 0;
                            break;

                        case "十位":
                            numArray[index] = 1;
                            break;

                        case "百位":
                            numArray[index] = 2;
                            break;

                        case "千位":
                            numArray[index] = 3;
                            break;

                        case "万位":
                            numArray[index] = 4;
                            break;

                        case "十万位":
                            numArray[index] = 5;
                            break;

                        default:
                            this.textFWDefine.Text = "";
                            index = 0;
                            break;
                    }
                    text = text.Substring(length + 1, (text.Length - length) - 1);
                    continue;
                }
                this.textFWDefine.Text = "";
                return;
            }
            if (this.textPCNPNum.Text != "")
            {
                if (this.textPCNPNum.Text.Length > index)
                {
                    MessageBox.Show(this, "请输入" + index + "位数的排除号码");
                }
                else
                {
                    this.PCNPNum.Add(this.textPCNPNum.Text);
                    this.textPCNPNum.Text = "";
                }
            }
            if ((this.textFWBegin.Text != "") || (this.textFWEnd.Text != ""))
            {
                if ((this.textFWBegin.Text.Length > index) || (this.textFWBegin.Text.Length == 0))
                {
                    MessageBox.Show(this, "请输入小于" + index + "位的最小值");
                }
                else if (this.textFWEnd.Text.Length != index)
                {
                    MessageBox.Show(this, "请输入" + index + "位的最大值");
                }
                else if (index > 0)
                {
                    if ((this.textFWBegin.Text.CompareTo("A") >= 0) && (this.textFWBegin.Text.CompareTo("Z") <= 0))
                    {
                        if (index > 1)
                        {
                            this.textFWDefine.Text = "";
                            this.textFWBegin.Text = "";
                            this.textFWEnd.Text = "";
                            return;
                        }
                        for (int j = 10; j < 0x24; j++)
                        {
                            if ((this.NPChar[j].CompareTo(this.textFWBegin.Text) >= 0) && (this.NPChar[j].CompareTo(this.textFWEnd.Text) <= 0))
                            {
                                this.NPArr[numArray[0], j] = true;
                            }
                        }
                    }
                    else
                    {
                        for (int k = Convert.ToInt16(this.textFWBegin.Text); k < (Convert.ToInt16(this.textFWEnd.Text) + 1); k++)
                        {
                            text = k.ToString("D" + index);
                            for (int m = 1; m < (index + 1); m++)
                            {
                                this.NPArr[numArray[m], Convert.ToInt16(text.Substring(m - 1, 1))] = true;
                            }
                        }
                        this.G_NPEnd = Convert.ToInt16(this.textFWEnd.Text);
                        this.G_NPBegin = Convert.ToInt16(this.textFWBegin.Text);
                    }
                    for (int i = 1; i < (index + 1); i++)
                    {
                        this.G_FWPos = this.G_FWPos + numArray[i].ToString();
                    }
                    this.textFWBegin.Text = "";
                    this.textFWEnd.Text = "";
                }
            }
        }

        private void btnIgnore_Click(object sender, EventArgs e)
        {
            this.AddOneNP();
            this.NpWaringtoInputMode();
        }

        private void btnImportFromHtm_Click(object sender, EventArgs e)
        {
            if ((this.InputDS.Tables[0].Rows.Count <= 0) || (MessageBox.Show(this, "计划单数据尚未保存，是否确定导入？", "计划单输入", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.No))
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.ShowDialog();
                if (dialog.FileName.Length != 0)
                {
                    DateTime time;
                    StreamReader reader = new StreamReader(dialog.FileName, Encoding.GetEncoding("gb2312"));
                    string str = reader.ReadToEnd();
                    int index = str.IndexOf("号牌选定时间");
                    str = str.Substring(index);
                    index = str.IndexOf("value=\"");
                    str = str.Substring(index + 7);
                    int length = str.IndexOf("\"");
                    DateTime.TryParse(str.Substring(0, length), out time);
                    this.G_SubPlan = 0;
                    int count = 0;
                    int num6 = 0;
                    int startIndex = str.IndexOf("发牌点需做号牌情况如下：");
                    if (startIndex == -1)
                    {
                        startIndex = str.IndexOf(" 需做号牌情况如下：");
                    }
                    while (startIndex > 0)
                    {
                        int num7 = 0;
                        while (str.Substring(startIndex - num7, 1).CompareTo("\t") != 0)
                        {
                            num7++;
                        }
                        string planDepart = str.Substring((startIndex - num7) + 1, num7 - 1).Trim();
                        int num4 = str.IndexOf("&nbsp;副", 3);
                        string strPlan = str.Substring(startIndex, num4 - startIndex);
                        num6 = str.IndexOf("共计&nbsp;");
                        count = int.Parse(str.Substring(num6 + 8, (num4 - num6) - 8));
                        if ((startIndex > 0) && (num4 > 0))
                        {
                            this.G_SubPlan++;
                            this.AutoFindNP(strPlan, time, planDepart);
                            startIndex = str.Substring(num4).IndexOf("发牌点需做号牌情况如下：");
                            DialogResult result = MessageBox.Show(this, string.Concat(new object[] { "导入第", this.G_SubPlan, "个子计划单! ", planDepart, count.ToString(), "副车牌" }), "计划单录入", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
                            if (result != DialogResult.No)
                            {
                                if (result == DialogResult.Cancel)
                                {
                                    break;
                                }
                                if (this.AutoSavePlan(time, count, planDepart))
                                {
                                    this.AutoSaveNP();
                                }
                                else
                                {
                                    MessageBox.Show(this, "htm文件导入错误！", "计划单录入", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                }
                            }
                        }
                    }
                    this.CountPlanID();
                    this.checkIsLargPlan.Checked = false;
                    this.TextPlanID.Text = "P" + DateTime.Now.ToString("yyyyMMdd") + this.G_PlanID.ToString("D3");
                    this.TextPlanID.BackColor = Color.Red;
                    reader.Close();
                }
            }
        }

        private void btnInputMode_Click(object sender, EventArgs e)
        {
            if (this.G_IsCheck)
            {
                this.G_IsCheck = false;
                this.tabControl1.SelectedIndex = 0;
            }
        }

        private void BtnLargInput_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 1;
        }

        private void btnLargInputCacel_Click(object sender, EventArgs e)
        {
            this.ClearLargInputPanel();
        }

        private void btnLargInputOK_Click(object sender, EventArgs e)
        {
            int num;
            this.Cursor = Cursors.WaitCursor;
            int[] numArray = new int[6];
            int[,] numArray2 = new int[6, 0x24];
            for (num = 0; num < 6; num++)
            {
                numArray[num] = 0;
                for (int j = 0; j < 0x24; j++)
                {
                    if (this.NPArr[num, j])
                    {
                        numArray2[num, numArray[num]] = j;
                        numArray[num]++;
                    }
                }
            }
            for (num = 0; num < 5; num++)
            {
                if ((this.G_NPWord[num] != null) && (this.G_NPWord[num] != ""))
                {
                    numArray[num] = 1;
                }
            }
            for (int i = 0; i < numArray[0]; i++)
            {
                for (int k = 0; k < numArray[1]; k++)
                {
                    for (int m = 0; m < numArray[2]; m++)
                    {
                        for (int n = 0; n < numArray[3]; n++)
                        {
                            for (int num7 = 0; num7 < numArray[4]; num7++)
                            {
                                for (int num8 = 0; num8 < numArray[5]; num8++)
                                {
                                    string str = "";
                                    string str2 = "";
                                    if (this.G_FWPos.Length > 0)
                                    {
                                        for (int num9 = 0; num9 < this.G_FWPos.Length; num9++)
                                        {
                                            string str4 = this.G_FWPos.Substring(num9, 1);
                                            if (str4 != null)
                                            {
                                                if (!(str4 == "0"))
                                                {
                                                    if (str4 == "1")
                                                    {
                                                        goto Label_017E;
                                                    }
                                                    if (str4 == "2")
                                                    {
                                                        goto Label_0199;
                                                    }
                                                    if (str4 == "3")
                                                    {
                                                        goto Label_01B4;
                                                    }
                                                    if (str4 == "4")
                                                    {
                                                        goto Label_01CF;
                                                    }
                                                    if (str4 == "5")
                                                    {
                                                        goto Label_01EA;
                                                    }
                                                }
                                                else
                                                {
                                                    str = str + this.NPChar[numArray2[0, i]];
                                                }
                                            }
                                            continue;
                                        Label_017E:
                                            str = str + this.NPChar[numArray2[1, k]];
                                            continue;
                                        Label_0199:
                                            str = str + this.NPChar[numArray2[2, m]];
                                            continue;
                                        Label_01B4:
                                            str = str + this.NPChar[numArray2[3, n]];
                                            continue;
                                        Label_01CF:
                                            str = str + this.NPChar[numArray2[4, num7]];
                                            continue;
                                        Label_01EA:
                                            str = str + this.NPChar[numArray2[5, num8]];
                                        }
                                        if ((Convert.ToInt32(str) < Convert.ToInt32(this.G_NPBegin)) || (Convert.ToInt32(str) > Convert.ToInt32(this.G_NPEnd)))
                                        {
                                            continue;
                                        }
                                        bool flag = false;
                                        if ((this.PCNPNum != null) && (this.PCNPNum.Count > 0))
                                        {
                                            foreach (string str3 in this.PCNPNum)
                                            {
                                                if (str.CompareTo(str3) == 0)
                                                {
                                                    flag = true;
                                                }
                                            }
                                            if (flag)
                                            {
                                                continue;
                                            }
                                        }
                                    }
                                    str2 = "鲁" + this.NPChar[numArray2[5, num8]];
                                    if ((this.G_NPWord[4] != null) && (this.G_NPWord[4] != ""))
                                    {
                                        str2 = str2 + this.G_NPWord[4];
                                    }
                                    else
                                    {
                                        str2 = str2 + this.NPChar[numArray2[4, num7]];
                                    }
                                    if ((this.G_NPWord[3] != null) && (this.G_NPWord[3] != ""))
                                    {
                                        str2 = str2 + this.G_NPWord[3];
                                    }
                                    else
                                    {
                                        str2 = str2 + this.NPChar[numArray2[3, n]];
                                    }
                                    if ((this.G_NPWord[2] != null) && (this.G_NPWord[2] != ""))
                                    {
                                        str2 = str2 + this.G_NPWord[2];
                                    }
                                    else
                                    {
                                        str2 = str2 + this.NPChar[numArray2[2, m]];
                                    }
                                    if ((this.G_NPWord[1] != null) && (this.G_NPWord[1] != ""))
                                    {
                                        str2 = str2 + this.G_NPWord[1];
                                    }
                                    else
                                    {
                                        str2 = str2 + this.NPChar[numArray2[1, k]];
                                    }
                                    if ((this.G_NPWord[0] != null) && (this.G_NPWord[0] != ""))
                                    {
                                        str2 = str2 + this.G_NPWord[0];
                                    }
                                    else
                                    {
                                        str2 = str2 + this.NPChar[numArray2[0, i]];
                                    }
                                    this.G_NPCount++;
                                    DataRow row = this.InputDS.Tables[0].Rows.Add(new object[0]);
                                    row["XH"] = this.G_NPCount;
                                    row["NPType"] = this.CombNPType.Text;
                                    row["NPNum"] = str2;
                                    row["NPTypeID"] = this.CombNPType.SelectedValue;
                                    row["FrontPiece"] = Convert.ToInt32(this.checkFontPiece.Checked).ToString();
                                    row["BackPiece"] = Convert.ToInt32(this.checkBackPiece.Checked).ToString();
                                    row["PlanID"] = this.TextPlanID.Text;
                                    row["PlanDepart"] = this.CombPlanDepart.Text;
                                    row["PlanTime"] = this.DatePlanTime.Value;
                                    row["DeadLine"] = this.DateDeadLine.Value;
                                }
                            }
                        }
                    }
                }
            }
            this.GridNPInfo.Refresh();
            this.ClearLargInputPanel();
            this.Cursor = Cursors.Default;
        }

        private void btnLargPlanOK_Click(object sender, EventArgs e)
        {
            this.labLargPlanTotal.Text = "";
            int num = 0;
            int num2 = 0;
            try
            {
                Convert.ToInt32(this.textLargPlanCount.Text.Trim());
            }
            catch
            {
                this.textLargPlanCount.Text = "";
                return;
            }
            if (this.G_LargPlanCount.ContainsKey(this.combLargPlanNPType.SelectedValue))
            {
                this.G_LargPlanCount.Remove(this.combLargPlanNPType.SelectedValue.ToString());
            }
            LargPlanInfo info = new LargPlanInfo {
                count = Convert.ToInt32(this.textLargPlanCount.Text.Trim()),
                NPType = this.combLargPlanNPType.Text
            };
            this.G_LargPlanCount.Add(this.combLargPlanNPType.SelectedValue, info);
            foreach (LargPlanInfo info2 in this.G_LargPlanCount.Values)
            {
                num += info2.count;
                string text = this.labLargPlanTotal.Text;
                this.labLargPlanTotal.Text = text + "[" + info2.NPType + ":" + info2.count.ToString() + "] ";
                num2++;
                if ((num2 % 3) == 0)
                {
                    this.labLargPlanTotal.Text = this.labLargPlanTotal.Text + "\r";
                }
            }
            this.labLargPlanTotal.Text = this.labLargPlanTotal.Text + "总数：" + num.ToString();
        }

        private void btnNumP_Click(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            int num = Convert.ToInt16(button.Name.Substring(7, 1));
            switch (this.G_Pos)
            {
                case 1:
                    this.btnNumP1.BackColor = Color.LightGreen;
                    break;

                case 2:
                    this.btnNumP2.BackColor = Color.LightGreen;
                    break;

                case 3:
                    this.btnNumP3.BackColor = Color.LightGreen;
                    break;

                case 4:
                    this.btnNumP4.BackColor = Color.LightGreen;
                    break;

                case 5:
                    this.btnNumP5.BackColor = Color.LightGreen;
                    break;
            }
            if (this.textNpWord.Text.Length > 0)
            {
                if (Encoding.Default.GetBytes(this.textNpWord.Text.Substring(0, 1)).Length <= 1)
                {
                    this.textNpWord.Text = "";
                }
                else
                {
                    this.textNpWord.Text = this.textNpWord.Text.Substring(0, 1);
                    switch (this.G_Pos)
                    {
                        case 1:
                            this.btnNumP1.Text = this.textNpWord.Text.Trim();
                            this.G_NPWord[0] = this.textNpWord.Text.Trim();
                            break;

                        case 2:
                            this.btnNumP2.Text = this.textNpWord.Text.Trim();
                            this.G_NPWord[1] = this.textNpWord.Text.Trim();
                            break;

                        case 3:
                            this.btnNumP3.Text = this.textNpWord.Text.Trim();
                            this.G_NPWord[2] = this.textNpWord.Text.Trim();
                            break;

                        case 4:
                            this.btnNumP4.Text = this.textNpWord.Text.Trim();
                            this.G_NPWord[3] = this.textNpWord.Text.Trim();
                            break;

                        case 5:
                            this.btnNumP5.Text = this.textNpWord.Text.Trim();
                            this.G_NPWord[4] = this.textNpWord.Text.Trim();
                            break;
                    }
                    for (int i = this.NPArr.GetLowerBound(1); i < (this.NPArr.GetUpperBound(1) + 1); i++)
                    {
                        this.NPArr[this.G_Pos - 1, i] = false;
                    }
                    this.textNpWord.Text = "";
                }
            }
            this.G_Pos = num;
            button.BackColor = Color.Red;
            if ((this.G_NPWord[num - 1] != "") && (this.G_NPWord[num - 1] != null))
            {
                this.textNpWord.Text = this.G_NPWord[num - 1];
            }
            if (e.Button == MouseButtons.Left)
            {
                foreach (Control control in this.groupChar.Controls)
                {
                    if (control is Button)
                    {
                        int num3 = Convert.ToInt32(control.Name.Substring(7, 2));
                        if (num3 < 0x22)
                        {
                            if (!this.NPArr[num - 1, num3 - 1])
                            {
                                control.BackColor = Color.Red;
                            }
                            else
                            {
                                control.BackColor = Color.LightGreen;
                            }
                        }
                    }
                }
                return;
            }
            if (e.Button == MouseButtons.Right)
            {
                if (num == 6)
                {
                    return;
                }
                num = Convert.ToInt16(button.Name.Substring(7, 1));
                if (button.FlatStyle != FlatStyle.Standard)
                {
                    switch (this.G_Pos)
                    {
                        case 1:
                            this.G_FWlist.Remove("个位");
                            goto Label_04EB;

                        case 2:
                            this.G_FWlist.Remove("十位");
                            goto Label_04EB;

                        case 3:
                            this.G_FWlist.Remove("百位");
                            goto Label_04EB;

                        case 4:
                            this.G_FWlist.Remove("千位");
                            goto Label_04EB;

                        case 5:
                            this.G_FWlist.Remove("万位");
                            goto Label_04EB;
                    }
                    goto Label_04EB;
                }
                switch (this.G_Pos)
                {
                    case 1:
                        this.G_FWlist.Add("个位");
                        break;

                    case 2:
                        this.G_FWlist.Add("十位");
                        break;

                    case 3:
                        this.G_FWlist.Add("百位");
                        break;

                    case 4:
                        this.G_FWlist.Add("千位");
                        break;

                    case 5:
                        this.G_FWlist.Add("万位");
                        break;
                }
                this.textFWDefine.Text = this.textFWDefine.Text + this.G_FWlist[this.G_FWlist.Count - 1].ToString() + ",";
                button.FlatStyle = FlatStyle.Popup;
            }
            return;
        Label_04EB:
            this.textFWDefine.Text = "";
            foreach (string str in this.G_FWlist)
            {
                this.textFWDefine.Text = this.textFWDefine.Text + str + ",";
            }
            button.FlatStyle = FlatStyle.Standard;
        }

        private void btnNumP6_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.btnNumP6.Text == "B")
            {
                this.btnNumP6.Text = "U";
                this.NPArr[5, 11] = false;
                this.NPArr[5, 0x1c] = true;
            }
            else
            {
                this.btnNumP6.Text = "B";
                this.NPArr[5, 11] = true;
                this.NPArr[5, 0x1c] = false;
            }
        }

        private void BtnSaveNP_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                try
                {
                    if ((this.InputDS.Tables[0].Rows.Count < Convert.ToInt32(this.TextTotalCount.Text)) && (MessageBox.Show(this, "输入的车牌总数小于计划单中总数！确定要保存！", "通产车牌生产", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No))
                    {
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show(this, "输入计划单中车牌总数！", "通产车牌生产", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.Cursor = Cursors.Default;
                    return;
                }
                string queryStr = "";
                queryStr = "delete from T_tempNP";
                this.ds = this.MyDB.Run_SqlText(queryStr);
                queryStr = "Update T_Plan set remark='" + this.rTextAddition.Text.Trim() + "' where Planid='" + this.TextPlanID.Text + "'";
                this.ds = this.MyDB.Run_SqlText(queryStr);
                if (this.ds == null)
                {
                    MessageBox.Show("计划单：" + this.TextPlanID.Text + "不存在！请先保存计划单！");
                    this.Cursor = Cursors.Default;
                    return;
                }
                for (int i = 0; i < this.InputDS.Tables[0].Rows.Count; i++)
                {
                    queryStr = string.Concat(new object[] { "Insert into T_NP(PlanID,NPNo,NPType,frontPiece,backPiece,Mail) values('", this.TextPlanID.Text, "','", this.InputDS.Tables[0].Rows[i][2], "','", this.InputDS.Tables[0].Rows[i][3], "','", this.InputDS.Tables[0].Rows[i]["FrontPiece"], "','", this.InputDS.Tables[0].Rows[i]["BackPiece"], "','", this.InputDS.Tables[0].Rows[i]["Mail"], "')" });
                    this.ds = this.MyDB.Run_SqlText(queryStr);
                    if (this.ds == null)
                    {
                        MessageBox.Show("计划单：" + this.TextPlanID.Text + "不存在！请先保存计划单！");
                        this.Cursor = Cursors.Default;
                        return;
                    }
                }
                MessageBox.Show("计划单：" + this.TextPlanID.Text + "的车牌信息添加成功！");
                if (this.checkIsLargPlan.Checked)
                {
                    bool flag = false;
                    int num2 = 0;
                    int num3 = 0;
                    string str2 = "";
                    foreach (LargPlanInfo info in this.G_LargPlanCount.Values)
                    {
                        num3 += info.count;
                    }
                    str2 = "SELECT NPType, COUNT(NPType) FROM T_NP WHERE (LEFT(PlanID, 12) = '" + this.TextPlanID.Text.Substring(0, 12) + "') GROUP BY NPType";
                    this.ds = this.MyDB.Run_SqlText(str2);
                    if ((this.ds != null) && (this.ds.Tables[0].Rows.Count > 0))
                    {
                        for (int j = 0; j < this.ds.Tables[0].Rows.Count; j++)
                        {
                            num2 += Convert.ToInt32(this.ds.Tables[0].Rows[j][1].ToString());
                            flag = false;
                            for (int k = 0; k < this.G_LargPlanCount.Count; k++)
                            {
                                if (this.G_LargPlanCount.ContainsKey(this.ds.Tables[0].Rows[j][0].ToString()))
                                {
                                    LargPlanInfo info2 = (LargPlanInfo) this.G_LargPlanCount[this.ds.Tables[0].Rows[j][0].ToString()];
                                    if (info2.count < Convert.ToInt32(this.ds.Tables[0].Rows[j][1].ToString()))
                                    {
                                        MessageBox.Show(this, string.Concat(new object[] { "输入的该类型", info2.NPType, " 车牌总数", this.ds.Tables[0].Rows[j][1].ToString(), "与总单中", info2.count, "不一致！" }), "通产车牌生产", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                        flag = true;
                                        break;
                                    }
                                }
                            }
                        }
                        if (num2 > num3)
                        {
                            MessageBox.Show(this, "输入的车牌总数" + num2.ToString() + "与总单中" + num3.ToString() + "的不一致！", "通产车牌生产", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            flag = true;
                            this.TextPlanID.BackColor = Color.Red;
                            this.G_SubPlan++;
                            this.TextPlanID.Text = "P" + DateTime.Now.ToString("yyyyMMdd") + this.G_PlanID.ToString("D3") + "-" + this.G_SubPlan.ToString("D2");
                        }
                        else if (num2 == num3)
                        {
                            this.G_LargPlanCount.Clear();
                            this.labLargPlanTotal.Text = "";
                            this.TextPlanID.BackColor = Color.Red;
                            this.CountPlanID();
                            this.G_SubPlan = 1;
                            this.TextPlanID.Text = "P" + DateTime.Now.ToString("yyyyMMdd") + this.G_PlanID.ToString("D3") + "-" + this.G_SubPlan.ToString("D2");
                        }
                        else
                        {
                            this.TextPlanID.BackColor = Color.Red;
                            this.G_SubPlan++;
                            this.TextPlanID.Text = "P" + DateTime.Now.ToString("yyyyMMdd") + this.G_PlanID.ToString("D3") + "-" + this.G_SubPlan.ToString("D2");
                        }
                    }
                }
                else
                {
                    this.CountPlanID();
                    this.TextPlanID.Text = "P" + DateTime.Now.ToString("yyyyMMdd") + this.G_PlanID.ToString("D3");
                }
            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;
            }
            this.TextTotalCount.Text = "";
            this.InputDS.Tables[0].Clear();
            this.checkBoxMail.Checked = false;
            this.G_IsCheck = false;
            this.G_NPCount = 0;
            this.G_GridNPInfoLastIndex = 1;
            this.Cursor = Cursors.Default;
        }

        private void btnSaveNPTemp_Click(object sender, EventArgs e)
        {
            string queryStr = "";
            queryStr = "delete T_TempNP";
            this.ds = this.MyDB.Run_SqlText(queryStr);
            if (this.ds == null)
            {
                MessageBox.Show("保存不成功！");
            }
            else
            {
                for (int i = 0; i < this.InputDS.Tables[0].Rows.Count; i++)
                {
                    queryStr = string.Concat(new object[] { "Insert into T_TempNP(PlanID,NPNo,NPType,frontPiece,backPiece,Mail) values('", this.TextPlanID.Text, "','", this.InputDS.Tables[0].Rows[i][2], "','", this.InputDS.Tables[0].Rows[i][3], "','", this.InputDS.Tables[0].Rows[i]["FrontPiece"], "','", this.InputDS.Tables[0].Rows[i]["BackPiece"], "','", this.InputDS.Tables[0].Rows[i]["Mail"], "')" });
                    this.ds = this.MyDB.Run_SqlText(queryStr);
                    if (this.ds == null)
                    {
                        MessageBox.Show("计划单：" + this.TextPlanID.Text + "不存在！请先保存计划单！");
                        return;
                    }
                }
                MessageBox.Show("保存成功！");
            }
        }

        private void btnUpdatePlanID_Click(object sender, EventArgs e)
        {
            if ((this.ds.Tables[0].Rows[this.GridSameNPInfo.CurrentCell.RowIndex]["计划单位"].ToString().CompareTo("瑞华公司") == 0) && (this.ds.Tables[0].Rows[this.GridSameNPInfo.CurrentCell.RowIndex]["计划类型"].ToString().CompareTo("加急") == 0))
            {
                string queryStr = "Update T_NP set PlanID='" + this.TextPlanID.Text.Trim() + "',PlanID1='" + this.ds.Tables[0].Rows[this.GridSameNPInfo.CurrentCell.RowIndex]["计划单号"].ToString() + "' where PlanID='" + this.ds.Tables[0].Rows[this.GridSameNPInfo.CurrentCell.RowIndex]["计划单号"].ToString() + "' and npno='" + this.ds.Tables[0].Rows[this.GridSameNPInfo.CurrentCell.RowIndex]["车牌号"].ToString() + "'";
                DataSet set = new DataSet();
                if (this.MyDB.Run_SqlText(queryStr) == null)
                {
                    MessageBox.Show(this, "转单失败！请先保存计划单！", "计划单输入", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                else
                {
                    this.NpWaringtoInputMode();
                }
            }
            else
            {
                MessageBox.Show(this, "不是内部单，转单失败！", "计划单输入", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void checkIsLargPlan_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void checkIsLargPlan_Click(object sender, EventArgs e)
        {
            if (this.checkIsLargPlan.Checked)
            {
                this.G_LargPlanCount.Clear();
                this.G_SubPlan = 1;
                this.TextPlanID.Text = this.TextPlanID.Text.Substring(0, 12) + "-" + this.G_SubPlan.ToString("D2");
                this.textLargPlanCount.Enabled = true;
                this.combLargPlanNPType.Enabled = true;
                this.btnLargPlanExit.Enabled = true;
                this.btnLargPlanOK.Enabled = true;
            }
            else if (MessageBox.Show(this, "大单输入中，是否要退出？", "计划单录入", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.No)
            {
                this.checkIsLargPlan.Checked = true;
            }
            else
            {
                bool flag = false;
                int num = 0;
                int num2 = 0;
                string queryStr = "";
                foreach (LargPlanInfo info in this.G_LargPlanCount.Values)
                {
                    num2 += info.count;
                }
                queryStr = "SELECT NPType, COUNT(NPType) FROM T_NP WHERE (LEFT(PlanID, 12) = '" + this.TextPlanID.Text.Substring(0, 12) + "') GROUP BY NPType";
                this.ds = this.MyDB.Run_SqlText(queryStr);
                if ((this.ds != null) && (this.ds.Tables[0].Rows.Count > 0))
                {
                    for (int i = 0; i < this.ds.Tables[0].Rows.Count; i++)
                    {
                        num += Convert.ToInt32(this.ds.Tables[0].Rows[i][1].ToString());
                        flag = false;
                        if (this.G_LargPlanCount.ContainsKey(this.ds.Tables[0].Rows[i][0].ToString()))
                        {
                            LargPlanInfo info2 = (LargPlanInfo) this.G_LargPlanCount[this.ds.Tables[0].Rows[i][0].ToString()];
                            if (info2.count < Convert.ToInt32(this.ds.Tables[0].Rows[i][1].ToString()))
                            {
                                MessageBox.Show(this, string.Concat(new object[] { "输入的该类型", info2.NPType, " 车牌总数", this.ds.Tables[0].Rows[i][1].ToString(), "与总单中", info2.count, "不一致！" }), "通产车牌生产", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                flag = true;
                                break;
                            }
                        }
                    }
                    if (num != num2)
                    {
                        MessageBox.Show(this, "输入的车牌总数" + num.ToString() + "与总单中" + num2.ToString() + "的不一致！", "通产车牌生产", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        flag = true;
                    }
                    this.CountPlanID();
                }
                this.G_SubPlan = 0;
                this.G_LargPlanCount.Clear();
                this.labLargPlanTotal.Text = "";
                this.textLargPlanCount.Enabled = false;
                this.combLargPlanNPType.Enabled = false;
                this.btnLargPlanExit.Enabled = false;
                this.btnLargPlanOK.Enabled = false;
                this.TextPlanID.Text = "P" + DateTime.Now.ToString("yyyyMMdd") + this.G_PlanID.ToString("D3");
            }
        }

        private void ClearLargInputPanel()
        {
            this.textFWBegin.Text = "";
            this.textFWDefine.Text = "";
            this.textFWEnd.Text = "";
            this.textNpWord.Text = "";
            this.textPCNPNum.Text = "";
            for (int i = 0; i < 7; i++)
            {
                for (int k = 0; k < 0x24; k++)
                {
                    this.NPArr[i, k] = false;
                }
            }
            foreach (Control control in this.groupChar.Controls)
            {
                if ((control is Button) && (Convert.ToInt32(control.Name.Substring(7, 2)) < 0x22))
                {
                    control.BackColor = Color.Red;
                }
            }
            this.btnNumP6.Text = "B";
            this.NPArr[5, 11] = true;
            this.btnNumP1.BackColor = Color.FromKnownColor(KnownColor.Control);
            this.btnNumP1.FlatStyle = FlatStyle.Standard;
            this.btnNumP1.Text = "";
            this.btnNumP2.BackColor = Color.FromKnownColor(KnownColor.Control);
            this.btnNumP2.FlatStyle = FlatStyle.Standard;
            this.btnNumP2.Text = "";
            this.btnNumP3.BackColor = Color.FromKnownColor(KnownColor.Control);
            this.btnNumP3.FlatStyle = FlatStyle.Standard;
            this.btnNumP3.Text = "";
            this.btnNumP4.BackColor = Color.FromKnownColor(KnownColor.Control);
            this.btnNumP4.FlatStyle = FlatStyle.Standard;
            this.btnNumP4.Text = "";
            this.btnNumP5.BackColor = Color.FromKnownColor(KnownColor.Control);
            this.btnNumP5.FlatStyle = FlatStyle.Standard;
            this.btnNumP5.Text = "";
            this.G_FWPos = "";
            this.G_NPBegin = 0;
            this.G_NPEnd = 0;
            this.PCNPNum.Clear();
            this.G_FWlist.Clear();
            for (int j = 0; j < 6; j++)
            {
                this.G_NPWord[j] = "";
            }
            this.G_Pos = 5;
            if (this.CombNPType.Text == "挂车")
            {
                this.btnNumP1.Text = "挂";
                this.G_NPWord[0] = "挂";
                this.textNpWord.Text = "挂";
                this.G_Pos = 1;
            }
            if (this.CombNPType.Text == "领馆")
            {
                this.btnNumP1.Text = "领";
                this.G_NPWord[0] = "领";
                this.textNpWord.Text = "领";
                this.G_Pos = 1;
            }
            if (this.CombNPType.Text == "教练")
            {
                this.btnNumP1.Text = "学";
                this.G_NPWord[0] = "学";
                this.textNpWord.Text = "学";
                this.G_Pos = 1;
            }
            if (this.CombNPType.Text == "境外")
            {
                this.btnNumP1.Text = "境";
                this.G_NPWord[0] = "境";
                this.textNpWord.Text = "境";
                this.G_Pos = 1;
            }
        }

        private void CombNPType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.checkBackPiece.Checked = true;
            if (this.CombNPType.Text.CompareTo("挂车") == 0)
            {
                this.checkFontPiece.Checked = false;
            }
            else
            {
                this.checkFontPiece.Checked = true;
            }
            if (this.CombNPType.Text.Substring(0, 2).CompareTo("其它") == 0)
            {
                this.textNPTemplate.Text = "";
                this.TextShowNPNum.Text = "";
            }
            else
            {
                this.textNPTemplate.Text = "鲁B";
                this.TextShowNPNum.Text = "鲁B";
            }
            this.ClearLargInputPanel();
        }

        private void CombPlanDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.CombPlanDepart.Text.CompareTo("瑞华公司") == 0) && (this.CombPlanType.SelectedValue.ToString().CompareTo("4") == 0))
            {
                this.checkBackPiece.Enabled = true;
                this.checkFontPiece.Enabled = true;
            }
            else
            {
                this.checkFontPiece.Checked = true;
                this.checkBackPiece.Checked = true;
                this.checkBackPiece.Enabled = false;
                this.checkFontPiece.Enabled = false;
            }
        }

        private void CombPlanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CombPlanType.Text == "加急")
            {
                this.DateDeadLine.Value = DateTime.Now;
                this.DateDeadLine.Enabled = false;
            }
            else if (this.CombPlanType.Text == "自选号")
            {
                this.DateDeadLine.Value = DateTime.Now.AddDays(2.0);
                this.DateDeadLine.Enabled = false;
            }
            else
            {
                this.DateDeadLine.Enabled = true;
                this.DateDeadLine.Focus();
            }
            if ((this.CombPlanDepart.Text.CompareTo("瑞华公司") == 0) && (this.CombPlanType.SelectedValue.ToString().CompareTo("4") == 0))
            {
                this.checkBackPiece.Enabled = true;
                this.checkFontPiece.Enabled = true;
            }
            else
            {
                this.checkBackPiece.Checked = true;
                this.checkFontPiece.Checked = true;
                this.checkBackPiece.Enabled = false;
                this.checkFontPiece.Enabled = false;
            }
        }

        private int ComPareDBNP(string NpNum, string NpType)
        {
            string queryStr = "";
            queryStr = "Select * from T_NP where NPNo='" + NpNum + "' and NPType='" + NpType + "'";
            this.ds = this.MyDB.Run_SqlText(queryStr);
            if (this.ds.Tables[0].Rows.Count > 0)
            {
                queryStr = "SELECT V_NpType.Description AS 车牌类型, V_NP.NpNo AS 车牌号, V_Plan.PlanKind AS 计划类型, V_Plan.PlanDepart AS 计划单位, V_Plan.Remark as 备注, V_NP.SendID AS 送货单号, V_NP.TaskID AS 生产任务号, V_NP.PlanID AS 计划单号, V_NP.PlanTime AS 录入时间 FROM V_NP INNER JOIN V_NpType ON V_NP.Code = V_NpType.Code INNER JOIN V_Plan ON V_NP.PlanID = V_Plan.PlanID WHERE V_NP.NPno='" + NpNum + "' and V_NP.Code='" + NpType + "'";
                this.ds = this.MyDB.Run_SqlText(queryStr);
                this.GridSameNPInfo.DataSource = this.ds.Tables[0];
                return 2;
            }
            return 0;
        }

        private void CountPlanID()
        {
            string str = "";
            Hashtable hT = new Hashtable();
            str = this.MyDB.RunProcedureNoRecord("@NewPlanID", SqlDbType.Char, hT, "P_CreatePlanID").Trim();
            this.TextPlanID.BackColor = Color.Red;
            this.G_PlanID = Convert.ToInt32(str);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Frm_Input_FontChanged(object sender, EventArgs e)
        {
            m_Instance = null;
        }

        private void Frm_Input_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_Instance = null;
        }

        private void Frm_Input_Load(object sender, EventArgs e)
        {
            this.AllCombShowInit();
            //base.Size = base.Parent.ClientSize;
            string queryStr = "";
            queryStr = "select * from T_TempNP";
            this.ds = this.MyDB.Run_SqlText(queryStr);
            if ((this.ds != null) && (this.ds.Tables[0].Rows.Count > 0))
            {
                DataSet set = this.MyDB.Run_SqlText("select * from t_plan where planid='" + this.ds.Tables[0].Rows[0]["PlanID"].ToString() + "'");
                if (set == null)
                {
                    this.MyDB.Run_SqlText("delete from T_TempNP");
                    this.CountPlanID();
                    this.TextPlanID.Text = "P" + DateTime.Now.ToString("yyyyMMdd") + this.G_PlanID.ToString("D3");
                    return;
                }
                if (set.Tables[0].Rows.Count == 0)
                {
                    this.MyDB.Run_SqlText("delete from T_TempNP");
                    this.CountPlanID();
                    this.TextPlanID.Text = "P" + DateTime.Now.ToString("yyyyMMdd") + this.G_PlanID.ToString("D3");
                    return;
                }
                this.TextPlanID.Text = set.Tables[0].Rows[0]["PlanID"].ToString();
                int index = this.TextPlanID.Text.IndexOf("-");
                this.G_PlanID = int.Parse(this.TextPlanID.Text.Substring(9, 3));
                if (this.TextPlanID.Text.Length > 14)
                {
                    this.G_SubPlan = int.Parse(this.TextPlanID.Text.Substring(index + 1, 2));
                    this.checkIsLargPlan.Checked = true;
                }
                else
                {
                    this.checkIsLargPlan.Checked = false;
                }
                this.CombPlanDepart.SelectedValue = set.Tables[0].Rows[0]["PlanDepart"].ToString();
                this.DatePlanTime.Value = Convert.ToDateTime(set.Tables[0].Rows[0]["PlanTime"].ToString());
                this.DateDeadLine.Value = Convert.ToDateTime(set.Tables[0].Rows[0]["DeadLine"].ToString());
                this.rTextAddition.Text = set.Tables[0].Rows[0]["Remark"].ToString();
                this.TextTotalCount.Text = set.Tables[0].Rows[0]["TotalCount"].ToString();
                for (int i = 0; i < this.ds.Tables[0].Rows.Count; i++)
                {
                    DataRow row = this.InputDS.Tables[0].Rows.Add(new object[0]);
                    row["XH"] = i + 1;
                    row["NPType"] = this.G_htNPType[this.ds.Tables[0].Rows[i]["NPType"]].ToString();
                    row["NPNum"] = this.ds.Tables[0].Rows[i]["NPNo"];
                    row["NPTypeID"] = this.ds.Tables[0].Rows[i]["NPType"];
                    row["FrontPiece"] = this.ds.Tables[0].Rows[i]["FrontPiece"];
                    row["BackPiece"] = this.ds.Tables[0].Rows[i]["BackPiece"];
                    row["Mail"] = this.ds.Tables[0].Rows[i]["Mail"];
                    row["PlanID"] = this.TextPlanID.Text;
                    row["PlanDepart"] = this.CombPlanDepart.Text;
                    row["PlanTime"] = this.DatePlanTime.Value;
                    row["DeadLine"] = this.DateDeadLine.Value;
                }
                this.G_NPCount = this.ds.Tables[0].Rows.Count;
            }
            else
            {
                this.MyDB.Run_SqlText("delete from T_TempNP");
                this.CountPlanID();
                this.TextPlanID.Text = "P" + DateTime.Now.ToString("yyyyMMdd") + this.G_PlanID.ToString("D3");
            }
            this.G_IsCheck = false;
            this.G_GridNPInfoLastIndex = 1;
        }

        private void GridNPInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void GridNPInfo_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.G_IsCheck)
                {
                    if (((this.G_GridNPInfoLastIndex < this.GridNPInfo.RowCount) && (this.GridNPInfo.Rows[this.G_GridNPInfoLastIndex].Cells[2].Value.ToString() != this.TextShowNPNum.Text)) && ((this.TextShowNPNum.Text.Length > 2) && (MessageBox.Show(this, "是否车牌号" + this.GridNPInfo.Rows[this.GridNPInfo.CurrentCell.RowIndex].Cells[2].Value.ToString() + "改为" + this.TextShowNPNum.Text, "车牌编辑提示", MessageBoxButtons.YesNo) == DialogResult.Yes)))
                    {
                        if (this.TextShowNPNum.Text.Length != 7)
                        {
                            MessageBox.Show(this, "车牌位数不对！请重新编辑！", "车牌编辑提示", MessageBoxButtons.OK);
                            this.GridNPInfo.Rows[this.G_GridNPInfoLastIndex].Cells[0].Selected = true;
                            return;
                        }
                        this.InputDS.Tables[0].Rows[this.G_GridNPInfoLastIndex]["NPNum"] = this.TextShowNPNum.Text;
                        this.GridNPInfo.Refresh();
                    }
                    this.TextShowNPNum.Text = this.GridNPInfo.Rows[this.GridNPInfo.CurrentCell.RowIndex].Cells[2].Value.ToString();
                    this.G_GridNPInfoLastIndex = e.RowIndex;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, exception.TargetSite.ToString(), exception.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void GridNPInfo_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (MessageBox.Show(this, "是否要请空车牌数据？", "计划单录入", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                this.InputDS.Tables[0].Rows.Clear();
                this.G_NPCount = 0;
            }
        }

        private void GridNPInfo_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            int count = this.InputDS.Tables[0].Rows.Count;
            this.G_NPCount = 0;
            for (int i = 0; i < this.InputDS.Tables[0].Rows.Count; i++)
            {
                this.G_NPCount++;
                this.InputDS.Tables[0].Rows[i]["XH"] = this.G_NPCount;
            }
        }

        private void InitializeComponent()
        {
            this.tabShow = new TabPage();
            this.PalNpNum = new Panel();
            this.label7 = new Label();
            this.checkBoxMail = new CheckBox();
            this.textNPTemplate = new TextBox();
            this.TextShowNPNum = new TextBox();
            this.tabNPInfo = new TabPage();
            this.palNPAllInfo = new Panel();
            this.btnUpdatePlanID = new Button();
            this.btnCancle = new Button();
            this.btnIgnore = new Button();
            this.GridSameNPInfo = new DataGridView();
            this.palLargInput = new Panel();
            this.groupBox3 = new GroupBox();
            this.groupChar = new GroupBox();
            this.btnChar33 = new Button();
            this.btnChar32 = new Button();
            this.btnChar31 = new Button();
            this.btnChar30 = new Button();
            this.btnChar29 = new Button();
            this.btnChar28 = new Button();
            this.btnChar27 = new Button();
            this.btnChar26 = new Button();
            this.btnChar25 = new Button();
            this.btnChar24 = new Button();
            this.btnChar23 = new Button();
            this.textNpWord = new TextBox();
            this.btnChar22 = new Button();
            this.btnChar21 = new Button();
            this.btnChar20 = new Button();
            this.btnChar19 = new Button();
            this.btnChar18 = new Button();
            this.btnChar17 = new Button();
            this.btnChar16 = new Button();
            this.btnChar15 = new Button();
            this.btnChar14 = new Button();
            this.btnChar13 = new Button();
            this.btnChar11 = new Button();
            this.btnChar12 = new Button();
            this.btnChar10 = new Button();
            this.btnChar09 = new Button();
            this.btnChar08 = new Button();
            this.btnChar07 = new Button();
            this.btnChar06 = new Button();
            this.btnChar05 = new Button();
            this.btnChar04 = new Button();
            this.btnChar03 = new Button();
            this.btnChar02 = new Button();
            this.btnChar01 = new Button();
            this.label22 = new Label();
            this.textPCNPNum = new TextBox();
            this.btnNumP2 = new Button();
            this.btnNumP7 = new Button();
            this.btnNumP1 = new Button();
            this.btnNumP3 = new Button();
            this.btnNumP4 = new Button();
            this.btnNumP5 = new Button();
            this.btnNumP6 = new Button();
            this.btnFW = new Button();
            this.label15 = new Label();
            this.textFWBegin = new TextBox();
            this.textFWEnd = new TextBox();
            this.label14 = new Label();
            this.textFWDefine = new TextBox();
            this.label16 = new Label();
            this.btnLargInputCacel = new Button();
            this.btnLargInputOK = new Button();
            this.tabLargInput = new TabPage();
            this.BtnSaveNP = new Button();
            this.CombNPType = new ComboBox();
            this.Label5 = new Label();
            this.tabControl1 = new TabControl();
            this.Label4 = new Label();
            this.btn_CheckMode = new Button();
            this.BtnLargInput = new Button();
            this.splitContainer1 = new SplitContainer();
            this.groupBox5 = new GroupBox();
            this.labLargPlanTotal = new Label();
            this.btnLargPlanOK = new Button();
            this.label9 = new Label();
            this.label8 = new Label();
            this.textLargPlanCount = new TextBox();
            this.combLargPlanNPType = new ComboBox();
            this.btnLargPlanExit = new Button();
            this.GroupBox2 = new GroupBox();
            this.checkIsLargPlan = new CheckBox();
            this.btnImportFromHtm = new Button();
            this.label21 = new Label();
            this.rTextAddition = new RichTextBox();
            this.Label13 = new Label();
            this.CombPlanType = new ComboBox();
            this.TextTotalCount = new TextBox();
            this.Label12 = new Label();
            this.BtnCreatePlan = new Button();
            this.Label3 = new Label();
            this.DateDeadLine = new DateTimePicker();
            this.TextPlanID = new TextBox();
            this.Label6 = new Label();
            this.Label2 = new Label();
            this.DatePlanTime = new DateTimePicker();
            this.Label1 = new Label();
            this.CombPlanDepart = new ComboBox();
            this.GroupBox1 = new GroupBox();
            this.btnSaveNPTemp = new Button();
            this.checkBackPiece = new CheckBox();
            this.checkFontPiece = new CheckBox();
            this.btnInputMode = new Button();
            this.groupBox4 = new GroupBox();
            this.GridNPInfo = new DataGridView();
            this.tabShow.SuspendLayout();
            this.PalNpNum.SuspendLayout();
            this.tabNPInfo.SuspendLayout();
            this.palNPAllInfo.SuspendLayout();
            ((ISupportInitialize) this.GridSameNPInfo).BeginInit();
            this.palLargInput.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupChar.SuspendLayout();
            this.tabLargInput.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((ISupportInitialize) this.GridNPInfo).BeginInit();
            base.SuspendLayout();
            this.tabShow.Controls.Add(this.PalNpNum);
            this.tabShow.Location = new Point(4, 0x15);
            this.tabShow.Name = "tabShow";
            this.tabShow.Padding = new Padding(3);
            this.tabShow.Size = new Size(0x306, 0xd8);
            this.tabShow.TabIndex = 0;
            this.tabShow.Text = "单体输入";
            this.tabShow.UseVisualStyleBackColor = true;
            this.PalNpNum.Controls.Add(this.label7);
            this.PalNpNum.Controls.Add(this.checkBoxMail);
            this.PalNpNum.Controls.Add(this.textNPTemplate);
            this.PalNpNum.Controls.Add(this.TextShowNPNum);
            this.PalNpNum.Dock = DockStyle.Fill;
            this.PalNpNum.Location = new Point(3, 3);
            this.PalNpNum.Name = "PalNpNum";
            this.PalNpNum.Size = new Size(0x300, 210);
            this.PalNpNum.TabIndex = 0x24;
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x4e, 0x1a);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x35, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "输入模板";
            this.checkBoxMail.AutoSize = true;
            this.checkBoxMail.Location = new Point(0x18b, 0x19);
            this.checkBoxMail.Name = "checkBoxMail";
            this.checkBoxMail.Size = new Size(0x30, 0x10);
            this.checkBoxMail.TabIndex = 0x1f;
            this.checkBoxMail.Text = "邮寄";
            this.checkBoxMail.UseVisualStyleBackColor = true;
            this.textNPTemplate.Font = new Font("宋体", 12f);
            this.textNPTemplate.Location = new Point(0x89, 0x13);
            this.textNPTemplate.Name = "textNPTemplate";
            this.textNPTemplate.Size = new Size(0xd0, 0x1a);
            this.textNPTemplate.TabIndex = 1;
            this.textNPTemplate.KeyUp += new KeyEventHandler(this.textNPTemplate_KeyUp);
            this.textNPTemplate.TextChanged += new EventHandler(this.textNPTemplate_TextChanged);
            this.TextShowNPNum.BorderStyle = BorderStyle.FixedSingle;
            this.TextShowNPNum.Font = new Font("Arial Black", 72f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.TextShowNPNum.Location = new Point(80, 0x33);
            this.TextShowNPNum.Name = "TextShowNPNum";
            this.TextShowNPNum.Size = new Size(0x24c, 0x8f);
            this.TextShowNPNum.TabIndex = 0;
            this.TextShowNPNum.Text = "鲁B";
            this.TextShowNPNum.KeyUp += new KeyEventHandler(this.TextNPNum_KeyUp);
            this.TextShowNPNum.TextChanged += new EventHandler(this.TextNPNum_TextChanged);
            this.tabNPInfo.Controls.Add(this.palNPAllInfo);
            this.tabNPInfo.Location = new Point(4, 0x15);
            this.tabNPInfo.Name = "tabNPInfo";
            this.tabNPInfo.Padding = new Padding(3);
            this.tabNPInfo.Size = new Size(0x306, 0xd8);
            this.tabNPInfo.TabIndex = 1;
            this.tabNPInfo.Text = "提示信息";
            this.tabNPInfo.UseVisualStyleBackColor = true;
            this.palNPAllInfo.Controls.Add(this.btnUpdatePlanID);
            this.palNPAllInfo.Controls.Add(this.btnCancle);
            this.palNPAllInfo.Controls.Add(this.btnIgnore);
            this.palNPAllInfo.Controls.Add(this.GridSameNPInfo);
            this.palNPAllInfo.Dock = DockStyle.Fill;
            this.palNPAllInfo.Location = new Point(3, 3);
            this.palNPAllInfo.Name = "palNPAllInfo";
            this.palNPAllInfo.Size = new Size(0x300, 210);
            this.palNPAllInfo.TabIndex = 0x25;
            this.btnUpdatePlanID.Location = new Point(670, 0x36);
            this.btnUpdatePlanID.Name = "btnUpdatePlanID";
            this.btnUpdatePlanID.Size = new Size(0x4b, 0x17);
            this.btnUpdatePlanID.TabIndex = 30;
            this.btnUpdatePlanID.Text = "转单";
            this.btnUpdatePlanID.UseVisualStyleBackColor = true;
            this.btnUpdatePlanID.Click += new EventHandler(this.btnUpdatePlanID_Click);
            this.btnCancle.Location = new Point(670, 140);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new Size(0x4b, 0x17);
            this.btnCancle.TabIndex = 0x1d;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new EventHandler(this.btnCancle_Click);
            this.btnIgnore.Location = new Point(670, 0x61);
            this.btnIgnore.Name = "btnIgnore";
            this.btnIgnore.Size = new Size(0x4b, 0x17);
            this.btnIgnore.TabIndex = 0x1c;
            this.btnIgnore.Text = "忽略";
            this.btnIgnore.UseVisualStyleBackColor = true;
            this.btnIgnore.Click += new EventHandler(this.btnIgnore_Click);
            this.GridSameNPInfo.AllowUserToAddRows = false;
            this.GridSameNPInfo.AllowUserToDeleteRows = false;
            this.GridSameNPInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridSameNPInfo.Location = new Point(3, 6);
            this.GridSameNPInfo.Name = "GridSameNPInfo";
            this.GridSameNPInfo.ReadOnly = true;
            this.GridSameNPInfo.RowTemplate.Height = 0x17;
            this.GridSameNPInfo.Size = new Size(640, 0xc9);
            this.GridSameNPInfo.TabIndex = 0x1b;
            this.palLargInput.Controls.Add(this.groupBox3);
            this.palLargInput.Controls.Add(this.btnLargInputCacel);
            this.palLargInput.Controls.Add(this.btnLargInputOK);
            this.palLargInput.Dock = DockStyle.Fill;
            this.palLargInput.Location = new Point(3, 3);
            this.palLargInput.Name = "palLargInput";
            this.palLargInput.Size = new Size(0x300, 210);
            this.palLargInput.TabIndex = 0x25;
            this.groupBox3.Controls.Add(this.groupChar);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.textPCNPNum);
            this.groupBox3.Controls.Add(this.btnNumP2);
            this.groupBox3.Controls.Add(this.btnNumP7);
            this.groupBox3.Controls.Add(this.btnNumP1);
            this.groupBox3.Controls.Add(this.btnNumP3);
            this.groupBox3.Controls.Add(this.btnNumP4);
            this.groupBox3.Controls.Add(this.btnNumP5);
            this.groupBox3.Controls.Add(this.btnNumP6);
            this.groupBox3.Controls.Add(this.btnFW);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.textFWBegin);
            this.groupBox3.Controls.Add(this.textFWEnd);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.textFWDefine);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Location = new Point(0x47, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(0x1e9, 0xcb);
            this.groupBox3.TabIndex = 0x25;
            this.groupBox3.TabStop = false;
            this.groupChar.Controls.Add(this.btnChar33);
            this.groupChar.Controls.Add(this.btnChar32);
            this.groupChar.Controls.Add(this.btnChar31);
            this.groupChar.Controls.Add(this.btnChar30);
            this.groupChar.Controls.Add(this.btnChar29);
            this.groupChar.Controls.Add(this.btnChar28);
            this.groupChar.Controls.Add(this.btnChar27);
            this.groupChar.Controls.Add(this.btnChar26);
            this.groupChar.Controls.Add(this.btnChar25);
            this.groupChar.Controls.Add(this.btnChar24);
            this.groupChar.Controls.Add(this.btnChar23);
            this.groupChar.Controls.Add(this.textNpWord);
            this.groupChar.Controls.Add(this.btnChar22);
            this.groupChar.Controls.Add(this.btnChar21);
            this.groupChar.Controls.Add(this.btnChar20);
            this.groupChar.Controls.Add(this.btnChar19);
            this.groupChar.Controls.Add(this.btnChar18);
            this.groupChar.Controls.Add(this.btnChar17);
            this.groupChar.Controls.Add(this.btnChar16);
            this.groupChar.Controls.Add(this.btnChar15);
            this.groupChar.Controls.Add(this.btnChar14);
            this.groupChar.Controls.Add(this.btnChar13);
            this.groupChar.Controls.Add(this.btnChar11);
            this.groupChar.Controls.Add(this.btnChar12);
            this.groupChar.Controls.Add(this.btnChar10);
            this.groupChar.Controls.Add(this.btnChar09);
            this.groupChar.Controls.Add(this.btnChar08);
            this.groupChar.Controls.Add(this.btnChar07);
            this.groupChar.Controls.Add(this.btnChar06);
            this.groupChar.Controls.Add(this.btnChar05);
            this.groupChar.Controls.Add(this.btnChar04);
            this.groupChar.Controls.Add(this.btnChar03);
            this.groupChar.Controls.Add(this.btnChar02);
            this.groupChar.Controls.Add(this.btnChar01);
            this.groupChar.Location = new Point(0x51, 0x4a);
            this.groupChar.Name = "groupChar";
            this.groupChar.Size = new Size(0x141, 0x68);
            this.groupChar.TabIndex = 0x43;
            this.groupChar.TabStop = false;
            this.btnChar33.Location = new Point(0x4c, 0x4e);
            this.btnChar33.Name = "btnChar33";
            this.btnChar33.Size = new Size(0x1c, 0x16);
            this.btnChar33.TabIndex = 0x48;
            this.btnChar33.Text = "2";
            this.btnChar33.UseVisualStyleBackColor = true;
            this.btnChar33.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar32.Location = new Point(0x2f, 0x4e);
            this.btnChar32.Name = "btnChar32";
            this.btnChar32.Size = new Size(0x1c, 0x16);
            this.btnChar32.TabIndex = 0x47;
            this.btnChar32.Text = "1";
            this.btnChar32.UseVisualStyleBackColor = true;
            this.btnChar32.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar31.Location = new Point(0x12, 0x4e);
            this.btnChar31.Name = "btnChar31";
            this.btnChar31.Size = new Size(0x1c, 0x16);
            this.btnChar31.TabIndex = 70;
            this.btnChar31.Text = "0";
            this.btnChar31.UseVisualStyleBackColor = true;
            this.btnChar31.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar30.Location = new Point(0x117, 0x37);
            this.btnChar30.Name = "btnChar30";
            this.btnChar30.Size = new Size(0x1c, 0x16);
            this.btnChar30.TabIndex = 0x45;
            this.btnChar30.Text = "9";
            this.btnChar30.UseVisualStyleBackColor = true;
            this.btnChar30.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar29.Location = new Point(250, 0x37);
            this.btnChar29.Name = "btnChar29";
            this.btnChar29.Size = new Size(0x1c, 0x16);
            this.btnChar29.TabIndex = 0x44;
            this.btnChar29.Text = "8";
            this.btnChar29.UseVisualStyleBackColor = true;
            this.btnChar29.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar28.Location = new Point(0xdd, 0x37);
            this.btnChar28.Name = "btnChar28";
            this.btnChar28.Size = new Size(0x1c, 0x16);
            this.btnChar28.TabIndex = 0x43;
            this.btnChar28.Text = "7";
            this.btnChar28.UseVisualStyleBackColor = true;
            this.btnChar28.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar27.Location = new Point(0xc0, 0x37);
            this.btnChar27.Name = "btnChar27";
            this.btnChar27.Size = new Size(0x1c, 0x16);
            this.btnChar27.TabIndex = 0x42;
            this.btnChar27.Text = "6";
            this.btnChar27.UseVisualStyleBackColor = true;
            this.btnChar27.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar26.Location = new Point(0xa3, 0x37);
            this.btnChar26.Name = "btnChar26";
            this.btnChar26.Size = new Size(0x1c, 0x16);
            this.btnChar26.TabIndex = 0x41;
            this.btnChar26.Text = "5";
            this.btnChar26.UseVisualStyleBackColor = true;
            this.btnChar26.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar25.Location = new Point(0x86, 0x37);
            this.btnChar25.Name = "btnChar25";
            this.btnChar25.Size = new Size(0x1c, 0x16);
            this.btnChar25.TabIndex = 0x40;
            this.btnChar25.Text = "4";
            this.btnChar25.UseVisualStyleBackColor = true;
            this.btnChar25.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar24.Location = new Point(0x69, 0x37);
            this.btnChar24.Name = "btnChar24";
            this.btnChar24.Size = new Size(0x1c, 0x16);
            this.btnChar24.TabIndex = 0x3f;
            this.btnChar24.Text = "3";
            this.btnChar24.UseVisualStyleBackColor = true;
            this.btnChar24.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar23.Location = new Point(0x4c, 0x37);
            this.btnChar23.Name = "btnChar23";
            this.btnChar23.Size = new Size(0x1c, 0x16);
            this.btnChar23.TabIndex = 0x3e;
            this.btnChar23.Text = "2";
            this.btnChar23.UseVisualStyleBackColor = true;
            this.btnChar23.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.textNpWord.Location = new Point(0x116, 80);
            this.textNpWord.Name = "textNpWord";
            this.textNpWord.Size = new Size(30, 0x15);
            this.textNpWord.TabIndex = 0x38;
            this.btnChar22.Location = new Point(0x2f, 0x37);
            this.btnChar22.Name = "btnChar22";
            this.btnChar22.Size = new Size(0x1c, 0x16);
            this.btnChar22.TabIndex = 0x3d;
            this.btnChar22.Text = "1";
            this.btnChar22.UseVisualStyleBackColor = true;
            this.btnChar22.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar21.Location = new Point(0x12, 0x37);
            this.btnChar21.Name = "btnChar21";
            this.btnChar21.Size = new Size(0x1c, 0x16);
            this.btnChar21.TabIndex = 60;
            this.btnChar21.Text = "0";
            this.btnChar21.UseVisualStyleBackColor = true;
            this.btnChar21.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar20.Location = new Point(0x117, 0x21);
            this.btnChar20.Name = "btnChar20";
            this.btnChar20.Size = new Size(0x1c, 0x16);
            this.btnChar20.TabIndex = 0x3b;
            this.btnChar20.Text = "9";
            this.btnChar20.UseVisualStyleBackColor = true;
            this.btnChar20.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar19.Location = new Point(250, 0x21);
            this.btnChar19.Name = "btnChar19";
            this.btnChar19.Size = new Size(0x1c, 0x16);
            this.btnChar19.TabIndex = 0x3a;
            this.btnChar19.Text = "8";
            this.btnChar19.UseVisualStyleBackColor = true;
            this.btnChar19.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar18.Location = new Point(0xdd, 0x21);
            this.btnChar18.Name = "btnChar18";
            this.btnChar18.Size = new Size(0x1c, 0x16);
            this.btnChar18.TabIndex = 0x39;
            this.btnChar18.Text = "7";
            this.btnChar18.UseVisualStyleBackColor = true;
            this.btnChar18.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar17.Location = new Point(0xc0, 0x21);
            this.btnChar17.Name = "btnChar17";
            this.btnChar17.Size = new Size(0x1c, 0x16);
            this.btnChar17.TabIndex = 0x38;
            this.btnChar17.Text = "6";
            this.btnChar17.UseVisualStyleBackColor = true;
            this.btnChar17.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar16.Location = new Point(0xa3, 0x21);
            this.btnChar16.Name = "btnChar16";
            this.btnChar16.Size = new Size(0x1c, 0x16);
            this.btnChar16.TabIndex = 0x37;
            this.btnChar16.Text = "5";
            this.btnChar16.UseVisualStyleBackColor = true;
            this.btnChar16.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar15.Location = new Point(0x86, 0x21);
            this.btnChar15.Name = "btnChar15";
            this.btnChar15.Size = new Size(0x1c, 0x16);
            this.btnChar15.TabIndex = 0x36;
            this.btnChar15.Text = "4";
            this.btnChar15.UseVisualStyleBackColor = true;
            this.btnChar15.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar14.Location = new Point(0x69, 0x21);
            this.btnChar14.Name = "btnChar14";
            this.btnChar14.Size = new Size(0x1c, 0x16);
            this.btnChar14.TabIndex = 0x35;
            this.btnChar14.Text = "3";
            this.btnChar14.UseVisualStyleBackColor = true;
            this.btnChar14.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar13.Location = new Point(0x4c, 0x21);
            this.btnChar13.Name = "btnChar13";
            this.btnChar13.Size = new Size(0x1c, 0x16);
            this.btnChar13.TabIndex = 0x34;
            this.btnChar13.Text = "2";
            this.btnChar13.UseVisualStyleBackColor = true;
            this.btnChar13.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar11.Location = new Point(0x12, 0x21);
            this.btnChar11.Name = "btnChar11";
            this.btnChar11.Size = new Size(0x1c, 0x16);
            this.btnChar11.TabIndex = 0x33;
            this.btnChar11.Text = "1";
            this.btnChar11.UseVisualStyleBackColor = true;
            this.btnChar11.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar12.Location = new Point(0x2f, 0x21);
            this.btnChar12.Name = "btnChar12";
            this.btnChar12.Size = new Size(0x1c, 0x16);
            this.btnChar12.TabIndex = 0x33;
            this.btnChar12.Text = "1";
            this.btnChar12.UseVisualStyleBackColor = true;
            this.btnChar12.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar10.Location = new Point(0x117, 11);
            this.btnChar10.Name = "btnChar10";
            this.btnChar10.Size = new Size(0x1c, 0x16);
            this.btnChar10.TabIndex = 0x31;
            this.btnChar10.Text = "9";
            this.btnChar10.UseVisualStyleBackColor = true;
            this.btnChar10.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar09.Location = new Point(250, 11);
            this.btnChar09.Name = "btnChar09";
            this.btnChar09.Size = new Size(0x1c, 0x16);
            this.btnChar09.TabIndex = 0x30;
            this.btnChar09.Text = "8";
            this.btnChar09.UseVisualStyleBackColor = true;
            this.btnChar09.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar08.Location = new Point(0xdd, 11);
            this.btnChar08.Name = "btnChar08";
            this.btnChar08.Size = new Size(0x1c, 0x16);
            this.btnChar08.TabIndex = 0x2f;
            this.btnChar08.Text = "7";
            this.btnChar08.UseVisualStyleBackColor = true;
            this.btnChar08.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar07.Location = new Point(0xc0, 11);
            this.btnChar07.Name = "btnChar07";
            this.btnChar07.Size = new Size(0x1c, 0x16);
            this.btnChar07.TabIndex = 0x2e;
            this.btnChar07.Text = "6";
            this.btnChar07.UseVisualStyleBackColor = true;
            this.btnChar07.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar06.Location = new Point(0xa3, 11);
            this.btnChar06.Name = "btnChar06";
            this.btnChar06.Size = new Size(0x1c, 0x16);
            this.btnChar06.TabIndex = 0x2d;
            this.btnChar06.Text = "5";
            this.btnChar06.UseVisualStyleBackColor = true;
            this.btnChar06.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar05.Location = new Point(0x86, 11);
            this.btnChar05.Name = "btnChar05";
            this.btnChar05.Size = new Size(0x1c, 0x16);
            this.btnChar05.TabIndex = 0x2c;
            this.btnChar05.Text = "4";
            this.btnChar05.UseVisualStyleBackColor = true;
            this.btnChar05.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar04.Location = new Point(0x69, 11);
            this.btnChar04.Name = "btnChar04";
            this.btnChar04.Size = new Size(0x1c, 0x16);
            this.btnChar04.TabIndex = 0x2b;
            this.btnChar04.Text = "3";
            this.btnChar04.UseVisualStyleBackColor = true;
            this.btnChar04.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar03.Location = new Point(0x4c, 11);
            this.btnChar03.Name = "btnChar03";
            this.btnChar03.Size = new Size(0x1c, 0x16);
            this.btnChar03.TabIndex = 0x2a;
            this.btnChar03.Text = "2";
            this.btnChar03.UseVisualStyleBackColor = true;
            this.btnChar03.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar02.Location = new Point(0x2f, 11);
            this.btnChar02.Name = "btnChar02";
            this.btnChar02.Size = new Size(0x1c, 0x16);
            this.btnChar02.TabIndex = 0x29;
            this.btnChar02.Text = "1";
            this.btnChar02.UseVisualStyleBackColor = true;
            this.btnChar02.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.btnChar01.Location = new Point(0x12, 11);
            this.btnChar01.Name = "btnChar01";
            this.btnChar01.Size = new Size(0x1c, 0x16);
            this.btnChar01.TabIndex = 40;
            this.btnChar01.Text = "0";
            this.btnChar01.UseVisualStyleBackColor = true;
            this.btnChar01.MouseUp += new MouseEventHandler(this.btnChar_Click);
            this.label22.AutoSize = true;
            this.label22.Location = new Point(0x134, 0xbb);
            this.label22.Name = "label22";
            this.label22.Size = new Size(0x1d, 12);
            this.label22.TabIndex = 0x42;
            this.label22.Text = "排除";
            this.textPCNPNum.Location = new Point(0x151, 0xb3);
            this.textPCNPNum.Name = "textPCNPNum";
            this.textPCNPNum.Size = new Size(0x52, 0x15);
            this.textPCNPNum.TabIndex = 0x41;
            this.btnNumP2.Font = new Font("Arial Black", 36f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnNumP2.Location = new Point(0x13c, 11);
            this.btnNumP2.Name = "btnNumP2";
            this.btnNumP2.Size = new Size(0x2c, 0x40);
            this.btnNumP2.TabIndex = 0x40;
            this.btnNumP2.UseVisualStyleBackColor = true;
            this.btnNumP2.MouseUp += new MouseEventHandler(this.btnNumP_Click);
            this.btnNumP7.BackColor = Color.Red;
            this.btnNumP7.Font = new Font("Arial Black", 36f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnNumP7.ForeColor = Color.Black;
            this.btnNumP7.Location = new Point(0x53, 11);
            this.btnNumP7.Name = "btnNumP7";
            this.btnNumP7.Size = new Size(0x2c, 0x40);
            this.btnNumP7.TabIndex = 0x3f;
            this.btnNumP7.Text = "鲁";
            this.btnNumP7.UseVisualStyleBackColor = false;
            this.btnNumP1.Font = new Font("Arial Black", 36f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnNumP1.Location = new Point(360, 11);
            this.btnNumP1.Name = "btnNumP1";
            this.btnNumP1.Size = new Size(0x2c, 0x40);
            this.btnNumP1.TabIndex = 0x3e;
            this.btnNumP1.UseVisualStyleBackColor = true;
            this.btnNumP1.MouseUp += new MouseEventHandler(this.btnNumP_Click);
            this.btnNumP3.Font = new Font("Arial Black", 36f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnNumP3.Location = new Point(0x110, 11);
            this.btnNumP3.Name = "btnNumP3";
            this.btnNumP3.Size = new Size(0x2c, 0x40);
            this.btnNumP3.TabIndex = 0x3d;
            this.btnNumP3.UseVisualStyleBackColor = true;
            this.btnNumP3.MouseUp += new MouseEventHandler(this.btnNumP_Click);
            this.btnNumP4.Font = new Font("Arial Black", 36f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnNumP4.Location = new Point(0xe4, 11);
            this.btnNumP4.Name = "btnNumP4";
            this.btnNumP4.Size = new Size(0x2c, 0x40);
            this.btnNumP4.TabIndex = 60;
            this.btnNumP4.UseVisualStyleBackColor = true;
            this.btnNumP4.MouseUp += new MouseEventHandler(this.btnNumP_Click);
            this.btnNumP5.Font = new Font("Arial Black", 36f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnNumP5.Location = new Point(0xb8, 11);
            this.btnNumP5.Name = "btnNumP5";
            this.btnNumP5.Size = new Size(0x2c, 0x40);
            this.btnNumP5.TabIndex = 0x3b;
            this.btnNumP5.UseVisualStyleBackColor = true;
            this.btnNumP5.MouseUp += new MouseEventHandler(this.btnNumP_Click);
            this.btnNumP6.Font = new Font("Arial Black", 36f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.btnNumP6.Location = new Point(0x7f, 11);
            this.btnNumP6.Name = "btnNumP6";
            this.btnNumP6.Size = new Size(0x2c, 0x40);
            this.btnNumP6.TabIndex = 0x3a;
            this.btnNumP6.Text = "B";
            this.btnNumP6.UseVisualStyleBackColor = true;
            this.btnNumP6.MouseUp += new MouseEventHandler(this.btnNumP6_MouseUp);
            this.btnFW.Location = new Point(0x1a6, 0xb2);
            this.btnFW.Name = "btnFW";
            this.btnFW.Size = new Size(0x3d, 0x18);
            this.btnFW.TabIndex = 0x1a;
            this.btnFW.Text = "确定";
            this.btnFW.UseVisualStyleBackColor = true;
            this.btnFW.Click += new EventHandler(this.btnFW_Click);
            this.label15.AutoSize = true;
            this.label15.Location = new Point(0xa3, 0xbb);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x11, 12);
            this.label15.TabIndex = 0x16;
            this.label15.Text = "从";
            this.textFWBegin.Location = new Point(180, 0xb3);
            this.textFWBegin.Name = "textFWBegin";
            this.textFWBegin.Size = new Size(0x33, 0x15);
            this.textFWBegin.TabIndex = 0x17;
            this.textFWBegin.TextChanged += new EventHandler(this.textFWBegin_TextChanged);
            this.textFWEnd.Location = new Point(0x101, 0xb3);
            this.textFWEnd.Name = "textFWEnd";
            this.textFWEnd.Size = new Size(0x33, 0x15);
            this.textFWEnd.TabIndex = 0x18;
            this.textFWEnd.TextChanged += new EventHandler(this.textFWEnd_TextChanged);
            this.label14.AutoSize = true;
            this.label14.Location = new Point(0xee, 0xbb);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x11, 12);
            this.label14.TabIndex = 0x19;
            this.label14.Text = "到";
            this.textFWDefine.Enabled = false;
            this.textFWDefine.Location = new Point(0x2c, 0xb3);
            this.textFWDefine.Name = "textFWDefine";
            this.textFWDefine.Size = new Size(0x6c, 0x15);
            this.textFWDefine.TabIndex = 0x23;
            this.label16.AutoSize = true;
            this.label16.Location = new Point(14, 0xb8);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x1d, 12);
            this.label16.TabIndex = 0x30;
            this.label16.Text = "范围";
            this.btnLargInputCacel.Location = new Point(0x279, 0x7e);
            this.btnLargInputCacel.Name = "btnLargInputCacel";
            this.btnLargInputCacel.Size = new Size(0x4b, 0x17);
            this.btnLargInputCacel.TabIndex = 0x33;
            this.btnLargInputCacel.Text = "取消";
            this.btnLargInputCacel.UseVisualStyleBackColor = true;
            this.btnLargInputCacel.Click += new EventHandler(this.btnLargInputCacel_Click);
            this.btnLargInputOK.Location = new Point(0x279, 0x44);
            this.btnLargInputOK.Name = "btnLargInputOK";
            this.btnLargInputOK.Size = new Size(0x4b, 0x17);
            this.btnLargInputOK.TabIndex = 0x1b;
            this.btnLargInputOK.Text = "确定";
            this.btnLargInputOK.UseVisualStyleBackColor = true;
            this.btnLargInputOK.Click += new EventHandler(this.btnLargInputOK_Click);
            this.tabLargInput.Controls.Add(this.palLargInput);
            this.tabLargInput.Location = new Point(4, 0x15);
            this.tabLargInput.Name = "tabLargInput";
            this.tabLargInput.Padding = new Padding(3);
            this.tabLargInput.Size = new Size(0x306, 0xd8);
            this.tabLargInput.TabIndex = 2;
            this.tabLargInput.Text = "号段输入";
            this.tabLargInput.UseVisualStyleBackColor = true;
            this.BtnSaveNP.Location = new Point(0x72, 0x6f);
            this.BtnSaveNP.Name = "BtnSaveNP";
            this.BtnSaveNP.Size = new Size(0x4b, 0x17);
            this.BtnSaveNP.TabIndex = 0x19;
            this.BtnSaveNP.Text = "保存数据";
            this.BtnSaveNP.UseVisualStyleBackColor = true;
            this.BtnSaveNP.Click += new EventHandler(this.BtnSaveNP_Click);
            this.CombNPType.FormattingEnabled = true;
            this.CombNPType.Location = new Point(0x51, 15);
            this.CombNPType.Name = "CombNPType";
            this.CombNPType.Size = new Size(0x70, 20);
            this.CombNPType.TabIndex = 15;
            this.CombNPType.SelectedIndexChanged += new EventHandler(this.CombNPType_SelectedIndexChanged);
            this.Label5.AutoSize = true;
            this.Label5.Location = new Point(0x16, 0x12);
            this.Label5.Name = "Label5";
            this.Label5.Size = new Size(0x35, 12);
            this.Label5.TabIndex = 14;
            this.Label5.Text = "车牌类型";
            this.tabControl1.Controls.Add(this.tabShow);
            this.tabControl1.Controls.Add(this.tabLargInput);
            this.tabControl1.Controls.Add(this.tabNPInfo);
            this.tabControl1.Dock = DockStyle.Bottom;
            this.tabControl1.Location = new Point(5, 0x20a);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(0x30e, 0xf1);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new EventHandler(this.tabControl1_SelectedIndexChanged);
            this.Label4.AutoSize = true;
            this.Label4.Location = new Point(0x18, 0x45);
            this.Label4.Name = "Label4";
            this.Label4.Size = new Size(0x35, 12);
            this.Label4.TabIndex = 13;
            this.Label4.Text = "车牌号码";
            this.Label4.Visible = false;
            this.btn_CheckMode.Location = new Point(0x72, 0x4d);
            this.btn_CheckMode.Name = "btn_CheckMode";
            this.btn_CheckMode.Size = new Size(0x4b, 0x17);
            this.btn_CheckMode.TabIndex = 0x1b;
            this.btn_CheckMode.Text = "校对模式";
            this.btn_CheckMode.UseVisualStyleBackColor = true;
            this.btn_CheckMode.Click += new EventHandler(this.Btn_CheckMode_Click);
            this.BtnLargInput.Location = new Point(-54, 0x2e1);
            this.BtnLargInput.Name = "BtnLargInput";
            this.BtnLargInput.Size = new Size(0x4b, 0x17);
            this.BtnLargInput.TabIndex = 0x1c;
            this.BtnLargInput.Text = "号段输入";
            this.BtnLargInput.UseVisualStyleBackColor = true;
            this.BtnLargInput.Visible = false;
            this.BtnLargInput.Click += new EventHandler(this.BtnLargInput_Click);
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.groupBox5);
            this.splitContainer1.Panel1.Controls.Add(this.btnLargPlanExit);
            this.splitContainer1.Panel1.Controls.Add(this.GroupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.GroupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.BtnLargInput);
            this.splitContainer1.Panel1.Padding = new Padding(5);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Padding = new Padding(5);
            this.splitContainer1.Size = new Size(0x400, 0x300);
            this.splitContainer1.SplitterDistance = 0xe4;
            this.splitContainer1.TabIndex = 1;
            this.groupBox5.Controls.Add(this.labLargPlanTotal);
            this.groupBox5.Controls.Add(this.btnLargPlanOK);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.textLargPlanCount);
            this.groupBox5.Controls.Add(this.combLargPlanNPType);
            this.groupBox5.Dock = DockStyle.Left;
            this.groupBox5.Location = new Point(5, 0x139);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new Size(0xda, 0x135);
            this.groupBox5.TabIndex = 0x21;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "大单信息";
            this.labLargPlanTotal.AutoSize = true;
            this.labLargPlanTotal.ForeColor = Color.Red;
            this.labLargPlanTotal.Location = new Point(7, 0x4b);
            this.labLargPlanTotal.Name = "labLargPlanTotal";
            this.labLargPlanTotal.Size = new Size(0x1d, 12);
            this.labLargPlanTotal.TabIndex = 13;
            this.labLargPlanTotal.Text = "总数";
            this.btnLargPlanOK.Enabled = false;
            this.btnLargPlanOK.Location = new Point(0x72, 0x71);
            this.btnLargPlanOK.Name = "btnLargPlanOK";
            this.btnLargPlanOK.Size = new Size(0x4b, 0x17);
            this.btnLargPlanOK.TabIndex = 12;
            this.btnLargPlanOK.Text = "确定";
            this.btnLargPlanOK.UseVisualStyleBackColor = true;
            this.btnLargPlanOK.Click += new EventHandler(this.btnLargPlanOK_Click);
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x18, 50);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x1d, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "数量";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0x16, 0x17);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x35, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "车牌类型";
            this.textLargPlanCount.Enabled = false;
            this.textLargPlanCount.Location = new Point(0x4f, 0x2f);
            this.textLargPlanCount.Name = "textLargPlanCount";
            this.textLargPlanCount.Size = new Size(0x72, 0x15);
            this.textLargPlanCount.TabIndex = 9;
            this.combLargPlanNPType.Enabled = false;
            this.combLargPlanNPType.FormattingEnabled = true;
            this.combLargPlanNPType.Location = new Point(0x4f, 20);
            this.combLargPlanNPType.Name = "combLargPlanNPType";
            this.combLargPlanNPType.Size = new Size(0x72, 20);
            this.combLargPlanNPType.TabIndex = 8;
            this.btnLargPlanExit.Enabled = false;
            this.btnLargPlanExit.Location = new Point(0x86, 0x1be);
            this.btnLargPlanExit.Name = "btnLargPlanExit";
            this.btnLargPlanExit.Size = new Size(0x4b, 0x17);
            this.btnLargPlanExit.TabIndex = 8;
            this.btnLargPlanExit.Text = "完成";
            this.btnLargPlanExit.UseVisualStyleBackColor = true;
            this.btnLargPlanExit.Visible = false;
            this.GroupBox2.BackColor = SystemColors.ButtonFace;
            this.GroupBox2.Controls.Add(this.checkIsLargPlan);
            this.GroupBox2.Controls.Add(this.btnImportFromHtm);
            this.GroupBox2.Controls.Add(this.label21);
            this.GroupBox2.Controls.Add(this.rTextAddition);
            this.GroupBox2.Controls.Add(this.Label13);
            this.GroupBox2.Controls.Add(this.CombPlanType);
            this.GroupBox2.Controls.Add(this.TextTotalCount);
            this.GroupBox2.Controls.Add(this.Label12);
            this.GroupBox2.Controls.Add(this.BtnCreatePlan);
            this.GroupBox2.Controls.Add(this.Label3);
            this.GroupBox2.Controls.Add(this.DateDeadLine);
            this.GroupBox2.Controls.Add(this.TextPlanID);
            this.GroupBox2.Controls.Add(this.Label6);
            this.GroupBox2.Controls.Add(this.Label2);
            this.GroupBox2.Controls.Add(this.DatePlanTime);
            this.GroupBox2.Controls.Add(this.Label1);
            this.GroupBox2.Controls.Add(this.CombPlanDepart);
            this.GroupBox2.Dock = DockStyle.Top;
            this.GroupBox2.Location = new Point(5, 5);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new Size(0xda, 0x134);
            this.GroupBox2.TabIndex = 0x1f;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "计划单录入";
            this.checkIsLargPlan.AutoSize = true;
            this.checkIsLargPlan.Location = new Point(0x4f, 0x7a);
            this.checkIsLargPlan.Name = "checkIsLargPlan";
            this.checkIsLargPlan.Size = new Size(0x30, 0x10);
            this.checkIsLargPlan.TabIndex = 0x1f;
            this.checkIsLargPlan.Text = "大单";
            this.checkIsLargPlan.UseVisualStyleBackColor = true;
            this.checkIsLargPlan.Click += new EventHandler(this.checkIsLargPlan_Click);
            this.checkIsLargPlan.CheckedChanged += new EventHandler(this.checkIsLargPlan_CheckedChanged);
            this.btnImportFromHtm.Location = new Point(0x18, 0x119);
            this.btnImportFromHtm.Name = "btnImportFromHtm";
            this.btnImportFromHtm.Size = new Size(0x4b, 0x17);
            this.btnImportFromHtm.TabIndex = 12;
            this.btnImportFromHtm.Text = "文件导入";
            this.btnImportFromHtm.UseVisualStyleBackColor = true;
            this.btnImportFromHtm.Click += new EventHandler(this.btnImportFromHtm_Click);
            this.label21.AutoSize = true;
            this.label21.Location = new Point(0x15, 0xc3);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0x35, 12);
            this.label21.TabIndex = 30;
            this.label21.Text = "备    注";
            this.rTextAddition.Location = new Point(0x17, 0xd7);
            this.rTextAddition.Name = "rTextAddition";
            this.rTextAddition.Size = new Size(0xa9, 0x38);
            this.rTextAddition.TabIndex = 0x1d;
            this.rTextAddition.Text = "";
            this.Label13.AutoSize = true;
            this.Label13.Location = new Point(0x15, 0x2f);
            this.Label13.Name = "Label13";
            this.Label13.Size = new Size(0x35, 12);
            this.Label13.TabIndex = 0x1c;
            this.Label13.Text = "计划类型";
            this.CombPlanType.FormattingEnabled = true;
            this.CombPlanType.Location = new Point(0x4f, 0x2b);
            this.CombPlanType.Name = "CombPlanType";
            this.CombPlanType.Size = new Size(0x72, 20);
            this.CombPlanType.TabIndex = 0x1b;
            this.CombPlanType.SelectedIndexChanged += new EventHandler(this.CombPlanType_SelectedIndexChanged);
            this.TextTotalCount.Location = new Point(0x4f, 0xab);
            this.TextTotalCount.Name = "TextTotalCount";
            this.TextTotalCount.Size = new Size(0x72, 0x15);
            this.TextTotalCount.TabIndex = 0x1a;
            this.Label12.AutoSize = true;
            this.Label12.Location = new Point(0x15, 0xb0);
            this.Label12.Name = "Label12";
            this.Label12.Size = new Size(0x35, 12);
            this.Label12.TabIndex = 0x19;
            this.Label12.Text = "计划数量";
            this.BtnCreatePlan.Location = new Point(0x72, 0x119);
            this.BtnCreatePlan.Name = "BtnCreatePlan";
            this.BtnCreatePlan.Size = new Size(0x4b, 0x17);
            this.BtnCreatePlan.TabIndex = 0x18;
            this.BtnCreatePlan.Text = "保存计划单";
            this.BtnCreatePlan.UseVisualStyleBackColor = true;
            this.BtnCreatePlan.Click += new EventHandler(this.BtnCreatePlan_Click);
            this.Label3.AutoSize = true;
            this.Label3.Location = new Point(20, 0x65);
            this.Label3.Name = "Label3";
            this.Label3.Size = new Size(0x35, 12);
            this.Label3.TabIndex = 0x12;
            this.Label3.Text = "交付时间";
            this.DateDeadLine.Location = new Point(0x4f, 0x60);
            this.DateDeadLine.Name = "DateDeadLine";
            this.DateDeadLine.Size = new Size(0x72, 0x15);
            this.DateDeadLine.TabIndex = 0x11;
            this.TextPlanID.Enabled = false;
            this.TextPlanID.Location = new Point(0x4f, 0x90);
            this.TextPlanID.Name = "TextPlanID";
            this.TextPlanID.Size = new Size(0x72, 0x15);
            this.TextPlanID.TabIndex = 0x10;
            this.Label6.AutoSize = true;
            this.Label6.Location = new Point(0x15, 0x95);
            this.Label6.Name = "Label6";
            this.Label6.Size = new Size(0x35, 12);
            this.Label6.TabIndex = 15;
            this.Label6.Text = "计划单号";
            this.Label2.AutoSize = true;
            this.Label2.Location = new Point(20, 0x4a);
            this.Label2.Name = "Label2";
            this.Label2.Size = new Size(0x35, 12);
            this.Label2.TabIndex = 5;
            this.Label2.Text = "下达时间";
            this.DatePlanTime.Location = new Point(0x4f, 0x45);
            this.DatePlanTime.Name = "DatePlanTime";
            this.DatePlanTime.Size = new Size(0x72, 0x15);
            this.DatePlanTime.TabIndex = 4;
            this.Label1.AutoSize = true;
            this.Label1.Location = new Point(0x15, 20);
            this.Label1.Name = "Label1";
            this.Label1.Size = new Size(0x35, 12);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "计划单位";
            this.CombPlanDepart.FormattingEnabled = true;
            this.CombPlanDepart.Location = new Point(0x4f, 0x11);
            this.CombPlanDepart.Name = "CombPlanDepart";
            this.CombPlanDepart.Size = new Size(0x72, 20);
            this.CombPlanDepart.TabIndex = 2;
            this.CombPlanDepart.SelectedIndexChanged += new EventHandler(this.CombPlanDepart_SelectedIndexChanged);
            this.GroupBox1.Controls.Add(this.btnSaveNPTemp);
            this.GroupBox1.Controls.Add(this.checkBackPiece);
            this.GroupBox1.Controls.Add(this.checkFontPiece);
            this.GroupBox1.Controls.Add(this.btnInputMode);
            this.GroupBox1.Controls.Add(this.btn_CheckMode);
            this.GroupBox1.Controls.Add(this.BtnSaveNP);
            this.GroupBox1.Controls.Add(this.CombNPType);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Dock = DockStyle.Bottom;
            this.GroupBox1.Location = new Point(5, 0x26e);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new Size(0xda, 0x8d);
            this.GroupBox1.TabIndex = 30;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "车牌录入";
            this.btnSaveNPTemp.Location = new Point(0x17, 0x6f);
            this.btnSaveNPTemp.Name = "btnSaveNPTemp";
            this.btnSaveNPTemp.Size = new Size(0x4b, 0x17);
            this.btnSaveNPTemp.TabIndex = 0x20;
            this.btnSaveNPTemp.Text = "临时保存";
            this.btnSaveNPTemp.UseVisualStyleBackColor = true;
            this.btnSaveNPTemp.Click += new EventHandler(this.btnSaveNPTemp_Click);
            this.checkBackPiece.AutoSize = true;
            this.checkBackPiece.Location = new Point(0x72, 0x2f);
            this.checkBackPiece.Name = "checkBackPiece";
            this.checkBackPiece.Size = new Size(0x30, 0x10);
            this.checkBackPiece.TabIndex = 0x1f;
            this.checkBackPiece.Text = "后片";
            this.checkBackPiece.UseVisualStyleBackColor = true;
            this.checkFontPiece.AutoSize = true;
            this.checkFontPiece.Location = new Point(0x17, 0x2c);
            this.checkFontPiece.Name = "checkFontPiece";
            this.checkFontPiece.Size = new Size(0x30, 0x10);
            this.checkFontPiece.TabIndex = 30;
            this.checkFontPiece.Text = "前片";
            this.checkFontPiece.UseVisualStyleBackColor = true;
            this.btnInputMode.Location = new Point(0x17, 0x4d);
            this.btnInputMode.Name = "btnInputMode";
            this.btnInputMode.Size = new Size(0x4b, 0x17);
            this.btnInputMode.TabIndex = 0x1d;
            this.btnInputMode.Text = "输入模式";
            this.btnInputMode.UseVisualStyleBackColor = true;
            this.btnInputMode.Click += new EventHandler(this.btnInputMode_Click);
            this.groupBox4.Controls.Add(this.GridNPInfo);
            this.groupBox4.Dock = DockStyle.Fill;
            this.groupBox4.Location = new Point(5, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x30e, 0x205);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "车牌信息";
            this.GridNPInfo.AllowUserToAddRows = false;
            this.GridNPInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridNPInfo.Dock = DockStyle.Fill;
            this.GridNPInfo.Location = new Point(3, 0x11);
            this.GridNPInfo.MultiSelect = false;
            this.GridNPInfo.Name = "GridNPInfo";
            this.GridNPInfo.ReadOnly = true;
            this.GridNPInfo.RowTemplate.Height = 0x17;
            this.GridNPInfo.ScrollBars = ScrollBars.Vertical;
            this.GridNPInfo.Size = new Size(0x308, 0x1f1);
            this.GridNPInfo.TabIndex = 0x25;
            this.GridNPInfo.UserDeletedRow += new DataGridViewRowEventHandler(this.GridNPInfo_UserDeletedRow);
            this.GridNPInfo.RowHeaderMouseDoubleClick += new DataGridViewCellMouseEventHandler(this.GridNPInfo_RowHeaderMouseDoubleClick);
            this.GridNPInfo.CellEnter += new DataGridViewCellEventHandler(this.GridNPInfo_CellEnter);
            this.GridNPInfo.CellContentClick += new DataGridViewCellEventHandler(this.GridNPInfo_CellContentClick);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x400, 0x300);
            base.Controls.Add(this.splitContainer1);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "Frm_Input";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "计划管理";
            base.FormClosed += new FormClosedEventHandler(this.Frm_Input_FormClosed);
            base.FontChanged += new EventHandler(this.Frm_Input_FontChanged);
            base.Load += new EventHandler(this.Frm_Input_Load);
            this.tabShow.ResumeLayout(false);
            this.PalNpNum.ResumeLayout(false);
            this.PalNpNum.PerformLayout();
            this.tabNPInfo.ResumeLayout(false);
            this.palNPAllInfo.ResumeLayout(false);
            ((ISupportInitialize) this.GridSameNPInfo).EndInit();
            this.palLargInput.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupChar.ResumeLayout(false);
            this.groupChar.PerformLayout();
            this.tabLargInput.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((ISupportInitialize) this.GridNPInfo).EndInit();
            base.ResumeLayout(false);
        }

        private bool IsInDataset(string NpNum, string NpType)
        {
            for (int i = 0; i < this.InputDS.Tables[0].Rows.Count; i++)
            {
                if ((NpNum.CompareTo(this.InputDS.Tables[0].Rows[i]["NPNum"]) == 0) && (NpType.CompareTo(this.InputDS.Tables[0].Rows[i]["NPTypeID"]) == 0))
                {
                    return (MessageBox.Show(this, "已经输入过该车牌信息！确定要输入吗？", "通产车牌生产", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No);
                }
            }
            return false;
        }

        private void NpWaringtoInputMode()
        {
            this.tabControl1.SelectedIndex = 0;
            this.TextShowNPNum.Text = this.TextShowNPNum.Text.Substring(0, 2);
            this.TextShowNPNum.SelectionStart = this.TextShowNPNum.Text.Length;
            this.TextShowNPNum.Focus();
            this.TextShowNPNum.SelectionStart = this.TextShowNPNum.Text.Length;
            this.TextShowNPNum.Enabled = true;
            this.GridSameNPInfo.DataSource = null;
            this.GridSameNPInfo.RowCount = 1;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.GridSameNPInfo.RowCount > 1)
            {
                this.tabControl1.SelectedIndex = 2;
            }
            else
            {
                switch (this.tabControl1.SelectedIndex)
                {
                    case 0:
                        this.TextShowNPNum.Text = this.TextShowNPNum.Text.Substring(0, 2);
                        return;

                    case 1:
                        this.ClearLargInputPanel();
                        break;

                    case 2:
                        break;

                    default:
                        return;
                }
            }
        }

        private void textFWBegin_TextChanged(object sender, EventArgs e)
        {
            this.textFWBegin.Text = this.textFWBegin.Text.ToUpper();
        }

        private void textFWEnd_TextChanged(object sender, EventArgs e)
        {
            this.textFWEnd.Text = this.textFWEnd.Text.ToUpper();
        }

        private void TextNPNum_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (this.TextShowNPNum.Text.Substring(0, 2) == "鲁B")
                {
                    this.TextShowNPNum.Text = "鲁U";
                }
                else if (this.TextShowNPNum.Text.Substring(0, 2) == "鲁U")
                {
                    this.TextShowNPNum.Text = "鲁B";
                }
            }
            this.tabControl1.SelectedIndex = 0;
        }

        private void TextNPNum_TextChanged(object sender, EventArgs e)
        {
            string str = this.VeryfyOthersNP(this.TextShowNPNum.Text);
            this.TextShowNPNum.Text = str;
            this.TextShowNPNum.SelectionStart = this.TextShowNPNum.Text.Length;
            if (!this.G_IsCheck)
            {
                this.UpdateNPNumInInputMode();
            }
            else
            {
                this.UpdateNPNumInCheckMode();
            }
        }

        private void textNPTemplate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space)
            {
                this.textNPTemplate.Text.ToUpper();
                this.textNPTemplate.Text = this.VeryfycharAfer(this.textNPTemplate.Text.Trim());
                this.textNPTemplate.SelectionStart = this.textNPTemplate.Text.Trim().Length;
            }
        }

        private void textNPTemplate_TextChanged(object sender, EventArgs e)
        {
            this.textNPTemplate.Text.ToUpper();
            if ((this.textNPTemplate.Text.IndexOf(" ") == 0) && (this.textNPTemplate.Text.Length >= 7))
            {
                this.textNPTemplate.Text = this.textNPTemplate.Text.Substring(0, 6);
            }
        }

        private void UpdateNPNumInCheckMode()
        {
            try
            {
                switch (this.TextShowNPNum.Text.Length)
                {
                    case 6:
                        if (this.CombNPType.Text == "挂车")
                        {
                            this.TextShowNPNum.Text = this.TextShowNPNum.Text + "挂";
                        }
                        if (this.CombNPType.Text == "领馆")
                        {
                            this.TextShowNPNum.Text = this.TextShowNPNum.Text + "领";
                        }
                        if (this.CombNPType.Text == "教练")
                        {
                            this.TextShowNPNum.Text = this.TextShowNPNum.Text + "学";
                        }
                        if (this.CombNPType.Text == "境外")
                        {
                            this.TextShowNPNum.Text = this.TextShowNPNum.Text + "境";
                        }
                        return;

                    case 7:
                        return;
                }
                this.TextShowNPNum.Text = this.TextShowNPNum.Text.Substring(0, 6);
            }
            catch (Exception)
            {
            }
        }

        private void UpdateNPNumInInputMode()
        {
            try
            {
                switch (this.TextShowNPNum.Text.Length)
                {
                    case 6:
                        if (this.CombNPType.Text == "挂车")
                        {
                            this.TextShowNPNum.Text = this.TextShowNPNum.Text + "挂";
                        }
                        if (this.CombNPType.Text == "领馆")
                        {
                            this.TextShowNPNum.Text = this.TextShowNPNum.Text + "领";
                        }
                        if (this.CombNPType.Text == "教练")
                        {
                            this.TextShowNPNum.Text = this.TextShowNPNum.Text + "学";
                        }
                        if (this.CombNPType.Text == "境外")
                        {
                            this.TextShowNPNum.Text = this.TextShowNPNum.Text + "境";
                        }
                        return;

                    case 7:
                        try
                        {
                            Convert.ToInt32(this.TextTotalCount.Text);
                        }
                        catch
                        {
                            return;
                        }
                        if (this.IsInDataset(this.TextShowNPNum.Text, this.CombNPType.SelectedValue.ToString()))
                        {
                            this.TextShowNPNum.Text = this.TextShowNPNum.Text.Substring(0, 2);
                        }
                        else if (!this.checkBackPiece.Checked && !this.checkFontPiece.Checked)
                        {
                            MessageBox.Show(this, "请选择前片、后片！", "通产车牌生产", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else if (this.ComPareDBNP(this.TextShowNPNum.Text, this.CombNPType.SelectedValue.ToString()) != 0)
                        {
                            this.tabControl1.SelectedIndex = 2;
                            this.TextShowNPNum.Enabled = false;
                        }
                        else
                        {
                            this.AddOneNP();
                            if (this.G_NPCount == Convert.ToInt32(this.TextTotalCount.Text.Trim()))
                            {
                                MessageBox.Show(this, "输入的车牌数量已经等于计划单中的总数！", "通产车牌生产", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            }
                        }
                        return;
                }
                this.TextShowNPNum.Text = this.TextShowNPNum.Text.Substring(0, 6);
            }
            catch (Exception)
            {
            }
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
                else if (((((num != 0xdb) && (num != 0xdd)) && ((num <= 0) || (num >= 0x30))) && ((num <= 0x39) || (num >= 0x41))) && (((num <= 90) || (num >= 0x61)) && (num <= 0x84)))
                {
                    str = str + charAfer.Substring(i, 1);
                }
            }
            return str;
        }

        private string VeryfyNP(string NpStr)
        {
            NpStr = NpStr.ToUpper();
            switch (NpStr.Length)
            {
                case 0:
                    return "鲁B";

                case 1:
                    return "鲁B";

                case 2:
                    if ((NpStr.Substring(0, 2).CompareTo("鲁B") == 0) || (NpStr.Substring(0, 2).CompareTo("鲁U") == 0))
                    {
                        return NpStr;
                    }
                    return "鲁B";

                case 3:
                    if ((NpStr.Substring(0, 2).CompareTo("鲁B") == 0) || (NpStr.Substring(0, 2).CompareTo("鲁U") == 0))
                    {
                        return (NpStr.Substring(0, 2) + this.VeryfycharAfer(NpStr.Substring(2, 1)));
                    }
                    return "鲁B";

                case 4:
                    if ((NpStr.Substring(0, 2).CompareTo("鲁B") == 0) || (NpStr.Substring(0, 2).CompareTo("鲁U") == 0))
                    {
                        return (NpStr.Substring(0, 2) + this.VeryfycharAfer(NpStr.Substring(2, 2)));
                    }
                    return "鲁B";

                case 5:
                    if ((NpStr.Substring(0, 2).CompareTo("鲁B") == 0) || (NpStr.Substring(0, 2).CompareTo("鲁U") == 0))
                    {
                        return (NpStr.Substring(0, 2) + this.VeryfycharAfer(NpStr.Substring(2, 3)));
                    }
                    return "鲁B";

                case 6:
                    if ((NpStr.Substring(0, 2).CompareTo("鲁B") == 0) || (NpStr.Substring(0, 2).CompareTo("鲁U") == 0))
                    {
                        return (NpStr.Substring(0, 2) + this.VeryfycharAfer(NpStr.Substring(2, 4)));
                    }
                    return "鲁B";

                case 7:
                    if ((NpStr.Substring(0, 2).CompareTo("鲁B") == 0) || (NpStr.Substring(0, 2).CompareTo("鲁U") == 0))
                    {
                        return (NpStr.Substring(0, 2) + this.VeryfycharAfer(NpStr.Substring(2, 5)));
                    }
                    return "鲁B";
            }
            return "";
        }

        private string VeryfyOthersNP(string NpStr)
        {
            NpStr = NpStr.ToUpper();
            try
            {
                if (this.textNPTemplate.Text.Substring(NpStr.Length, 1).CompareTo(" ") != 0)
                {
                    NpStr = NpStr + this.textNPTemplate.Text.Substring(NpStr.Length, 1);
                }
            }
            catch
            {
            }
            return this.VeryfycharAfer(NpStr);
        }

        public static Frm_Input Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Frm_Input();
                }
                return m_Instance;
            }
        }

        public string UserId
        {
            get
            {
                return this.G_UserID;
            }
            set
            {
                this.G_UserID = value;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LargPlanInfo
        {
            public int count;
            public string NPType;
        }
    }
}

