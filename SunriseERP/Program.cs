using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Sunrise.ERP.Lang;

namespace SunriseERP
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AutoUpdate.AppUpdater au = new AutoUpdate.AppUpdater();
            if (au.CheckForUpdate() > 0)
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"\AutoUpdate.exe", "FromERP");
            }
            else
            {
                LangCenter.Instance.LoadLangXmlDocument(System.Configuration.ConfigurationManager.AppSettings["Lang"]);
                Application.Run(new frmSysInit());
            }
            //Sunrise.ERP.Module.SystemManage.frmsysPlatformManage frm = new Sunrise.ERP.Module.SystemManage.frmsysPlatformManage(5004, "Test");
            //frm.ReportNo = "R001";
            //Application.Run(frm);
        }
    }
}
