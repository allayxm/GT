using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using JXDL.ClientBusiness;

namespace JXDL.Client
{
    static class Program
    {
        public static UserInfo LoginUserInfo;

        #region 地图数据库参数
        public static string MapDBAddress;
        public static string MapDBName;
        public static string MapDBUserName;
        public static string MapDBPassword;
        #endregion

        public static string[] MapTables = new string[] { "sde.SDE.乡镇街道", "sde.SDE.村委会", "sde.SDE.自然村" };

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
                Application.Run(new MainForm());
            }
            catch( Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
