using System;
using System.Collections.Generic;

using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpMain;
using AttnObjects;

namespace test
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture =
new System.Globalization.CultureInfo("zh-CHS");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string configDirectory = Application.StartupPath + "\\data\\options"; ;
            string dataDirectory = Application.StartupPath + "\\data";
            string propertiesName = "SharpDevelopProperties";
            PropertyService.InitializeService(configDirectory,
                                  dataDirectory,
                                  propertiesName);
            PropertyService.Load();
            string constr = TbrERPTools.DecodeBase64(PropertyService.Get("sqlconfig", ""));
            //string constr = PropertyService.Get("sqlconfig", "");
            
            CWData.ServerFactory.GetServer().ConnectionString = constr;

           // AttnObjects.PSWD.LoginUser = AttnObjects.PSWD.GetById("FYED", "LAW");
           // AttnObjects.PSWD.LoginUser = AttnObjects.PSWD.GetById("DEMO", "LAW");
            //AttnObjects.PSWD.LoginCOMP = AttnObjects.COMP.GetById < COMP>("FYED");
            AttnObjects.PSWD.LoginUser = AttnObjects.PSWD.GetById("0000", "tcco");
            Application.EnableVisualStyles();
            DevExpress.Utils.AppearanceObject.DefaultFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            PropertyService.Save();
        }
    }
}
