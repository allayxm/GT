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
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Display;

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
                    ConfigFile vConfigFile = new ConfigFile();
                    Program.LoginUserInfo = vLoginForm.LoginUserInfo;
                    RemoteInterface vRemoteInterface = new RemoteInterface();
                    vRemoteInterface.GetMapServer(ref Program.MapDBAddress, ref Program.MapDBName,ref Program.MapDBUserName,ref Program.MapDBPassword);
                    Text = string.Format("新农村建设地理信息系统 【当前用户:{0} 所属机构:{1}】", Program.LoginUserInfo.UserName, getPowerName(Program.LoginUserInfo.Power.Value));
                    init_Menu();
                    init_Heartbeat();
                    init_Map(vConfigFile.MapBackgroundColor);
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


        void test()
        {
            
        }
        //void init_Map(int background)
        //{
        //    IAGSServerObjectName pServerObjectName = GetMapServer(Program.MapDBAddress, Program.MapDBName, false);
        //    IName pName = (IName)pServerObjectName;
        //    //访问地图服务
        //    IAGSServerObject pServerObject = (IAGSServerObject)pName.Open();
        //    IMapServer pMapServer = (IMapServer)pServerObject;

        //    //测试代码
        //    IMapServerInfo pMapServerInfo = new MapServerInfoClass();
        //    pMapServerInfo = pMapServer.GetServerInfo(pMapServer.DefaultMapName);
        //    IMapLayerInfos pMapLayerInfos = new MapLayerInfosClass();
        //    pMapLayerInfos = pMapServerInfo.MapLayerInfos;
        //    IMapLayerInfo pMapLayerInfo = new MapLayerInfoClass();
        //    for (int i = 0; i < pMapLayerInfos.Count; i++)

        //    {

        //        //定位到具体的一个图层   
        //        pMapLayerInfo = pMapLayerInfos.get_Element(i);
        //        string name = pMapLayerInfo.Name;
        //        int count = pMapLayerInfo.Fields.FieldCount;
        //        //ILayer pLayer1 = pMapLayerInfo as ILayer;
        //        //axMapControl1.AddLayer(pLayer1);
        //    }




        //    ESRI.ArcGIS.Carto.IMapServerLayer pMapServerLayer = new MapServerLayerClass();
        //    //连接地图服务

        //    pMapServerLayer.ServerConnect(pServerObjectName, pMapServer.DefaultMapName);



        //    //获取图层(测试代码)
        //    //IMapDescription pMapDesc = pMapServerInfo.DefaultMapDescription;

        //    //ImageDisplay pImageDisp = new ImageDisplayClass();

        //    //pImageDisp.Height = 400;

        //    //pImageDisp.Width = 400;



        //    //IMapServerFindResults pMapServerFindResults = pMapServer.Find(pMapDesc, null, "", true, "", esriFindOption.esriFindAllLayers, null);

        //    //int t = pMapServerFindResults.Count;



        //    //IMapServerObjects pMapServerObjs = pMapServer as IMapServerObjects;

        //    //IMap pMap = pMapServerObjs.get_Map(pMapServer.DefaultMapName);

        //    //IFeatureLayer featl = pMap.get_Layer(0) as IFeatureLayer;



        //    ICompositeLayer2 pCompositeLayer = pMapServerLayer as ICompositeLayer2;
        //    for (int i = 0; i < pCompositeLayer.Count; i++)

        //    {

        //        ILayer pLayer = pCompositeLayer.get_Layer(i);
        //        axMapControl1.AddLayer(pLayer);
                
        //    }
        //    //添加数据图层

        //    axMapControl1.AddLayer(pMapServerLayer as ILayer);
        //    axMapControl1.Extent = axMapControl1.FullExtent;
        //    axMapControl1.BackColor = Color.FromArgb(background);
        //    axMapControl1.Refresh();
        //}

        //IMapServer pMapServer = null;
        //public IAGSServerObjectName GetMapServer(string pHostOrUrl, string pServiceName, bool pIsLAN)
        //{
        //    //设置连接属性
        //    IPropertySet pPropertySet = new PropertySetClass();
        //    if (pIsLAN)
        //        pPropertySet.SetProperty("machine", pHostOrUrl);
        //    else
        //        pPropertySet.SetProperty("url", pHostOrUrl);

        //    //打开连接

        //    IAGSServerConnectionFactory pFactory = new AGSServerConnectionFactory();
        //    //Type factoryType = Type.GetTypeFromProgID(
        //    //    "esriGISClient.AGSServerConnectionFactory");
        //    //IAGSServerConnectionFactory agsFactory = (IAGSServerConnectionFactory)
        //    //    Activator.CreateInstance(factoryType);
        //    IAGSServerConnection pConnection = pFactory.Open(pPropertySet, 0);

        //    //Get the image server.
        //    IAGSEnumServerObjectName pServerObjectNames = pConnection.ServerObjectNames;
        //    pServerObjectNames.Reset();
        //    IAGSServerObjectName ServerObjectName = pServerObjectNames.Next();
        //    while (ServerObjectName != null)
        //    {
        //        if ((ServerObjectName.Name.ToLower() == pServiceName.ToLower()) &&
        //            (ServerObjectName.Type == "MapServer"))
        //        {

        //            break;
        //        }

        //        //if (ServerObjectName.Name == "AllVector")

        //        //{

        //        //    IName pName = ServerObjectName as IName;

        //        //    pMapServer = pName.Open() as IMapServer;

        //        //    break;

        //        //}
        //        ServerObjectName = pServerObjectNames.Next();
        //    }

        //    //返回对象
        //    return ServerObjectName;
        //}

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
            SystemConfigForm vSystemConfigForm = new SystemConfigForm();
            if ( vSystemConfigForm.ShowDialog() == DialogResult.OK )
            {
                axMapControl1.BackColor = Color.FromArgb(vSystemConfigForm.SelectColor);
            }
            vSystemConfigForm.Close();
            vSystemConfigForm.Dispose();
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


        private void init_Map( int background)
        {
            IWorkspaceFactory vWorkspaceFactory = new SdeWorkspaceFactoryClass();
            
            IPropertySet vPropSet = new PropertySetClass();

            vPropSet.SetProperty("SERVER", Program.MapDBAddress);
            vPropSet.SetProperty("INSTANCE",string.Format( @"{0}:sqlserver:{1}",Program.MapDBName,Program.MapDBAddress));
            vPropSet.SetProperty("DATABASE", Program.MapDBName);
            vPropSet.SetProperty("USER", Program.MapDBUserName);
            vPropSet.SetProperty("PASSWORD", Program.MapDBPassword);
            vPropSet.SetProperty("VERSION", "SDE.DEFAULT");
            vPropSet.SetProperty("AUTHENTICATION_MODE", "DBMS");

            axMapControl1.ClearLayers();
            IWorkspace vWorkspace = vWorkspaceFactory.Open(vPropSet, 0);
            IFeatureWorkspace vFeatWS = vWorkspace as IFeatureWorkspace;
            for( int i=0;i<Program.MapTables.Length;i++)
            {
                IFeatureClass vFeatureClass = vFeatWS.OpenFeatureClass(Program.MapTables[i]);
                if (vFeatureClass != null)
                {
                    IFeatureLayer vLayerFeature = new FeatureLayerClass();
                    vLayerFeature.FeatureClass = vFeatureClass;
                    vLayerFeature.Name = Program.MapTables[i];

                    //IGeoFeatureLayer geoFeatureLayer = (IGeoFeatureLayer)vLayerFeature;
                    //ISimpleRenderer simpleRenderer = (ISimpleRenderer)geoFeatureLayer.Renderer;
                    //IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
                    //pFillSymbol.Color = Color.Green;
                    //simpleRenderer.Symbol = (ISymbol)pFillSymbol;

                    axMapControl1.Map.AddLayer(vLayerFeature as ILayer);

                }
            }
            axMapControl1.BackColor = Color.FromArgb(background);
            axMapControl1.Extent = axMapControl1.FullExtent;
            axMapControl1.Refresh();

            axMapControl1.OnSelectionChanged += AxMapControl1_OnSelectionChanged;
            //IFeatureClass xjmFeatureClass = vFeatWS.OpenFeatureClass("县界面");
            //IFeatureLayer xjmLayerFeature = new FeatureLayerClass();
            //xjmLayerFeature.FeatureClass = xjmFeatureClass;

            //IFeatureClass cjmFeatureClass = vFeatWS.OpenFeatureClass("自然村面");
            //IFeatureLayer cjmLayerFeature = new FeatureLayerClass();
            //cjmLayerFeature.FeatureClass = cjmFeatureClass;

            //IFeatureClass zrcFeatureClass = vFeatWS.OpenFeatureClass("自然村点");
            //IFeatureLayer zrcLayerFeature = new FeatureLayerClass();
            //zrcLayerFeature.FeatureClass = zrcFeatureClass;


            //axMapControl1.Map.AddLayer(xjmLayerFeature as ILayer);
            //axMapControl1.Map.AddLayer(cjmLayerFeature as ILayer);
            //axMapControl1.Map.AddLayer(zrcLayerFeature as ILayer);

        }

        private void AxMapControl1_OnSelectionChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //MessageBox.Show("Is OK");
            //axMapControl1.Map.FeatureSelection.coun
            ISelection pSelection = axMapControl1.Map.FeatureSelection;
            IEnumFeatureSetup pEnumFeatureSetup = pSelection as IEnumFeatureSetup;
            pEnumFeatureSetup.AllFields = true;
            IEnumFeature pEnumFeature = pSelection as IEnumFeature;
            IFeature pFeature = pEnumFeature.Next();
            while (pFeature != null)
            {
                string vName = pFeature.Class.AliasName;
                Console.Write("Name:"+vName);
                Console.WriteLine(pFeature.get_Value(2));
                pFeature = pEnumFeature.Next();
            }
        }

        private void ToolStripMenuItem_About_Click(object sender, EventArgs e)
        {
            //OpenFileDialog vOpenFileDialog = new OpenFileDialog();
            //vOpenFileDialog.Multiselect = true;
            //if (  vOpenFileDialog.ShowDialog() == DialogResult.OK )
            //{
            //    string[] vFiles = vOpenFileDialog.FileNames;
            //    RemoteInterface vRemoteInterface = new RemoteInterface();
            //    vRemoteInterface.UploadFile(Program.LoginUserInfo.ID.Value, Program.LoginUserInfo.UserName, vFiles, new string[] { "Test1","Test2" }, new string[] { "12345","78945" });
            //}
            //SaveFileDialog vSaveFileDialog = new SaveFileDialog();
            //if ( vSaveFileDialog.ShowDialog()== DialogResult.OK )
            //{
            // RemoteInterface vRemoteInterface = new RemoteInterface();
            //if (  vRemoteInterface.DownloadFile(6, @"c:\\kwmusic2016_web_4.exe") )
            //{
            //    MessageBox.Show("下载成功");
            //}
            //}
            //int count = axMapControl1.LayerCount;
            //for( int i=0; i<count;i++)
            //{
            //    ILayer layer = axMapControl1.get_Layer(i);
            //    IFeatureLayer ifa = layer as IFeatureLayer;
            //    if (ifa != null)
            //        MessageBox.Show("OK");
            //}
            //OpenDB();
            
            //axMapControl1.BackColor = Color.Red;
        }

        private void axMapControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            //axMapControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            //ISelection selection;
            
            //ITool vTool=  axMapControl1.CurrentTool;
            //IGeometry geometry;
            //geometry = axMapControl1.TrackRectangle();
            //axMapControl1.Map.SelectByShape(geometry, null, false);
            //axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ILayer zrcLayer = null;
            for ( int i=0;i< axMapControl1.LayerCount;i++)
            {
                ILayer layer =  axMapControl1.get_Layer(i);
                if ( layer.Name == "sde.DBO.自然村点")
                {
                    zrcLayer = layer;
                    break;
                }
            }
            IFeatureLayer featureLayer = zrcLayer as IFeatureLayer;
            IFeatureClass featureClss = featureLayer.FeatureClass;
            IQueryFilter queryFilter = new QueryFilterClass();
            IFeatureCursor featureCursor;
            IFeature feature = null;
            queryFilter.WhereClause = ("RefName = '芳山'");
            featureCursor = featureClss.Search(queryFilter, true);
            feature = featureCursor.NextFeature();
            if (feature!=null )
            {
                axMapControl1.Map.SelectFeature(zrcLayer, feature);
                axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection,null,null);
            }
        }

        private void axToolbarControl1_MouseCaptureChanged(object sender, EventArgs e)
        {
            
        }

        private void axToolbarControl1_OnMouseUp(object sender, IToolbarControlEvents_OnMouseUpEvent e)
        {
            int i = e.button;
            
        }
    }
}
