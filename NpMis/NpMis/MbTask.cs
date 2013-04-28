namespace NpMis
{
    using Common;
    using System;
    using System.Collections;
    using System.Data;

    internal class MbTask
    {
        public int GetBoxCapability()
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            new DataSet();
            Hashtable hT = new Hashtable();
            hT.Add("@SetKey", "MbBoxCapability");
            hT.Add("@UserName", User.UserName);
            return int.Parse(access.RunProcedureNoRecord("@SetValue", SqlDbType.VarChar, hT, "P_GetSet"));
        }

        public DataSet GetNewTask()
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            DataSet dataset = new DataSet();
            Hashtable hT = new Hashtable();
            access.RunProcedure(ref dataset, hT, "P_GetNewMbTask");
            return dataset;
        }

        public DataSet GetToDoPan(string MbType, string WProcedure)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            DataSet dataset = new DataSet();
            hT.Add("@MbType", MbType);
            hT.Add("@CurrentProcedure", WProcedure);
            access.RunProcedure(ref dataset, hT, "P_GetToDoPan");
            return dataset;
        }

        public string NewTask(string MbType, long MbAmount, string WProcedure)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            hT.Add("@UserName", User.UserName);
            hT.Add("@MbType", MbType);
            hT.Add("@MbAmount", MbAmount);
            hT.Add("@WProcedure", WProcedure);
            return access.RunProcedureNoRecord("@MbTaskId", SqlDbType.VarChar, hT, "P_NewMbTask");
        }

        public bool SaveBoxCapability(int Capability)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            hT.Add("@SetKey", "MbBoxCapability");
            hT.Add("@SetValue", Capability);
            hT.Add("@UserName", User.UserName);
            return access.RunProcedureNoReturn(hT, "P_SaveSet");
        }
    }
}

