namespace NpMis
{
    using Common;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Security.Cryptography;
    using System.Text;

    public class User
    {
        private PersonInfo[] m_PersonArr;
        private static string m_UserName;

        public User()
        {
        }

        public User(string UserName, string Password)
        {
        }

        public bool AddUser(string UserName, string Pwd, string TrueName, string Duty, string BarCode)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            hT.Add("@UserName", UserName);
            hT.Add("@Pwd", this.GetPwdHashCode(Pwd));
            hT.Add("@truename", TrueName);
            hT.Add("@duty", Duty);
            hT.Add("@BarCode", BarCode);
            if (!(access.RunProcedureNoRecord("@IsSuccess", SqlDbType.Int, hT, "P_AddUser") == "0"))
            {
                return false;
            }
            return true;
        }

        public bool DeleteUser(string UserName)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            hT.Add("@UserName", UserName);
            return access.RunProcedureNoReturn(hT, "P_DeleteUser");
        }

        public DataSet GetAllAuthority()
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            DataSet dataset = new DataSet();
            access.RunProcedure(ref dataset, hT, "P_GetAllAuthority");
            return dataset;
        }

        public DataSet GetAllUser()
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            DataSet dataset = new DataSet();
            access.RunProcedure(ref dataset, hT, "P_GetAllUser");
            return dataset;
        }

        public static List<PersonInfo> GetPersonInfoByBarcode(string Barcode)
        {
            List<PersonInfo> list2;
            try
            {
                Sql2KDataAccess access = new Sql2KDataAccess();
                List<PersonInfo> list = new List<PersonInfo>();
                DataSet set = access.Run_SqlText("select * from t_user where Barcode='" + Barcode + "'");
                if ((set != null) && (set.Tables[0].Rows.Count > 0))
                {
                    for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                    {
                        PersonInfo item = new PersonInfo {
                            UserName = set.Tables[0].Rows[i]["UserName"].ToString(),
                            TrueName = set.Tables[0].Rows[i]["TrueName"].ToString(),
                            BarCode = set.Tables[0].Rows[i]["BarCode"].ToString()
                        };
                        list.Add(item);
                    }
                }
                list2 = list;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list2;
        }

        public static List<PersonInfo> GetPersonInfoByTrueName(string TrueName)
        {
            List<PersonInfo> list2;
            try
            {
                Sql2KDataAccess access = new Sql2KDataAccess();
                List<PersonInfo> list = new List<PersonInfo>();
                DataSet set = access.Run_SqlText("select * from t_user where TrueName='" + TrueName + "'");
                if ((set != null) && (set.Tables[0].Rows.Count > 0))
                {
                    for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                    {
                        PersonInfo item = new PersonInfo {
                            UserName = set.Tables[0].Rows[i]["UserName"].ToString(),
                            TrueName = set.Tables[0].Rows[i]["TrueName"].ToString(),
                            BarCode = set.Tables[0].Rows[i]["BarCode"].ToString()
                        };
                        list.Add(item);
                    }
                }
                list2 = list;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list2;
        }

        public static List<PersonInfo> GetPersonInfoByUserName(string UserName)
        {
            List<PersonInfo> list2;
            try
            {
                Sql2KDataAccess access = new Sql2KDataAccess();
                List<PersonInfo> list = new List<PersonInfo>();
                DataSet set = access.Run_SqlText("select * from t_user where username='" + UserName + "'");
                if ((set != null) && (set.Tables[0].Rows.Count > 0))
                {
                    for (int i = 0; i < set.Tables[0].Rows.Count; i++)
                    {
                        PersonInfo item = new PersonInfo {
                            UserName = set.Tables[0].Rows[i]["UserName"].ToString(),
                            TrueName = set.Tables[0].Rows[i]["TrueName"].ToString(),
                            BarCode = set.Tables[0].Rows[i]["BarCode"].ToString()
                        };
                        list.Add(item);
                    }
                }
                list2 = list;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return list2;
        }

        private string GetPwdHashCode(string tmpPassword)
        {
            MD5 md = MD5.Create();
            byte[] inArray = md.ComputeHash(Encoding.Unicode.GetBytes(tmpPassword));
            md.Clear();
            return Convert.ToBase64String(inArray);
        }

        public DataSet GetUserAllAuthority(string UserName)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            DataSet dataset = new DataSet();
            hT.Add("@UserName", UserName);
            access.RunProcedure(ref dataset, hT, "P_GetUserAuthority");
            return dataset;
        }

        public static bool IsHaveAuthority(string AuthorityName)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            hT.Add("@UserName ", m_UserName);
            hT.Add("@AutorityName ", AuthorityName);
            if (!(access.RunProcedureNoRecord("@IsHave", SqlDbType.Int, hT, "P_CheckAuthority") == "1"))
            {
                return false;
            }
            return true;
        }

        public bool ModiPwd(string NewPwd)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            hT.Add("@UserName", UserName);
            hT.Add("@NewPwd", this.GetPwdHashCode(NewPwd));
            return access.RunProcedureNoReturn(hT, "P_ModiPwd");
        }

        public bool ModiUser(string UserName, string Pwd, string TrueName, string Duty, string BarCode)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            hT.Add("@UserName", UserName);
            hT.Add("@truename", TrueName);
            hT.Add("@Pwd", this.GetPwdHashCode(Pwd));
            hT.Add("@duty", Duty);
            hT.Add("@BarCode", BarCode);
            return access.RunProcedureNoReturn(hT, "P_ModiUser");
        }

        public bool UpdateUserAuthority(string UserName, string AuthorityCode, bool IsDelete)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            hT.Add("@UserName", UserName);
            hT.Add("@Code", AuthorityCode.Trim());
            hT.Add("@IsDelete", IsDelete ? 1 : 0);
            return access.RunProcedureNoReturn(hT, "P_UpdateUserAuthority");
        }

        public int UserLogin(string UserName, string Pwd)
        {
            Sql2KDataAccess access = new Sql2KDataAccess();
            Hashtable hT = new Hashtable();
            hT.Add("@UserName", UserName);
            hT.Add("@Pwd", this.GetPwdHashCode(Pwd));
            string s = access.RunProcedureNoRecord("result", SqlDbType.VarChar, hT, "P_UserLogin");
            if (s == "1")
            {
                m_UserName = UserName.Trim();
            }
            return int.Parse(s);
        }

        public static string UserName
        {
            get
            {
                return m_UserName;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PersonInfo
        {
            public string BarCode;
            public string UserName;
            public string TrueName;
        }
    }
}

