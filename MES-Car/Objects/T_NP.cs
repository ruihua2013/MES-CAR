using System;
using System.Collections.Generic;
using System.Text;
using CWData;

namespace MES_Car.Objects
{
    public class T_NP : Entity
    {
        public T_NP()
        {
            _TableName = "T_NP";
            _ColumnId = "NpId";
            doit = 0;
        }
        //public static List<ZZ_Device> GetAllDevice()
        //{
        //    string sql = "select * from ZZ_Device order by DNO";
        //    return GetEntityList<ZZ_Device>(sql, null);
        //}
        // NpId, , SendID, TaskID, PlanID, PlanID1, NPType, BarCode, FrontPiece, BackPiece, Mail, IsExe
        const string sql1 = "update T_NP set IsExe=1 where NpId='{0}'";

        int doit = 0;

        public bool SetExeOver()
        {
            string sql = string.Format(sql1, this.NpId);
            if (NPType == "A" || NPType == "G")
            {
                DbServer.ExecuteSQL(sql, null);
                return true;
            }
            else
            { 
                doit++;
                if (doit == 2)
                {
                    DbServer.ExecuteSQL(sql, null);
                    return true;
                }
            }
            return false;
        }
        const string sql2 = "select top 1 * from T_NP where TaskID='{0}' and isnull(IsExe,0)=0  and NpNo<>'{1}'";
        public T_NP GetNextTwoNp()
        {
            string sql = string.Format(sql2, this.TaskID,this.NpNo);
            return GetEntity<T_NP>(sql, null);
        }
        public int NpId
        {
            get
            {
                return base.GetProperty("NpId", 0);
            }
            set
            {
                base.SetValue("NpId", value);
            }
        }
        public string NpNo
        {
            get
            {
                return base.GetProperty("NpNo", "");
            }
            set
            {
                base.SetValue("NpNo", value);
            }
        }
        public string TaskID
        {
            get
            {
                return base.GetProperty("TaskID", "");
            }
            set
            {
                base.SetValue("TaskID", value);
            }
        }
        public string PlanID
        {
            get
            {
                return base.GetProperty("PlanID", "");
            }
            set
            {
                base.SetValue("PlanID", value);
            }
        }
        public string NPType
        {
            get
            {
                return base.GetProperty("NPType", "");
            }
            set
            {
                base.SetValue("NPType", value);
            }
        }

        public bool IsExe
        {
            get
            {
                return base.GetProperty("IsExe", false);
            }
            set
            {
                base.SetValue("IsExe", value);
            }
        }
        
    }
}
