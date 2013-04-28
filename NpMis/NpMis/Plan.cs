namespace NpMis
{
    using Common;
    using System;
    using System.Collections;
    using System.Data;

    internal class Plan
    {
        private string m_Depart;
        private DateTime m_InputTime;
        private int m_InputUser;
        private DateTime m_LastTime;
        private DateTime m_OverTime;
        private string m_PlandKind;
        private string m_PlanId;
        private DateTime m_PlanTime;
        private int m_TotalCount;

        public DataSet GetAuditedPlan(bool IsInner, bool IsRemark)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            DataSet dataset = new DataSet();
            hT.Add("@Result", 1);
            hT.Add("@IsInner", IsInner ? 1 : 0);
            if (IsRemark)
            {
                access.RunProcedure(ref dataset, hT, "P_GetReMakePlanforAppr");
                return dataset;
            }
            access.RunProcedure(ref dataset, hT, "P_GetPlanByState");
            return dataset;
        }

        public DataSet GetNpByPlanId(string PlanId)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            DataSet dataset = new DataSet();
            hT.Add("@PlanId", PlanId);
            access.RunProcedure(ref dataset, hT, "P_GetNpByPlanId");
            return dataset;
        }

        public DataSet GetNpCountByPlanID(string PlanID)
        {
            object obj2 = new object();
            lock (obj2)
            {
                Sql2KDataAccess access = new Sql2KDataAccess();
                new Hashtable();
                DataSet set = new DataSet();
                string queryStr = "select nptype,count(*) Count from t_NP where planid='" + PlanID + "' group by nptype";
                return access.Run_SqlText(queryStr);
            }
        }

        public static Hashtable GetNpType()
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            DataSet set = new DataSet();
            Hashtable hashtable = new Hashtable();
            set = access.Run_SqlText("select Code,CodeName from V_nptype");
            if ((set != null) && (set.Tables[0].Rows.Count > 0))
            {
                for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                {
                    hashtable.Add(set.Tables[0].Rows[i]["Code"].ToString(), set.Tables[0].Rows[i]["CodeName"].ToString());
                }
            }
            return hashtable;
        }

        public static DataSet GetNpTypeDS()
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            DataSet set = new DataSet();
            return access.Run_SqlText("select Code,Description from V_nptype");
        }

        public static Hashtable GetPlanDepart()
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            DataSet set = new DataSet();
            Hashtable hashtable = new Hashtable();
            set = access.Run_SqlText("select Code,CodeName from V_Depart");
            if ((set != null) && (set.Tables[0].Rows.Count > 0))
            {
                for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                {
                    hashtable.Add(set.Tables[0].Rows[i]["Code"].ToString(), set.Tables[0].Rows[i]["CodeName"].ToString());
                }
            }
            return hashtable;
        }

        public bool GetPlanInfo(string PlanId)
        {
            return true;
        }

        public string GetPlanRemark(string PlanId)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            hT.Add("@PlanId", PlanId);
            return access.RunProcedureNoRecord("@Remark", SqlDbType.VarChar, hT, "P_GetPlanRemark");
        }

        public static Hashtable GetPlanType()
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            DataSet set = new DataSet();
            Hashtable hashtable = new Hashtable();
            set = access.Run_SqlText("select Code,CodeName from V_PlanKind");
            if ((set != null) && (set.Tables[0].Rows.Count > 0))
            {
                for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                {
                    hashtable.Add(set.Tables[0].Rows[i]["Code"].ToString(), set.Tables[0].Rows[i]["CodeName"].ToString());
                }
            }
            return hashtable;
        }

        public DataSet GetToDoPlan()
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            DataSet dataset = new DataSet();
            access.RunProcedure(ref dataset, hT, "P_GetToDoPlan");
            return dataset;
        }

        public DataSet GetUnApprPlan(bool IsInner, bool IsRemark)
        {
            object obj2 = new object();
            lock (obj2)
            {
                Sql2KDataAccess access = new Sql2KDataAccess();
                Hashtable hT = new Hashtable();
                DataSet dataset = new DataSet();
                hT.Add("@Result", 0);
                hT.Add("@IsInner", IsInner ? 1 : 0);
                if (IsRemark)
                {
                    access.RunProcedure(ref dataset, hT, "P_GetReMakePlanforAppr");
                }
                else
                {
                    access.RunProcedure(ref dataset, hT, "P_GetPlanByState");
                }
                return dataset;
            }
        }

        public bool NewPlan()
        {
            return false;
        }

        public bool SmtAudiResult(string PlanId, bool IsPass)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            hT.Add("@PlanId", PlanId);
            hT.Add("@UserName", User.UserName);
            hT.Add("@IsPass", IsPass ? "1" : "0");
            return access.RunProcedureNoReturn(hT, "P_AudiResult");
        }

        public static bool UpdateReceiveMonyState(string PlanId, bool Received)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            hT.Add("@PlanId", PlanId);
            hT.Add("@ReceiveMoney", Received ? "1" : "0");
            return access.RunProcedureNoReturn(hT, "P_ReceiveMoney ");
        }

        public string Depart
        {
            get
            {
                return this.m_Depart;
            }
            set
            {
                this.m_Depart = value;
            }
        }

        public DateTime InputTime
        {
            get
            {
                return this.m_InputTime;
            }
            set
            {
                this.m_InputTime = value;
            }
        }

        public int InputUser
        {
            get
            {
                return this.m_InputUser;
            }
            set
            {
                this.m_InputUser = value;
            }
        }

        public DateTime LastTime
        {
            get
            {
                return this.m_LastTime;
            }
            set
            {
                this.m_LastTime = value;
            }
        }

        public DateTime OverTime
        {
            get
            {
                return this.m_OverTime;
            }
            set
            {
                this.m_OverTime = value;
            }
        }

        public DateTime PlandTime
        {
            get
            {
                return this.m_PlanTime;
            }
            set
            {
                this.m_PlanTime = value;
            }
        }

        public string PlanId
        {
            get
            {
                return this.m_PlanId;
            }
            set
            {
                this.m_PlanId = value;
            }
        }

        public string PlanKind
        {
            get
            {
                return this.m_PlandKind;
            }
            set
            {
                this.m_PlandKind = value;
            }
        }

        public int TotalCount
        {
            get
            {
                return this.m_TotalCount;
            }
            set
            {
                this.m_TotalCount = value;
            }
        }
    }
}

