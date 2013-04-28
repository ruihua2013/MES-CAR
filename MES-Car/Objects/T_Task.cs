using System;
using System.Collections.Generic;
using System.Text;
using CWData;

namespace MES_Car.Objects
{
    public class T_Task : Entity
    {
        public T_Task()
        {
            _TableName = "T_Task";
            _ColumnId = "TaskID";
        }
        //public static List<ZZ_Device> GetAllDevice()
        //{
        //    string sql = "select * from ZZ_Device order by DNO";
        //    return GetEntityList<ZZ_Device>(sql, null);
        //}
        // TaskTime,  IsInWarehouse, WithDrawU, PressU, EraseU, SmearU, DryingU, ClashU, ProcessPackU, PackU, SendU, WithDrawTime, PressTime, EraseTime, SmearTime, DryingTime, ClashTime, ProcessPackTime, PackTime, SendTime,  DNO
        const string sql1 = "select top 1 * from T_NP where TaskID='{0}' and isnull(IsExe,0)=0 ";
        public T_NP GetNextNp()
        {
            string sql = string.Format(sql1, this.TaskID);
            return GetEntity<T_NP>(sql, null);

        }
        const string sql2 = "update T_Task set IsExe=1 where TaskID='{0}'";
        public void SetExeOver()
        {
            string sql = string.Format(sql2, this.TaskID);
            DbServer.ExecuteSQL(sql, null);
        }
        const string sql3 = "select count(1) as cts from T_NP where TaskID='{0}'";
        public int GetCountNp()
        {
            string sql = string.Format(sql3, this.TaskID);
            return int.Parse(DbServer.GetFirstString(sql));
        }
        const string sql4 = "select count(1) as cts from T_NP where TaskID='{0}' and isnull(IsExe,0)=1";
        public int GetCountExeNp()
        {
            string sql = string.Format(sql4, this.TaskID);
            return int.Parse(DbServer.GetFirstString(sql));
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
        public string TaskUser
        {
            get
            {
                return base.GetProperty("TaskUser", "");
            }
            set
            {
                base.SetValue("TaskUser", value);
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
        public string TaskType
        {
            get
            {
                return base.GetProperty("TaskType", "");
            }
            set
            {
                base.SetValue("TaskType", value);
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
