namespace NpMis
{
    using Common;
    using System;
    using System.Collections;
    using System.Data;

    internal class Invoice
    {
        public DataSet GetSendList()
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            DataSet dataset = new DataSet();
            access.RunProcedure(ref dataset, hT, "P_GetNewSendList");
            return dataset;
        }

        public static string GetSendListPrinter()
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            new DataSet();
            Hashtable hT = new Hashtable();
            hT.Add("@SetKey", "SendListPrinter");
            hT.Add("@UserName", User.UserName);
            return access.RunProcedureNoRecord("@SetValue", SqlDbType.VarChar, hT, "P_GetSet");
        }

        public static string GetSendPrinter()
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            new DataSet();
            Hashtable hT = new Hashtable();
            hT.Add("@SetKey", "SendPrinter");
            hT.Add("@UserName", User.UserName);
            return access.RunProcedureNoRecord("@SetValue", SqlDbType.VarChar, hT, "P_GetSet");
        }

        public bool NewSend(string TaskStr)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            hT.Add("@TaskIDStr", TaskStr.Trim());
            hT.Add("@UserName", User.UserName);
            return access.RunProcedureNoReturn(hT, "P_CreateNewSend");
        }

        public bool NewSendList(string TaskId)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            hT.Add("@TaskId", TaskId.Trim());
            hT.Add("@UserName", User.UserName);
            return access.RunProcedureNoReturn(hT, "P_OutStorage");
        }

        public DataSet PrintSendList(string SendId)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            new Hashtable();
            DataSet ds = new DataSet();
            access.Run_SqlText(ref ds, "select * from V_SendList Where SendId='" + SendId.Trim() + "'", "V_SendList");
            access.Run_SqlText(ref ds, "select * from V_List Where SendId='" + SendId.Trim() + "'", "V_List");
            access.Run_SqlText(ref ds, "select * from V_Np Where SendId='" + SendId.Trim() + "'", "V_Np");
            return ds;
        }

        public static bool SaveSendListPrinter(string SendListPrinter)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            hT.Add("@SetKey", "SendListPrinter");
            hT.Add("@SetValue", SendListPrinter);
            hT.Add("@UserName", User.UserName);
            return access.RunProcedureNoReturn(hT, "P_SaveSet");
        }

        public static bool SaveSendPrinter(string SendPrinter)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            hT.Add("@SetKey", "SendPrinter");
            hT.Add("@SetValue", SendPrinter);
            hT.Add("@UserName", User.UserName);
            return access.RunProcedureNoReturn(hT, "P_SaveSet");
        }

        public bool SignInSendList(string SendId, string ReceivePerson)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            hT.Add("@SendId", SendId.Trim());
            hT.Add("@ReceivePerson", ReceivePerson.Trim());
            return access.RunProcedureNoReturn(hT, "P_SignInSendList");
        }
    }
}

