namespace NpMis
{
    using Common;
    using System;
    using System.Collections;
    using System.Data;

    internal class WorkPlan
    {
        private static int m_IntWorkPlanID = -1;
        private static string m_StrWorkPlanID;

        public static string CreateWorkPlanID()
        {
            Hashtable hT = new Hashtable();
            Sql2KDataAccess access = new Sql2KDataAccess();
            m_IntWorkPlanID = Convert.ToInt32(access.RunProcedureNoRecord("@NewWorkPlanID", SqlDbType.Char, hT, "P_CreateWorkPlanID").Trim());
            m_StrWorkPlanID = "W" + DateTime.Now.ToString("yyyyMMdd") + m_IntWorkPlanID.ToString("D3");
            return m_StrWorkPlanID;
        }

        public static DataSet GetTodayWork(DateTime theDay)
        {
            string queryStr = "";
            DataSet set = new DataSet();
            Sql2KDataAccess access = new Sql2KDataAccess();
            queryStr = "select * from v_workplan where datediff(day,'" + theDay + "',WorkDay)=0";
            return access.Run_SqlText(queryStr);
        }

        public static void InsertFixedBox(DataSet FixedBoxSet)
        {
            if ((FixedBoxSet != null) && (FixedBoxSet.Tables[0].Rows.Count != 0))
            {
                string queryStr = "";
                Sql2KDataAccess access = new Sql2KDataAccess();
                for (int i = 0; i < FixedBoxSet.Tables[0].Rows.Count; i++)
                {
                    queryStr = "Insert into t_WorkPlanFixedBox(WorkPLanID,TaskID)  values('" + FixedBoxSet.Tables[0].Rows[i]["WorkPlanID"].ToString() + "','" + FixedBoxSet.Tables[0].Rows[i]["TaskID"].ToString() + "')";
                    access.Run_SqlText(queryStr);
                }
            }
        }

        public static void InsertWorkPlan(DataSet WorkPlanSet)
        {
            if ((WorkPlanSet != null) && (WorkPlanSet.Tables[0].Rows.Count != 0))
            {
                string queryStr = "";
                Sql2KDataAccess access = new Sql2KDataAccess();
                for (int i = 0; i < WorkPlanSet.Tables[0].Rows.Count; i++)
                {
                    queryStr = string.Concat(new object[] { "Insert into t_WorkPlan(WorkPLanID,UserName,ProcessID,WorkLoad,WorkDay,MakeTime,MakePeron,Remark)  values('", WorkPlanSet.Tables[0].Rows[i]["WorkPLanID"].ToString(), "','", WorkPlanSet.Tables[0].Rows[i]["UserName"].ToString(), "','", WorkPlanSet.Tables[0].Rows[i]["ProcessID"].ToString(), "',", int.Parse(WorkPlanSet.Tables[0].Rows[i]["WorkLoad"].ToString()), ",'", WorkPlanSet.Tables[0].Rows[i]["WorkDay"].ToString(), "',getdate(),'", WorkPlanSet.Tables[0].Rows[i]["MakePeron"].ToString(), "','", WorkPlanSet.Tables[0].Rows[i]["Remark"].ToString(), "')" });
                    access.Run_SqlText(queryStr);
                }
            }
        }

        public string WorkPlanID
        {
            get
            {
                m_StrWorkPlanID = "W" + DateTime.Now.ToString("yyyyMMdd") + m_IntWorkPlanID.ToString("D3");
                return m_StrWorkPlanID;
            }
        }
    }
}

