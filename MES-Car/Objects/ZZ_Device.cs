using System;
using System.Collections.Generic;
using System.Text;
using CWData;

namespace MES_Car.Objects
{
    public class ZZ_Device : Entity
    {
        public ZZ_Device()
        {
            _TableName = "ZZ_Device";
            _ColumnId = "DNO";
        }
        public static List<ZZ_Device> GetAllDevice()
        {
            string sql = "select * from ZZ_Device where isnull(Enabled,1)=1 order by DNO";
            return GetEntityList<ZZ_Device>(sql, null);
        }
        const string sql1 = "select top 1 * from T_Task where IsExe=0  and isnull(DNO,'')='{0}' and TaskID<>''  order by TaskId Asc";
        public T_Task GetNextTask()
        {
            string sql = string.Format(sql1, this.DNO);
            return GetEntity<T_Task>(sql, null);

        }
        public string DNO
        {
            get
            {
                return base.GetProperty("DNO", "");
            }
            set
            {
                base.SetValue("DNO", value);
            }
        }
        public string Name
        {
            get
            {
                return base.GetProperty("Name", "");
            }
            set
            {
                base.SetValue("Name", value);
            }
        }
        public int State
        {
            get
            {
                return base.GetProperty("State", 0);
            }
            set
            {
                base.SetValue("State", value);
            }
        }
        public string OnCode
        {
            get
            {
                return base.GetProperty("OnCode", "");
            }
            set
            {
                base.SetValue("OnCode", value);
            }
        }
        public string OffCode
        {
            get
            {
                return base.GetProperty("OffCode", "");
            }
            set
            {
                base.SetValue("OffCode", value);
            }
        }
        public string IP
        {
            get
            {
                return base.GetProperty("IP", "");
            }
            set
            {
                base.SetValue("IP", value);
            }
        }
        public bool Enabled
        {
            get
            {
                return base.GetProperty("Enabled", true);
            }
            set
            {
                base.SetValue("Enabled", value);
            }
        }
    }
}
