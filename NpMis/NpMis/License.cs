namespace NpMis
{
    using Common;
    using System;
    using System.Collections;
    using System.Data;

    internal class License
    {
        private string m_LcType;
        private string m_LicenseNo;

        public DataSet GetToDoNp(string PlanId)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            DataSet dataset = new DataSet();
            hT.Add("@PlanId", PlanId.Trim());
            access.RunProcedure(ref dataset, hT, "P_GetToDoNp");
            return dataset;
        }

        public string LcNo
        {
            get
            {
                return this.m_LicenseNo;
            }
            set
            {
                this.m_LicenseNo = value;
            }
        }

        public string LcType
        {
            get
            {
                return this.m_LcType;
            }
            set
            {
                this.m_LcType = value;
            }
        }
    }
}

