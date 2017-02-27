using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using JXDL.ClientBusiness;
using System.Text;

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
        public const string TownshipTableName = "乡镇街道";
        public const string TownshipCodeName = "XZDM";

        public const string VillageCommitteeTableName = "村委会";
        public const string VillageCommitteeCodeName = "CWHDM";

        public const string VillageTableName = "自然村";
        public const string VillageCodeName = "ZRCDM";

        public const string FCAreaTableName = "丰城市市界";
        public const string TownshipAreaTableName = "乡镇街道界线";

        public static string[] MapTables = new string[] { TownshipTableName, VillageCommitteeTableName, VillageTableName, FCAreaTableName, TownshipAreaTableName };
        #endregion

        #region 地图图层显示比例尺
        //乡镇街道
        public static double Township_MaximumScale = 200000;
        public static double Township_MinimumScale = 0;

        public static double Township_Annotation_MaximumScale = 200000;
        public static double Township_Annotation_MinimumScale = 0;

        //村委会
        public static double VillageCommittee_MaximumScale = 0;
        public static double VillageCommittee_MinimumScale = 190000;

        public static double VillageCommittee_Annotation_MaximumScale = 0;
        public static double VillageCommittee_Annotation_MinimumScale = 150000;

        //自然村
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

                // 设置应用程序处理异常方式：ThreadException处理
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常  
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                //处理非UI线程异常  
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

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

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.Exception, e.ToString());
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //LogManager.WriteLog(str);  
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = GetExceptionMsg(e.ExceptionObject as Exception, e.ToString());
            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //LogManager.WriteLog(str);  
        }

        /// <summary>  
        /// 生成自定义异常消息  
        /// </summary>  
        /// <param name="ex">异常对象</param>  
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>  
        /// <returns>异常字符串文本</returns>  
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                //sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }
    }
}
