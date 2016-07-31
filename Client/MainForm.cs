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
using JXDL.IntrefaceStruct;


namespace JXDL.Client
{
    public partial class MainForm : Form
    {
        Timer m_HeartbeatTimer = null;
        /// <summary>
        /// 乡镇及街道要素
        /// </summary>
        IFeatureLayer m_TownshipFeatureLayer = null;
        /// <summary>
        /// 村委会要素
        /// </summary>
        IFeatureLayer m_VillageCommitteeFeatureLayer = null;
        /// <summary>
        /// 自然村要素
        /// </summary>
        IFeatureLayer m_VillageFeatureLayer = null;

        /// <summary>
        /// 鹰眼要素
        /// </summary>
        IFeatureLayer m_EagleEyeFeatureClass = null;

        int m_ToolButtonIndex;

        LayerStruct[] m_Lyaers;
        /// <summary>
        /// 鹰眼对话框
        /// </summary>
        EagleEyeForm m_EagleEyeForm = null;
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
                    RemoteInterface vRemoteInterface = new RemoteInterface(Program.LoginUserInfo.ID.Value, Program.LoginUserInfo.UserName, Program.LoginUserInfo.Token);
                    vRemoteInterface.GetMapServer(ref Program.MapDBAddress, ref Program.MapDBName,ref Program.MapDBUserName,ref Program.MapDBPassword);
                    Text = string.Format("新农村建设地理信息系统 【当前用户:{0} 所属机构:{1}】", Program.LoginUserInfo.UserName, getPowerName(Program.LoginUserInfo.Power.Value));
                    init_Menu();
                    init_Heartbeat();
                    init_Map(vConfigFile.MapBackgroundColor,vConfigFile.TownshipBackgroundColor,vConfigFile.VillageCommitteeBackgroundColor,vConfigFile.VillageBackgroundColor);
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


        //void test()
        //{
        //    getTownshipDict();
        //}
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
                //村民
                case 1:
                    ToolStripMenuItem_System_Setup.Enabled = true;
                    ToolStripMenuItem_System_Logout.Enabled = true;
                    ToolStripMenuItem_System_Exit.Enabled = true;

                    ToolStripMenuItem_Doc_Edit.Enabled = false;
                    ToolStripMenuItem_Doc_Input.Enabled = false;
                    ToolStripMenuItem_Doc_Query.Enabled = true;
                    ToolStripMenuItem_Doc_Report.Enabled = true;

                    ToolStripMenuItem_Pic_Anayle.Enabled = false;
                    ToolStripMenuItem_Pic_Browse.Enabled = true;
                    ToolStripMenuItem_Pic_Layer.Enabled = false;
                    ToolStripMenuItem_Pic_Map.Enabled = true;
                    ToolStripMenuItem_Pic_Statistics.Enabled = true;
                    ToolStripMenuItem_EagleEye.Enabled = true;
                    break;
                //村委会
                case 2:
                    ToolStripMenuItem_System_Setup.Enabled = true;
                    ToolStripMenuItem_System_Logout.Enabled = true;
                    ToolStripMenuItem_System_Exit.Enabled = true;

                    ToolStripMenuItem_Doc_Edit.Enabled = true;
                    ToolStripMenuItem_Doc_Input.Enabled = true;
                    ToolStripMenuItem_Doc_Query.Enabled = false;
                    ToolStripMenuItem_Doc_Report.Enabled = true;

                    ToolStripMenuItem_Pic_Browse.Enabled = true;
                    ToolStripMenuItem_Pic_Layer.Enabled = false;
                    ToolStripMenuItem_Pic_Map.Enabled = false;
                    ToolStripMenuItem_Pic_Anayle.Enabled = true;
                    ToolStripMenuItem_Pic_Statistics.Enabled = true;
                    ToolStripMenuItem_EagleEye.Enabled = true;
                    break;
                //政府及城建部门
                case 3:
                    ToolStripMenuItem_System_Setup.Enabled = true;
                    ToolStripMenuItem_System_Logout.Enabled = true;
                    ToolStripMenuItem_System_Exit.Enabled = true;

