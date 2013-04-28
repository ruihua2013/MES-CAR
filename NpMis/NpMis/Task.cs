namespace NpMis
{
    using Common;
    using System;
    using System.Collections;
    using System.Data;

    internal class Task
    {
        public DataSet GetFinishTask()
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            DataSet dataset = new DataSet();
            Hashtable hT = new Hashtable();
            access.RunProcedure(ref dataset, hT, "P_GetFinishTask");
            return dataset;
        }

        public DataSet GetNewTask()
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            DataSet dataset = new DataSet();
            Hashtable hT = new Hashtable();
            access.RunProcedure(ref dataset, hT, "P_GetNewTask");
            return dataset;
        }

        public DataSet GetNpByTaskId(string TaskId)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            DataSet dataset = new DataSet();
            hT.Add("@TaskId", TaskId);
            access.RunProcedure(ref dataset, hT, "P_GetNpByTaskId");
            return dataset;
        }

        public string NewTask()
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            new DataSet();
            hT.Add("@UserName", User.UserName);
            return access.RunProcedureNoRecord("@TaskId", SqlDbType.VarChar, hT, "P_NewTask");
        }

        public bool NpToTask(string TaskId, string NpId)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            hT.Add("@TaskId", TaskId);
            hT.Add("@NpId", NpId);
            return access.RunProcedureNoReturn(hT, "P_NpToTask");
        }

        public bool PlanToTask(string PlanId)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            hT.Add("@PlanId", PlanId);
            hT.Add("@UserName", User.UserName);
            return access.RunProcedureNoReturn(hT, "P_PlanToTask");
        }

        public DataSet PrintTaskList(string TaskId)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            new Hashtable();
            DataSet ds = new DataSet();
            access.Run_SqlText(ref ds, "select * from V_Task Where TaskId='" + TaskId.Trim() + "'", "V_Task");
            access.Run_SqlText(ref ds, "select * from V_Np Where TaskId='" + TaskId.Trim() + "' Order By NpNo ", "V_Np");
            return ds;
        }
    }
}

