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

        #region 地图名称
        public const string TownshipTableName = "sde.SDE.乡镇街道";
        public const string TownshipCodeName = "XZDM";

        public const string VillageCommitteeTableName = "sde.SDE.村委会";
        public const string VillageCommitteeCodeName = "CWHDM";

        public const string VillageTableName = "sde.SDE.自然村";
        public const string VillageCodeName = "ZRCDM";

        public static string[] MapTables = new string[] { TownshipTableName, VillageCommitteeTableName, VillageTableName };
        #endregion
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