                    ToolStripMenuItem_Doc_Edit.Enabled = true;
                    ToolStripMenuItem_Doc_Input.Enabled = true;
                    ToolStripMenuItem_Doc_Query.Enabled = false;
                    ToolStripMenuItem_Doc_Report.Enabled = true;

                    ToolStripMenuItem_Pic_Anayle.Enabled = true;
                    ToolStripMenuItem_Pic_Browse.Enabled = true;
                    ToolStripMenuItem_Pic_Layer.Enabled = true;
                    ToolStripMenuItem_Pic_Map.Enabled = true;
                    ToolStripMenuItem_Pic_Statistics.Enabled = true;
                    ToolStripMenuItem_EagleEye.Enabled = true;
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
            RemoteInterface vRemoteInterface = new RemoteInterface(Program.LoginUserInfo.ID.Value, Program.LoginUserInfo.UserName, Program.LoginUserInfo.Token);
            vRemoteInterface.Heartbeat(Program.LoginUserInfo);
        }

        private void ToolStripMenuItem_System_Setup_Click(object sender, EventArgs e)
        {
            ConfigFile vConfigFile = new ConfigFile();
            SystemConfigForm vSystemConfigForm = new SystemConfigForm();
            vSystemConfigForm.DownlaodPath = vConfigFile.DownloadPath;
            vSystemConfigForm.MapBackgroundColor = vConfigFile.MapBackgroundColor;
            vSystemConfigForm.TownshipBackgroundColor = vConfigFile.TownshipBackgroundColor;
            vSystemConfigForm.VillageCommitteeBackgroundColor = vConfigFile.VillageCommitteeBackgroundColor;
            vSystemConfigForm.VillageBackgroundColor = vConfigFile.VillageBackgroundColor;

            if ( vSystemConfigForm.ShowDialog() == DialogResult.OK )
            {
                vConfigFile.MapBackgroundColor = vSystemConfigForm.MapBackgroundColor;
                vConfigFile.TownshipBackgroundColor = vSystemConfigForm.TownshipBackgroundColor;
                vConfigFile.VillageCommitteeBackgroundColor = vSystemConfigForm.VillageCommitteeBackgroundColor;
                vConfigFile.VillageBackgroundColor = vSystemConfigForm.VillageBackgroundColor;
                vConfigFile.Save();
                changeMapColor(vConfigFile.MapBackgroundColor, vConfigFile.TownshipBackgroundColor,
                    vConfigFile.VillageCommitteeBackgroundColor, vConfigFile.VillageBackgroundColor);
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
            if (Program.LoginUserInfo.ID != null )
            {
                RemoteInterface vRemoteInterface = new RemoteInterface(Program.LoginUserInfo.ID.Value, Program.LoginUserInfo.UserName, Program.LoginUserInfo.Token);
                vRemoteInterface.Logout(Program.LoginUserInfo.UserName, Program.LoginUserInfo.Token);
            }
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


        private void init_Map( int background, int townshipBackgroundColor,
            int villageCommitteeBackgroundColor,int villageBackgroundColor)
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
            //初始化行政区域图层
            for( int i=0;i<Program.MapTables.Length;i++)
            {
                IFeatureClass vFeatureClass = vFeatWS.OpenFeatureClass(Program.MapTables[i]);
                if (Program.MapTables[i] == "sde.SDE.乡镇街道")
                {
                    m_EagleEyeFeatureClass = new FeatureLayerClass();
                    m_EagleEyeFeatureClass.FeatureClass = vFeatWS.OpenFeatureClass(Program.MapTables[i]);
                }
                if (vFeatureClass != null)
                {
                    IFeatureLayer vLayerFeature = new FeatureLayerClass();
                    vLayerFeature.FeatureClass = vFeatureClass;
                    vLayerFeature.Name = Program.MapTables[i];
                    axMapControl1.Map.AddLayer(vLayerFeature as ILayer);
                  
                    switch (Program.MapTables[i])
                    {
                        case "sde.SDE.乡镇街道":
                            m_TownshipFeatureLayer = vLayerFeature;
                            m_TownshipFeatureLayer.MaximumScale = Program.Township_MaximumScale;
                            m_TownshipFeatureLayer.MinimumScale = Program.Township_MinimumScale;
                            showAnnotationByScale(m_TownshipFeatureLayer, "街道", Program.Township_Annotation_MaximumScale, Program.Township_Annotation_MinimumScale);
                            break;
                        case "sde.SDE.村委会":
                            m_VillageCommitteeFeatureLayer = vLayerFeature;
                            m_VillageCommitteeFeatureLayer.MaximumScale = Program.VillageCommittee_MaximumScale;
                            m_VillageCommitteeFeatureLayer.MinimumScale = Program.VillageCommittee_MinimumScale;
                            showAnnotationByScale(m_VillageCommitteeFeatureLayer, "村委会_dwg", Program.VillageCommittee_Annotation_MaximumScale, Program.VillageCommittee_Annotation_MinimumScale);
                            break;
                        case "sde.SDE.自然村":
                            m_VillageFeatureLayer = vLayerFeature;
                            m_VillageFeatureLayer.MaximumScale = Program.Village_MaximumScale;
                            m_VillageFeatureLayer.MinimumScale = Program.Village_MinimumScale;
                            showAnnotationByScale(m_VillageFeatureLayer, "Text", Program.Village_Annotation_MaximumScale, Program.Village_Annotation_MinimumScale);
                            break;
                    }
                }
            }
            changeMapColor(background, townshipBackgroundColor, villageCommitteeBackgroundColor, villageBackgroundColor);

            ////加载资源图层
            RemoteInterface vRemoteInterface = new RemoteInterface();
            m_Lyaers = vRemoteInterface.GetLayers();
            foreach (LayerStruct vTempLayer in m_Lyaers)
            {
                try
                {
                    IFeatureClass vFeatureClass = vFeatWS.OpenFeatureClass(string.Format("sde.{0}", vTempLayer.Name));
                    IFeatureLayer vLayerFeature = new FeatureLayerClass();
                    vLayerFeature.MaximumScale = Program.Village_MaximumScale;
                    vLayerFeature.MinimumScale = Program.Village_MinimumScale;
                    vLayerFeature.FeatureClass = vFeatureClass;
                    vLayerFeature.Name = vTempLayer.Name;
                    axMapControl1.Map.AddLayer(vLayerFeature as ILayer);
                }
                catch 
                {
                    MessageBox.Show(string.Format("{0}图层读取失败", vTempLayer.Name), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //axMapControl1.Extent = axMapControl1.FullExtent;
            axMapControl1.Refresh();

            axMapControl1.OnSelectionChanged += AxMapControl1_OnSelectionChanged;

            m_EagleEyeForm = new EagleEyeForm();
            m_EagleEyeForm.MainMapControl = axMapControl1;
            m_EagleEyeForm.TownshipFeatureLayer = m_EagleEyeFeatureClass;
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

        void changeMapColor(int background, int townshipBackgroundColor,
            int villageCommitteeBackgroundColor, int villageBackgroundColor)
        {
            axMapControl1.BackColor = Color.FromArgb(background);

            IGeoFeatureLayer vGeoFeatureLayer;
            ISimpleRenderer vSimpleRenderer;
            IFillSymbol vFillSymbol;
            Color vColorRgb;

            if (m_TownshipFeatureLayer != null)
            {
                vGeoFeatureLayer = (IGeoFeatureLayer)m_TownshipFeatureLayer;
                vSimpleRenderer = (ISimpleRenderer)vGeoFeatureLayer.Renderer;
                vFillSymbol = new SimpleFillSymbolClass();
                vColorRgb = Color.FromArgb(townshipBackgroundColor);
                IRgbColor vColor = new RgbColorClass();
                vColor.Red = vColorRgb.R;
                vColor.Green = vColorRgb.G;
                vColor.Blue = vColorRgb.B;
                vFillSymbol.Color = vColor;
                vSimpleRenderer.Symbol = (ISymbol)vFillSymbol;
            }

            if (m_VillageCommitteeFeatureLayer != null)
            {
                vGeoFeatureLayer = (IGeoFeatureLayer)m_VillageCommitteeFeatureLayer;
                vSimpleRenderer = (ISimpleRenderer)vGeoFeatureLayer.Renderer;
                vFillSymbol = new SimpleFillSymbolClass();
             
                vColorRgb = Color.FromArgb(villageCommitteeBackgroundColor);
                IRgbColor vColor = new RgbColorClass();
                vColor.Red = vColorRgb.R;
                vColor.Green = vColorRgb.G;
                vColor.Blue = vColorRgb.B;
                vFillSymbol.Color = vColor;
                vSimpleRenderer.Symbol = (ISymbol)vFillSymbol;
            }

            if (m_VillageFeatureLayer != null)
            {
                vGeoFeatureLayer = (IGeoFeatureLayer)m_VillageFeatureLayer;
                vSimpleRenderer = (ISimpleRenderer)vGeoFeatureLayer.Renderer;
                ISimpleMarkerSymbol vSimpleMarkerSymbol = new SimpleMarkerSymbolClass();
                
                vColorRgb = Color.FromArgb(villageBackgroundColor);
                IRgbColor vColor = new RgbColorClass();
                vColor.Red = vColorRgb.R;
                vColor.Green = vColorRgb.G;
                vColor.Blue = vColorRgb.B;
                vSimpleMarkerSymbol.Color = vColor;
                //设置pSimpleMarkerSymbol对象的符号类型，选择钻石
                vSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
                //设置pSimpleMarkerSymbol对象大小，设置为５
                vSimpleMarkerSymbol.Size = 5;
                vSimpleRenderer.Symbol = (ISymbol)vSimpleMarkerSymbol;
            }
            axMapControl1.Refresh();
        }

        private void AxMapControl1_OnSelectionChanged(object sender, EventArgs e)
        {
            ISelection pSelection = axMapControl1.Map.FeatureSelection;
            IEnumFeatureSetup pEnumFeatureSetup = pSelection as IEnumFeatureSetup;
            pEnumFeatureSetup.AllFields = true;
            IEnumFeature pEnumFeature = pSelection as IEnumFeature;
            IFeature pFeature = pEnumFeature.Next();
            List<string> vAreaCodeList = new List<string>();
            while (pFeature != null)
            {
                int vXZDMIndex = 0;
                string vName = pFeature.Class.AliasName;
                switch( vName)
                {
                    case Program.TownshipTableName:
                        vXZDMIndex = pFeature.Fields.FindField(Program.TownshipCodeName);
                        break;
                    case Program.VillageCommitteeTableName:
                        vXZDMIndex = pFeature.Fields.FindField(Program.VillageCommitteeCodeName);
                        break;
                    case Program.VillageTableName:
                        vXZDMIndex = pFeature.Fields.FindField(Program.VillageCodeName);
                        break;
                }
                //Console.Write("Name:" + vName);
                string vCode = (string)pFeature.get_Value(vXZDMIndex);
                vCode = System.Web.HttpUtility.UrlEncode(vCode);
                vAreaCodeList.Add( vCode );
                pFeature = pEnumFeature.Next();
            }
            if (vAreaCodeList.Count > 0)
            {
                DisplayFilesForm vDisplayFilesForm = new DisplayFilesForm();
                vDisplayFilesForm.AreaCodeArray = vAreaCodeList.ToArray();
                vDisplayFilesForm.ShowDialog();
            }
            else
            {
                if ( m_ToolButtonIndex != 7 )
                    MessageBox.Show("选择的单位没有文档数据","信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            //ILayer zrcLayer = null;
            //for ( int i=0;i< axMapControl1.LayerCount;i++)
            //{
            //    ILayer layer =  axMapControl1.get_Layer(i);
            //    if ( layer.Name == "sde.DBO.自然村点")
            //    {
            //        zrcLayer = layer;
            //        break;
            //    }
            //}
            //IFeatureLayer featureLayer = zrcLayer as IFeatureLayer;
            //IFeatureClass featureClss = featureLayer.FeatureClass;
            //IQueryFilter queryFilter = new QueryFilterClass();
            //IFeatureCursor featureCursor;
            //IFeature feature = null;
            //queryFilter.WhereClause = ("RefName = '芳山'");
            //featureCursor = featureClss.Search(queryFilter, true);
            //feature = featureCursor.NextFeature();
            //if (feature!=null )
            //{
            //    axMapControl1.Map.SelectFeature(zrcLayer, feature);
            //    axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection,null,null);
            //}

            RemoteInterface vRemoteInterface = new RemoteInterface();
            var aa =  vRemoteInterface.GetLayers();
        }


        private void ToolStripMenuItem_Doc_Input_Click(object sender, EventArgs e)
        {
            UploadFileForm vUploadFileForm = new UploadFileForm();
            vUploadFileForm.TownshipFeatureLayer = m_TownshipFeatureLayer;
            vUploadFileForm.VillageCommitteeFeatureLayer = m_VillageCommitteeFeatureLayer;
            vUploadFileForm.VillageFeatureLayer = m_VillageFeatureLayer;
            vUploadFileForm.ShowDialog();
            
        }

        public void showAnnotationByScale(IFeatureLayer featureLayer,string annotationField,double maximumScale,double minimumScale)
        {
            
            //IFeatureLayer FeatureLayer = pMap.get_Layer(0) as IFeatureLayer;
            IGeoFeatureLayer pGeoFeatureLayer = featureLayer as IGeoFeatureLayer;
            //创建标注集接口，可以对标注进行添加、删除、查询、排序等操作
            IAnnotateLayerPropertiesCollection pAnnotateLayerPropertiesCollection = new AnnotateLayerPropertiesCollectionClass();
            pAnnotateLayerPropertiesCollection = pGeoFeatureLayer.AnnotationProperties;
            pAnnotateLayerPropertiesCollection.Clear();
            //创建标注的颜色
            IRgbColor pRgbColor = new RgbColorClass();
            pRgbColor.Red = 255;
            pRgbColor.Green = 0;
            pRgbColor.Blue = 0;
            //创建标注的字体样式
            ITextSymbol pTextSymbol = new TextSymbolClass();
            pTextSymbol.Color = pRgbColor;
            pTextSymbol.Size = 12;
            pTextSymbol.Font.Name = "宋体";
            //定义 ILineLabelPosition接口，用来管理line features的标注属性，指定标注和线要素的位置关系
            ILineLabelPosition pLineLabelPosition = new LineLabelPositionClass();
            pLineLabelPosition.Parallel = false;
            pLineLabelPosition.Perpendicular = true;
            pLineLabelPosition.InLine = true;
            //定义 ILineLabelPlacementPriorities接口用来控制标注冲突
            ILineLabelPlacementPriorities pLineLabelPlacementPriorities = new LineLabelPlacementPrioritiesClass();
            pLineLabelPlacementPriorities.AboveStart = 5;
            pLineLabelPlacementPriorities.BelowAfter = 4;
            //定义 IBasicOverposterLayerProperties 接口实现 LineLabelPosition 和 LineLabelPlacementPriorities对象的控制
            IBasicOverposterLayerProperties pBasicOverposterLayerProperties = new BasicOverposterLayerPropertiesClass();
            pBasicOverposterLayerProperties.LineLabelPlacementPriorities = pLineLabelPlacementPriorities;
            pBasicOverposterLayerProperties.LineLabelPosition = pLineLabelPosition;
            pBasicOverposterLayerProperties.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolygon;
            //创建标注对象
            ILabelEngineLayerProperties pLabelEngineLayerProperties = new LabelEngineLayerPropertiesClass();
            //设置标注符号
            pLabelEngineLayerProperties.Symbol = pTextSymbol;
            pLabelEngineLayerProperties.BasicOverposterLayerProperties = pBasicOverposterLayerProperties;
            //声明标注的Expression是否为Simple
            pLabelEngineLayerProperties.IsExpressionSimple = true;
            //设置标注字段
            pLabelEngineLayerProperties.Expression = "["+annotationField+"]";
            //定义IAnnotateLayerTransformationProperties 接口用来控制feature layer的display of dynamic labels
            IAnnotateLayerTransformationProperties pAnnotateLayerTransformationProperties = pLabelEngineLayerProperties as IAnnotateLayerTransformationProperties;
            //设置标注参考比例尺
            pAnnotateLayerTransformationProperties.ReferenceScale = maximumScale;
            //定义IAnnotateLayerProperties接口，决定FeatureLayer动态标注信息
            IAnnotateLayerProperties pAnnotateLayerProperties = pLabelEngineLayerProperties as IAnnotateLayerProperties;
            //设置显示标注最大比例尺
            pAnnotateLayerProperties.AnnotationMaximumScale = maximumScale;
            //设置显示标注的最小比例
            pAnnotateLayerProperties.AnnotationMinimumScale = minimumScale;
            //决定要标注的要素
            //pAnnotateLayerProperties.WhereClause = annotationField;
            //pAnnotateLayerProperties.WhereClause = "DIQU<>'宿州市'";
            //将创建好的标注对象添加到标注集对象中
            pAnnotateLayerPropertiesCollection.Add(pAnnotateLayerProperties);
            //声明标注是否显示
            pGeoFeatureLayer.DisplayAnnotation = true;
            ////刷新视图
            //this.axMapControl1.Refresh();
        }

        private void ToolStripMenuItem_Doc_Edit_Click(object sender, EventArgs e)
        {
            FileManageForm vFileManageForm = new FileManageForm();
            vFileManageForm.TownshipFeatureLayer = m_TownshipFeatureLayer;
            vFileManageForm.VillageCommitteeFeatureLayer = m_VillageCommitteeFeatureLayer;
            vFileManageForm.VillageFeatureLayer = m_VillageFeatureLayer;
            vFileManageForm.ShowDialog();
        }

        private void ToolStripMenuItem_EagleEye_Click(object sender, EventArgs e)
        {
            m_EagleEyeForm.Show();
        }

        private void ToolStripMenuItem_Doc_Report_Click(object sender, EventArgs e)
        {
            StatisticsReportForm vStatisticsReportForm = new StatisticsReportForm();
            vStatisticsReportForm.TownshipFeatureLayer = m_TownshipFeatureLayer;
            vStatisticsReportForm.VillageCommitteeFeatureLayer = m_VillageCommitteeFeatureLayer;
            vStatisticsReportForm.VillageFeatureLayer = m_VillageFeatureLayer;
            vStatisticsReportForm.ShowDialog();
        }

        private void ToolStripMenuItem_Doc_Query_Click(object sender, EventArgs e)
        {
            FileManageForm vFileManageForm = new FileManageForm();
            vFileManageForm.TownshipFeatureLayer = m_TownshipFeatureLayer;
            vFileManageForm.VillageCommitteeFeatureLayer = m_VillageCommitteeFeatureLayer;
            vFileManageForm.VillageFeatureLayer = m_VillageFeatureLayer;
            vFileManageForm.ShowDialog();
        }

        private void axToolbarControl1_OnItemClick(object sender, IToolbarControlEvents_OnItemClickEvent e)
        {
            m_ToolButtonIndex = e.index;
        }

        private void ToolStripMenuItem_Pic_Layer_Click(object sender, EventArgs e)
        {
            LayerManageForm vLayerManageForm = new LayerManageForm();
            vLayerManageForm.Layers = m_Lyaers;
            if ( vLayerManageForm.ShowDialog() == DialogResult.OK )
            {
                for( int i=0;i<axMapControl1.Map.LayerCount;i++)
                {
                    string vName = axMapControl1.Map.Layer[i].Name;
                    LayerStruct vLayer  = m_Lyaers.Where(m => m.Name == vName).FirstOrDefault();
                    if ( vLayer!=null)
                        axMapControl1.Map.Layer[i].Visible = vLayer.IsView;
                }
                axMapControl1.Refresh();
            }
        }
    }
}
