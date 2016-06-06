using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JXDL.ClientBusiness;
using ESRI.ArcGIS.GISClient;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;

namespace JXDL.Client
{
    public partial class MainForm : Form
    {
        Timer m_HeartbeatTimer = null;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoginForm vLoginForm = new LoginForm();
                if (vLoginForm.ShowDialog() == DialogResult.OK)
                {
                    Program.LoginUserInfo = vLoginForm.LoginUserInfo;
                    RemoteInterface vRemoteInterface = new RemoteInterface();
                    vRemoteInterface.GetMapServer(ref Program.MapServerAddress, ref Program.MapServerName);
                    Text = string.Format("新农村建设地理信息系统 【当前用户:{0} 所属机构:{1}】", Program.LoginUserInfo.UserName, getPowerName(Program.LoginUserInfo.Power.Value));
                    init_Menu();
                    init_Heartbeat();
                    init_Map();
                }
                else
                {
                    Application.Exit();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        string getPowerName( int Power)
        {
            string vPowerName = "";
            switch(Power)
            {
                case 1:
                    vPowerName = "村民";
                    break;
                case 2:
                    vPowerName = "村委会";
                    break;
                case 3:
                    vPowerName = "政府及城建部门";
                    break;
            }
            return vPowerName;
        }

        void init_Map()
        {
            IAGSServerObjectName pServerObjectName = GetMapServer(Program.MapServerAddress, Program.MapServerName, false);
            IName pName = (IName)pServerObjectName;
            //访问地图服务
            IAGSServerObject pServerObject = (IAGSServerObject)pName.Open();
            IMapServer pMapServer = (IMapServer)pServerObject;

            ESRI.ArcGIS.Carto.IMapServerLayer pMapServerLayer = new MapServerLayerClass();
            //连接地图服务

            pMapServerLayer.ServerConnect(pServerObjectName, pMapServer.DefaultMapName);
            //添加数据图层

            axMapControl1.AddLayer(pMapServerLayer as ILayer);

            axMapControl1.Extent = axMapControl1.FullExtent;
            axMapControl1.Refresh();
        }

        public IAGSServerObjectName GetMapServer(string pHostOrUrl, string pServiceName, bool pIsLAN)
        {


            //设置连接属性
            IPropertySet pPropertySet = new PropertySetClass();
            if (pIsLAN)
                pPropertySet.SetProperty("machine", pHostOrUrl);
            else
                pPropertySet.SetProperty("url", pHostOrUrl);

            //打开连接

            IAGSServerConnectionFactory pFactory = new AGSServerConnectionFactory();
            //Type factoryType = Type.GetTypeFromProgID(
            //    "esriGISClient.AGSServerConnectionFactory");
            //IAGSServerConnectionFactory agsFactory = (IAGSServerConnectionFactory)
            //    Activator.CreateInstance(factoryType);
            IAGSServerConnection pConnection = pFactory.Open(pPropertySet, 0);

            //Get the image server.
            IAGSEnumServerObjectName pServerObjectNames = pConnection.ServerObjectNames;
            pServerObjectNames.Reset();
            IAGSServerObjectName ServerObjectName = pServerObjectNames.Next();
            while (ServerObjectName != null)
            {
                if ((ServerObjectName.Name.ToLower() == pServiceName.ToLower()) &&
                    (ServerObjectName.Type == "MapServer"))
                {

                    break;
                }
                ServerObjectName = pServerObjectNames.Next();
            }

            //返回对象
            return ServerObjectName;
        }

        void init_Menu()
        {
            switch( Program.LoginUserInfo.Power)
            {
                case 1:
                    ToolStripMenuItem_System_Setup.Enabled = true;
                    ToolStripMenuItem_System_Logout.Enabled = true;
                    ToolStripMenuItem_System_Exit.Enabled = true;

                    ToolStripMenuItem_Doc_Edit.Enabled = false;
                    ToolStripMenuItem_Doc_Input.Enabled = false;
                    ToolStripMenuItem_Doc_Query.Enabled = true;
                    ToolStripMenuItem_Doc_Report.Enabled = true;
                    ToolStripMenuItem_Doc_Setup.Enabled = false;

                    ToolStripMenuItem_Pic_Anayle.Enabled = false;
                    ToolStripMenuItem_Pic_Browse.Enabled = true;
                    ToolStripMenuItem_Pic_Layer.Enabled = false;
                    ToolStripMenuItem_Pic_Map.Enabled = true;
                    ToolStripMenuItem_Pic_Statistics.Enabled = true;
                    break;
                case 2:
                    ToolStripMenuItem_System_Setup.Enabled = true;
                    ToolStripMenuItem_System_Logout.Enabled = true;
                    ToolStripMenuItem_System_Exit.Enabled = true;

                    ToolStripMenuItem_Doc_Edit.Enabled = true;
                    ToolStripMenuItem_Doc_Input.Enabled = true;
                    ToolStripMenuItem_Doc_Query.Enabled = true;
                    ToolStripMenuItem_Doc_Report.Enabled = true;
                    ToolStripMenuItem_Doc_Setup.Enabled = true;

                    
                    ToolStripMenuItem_Pic_Browse.Enabled = true;
                    ToolStripMenuItem_Pic_Layer.Enabled = false;
                    ToolStripMenuItem_Pic_Map.Enabled = false;
                    ToolStripMenuItem_Pic_Anayle.Enabled = true;
                    ToolStripMenuItem_Pic_Statistics.Enabled = true;
                    break;
                case 3:
                    ToolStripMenuItem_System_Setup.Enabled = true;
                    ToolStripMenuItem_System_Logout.Enabled = true;
                    ToolStripMenuItem_System_Exit.Enabled = true;

                    ToolStripMenuItem_Doc_Edit.Enabled = true;
                    ToolStripMenuItem_Doc_Input.Enabled = true;
                    ToolStripMenuItem_Doc_Query.Enabled = true;
                    ToolStripMenuItem_Doc_Report.Enabled = true;
                    ToolStripMenuItem_Doc_Setup.Enabled = true;

                    ToolStripMenuItem_Pic_Anayle.Enabled = true;
                    ToolStripMenuItem_Pic_Browse.Enabled = true;
                    ToolStripMenuItem_Pic_Layer.Enabled = true;
                    ToolStripMenuItem_Pic_Map.Enabled = true;
                    ToolStripMenuItem_Pic_Statistics.Enabled = true;
                    break;
            }
        }

        void init_Heartbeat()
        {
            m_HeartbeatTimer = new Timer();
            m_HeartbeatTimer.Interval = 60000;
            m_HeartbeatTimer.Tick += M_HeartbeatTimer_Tick;
            m_HeartbeatTimer.Start();
        }

        private void M_HeartbeatTimer_Tick(object sender, EventArgs e)
        {
            RemoteInterface vRemoteInterface = new RemoteInterface();
            vRemoteInterface.Heartbeat(Program.LoginUserInfo);
        }

        private void ToolStripMenuItem_System_Setup_Click(object sender, EventArgs e)
        {
            ConfigForm vConfigForm = new ConfigForm();
            vConfigForm.ShowDialog();
        }

        private void ToolStripMenuItem_System_Exit_Click(object sender, EventArgs e)
        {
            if ( MessageBox.Show("是否确认退出?","信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK )
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        void exit()
        {
            RemoteInterface vRemoteInterface = new RemoteInterface();
            vRemoteInterface.Logout(Program.LoginUserInfo.UserName, Program.LoginUserInfo.Token);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            exit();
        }

        private void ToolStripMenuItem_System_Logout_Click(object sender, EventArgs e)
        {
            LoginForm vLoginForm = new LoginForm();
            vLoginForm.button_Config.Visible = false;
            vLoginForm.button_Login.Left = vLoginForm.button_Login.Left + 80;
            if (vLoginForm.ShowDialog() == DialogResult.OK)
            {
                Program.LoginUserInfo = vLoginForm.LoginUserInfo;
                Text = string.Format("新农村建设地理信息系统 【当前用户:{0} 所属机构:{1}】", Program.LoginUserInfo.UserName, getPowerName(Program.LoginUserInfo.Power.Value));
                init_Menu();
            }
        }
    }
}
