﻿using System;
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

        public const string VillageTableName = "sde.SDE.自然村面";
        public const string VillageCodeName = "ZRCDM";

        public static string[] MapTables = new string[] { TownshipTableName, VillageCommitteeTableName, VillageTableName };
        #endregion

        #region 地图图层显示比例尺
        public static double Township_MaximumScale = 200000;
        public static double Township_MinimumScale = 0;
        public static double Township_Annotation_MaximumScale = 200000;
        public static double Township_Annotation_MinimumScale = 0;


        public static double VillageCommittee_MaximumScale = 0;
        public static double VillageCommittee_MinimumScale = 190000;
        public static double VillageCommittee_Annotation_MaximumScale = 125000;
        public static double VillageCommittee_Annotation_MinimumScale = 150000;

        public static double Village_MaximumScale = 0;
        public static double Village_MinimumScale = 100000;
        public static double Village_Annotation_MaximumScale = 0;
        public static double Village_Annotation_MinimumScale = 100000;
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
