using System;
using System.Collections.Generic;
using System.Text;

using ICSharpCode.Core;
using AttnObjects;

namespace MES_Car
{

    public class LoginUser : ILoginUser
    {
        PSWD _pswd;
        public LoginUser(PSWD pswd)
        {
            _pswd = pswd;
        }
        #region ILoginUser 成员

        public string USR
        {
            get { return _pswd.USR; }
        }

        public string Name
        {
            get { return _pswd.NAME; }
        }
        public string CompNo
        {
            get { return _pswd.COMPNO; }
        }
        public bool IsCust
        {
            get { return false; }
        }
        #endregion
    }
}
