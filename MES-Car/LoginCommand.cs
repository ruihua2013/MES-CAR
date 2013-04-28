using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using AttnERP;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.Core;

namespace AttnERP.Commands
{
    public class LoginCommand : AbstractMenuCommand
    {
        static bool isLogin = false;
        public override void Run()
        {
            Fm_Login fm = new Fm_Login();
            if (fm.ShowDialog() != DialogResult.OK)
            {

                try
                {
                    if (!isLogin)
                    {
                        System.Environment.Exit(0);
                    }
                }
                catch { }
                return;

            }
            if (isLogin)
            {

                WorkbenchSingleton.Workbench.CloseAllViews();
                WorkbenchSingleton.Workbench.RedrawAllComponents();
                //IdeModViewSingleton.IdeModView.Show(Main.Gui.Pads.IdeModPad.IdeModPad.NavBarControl.IdeModState);
                //ShowStartPageCommand startpage = new ShowStartPageCommand();
                //startpage.Run();
            }

            StatusBarService.SetLoginUser(AttnObjects.PSWD.LoginUser.LoginComp.NAME + "   " + AttnObjects.PSWD.LoginUser.NAME);
            isLogin = true;
        }
    }
}
