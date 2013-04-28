namespace NpMis
{
    using BarCodePrint;
    using Common;
    //using Excel;
    using NpMis.Control;
    using NpMis.Frm;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Text;
    using System.Windows.Forms;
    using WarehouseMan;

    public class Frm_Query : Form
    {
        private string AttendanceQueryHead = "SELECT T_User.TrueName AS 姓名, T_Attendance.ArriveTime AS 上班时间,   T_Attendance.LeaveTime AS 下班时间 FROM T_Attendance INNER JOIN T_User ON T_Attendance.PersonID = T_User.TrueName";
        private Button btn_AttendStat;
        private Button btn_ComQuery;
        private Button btn_ComQuery_Task;
        private Button btn_MbTaskId;
        private Button btn_MbTime;
        private Button btn_OutTime;
        private Button btn_PlanId;
        private Button btn_PlanId_Task;
        private Button btn_PlanIdQuery;
        private Button btn_PrintCode;
        private Button btn_PrintStateResult;
        private Button btn_Query_Task;
        private Button btn_QueryNpNo;
        private Button btn_RePrintSend;
        private Button btn_RePrintTask;
        private Button btn_SendId;
        private Button btn_SendId_Send;
        private Button btn_State;
        private Button btn_TaskId;
        private Button btn_Works;
        private Button btnAbsent;
        private Button btnExpAccount2Excel;
        private Button btnExpDetails2Excel;
        private Button btnExpExcel;
        private Button btnInHouse;
        private Button btnPanAccount;
        private Button btnPanProcess;
        private Button btnRemakeAppr;
        private Button btnTaskPan;
        private Button btnTaskProcess;
        private Button btnTaskSend;
        private Button btnWarehouseOperate;
        private CheckBox cbx_Big;
        private ComboBox cbx_ChangeList;
        private ComboBox cbx_IsReceive;
        private ComboBox cbx_NpState;
        private ComboBox cbx_Person;
        private ComboBox cbx_PlanDepart;
        private ComboBox cbx_PlanState;
        private ComboBox cbx_PlanType;
        private CheckBox cbx_PreviewPrintState;
        private CheckBox cbx_PrintCodeBar;
        private CheckBox cbx_PrintSendView;
        private CheckBox cbx_PrintView;
        private CheckBox cbx_ReceiveMoney;
        private ComboBox cbx_relation;
        private CheckBox cbx_SignIn;
        private ComboBox cbx_TimeType;
        private ComboBox cbx_Workers;
        private IContainer components;
        private DataGridView dataGridView1;
        private int DetailPrintPage;
        private DataGridView dgv_Mboard;
        private DataGridView dgv_Plan;
        private DataGridView dgv_Send;
        private DataGridView dgv_StatbyNPType;
        private DataGridView dgv_State;
        private DataGridView dgv_Task;
        private DataGridView dgv_TaskDetail;
        private DataGridView drw_NpResult;
        private DateTimePicker dtp_absentFrom;
        private DateTimePicker dtp_absentTo;
        private DateTimePicker dtp_From;
        private DateTimePicker dtp_From_Send;
        private DateTimePicker dtp_From_Task;
        private DateTimePicker dtp_FromAttend;
        private DateTimePicker dtp_FromPlan;
        private DateTimePicker dtp_FromSend;
        private DateTimePicker dtp_FromState;
        private DateTimePicker dtp_FromWorks;
        private DateTimePicker dtp_MbFrom;
        private DateTimePicker dtp_MbTo;
        private DateTimePicker dtp_To;
        private DateTimePicker dtp_To_Send;
        private DateTimePicker dtp_To_Task;
        private DateTimePicker dtp_ToAttend;
        private DateTimePicker dtp_ToPlan;
        private DateTimePicker dtp_ToSend;
        private DateTimePicker dtp_ToState;
        private DateTimePicker dtp_ToWorks;
        private GroupBox groupBox1;
        private GroupBox groupBox10;
        private GroupBox groupBox11;
        private GroupBox groupBox12;
        private GroupBox groupBox13;
        private GroupBox groupBox14;
        private GroupBox groupBox15;
        private GroupBox groupBox16;
        private GroupBox groupBox17;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        private GroupBox groupBox8;
        private GroupBox groupBox9;
        private Label lab_StateInfo;
        private Label lab_Total;
        private Label lab_Total_Mb;
        private Label lab_Total_Send;
        private Label lab_Total_Task;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label16;
        private Label label17;
        private Label label18;
        private Label label19;
        private Label label2;
        private Label label20;
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label24;
        private Label label25;
        private Label label26;
        private Label label27;
        private Label label28;
        private Label label29;
        private Label label3;
        private Label label30;
        private Label label31;
        private Label label32;
        private Label label33;
        private Label label34;
        private Label label35;
        private Label label36;
        private Label label37;
        private Label label38;
        private Label label39;
        private Label label4;
        private Label label40;
        private Label label41;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label labelResultInfo;
        private Label labelTaskInfo;
        private Label labTotal;
        private static Frm_Query m_Instance;
        private string MbTaskProcessQueryHead = "SELECT T_WorkInfo.TaskID AS 任务单号,V_ProcessAll.CodeName AS 工序, t_user.TrueName AS 操作人员, T_WorkInfo.BeginTime AS 开始时间, T_WorkInfo.EndTime AS 完成时间 FROM T_WorkInfo INNER JOIN V_ProcessAll ON T_WorkInfo.ProcessID = V_ProcessAll.Code INNER JOIN T_User t_user ON T_WorkInfo.PersonID = t_user.UserName";
        private string MbTaskResult = "select MbTaskId as '底板任务单号',Type as '尺寸',Wpro as '工序',MbAmount as '底板总数',BarCodeAmount as '箱数',MbTaskUser as '下达人员',MbTaskTime as '下达时间' from V_MbTask  ";
        private Sql2KDataAccess MyDB = new Sql2KDataAccess();
        private string NpResult = "select NpNo as '车牌号码',CodeName as '车牌类型',Description as '类型简称',PlanId as '计划单号',TaskId as '生产任务单号',SendId as '送货单号',IsFront as '前片',IsBack as '后片', IsMail as '邮寄',PlanTime as '计划下达时间',DeadLine as '最后完成期限',InputTime as '录入时间',InputUser as '录入人员',AuditingTime as '审核时间',AuditingUser as '审核人员',TaskTime as '任务下达时间',TaskUser as '任务下达人员',PressTime as '压字时间',PressU as '压字人员',EraseTime as '擦字时间',EraseU as '擦字人员',DryingTime as '烘干时间',DryingU as '烘干人员',SmearTime as '涂印时间',SmearU as '涂印人员',ClashTime as '冲安装孔时间',ClashU as '冲安装孔人员',PackTime as '打包时间',PackU as '打包人员',SendTime as '送货时间',SendUser as '送货人员' from V_NpQuery ";
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private string PlanResult = "select PlanId as '计划指令单号',PlanTime as '计划下达时间',PlanNum as '计划数',TotalCount as '实际数',MakedNum as '已制作数',PlanDepart as '计划部门',PlanKind as '计划单类型',DeadLine as '最后完成期限',InputTime as '录入时间',InputUser as '录入人员',AuditingTime as '审核时间',AuditingUser as '审核人员',case IsPass When '1' then '审核通过' When '0' then '审核未通过' end as '审核结果',ReceivedMoney as '收款标记' from V_Plan ";
        private DataSet SendListDetailsds;
        private DataSet SendListds;
        private string SendResult = "select SendId as '送货单号',SendTime as '送货单生成时间',SendUser as '送货单下达人员',PlanId as '计划单号',PlanDepart as '计划部门',ReceivePerson as '签收人' from V_SendList ";
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private SplitContainer splitContainer6;
        private SplitContainer splitDGD;
        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private TabPage tabPage7;
        private string TaskProcessQueryHead = " SELECT  T_WorkInfo.TaskID AS 任务单号, V_ProcessAll.CodeName AS 工序,  T_User.TrueName AS 操作人员, T_WorkInfo.BeginTime AS 开始时间,  T_WorkInfo.EndTime AS 完成时间  FROM T_WorkInfo INNER JOIN  V_ProcessAll ON T_WorkInfo.ProcessID = V_ProcessAll.Code INNER JOIN  T_User ON T_WorkInfo.PersonID = T_User.UserName INNER JOIN  T_Task ON T_WorkInfo.TaskID = T_Task.TaskID ";
        private string TaskResult = "select TaskId as '生产任务单号',TaskTime as '任务下达时间',TaskUser as '任务下达用户',NpNum as '车牌总数',PressTime as '压字时间',PressU as '压字人员',EraseTime as '擦字时间',EraseU as '擦字人员',DryingTime as '烘干时间',DryingU as '烘干人员',SmearTime as '涂印时间',SmearU as '涂印人员',ClashTime as '冲安装孔时间',ClashU as '冲安装孔人员',PackTime as '打包时间',PackU as '打包人员' from V_Task ";
        private ReturnText tbx_MbTaskId;
        private ReturnText tbx_NpNo;
        private ReturnText tbx_PlanId;
        private ReturnText tbx_PlanId_Task;
        private ReturnText tbx_SendId;
        private ReturnText tbx_SendId_Send;
        private ReturnText tbx_TaskId;
        private TreeView treeVwAccount;
        private ReturnText txt_PlanId;
        private ReturnText txt_TaskId;
        private string WithdrawQueryHead = " SELECT T_WithdrawPan.TaskID AS 任务单号, T_WithdrawPan.PanTaskID AS 底板编号,  V_MbType.CodeName AS 底板类型, T_WithdrawPan.PanCount AS 领取片数,  T_WithdrawPan.WithdrawTime AS 领料时间, u1.TrueName AS 领料人,  u2.TrueName AS 操作人 FROM T_WithdrawPan INNER JOIN  T_Task ON T_WithdrawPan.TaskID = T_Task.TaskID INNER JOIN V_MbType ON T_WithdrawPan.PanType = V_MbType.Code INNER JOIN T_User u1 ON T_WithdrawPan.WithdrawPerson = u1.UserName INNER JOIN T_User u2 ON T_WithdrawPan.OperatePerson = u2.UserName";

        private Frm_Query()
        {
            this.InitializeComponent();
        }

        private void AccountNP()
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            DataSet set = new DataSet();
            string[] strArray = new string[7];
            string[] strArray2 = new string[7];
            int num = 0;
            strArray[0] = "领料";
            strArray[1] = "压字";
            strArray[2] = "擦字";
            strArray[3] = "涂印";
            strArray[4] = "烘干";
            strArray[5] = "冲孔";
            strArray[6] = "装箱";
            string str2 = "SELECT description, COUNT(npid) AS COUNT FROM V_NPQuery  WHERE (SendID IS NULL) AND description <> '4' and taskid is not null ";
            string str3 = " GROUP BY description";
            strArray2[0] = " and PressTime is null and withdrawtime is not null" + str3;
            strArray2[1] = " and presstime is not null and EraseTime is null and SmearTime is null and DryingTime is null and ClashTime is  null" + str3;
            strArray2[2] = " and presstime is not null and EraseTime is not null and SmearTime is null and DryingTime is null and ClashTime is  null" + str3;
            strArray2[3] = " and presstime is not null and EraseTime is null and SmearTime is not null and DryingTime is null and ClashTime is  null  " + str3;
            strArray2[4] = " and presstime is not null  and DryingTime is not null and ClashTime is  null and ProcessPackTime is  null " + str3;
            strArray2[5] = " and (ProcessPackTime is  null) and ClashTime is not  null and (presstime is not null and ((erasetime is not null and clashtime is not null) or (smeartime is not null)) and  dryingtime  is not null)" + str3;
            strArray2[6] = " and (ProcessPackTime is  not null) and (presstime is not null )" + str3;
            string queryStr = "select * from V_NPType";
            set = access.Run_SqlText(queryStr);
            if (set != null)
            {
                if (this.treeVwAccount.Nodes.ContainsKey("TCCONP"))
                {
                    this.treeVwAccount.Nodes.RemoveByKey("TCCONP");
                }
                TreeNode node = this.treeVwAccount.Nodes.Add("TCCONP", "车牌");
                for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                {
                    int num3 = 0;
                    TreeNode node2 = node.Nodes.Add("TCCONP" + set.Tables[0].Rows[i]["code"].ToString(), set.Tables[0].Rows[i]["description"].ToString());
                    queryStr = str2 + " and  description='" + set.Tables[0].Rows[i]["description"].ToString() + "' " + strArray2[0];
                    DataSet set2 = access.Run_SqlText(queryStr);
                    if ((set2 != null) && (set2.Tables[0].Rows.Count > 0))
                    {
                        node2.Nodes.Add("TCCONPD0", strArray[0] + "(" + set2.Tables[0].Rows[0]["count"].ToString() + ")");
                        num3 += Convert.ToInt32(set2.Tables[0].Rows[0]["count"].ToString());
                    }
                    queryStr = str2 + " and  description='" + set.Tables[0].Rows[i]["description"].ToString() + "' " + strArray2[1];
                    set2 = access.Run_SqlText(queryStr);
                    if ((set2 != null) && (set2.Tables[0].Rows.Count > 0))
                    {
                        node2.Nodes.Add("TCCONPD1", strArray[1] + "(" + set2.Tables[0].Rows[0]["count"].ToString() + ")");
                        num3 += Convert.ToInt32(set2.Tables[0].Rows[0]["count"].ToString());
                    }
                    queryStr = str2 + " and  description='" + set.Tables[0].Rows[i]["description"].ToString() + "' " + strArray2[2];
                    set2 = access.Run_SqlText(queryStr);
                    if ((set2 != null) && (set2.Tables[0].Rows.Count > 0))
                    {
                        node2.Nodes.Add("TCCONPD2", strArray[2] + "(" + set2.Tables[0].Rows[0]["count"].ToString() + ")");
                        num3 += Convert.ToInt32(set2.Tables[0].Rows[0]["count"].ToString());
                    }
                    queryStr = str2 + " and  description='" + set.Tables[0].Rows[i]["description"].ToString() + "' " + strArray2[3];
                    set2 = access.Run_SqlText(queryStr);
                    if ((set2 != null) && (set2.Tables[0].Rows.Count > 0))
                    {
                        node2.Nodes.Add("TCCONPD3", strArray[3] + "(" + set2.Tables[0].Rows[0]["count"].ToString() + ")");
                        num3 += Convert.ToInt32(set2.Tables[0].Rows[0]["count"].ToString());
                    }
                    queryStr = str2 + " and  description='" + set.Tables[0].Rows[i]["description"].ToString() + "' " + strArray2[4];
                    set2 = access.Run_SqlText(queryStr);
                    if ((set2 != null) && (set2.Tables[0].Rows.Count > 0))
                    {
                        node2.Nodes.Add("TCCONPD4", strArray[4] + "(" + set2.Tables[0].Rows[0]["count"].ToString() + ")");
                        num3 += Convert.ToInt32(set2.Tables[0].Rows[0]["count"].ToString());
                    }
                    queryStr = str2 + " and  description='" + set.Tables[0].Rows[i]["description"].ToString() + "' " + strArray2[5];
                    set2 = access.Run_SqlText(queryStr);
                    if ((set2 != null) && (set2.Tables[0].Rows.Count > 0))
                    {
                        node2.Nodes.Add("TCCONPD5", strArray[5] + "(" + set2.Tables[0].Rows[0]["count"].ToString() + ")");
                        num3 += Convert.ToInt32(set2.Tables[0].Rows[0]["count"].ToString());
                    }
                    queryStr = str2 + " and  description='" + set.Tables[0].Rows[i]["description"].ToString() + "'  " + strArray2[6];
                    set2 = access.Run_SqlText(queryStr);
                    if ((set2 != null) && (set2.Tables[0].Rows.Count > 0))
                    {
                        node2.Nodes.Add("TCCONPD6", strArray[6] + "(" + set2.Tables[0].Rows[0]["count"].ToString() + ")");
                        num3 += Convert.ToInt32(set2.Tables[0].Rows[0]["count"].ToString());
                    }
                    node2.Text = node2.Text + "(" + num3.ToString() + ")";
                    num += num3;
                }
                node.Text = node.Text + "(" + num.ToString() + ")";
            }
        }

        private void AccountNPWareOper()
        {
            string queryStr = "SELECT OperateType, COUNT(*) AS count FROM T_WarehouseOperation WHERE (DATEDIFF([day], OperateTime, GETDATE()) = 0) and left(taskid,1)='T'GROUP BY OperateType";
            DataSet set = this.MyDB.Run_SqlText(queryStr);
            if (set != null)
            {
                switch (set.Tables[0].Rows.Count)
                {
                    case 0:
                        this.treeVwAccount.Nodes.Add("车牌出库", "车牌出库(0)箱");
                        this.treeVwAccount.Nodes.Add("车牌入库", "车牌入库(0)箱");
                        return;

                    case 1:
                        this.treeVwAccount.Nodes.Add("车牌" + set.Tables[0].Rows[0]["operatetype"].ToString(), "车牌" + set.Tables[0].Rows[0]["operatetype"].ToString() + "(" + set.Tables[0].Rows[0]["count"].ToString() + ")箱");
                        if (set.Tables[0].Rows[0]["operatetype"].ToString().CompareTo("出库") == 0)
                        {
                            this.treeVwAccount.Nodes.Add("车牌入库", "车牌入库(0)箱");
                        }
                        return;

                    case 2:
                        this.treeVwAccount.Nodes.Add("车牌" + set.Tables[0].Rows[0]["operatetype"].ToString(), "车牌" + set.Tables[0].Rows[0]["operatetype"].ToString() + "(" + set.Tables[0].Rows[0]["count"].ToString() + ")箱");
                        this.treeVwAccount.Nodes.Add("车牌" + set.Tables[0].Rows[1]["operatetype"].ToString(), "车牌" + set.Tables[0].Rows[0]["operatetype"].ToString() + "(" + set.Tables[0].Rows[1]["count"].ToString() + ")箱");
                        return;

                    default:
                        return;
                }
            }
        }

        private void AccountPan()
        {
            int num;
            int num2;
            Sql2KDataAccess access = new Sql2KDataAccess();
            DataSet set = new DataSet();
            string[] strArray = new string[9];
            string[] strArray2 = new string[9];
            string[] strArray3 = new string[9];
            string[] strArray4 = new string[9];
            int[,] numArray = new int[6, 9];
            strArray[0] = "清洗中";
            strArray[1] = "清洗好";
            strArray[2] = "贴白膜";
            strArray[3] = "贴黄膜";
            strArray[4] = "丝印好";
            strArray[5] = "冲孔好";
            for (num = 0; num < 6; num++)
            {
                num2 = 0;
                while (num2 < 9)
                {
                    numArray[num, num2] = 0;
                    num2++;
                }
            }
            Hashtable hashtable = new Hashtable();
            string queryStr = "select * from v_Mbtype";
            set = access.Run_SqlText(queryStr);
            if ((set != null) && (set.Tables[0].Rows.Count > 0))
            {
                for (num = 0; num < set.Tables[0].Rows.Count; num++)
                {
                    hashtable.Add(Convert.ToInt32(set.Tables[0].Rows[num]["code"].ToString()), set.Tables[0].Rows[num]["CodeName"].ToString());
                }
            }
            string str2 = "SELECT SUM(pancount) as count FROM T_PanTaskInfo WHERE pancount>0 and";
            strArray2[5] = str2 + "  whiteoryellow='1'  and  (ClashU IS not NULL) GROUP BY Pantype";
            strArray2[4] = str2 + "  whiteoryellow='0' and (SilkScreenU IS not NULL) GROUP BY Pantype";
            strArray2[3] = str2 + "  whiteoryellow='1' and FilmU is not null and (ClashU IS NULL) GROUP BY Pantype";
            strArray2[2] = str2 + "  whiteoryellow='0' and FilmU is not null and (SilkScreenU IS NULL) GROUP BY Pantype";
            strArray2[1] = str2 + "  whiteoryellow is null  and cleanU is not null GROUP BY Pantype";
            strArray2[0] = str2 + "  whiteoryellow is null and cleanU is null  GROUP BY Pantype";
            strArray3[5] = str2 + " isinwarehouse='1' and  whiteoryellow='1'  and  (ClashU IS not NULL) ";
            strArray3[4] = str2 + " isinwarehouse='1' and whiteoryellow='0' and (SilkScreenU IS not NULL) ";
            strArray3[3] = str2 + " isinwarehouse='1' and whiteoryellow='1' and FilmU is not null and ClashU IS NULL ";
            strArray3[2] = str2 + " isinwarehouse='1' and whiteoryellow='0' and FilmU is not null and (SilkScreenU IS NULL) ";
            strArray3[1] = str2 + " isinwarehouse='1' and whiteoryellow is null  and cleanU is not null ";
            strArray3[0] = str2 + " isinwarehouse is null and whiteoryellow is null and cleanU is null ";
            strArray4[5] = str2 + " isinwarehouse='0' and whiteoryellow='1'  and  (ClashU IS not NULL) ";
            strArray4[4] = str2 + " isinwarehouse='0' and whiteoryellow='0' and (SilkScreenU IS not NULL) ";
            strArray4[3] = str2 + " isinwarehouse='0' and whiteoryellow='1' and FilmU is not null and (ClashU IS NULL) ";
            strArray4[2] = str2 + " isinwarehouse='0' and whiteoryellow='0' and FilmU is not null and (SilkScreenU IS NULL) ";
            strArray4[1] = str2 + " isinwarehouse='0' and whiteoryellow is null  and cleanU is not null ";
            strArray4[0] = str2 + " isinwarehouse='0' and whiteoryellow is null and cleanU is null  ";
            if (this.treeVwAccount.Nodes.ContainsKey("Pan"))
            {
                this.treeVwAccount.Nodes.RemoveByKey("Pan");
            }
            TreeNode node = this.treeVwAccount.Nodes.Add("Pan", "底板");
            long num3 = 0L;
            if ((set != null) && (set.Tables[0].Rows.Count > 0))
            {
                for (num = 0; num < set.Tables[0].Rows.Count; num++)
                {
                    long num5;
                    long num6;
                    long num4 = num5 = num6 = 0L;
                    TreeNode node2 = node.Nodes.Add("Pan" + set.Tables[0].Rows[num]["code"].ToString(), hashtable[Convert.ToInt32(set.Tables[0].Rows[num]["code"].ToString())].ToString());
                    TreeNode node3 = node2.Nodes.Add("PanIn" + set.Tables[0].Rows[num]["code"].ToString(), "库中");
                    num2 = 0;
                    while (num2 < 6)
                    {
                        long num7;
                        string str3 = strArray3[num2] + "and pantype='" + set.Tables[0].Rows[num]["code"].ToString() + "'";
                        DataSet set2 = access.Run_SqlText(str3);
                        try
                        {
                            num7 = Convert.ToInt64(set2.Tables[0].Rows[0]["count"].ToString());
                        }
                        catch
                        {
                            num7 = 0L;
                        }
                        if (num7 > 0L)
                        {
                            node3.Nodes.Add("PanInD" + num2.ToString(), strArray[num2] + "(" + num7.ToString() + ")");
                            num5 += num7;
                        }
                        num2++;
                    }
                    node3.Text = node3.Text + "(" + num5.ToString() + ")";
                    num4 += num5;
                    TreeNode node4 = node2.Nodes.Add("PanNotIn" + set.Tables[0].Rows[num]["code"].ToString(), "工序中");
                    for (num2 = 0; num2 < 6; num2++)
                    {
                        long num8;
                        string str4 = strArray4[num2] + "and pantype='" + set.Tables[0].Rows[num]["code"].ToString() + "'";
                        DataSet set3 = access.Run_SqlText(str4);
                        try
                        {
                            num8 = Convert.ToInt64(set3.Tables[0].Rows[0]["count"].ToString());
                        }
                        catch
                        {
                            num8 = 0L;
                        }
                        if (num8 > 0L)
                        {
                            node4.Nodes.Add("PanNotInD" + num2.ToString(), strArray[num2] + "(" + num8.ToString() + ")");
                            num6 += num8;
                        }
                    }
                    node4.Text = node4.Text + "(" + num6.ToString() + ")";
                    num4 += num6;
                    num3 += num4;
                    node2.Text = node2.Text + "(" + num4.ToString() + ")";
                }
            }
            node.Text = node.Text + "(" + num3.ToString() + ")";
        }

        private void AccountPanWareOper()
        {
            string queryStr = "SELECT OperateType, COUNT(*) AS count FROM T_WarehouseOperation WHERE (DATEDIFF([day], OperateTime, GETDATE()) = 0) and left(taskid,1)='M'GROUP BY OperateType";
            DataSet set = this.MyDB.Run_SqlText(queryStr);
            if (set != null)
            {
                switch (set.Tables[0].Rows.Count)
                {
                    case 0:
                        this.treeVwAccount.Nodes.Add("底板出库", "底板出库(0)箱");
                        this.treeVwAccount.Nodes.Add("底板入库", "底板入库(0)箱");
                        return;

                    case 1:
                        this.treeVwAccount.Nodes.Add("底板" + set.Tables[0].Rows[0]["operatetype"].ToString(), "底板" + set.Tables[0].Rows[0]["operatetype"].ToString() + "(" + set.Tables[0].Rows[0]["count"].ToString() + ")箱");
                        if (set.Tables[0].Rows[0]["operatetype"].ToString().CompareTo("出库") != 0)
                        {
                            this.treeVwAccount.Nodes.Add("底板出库", "底板出库(0)箱");
                            return;
                        }
                        this.treeVwAccount.Nodes.Add("底板入库", "底板入库(0)箱");
                        return;

                    case 2:
                        this.treeVwAccount.Nodes.Add("底板" + set.Tables[0].Rows[0]["operatetype"].ToString(), "底板" + set.Tables[0].Rows[0]["operatetype"].ToString() + "(" + set.Tables[0].Rows[0]["count"].ToString() + ")箱");
                        this.treeVwAccount.Nodes.Add("底板" + set.Tables[0].Rows[1]["operatetype"].ToString(), "底板" + set.Tables[0].Rows[1]["operatetype"].ToString() + "(" + set.Tables[0].Rows[1]["count"].ToString() + ")箱");
                        return;

                    default:
                        return;
                }
            }
        }

        private void AccountWithdrawToday()
        {
            string queryStr = "SELECT codename, pantype, SUM(pancount) AS COUNT FROM T_WithdrawPan, v_mbtype WHERE (DATEDIFF([day], WithDrawTime, GETDATE()) = 0) AND  v_mbtype.code = T_WithdrawPan.pantype GROUP BY codename, pantype";
            DataSet set = this.MyDB.Run_SqlText(queryStr);
            if (set != null)
            {
                TreeNode node = this.treeVwAccount.Nodes.Add("WithDrawPan", "车牌领料合计");
                int num = 0;
                if (set.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                    {
                        node.Nodes.Add("WithDrawPan" + set.Tables[0].Rows[i]["pantype"].ToString(), set.Tables[0].Rows[i]["codename"].ToString() + "(" + set.Tables[0].Rows[i]["count"].ToString() + ")");
                        num += int.Parse(set.Tables[0].Rows[i]["count"].ToString());
                    }
                }
                node.Text = node.Text + "(" + num.ToString() + ")";
            }
        }

        private void btn_AttendStat_Click(object sender, EventArgs e)
        {
            string str = "";
            this.splitContainer1.Panel2Collapsed = true;
            if (this.cbx_Person.Text.Trim() != "")
            {
                str = " and PersonId='" + this.cbx_Person.Text.Trim() + "'";
            }
            Sql2KDataAccess access = new Sql2KDataAccess();
            string queryStr = this.AttendanceQueryHead + " Where Arrivetime between '" + this.dtp_FromAttend.Value.ToShortDateString() + "' and '" + this.dtp_ToAttend.Value.AddDays(1.0).ToShortDateString() + "' " + str + "  order By PersonId,arrivetime desc";
            DataSet set = access.Run_SqlText(queryStr);
            this.dgv_State.DataSource = set.Tables[0];
            this.lab_StateInfo.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录";
        }

        private void btn_ComQuery_Click(object sender, EventArgs e)
        {
            string str = null;
            if (this.cbx_PlanDepart.Text != "")
            {
                str = " Code='" + this.cbx_PlanDepart.SelectedValue.ToString().Trim() + "'";
            }
            if (this.cbx_PlanType.Text != "")
            {
                str = str + " And PlanType='" + this.cbx_PlanType.SelectedValue.ToString().Trim() + "'";
            }
            string text = this.cbx_TimeType.Text;
            if (text != null)
            {
                if (!(text == "计划下达时间"))
                {
                    if (text == "录入时间")
                    {
                        string str4 = str;
                        str = str4 + " And InPutTime between '" + this.dtp_From.Value.ToShortDateString() + "' and '" + this.dtp_To.Value.AddDays(1.0).ToShortDateString() + "'";
                    }
                    else if (text == "审核时间")
                    {
                        string str5 = str;
                        str = str5 + " And AuditingTime between '" + this.dtp_From.Value.ToShortDateString() + "' and '" + this.dtp_To.Value.AddDays(1.0).ToShortDateString() + "'";
                    }
                    else if (text == "最后完成期限")
                    {
                        string str6 = str;
                        str = str6 + " And DeadLine between '" + this.dtp_From.Value.ToShortDateString() + "' and '" + this.dtp_To.Value.AddDays(1.0).ToShortDateString() + "'";
                    }
                }
                else
                {
                    string str3 = str;
                    str = str3 + " And PlanTime between '" + this.dtp_From.Value.ToShortDateString() + "' and '" + this.dtp_To.Value.AddDays(1.0).ToShortDateString() + "'";
                }
            }
            if (this.cbx_PlanState.Text == "审核通过")
            {
                str = str + " And IsPass='1'";
            }
            else if (this.cbx_PlanState.Text == "未审核通过")
            {
                str = str + " And IsPass='0'";
            }
            if (this.cbx_ChangeList.Text == "已转单")
            {
                str = str + " And PlanId1 is not null";
            }
            else if (this.cbx_ChangeList.Text == "未转单")
            {
                str = str + " And PlanId1 is null";
            }
            if (this.cbx_relation.Text.Trim() != "")
            {
                str = str + " And PlanNum" + this.cbx_relation.Text.Trim() + "TotalCount";
            }
            Sql2KDataAccess access = new Sql2KDataAccess();
            if (str.StartsWith(" And"))
            {
                int length = str.Length - 4;
                str = str.Substring(4, length);
            }
            DataSet set = access.Run_SqlText(this.PlanResult + " Where " + str + " order By PlanTime desc");
            this.dgv_Plan.DataSource = set.Tables[0];
            this.lab_Total.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录";
        }

        private void btn_ComQuery_Task_Click(object sender, EventArgs e)
        {
            this.splitDGD.Panel2Collapsed = true;
            DataSet set = new Sql2KDataAccess().Run_SqlText(this.TaskResult + " Where TaskTime between '" + this.dtp_From_Task.Value.ToShortDateString() + "' and '" + this.dtp_To_Task.Value.AddDays(1.0).ToShortDateString() + "' order by TaskTime desc");
            this.dgv_Task.DataSource = set.Tables[0];
            this.lab_Total_Task.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录";
        }

        private void btn_MbTaskId_Click(object sender, EventArgs e)
        {
            if (this.tbx_MbTaskId.Text.Trim() == "")
            {
                MessageBox.Show("请输入要查询底板任务单单号", "底板任务查询", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tbx_MbTaskId.Focus();
            }
            else
            {
                DataSet set = new Sql2KDataAccess().Run_SqlText(this.MbTaskResult + "  Where MbTaskId='" + this.tbx_MbTaskId.Text.Trim() + "'");
                this.dgv_Mboard.DataSource = set.Tables[0];
                this.lab_Total_Mb.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录";
            }
        }

        private void btn_MbTime_Click(object sender, EventArgs e)
        {
            DataSet set = new Sql2KDataAccess().Run_SqlText(this.MbTaskResult + "  Where MbTaskTime between '" + this.dtp_MbFrom.Value.ToShortDateString() + "' and '" + this.dtp_MbTo.Value.AddDays(1.0).ToShortDateString() + "' order by MbTaskTime desc");
            this.dgv_Mboard.DataSource = set.Tables[0];
            this.lab_Total_Mb.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录";
        }

        private void btn_OutTime_Click(object sender, EventArgs e)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            string str = "";
            if (this.cbx_IsReceive.Text == "已签收")
            {
                str = " and ReceivePerson is not null";
            }
            else if (this.cbx_IsReceive.Text == "未签收")
            {
                str = " and ReceivePerson is null";
            }
            DataSet set = access.Run_SqlText(this.SendResult + " Where SendTime between '" + this.dtp_From_Send.Value.ToShortDateString() + "' and '" + this.dtp_To_Send.Value.AddDays(1.0).ToShortDateString() + "' " + str + " order by sendTime desc");
            this.dgv_Send.DataSource = set.Tables[0];
            this.lab_Total_Send.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录";
        }

        private void btn_PlanId_Click(object sender, EventArgs e)
        {
            if (this.tbx_PlanId.Text.Trim() == "")
            {
                MessageBox.Show("请输入要查询的计划单号", "车牌查询", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataSet set = new Sql2KDataAccess().Run_SqlText(this.NpResult + " where PlanId='" + this.tbx_PlanId.Text.Trim() + "'");
                this.drw_NpResult.DataSource = set.Tables[0];
                this.labTotal.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录";
            }
        }

        private void btn_PlanId_Task_Click(object sender, EventArgs e)
        {
            if (this.tbx_PlanId_Task.Text.Trim() == "")
            {
                MessageBox.Show("请输入要查询的计划单号", "送货单查询", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tbx_PlanId_Task.Focus();
            }
            else
            {
                DataSet set = new Sql2KDataAccess().Run_SqlText(this.SendResult + " Where PlanId='" + this.tbx_PlanId_Task.Text.Trim() + "' order by sendtime desc");
                this.dgv_Send.DataSource = set.Tables[0];
                this.lab_Total_Send.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录";
            }
        }

        private void btn_PlanIdQuery_Click(object sender, EventArgs e)
        {
            if (this.txt_PlanId.Text.Trim() == "")
            {
                MessageBox.Show("请输入要查询的计划单号", "计划单查询", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txt_PlanId.Focus();
            }
            else
            {
                Sql2KDataAccess access = new Sql2KDataAccess();
                DataSet set = new DataSet();
                if (!this.cbx_Big.Checked)
                {
                    set = access.Run_SqlText(this.PlanResult + " where PlanId='" + this.txt_PlanId.Text.Trim() + "' Order By PlanTime desc");
                }
                else
                {
                    set = access.Run_SqlText(this.PlanResult + " where PlanId like '" + this.txt_PlanId.Text.Trim() + "%' Order By PlanTime desc");
                }
                this.dgv_Plan.DataSource = set.Tables[0];
                this.lab_Total.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录";
            }
        }

        private void btn_PrintCode_Click(object sender, EventArgs e)
        {
            int boxCapability = new MbTask().GetBoxCapability();
            try
            {
                if (this.dgv_Mboard.SelectedCells[2].Value.ToString().Trim() != "清洗烘干")
                {
                    MessageBox.Show("只有清洗烘干工序可以打印条形码", "底板任务查询", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    PanBarCode[] codeArray;
                    if ((int.Parse(this.dgv_Mboard.SelectedCells[3].Value.ToString()) % boxCapability) != 0)
                    {
                        int num1 = int.Parse(this.dgv_Mboard.SelectedCells[3].Value.ToString()) / boxCapability;
                    }
                    else
                    {
                        int num3 = int.Parse(this.dgv_Mboard.SelectedCells[3].Value.ToString()) / boxCapability;
                    }
                    string queryStr = "select * from t_pantaskinfo where left(pantaskid,12)='" + this.dgv_Mboard.SelectedCells[0].Value.ToString() + "'";
                    DataSet set = new Sql2KDataAccess().Run_SqlText(queryStr);
                    if ((set != null) && (set.Tables[0].Rows.Count > 0))
                    {
                        codeArray = new PanBarCode[set.Tables[0].Rows.Count];
                        for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                        {
                            codeArray[i].strCount = set.Tables[0].Rows[i]["pancount"].ToString();
                            codeArray[i].strTaskID = set.Tables[0].Rows[i]["pantaskid"].ToString();
                            codeArray[i].strPanType = this.dgv_Mboard.SelectedCells[1].Value.ToString();
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

        private void btn_PrintStateResult_Click(object sender, EventArgs e)
        {
            Frm_NpStat stat = new Frm_NpStat {
                FromDate = this.dtp_FromState.Value,
                ToDate = this.dtp_ToState.Value,
                NpState = this.cbx_NpState.Text.Trim()
            };
            if (this.cbx_PreviewPrintState.Checked)
            {
                stat.Show();
            }
            else
            {
                stat.PrintSendDetail();
            }
        }

        private void btn_Query_Task_Click(object sender, EventArgs e)
        {
            this.splitDGD.Panel2Collapsed = true;
            if (this.txt_TaskId.Text.Trim() == "")
            {
                MessageBox.Show("请输入要查询的生产任务单号", "生产任务单查询", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txt_TaskId.Focus();
            }
            else
            {
                DataSet set = new Sql2KDataAccess().Run_SqlText(this.TaskResult + " Where TaskId='" + this.txt_TaskId.Text.Trim() + "' order by TaskTime desc");
                this.dgv_Task.DataSource = set.Tables[0];
                this.lab_Total_Task.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录";
            }
        }

        private void btn_QueryNpNo_Click(object sender, EventArgs e)
        {
            if (this.tbx_NpNo.Text.Trim() == "")
            {
                MessageBox.Show("请输入要查询的车牌号码", "车牌号查询", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (Encoding.Default.GetBytes(this.tbx_NpNo.Text.Substring(0, 1)).Length == 1)
                {
                    this.tbx_NpNo.Text = "鲁" + this.tbx_NpNo.Text;
                }
                DataSet set = new Sql2KDataAccess().Run_SqlText(this.NpResult + " where NpNo='" + this.tbx_NpNo.Text.Trim() + "'");
                this.drw_NpResult.DataSource = set.Tables[0];
                this.labTotal.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录";
            }
        }

        private void btn_RePrintSend_Click(object sender, EventArgs e)
        {
            if ((this.dgv_Send.SelectedCells.Count > 0) && (this.dgv_Send.SelectedCells[0].ColumnIndex == 0))
            {
                Sql2KDataAccess access = new Sql2KDataAccess();
                string queryStr = "select * from V_SendList Where SendId='" + this.dgv_Send.SelectedCells[0].Value.ToString().Trim() + "'";
                this.SendListds = access.Run_SqlText(queryStr);
                if ((this.SendListds != null) && (this.SendListds.Tables[0].Rows.Count != 0))
                {
                    queryStr = "select t_np.npno,v_nptype.codename,taskid from T_NP,v_nptype Where t_np.nptype=v_nptype.code and  SendId='" + this.dgv_Send.SelectedCells[0].Value.ToString().Trim() + "' order by taskid";
                    this.SendListDetailsds = access.Run_SqlText(queryStr);
                    if ((this.SendListDetailsds != null) && (this.SendListDetailsds.Tables[0].Rows.Count != 0))
                    {
                        PrintDocument document = new PrintDocument();
                        PrintPreviewDialog dialog = new PrintPreviewDialog();
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
                            MessageBox.Show("打印时发生错误" + '\n' + exception.Message, "打印送货单明细", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        FrmPrintSendList list = new FrmPrintSendList {
                            IsPreview = this.cbx_PrintSendView.Checked,
                            labelMakeperson = { Text = "制表人 " + this.SendListds.Tables[0].Rows[0]["truename"].ToString() },
                            labelMakeTime = { Text = "制表时间 " + this.SendListds.Tables[0].Rows[0]["sendtime"].ToString() },
                            textPlanID = { Text = this.SendListds.Tables[0].Rows[0]["planid"].ToString() },
                            combPlanDepart = { Text = this.SendListds.Tables[0].Rows[0]["plandepart"].ToString() },
                            textSendID = { Text = this.SendListds.Tables[0].Rows[0]["sendid"].ToString() },
                            textSendPerson = { Text = "" },
                            textRemark = { Text = "" }
                        };
                        queryStr = "select * from V_List Where SendId='" + this.dgv_Send.SelectedCells[0].Value.ToString().Trim() + "'";
                        DataSet set = access.Run_SqlText(queryStr);
                        if ((set != null) && (set.Tables[0].Rows.Count != 0))
                        {
                            int num = 0;
                            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                            {
                                list.GridVwSendInfo.Rows.Add();
                                list.GridVwSendInfo.Rows[list.GridVwSendInfo.NewRowIndex - 1].Cells[0].Value = Convert.ToString((int) (i + 1));
                                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell) list.GridVwSendInfo.Rows[list.GridVwSendInfo.NewRowIndex - 1].Cells[1];
                                cell.Value = set.Tables[0].Rows[i]["codename"].ToString();
                                list.GridVwSendInfo.Rows[list.GridVwSendInfo.NewRowIndex - 1].Cells[2].Value = set.Tables[0].Rows[i]["totalnum"].ToString();
                                num += int.Parse(set.Tables[0].Rows[i]["totalnum"].ToString());
                            }
                            list.labelTotal.Text = "总计" + num.ToString();
                            list.ShowDialog();
                        }
                    }
                }
            }
        }

        private void btn_RePrintTask_Click(object sender, EventArgs e)
        {
            if ((this.dgv_Task.SelectedCells.Count > 0) && (this.dgv_Task.SelectedCells[0].ColumnIndex == 0))
            {
                DataSet npByTaskId = new Task().GetNpByTaskId(this.dgv_Task.SelectedCells[0].Value.ToString());
                TaskList list = new TaskList {
                    TaskId = this.dgv_Task.SelectedCells[0].Value.ToString().Trim(),
                    TaskUser = this.dgv_Task.Rows[this.dgv_Task.SelectedCells[0].RowIndex].Cells[2].Value.ToString().Trim(),
                    TaskTime = this.dgv_Task.Rows[this.dgv_Task.SelectedCells[0].RowIndex].Cells[1].Value.ToString().Trim(),
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
                    new Print().LabelPrintTaskNP(this.dgv_Task.SelectedCells[0].Value.ToString().Trim(), nPNum);
                }
            }
        }

        private void btn_SendId_Click(object sender, EventArgs e)
        {
            if (this.tbx_SendId.Text.Trim() == "")
            {
                MessageBox.Show("请输入要查询的送货单号号", "车牌查询", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tbx_SendId.Focus();
            }
            else
            {
                DataSet set = new Sql2KDataAccess().Run_SqlText(this.NpResult + " where SendId='" + this.tbx_SendId.Text.Trim() + "'");
                this.drw_NpResult.DataSource = set.Tables[0];
                this.labTotal.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录";
            }
        }

        private void btn_SendId_Send_Click(object sender, EventArgs e)
        {
            if (this.tbx_SendId_Send.Text.Trim() == "")
            {
                MessageBox.Show("请输入要查询的送货单号", "送货单查询", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tbx_SendId_Send.Focus();
            }
            else
            {
                DataSet set = new Sql2KDataAccess().Run_SqlText(this.SendResult + " Where SendId ='" + this.tbx_SendId_Send.Text.Trim() + "' order by sendTime desc");
                this.dgv_Send.DataSource = set.Tables[0];
                this.lab_Total_Send.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录";
            }
        }

        private void btn_State_Click(object sender, EventArgs e)
        {
            string str = "";
            long num = 0L;
            this.splitContainer1.Panel2Collapsed = true;
            string str2 = this.cbx_NpState.Text.Trim();
            if (str2 != null)
            {
                if (!(str2 == "未制作"))
                {
                    if (str2 == "制作中")
                    {
                        str = " and TaskId is not null and SendId is null";
                    }
                    else if (str2 == "制作完毕")
                    {
                        str = " and SendId is not null";
                    }
                }
                else
                {
                    str = " and TaskId is null";
                }
            }
            DataSet set = new Sql2KDataAccess().Run_SqlText("select Description as '车牌类型',TotalNum as '总数' from V_NpType t inner join (select Code,Count(NpId) as TotalNum from V_Np Where InPutTime between '" + this.dtp_FromState.Value.ToShortDateString() + "' and '" + this.dtp_ToState.Value.AddDays(1.0).ToShortDateString() + "'" + str + " Group By Code) t1 on t.Code=t1.Code");
            this.dgv_State.DataSource = set.Tables[0];
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                num += long.Parse(set.Tables[0].Rows[i]["总数"].ToString());
            }
            this.lab_StateInfo.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录 车牌总数:" + num.ToString();
        }

        private void btn_TaskId_Click(object sender, EventArgs e)
        {
            if (this.tbx_TaskId.Text.Trim() == "")
            {
                MessageBox.Show("请输入要查询的任务单号", "车牌查询", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.tbx_TaskId.Focus();
            }
            else
            {
                Sql2KDataAccess access = new Sql2KDataAccess();
                string queryStr = this.NpResult + " where TaskId='" + this.tbx_TaskId.Text.Trim() + "'";
                DataSet set = access.Run_SqlText(queryStr);
                this.drw_NpResult.DataSource = set.Tables[0];
                this.labTotal.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录";
            }
        }

        private void btn_Works_Click(object sender, EventArgs e)
        {
            string str = "";
            double num = 0.0;
            this.splitContainer1.Panel2Collapsed = true;
            if (this.cbx_Workers.Text.Trim() != "")
            {
                str = " and PersonId='" + this.cbx_Workers.SelectedValue.ToString().Trim() + "'";
            }
            Sql2KDataAccess access = new Sql2KDataAccess();
            string queryStr = "select truename as '工作人员', Wprocess as '工序',MaterialKind as '材料类型',Type as '类型/尺寸',WhichPiece as '前片/后片',sum(WorkLoad) as '工作量' from V_Works  Where begintime between '" + this.dtp_FromWorks.Value.ToShortDateString() + "' and '" + this.dtp_ToWorks.Value.AddDays(1.0).ToShortDateString() + "' " + str + " group By truename ,PersonId,Wprocess,MaterialKind,Type,WhichPiece order By PersonId";
            DataSet set = access.Run_SqlText(queryStr);
            this.dgv_State.DataSource = set.Tables[0];
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                try
                {
                    num += double.Parse(set.Tables[0].Rows[i]["工作量"].ToString());
                }
                catch
                {
                }
            }
            this.lab_StateInfo.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录 工作量总数:" + num.ToString();
        }

        private void btnAbsent_Click(object sender, EventArgs e)
        {
            string queryStr = "SELECT TaskID AS 箱号, ErrCount AS 数量,  v_processall.codename AS 工序,  DestoryorMis AS 缺损类型, RecordTime AS 登记时间, Remark AS 备注 FROM T_ProcessErr,v_processall where FindProcess=v_processall.code and  recordtime between '" + this.dtp_absentFrom.Value.ToShortDateString() + "' and '" + this.dtp_absentTo.Value.AddDays(1.0).ToShortDateString() + "'";
            DataSet set = this.MyDB.Run_SqlText(queryStr);
            if (set != null)
            {
                this.dataGridView1.DataSource = set.Tables[0];
                this.labelResultInfo.Text = "共有" + set.Tables[0].Rows.Count + "条缺损信息";
            }
        }

        private void btnExp2Excel_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (this.treeVwAccount.Nodes.Count > 0)
            {
                if (this.treeVwAccount.Nodes[0].Text.Substring(0, 3).CompareTo("Pan") == 0)
                {
                    this.ExpTreeVwtoExcel("机动车号牌生产库存统计表", this.treeVwAccount);
                }
                else
                {
                    this.ExpTreeVwtoExcel("机动车号牌仓库操作统计表", this.treeVwAccount);
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void btnExpDetails2Excel_Click(object sender, EventArgs e)
        {
            this.ExpGridtoExcel("仓库信息明细表", this.dataGridView1, DateTime.Now, DateTime.Now);
        }

        private void btnExpExcel_Click(object sender, EventArgs e)
        {
            this.ExpGridtoExcel("机动车号牌生产工作量统计表", this.dgv_State, this.dtp_FromState.Value, this.dtp_ToState.Value);
        }

        private void btnInHouse_Click(object sender, EventArgs e)
        {
            new FrmManualInHouse().ShowDialog();
        }

        private void btnPanAccount_Click(object sender, EventArgs e)
        {
            this.treeVwAccount.Nodes.Clear();
            this.Cursor = Cursors.WaitCursor;
            this.AccountPan();
            this.AccountNP();
            this.Cursor = Cursors.Default;
        }

        private void btnPanProcess_Click(object sender, EventArgs e)
        {
            string queryStr = this.MbTaskProcessQueryHead + " WHERE (T_WorkInfo.TaskID ='" + this.tbx_MbTaskId.Text.Trim() + "') ORDER BY T_WorkInfo.TaskID, T_WorkInfo.ProcessID";
            DataSet set = new Sql2KDataAccess().Run_SqlText(queryStr);
            this.dgv_Mboard.DataSource = set.Tables[0];
            this.lab_Total_Mb.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录";
        }

        private void btnProcessErr_Click(object sender, EventArgs e)
        {
            new FrmProcessErr().ShowDialog();
        }

        private void btnRemakeAppr_Click(object sender, EventArgs e)
        {
            Frm_Appr.Instance.MdiParent = base.ParentForm;
            Frm_Appr.G_IsRemarkAppr = true;
            Frm_Appr.Instance.Show();
            Frm_Appr.Instance.Focus();
        }

        private void btnTaskPan_Click(object sender, EventArgs e)
        {
            if (this.txt_TaskId.Text.Trim() == "")
            {
                MessageBox.Show("请输入要查询的生产任务单号", "生产任务单查询", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txt_TaskId.Focus();
            }
            else
            {
                Sql2KDataAccess access = new Sql2KDataAccess();
                DataSet set = new DataSet();
                string queryStr = this.WithdrawQueryHead + " WHERE (T_Task.TaskID = '" + this.txt_TaskId.Text.Trim() + "')";
                set = access.Run_SqlText(queryStr);
                this.dgv_Task.DataSource = set.Tables[0];
                this.lab_Total_Task.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录";
                if ((set != null) && (set.Tables[0].Rows.Count > 0))
                {
                    queryStr = this.MbTaskProcessQueryHead + " WHERE (T_WorkInfo.TaskID IN (SELECT T_WithdrawPan.PanTaskID FROM T_WithdrawPan INNER JOIN T_Task ON T_WithdrawPan.TaskID = T_Task.TaskID INNER JOIN V_MbType ON T_WithdrawPan.PanType = V_MbType.Code INNER JOIN T_User u1 ON T_WithdrawPan.WithdrawPerson = u1.UserName INNER JOIN T_User u2 ON T_WithdrawPan.OperatePerson = u2.UserName WHERE (T_Task.TaskID = '" + this.txt_TaskId.Text.Trim() + "'))) ORDER BY T_WorkInfo.TaskID, T_WorkInfo.ProcessID";
                    DataSet set2 = access.Run_SqlText(queryStr);
                    this.dgv_TaskDetail.DataSource = set2.Tables[0];
                    this.splitDGD.Panel2Collapsed = false;
                }
            }
        }

        private void btnTaskProcess_Click(object sender, EventArgs e)
        {
            this.splitDGD.Panel2Collapsed = true;
            if (this.txt_TaskId.Text.Trim() == "")
            {
                MessageBox.Show("请输入要查询的生产任务单号", "生产任务单查询", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txt_TaskId.Focus();
            }
            else
            {
                Sql2KDataAccess access = new Sql2KDataAccess();
                DataSet set = new DataSet();
                string queryStr = this.TaskProcessQueryHead + " WHERE (T_Task.TaskID = '" + this.txt_TaskId.Text.Trim() + "')";
                set = access.Run_SqlText(queryStr);
                this.dgv_Task.DataSource = set.Tables[0];
                this.lab_Total_Task.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录";
            }
        }

        private void btnTaskSend_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2Collapsed = false;
            string queryStr = "SELECT PlanID as 计划单号, SendID as 送货单号, Description as 车牌类型, COUNT(*) AS 数量 FROM V_NpQuery WHERE (PlanTime BETWEEN '" + this.dtp_FromPlan.Value.ToString("yyyy-MM-dd") + "' AND '" + this.dtp_ToPlan.Value.ToString("yyyy-MM-dd") + "')   GROUP BY PlanID, SendID, Description  ORDER BY PlanID";
            DataSet set = this.MyDB.Run_SqlText(queryStr);
            if (set != null)
            {
                this.dgv_State.DataSource = set.Tables[0];
            }
            this.lab_StateInfo.Text = "共查询到 " + set.Tables[0].Rows.Count.ToString() + " 条符合条件的记录";
            queryStr = "SELECT V_NpType.Description AS 车牌类型, COUNT(*) AS 计划数 FROM T_NP INNER JOIN V_NpType ON T_NP.NPType = V_NpType.Code INNER JOIN T_Plan ON T_NP.PlanID = T_Plan.PlanID and (PlanTime BETWEEN '" + this.dtp_FromPlan.Value.ToString("yyyy-MM-dd") + "' AND '" + this.dtp_ToPlan.Value.ToString("yyyy-MM-dd") + "') GROUP BY V_NpType.Description ";
            DataSet set2 = this.MyDB.Run_SqlText(queryStr);
            queryStr = "SELECT V_NpType.Description AS 车牌类型, COUNT(*) AS 送货数 FROM T_NP INNER JOIN V_NpType ON T_NP.NPType = V_NpType.Code INNER JOIN T_Send ON T_NP.SendID = T_Send.SendID and (SendTime BETWEEN '" + this.dtp_FromSend.Value.ToString("yyyy-MM-dd") + "' AND '" + this.dtp_ToSend.Value.ToString("yyyy-MM-dd") + "') GROUP BY V_NpType.Description ";
            DataSet set3 = this.MyDB.Run_SqlText(queryStr);
            if ((set2 != null) && (set3 != null))
            {
                queryStr = "SELECT Description, Code = '0', Codename = '0' FROM V_NpType UNION SELECT TOP 1 Description = '合计', Code = '1', Codename = '0' FROM V_NpType order by code";
                set = this.MyDB.Run_SqlText(queryStr);
                this.dgv_StatbyNPType.DataSource = set.Tables[0];
                this.dgv_StatbyNPType.Columns[0].HeaderText = "车牌类型";
                this.dgv_StatbyNPType.Columns[1].HeaderText = "计划数";
                this.dgv_StatbyNPType.Columns[2].HeaderText = "送货数";
                long num = 0L;
                long num2 = 0L;
                for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < set2.Tables[0].Rows.Count; j++)
                    {
                        if (set2.Tables[0].Rows[j]["车牌类型"].ToString().CompareTo(set.Tables[0].Rows[i]["description"].ToString()) == 0)
                        {
                            this.dgv_StatbyNPType.Rows[i].Cells["code"].Value = set2.Tables[0].Rows[j]["计划数"].ToString();
                            num += int.Parse(set2.Tables[0].Rows[j]["计划数"].ToString());
                            break;
                        }
                    }
                    for (int k = 0; k < set3.Tables[0].Rows.Count; k++)
                    {
                        if (set3.Tables[0].Rows[k]["车牌类型"].ToString().CompareTo(set.Tables[0].Rows[i]["description"].ToString()) == 0)
                        {
                            this.dgv_StatbyNPType.Rows[i].Cells["codename"].Value = set3.Tables[0].Rows[k]["送货数"].ToString();
                            num2 += int.Parse(set3.Tables[0].Rows[k]["送货数"].ToString());
                            break;
                        }
                    }
                }
                this.dgv_StatbyNPType.Rows[this.dgv_StatbyNPType.RowCount - 1].Cells["description"].Value = "合计";
                this.dgv_StatbyNPType.Rows[this.dgv_StatbyNPType.RowCount - 1].Cells["code"].Value = num.ToString();
                this.dgv_StatbyNPType.Rows[this.dgv_StatbyNPType.RowCount - 1].Cells["codename"].Value = num2.ToString();
            }
        }

        private void btnWarehouseOperate_Click(object sender, EventArgs e)
        {
            this.treeVwAccount.Nodes.Clear();
            this.AccountWithdrawToday();
            this.AccountPanWareOper();
            this.AccountNPWareOper();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ExpTreeVwtoExcel("机动车号牌生产库存统计表", this.treeVwAccount);
        }

        private void cbx_ReceiveMoney_CheckedChanged(object sender, EventArgs e)
        {
            if (this.dgv_Plan.RowCount > 0)
            {
                this.dgv_Plan.Columns[13].SortMode = this.cbx_ReceiveMoney.Checked ? DataGridViewColumnSortMode.NotSortable : DataGridViewColumnSortMode.Automatic;
            }
        }

        private void cbx_SignIn_CheckedChanged(object sender, EventArgs e)
        {
            this.dgv_Send.Columns[5].SortMode = this.cbx_SignIn.Checked ? DataGridViewColumnSortMode.NotSortable : DataGridViewColumnSortMode.Automatic;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
        }

        private void dgv_Plan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((this.cbx_ReceiveMoney.Checked && (e.ColumnIndex == 13)) && (e.RowIndex >= 0))
            {
                if (this.dgv_Plan.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
                {
                    this.dgv_Plan.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "√";
                    if (!Plan.UpdateReceiveMonyState(this.dgv_Plan.Rows[e.RowIndex].Cells[0].Value.ToString(), true))
                    {
                        MessageBox.Show("标记收款失败", "收款标记", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    this.dgv_Plan.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    if (!Plan.UpdateReceiveMonyState(this.dgv_Plan.Rows[e.RowIndex].Cells[0].Value.ToString(), false))
                    {
                        MessageBox.Show("标记收款失败", "收款标记", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void dgv_Plan_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgv_Plan.Cursor = Cursors.WaitCursor;
            try
            {
                if ((e.ColumnIndex == 0) && (this.dgv_Plan.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != ""))
                {
                    this.tbx_PlanId.Text = this.dgv_Plan.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    this.btn_PlanId_Click(this, e);
                    this.tabControl.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                this.dgv_Plan.Cursor = Cursors.Default;
            }
        }

        private void dgv_Send_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgv_Send.Cursor = Cursors.WaitCursor;
            try
            {
                if ((e.ColumnIndex == 0) && (this.dgv_Send.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != ""))
                {
                    this.tbx_SendId.Text = this.dgv_Send.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    this.btn_SendId_Click(this, e);
                    this.tabControl.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                this.dgv_Send.Cursor = Cursors.Default;
            }
        }

        private void dgv_Send_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 5) && this.cbx_SignIn.Checked)
            {
                this.dgv_Send.BeginEdit(false);
            }
        }

        private void dgv_Send_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.cbx_SignIn.Checked && (this.dgv_Send.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != ""))
            {
                this.dgv_Send.Cursor = Cursors.WaitCursor;
                Invoice invoice = new Invoice();
                if (!invoice.SignInSendList(this.dgv_Send.Rows[e.RowIndex].Cells[0].Value.ToString(), this.dgv_Send.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
                {
                    MessageBox.Show("回写签收人失败", "回写签收人", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                this.dgv_Send.Cursor = Cursors.Default;
            }
        }

        private void dgv_Task_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgv_Task.Cursor = Cursors.WaitCursor;
            try
            {
                if ((e.ColumnIndex == 0) && (this.dgv_Task.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != ""))
                {
                    this.tbx_TaskId.Text = this.dgv_Task.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim();
                    this.btn_TaskId_Click(this, e);
                    this.tabControl.SelectedIndex = 0;
                }
                if (this.dgv_Task.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Substring(0, 1).CompareTo("M") == 0)
                {
                    string queryStr = "SELECT t_user.TrueName AS 操作人员, V_ProcessAll.CodeName AS 工序,  T_WorkInfo.TaskID AS 任务单号, T_WorkInfo.BeginTime AS 开始时间,  T_WorkInfo.EndTime AS 完成时间 FROM T_WorkInfo INNER JOIN V_ProcessAll ON T_WorkInfo.ProcessID = V_ProcessAll.Code INNER JOIN T_User t_user ON T_WorkInfo.PersonID = t_user.UserName WHERE (T_WorkInfo.TaskID ='" + this.dgv_Task.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() + "') ORDER BY T_WorkInfo.TaskID, T_WorkInfo.ProcessID";
                    DataSet set = new Sql2KDataAccess().Run_SqlText(queryStr);
                    this.dgv_TaskDetail.DataSource = set.Tables[0];
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                this.dgv_Task.Cursor = Cursors.Default;
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

        private void drw_NpResult_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.drw_NpResult.Cursor = Cursors.WaitCursor;
            try
            {
                switch (e.ColumnIndex)
                {
                    case 3:
                        if (this.drw_NpResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
                        {
                            this.txt_PlanId.Text = this.drw_NpResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            this.btn_PlanIdQuery_Click(this, e);
                            this.tabControl.SelectedIndex = 1;
                        }
                        return;

                    case 4:
                        if (this.drw_NpResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
                        {
                            this.txt_TaskId.Text = this.drw_NpResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            this.btn_Query_Task_Click(this, e);
                            this.tabControl.SelectedIndex = 2;
                        }
                        return;

                    case 5:
                        if (this.drw_NpResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
                        {
                            this.tbx_SendId_Send.Text = this.drw_NpResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            this.btn_SendId_Send_Click(this, e);
                            this.tabControl.SelectedIndex = 3;
                        }
                        return;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                this.drw_NpResult.Cursor = Cursors.Default;
            }
        }

        private void ExpGridtoExcel(string Head, DataGridView gdv, DateTime BeginTime, DateTime EndTime)
        {
            if (gdv.RowCount != 0)
            {
                Excel.Application application = new Excel.ApplicationClass();
                Excel._Worksheet activeSheet = (Excel._Worksheet)application.Application.Workbooks.Add(true).ActiveSheet;
                activeSheet.Name = Head.Replace("\r\n", "") + DateTime.Now.ToString("yy-MM-dd");
                application.Visible = true;
                activeSheet.get_Range(application.Cells[1, 1], application.Cells[1, gdv.ColumnCount]).Font.Bold = true;
                activeSheet.get_Range(application.Cells[1, 1], application.Cells[1, gdv.ColumnCount]).Font.Size = 0x10;
                activeSheet.get_Range(application.Cells[1, 1], application.Cells[1, gdv.ColumnCount]).HorizontalAlignment =Excel.XlVAlign.xlVAlignCenter;
                activeSheet.get_Range(application.Cells[1, 1], application.Cells[1, gdv.ColumnCount]).Merge(true);
                application.Cells[1, 1] = Head;
                if (BeginTime.ToString().Length > 0)
                {
                    application.Cells[2, 1] = "从" + BeginTime.ToString() + "到" + EndTime.ToString();
                }
                application.Cells[3, 1] = " 报表时间 " + DateTime.Now;
                activeSheet.get_Range(application.Cells[4, 1], application.Cells[4, gdv.ColumnCount]).Interior.ColorIndex = 0x13;
                for (int i = 0; i < gdv.ColumnCount; i++)
                {
                    application.Cells[4, i + 1] = gdv.Columns[i].Name.ToString();
                }
                for (int j = 0; j < gdv.RowCount; j++)
                {
                    for (int k = 0; k < gdv.ColumnCount; k++)
                    {
                        if (gdv[k, j].Value.ToString().Length >= 0)
                        {
                            application.Cells[j + 5, k + 1] = gdv[k, j].Value.ToString();
                        }
                        else
                        {
                            application.Cells[j + 5, k + 1] = "0";
                        }
                    }
                }
            }
        }

        public void ExpTreeVwtoExcel(string Head, TreeView tvw)
        {
            if (tvw.Nodes.Count != 0)
            {
                Excel.Application application = new Excel.ApplicationClass();
                Excel._Worksheet activeSheet = (Excel._Worksheet)application.Application.Workbooks.Add(true).ActiveSheet;
                activeSheet.Name = Head.Replace("\r\n", "") + DateTime.Now.ToString("yy-MM-dd");
                application.Visible = true;
                TreeNode temNode = tvw.Nodes[0];
                activeSheet.get_Range(application.Cells[1, 1], application.Cells[1, 5]).Font.Bold = true;
                activeSheet.get_Range(application.Cells[1, 1], application.Cells[1, 5]).Font.Size = 0x10;
                activeSheet.get_Range(application.Cells[1, 1], application.Cells[1, 5]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
                activeSheet.get_Range(application.Cells[1, 1], application.Cells[1, 5]).Merge(true);
                application.Cells[1, 1] = Head;
                application.Cells[3, 1] = " 报表时间 " + DateTime.Now;
                activeSheet.get_Range(application.Cells[4, 1], application.Cells[4, 5]).Interior.ColorIndex = 0x13;
                int row = 5;
                this.ReadNodetoSheet(temNode, activeSheet, ref row);
            }
        }

        private void Frm_Query_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_Instance = null;
        }

        private void Frm_Query_Load(object sender, EventArgs e)
        {
            base.Size = base.Parent.ClientSize;
            if (!User.IsHaveAuthority("车牌查询"))
            {
                this.tabControl.TabPages.Remove(this.tabPage1);
            }
            if (!User.IsHaveAuthority("计划单查询"))
            {
                this.tabControl.TabPages.Remove(this.tabPage2);
            }
            if (!User.IsHaveAuthority("生产任务单查询"))
            {
                this.tabControl.TabPages.Remove(this.tabPage3);
            }
            if (!User.IsHaveAuthority("送货单查询"))
            {
                this.tabControl.TabPages.Remove(this.tabPage4);
            }
            if (!User.IsHaveAuthority("底板查询"))
            {
                this.tabControl.TabPages.Remove(this.tabPage6);
            }
            if (!User.IsHaveAuthority("统计打印"))
            {
                this.tabControl.TabPages.Remove(this.tabPage5);
            }
            if (!User.IsHaveAuthority("库存统计"))
            {
                this.tabControl.TabPages.Remove(this.tabPage7);
            }
            if (!User.IsHaveAuthority("手工入库"))
            {
                this.btnInHouse.Enabled = false;
            }
            if (!User.IsHaveAuthority("补做审批"))
            {
                this.btnRemakeAppr.Enabled = false;
            }
            Sql2KDataAccess access = new Sql2KDataAccess();
            DataSet set = access.Run_SqlText("select * from V_Depart");
            DataRow row = set.Tables[0].NewRow();
            set.Tables[0].Rows.InsertAt(row, 0);
            this.cbx_PlanDepart.DataSource = set.Tables[0];
            this.cbx_PlanDepart.DisplayMember = "CodeName";
            this.cbx_PlanDepart.ValueMember = "Code";
            set = access.Run_SqlText("select * from V_PlanKind order by Code Desc");
            DataRow row2 = set.Tables[0].NewRow();
            set.Tables[0].Rows.InsertAt(row2, 0);
            this.cbx_PlanType.DataSource = set.Tables[0];
            this.cbx_PlanType.DisplayMember = "CodeName";
            this.cbx_PlanType.ValueMember = "Code";
            set = access.Run_SqlText("select UserName,TrueName from T_User ");
            DataRow row3 = set.Tables[0].NewRow();
            set.Tables[0].Rows.InsertAt(row3, 0);
            this.cbx_Workers.DataSource = set.Tables[0];
            this.cbx_Workers.DisplayMember = "TrueName";
            this.cbx_Workers.ValueMember = "UserName";
            set = access.Run_SqlText("select UserName,TrueName from T_User ");
            DataRow row4 = set.Tables[0].NewRow();
            set.Tables[0].Rows.InsertAt(row4, 0);
            this.cbx_Person.DataSource = set.Tables[0];
            this.cbx_Person.DisplayMember = "TrueName";
            this.cbx_Person.ValueMember = "UserName";
            this.cbx_TimeType.Items.Add("计划下达时间");
            this.cbx_TimeType.Items.Add("录入时间");
            this.cbx_TimeType.Items.Add("审核时间");
            this.cbx_TimeType.Items.Add("最后完成期限");
            this.cbx_TimeType.Text = this.cbx_TimeType.Items[0].ToString();
            this.cbx_PlanState.Items.Add("审核通过");
            this.cbx_PlanState.Items.Add("未审核通过");
            this.cbx_PlanState.Items.Insert(0, "");
            this.dtp_From.Value = DateTime.Now;
            this.dtp_To.Value = DateTime.Now;
            this.cbx_IsReceive.Items.Add("已签收");
            this.cbx_IsReceive.Items.Add("未签收");
            this.cbx_IsReceive.Items.Insert(0, "");
            this.cbx_ChangeList.Items.Add("已转单");
            this.cbx_ChangeList.Items.Add("未转单");
            this.cbx_ChangeList.Items.Insert(0, "");
            this.cbx_relation.Items.Add(">");
            this.cbx_relation.Items.Add("<");
            this.cbx_relation.Items.Add("=");
            this.cbx_relation.Items.Add("<>");
            this.cbx_relation.Items.Add(">=");
            this.cbx_relation.Items.Add("<=");
            this.cbx_relation.Items.Insert(0, "");
            this.cbx_NpState.Items.Add("未制作");
            this.cbx_NpState.Items.Add("制作中");
            this.cbx_NpState.Items.Add("制作完成");
            this.cbx_NpState.Items.Insert(0, "");
            this.cbx_SignIn.Visible = User.IsHaveAuthority("填写签收人");
            this.btn_RePrintSend.Visible = User.IsHaveAuthority("重新打印送货单");
            this.btn_RePrintTask.Visible = User.IsHaveAuthority("重新打印生产任务单");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.tabPage3 = new TabPage();
            this.groupBox5 = new GroupBox();
            this.splitContainer6 = new SplitContainer();
            this.splitDGD = new SplitContainer();
            this.dgv_Task = new DataGridView();
            this.dgv_TaskDetail = new DataGridView();
            this.lab_Total_Task = new Label();
            this.groupBox6 = new GroupBox();
            this.btnTaskPan = new Button();
            this.btnTaskProcess = new Button();
            this.cbx_PrintCodeBar = new CheckBox();
            this.cbx_PrintView = new CheckBox();
            this.btn_RePrintTask = new Button();
            this.btn_Query_Task = new Button();
            this.dtp_To_Task = new DateTimePicker();
            this.label13 = new Label();
            this.dtp_From_Task = new DateTimePicker();
            this.label15 = new Label();
            this.btn_ComQuery_Task = new Button();
            this.label17 = new Label();
            this.tabPage2 = new TabPage();
            this.groupBox3 = new GroupBox();
            this.dgv_Plan = new DataGridView();
            this.lab_Total = new Label();
            this.groupBox4 = new GroupBox();
            this.cbx_ReceiveMoney = new CheckBox();
            this.cbx_Big = new CheckBox();
            this.label22 = new Label();
            this.cbx_relation = new ComboBox();
            this.label21 = new Label();
            this.btn_PlanIdQuery = new Button();
            this.cbx_ChangeList = new ComboBox();
            this.label20 = new Label();
            this.label9 = new Label();
            this.dtp_To = new DateTimePicker();
            this.label11 = new Label();
            this.dtp_From = new DateTimePicker();
            this.cbx_PlanState = new ComboBox();
            this.label6 = new Label();
            this.cbx_TimeType = new ComboBox();
            this.label10 = new Label();
            this.cbx_PlanDepart = new ComboBox();
            this.cbx_PlanType = new ComboBox();
            this.btn_ComQuery = new Button();
            this.label5 = new Label();
            this.label7 = new Label();
            this.label8 = new Label();
            this.tabPage1 = new TabPage();
            this.groupBox2 = new GroupBox();
            this.drw_NpResult = new DataGridView();
            this.labTotal = new Label();
            this.groupBox1 = new GroupBox();
            this.btn_SendId = new Button();
            this.btn_TaskId = new Button();
            this.btn_PlanId = new Button();
            this.btn_QueryNpNo = new Button();
            this.label4 = new Label();
            this.label3 = new Label();
            this.label2 = new Label();
            this.label1 = new Label();
            this.tabControl = new TabControl();
            this.tabPage4 = new TabPage();
            this.groupBox7 = new GroupBox();
            this.dgv_Send = new DataGridView();
            this.lab_Total_Send = new Label();
            this.groupBox8 = new GroupBox();
            this.cbx_PrintSendView = new CheckBox();
            this.btn_RePrintSend = new Button();
            this.cbx_SignIn = new CheckBox();
            this.btn_PlanId_Task = new Button();
            this.btn_SendId_Send = new Button();
            this.cbx_IsReceive = new ComboBox();
            this.label19 = new Label();
            this.label12 = new Label();
            this.dtp_To_Send = new DateTimePicker();
            this.label14 = new Label();
            this.dtp_From_Send = new DateTimePicker();
            this.label16 = new Label();
            this.btn_OutTime = new Button();
            this.label18 = new Label();
            this.tabPage6 = new TabPage();
            this.groupBox11 = new GroupBox();
            this.dgv_Mboard = new DataGridView();
            this.lab_Total_Mb = new Label();
            this.groupBox12 = new GroupBox();
            this.btnPanProcess = new Button();
            this.btn_PrintCode = new Button();
            this.btn_MbTaskId = new Button();
            this.dtp_MbTo = new DateTimePicker();
            this.label30 = new Label();
            this.dtp_MbFrom = new DateTimePicker();
            this.label31 = new Label();
            this.btn_MbTime = new Button();
            this.label32 = new Label();
            this.tabPage7 = new TabPage();
            this.dataGridView1 = new DataGridView();
            this.panel3 = new Panel();
            this.labelResultInfo = new Label();
            this.panel2 = new Panel();
            this.treeVwAccount = new TreeView();
            this.panel1 = new Panel();
            this.groupBox15 = new GroupBox();
            this.groupBox17 = new GroupBox();
            this.label37 = new Label();
            this.dtp_absentTo = new DateTimePicker();
            this.btnAbsent = new Button();
            this.dtp_absentFrom = new DateTimePicker();
            this.label38 = new Label();
            this.label36 = new Label();
            this.btnWarehouseOperate = new Button();
            this.btnPanAccount = new Button();
            this.groupBox16 = new GroupBox();
            this.labelTaskInfo = new Label();
            this.btnRemakeAppr = new Button();
            this.btnInHouse = new Button();
            this.groupBox14 = new GroupBox();
            this.btnExpAccount2Excel = new Button();
            this.btnExpDetails2Excel = new Button();
            this.tabPage5 = new TabPage();
            this.splitContainer2 = new SplitContainer();
            this.groupBox9 = new GroupBox();
            this.splitContainer1 = new SplitContainer();
            this.dgv_State = new DataGridView();
            this.dgv_StatbyNPType = new DataGridView();
            this.lab_StateInfo = new Label();
            this.groupBox10 = new GroupBox();
            this.btnExpExcel = new Button();
            this.cbx_Person = new ComboBox();
            this.cbx_Workers = new ComboBox();
            this.label41 = new Label();
            this.label23 = new Label();
            this.dtp_ToAttend = new DateTimePicker();
            this.dtp_ToWorks = new DateTimePicker();
            this.label40 = new Label();
            this.label25 = new Label();
            this.dtp_FromAttend = new DateTimePicker();
            this.dtp_FromWorks = new DateTimePicker();
            this.label39 = new Label();
            this.btn_AttendStat = new Button();
            this.label28 = new Label();
            this.btn_Works = new Button();
            this.cbx_NpState = new ComboBox();
            this.label24 = new Label();
            this.cbx_PreviewPrintState = new CheckBox();
            this.btn_PrintStateResult = new Button();
            this.dtp_ToState = new DateTimePicker();
            this.label26 = new Label();
            this.dtp_FromState = new DateTimePicker();
            this.label27 = new Label();
            this.btn_State = new Button();
            this.groupBox13 = new GroupBox();
            this.label35 = new Label();
            this.btnTaskSend = new Button();
            this.dtp_ToSend = new DateTimePicker();
            this.dtp_FromPlan = new DateTimePicker();
            this.label29 = new Label();
            this.label34 = new Label();
            this.dtp_FromSend = new DateTimePicker();
            this.dtp_ToPlan = new DateTimePicker();
            this.label33 = new Label();
            this.tbx_SendId = new ReturnText();
            this.tbx_TaskId = new ReturnText();
            this.tbx_PlanId = new ReturnText();
            this.tbx_NpNo = new ReturnText();
            this.txt_PlanId = new ReturnText();
            this.txt_TaskId = new ReturnText();
            this.tbx_PlanId_Task = new ReturnText();
            this.tbx_SendId_Send = new ReturnText();
            this.tbx_MbTaskId = new ReturnText();
            this.tabPage3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            this.splitDGD.Panel1.SuspendLayout();
            this.splitDGD.Panel2.SuspendLayout();
            this.splitDGD.SuspendLayout();
            ((ISupportInitialize) this.dgv_Task).BeginInit();
            ((ISupportInitialize) this.dgv_TaskDetail).BeginInit();
            this.groupBox6.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((ISupportInitialize) this.dgv_Plan).BeginInit();
            this.groupBox4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((ISupportInitialize) this.drw_NpResult).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((ISupportInitialize) this.dgv_Send).BeginInit();
            this.groupBox8.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupBox11.SuspendLayout();
            ((ISupportInitialize) this.dgv_Mboard).BeginInit();
            this.groupBox12.SuspendLayout();
            this.tabPage7.SuspendLayout();
            ((ISupportInitialize) this.dataGridView1).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((ISupportInitialize) this.dgv_State).BeginInit();
            ((ISupportInitialize) this.dgv_StatbyNPType).BeginInit();
            this.groupBox10.SuspendLayout();
            this.groupBox13.SuspendLayout();
            base.SuspendLayout();
            this.tabPage3.Controls.Add(this.groupBox5);
            this.tabPage3.Controls.Add(this.groupBox6);
            this.tabPage3.Location = new Point(4, 0x15);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new Padding(8);
            this.tabPage3.Size = new Size(0x404, 0x199);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "生产任务单查询";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.groupBox5.Controls.Add(this.splitContainer6);
            this.groupBox5.Dock = DockStyle.Fill;
            this.groupBox5.Location = new Point(8, 0xa4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new Padding(8);
            this.groupBox5.Size = new Size(0x3f4, 0xed);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "查询结果";
            this.splitContainer6.Dock = DockStyle.Fill;
            this.splitContainer6.Location = new Point(8, 0x16);
            this.splitContainer6.Name = "splitContainer6";
            this.splitContainer6.Orientation = Orientation.Horizontal;
            this.splitContainer6.Panel1.Controls.Add(this.splitDGD);
            this.splitContainer6.Panel2.Controls.Add(this.lab_Total_Task);
            this.splitContainer6.Panel2MinSize = 15;
            this.splitContainer6.Size = new Size(0x3e4, 0xcf);
            this.splitContainer6.SplitterDistance = 0xae;
            this.splitContainer6.TabIndex = 6;
            this.splitDGD.Dock = DockStyle.Fill;
            this.splitDGD.Location = new Point(0, 0);
            this.splitDGD.Name = "splitDGD";
            this.splitDGD.Panel1.Controls.Add(this.dgv_Task);
            this.splitDGD.Panel2.Controls.Add(this.dgv_TaskDetail);
            this.splitDGD.Size = new Size(0x3e4, 0xae);
            this.splitDGD.SplitterDistance = 0x26e;
            this.splitDGD.TabIndex = 5;
            this.dgv_Task.AllowUserToAddRows = false;
            this.dgv_Task.AllowUserToDeleteRows = false;
            this.dgv_Task.AllowUserToOrderColumns = true;
            this.dgv_Task.AllowUserToResizeRows = false;
            this.dgv_Task.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_Task.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_Task.BorderStyle = BorderStyle.Fixed3D;
            this.dgv_Task.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Task.Dock = DockStyle.Fill;
            this.dgv_Task.GridColor = SystemColors.ActiveCaption;
            this.dgv_Task.Location = new Point(0, 0);
            this.dgv_Task.MultiSelect = false;
            this.dgv_Task.Name = "dgv_Task";
            this.dgv_Task.ReadOnly = true;
            this.dgv_Task.RowTemplate.Height = 0x17;
            this.dgv_Task.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.dgv_Task.Size = new Size(0x26e, 0xae);
            this.dgv_Task.TabIndex = 4;
            this.dgv_Task.CellContentDoubleClick += new DataGridViewCellEventHandler(this.dgv_Task_CellContentDoubleClick);
            this.dgv_TaskDetail.AllowUserToAddRows = false;
            this.dgv_TaskDetail.AllowUserToDeleteRows = false;
            this.dgv_TaskDetail.AllowUserToOrderColumns = true;
            this.dgv_TaskDetail.AllowUserToResizeRows = false;
            this.dgv_TaskDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_TaskDetail.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_TaskDetail.BorderStyle = BorderStyle.Fixed3D;
            this.dgv_TaskDetail.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_TaskDetail.Dock = DockStyle.Fill;
            this.dgv_TaskDetail.GridColor = SystemColors.ActiveCaption;
            this.dgv_TaskDetail.Location = new Point(0, 0);
            this.dgv_TaskDetail.MultiSelect = false;
            this.dgv_TaskDetail.Name = "dgv_TaskDetail";
            this.dgv_TaskDetail.ReadOnly = true;
            this.dgv_TaskDetail.RowTemplate.Height = 0x17;
            this.dgv_TaskDetail.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.dgv_TaskDetail.Size = new Size(370, 0xae);
            this.dgv_TaskDetail.TabIndex = 5;
            this.lab_Total_Task.AutoSize = true;
            this.lab_Total_Task.Dock = DockStyle.Bottom;
            this.lab_Total_Task.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.lab_Total_Task.Location = new Point(0, 7);
            this.lab_Total_Task.Name = "lab_Total_Task";
            this.lab_Total_Task.Padding = new Padding(5);
            this.lab_Total_Task.Size = new Size(10, 0x16);
            this.lab_Total_Task.TabIndex = 3;
            this.groupBox6.Controls.Add(this.btnTaskPan);
            this.groupBox6.Controls.Add(this.btnTaskProcess);
            this.groupBox6.Controls.Add(this.cbx_PrintCodeBar);
            this.groupBox6.Controls.Add(this.cbx_PrintView);
            this.groupBox6.Controls.Add(this.btn_RePrintTask);
            this.groupBox6.Controls.Add(this.txt_TaskId);
            this.groupBox6.Controls.Add(this.dtp_To_Task);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.dtp_From_Task);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.btn_ComQuery_Task);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.btn_Query_Task);
            this.groupBox6.Dock = DockStyle.Top;
            this.groupBox6.Location = new Point(8, 8);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new Size(0x3f4, 0x9c);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "查询条件";
            this.btnTaskPan.Location = new Point(0x1aa, 0x44);
            this.btnTaskPan.Name = "btnTaskPan";
            this.btnTaskPan.Size = new Size(0x56, 0x17);
            this.btnTaskPan.TabIndex = 0x36;
            this.btnTaskPan.Text = "底板来源查询";
            this.btnTaskPan.UseVisualStyleBackColor = true;
            this.btnTaskPan.Click += new EventHandler(this.btnTaskPan_Click);
            this.btnTaskProcess.Location = new Point(0x13e, 0x44);
            this.btnTaskProcess.Name = "btnTaskProcess";
            this.btnTaskProcess.Size = new Size(0x5c, 0x17);
            this.btnTaskProcess.TabIndex = 0x35;
            this.btnTaskProcess.Text = "制作人查询";
            this.btnTaskProcess.UseVisualStyleBackColor = true;
            this.btnTaskProcess.Click += new EventHandler(this.btnTaskProcess_Click);
            this.cbx_PrintCodeBar.AutoSize = true;
            this.cbx_PrintCodeBar.Checked = true;
            this.cbx_PrintCodeBar.CheckState = CheckState.Checked;
            this.cbx_PrintCodeBar.Location = new Point(600, 0x1b);
            this.cbx_PrintCodeBar.Name = "cbx_PrintCodeBar";
            this.cbx_PrintCodeBar.Size = new Size(0x6c, 0x10);
            this.cbx_PrintCodeBar.TabIndex = 0x34;
            this.cbx_PrintCodeBar.Text = "同时打印条形码";
            this.cbx_PrintCodeBar.UseVisualStyleBackColor = true;
            this.cbx_PrintCodeBar.Visible = false;
            this.cbx_PrintView.AutoSize = true;
            this.cbx_PrintView.Checked = true;
            this.cbx_PrintView.CheckState = CheckState.Checked;
            this.cbx_PrintView.Location = new Point(0x20a, 0x1b);
            this.cbx_PrintView.Name = "cbx_PrintView";
            this.cbx_PrintView.Size = new Size(0x48, 0x10);
            this.cbx_PrintView.TabIndex = 0x33;
            this.cbx_PrintView.Text = "打印预览";
            this.cbx_PrintView.UseVisualStyleBackColor = true;
            this.btn_RePrintTask.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btn_RePrintTask.ForeColor = Color.LimeGreen;
            this.btn_RePrintTask.Location = new Point(0x1aa, 0x19);
            this.btn_RePrintTask.Name = "btn_RePrintTask";
            this.btn_RePrintTask.Size = new Size(0x56, 0x17);
            this.btn_RePrintTask.TabIndex = 50;
            this.btn_RePrintTask.Text = "重新打印";
            this.btn_RePrintTask.UseVisualStyleBackColor = true;
            this.btn_RePrintTask.Click += new EventHandler(this.btn_RePrintTask_Click);
            this.btn_Query_Task.Location = new Point(0xdf, 0x44);
            this.btn_Query_Task.Name = "btn_Query_Task";
            this.btn_Query_Task.Size = new Size(0x4b, 0x17);
            this.btn_Query_Task.TabIndex = 0x24;
            this.btn_Query_Task.Text = "查询";
            this.btn_Query_Task.UseVisualStyleBackColor = true;
            this.btn_Query_Task.Click += new EventHandler(this.btn_Query_Task_Click);
            this.dtp_To_Task.CustomFormat = "";
            this.dtp_To_Task.Format = DateTimePickerFormat.Short;
            this.dtp_To_Task.Location = new Point(0xd5, 0x1b);
            this.dtp_To_Task.Name = "dtp_To_Task";
            this.dtp_To_Task.Size = new Size(0x54, 0x15);
            this.dtp_To_Task.TabIndex = 0x30;
            this.label13.AutoSize = true;
            this.label13.Location = new Point(0xbf, 0x1f);
            this.label13.Name = "label13";
            this.label13.Size = new Size(0x11, 12);
            this.label13.TabIndex = 0x2f;
            this.label13.Text = "到";
            this.dtp_From_Task.CustomFormat = "";
            this.dtp_From_Task.Format = DateTimePickerFormat.Short;
            this.dtp_From_Task.Location = new Point(0x63, 0x1b);
            this.dtp_From_Task.Name = "dtp_From_Task";
            this.dtp_From_Task.Size = new Size(0x56, 0x15);
            this.dtp_From_Task.TabIndex = 0x2e;
            this.label15.AutoSize = true;
            this.label15.Location = new Point(0x16, 0x1f);
            this.label15.Name = "label15";
            this.label15.Size = new Size(0x47, 12);
            this.label15.TabIndex = 0x2a;
            this.label15.Text = "下达时间从:";
            this.btn_ComQuery_Task.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btn_ComQuery_Task.ForeColor = Color.FromArgb(0, 0xc0, 0);
            this.btn_ComQuery_Task.Location = new Point(0x13e, 0x19);
            this.btn_ComQuery_Task.Name = "btn_ComQuery_Task";
            this.btn_ComQuery_Task.Size = new Size(0x5c, 0x17);
            this.btn_ComQuery_Task.TabIndex = 0x27;
            this.btn_ComQuery_Task.Text = "任务单查询";
            this.btn_ComQuery_Task.UseVisualStyleBackColor = true;
            this.btn_ComQuery_Task.Click += new EventHandler(this.btn_ComQuery_Task_Click);
            this.label17.AutoSize = true;
            this.label17.Location = new Point(0x16, 0x49);
            this.label17.Name = "label17";
            this.label17.Size = new Size(0x3b, 12);
            this.label17.TabIndex = 0x25;
            this.label17.Text = "任务单号:";
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new Point(4, 0x15);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(8);
            this.tabPage2.Size = new Size(0x404, 0x199);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "计划单查询";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.groupBox3.Controls.Add(this.dgv_Plan);
            this.groupBox3.Controls.Add(this.lab_Total);
            this.groupBox3.Dock = DockStyle.Fill;
            this.groupBox3.Location = new Point(8, 0xa4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new Padding(8);
            this.groupBox3.Size = new Size(0x3f4, 0xed);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询结果";
            this.dgv_Plan.AllowUserToAddRows = false;
            this.dgv_Plan.AllowUserToDeleteRows = false;
            this.dgv_Plan.AllowUserToOrderColumns = true;
            this.dgv_Plan.AllowUserToResizeRows = false;
            this.dgv_Plan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_Plan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_Plan.BorderStyle = BorderStyle.Fixed3D;
            this.dgv_Plan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Plan.Dock = DockStyle.Fill;
            this.dgv_Plan.GridColor = SystemColors.ActiveCaption;
            this.dgv_Plan.Location = new Point(8, 0x16);
            this.dgv_Plan.MultiSelect = false;
            this.dgv_Plan.Name = "dgv_Plan";
            this.dgv_Plan.ReadOnly = true;
            this.dgv_Plan.RowTemplate.Height = 0x17;
            this.dgv_Plan.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.dgv_Plan.Size = new Size(0x3e4, 0xb9);
            this.dgv_Plan.TabIndex = 2;
            this.dgv_Plan.CellClick += new DataGridViewCellEventHandler(this.dgv_Plan_CellClick);
            this.dgv_Plan.CellContentDoubleClick += new DataGridViewCellEventHandler(this.dgv_Plan_CellContentDoubleClick);
            this.lab_Total.AutoSize = true;
            this.lab_Total.Dock = DockStyle.Bottom;
            this.lab_Total.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.lab_Total.Location = new Point(8, 0xcf);
            this.lab_Total.Name = "lab_Total";
            this.lab_Total.Padding = new Padding(5);
            this.lab_Total.Size = new Size(10, 0x16);
            this.lab_Total.TabIndex = 1;
            this.groupBox4.Controls.Add(this.cbx_ReceiveMoney);
            this.groupBox4.Controls.Add(this.cbx_Big);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.cbx_relation);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.txt_PlanId);
            this.groupBox4.Controls.Add(this.cbx_ChangeList);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.dtp_To);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.dtp_From);
            this.groupBox4.Controls.Add(this.cbx_PlanState);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.cbx_TimeType);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.cbx_PlanDepart);
            this.groupBox4.Controls.Add(this.cbx_PlanType);
            this.groupBox4.Controls.Add(this.btn_ComQuery);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.btn_PlanIdQuery);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Dock = DockStyle.Top;
            this.groupBox4.Location = new Point(8, 8);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new Size(0x3f4, 0x9c);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "查询条件";
            this.cbx_ReceiveMoney.AutoSize = true;
            this.cbx_ReceiveMoney.ForeColor = Color.FromArgb(0, 0xc0, 0);
            this.cbx_ReceiveMoney.Location = new Point(0x175, 0x72);
            this.cbx_ReceiveMoney.Name = "cbx_ReceiveMoney";
            this.cbx_ReceiveMoney.Size = new Size(0x48, 0x10);
            this.cbx_ReceiveMoney.TabIndex = 0x29;
            this.cbx_ReceiveMoney.Text = "收款标记";
            this.cbx_ReceiveMoney.UseVisualStyleBackColor = true;
            this.cbx_ReceiveMoney.CheckedChanged += new EventHandler(this.cbx_ReceiveMoney_CheckedChanged);
            this.cbx_Big.AutoSize = true;
            this.cbx_Big.ForeColor = Color.Black;
            this.cbx_Big.Location = new Point(0xe2, 0x71);
            this.cbx_Big.Name = "cbx_Big";
            this.cbx_Big.Size = new Size(0x30, 0x10);
            this.cbx_Big.TabIndex = 40;
            this.cbx_Big.Text = "大单";
            this.cbx_Big.UseVisualStyleBackColor = true;
            this.label22.AutoSize = true;
            this.label22.Location = new Point(0x1e6, 0x49);
            this.label22.Name = "label22";
            this.label22.Size = new Size(0x35, 12);
            this.label22.TabIndex = 0x27;
            this.label22.Text = "实际数量";
            this.cbx_relation.FormattingEnabled = true;
            this.cbx_relation.Location = new Point(0x1af, 0x44);
            this.cbx_relation.Name = "cbx_relation";
            this.cbx_relation.Size = new Size(0x2f, 20);
            this.cbx_relation.TabIndex = 0x26;
            this.label21.AutoSize = true;
            this.label21.Location = new Point(0x173, 0x49);
            this.label21.Name = "label21";
            this.label21.Size = new Size(0x35, 12);
            this.label21.TabIndex = 0x25;
            this.label21.Text = "计划数量";
            this.btn_PlanIdQuery.Location = new Point(0x11d, 0x6c);
            this.btn_PlanIdQuery.Name = "btn_PlanIdQuery";
            this.btn_PlanIdQuery.Size = new Size(0x30, 0x17);
            this.btn_PlanIdQuery.TabIndex = 14;
            this.btn_PlanIdQuery.Text = "查询";
            this.btn_PlanIdQuery.UseVisualStyleBackColor = true;
            this.btn_PlanIdQuery.Click += new EventHandler(this.btn_PlanIdQuery_Click);
            this.cbx_ChangeList.FormattingEnabled = true;
            this.cbx_ChangeList.Location = new Point(0x57, 0x44);
            this.cbx_ChangeList.Name = "cbx_ChangeList";
            this.cbx_ChangeList.Size = new Size(0x85, 20);
            this.cbx_ChangeList.TabIndex = 0x23;
            this.label20.AutoSize = true;
            this.label20.Location = new Point(0x16, 0x47);
            this.label20.Name = "label20";
            this.label20.Size = new Size(0x3b, 12);
            this.label20.TabIndex = 0x22;
            this.label20.Text = "是否转单:";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(0x1fc, 0x1d);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x11, 12);
            this.label9.TabIndex = 0x21;
            this.label9.Text = "从";
            this.dtp_To.CustomFormat = "";
            this.dtp_To.Format = DateTimePickerFormat.Short;
            this.dtp_To.Location = new Point(0x27b, 0x19);
            this.dtp_To.Name = "dtp_To";
            this.dtp_To.Size = new Size(0x54, 0x15);
            this.dtp_To.TabIndex = 0x20;
            this.label11.AutoSize = true;
            this.label11.Location = new Point(0x266, 0x1d);
            this.label11.Name = "label11";
            this.label11.Size = new Size(0x11, 12);
            this.label11.TabIndex = 0x1f;
            this.label11.Text = "到";
            this.dtp_From.CustomFormat = "";
            this.dtp_From.Format = DateTimePickerFormat.Short;
            this.dtp_From.Location = new Point(0x20e, 0x19);
            this.dtp_From.Name = "dtp_From";
            this.dtp_From.Size = new Size(0x56, 0x15);
            this.dtp_From.TabIndex = 30;
            this.cbx_PlanState.FormattingEnabled = true;
            this.cbx_PlanState.Location = new Point(0x11d, 0x44);
            this.cbx_PlanState.Name = "cbx_PlanState";
            this.cbx_PlanState.Size = new Size(0x4e, 20);
            this.cbx_PlanState.TabIndex = 0x1d;
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0xe0, 0x47);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x3b, 12);
            this.label6.TabIndex = 0x1c;
            this.label6.Text = "审批状态:";
            this.cbx_TimeType.FormattingEnabled = true;
            this.cbx_TimeType.Location = new Point(0x1af, 0x1a);
            this.cbx_TimeType.Name = "cbx_TimeType";
            this.cbx_TimeType.Size = new Size(0x4b, 20);
            this.cbx_TimeType.TabIndex = 0x1b;
            this.label10.AutoSize = true;
            this.label10.Location = new Point(0x173, 0x1d);
            this.label10.Name = "label10";
            this.label10.Size = new Size(0x3b, 12);
            this.label10.TabIndex = 0x1a;
            this.label10.Text = "时间类型:";
            this.cbx_PlanDepart.FormattingEnabled = true;
            this.cbx_PlanDepart.Location = new Point(0x57, 0x1a);
            this.cbx_PlanDepart.Name = "cbx_PlanDepart";
            this.cbx_PlanDepart.Size = new Size(0x85, 20);
            this.cbx_PlanDepart.TabIndex = 0x19;
            this.cbx_PlanType.FormattingEnabled = true;
            this.cbx_PlanType.Location = new Point(0x11d, 0x1a);
            this.cbx_PlanType.Name = "cbx_PlanType";
            this.cbx_PlanType.Size = new Size(0x4e, 20);
            this.cbx_PlanType.TabIndex = 0x18;
            this.btn_ComQuery.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btn_ComQuery.ForeColor = Color.LimeGreen;
            this.btn_ComQuery.Location = new Point(0x27b, 0x44);
            this.btn_ComQuery.Name = "btn_ComQuery";
            this.btn_ComQuery.Size = new Size(0x4b, 0x17);
            this.btn_ComQuery.TabIndex = 0x17;
            this.btn_ComQuery.Text = "组合查询";
            this.btn_ComQuery.UseVisualStyleBackColor = true;
            this.btn_ComQuery.Click += new EventHandler(this.btn_ComQuery_Click);
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x16, 0x1d);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x3b, 12);
            this.label5.TabIndex = 0x15;
            this.label5.Text = "计划单位:";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(0x17, 0x72);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x3b, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "计划单号:";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(0xe2, 0x1d);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x3b, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "计划类型:";
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new Point(4, 0x15);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(8);
            this.tabPage1.Size = new Size(0x404, 0x199);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "车牌查询";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.groupBox2.Controls.Add(this.drw_NpResult);
            this.groupBox2.Controls.Add(this.labTotal);
            this.groupBox2.Dock = DockStyle.Fill;
            this.groupBox2.Location = new Point(8, 0xa4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new Padding(8);
            this.groupBox2.Size = new Size(0x3f4, 0xed);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "查询结果";
            this.drw_NpResult.AllowUserToAddRows = false;
            this.drw_NpResult.AllowUserToDeleteRows = false;
            this.drw_NpResult.AllowUserToOrderColumns = true;
            this.drw_NpResult.AllowUserToResizeRows = false;
            this.drw_NpResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.drw_NpResult.BorderStyle = BorderStyle.Fixed3D;
            this.drw_NpResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.drw_NpResult.Dock = DockStyle.Fill;
            this.drw_NpResult.GridColor = SystemColors.ActiveCaption;
            this.drw_NpResult.Location = new Point(8, 0x16);
            this.drw_NpResult.MultiSelect = false;
            this.drw_NpResult.Name = "drw_NpResult";
            this.drw_NpResult.ReadOnly = true;
            this.drw_NpResult.RowTemplate.Height = 0x17;
            this.drw_NpResult.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.drw_NpResult.Size = new Size(0x3e4, 0xb9);
            this.drw_NpResult.TabIndex = 1;
            this.drw_NpResult.CellContentDoubleClick += new DataGridViewCellEventHandler(this.drw_NpResult_CellContentDoubleClick);
            this.labTotal.AutoSize = true;
            this.labTotal.Dock = DockStyle.Bottom;
            this.labTotal.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.labTotal.Location = new Point(8, 0xcf);
            this.labTotal.Name = "labTotal";
            this.labTotal.Padding = new Padding(5);
            this.labTotal.Size = new Size(10, 0x16);
            this.labTotal.TabIndex = 0;
            this.groupBox1.Controls.Add(this.tbx_SendId);
            this.groupBox1.Controls.Add(this.tbx_TaskId);
            this.groupBox1.Controls.Add(this.tbx_PlanId);
            this.groupBox1.Controls.Add(this.tbx_NpNo);
            this.groupBox1.Controls.Add(this.btn_SendId);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btn_TaskId);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btn_PlanId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btn_QueryNpNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = DockStyle.Top;
            this.groupBox1.Location = new Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(0x3f4, 0x9c);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            this.groupBox1.Enter += new EventHandler(this.groupBox1_Enter);
            this.btn_SendId.Location = new Point(0x20d, 0x3d);
            this.btn_SendId.Name = "btn_SendId";
            this.btn_SendId.Size = new Size(0x30, 0x17);
            this.btn_SendId.TabIndex = 11;
            this.btn_SendId.Text = "查询";
            this.btn_SendId.UseVisualStyleBackColor = true;
            this.btn_SendId.Click += new EventHandler(this.btn_SendId_Click);
            this.btn_TaskId.Location = new Point(0xe4, 0x3f);
            this.btn_TaskId.Name = "btn_TaskId";
            this.btn_TaskId.Size = new Size(0x30, 0x17);
            this.btn_TaskId.TabIndex = 8;
            this.btn_TaskId.Text = "查询";
            this.btn_TaskId.UseVisualStyleBackColor = true;
            this.btn_TaskId.Click += new EventHandler(this.btn_TaskId_Click);
            this.btn_PlanId.Location = new Point(0x20d, 0x16);
            this.btn_PlanId.Name = "btn_PlanId";
            this.btn_PlanId.Size = new Size(0x30, 0x17);
            this.btn_PlanId.TabIndex = 5;
            this.btn_PlanId.Text = "查询";
            this.btn_PlanId.UseVisualStyleBackColor = true;
            this.btn_PlanId.Click += new EventHandler(this.btn_PlanId_Click);
            this.btn_QueryNpNo.Location = new Point(0xe3, 0x17);
            this.btn_QueryNpNo.Name = "btn_QueryNpNo";
            this.btn_QueryNpNo.Size = new Size(0x30, 0x17);
            this.btn_QueryNpNo.TabIndex = 2;
            this.btn_QueryNpNo.Text = "查询";
            this.btn_QueryNpNo.UseVisualStyleBackColor = true;
            this.btn_QueryNpNo.Click += new EventHandler(this.btn_QueryNpNo_Click);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x13e, 0x42);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x3b, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "送货单号:";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x15, 0x44);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x3b, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "任务单号:";
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x13e, 0x1c);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x3b, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "计划单号:";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x15, 0x1c);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x3b, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "车牌号码:";
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage6);
            this.tabControl.Controls.Add(this.tabPage7);
            this.tabControl.Controls.Add(this.tabPage5);
            this.tabControl.Dock = DockStyle.Fill;
            this.tabControl.Location = new Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new Size(0x40c, 0x1b2);
            this.tabControl.TabIndex = 0;
            this.tabPage4.Controls.Add(this.groupBox7);
            this.tabPage4.Controls.Add(this.groupBox8);
            this.tabPage4.Location = new Point(4, 0x15);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new Padding(8);
            this.tabPage4.Size = new Size(0x404, 0x199);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "送货单查询";
            this.tabPage4.UseVisualStyleBackColor = true;
            this.groupBox7.Controls.Add(this.dgv_Send);
            this.groupBox7.Controls.Add(this.lab_Total_Send);
            this.groupBox7.Dock = DockStyle.Fill;
            this.groupBox7.Location = new Point(8, 0xa4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new Padding(8);
            this.groupBox7.Size = new Size(0x3f4, 0xed);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "查询结果";
            this.dgv_Send.AllowUserToAddRows = false;
            this.dgv_Send.AllowUserToDeleteRows = false;
            this.dgv_Send.AllowUserToOrderColumns = true;
            this.dgv_Send.AllowUserToResizeRows = false;
            this.dgv_Send.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Send.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_Send.BorderStyle = BorderStyle.Fixed3D;
            this.dgv_Send.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Send.Dock = DockStyle.Fill;
            this.dgv_Send.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.dgv_Send.GridColor = SystemColors.ActiveCaption;
            this.dgv_Send.Location = new Point(8, 0x16);
            this.dgv_Send.MultiSelect = false;
            this.dgv_Send.Name = "dgv_Send";
            this.dgv_Send.RowTemplate.Height = 0x17;
            this.dgv_Send.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.dgv_Send.Size = new Size(0x3e4, 0xb9);
            this.dgv_Send.TabIndex = 4;
            this.dgv_Send.CellDoubleClick += new DataGridViewCellEventHandler(this.dgv_Send_CellDoubleClick);
            this.dgv_Send.CellContentDoubleClick += new DataGridViewCellEventHandler(this.dgv_Send_CellContentDoubleClick);
            this.dgv_Send.CellEndEdit += new DataGridViewCellEventHandler(this.dgv_Send_CellEndEdit);
            this.lab_Total_Send.AutoSize = true;
            this.lab_Total_Send.Dock = DockStyle.Bottom;
            this.lab_Total_Send.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.lab_Total_Send.Location = new Point(8, 0xcf);
            this.lab_Total_Send.Name = "lab_Total_Send";
            this.lab_Total_Send.Padding = new Padding(5);
            this.lab_Total_Send.Size = new Size(10, 0x16);
            this.lab_Total_Send.TabIndex = 3;
            this.groupBox8.Controls.Add(this.cbx_PrintSendView);
            this.groupBox8.Controls.Add(this.btn_RePrintSend);
            this.groupBox8.Controls.Add(this.cbx_SignIn);
            this.groupBox8.Controls.Add(this.tbx_PlanId_Task);
            this.groupBox8.Controls.Add(this.tbx_SendId_Send);
            this.groupBox8.Controls.Add(this.cbx_IsReceive);
            this.groupBox8.Controls.Add(this.label19);
            this.groupBox8.Controls.Add(this.label12);
            this.groupBox8.Controls.Add(this.btn_PlanId_Task);
            this.groupBox8.Controls.Add(this.dtp_To_Send);
            this.groupBox8.Controls.Add(this.label14);
            this.groupBox8.Controls.Add(this.dtp_From_Send);
            this.groupBox8.Controls.Add(this.label16);
            this.groupBox8.Controls.Add(this.btn_OutTime);
            this.groupBox8.Controls.Add(this.label18);
            this.groupBox8.Controls.Add(this.btn_SendId_Send);
            this.groupBox8.Dock = DockStyle.Top;
            this.groupBox8.Location = new Point(8, 8);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new Size(0x3f4, 0x9c);
            this.groupBox8.TabIndex = 6;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "查询条件";
            this.cbx_PrintSendView.AutoSize = true;
            this.cbx_PrintSendView.Checked = true;
            this.cbx_PrintSendView.CheckState = CheckState.Checked;
            this.cbx_PrintSendView.Location = new Point(0x23e, 0x44);
            this.cbx_PrintSendView.Name = "cbx_PrintSendView";
            this.cbx_PrintSendView.Size = new Size(0x48, 0x10);
            this.cbx_PrintSendView.TabIndex = 0x3b;
            this.cbx_PrintSendView.Text = "打印预览";
            this.cbx_PrintSendView.UseVisualStyleBackColor = true;
            this.btn_RePrintSend.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btn_RePrintSend.ForeColor = Color.LimeGreen;
            this.btn_RePrintSend.Location = new Point(700, 0x40);
            this.btn_RePrintSend.Name = "btn_RePrintSend";
            this.btn_RePrintSend.Size = new Size(0x4b, 0x17);
            this.btn_RePrintSend.TabIndex = 0x3a;
            this.btn_RePrintSend.Text = "重新打印";
            this.btn_RePrintSend.UseVisualStyleBackColor = true;
            this.btn_RePrintSend.Click += new EventHandler(this.btn_RePrintSend_Click);
            this.cbx_SignIn.AutoSize = true;
            this.cbx_SignIn.ForeColor = Color.LimeGreen;
            this.cbx_SignIn.Location = new Point(0x128, 0x44);
            this.cbx_SignIn.Name = "cbx_SignIn";
            this.cbx_SignIn.Size = new Size(0x54, 0x10);
            this.cbx_SignIn.TabIndex = 0x39;
            this.cbx_SignIn.Text = "填写签收人";
            this.cbx_SignIn.UseVisualStyleBackColor = true;
            this.cbx_SignIn.CheckedChanged += new EventHandler(this.cbx_SignIn_CheckedChanged);
            this.btn_PlanId_Task.Location = new Point(0xe2, 0x42);
            this.btn_PlanId_Task.Name = "btn_PlanId_Task";
            this.btn_PlanId_Task.Size = new Size(0x30, 0x17);
            this.btn_PlanId_Task.TabIndex = 50;
            this.btn_PlanId_Task.Text = "查询";
            this.btn_PlanId_Task.UseVisualStyleBackColor = true;
            this.btn_PlanId_Task.Click += new EventHandler(this.btn_PlanId_Task_Click);
            this.btn_SendId_Send.Location = new Point(0xe2, 0x19);
            this.btn_SendId_Send.Name = "btn_SendId_Send";
            this.btn_SendId_Send.Size = new Size(0x30, 0x17);
            this.btn_SendId_Send.TabIndex = 0x24;
            this.btn_SendId_Send.Text = "查询";
            this.btn_SendId_Send.UseVisualStyleBackColor = true;
            this.btn_SendId_Send.Click += new EventHandler(this.btn_SendId_Send_Click);
            this.cbx_IsReceive.FormattingEnabled = true;
            this.cbx_IsReceive.Location = new Point(0x289, 0x1b);
            this.cbx_IsReceive.Name = "cbx_IsReceive";
            this.cbx_IsReceive.Size = new Size(0x48, 20);
            this.cbx_IsReceive.TabIndex = 0x35;
            this.label19.AutoSize = true;
            this.label19.Location = new Point(0x23c, 30);
            this.label19.Name = "label19";
            this.label19.Size = new Size(0x47, 12);
            this.label19.TabIndex = 0x34;
            this.label19.Text = "是否已签收:";
            this.label12.AutoSize = true;
            this.label12.Location = new Point(0x16, 0x47);
            this.label12.Name = "label12";
            this.label12.Size = new Size(0x3b, 12);
            this.label12.TabIndex = 0x33;
            this.label12.Text = "计划单号:";
            this.dtp_To_Send.CustomFormat = "";
            this.dtp_To_Send.Format = DateTimePickerFormat.Short;
            this.dtp_To_Send.Location = new Point(0x1e2, 0x1b);
            this.dtp_To_Send.Name = "dtp_To_Send";
            this.dtp_To_Send.Size = new Size(0x54, 0x15);
            this.dtp_To_Send.TabIndex = 0x30;
            this.label14.AutoSize = true;
            this.label14.Location = new Point(0x1cd, 0x1f);
            this.label14.Name = "label14";
            this.label14.Size = new Size(0x11, 12);
            this.label14.TabIndex = 0x2f;
            this.label14.Text = "到";
            this.dtp_From_Send.CustomFormat = "";
            this.dtp_From_Send.Format = DateTimePickerFormat.Short;
            this.dtp_From_Send.Location = new Point(0x170, 0x1b);
            this.dtp_From_Send.Name = "dtp_From_Send";
            this.dtp_From_Send.Size = new Size(0x56, 0x15);
            this.dtp_From_Send.TabIndex = 0x2e;
            this.label16.AutoSize = true;
            this.label16.Location = new Point(0x126, 0x1f);
            this.label16.Name = "label16";
            this.label16.Size = new Size(0x47, 12);
            this.label16.TabIndex = 0x2a;
            this.label16.Text = "出库时间从:";
            this.btn_OutTime.Location = new Point(0x2d7, 0x19);
            this.btn_OutTime.Name = "btn_OutTime";
            this.btn_OutTime.Size = new Size(0x30, 0x17);
            this.btn_OutTime.TabIndex = 0x27;
            this.btn_OutTime.Text = "查询";
            this.btn_OutTime.UseVisualStyleBackColor = true;
            this.btn_OutTime.Click += new EventHandler(this.btn_OutTime_Click);
            this.label18.AutoSize = true;
            this.label18.Location = new Point(0x16, 30);
            this.label18.Name = "label18";
            this.label18.Size = new Size(0x3b, 12);
            this.label18.TabIndex = 0x25;
            this.label18.Text = "送货单号:";
            this.tabPage6.Controls.Add(this.groupBox11);
            this.tabPage6.Controls.Add(this.groupBox12);
            this.tabPage6.Location = new Point(4, 0x15);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new Padding(8);
            this.tabPage6.Size = new Size(0x404, 0x199);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "底板查询";
            this.tabPage6.UseVisualStyleBackColor = true;
            this.groupBox11.Controls.Add(this.dgv_Mboard);
            this.groupBox11.Controls.Add(this.lab_Total_Mb);
            this.groupBox11.Dock = DockStyle.Fill;
            this.groupBox11.Location = new Point(8, 0xa4);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Padding = new Padding(8);
            this.groupBox11.Size = new Size(0x3f4, 0xed);
            this.groupBox11.TabIndex = 5;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "查询结果";
            this.dgv_Mboard.AllowUserToAddRows = false;
            this.dgv_Mboard.AllowUserToDeleteRows = false;
            this.dgv_Mboard.AllowUserToOrderColumns = true;
            this.dgv_Mboard.AllowUserToResizeRows = false;
            this.dgv_Mboard.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_Mboard.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_Mboard.BorderStyle = BorderStyle.Fixed3D;
            this.dgv_Mboard.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Mboard.Dock = DockStyle.Fill;
            this.dgv_Mboard.GridColor = SystemColors.ActiveCaption;
            this.dgv_Mboard.Location = new Point(8, 0x16);
            this.dgv_Mboard.MultiSelect = false;
            this.dgv_Mboard.Name = "dgv_Mboard";
            this.dgv_Mboard.ReadOnly = true;
            this.dgv_Mboard.RowTemplate.Height = 0x17;
            this.dgv_Mboard.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Mboard.Size = new Size(0x3e4, 0xb9);
            this.dgv_Mboard.TabIndex = 4;
            this.lab_Total_Mb.AutoSize = true;
            this.lab_Total_Mb.Dock = DockStyle.Bottom;
            this.lab_Total_Mb.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.lab_Total_Mb.Location = new Point(8, 0xcf);
            this.lab_Total_Mb.Name = "lab_Total_Mb";
            this.lab_Total_Mb.Padding = new Padding(5);
            this.lab_Total_Mb.Size = new Size(10, 0x16);
            this.lab_Total_Mb.TabIndex = 3;
            this.groupBox12.Controls.Add(this.btnPanProcess);
            this.groupBox12.Controls.Add(this.btn_PrintCode);
            this.groupBox12.Controls.Add(this.tbx_MbTaskId);
            this.groupBox12.Controls.Add(this.dtp_MbTo);
            this.groupBox12.Controls.Add(this.label30);
            this.groupBox12.Controls.Add(this.dtp_MbFrom);
            this.groupBox12.Controls.Add(this.label31);
            this.groupBox12.Controls.Add(this.btn_MbTime);
            this.groupBox12.Controls.Add(this.label32);
            this.groupBox12.Controls.Add(this.btn_MbTaskId);
            this.groupBox12.Dock = DockStyle.Top;
            this.groupBox12.Location = new Point(8, 8);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new Size(0x3f4, 0x9c);
            this.groupBox12.TabIndex = 4;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "查询条件";
            this.btnPanProcess.Location = new Point(0x12f, 70);
            this.btnPanProcess.Name = "btnPanProcess";
            this.btnPanProcess.Size = new Size(0x4b, 0x17);
            this.btnPanProcess.TabIndex = 0x36;
            this.btnPanProcess.Text = "制作人查询";
            this.btnPanProcess.UseVisualStyleBackColor = true;
            this.btnPanProcess.Click += new EventHandler(this.btnPanProcess_Click);
            this.btn_PrintCode.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btn_PrintCode.ForeColor = Color.Red;
            this.btn_PrintCode.Location = new Point(0x1a9, 70);
            this.btn_PrintCode.Name = "btn_PrintCode";
            this.btn_PrintCode.Size = new Size(0x4b, 0x17);
            this.btn_PrintCode.TabIndex = 50;
            this.btn_PrintCode.Text = "打印条码";
            this.btn_PrintCode.UseVisualStyleBackColor = true;
            this.btn_PrintCode.Click += new EventHandler(this.btn_PrintCode_Click);
            this.btn_MbTaskId.Location = new Point(0xe2, 0x44);
            this.btn_MbTaskId.Name = "btn_MbTaskId";
            this.btn_MbTaskId.Size = new Size(0x30, 0x17);
            this.btn_MbTaskId.TabIndex = 0x24;
            this.btn_MbTaskId.Text = "查询";
            this.btn_MbTaskId.UseVisualStyleBackColor = true;
            this.btn_MbTaskId.Click += new EventHandler(this.btn_MbTaskId_Click);
            this.dtp_MbTo.CustomFormat = "";
            this.dtp_MbTo.Format = DateTimePickerFormat.Short;
            this.dtp_MbTo.Location = new Point(0xd5, 0x1b);
            this.dtp_MbTo.Name = "dtp_MbTo";
            this.dtp_MbTo.Size = new Size(0x54, 0x15);
            this.dtp_MbTo.TabIndex = 0x30;
            this.label30.AutoSize = true;
            this.label30.Location = new Point(0xbf, 0x1f);
            this.label30.Name = "label30";
            this.label30.Size = new Size(0x11, 12);
            this.label30.TabIndex = 0x2f;
            this.label30.Text = "到";
            this.dtp_MbFrom.CustomFormat = "";
            this.dtp_MbFrom.Format = DateTimePickerFormat.Short;
            this.dtp_MbFrom.Location = new Point(0x63, 0x1b);
            this.dtp_MbFrom.Name = "dtp_MbFrom";
            this.dtp_MbFrom.Size = new Size(0x56, 0x15);
            this.dtp_MbFrom.TabIndex = 0x2e;
            this.label31.AutoSize = true;
            this.label31.Location = new Point(0x16, 0x1f);
            this.label31.Name = "label31";
            this.label31.Size = new Size(0x47, 12);
            this.label31.TabIndex = 0x2a;
            this.label31.Text = "下达时间从:";
            this.btn_MbTime.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btn_MbTime.ForeColor = Color.Red;
            this.btn_MbTime.Location = new Point(0x12f, 0x19);
            this.btn_MbTime.Name = "btn_MbTime";
            this.btn_MbTime.Size = new Size(0x4b, 0x17);
            this.btn_MbTime.TabIndex = 0x27;
            this.btn_MbTime.Text = "开始查询";
            this.btn_MbTime.UseVisualStyleBackColor = true;
            this.btn_MbTime.Click += new EventHandler(this.btn_MbTime_Click);
            this.label32.AutoSize = true;
            this.label32.Location = new Point(0x16, 0x49);
            this.label32.Name = "label32";
            this.label32.Size = new Size(0x3b, 12);
            this.label32.TabIndex = 0x25;
            this.label32.Text = "任务单号:";
            this.tabPage7.Controls.Add(this.dataGridView1);
            this.tabPage7.Controls.Add(this.panel3);
            this.tabPage7.Controls.Add(this.panel2);
            this.tabPage7.Location = new Point(4, 0x15);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new Size(0x404, 0x199);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "库存统计";
            this.tabPage7.UseVisualStyleBackColor = true;
            this.tabPage7.Click += new EventHandler(this.tabPage7_Click);
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = DockStyle.Fill;
            this.dataGridView1.Location = new Point(0, 0x1b);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 0x17;
            this.dataGridView1.Size = new Size(0x2d3, 0x17e);
            this.dataGridView1.TabIndex = 14;
            this.panel3.BorderStyle = BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.labelResultInfo);
            this.panel3.Dock = DockStyle.Top;
            this.panel3.Location = new Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(0x2d3, 0x1b);
            this.panel3.TabIndex = 0x12;
            this.labelResultInfo.AutoSize = true;
            this.labelResultInfo.Location = new Point(3, 13);
            this.labelResultInfo.Name = "labelResultInfo";
            this.labelResultInfo.Size = new Size(11, 12);
            this.labelResultInfo.TabIndex = 15;
            this.labelResultInfo.Text = "0";
            this.panel2.Controls.Add(this.treeVwAccount);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = DockStyle.Right;
            this.panel2.Location = new Point(0x2d3, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x131, 0x199);
            this.panel2.TabIndex = 0x11;
            this.treeVwAccount.Dock = DockStyle.Fill;
            this.treeVwAccount.Location = new Point(0, 0x11e);
            this.treeVwAccount.Name = "treeVwAccount";
            this.treeVwAccount.Size = new Size(0x131, 0x7b);
            this.treeVwAccount.TabIndex = 13;
            this.treeVwAccount.AfterSelect += new TreeViewEventHandler(this.treeVwAccount_AfterSelect);
            this.panel1.BorderStyle = BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox15);
            this.panel1.Controls.Add(this.groupBox16);
            this.panel1.Controls.Add(this.groupBox14);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x131, 0x11e);
            this.panel1.TabIndex = 0x10;
            this.groupBox15.Controls.Add(this.groupBox17);
            this.groupBox15.Controls.Add(this.btnWarehouseOperate);
            this.groupBox15.Controls.Add(this.btnPanAccount);
            this.groupBox15.Location = new Point(15, 8);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new Size(0xfc, 0x8f);
            this.groupBox15.TabIndex = 11;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "统计";
            this.groupBox17.Controls.Add(this.label37);
            this.groupBox17.Controls.Add(this.dtp_absentTo);
            this.groupBox17.Controls.Add(this.btnAbsent);
            this.groupBox17.Controls.Add(this.dtp_absentFrom);
            this.groupBox17.Controls.Add(this.label38);
            this.groupBox17.Controls.Add(this.label36);
            this.groupBox17.Location = new Point(0x11, 50);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new Size(220, 0x57);
            this.groupBox17.TabIndex = 0x35;
            this.groupBox17.TabStop = false;
            this.label37.AutoSize = true;
            this.label37.Location = new Point(9, 0x11);
            this.label37.Name = "label37";
            this.label37.Size = new Size(0x3b, 12);
            this.label37.TabIndex = 0x31;
            this.label37.Text = "登记时间:";
            this.dtp_absentTo.CustomFormat = "";
            this.dtp_absentTo.Format = DateTimePickerFormat.Short;
            this.dtp_absentTo.Location = new Point(0x20, 60);
            this.dtp_absentTo.Name = "dtp_absentTo";
            this.dtp_absentTo.Size = new Size(0x56, 0x15);
            this.dtp_absentTo.TabIndex = 0x34;
            this.dtp_absentTo.ValueChanged += new EventHandler(this.dateTimePicker1_ValueChanged);
            this.btnAbsent.Location = new Point(0x7c, 40);
            this.btnAbsent.Name = "btnAbsent";
            this.btnAbsent.Size = new Size(0x60, 0x1c);
            this.btnAbsent.TabIndex = 7;
            this.btnAbsent.Text = "查看缺损";
            this.btnAbsent.UseVisualStyleBackColor = true;
            this.btnAbsent.Click += new EventHandler(this.btnAbsent_Click);
            this.dtp_absentFrom.CustomFormat = "";
            this.dtp_absentFrom.Format = DateTimePickerFormat.Short;
            this.dtp_absentFrom.Location = new Point(0x20, 0x21);
            this.dtp_absentFrom.Name = "dtp_absentFrom";
            this.dtp_absentFrom.Size = new Size(0x56, 0x15);
            this.dtp_absentFrom.TabIndex = 50;
            this.label38.AutoSize = true;
            this.label38.Location = new Point(9, 0x25);
            this.label38.Name = "label38";
            this.label38.Size = new Size(0x11, 12);
            this.label38.TabIndex = 0x33;
            this.label38.Text = "从";
            this.label36.AutoSize = true;
            this.label36.Location = new Point(9, 0x40);
            this.label36.Name = "label36";
            this.label36.Size = new Size(0x11, 12);
            this.label36.TabIndex = 0x33;
            this.label36.Text = "到";
            this.btnWarehouseOperate.Location = new Point(0x8d, 0x16);
            this.btnWarehouseOperate.Name = "btnWarehouseOperate";
            this.btnWarehouseOperate.Size = new Size(0x60, 0x1c);
            this.btnWarehouseOperate.TabIndex = 7;
            this.btnWarehouseOperate.Text = "查看当日操作";
            this.btnWarehouseOperate.UseVisualStyleBackColor = true;
            this.btnWarehouseOperate.Click += new EventHandler(this.btnWarehouseOperate_Click);
            this.btnPanAccount.Location = new Point(0x11, 0x16);
            this.btnPanAccount.Name = "btnPanAccount";
            this.btnPanAccount.Size = new Size(0x60, 0x1c);
            this.btnPanAccount.TabIndex = 7;
            this.btnPanAccount.Text = "查看库存";
            this.btnPanAccount.UseVisualStyleBackColor = true;
            this.btnPanAccount.Click += new EventHandler(this.btnPanAccount_Click);
            this.groupBox16.Controls.Add(this.labelTaskInfo);
            this.groupBox16.Controls.Add(this.btnRemakeAppr);
            this.groupBox16.Controls.Add(this.btnInHouse);
            this.groupBox16.Location = new Point(15, 0xda);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new Size(0xfc, 0x3d);
            this.groupBox16.TabIndex = 12;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "入库操作";
            this.labelTaskInfo.AutoSize = true;
            this.labelTaskInfo.Location = new Point(0x97, 0x11);
            this.labelTaskInfo.Name = "labelTaskInfo";
            this.labelTaskInfo.Size = new Size(0, 12);
            this.labelTaskInfo.TabIndex = 10;
            this.btnRemakeAppr.Location = new Point(0x8d, 20);
            this.btnRemakeAppr.Name = "btnRemakeAppr";
            this.btnRemakeAppr.Size = new Size(0x60, 0x1c);
            this.btnRemakeAppr.TabIndex = 7;
            this.btnRemakeAppr.Text = "补做审批";
            this.btnRemakeAppr.UseVisualStyleBackColor = true;
            this.btnRemakeAppr.Click += new EventHandler(this.btnRemakeAppr_Click);
            this.btnInHouse.Location = new Point(0x11, 20);
            this.btnInHouse.Name = "btnInHouse";
            this.btnInHouse.Size = new Size(0x60, 0x1c);
            this.btnInHouse.TabIndex = 7;
            this.btnInHouse.Text = "手工入库";
            this.btnInHouse.UseVisualStyleBackColor = true;
            this.btnInHouse.Click += new EventHandler(this.btnInHouse_Click);
            this.groupBox14.Controls.Add(this.btnExpAccount2Excel);
            this.groupBox14.Controls.Add(this.btnExpDetails2Excel);
            this.groupBox14.Location = new Point(15, 0x9d);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new Size(0xfc, 0x37);
            this.groupBox14.TabIndex = 12;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "导出Excel";
            this.btnExpAccount2Excel.Location = new Point(0x11, 20);
            this.btnExpAccount2Excel.Name = "btnExpAccount2Excel";
            this.btnExpAccount2Excel.Size = new Size(0x60, 0x1c);
            this.btnExpAccount2Excel.TabIndex = 7;
            this.btnExpAccount2Excel.Text = "统计信息导出";
            this.btnExpAccount2Excel.UseVisualStyleBackColor = true;
            this.btnExpAccount2Excel.Click += new EventHandler(this.btnExp2Excel_Click);
            this.btnExpDetails2Excel.Location = new Point(0x8d, 20);
            this.btnExpDetails2Excel.Name = "btnExpDetails2Excel";
            this.btnExpDetails2Excel.Size = new Size(0x60, 0x1c);
            this.btnExpDetails2Excel.TabIndex = 7;
            this.btnExpDetails2Excel.Text = "明细信息导出";
            this.btnExpDetails2Excel.UseVisualStyleBackColor = true;
            this.btnExpDetails2Excel.Click += new EventHandler(this.btnExpDetails2Excel_Click);
            this.tabPage5.Controls.Add(this.splitContainer2);
            this.tabPage5.Controls.Add(this.groupBox10);
            this.tabPage5.Location = new Point(4, 0x15);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new Padding(8);
            this.tabPage5.Size = new Size(0x404, 0x199);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "统计打印";
            this.tabPage5.UseVisualStyleBackColor = true;
            this.splitContainer2.Dock = DockStyle.Fill;
            this.splitContainer2.Location = new Point(8, 0x8f);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = Orientation.Horizontal;
            this.splitContainer2.Panel1.Controls.Add(this.groupBox9);
            this.splitContainer2.Panel2.Controls.Add(this.lab_StateInfo);
            this.splitContainer2.Size = new Size(0x3f4, 0x102);
            this.splitContainer2.SplitterDistance = 0xe4;
            this.splitContainer2.TabIndex = 10;
            this.groupBox9.Controls.Add(this.splitContainer1);
            this.groupBox9.Dock = DockStyle.Fill;
            this.groupBox9.Location = new Point(0, 0);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new Padding(8);
            this.groupBox9.Size = new Size(0x3f4, 0xe4);
            this.groupBox9.TabIndex = 9;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "统计结果";
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(8, 0x16);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.dgv_State);
            this.splitContainer1.Panel2.Controls.Add(this.dgv_StatbyNPType);
            this.splitContainer1.Size = new Size(0x3e4, 0xc6);
            this.splitContainer1.SplitterDistance = 0x24c;
            this.splitContainer1.TabIndex = 6;
            this.dgv_State.AllowUserToAddRows = false;
            this.dgv_State.AllowUserToDeleteRows = false;
            this.dgv_State.AllowUserToOrderColumns = true;
            this.dgv_State.AllowUserToResizeRows = false;
            this.dgv_State.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_State.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_State.BorderStyle = BorderStyle.Fixed3D;
            this.dgv_State.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_State.Dock = DockStyle.Fill;
            this.dgv_State.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.dgv_State.GridColor = SystemColors.ActiveCaption;
            this.dgv_State.Location = new Point(0, 0);
            this.dgv_State.MultiSelect = false;
            this.dgv_State.Name = "dgv_State";
            this.dgv_State.RowTemplate.Height = 0x17;
            this.dgv_State.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.dgv_State.Size = new Size(0x24c, 0xc6);
            this.dgv_State.TabIndex = 4;
            this.dgv_StatbyNPType.AllowUserToAddRows = false;
            this.dgv_StatbyNPType.AllowUserToDeleteRows = false;
            this.dgv_StatbyNPType.AllowUserToOrderColumns = true;
            this.dgv_StatbyNPType.AllowUserToResizeRows = false;
            this.dgv_StatbyNPType.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_StatbyNPType.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv_StatbyNPType.BorderStyle = BorderStyle.Fixed3D;
            this.dgv_StatbyNPType.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_StatbyNPType.Dock = DockStyle.Fill;
            this.dgv_StatbyNPType.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.dgv_StatbyNPType.GridColor = SystemColors.ActiveCaption;
            this.dgv_StatbyNPType.Location = new Point(0, 0);
            this.dgv_StatbyNPType.MultiSelect = false;
            this.dgv_StatbyNPType.Name = "dgv_StatbyNPType";
            this.dgv_StatbyNPType.RowTemplate.Height = 0x17;
            this.dgv_StatbyNPType.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.dgv_StatbyNPType.Size = new Size(0x194, 0xc6);
            this.dgv_StatbyNPType.TabIndex = 5;
            this.lab_StateInfo.AutoSize = true;
            this.lab_StateInfo.Location = new Point(6, 9);
            this.lab_StateInfo.Name = "lab_StateInfo";
            this.lab_StateInfo.Size = new Size(11, 12);
            this.lab_StateInfo.TabIndex = 0x40;
            this.lab_StateInfo.Text = "0";
            this.groupBox10.Controls.Add(this.btnExpExcel);
            this.groupBox10.Controls.Add(this.cbx_Person);
            this.groupBox10.Controls.Add(this.cbx_Workers);
            this.groupBox10.Controls.Add(this.label41);
            this.groupBox10.Controls.Add(this.label23);
            this.groupBox10.Controls.Add(this.dtp_ToAttend);
            this.groupBox10.Controls.Add(this.dtp_ToWorks);
            this.groupBox10.Controls.Add(this.label40);
            this.groupBox10.Controls.Add(this.label25);
            this.groupBox10.Controls.Add(this.dtp_FromAttend);
            this.groupBox10.Controls.Add(this.dtp_FromWorks);
            this.groupBox10.Controls.Add(this.label39);
            this.groupBox10.Controls.Add(this.btn_AttendStat);
            this.groupBox10.Controls.Add(this.label28);
            this.groupBox10.Controls.Add(this.btn_Works);
            this.groupBox10.Controls.Add(this.cbx_NpState);
            this.groupBox10.Controls.Add(this.label24);
            this.groupBox10.Controls.Add(this.cbx_PreviewPrintState);
            this.groupBox10.Controls.Add(this.btn_PrintStateResult);
            this.groupBox10.Controls.Add(this.dtp_ToState);
            this.groupBox10.Controls.Add(this.label26);
            this.groupBox10.Controls.Add(this.dtp_FromState);
            this.groupBox10.Controls.Add(this.label27);
            this.groupBox10.Controls.Add(this.btn_State);
            this.groupBox10.Controls.Add(this.groupBox13);
            this.groupBox10.Dock = DockStyle.Top;
            this.groupBox10.Location = new Point(8, 8);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new Size(0x3f4, 0x87);
            this.groupBox10.TabIndex = 8;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "统计条件";
            this.btnExpExcel.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btnExpExcel.ForeColor = Color.LimeGreen;
            this.btnExpExcel.Location = new Point(0x2d2, 0x69);
            this.btnExpExcel.Name = "btnExpExcel";
            this.btnExpExcel.Size = new Size(0x4b, 0x17);
            this.btnExpExcel.TabIndex = 0x4f;
            this.btnExpExcel.Text = "导入Excel";
            this.btnExpExcel.UseVisualStyleBackColor = true;
            this.btnExpExcel.Click += new EventHandler(this.btnExpExcel_Click);
            this.cbx_Person.FormattingEnabled = true;
            this.cbx_Person.Location = new Point(0x156, 0x5f);
            this.cbx_Person.Name = "cbx_Person";
            this.cbx_Person.Size = new Size(0x48, 20);
            this.cbx_Person.TabIndex = 0x44;
            this.cbx_Workers.FormattingEnabled = true;
            this.cbx_Workers.Location = new Point(0x156, 60);
            this.cbx_Workers.Name = "cbx_Workers";
            this.cbx_Workers.Size = new Size(0x48, 20);
            this.cbx_Workers.TabIndex = 0x44;
            this.label41.AutoSize = true;
            this.label41.Location = new Point(0x130, 0x63);
            this.label41.Name = "label41";
            this.label41.Size = new Size(0x23, 12);
            this.label41.TabIndex = 0x43;
            this.label41.Text = "人员:";
            this.label23.AutoSize = true;
            this.label23.Location = new Point(0x130, 0x40);
            this.label23.Name = "label23";
            this.label23.Size = new Size(0x23, 12);
            this.label23.TabIndex = 0x43;
            this.label23.Text = "人员:";
            this.dtp_ToAttend.CustomFormat = "";
            this.dtp_ToAttend.Format = DateTimePickerFormat.Short;
            this.dtp_ToAttend.Location = new Point(210, 0x5f);
            this.dtp_ToAttend.Name = "dtp_ToAttend";
            this.dtp_ToAttend.Size = new Size(0x54, 0x15);
            this.dtp_ToAttend.TabIndex = 0x42;
            this.dtp_ToAttend.ValueChanged += new EventHandler(this.dateTimePicker2_ValueChanged);
            this.dtp_ToWorks.CustomFormat = "";
            this.dtp_ToWorks.Format = DateTimePickerFormat.Short;
            this.dtp_ToWorks.Location = new Point(210, 60);
            this.dtp_ToWorks.Name = "dtp_ToWorks";
            this.dtp_ToWorks.Size = new Size(0x54, 0x15);
            this.dtp_ToWorks.TabIndex = 0x42;
            this.label40.AutoSize = true;
            this.label40.Location = new Point(0xbd, 0x63);
            this.label40.Name = "label40";
            this.label40.Size = new Size(0x11, 12);
            this.label40.TabIndex = 0x41;
            this.label40.Text = "到";
            this.label25.AutoSize = true;
            this.label25.Location = new Point(0xbd, 0x40);
            this.label25.Name = "label25";
            this.label25.Size = new Size(0x11, 12);
            this.label25.TabIndex = 0x41;
            this.label25.Text = "到";
            this.dtp_FromAttend.CustomFormat = "";
            this.dtp_FromAttend.Format = DateTimePickerFormat.Short;
            this.dtp_FromAttend.Location = new Point(0x60, 0x5f);
            this.dtp_FromAttend.Name = "dtp_FromAttend";
            this.dtp_FromAttend.Size = new Size(0x56, 0x15);
            this.dtp_FromAttend.TabIndex = 0x40;
            this.dtp_FromWorks.CustomFormat = "";
            this.dtp_FromWorks.Format = DateTimePickerFormat.Short;
            this.dtp_FromWorks.Location = new Point(0x60, 60);
            this.dtp_FromWorks.Name = "dtp_FromWorks";
            this.dtp_FromWorks.Size = new Size(0x56, 0x15);
            this.dtp_FromWorks.TabIndex = 0x40;
            this.label39.AutoSize = true;
            this.label39.Location = new Point(0x16, 0x63);
            this.label39.Name = "label39";
            this.label39.Size = new Size(0x47, 12);
            this.label39.TabIndex = 0x3f;
            this.label39.Text = "考勤时间从:";
            this.btn_AttendStat.Location = new Point(0x1ab, 0x5d);
            this.btn_AttendStat.Name = "btn_AttendStat";
            this.btn_AttendStat.Size = new Size(0x4b, 0x17);
            this.btn_AttendStat.TabIndex = 0x3e;
            this.btn_AttendStat.Text = "考勤统计";
            this.btn_AttendStat.UseVisualStyleBackColor = true;
            this.btn_AttendStat.Click += new EventHandler(this.btn_AttendStat_Click);
            this.label28.AutoSize = true;
            this.label28.Location = new Point(0x16, 0x40);
            this.label28.Name = "label28";
            this.label28.Size = new Size(0x47, 12);
            this.label28.TabIndex = 0x3f;
            this.label28.Text = "工作时间从:";
            this.btn_Works.Location = new Point(0x1ab, 0x3a);
            this.btn_Works.Name = "btn_Works";
            this.btn_Works.Size = new Size(0x4b, 0x17);
            this.btn_Works.TabIndex = 0x3e;
            this.btn_Works.Text = "工作量统计";
            this.btn_Works.UseVisualStyleBackColor = true;
            this.btn_Works.Click += new EventHandler(this.btn_Works_Click);
            this.cbx_NpState.FormattingEnabled = true;
            this.cbx_NpState.Location = new Point(0x156, 0x1b);
            this.cbx_NpState.Name = "cbx_NpState";
            this.cbx_NpState.Size = new Size(0x48, 20);
            this.cbx_NpState.TabIndex = 0x3d;
            this.label24.AutoSize = true;
            this.label24.Location = new Point(0x130, 0x1f);
            this.label24.Name = "label24";
            this.label24.Size = new Size(0x23, 12);
            this.label24.TabIndex = 60;
            this.label24.Text = "状态:";
            this.cbx_PreviewPrintState.AutoSize = true;
            this.cbx_PreviewPrintState.Checked = true;
            this.cbx_PreviewPrintState.CheckState = CheckState.Checked;
            this.cbx_PreviewPrintState.Location = new Point(0x224, 110);
            this.cbx_PreviewPrintState.Name = "cbx_PreviewPrintState";
            this.cbx_PreviewPrintState.Size = new Size(0x48, 0x10);
            this.cbx_PreviewPrintState.TabIndex = 0x3b;
            this.cbx_PreviewPrintState.Text = "打印预览";
            this.cbx_PreviewPrintState.UseVisualStyleBackColor = true;
            this.btn_PrintStateResult.Font = new Font("宋体", 9f, FontStyle.Bold, GraphicsUnit.Point, 0x86);
            this.btn_PrintStateResult.ForeColor = Color.LimeGreen;
            this.btn_PrintStateResult.Location = new Point(0x281, 0x69);
            this.btn_PrintStateResult.Name = "btn_PrintStateResult";
            this.btn_PrintStateResult.Size = new Size(0x4b, 0x17);
            this.btn_PrintStateResult.TabIndex = 0x3a;
            this.btn_PrintStateResult.Text = "打印";
            this.btn_PrintStateResult.UseVisualStyleBackColor = true;
            this.btn_PrintStateResult.Click += new EventHandler(this.btn_PrintStateResult_Click);
            this.dtp_ToState.CustomFormat = "";
            this.dtp_ToState.Format = DateTimePickerFormat.Short;
            this.dtp_ToState.Location = new Point(210, 0x1b);
            this.dtp_ToState.Name = "dtp_ToState";
            this.dtp_ToState.Size = new Size(0x54, 0x15);
            this.dtp_ToState.TabIndex = 0x30;
            this.label26.AutoSize = true;
            this.label26.Location = new Point(0xbd, 0x1f);
            this.label26.Name = "label26";
            this.label26.Size = new Size(0x11, 12);
            this.label26.TabIndex = 0x2f;
            this.label26.Text = "到";
            this.dtp_FromState.CustomFormat = "";
            this.dtp_FromState.Format = DateTimePickerFormat.Short;
            this.dtp_FromState.Location = new Point(0x60, 0x1b);
            this.dtp_FromState.Name = "dtp_FromState";
            this.dtp_FromState.Size = new Size(0x56, 0x15);
            this.dtp_FromState.TabIndex = 0x2e;
            this.label27.AutoSize = true;
            this.label27.Location = new Point(0x16, 0x1f);
            this.label27.Name = "label27";
            this.label27.Size = new Size(0x47, 12);
            this.label27.TabIndex = 0x2a;
            this.label27.Text = "录入时间从:";
            this.btn_State.Location = new Point(0x1ab, 0x19);
            this.btn_State.Name = "btn_State";
            this.btn_State.Size = new Size(0x4b, 0x17);
            this.btn_State.TabIndex = 0x27;
            this.btn_State.Text = "车牌统计";
            this.btn_State.UseVisualStyleBackColor = true;
            this.btn_State.Click += new EventHandler(this.btn_State_Click);
            this.groupBox13.Controls.Add(this.label35);
            this.groupBox13.Controls.Add(this.btnTaskSend);
            this.groupBox13.Controls.Add(this.dtp_ToSend);
            this.groupBox13.Controls.Add(this.dtp_FromPlan);
            this.groupBox13.Controls.Add(this.label29);
            this.groupBox13.Controls.Add(this.label34);
            this.groupBox13.Controls.Add(this.dtp_FromSend);
            this.groupBox13.Controls.Add(this.dtp_ToPlan);
            this.groupBox13.Controls.Add(this.label33);
            this.groupBox13.Location = new Point(0x220, 5);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new Size(0x191, 0x5e);
            this.groupBox13.TabIndex = 0x4e;
            this.groupBox13.TabStop = false;
            this.label35.AutoSize = true;
            this.label35.Location = new Point(15, 0x18);
            this.label35.Name = "label35";
            this.label35.Size = new Size(0x47, 12);
            this.label35.TabIndex = 70;
            this.label35.Text = "计划时间从:";
            this.btnTaskSend.Location = new Point(0x142, 0x13);
            this.btnTaskSend.Name = "btnTaskSend";
            this.btnTaskSend.Size = new Size(0x3f, 0x36);
            this.btnTaskSend.TabIndex = 0x45;
            this.btnTaskSend.Text = "计划单  送货单  统计";
            this.btnTaskSend.UseVisualStyleBackColor = true;
            this.btnTaskSend.Click += new EventHandler(this.btnTaskSend_Click);
            this.dtp_ToSend.CustomFormat = "";
            this.dtp_ToSend.Format = DateTimePickerFormat.Short;
            this.dtp_ToSend.Location = new Point(0xcb, 0x39);
            this.dtp_ToSend.Name = "dtp_ToSend";
            this.dtp_ToSend.Size = new Size(0x54, 0x15);
            this.dtp_ToSend.TabIndex = 0x4d;
            this.dtp_FromPlan.CustomFormat = "";
            this.dtp_FromPlan.Format = DateTimePickerFormat.Short;
            this.dtp_FromPlan.Location = new Point(0x59, 20);
            this.dtp_FromPlan.Name = "dtp_FromPlan";
            this.dtp_FromPlan.Size = new Size(0x56, 0x15);
            this.dtp_FromPlan.TabIndex = 0x47;
            this.label29.AutoSize = true;
            this.label29.Location = new Point(0xb6, 0x3d);
            this.label29.Name = "label29";
            this.label29.Size = new Size(0x11, 12);
            this.label29.TabIndex = 0x4c;
            this.label29.Text = "到";
            this.label34.AutoSize = true;
            this.label34.Location = new Point(0xb6, 0x18);
            this.label34.Name = "label34";
            this.label34.Size = new Size(0x11, 12);
            this.label34.TabIndex = 0x48;
            this.label34.Text = "到";
            this.dtp_FromSend.CustomFormat = "";
            this.dtp_FromSend.Format = DateTimePickerFormat.Short;
            this.dtp_FromSend.Location = new Point(0x59, 0x39);
            this.dtp_FromSend.Name = "dtp_FromSend";
            this.dtp_FromSend.Size = new Size(0x56, 0x15);
            this.dtp_FromSend.TabIndex = 0x4b;
            this.dtp_ToPlan.CustomFormat = "";
            this.dtp_ToPlan.Format = DateTimePickerFormat.Short;
            this.dtp_ToPlan.Location = new Point(0xcb, 20);
            this.dtp_ToPlan.Name = "dtp_ToPlan";
            this.dtp_ToPlan.Size = new Size(0x54, 0x15);
            this.dtp_ToPlan.TabIndex = 0x49;
            this.label33.AutoSize = true;
            this.label33.Location = new Point(15, 0x3d);
            this.label33.Name = "label33";
            this.label33.Size = new Size(0x47, 12);
            this.label33.TabIndex = 0x4a;
            this.label33.Text = "送货时间从:";
            this.tbx_SendId.LickButton = this.btn_SendId;
            this.tbx_SendId.Location = new Point(0x181, 0x3f);
            this.tbx_SendId.Name = "tbx_SendId";
            this.tbx_SendId.Size = new Size(0x84, 0x15);
            this.tbx_SendId.TabIndex = 15;
            this.tbx_TaskId.LickButton = this.btn_TaskId;
            this.tbx_TaskId.Location = new Point(0x58, 0x40);
            this.tbx_TaskId.Name = "tbx_TaskId";
            this.tbx_TaskId.Size = new Size(0x85, 0x15);
            this.tbx_TaskId.TabIndex = 14;
            this.tbx_PlanId.LickButton = this.btn_PlanId;
            this.tbx_PlanId.Location = new Point(0x181, 0x18);
            this.tbx_PlanId.Name = "tbx_PlanId";
            this.tbx_PlanId.Size = new Size(0x84, 0x15);
            this.tbx_PlanId.TabIndex = 13;
            this.tbx_NpNo.LickButton = this.btn_QueryNpNo;
            this.tbx_NpNo.Location = new Point(0x58, 0x19);
            this.tbx_NpNo.Name = "tbx_NpNo";
            this.tbx_NpNo.Size = new Size(0x85, 0x15);
            this.tbx_NpNo.TabIndex = 12;
            this.txt_PlanId.LickButton = this.btn_PlanIdQuery;
            this.txt_PlanId.Location = new Point(0x57, 0x6d);
            this.txt_PlanId.Name = "txt_PlanId";
            this.txt_PlanId.Size = new Size(0x85, 0x15);
            this.txt_PlanId.TabIndex = 0x24;
            this.txt_TaskId.LickButton = this.btn_Query_Task;
            this.txt_TaskId.Location = new Point(0x57, 70);
            this.txt_TaskId.Name = "txt_TaskId";
            this.txt_TaskId.Size = new Size(0x84, 0x15);
            this.txt_TaskId.TabIndex = 0x31;
            this.tbx_PlanId_Task.LickButton = this.btn_PlanId_Task;
            this.tbx_PlanId_Task.Location = new Point(0x58, 0x44);
            this.tbx_PlanId_Task.Name = "tbx_PlanId_Task";
            this.tbx_PlanId_Task.Size = new Size(0x84, 0x15);
            this.tbx_PlanId_Task.TabIndex = 0x37;
            this.tbx_SendId_Send.LickButton = this.btn_SendId_Send;
            this.tbx_SendId_Send.Location = new Point(0x57, 0x1b);
            this.tbx_SendId_Send.Name = "tbx_SendId_Send";
            this.tbx_SendId_Send.Size = new Size(0x85, 0x15);
            this.tbx_SendId_Send.TabIndex = 0x36;
            this.tbx_MbTaskId.LickButton = this.btn_MbTaskId;
            this.tbx_MbTaskId.Location = new Point(0x57, 70);
            this.tbx_MbTaskId.Name = "tbx_MbTaskId";
            this.tbx_MbTaskId.Size = new Size(0x84, 0x15);
            this.tbx_MbTaskId.TabIndex = 0x31;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x40c, 0x1b2);
            base.Controls.Add(this.tabControl);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "Frm_Query";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            this.Text = "查询统计";
            base.FormClosed += new FormClosedEventHandler(this.Frm_Query_FormClosed);
            base.Load += new EventHandler(this.Frm_Query_Load);
            this.tabPage3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel2.ResumeLayout(false);
            this.splitContainer6.Panel2.PerformLayout();
            this.splitContainer6.ResumeLayout(false);
            this.splitDGD.Panel1.ResumeLayout(false);
            this.splitDGD.Panel2.ResumeLayout(false);
            this.splitDGD.ResumeLayout(false);
            ((ISupportInitialize) this.dgv_Task).EndInit();
            ((ISupportInitialize) this.dgv_TaskDetail).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((ISupportInitialize) this.dgv_Plan).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((ISupportInitialize) this.drw_NpResult).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((ISupportInitialize) this.dgv_Send).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            ((ISupportInitialize) this.dgv_Mboard).EndInit();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            ((ISupportInitialize) this.dataGridView1).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((ISupportInitialize) this.dgv_State).EndInit();
            ((ISupportInitialize) this.dgv_StatbyNPType).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            base.ResumeLayout(false);
        }

        private void listVwClickNP()
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            DataSet set = new DataSet();
            string[] strArray = new string[7];
            string str = "";
            strArray[0] = " PressTime is null and withdrawtime is not null" + str;
            strArray[1] = " presstime is not null and EraseTime is null and SmearTime is null and DryingTime is null and ClashTime is  null" + str;
            strArray[2] = " presstime is not null and EraseTime is not null and SmearTime is null and DryingTime is null and ClashTime is  null" + str;
            strArray[3] = " presstime is not null and EraseTime is null and SmearTime is not null and DryingTime is null and ClashTime is  null  " + str;
            strArray[4] = " presstime is not null  and DryingTime is not null and ClashTime is  null and ProcessPackTime is  null " + str;
            strArray[5] = " (ProcessPackTime is  null) and (presstime is not null and ((erasetime is not null and clashtime is not null) or (smeartime is not null)) and  dryingtime  is not null)" + str;
            strArray[6] = " (ProcessPackTime is  not null) and (presstime is not null )" + str;
            string str2 = "";
            if (this.treeVwAccount.SelectedNode.Name.Substring(0, 3).CompareTo("TCC") == 0)
            {
                switch (this.treeVwAccount.SelectedNode.Name.Length)
                {
                    case 6:
                        str2 = " and withdrawtime is not null";
                        break;

                    case 7:
                        str2 = " and withdrawtime is not null and Code ='" + this.treeVwAccount.SelectedNode.Name.Substring(6, 1) + "'";
                        break;

                    case 8:
                        str2 = " and Code ='" + this.treeVwAccount.SelectedNode.Parent.Name.Substring(6, 1) + "' and " + strArray[int.Parse(this.treeVwAccount.SelectedNode.Name.Substring(7, 1))];
                        break;
                }
                string queryStr = this.NpResult + "where (SendID IS NULL) AND description <> '4' and taskid is not null " + str2;
                set = access.Run_SqlText(queryStr);
                if (set != null)
                {
                    this.dataGridView1.DataSource = set.Tables[0].DefaultView;
                    this.dataGridView1.Refresh();
                    this.labelResultInfo.Text = "一共查询到" + set.Tables[0].Rows.Count.ToString() + "记录!";
                    queryStr = "Select count(*) as count FROM V_NPquery WHERE  (SendID IS NULL) AND description <> '4' and taskid is not null " + str2;
                    set = access.Run_SqlText(queryStr);
                    this.labelResultInfo.Text = this.labelResultInfo.Text + "车牌合计" + set.Tables[0].Rows[0]["count"].ToString();
                }
                else
                {
                    this.dataGridView1.DataSource = null;
                    this.dataGridView1.Rows.Clear();
                }
            }
        }

        private void listVwClickNPOper()
        {
            string queryStr = "SELECT T_WarehouseOperation.TaskID AS 底板箱号, T_WarehouseOperation.OperateTime AS 操作时间, u1.TrueName AS 经办人,  u2.TrueName AS 操作人  FROM T_WarehouseOperation INNER JOIN  T_User u1 ON T_WarehouseOperation.HandlePerson = u1.UserName INNER JOIN  T_User u2 ON T_WarehouseOperation.OperatePerson = u2.UserName where (DATEDIFF([day], OperateTime, GETDATE()) = 0) and left(taskid,1)='T' ";
            if (this.treeVwAccount.SelectedNode.Name.CompareTo("车牌出库") == 0)
            {
                queryStr = queryStr + "and operatetype='出库'";
            }
            else
            {
                queryStr = queryStr + "and operatetype='入库'";
            }
            DataSet set = this.MyDB.Run_SqlText(queryStr);
            if (set != null)
            {
                this.dataGridView1.DataSource = set.Tables[0].DefaultView;
                this.dataGridView1.Refresh();
                this.labelResultInfo.Text = "一共查询到" + set.Tables[0].Rows.Count.ToString() + "记录!";
            }
            else
            {
                this.dataGridView1.DataSource = null;
                this.dataGridView1.Rows.Clear();
            }
        }

        private void listVwClickPan()
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            DataSet set = new DataSet();
            string[] strArray = new string[6];
            string[] strArray2 = new string[6];
            string str = "";
            if (this.treeVwAccount.SelectedNode.Name.Substring(0, 3).CompareTo("Pan") == 0)
            {
                string str2 = "SELECT PanTaskID AS 底板箱号, Pantype AS 底板尺寸, PanCount AS 数量, IsInWarehouse AS 是否在库中, CleanU AS 清洗人员, CleanTime AS 清洗时间,FilmU AS 贴膜人员, FilmTime AS 贴膜时间, SilkScreenU AS 丝印人员, SilkScreenTime AS 丝印时间, ClashU AS 冲孔人员, ClashTime AS 冲孔时间  FROM T_PanTaskInfo WHERE pancount>0 ";
                strArray[5] = " and isinwarehouse='1' and  whiteoryellow='1'  and  (ClashU IS not NULL) ";
                strArray[4] = " and isinwarehouse='1' and whiteoryellow='0' and (SilkScreenU IS not NULL) ";
                strArray[3] = " and isinwarehouse='1' and whiteoryellow='1' and FilmU is not null and ClashU IS NULL ";
                strArray[2] = " and isinwarehouse='1' and whiteoryellow='0' and FilmU is not null and (SilkScreenU IS NULL) ";
                strArray[1] = " and isinwarehouse='1' and whiteoryellow is null  and cleanU is not null ";
                strArray[0] = " and isinwarehouse is null and whiteoryellow is null and cleanU is null ";
                strArray2[5] = " and isinwarehouse='0' and whiteoryellow='1'  and  (ClashU IS not NULL) ";
                strArray2[4] = " and isinwarehouse='0' and whiteoryellow='0' and (SilkScreenU IS not NULL) ";
                strArray2[3] = " and isinwarehouse='0' and whiteoryellow='1' and FilmU is not null and (ClashU IS NULL) ";
                strArray2[2] = " and isinwarehouse='0' and whiteoryellow='0' and FilmU is not null and (SilkScreenU IS NULL) ";
                strArray2[1] = " and isinwarehouse='0' and whiteoryellow is null  and cleanU is not null ";
                strArray2[0] = " and isinwarehouse='0' and whiteoryellow is null and cleanU is null  ";
                switch (this.treeVwAccount.SelectedNode.Name.Length)
                {
                    case 3:
                        str = "";
                        break;

                    case 4:
                        str = " and Pantype='" + this.treeVwAccount.SelectedNode.Name.Substring(3, 1) + "'";
                        break;

                    case 6:
                        str = " and (isinwarehouse='1'or isinwarehouse is null) and Pantype='" + this.treeVwAccount.SelectedNode.Parent.Name.Substring(3, 1) + "'";
                        break;

                    case 7:
                        str = strArray[int.Parse(this.treeVwAccount.SelectedNode.Name.Substring(6, 1))] + " and Pantype='" + this.treeVwAccount.SelectedNode.Parent.Name.Substring(5, 1) + "'";
                        break;

                    case 9:
                        str = " and (isinwarehouse='0') and Pantype='" + this.treeVwAccount.SelectedNode.Parent.Name.Substring(3, 1) + "'";
                        break;

                    case 10:
                        str = strArray2[int.Parse(this.treeVwAccount.SelectedNode.Name.Substring(9, 1))] + " and Pantype='" + this.treeVwAccount.SelectedNode.Parent.Name.Substring(8, 1) + "'";
                        break;
                }
                set = access.Run_SqlText(str2 + str);
                if (set != null)
                {
                    this.dataGridView1.DataSource = set.Tables[0].DefaultView;
                    this.dataGridView1.Refresh();
                    this.labelResultInfo.Text = "一共查询到" + set.Tables[0].Rows.Count.ToString() + "记录!";
                    set = access.Run_SqlText("Select sum(pancount) as count FROM T_PanTaskInfo WHERE pancount>0 " + str);
                    this.labelResultInfo.Text = this.labelResultInfo.Text + "底板合计" + set.Tables[0].Rows[0]["count"].ToString();
                }
                else
                {
                    this.dataGridView1.DataSource = null;
                    this.dataGridView1.Rows.Clear();
                }
            }
        }

        private void listVwClickPanOper()
        {
            string queryStr = "SELECT T_WarehouseOperation.TaskID AS 底板箱号, T_WarehouseOperation.OperateTime AS 操作时间, u1.TrueName AS 经办人,  u2.TrueName AS 操作人  FROM T_WarehouseOperation INNER JOIN  T_User u1 ON T_WarehouseOperation.HandlePerson = u1.UserName INNER JOIN  T_User u2 ON T_WarehouseOperation.OperatePerson = u2.UserName where (DATEDIFF([day], OperateTime, GETDATE()) = 0) and left(taskid,1)='M' ";
            if (this.treeVwAccount.SelectedNode.Name.CompareTo("底板出库") == 0)
            {
                queryStr = queryStr + "and operatetype='出库'";
            }
            else
            {
                queryStr = queryStr + "and operatetype='入库'";
            }
            DataSet set = this.MyDB.Run_SqlText(queryStr);
            if (set != null)
            {
                this.dataGridView1.DataSource = set.Tables[0].DefaultView;
                this.dataGridView1.Refresh();
                this.labelResultInfo.Text = "一共查询到" + set.Tables[0].Rows.Count.ToString() + "记录!";
            }
            else
            {
                this.dataGridView1.DataSource = null;
                this.dataGridView1.Rows.Clear();
            }
        }

        private void listVwClickWithDraw()
        {
            string queryStr = "SELECT V_MbType.CodeName AS 底板类型, T_WithdrawPan.TaskID AS 车牌箱号,  T_WithdrawPan.PanTaskID AS 底板箱号, T_WithdrawPan.PanCount AS 数量,   T_WithdrawPan.WithdrawTime AS 领料时间, u1.TrueName AS 领料人,   u2.TrueName AS 操作员  FROM T_WithdrawPan INNER JOIN  V_MbType ON T_WithdrawPan.PanType = V_MbType.Code INNER JOIN  T_User u1 ON T_WithdrawPan.WithdrawPerson = u1.UserName INNER JOIN  T_User u2 ON T_WithdrawPan.OperatePerson = u2.UserName  WHERE (DATEDIFF([day], T_WithdrawPan.WithdrawTime, GETDATE()) = 0)";
            if (this.treeVwAccount.SelectedNode.Name.Length != 11)
            {
                queryStr = queryStr + " and T_WithdrawPan.pantype='" + this.treeVwAccount.SelectedNode.Name.Substring(11, this.treeVwAccount.SelectedNode.Name.Length - 11) + "'";
            }
            DataSet set = this.MyDB.Run_SqlText(queryStr);
            if (set != null)
            {
                this.dataGridView1.DataSource = set.Tables[0].DefaultView;
                this.dataGridView1.Refresh();
                this.labelResultInfo.Text = "一共查询到" + set.Tables[0].Rows.Count.ToString() + "记录!";
            }
            else
            {
                this.dataGridView1.DataSource = null;
                this.dataGridView1.Rows.Clear();
            }
        }

        private void Pd_PrintPageSendDetails(object sender, PrintPageEventArgs e)
        {
            this.DetailPrintPage++;
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

        public void ReadNodetoSheet(TreeNode temNode, Excel._Worksheet mySheet, ref int row)
        {
            if (temNode != null)
            {
                int index = temNode.Text.IndexOf("(");
                int num2 = temNode.Text.IndexOf(")");
                if ((index > 0) && (num2 > 0))
                {
                    mySheet.Cells[(int) row, temNode.Level + 1] = temNode.Text.Substring(0, index);
                }
                mySheet.Cells[(int) row, temNode.Level + 2] = temNode.Text.Substring(index + 1, (num2 - index) - 1);
                row++;
            }
            if (temNode.Nodes.Count > 0)
            {
                this.ReadNodetoSheet(temNode.Nodes[0], mySheet, ref row);
            }
            if (temNode.NextNode != null)
            {
                this.ReadNodetoSheet(temNode.NextNode, mySheet, ref row);
            }
        }

        private void tabPage7_Click(object sender, EventArgs e)
        {
        }

        private void treeVwAccount_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string str = e.Node.Name.Substring(0, 2);
            if (str != null)
            {
                if (!(str == "Pa"))
                {
                    if (!(str == "TC"))
                    {
                        if (!(str == "Wi"))
                        {
                            if (!(str == "车牌"))
                            {
                                if (str == "底板")
                                {
                                    this.listVwClickPanOper();
                                }
                                return;
                            }
                            this.listVwClickNPOper();
                            return;
                        }
                        this.listVwClickWithDraw();
                        return;
                    }
                }
                else
                {
                    this.listVwClickPan();
                    return;
                }
                this.listVwClickNP();
            }
        }

        public static Frm_Query Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Frm_Query();
                }
                return m_Instance;
            }
        }
    }
}

