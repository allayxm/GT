﻿using System;
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
using ESRI.ArcGIS.AnalysisTools;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.Geoprocessor;
using JXDL.IntrefaceStruct;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.Output;
using System.Collections;

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

        public LayerStruct[] m_Layers;

        /// <summary>
        /// 缓冲区图层
        /// </summary>
        public List<LayerStruct> m_BufferLayers = new List<LayerStruct>();
        /// <summary>
        /// 鹰眼对话框
        /// </summary>
        EagleEyeForm m_EagleEyeForm = null;

        public void ChangeLayerVisible( string LayerName,bool Visible )
        {
            for( int i=0;i<axMapControl1.LayerCount;i++)
            {
                ILayer vLayer = axMapControl1.get_Layer(i);
                if ( vLayer.Name == LayerName)
                {
                    vLayer.Visible = Visible;
                    axMapControl1.Refresh();
                    break;
                }
            }
        }

        public void DeleteLayer(string LayerName)
        {
            for (int i = 0; i < axMapControl1.LayerCount; i++)
            {
                ILayer vLayer = axMapControl1.get_Layer(i);

                if (vLayer.Name == LayerName)
                {
                    axMapControl1.DeleteLayer(i);
                    m_BufferLayers.Remove(m_BufferLayers.Where(m => m.Name == LayerName).FirstOrDefault());
                    break;
                }
            }
            axMapControl1.Refresh();
        }


        public void DeleteAllBufferLayers()
        {
            m_SelectionChanged = false;
            foreach ( LayerStruct vTempLayer in m_BufferLayers)
            {
                for ( int i=0;i<axMapControl1.LayerCount;i++)
                {
                    if (axMapControl1.Map.Layer[i].Name == vTempLayer.Name)
                    {
                        axMapControl1.DeleteLayer(i);
                        break;
                    }
                }
            }
            m_BufferLayers.Clear();
            m_SelectionChanged = true;
        }

        /// <summary>
        /// 缓冲区临时文件生成路径
        /// </summary>
        readonly string m_BufferPath = string.Format(@"{0}\buffer\Buffer.shp", System.Environment.CurrentDirectory);
        string markBufferPath( string layerName)
        {
            string vBufferPath = string.Format(@"{0}\buffer\{1}_Buffer.shp", System.Environment.CurrentDirectory, layerName);
            return vBufferPath;
        }
        #region 右键菜单
        /// <summary>
        /// 地图查询
        /// </summary>
        bool m_MapQuery = false;

        bool m_FileQuery = true;
        #endregion

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
                    vRemoteInterface.GetMapServer(ref Program.MapDBAddress, ref Program.MapDBName, ref Program.MapDBUserName, ref Program.MapDBPassword);
                    Text = string.Format("新农村建设地理信息系统 【当前用户:{0} 所属机构:{1}】", Program.LoginUserInfo.UserName, getPowerName(Program.LoginUserInfo.Power.Value));
                    init_Menu();
                    init_Heartbeat();
                    init_Map(vConfigFile.MapBackgroundColor, vConfigFile.TownshipBackgroundColor, vConfigFile.VillageCommitteeBackgroundColor, vConfigFile.VillageBackgroundColor);
                }
                else
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        string getPowerName(int Power)
        {
            string vPowerName = "";
            switch (Power)
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

        public string[] GetColumns(string LayerName)
        {
            List<string> vResult = new List<string>();
            for (int i = 0; i < axMapControl1.Map.LayerCount; i++)
            {
                ILayer vLayer = axMapControl1.get_Layer(i);
                IFeatureLayer vFeatureLayer = vLayer as IFeatureLayer;
                string vLayerName = fixLayerName(vFeatureLayer);
                if (vLayerName== LayerName)
                {
                    for (int j = 0; j < vFeatureLayer.FeatureClass.Fields.FieldCount; j++)
                    {
                        string vColumnName = vFeatureLayer.FeatureClass.Fields.get_Field(j).Name.ToUpper();
                        switch (vColumnName)
                        {
                            case "OBJECTID":
                            case "SHAPE":
                            case "SHAPE_LENGTH":
                            case "SHAPE_AREA":
                            case "FID":
                            case "SHAPE.STAREA()":
                            case "SHAPE.STLENGTH()":
                                    //以上字段由系统自动生成  
                                    break;
                            default:
                                vResult.Add(vColumnName);
                                break;
                        }
                    }
                    break;
                }
                
            }
            return vResult.ToArray();
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
            switch (Program.LoginUserInfo.Power)
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

                    ToolStripMenuItem_Pic_Buffer.Enabled = false;
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
                    ToolStripMenuItem_Pic_Buffer.Enabled = true;
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
                    ToolStripMenuItem_Doc_Query.Enabled = true;
                    ToolStripMenuItem_Doc_Report.Enabled = true;

                    ToolStripMenuItem_Pic_Buffer.Enabled = true;
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

            if (vSystemConfigForm.ShowDialog() == DialogResult.OK)
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
            if (MessageBox.Show("是否确认退出?", "信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// 注销
        /// </summary>
        void exit()
        {
            if (Program.LoginUserInfo.ID != null)
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

        private void init_Map(int background, int townshipBackgroundColor,
            int villageCommitteeBackgroundColor, int villageBackgroundColor)
        {
            IWorkspaceFactory vWorkspaceFactory = new SdeWorkspaceFactoryClass();

            IPropertySet vPropSet = new PropertySetClass();

            vPropSet.SetProperty("SERVER", Program.MapDBAddress);
            vPropSet.SetProperty("INSTANCE", string.Format(@"{0}:sqlserver:{1}", Program.MapDBName, Program.MapDBAddress));
            vPropSet.SetProperty("DATABASE", Program.MapDBName);
            vPropSet.SetProperty("USER", Program.MapDBUserName);
            vPropSet.SetProperty("PASSWORD", Program.MapDBPassword);
            vPropSet.SetProperty("VERSION", "SDE.DEFAULT");
            vPropSet.SetProperty("AUTHENTICATION_MODE", "DBMS");
            axMapControl1.ClearLayers();

            IWorkspace vWorkspace = vWorkspaceFactory.Open(vPropSet, 0);
            IFeatureWorkspace vFeatWS = vWorkspace as IFeatureWorkspace;

            //初始化行政区域图层
            for (int i = 0; i < Program.MapTables.Length; i++)
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
                        case Program.TownshipTableName:
                            m_TownshipFeatureLayer = vLayerFeature;
                            m_TownshipFeatureLayer.MaximumScale = Program.Township_MaximumScale;
                            m_TownshipFeatureLayer.MinimumScale = Program.Township_MinimumScale;
                            showAnnotationByScale(m_TownshipFeatureLayer, "街道", Program.Township_Annotation_MaximumScale, Program.Township_Annotation_MinimumScale);
                            break;
                        case Program.VillageCommitteeTableName:
                            m_VillageCommitteeFeatureLayer = vLayerFeature;
                            m_VillageCommitteeFeatureLayer.MaximumScale = Program.VillageCommittee_MaximumScale;
                            m_VillageCommitteeFeatureLayer.MinimumScale = Program.VillageCommittee_MinimumScale;
                            showAnnotationByScale(m_VillageCommitteeFeatureLayer, "村委会_dwg", Program.VillageCommittee_Annotation_MaximumScale, Program.VillageCommittee_Annotation_MinimumScale);
                            break;
                        case Program.VillageTableName:
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
            ConfigFile vConfigFile = new ConfigFile();
            //var vLayerColor = vConfigFile.LayerColor;
            LayerStruct[] vLayerConfig = vConfigFile.LayerConfig;
            RemoteInterface vRemoteInterface = new RemoteInterface();
            m_Layers = vRemoteInterface.GetLayers();
            int vLayerIndex = 0;
            foreach (LayerStruct vTempLayer in m_Layers)
            {
                try
                {
                    IFeatureClass vFeatureClass = vFeatWS.OpenFeatureClass(string.Format("sde.{0}", vTempLayer.Name));
                    int vCount = vFeatureClass.FeatureCount(null);
                    if (vCount > 0)
                    {
                        vLayerIndex++;
                        IFeatureLayer vLayerFeature = new FeatureLayerClass();
                        vLayerFeature.MaximumScale = Program.Village_MaximumScale;
                        vLayerFeature.MinimumScale = Program.Village_MinimumScale;
                        vLayerFeature.FeatureClass = vFeatureClass;
                        vLayerFeature.Name = vTempLayer.Name;
                        axMapControl1.Map.AddLayer(vLayerFeature as ILayer);
                        LayerStruct vLayer = null;
                        if (vLayerConfig!=null)
                            vLayer = vLayerConfig.Where(m => m.Name == vTempLayer.Name).FirstOrDefault();
                        if (vLayer != null)
                        {
                            vTempLayer.Color = vLayer.Color;
                            vTempLayer.IsView = vLayer.IsView;
                            vTempLayer.Order = vLayer.Order;
                            vTempLayer.ShowAnnotation = vLayer.ShowAnnotation;
                            vTempLayer.AnnotationField = vLayer.AnnotationField;
                            vTempLayer.AnnotationFontColor = vLayer.AnnotationFontColor;
                            vTempLayer.AnnotationFontSize = vLayer.AnnotationFontSize;
                        }
                        vTempLayer.Order = vLayerIndex;
                        if (vTempLayer.Color != -1)
                            ChangeLayerColor(vLayerFeature.Name, vTempLayer.Color);
                        if (vTempLayer.ShowAnnotation)
                            EnableFeatureLayerLabel(vLayerFeature.Name, vTempLayer.AnnotationField, CommonUnit.ColorToIRgbColor(Color.FromArgb(vTempLayer.AnnotationFontColor)), vTempLayer.AnnotationFontSize);
                        vLayerFeature.Visible = vTempLayer.IsView;
                    }
                    else
                    {
                        vTempLayer.Order = -1;
                        vTempLayer.IsView = false;
                    }
                }
                catch
                {
                    MessageBox.Show(string.Format("{0}图层读取失败", vTempLayer.Name), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            m_Layers = m_Layers.Where(m => m.Order != -1).ToArray();

            

            //axMapControl1.FullExtent.Envelope.set_MinMaxAttributes( ref esriPointAttributes)
            //axMapControl1.FullExtent.Envelope.XMax = 416486.4234;
            //axMapControl1.FullExtent.Envelope.XMin = 416486.4234;
            //axMapControl1.FullExtent.Envelope.YMax = 416486.4234;
            //axMapControl1.FullExtent.Envelope.YMin = 416486.4234;
            //axMapControl1.FullExtent.QueryEnvelope();

            axMapControl1.Extent = axMapControl1.FullExtent;
            axMapControl1.Refresh();
            axMapControl1.OnFullExtentUpdated += AxMapControl1_OnFullExtentUpdated;

            //axMapControl1.OnSelectionChanged += AxMapControl1_OnSelectionChanged;

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

        private void AxMapControl1_OnFullExtentUpdated(object sender, IMapControlEvents2_OnFullExtentUpdatedEvent e)
        {
            //MessageBox.Show("test");
            //throw new NotImplementedException();
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
                vFillSymbol = new SimpleFillSymbolClass();

                vColorRgb = Color.FromArgb(villageBackgroundColor);
                IRgbColor vColor = new RgbColorClass();
                vColor.Red = vColorRgb.R;
                vColor.Green = vColorRgb.G;
                vColor.Blue = vColorRgb.B;
                vFillSymbol.Color = vColor;
                vSimpleRenderer.Symbol = (ISymbol)vFillSymbol;
            }

            //if (m_VillageFeatureLayer != null)
            //{
            //    vGeoFeatureLayer = (IGeoFeatureLayer)m_VillageFeatureLayer;
            //    vSimpleRenderer = (ISimpleRenderer)vGeoFeatureLayer.Renderer;
            //    ISimpleMarkerSymbol vSimpleMarkerSymbol = new SimpleMarkerSymbolClass();

            //    vColorRgb = Color.FromArgb(villageBackgroundColor);
            //    IRgbColor vColor = new RgbColorClass();
            //    vColor.Red = vColorRgb.R;
            //    vColor.Green = vColorRgb.G;
            //    vColor.Blue = vColorRgb.B;
            //    vSimpleMarkerSymbol.Color = vColor;
            //    //设置pSimpleMarkerSymbol对象的符号类型，选择钻石
            //    vSimpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle;
            //    //设置pSimpleMarkerSymbol对象大小，设置为５
            //    vSimpleMarkerSymbol.Size = 5;
            //    vSimpleRenderer.Symbol = (ISymbol)vSimpleMarkerSymbol;
            //}
            axMapControl1.Refresh();
        }

        bool m_SelectionChanged = true;
        private void AxMapControl1_OnSelectionChanged(object sender, EventArgs e)
        {
            //bufferAnayleEx();
            //IActiveView activeView = axMapControl1.ActiveView;
            //activeView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, 0, axMapControl1.Extent);
            ICommand vCurrenTool = axMapControl1.CurrentTool as ICommand;
            if (vCurrenTool.Name == "ControlToolsFeatureSelection_SelectFeatures" && m_SelectionChanged)
            {
                if (toolStripMenuItem_MapQuery.Checked)
                    mapQuery();
                else if (toolStripMenuItem_FileQuery.Checked)
                    viewFiles();
                else if (toolStripMenuItem_SpaceAnalyze.Checked)
                    bufferAnayleEx();
            }

            //if (m_MapQuery)
            //    mapQuery();
            //else if ( m_FileQuery )
            //    viewFiles();
        }

        void bufferAnayle()
        {
            Dictionary<string, List<IFeature>> vSelectFeaturesLayer = new Dictionary<string, List<IFeature>>();
            Dictionary<string, IFeatureLayer> vSelectFeaturesFields = new Dictionary<string, IFeatureLayer>();
          
            //获取所有的选择的要素，并按图层放入对应的Dictionary中
            ISelection vSelection = axMapControl1.Map.FeatureSelection;
            IEnumFeatureSetup vEnumFeatureSetup = vSelection as IEnumFeatureSetup;
            vEnumFeatureSetup.AllFields = true;
            IEnumFeature vEnumFeature = vSelection as IEnumFeature;
            IFeature vFeature = vEnumFeature.Next();
            while (vFeature!= null)
            {
                IFeatureLayer vFeatureLayer = findIndexByFeature(vFeature);
                string vFeatureLayerName = vFeatureLayer.FeatureClass.AliasName;
                vFeatureLayerName = vFeatureLayerName.Substring(vFeatureLayerName.LastIndexOf('.')+1);
                if (vSelectFeaturesLayer.ContainsKey(vFeatureLayerName))
                    vSelectFeaturesLayer[vFeatureLayerName].Add(vFeature);
                else
                {
                    vSelectFeaturesLayer.Add(vFeatureLayerName, new List<IFeature>() { vFeature });
                    vSelectFeaturesFields.Add(vFeatureLayerName, vFeatureLayer);
                }
                vFeature = vEnumFeature.Next();
            }
            //创建内存图层，并加入应的要素
            List<IFeatureLayer> vMemFeatureLayerList = new List<IFeatureLayer>();
            foreach (KeyValuePair<string, List<IFeature>> vTempDict in vSelectFeaturesLayer)
            {
                IFeatureLayer vSourceLayer = vSelectFeaturesFields[vTempDict.Key];
                IFields vOldFields = vSourceLayer.FeatureClass.Fields;
                ISpatialReference vSpatialReference = (vSourceLayer.FeatureClass as IGeoDataset).SpatialReference;
                IFields vFields =  CloneFeatureClassFields(vSourceLayer.FeatureClass, null);
                IFeatureLayer vMemFeatureLayer = CreateFeatureLayerInmemeory(vTempDict.Key, vTempDict.Key, new UnknownCoordinateSystemClass(), vSourceLayer.FeatureClass.ShapeType, vSourceLayer.FeatureClass.Fields);
                IFeatureCursor vMemFeatureCursor = vMemFeatureLayer.FeatureClass.Insert(true);

                //生成两个要素类字段的对应表  
                Dictionary<int, int> pFieldsDict = new Dictionary<int, int>();
                GetFCFieldsDirectory(vSourceLayer.FeatureClass, vMemFeatureLayer.FeatureClass, ref pFieldsDict);

                foreach (IFeature vTempFeature in vTempDict.Value)
                {
                    IFeatureBuffer vNewFeatureBuffer =  vMemFeatureLayer.FeatureClass.CreateFeatureBuffer();
                    IFeature vNewFeature = vNewFeatureBuffer as IFeature;
                    //vNewFeature = vTempFeature;
                    //for ( int i=0;i< vTempFeature.Fields.FieldCount;i++)
                    //{
                    // int vShapeFieldIndex = vMemFeatureLayer.FeatureClass.FindField( vMemFeatureLayer.FeatureClass.ShapeFieldName );
                    IGeometry pGeom = vTempFeature.ShapeCopy;

                    //IZAware pZaware = pGeom as IZAware;
                    //pZaware.DropZs();
                    //pZaware.ZAware = false;

                    //vNewFeatureBuffer.Shape = pGeom;
                    //vNewFeatureBuffer.set_Value(vShapeFieldIndex, vTempFeature);
                    //ITopologicalOperator buff = vTempFeature.Shape as ITopologicalOperator;

                    //vNewFeature.Shape = buff.Buffer(100);

                    FeatureHelper.CopyFeature(vTempFeature, vNewFeature);
                    int vShapeFieldIndex = vMemFeatureLayer.FeatureClass.FindField(vMemFeatureLayer.FeatureClass.ShapeFieldName);
                    IGeometry vIG = (IGeometry)vTempFeature.get_Value(vShapeFieldIndex);
                    IPoint xpoint = new PointClass();
                    xpoint.PutCoords(100, 200);
                    vNewFeature.set_Value(vShapeFieldIndex, pGeom);

                    //vNewFeature.Store();
                    //foreach (KeyValuePair<int, int> keyvalue in pFieldsDict)
                    //{
                    //    if (vTempFeature.get_Value(keyvalue.Key).ToString() == "")
                    //    {
                    //        if (vNewFeature.Fields.get_Field(keyvalue.Value).Type == esriFieldType.esriFieldTypeString)
                    //        {
                    //            vNewFeature.set_Value(keyvalue.Value, "");
                    //        }
                    //        else
                    //        {
                    //            vNewFeature.set_Value(keyvalue.Value, 0);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        vNewFeature.set_Value(keyvalue.Value, vNewFeature.get_Value(keyvalue.Key));
                    //    }
                    //    //}
                    //}
                    object vM = vMemFeatureCursor.InsertFeature(vNewFeatureBuffer);
                   
                }
                vMemFeatureCursor.Flush();
                ChangeLayerColor(vMemFeatureLayer, -65536);
                axMapControl1.Map.AddLayer(vMemFeatureLayer);
                vMemFeatureLayerList.Add(vMemFeatureLayer);
            }
            m_VillageCommitteeFeatureLayer.Visible = false;
            m_VillageFeatureLayer.Visible = false;
            vMemFeatureLayerList.Clear();
            vMemFeatureLayerList.Add(m_VillageFeatureLayer);

            //生成缓冲区
            Geoprocessor vGP = new Geoprocessor();
            //OverwriteOutput为真时，输出图层会覆盖当前文件夹下的同名图层
            vGP.OverwriteOutput = true;
            string vBufferResult = "";
            foreach (IFeatureLayer vMemFeatureLayer in vMemFeatureLayerList)
            {
                int vShapeFieldIndex = vMemFeatureLayer.FeatureClass.FindField(vMemFeatureLayer.FeatureClass.ShapeFieldName);
                IQueryFilter vQueryFilter = new QueryFilter();
                int vCount = vMemFeatureLayer.FeatureClass.FeatureCount(vQueryFilter);
                IFeatureCursor vv = vMemFeatureLayer.FeatureClass.Search(vQueryFilter, true);
                IFeature vFF = vv.NextFeature();
                
                //while (vFF != null)
                //{
                //    IGeometry vIG = (IGeometry)vFF.get_Value(vShapeFieldIndex);
                //    vFF = vv.NextFeature();
                //}
                ESRI.ArcGIS.AnalysisTools.Buffer vBuffer = new ESRI.ArcGIS.AnalysisTools.Buffer(vMemFeatureLayer, markBufferPath("Test"), 200);
                IGeoProcessorResult results = null;
                results = (IGeoProcessorResult)vGP.Execute(vBuffer, null);
                if (results.Status != esriJobStatus.esriJobSucceeded)
                    vBufferResult += string.Format("{0}缓冲区生成失败！\r\n", vMemFeatureLayer.Name);
                else
                {
                    vBufferResult += string.Format("{0}缓冲区生成成功！\r\n", vMemFeatureLayer.Name);
                }
            }
        }

        private IFields CloneFeatureClassFields(IFeatureClass pFeatureClass, IEnvelope pDomainEnv)
        {
            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit = (IFieldsEdit)pFields;
            //根据传入的要素类,将除了shape字段之外的字段复制  
            long nOldFieldsCount = pFeatureClass.Fields.FieldCount;
            long nOldGeoIndex = pFeatureClass.Fields.FindField(pFeatureClass.ShapeFieldName);
            for (int i = 0; i < nOldFieldsCount; i++)
            {
                if (i != nOldGeoIndex)
                {
                    pFieldsEdit.AddField(pFeatureClass.Fields.get_Field(i));
                }
                else
                {
                    IGeometryDef pGeomDef = new GeometryDefClass();
                    IGeometryDefEdit pGeomDefEdit = (IGeometryDefEdit)pGeomDef;
                    ISpatialReference pSR = null;
                    if (pDomainEnv != null)
                    {
                        pSR = new UnknownCoordinateSystemClass();
                        pSR.SetDomain(pDomainEnv.XMin, pDomainEnv.XMax, pDomainEnv.YMin, pDomainEnv.YMax);
                    }
                    else
                    {
                        IGeoDataset pGeoDataset = pFeatureClass as IGeoDataset;
                        pSR = CloneSpatialReference(pGeoDataset.SpatialReference);
                    }
                    //设置新要素类Geometry的参数  
                    pGeomDefEdit.GeometryType_2 = pFeatureClass.ShapeType;
                    pGeomDefEdit.GridCount_2 = 1;
                    pGeomDefEdit.set_GridSize(0, 10);
                    pGeomDefEdit.AvgNumPoints_2 = 2;
                    pGeomDefEdit.SpatialReference_2 = pSR;
                    pGeomDefEdit.HasZ_2 = false;
                    //产生新的shape字段  
                    IField pField = new FieldClass();
                    IFieldEdit pFieldEdit = (IFieldEdit)pField;
                    pFieldEdit.Name_2 = "shape";
                    pFieldEdit.AliasName_2 = "shape";
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                    pFieldEdit.GeometryDef_2 = pGeomDef;
                    pFieldsEdit.AddField(pField);
                }
            }
            return pFields;
        }
        private ISpatialReference CloneSpatialReference(ISpatialReference pSrcSpatialReference)
        {
            double xmin, xmax, ymin, ymax;
            pSrcSpatialReference.GetDomain(out xmin, out xmax, out ymin, out ymax);
            ISpatialReference pSR = new UnknownCoordinateSystemClass();
            pSR.SetDomain(xmin, xmax, ymin, ymax);
            return pSR;
        }

        private void GetFCFieldsDirectory(IFeatureClass pFCold, IFeatureClass pFCnew, ref Dictionary<int, int> FieldsDictionary)
        {
            for (int i = 0; i < pFCold.Fields.FieldCount; i++)
            {
                string tmpstrold = pFCold.Fields.get_Field(i).Name.ToUpper();
                switch (tmpstrold)
                {
                    case "OBJECTID":
                    case "SHAPE":
                    case "SHAPE_LENGTH":
                    case "SHAPE_AREA":
                    case "FID":
                    case "SHAPE.STAREA()":
                    case "SHAPE.STLENGTH()":
                        {
                            //以上字段由系统自动生成  
                            break;
                        }
                    default:
                        {
                            for (int j = 0; j < pFCnew.Fields.FieldCount; j++)
                            {
                                string tmpstrnew = pFCnew.Fields.get_Field(j).Name.ToUpper();
                                if (tmpstrold == tmpstrnew)
                                {
                                    FieldsDictionary.Add(i, j);
                                    break;
                                }
                            }
                            break;
                        }
                }
            }
        }

        BufferForm m_BufferForm = null;
        void bufferAnayleEx()
        {
            Dictionary<string, BufferConfig> vSelectFeatureLayers = new Dictionary<string, BufferConfig>();

            //获取所有的选择的要素，并按图层放入对应的Dictionary中
            ISelection vSelection = axMapControl1.Map.FeatureSelection;
            IEnumFeatureSetup vEnumFeatureSetup = vSelection as IEnumFeatureSetup;
            vEnumFeatureSetup.AllFields = true;
            IEnumFeature vEnumFeature = vSelection as IEnumFeature;
            IFeature vFeature = vEnumFeature.Next();
            while (vFeature != null)
            {
                IFeatureLayer vFeatureLayer = findIndexByFeature(vFeature);
                string vFeatureLayerName = fixLayerName(vFeatureLayer);
                //排除村委会、乡镇街道、自然村三个图层
                if (vFeatureLayerName != "村委会" && vFeatureLayerName != "乡镇街道" && vFeatureLayerName != "自然村" && m_BufferLayers.Where(m=>m.Name== vFeatureLayerName).Count()== 0 )
                {
                    if (!vSelectFeatureLayers.ContainsKey(vFeatureLayerName))
                        vSelectFeatureLayers.Add(vFeatureLayerName, new BufferConfig() { LayerName= vFeatureLayerName });
                }
                vFeature = vEnumFeature.Next();
            }
            if (vSelectFeatureLayers.Count > 0)
            {
                if (m_BufferForm == null || m_BufferForm.IsDisposed )
                {
                    m_BufferForm = new BufferForm();
                    m_BufferForm.Layers = m_Layers;
                    m_BufferForm.VMainForm = this;
                    m_BufferForm.BufferLayers = vSelectFeatureLayers;
                    m_BufferForm.Show();
                }
                else
                {
                    m_BufferForm.BufferLayers = vSelectFeatureLayers;
                    //m_BufferForm.initSelectedLayers();
                    m_BufferForm.initTreeView();
                    m_BufferForm.Refresh();
                }
            }
        }

        public string CreateBufferLayerEx(Dictionary<string, BufferConfig> selectFeatureLayers)
        {
            //Dictionary<IFeatureLayer, int> vBufferLayers = new Dictionary<IFeatureLayer, int>();
            //生成缓冲区
            Geoprocessor vGP = new Geoprocessor();
            //OverwriteOutput为真时，输出图层会覆盖当前文件夹下的同名图层
            vGP.OverwriteOutput = true;
            string vBufferResult = "";

            for (int i = 0; i < axMapControl1.LayerCount; i++)
            {
                ILayer vLayer = axMapControl1.get_Layer(i);
                IFeatureLayer vFeatureLayer = vLayer as IFeatureLayer;
                string vAliasName = fixLayerName(vFeatureLayer);
                if (selectFeatureLayers.ContainsKey(vAliasName) && selectFeatureLayers[vAliasName].IsSelect)
                {
                    var vSelectLayer = selectFeatureLayers[vAliasName];
                    try
                    {
                        //string vLayerName = fixLayerName(vSelectLayer.Value.);
                        string vBufferFileName = markBufferPath(vAliasName);
                        ESRI.ArcGIS.AnalysisTools.Buffer vBuffer = new ESRI.ArcGIS.AnalysisTools.Buffer(vFeatureLayer, vBufferFileName, vSelectLayer.Distance);
                        IGeoProcessorResult results = null;
                        results = (IGeoProcessorResult)vGP.Execute(vBuffer, null);
                        if (results.Status != esriJobStatus.esriJobSucceeded)
                            vBufferResult += string.Format("{0}缓冲区生成失败！\r\n", vSelectLayer.Expository);
                        else
                        {
                            int vLateIndex = vBufferFileName.LastIndexOf('\\');
                            string vFilePath = vBufferFileName.Substring(0, vLateIndex);
                            string vFileName = vBufferFileName.Substring(vLateIndex + 1);
                            //检查缓存目录是否存在
                            if (!System.IO.Directory.Exists(vFilePath))
                                System.IO.Directory.CreateDirectory(vFilePath);
                            vBufferResult += string.Format("{0}缓冲区生成成功！\r\n", vSelectLayer.Expository);

                            LayerStruct vBufferLayer = new LayerStruct()
                            {
                                Name = vAliasName + "_Buffer",
                                IsView = true,
                                Expository = vAliasName + "缓冲图层"
                            };
                            vSelectLayer.BufferLayerName = vBufferLayer.Name;
                            m_BufferLayers.Add(vBufferLayer);
                        }
                    }
                    catch (Exception ex)
                    {
                        vBufferResult += string.Format("{0}缓冲区生成失败！{1}\r\n", vSelectLayer.Expository, ex.Message);
                    }
                }

            }

            ISpatialFilter vSpatialFilter = new SpatialFilter();
            vSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIndexIntersects;

            foreach (LayerStruct vBufferStruct in m_BufferLayers)
            {
                BufferConfig vBufferConfig = selectFeatureLayers.Where(m => m.Value.BufferLayerName == vBufferStruct.Name).FirstOrDefault().Value;
                string vBufferFileName = string.Format(@"{0}\buffer\{1}.shp", System.Environment.CurrentDirectory, vBufferStruct.Name);
                int vLateIndex = vBufferFileName.LastIndexOf('\\');
                string vFilePath = vBufferFileName.Substring(0, vLateIndex);
                string vFileName = vBufferFileName.Substring(vLateIndex + 1);
                axMapControl1.AddShapeFile(vFilePath, vFileName);

                foreach (ListViewItem vItem in vBufferConfig.SelectedLayers)
                {
                    ILayer vAnalyseLayer = GetLayerFromName(vItem.Name);
                    IFeatureLayer vAnalyseFeatureLayer = vAnalyseLayer as IFeatureLayer;
                    vBufferConfig.AnalyzeLayers.Add(new ListViewItem() { Name = vItem.Name, Text = vItem.Text });

                    DataTable vTable = CommonUnit.CreateFeaturesTableStruct(vAnalyseFeatureLayer.FeatureClass);
                    vTable.TableName = vItem.Name;
                    vBufferConfig.AnalyzeLayers_Detail.Add(vTable);

                    ILayer vBufferLayer = GetLayerFromName(vBufferStruct.Name);
                    IFeatureLayer vBufferFeatureLayer = vBufferLayer as IFeatureLayer;
                    IFeatureCursor vFeatureCursor = vBufferFeatureLayer.FeatureClass.Search(null, true);
                    IFeature vFeature = vFeatureCursor.NextFeature();

                    while (vFeature != null)
                    {
                        vSpatialFilter.Geometry = vFeature.Shape;
                        IFeatureCursor vAnalyseFeatureCursor = vAnalyseFeatureLayer.Search(vSpatialFilter, true);
                        IFeature vAnayleFeature = vAnalyseFeatureCursor.NextFeature();
                        while(vAnayleFeature!=null)
                        {
                            DataRow vNewRow = vTable.NewRow();
                            for( int i=0;i<vTable.Columns.Count;i++)
                            {
                                if ( vAnayleFeature.Fields.Field[i].Name != "Shape" )
                                {
                                    object vValue = vAnayleFeature.get_Value(i);
                                    vNewRow[i] = vValue;
                                }
                            }
                            vTable.Rows.Add(vNewRow);
                            vAnayleFeature = vAnalyseFeatureCursor.NextFeature();
                        }
                        vFeature = vFeatureCursor.NextFeature();
                    }
                    vTable.AcceptChanges();


                }
            }

            return vBufferResult;
        }

        public string CreateBufferLayer(Dictionary<string, BufferConfig> selectFeatureLayers)
        {
            Dictionary<IFeatureLayer, int> vBufferLayers = new Dictionary<IFeatureLayer, int>();
            for (int i = 0; i < axMapControl1.LayerCount; i++)
            {
                ILayer vLayer = axMapControl1.get_Layer(i);
                IFeatureLayer vFeatureLayer = vLayer as IFeatureLayer;
                string vAliasName = fixLayerName(vFeatureLayer);
                foreach (var vSelectLayer in selectFeatureLayers)
                {
                    if (vAliasName == vSelectLayer.Key)
                    {
                        vBufferLayers.Add(vFeatureLayer, vSelectLayer.Value.Distance);
                        break;
                    }

                }
            }

            //生成缓冲区
            Geoprocessor vGP = new Geoprocessor();
            //OverwriteOutput为真时，输出图层会覆盖当前文件夹下的同名图层
            vGP.OverwriteOutput = true;
            string vBufferResult = "";
            
            foreach (var vBufferDict in vBufferLayers)
            {
                try
                {
                    string vLayerName = fixLayerName(vBufferDict.Key);
                    string vBufferFileName = markBufferPath(vLayerName);
                    ESRI.ArcGIS.AnalysisTools.Buffer vBuffer = new ESRI.ArcGIS.AnalysisTools.Buffer(vBufferDict.Key, vBufferFileName, vBufferDict.Value);
                    IGeoProcessorResult results = null;
                    results = (IGeoProcessorResult)vGP.Execute(vBuffer, null);
                    if (results.Status != esriJobStatus.esriJobSucceeded)
                        vBufferResult += string.Format("{0}缓冲区生成失败！\r\n", vBufferDict.Key.Name);
                    else
                    {
                        int vLateIndex = vBufferFileName.LastIndexOf('\\');
                        string vFilePath = vBufferFileName.Substring(0, vLateIndex);
                        string vFileName = vBufferFileName.Substring(vLateIndex + 1);
                        //检查缓存目录是否存在
                        if (!System.IO.Directory.Exists(vFilePath))
                            System.IO.Directory.CreateDirectory(vFilePath);
                        vBufferResult += string.Format("{0}缓冲区生成成功！\r\n", vBufferDict.Key.Name);
                        axMapControl1.AddShapeFile(vFilePath, vFileName);
                        LayerStruct vBufferLayer = new LayerStruct()
                        {
                            Name = vLayerName + "_Buffer",
                            IsView = true,
                            Expository = vLayerName + "缓冲图层"
                        };
                        m_BufferLayers.Add(vBufferLayer);
                    }
                }
                catch (Exception ex)
                {
                    vBufferResult += string.Format("{0}缓冲区生成失败！{1}\r\n", vBufferDict.Key.Name, ex.Message);
                }

            }
            //MessageBox.Show(vBufferResult, "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

            for (int i = 0; i < axMapControl1.Map.LayerCount; i++)
            {
                ILayer vLayer = axMapControl1.get_Layer(i);
                System.Diagnostics.Debug.WriteLine(vLayer.Name);
            }
            return vBufferResult;
        }

        string fixLayerName( IFeatureLayer featureLayer)
        {
            string vAliasName = featureLayer.FeatureClass.AliasName;
            int vIndex = vAliasName.LastIndexOf('.');
            if (vIndex != -1)
                vAliasName = vAliasName.Substring(vAliasName.LastIndexOf('.') + 1);
            return vAliasName;
        }

        private IFeatureLayer findIndexByFeature(IFeature pFeature)
        {
            IFeatureLayer vResult = null;
            IFeatureClass pFeatureClass = pFeature.Class as IFeatureClass;
            for (int i = 0; i < axMapControl1.Map.LayerCount; i++)
            {
                IFeatureLayer iFeatureLayer = axMapControl1.get_Layer(i) as IFeatureLayer;
                IFeatureClass iFeatureCla = iFeatureLayer.FeatureClass;

                if (iFeatureCla == pFeatureClass)
                {
                    vResult = iFeatureLayer;
                    return iFeatureLayer;
                }
            }
            return vResult;
        }

        MapQueryForm m_MapQueryForm = null;
        void mapQuery()
        {
            Dictionary<string, List<IFeature>> vSelectFeatures = new Dictionary<string, List<IFeature>>();
            ISelection pSelection = axMapControl1.Map.FeatureSelection;
            IEnumFeatureSetup pEnumFeatureSetup = pSelection as IEnumFeatureSetup;
            pEnumFeatureSetup.AllFields = true;
            IEnumFeature pEnumFeature = pSelection as IEnumFeature;
            IFeature pFeature = pEnumFeature.Next();
            while (pFeature != null)
            {
                //int vXZDMIndex = 0;
                string vName = pFeature.Class.AliasName;
                vName = vName.Remove(0, vName.LastIndexOf('.') + 1);
                if (!vSelectFeatures.ContainsKey(vName))
                    vSelectFeatures.Add(vName, new List<IFeature>());
                vSelectFeatures[vName].Add(pFeature);
                pFeature = pEnumFeature.Next();
            }
            if (vSelectFeatures.Count > 0)
            {
                if (m_MapQueryForm == null || m_MapQueryForm.IsDisposed)
                {
                    m_MapQueryForm = new MapQueryForm();
                    m_MapQueryForm.SelectFeatures = vSelectFeatures;
                    m_MapQueryForm.VMainForm = this;
                    m_MapQueryForm.Show();
                }
                else
                {
                    m_MapQueryForm.SelectFeatures = vSelectFeatures;
                    m_MapQueryForm.Show();
                    m_MapQueryForm.InitFeatureLayers();
                    m_MapQueryForm.Refresh();
                }
            }
            //else
            //{
            //    if (m_ToolButtonIndex != 7)
            //        MessageBox.Show("没有选择任何要素", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        public void SelectFeatures(int[] ObjectIDArray,string LayerName)
        {
            axMapControl1.Map.ClearSelection();
            QueryFilterClass vQueryFilter = new QueryFilterClass();
            string vObjectIDStr = "";
            foreach (int vTempID in ObjectIDArray)
            {
                vObjectIDStr += vTempID + ",";
            }
            if (vObjectIDStr != "")
                vObjectIDStr = vObjectIDStr.Remove(vObjectIDStr.Length - 1);
            vQueryFilter.WhereClause = string.Format("OBJECTID in ({0})", vObjectIDStr);
            for (int i = 0; i < axMapControl1.LayerCount; i++)
            {
                ILayer vLayer = axMapControl1.get_Layer(i);
                if (vLayer.Name == LayerName)
                {
                    IFeatureLayer vFeatureLayer = vLayer as IFeatureLayer;
                    IFeatureClass vFeatureClass = vFeatureLayer.FeatureClass;
                    IFeatureCursor vSerachResult = vFeatureClass.Search(vQueryFilter, true);
                    IFeature vFeature = vSerachResult.NextFeature();
                    while (vFeature != null)
                    {
                        axMapControl1.Map.SelectFeature(vLayer, vFeature);
                        vFeature = vSerachResult.NextFeature();

                        //axMapControl1.Extent.Union(vFeature.Shape.Envelope);
                    }
                }
            }
            axMapControl1.Refresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
        }

        DisplayFilesForm m_DisplayFilesForm = null;
        void viewFiles()
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
                switch (vName)
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
                vAreaCodeList.Add(vCode);
                pFeature = pEnumFeature.Next();
            }
            if (vAreaCodeList.Count > 0)
            {
                if (m_DisplayFilesForm == null || m_DisplayFilesForm.IsDisposed )
                {
                    m_DisplayFilesForm = new DisplayFilesForm();
                    m_DisplayFilesForm.AreaCodeArray = vAreaCodeList.ToArray();
                    m_DisplayFilesForm.Show();
                }
                else
                {
                    m_DisplayFilesForm.AreaCodeArray = vAreaCodeList.ToArray();
                    m_DisplayFilesForm.InitFileInfo();
                    m_DisplayFilesForm.Show();
                    m_DisplayFilesForm.Refresh();
                }
            }
            //else
            //{
            //    if (m_ToolButtonIndex != 7)
            //        MessageBox.Show("选择的单位没有文档数据", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
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
            //bool a = axMapControl1.CurrentTool is ESRI.ArcGIS.Controls.ControlsNewRectangleToolClass;

            //鼠标右键菜单
            if (e.button == 2)
            {
                System.Drawing.Point p = new System.Drawing.Point();
                p.X = e.x;
                p.Y = e.y;
                contextMenuStrip_Right.Show(axMapControl1, p);
            }


            //if (e.button != 2) return;
            //esriTOCControlItem pItem = esriTOCControlItem.esriTOCControlItemNone; 
            //IBasicMap pMap = null;
            //ILayer pLayer = null;
            //object pOther = new object();
            //object pIndex = new object();

            //axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pMap, ref pLayer, ref pOther, ref pIndex);
            //if (pItem == esriTOCControlItem.esriTOCControlItemLayer)
            //{

            //System.Drawing.Point p = new System.Drawing.Point();
            //p.X = e.x;
            //p.Y = e.y;
            //contextMenuStrip1.Show(axMapControl1, p);
            //}
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
            //bufferAnayle();
            bufferAnayleEx();
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

            //RemoteInterface vRemoteInterface = new RemoteInterface();
            //var aa = vRemoteInterface.GetLayers();
        }


        private void ToolStripMenuItem_Doc_Input_Click(object sender, EventArgs e)
        {
            UploadFileForm vUploadFileForm = new UploadFileForm();
            vUploadFileForm.TownshipFeatureLayer = m_TownshipFeatureLayer;
            vUploadFileForm.VillageCommitteeFeatureLayer = m_VillageCommitteeFeatureLayer;
            vUploadFileForm.VillageFeatureLayer = m_VillageFeatureLayer;
            vUploadFileForm.ShowDialog();
        }

        public void showAnnotationByScale(IFeatureLayer featureLayer, string annotationField, double maximumScale, double minimumScale)
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
            pLabelEngineLayerProperties.Expression = "[" + annotationField + "]";
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
            vLayerManageForm.Layers = m_Layers;
            vLayerManageForm.VMainForm = this;
            vLayerManageForm.Show();

            //if (vLayerManageForm.ShowDialog() == DialogResult.OK)
            //{
            //    Dictionary<string, int> vLayerConfig = new Dictionary<string, int>();
            //    for (int i = 0; i < axMapControl1.Map.LayerCount; i++)
            //    {
            //        string vName = axMapControl1.Map.Layer[i].Name;
            //        LayerStruct vLayer = m_Layers.Where(m => m.Name == vName).FirstOrDefault();
            //        if (vLayer != null && vLayer.Color != 0)
            //        {
            //            axMapControl1.Map.Layer[i].Visible = vLayer.IsView;
            //            if (vLayer.Color != -1)
            //                changeLayerColor(axMapControl1.Map.Layer[i], vLayer.Color);
            //            vLayerConfig.Add(vName, vLayer.Color);
            //        }
            //    }

            //    ConfigFile vConfigFile = new ConfigFile();
            //    vConfigFile.LayerColor = vLayerConfig;
            //    vConfigFile.Save();
            //    axMapControl1.Refresh();
            //}
        }

        public void ChangeLayerColor(ILayer Layer, int color)
        {
            IGeoFeatureLayer vGeoFeatureLayer;
            ISimpleRenderer vSimpleRenderer;
            IFillSymbol vFillSymbol;
            Color vColorRgb;
            vGeoFeatureLayer = (IGeoFeatureLayer)Layer;
            vSimpleRenderer = (ISimpleRenderer)vGeoFeatureLayer.Renderer;
            vFillSymbol = new SimpleFillSymbolClass();
            vColorRgb = Color.FromArgb(color);
            IRgbColor vColor = new RgbColorClass();
            vColor.Red = vColorRgb.R;
            vColor.Green = vColorRgb.G;
            vColor.Blue = vColorRgb.B;
            vFillSymbol.Color = vColor;
            vSimpleRenderer.Symbol = (ISymbol)vFillSymbol;
            axMapControl1.Refresh();
        }

        public void ChangeLayerColor(string layerName, int color)
        {

            for (int i = 0; i < axMapControl1.Map.LayerCount; i++)
            {
                string vName = axMapControl1.Map.Layer[i].Name;
                if (vName == layerName)
                {
                    ILayer vLayer = axMapControl1.Map.Layer[i] as ILayer;
                    IGeoFeatureLayer vGeoFeatureLayer;
                    ISimpleRenderer vSimpleRenderer;
                    IFillSymbol vFillSymbol;
                    Color vColorRgb;
                    vGeoFeatureLayer = (IGeoFeatureLayer)vLayer;
                    vSimpleRenderer = (ISimpleRenderer)vGeoFeatureLayer.Renderer;
                    vFillSymbol = new SimpleFillSymbolClass();
                    vColorRgb = Color.FromArgb(color);
                    IRgbColor vColor = new RgbColorClass();
                    vColor.Red = vColorRgb.R;
                    vColor.Green = vColorRgb.G;
                    vColor.Blue = vColorRgb.B;
                    vFillSymbol.Color = vColor;
                    vSimpleRenderer.Symbol = (ISymbol)vFillSymbol;
                    axMapControl1.Refresh();
                    break;
                }
            }

          
        }

        private void ToolStripMenuItem_Pic_Anayle_Click(object sender, EventArgs e)
        {
            SpaceAnalyzeMode();
            bufferAnayleEx();
        }

        private void ToolStripMenuItem_Pic_Map_Click(object sender, EventArgs e)
        {
            MapQueryMode();
            mapQuery();

            //Dictionary<string, List<IFeature>> vSelectFeatures = new Dictionary<string, List<IFeature>>();
            //int vCount = axMapControl1.LayerCount;
            //for (int i = 0; i < vCount; i++)
            //{
            //    ILayer vLayer = axMapControl1.get_Layer(i);
            //    IFeatureLayer vFeatureLayer = vLayer as IFeatureLayer;
            //    IFeature vFeature = vFeatureLayer.FeatureClass as IFeature;
            //    if (vFeature != null)
            //    {
            //        string vName = vFeature.Class.AliasName;
            //        vName = vName.Remove(0, vName.LastIndexOf('.') + 1);
            //        if (!vSelectFeatures.ContainsKey(vName))
            //            vSelectFeatures.Add(vName, new List<IFeature>());
            //        vSelectFeatures[vName].Add(vFeature);
            //    }
            //}

            //MapQueryForm vMapQueryForm = new MapQueryForm();
            //vMapQueryForm.SelectFeatures = vSelectFeatures;
            //vMapQueryForm.Show();
        }


        public static IFeatureLayer CreateFeatureLayerInmemeory(string dataSetName, string aliasName, ISpatialReference spatialRef,
            esriGeometryType geometryType, IFields propertyFields)
        {
            IWorkspaceFactory workspaceFactory =
            new InMemoryWorkspaceFactoryClass();
            IWorkspaceName workspaceName = workspaceFactory.Create(
            "", "MyWorkspace", null, 0);
            IName name = (IName)workspaceName;
            IWorkspace inmemWor = (IWorkspace)name.Open();
            IField oField = new FieldClass();
            IFields oFields = new FieldsClass();
            IFieldsEdit oFieldsEdit = null;
            IFieldEdit oFieldEdit = null;
            IFeatureClass oFeatureClass = null;
            IFeatureLayer oFeatureLayer = null;
            try
            {
                oFieldsEdit = oFields as IFieldsEdit;
                oFieldEdit = oField as IFieldEdit;
                for (int i = 0; i < propertyFields.FieldCount; i++)
                {
                    oFieldsEdit.AddField(propertyFields.get_Field(i));
                }
                IGeometryDef geometryDef = new GeometryDefClass();
                IGeometryDefEdit geometryDefEdit = (IGeometryDefEdit)geometryDef;
                geometryDefEdit.AvgNumPoints_2 = 5;
                geometryDefEdit.GeometryType_2 = geometryType;
                geometryDefEdit.GridCount_2 = 1;
                geometryDefEdit.HasM_2 = false;
                geometryDefEdit.HasZ_2 = false;
                geometryDefEdit.SpatialReference_2 = spatialRef;
                oFieldEdit.Name_2 = "SHAPE";
                oFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                oFieldEdit.GeometryDef_2 = geometryDef;
                oFieldEdit.IsNullable_2 = true;
                oFieldEdit.Required_2 = true;
                oFieldsEdit.AddField(oField);
                oFeatureClass = (inmemWor as IFeatureWorkspace).CreateFeatureClass(
                dataSetName, oFields, null, null,
                esriFeatureType.esriFTSimple, "SHAPE", "");
                (oFeatureClass as IDataset).BrowseName = dataSetName;
                oFeatureLayer = new FeatureLayerClass();
                oFeatureLayer.Name = aliasName;
                oFeatureLayer.FeatureClass = oFeatureClass;
            }
            catch
            {
            }
            finally
            {
                try
                {
                    Marshal.ReleaseComObject(oField);
                    Marshal.ReleaseComObject(oFields);
                    Marshal.ReleaseComObject(oFieldsEdit);
                    Marshal.ReleaseComObject(oFieldEdit);
                    Marshal.ReleaseComObject(name);
                    Marshal.ReleaseComObject(workspaceFactory);
                    Marshal.ReleaseComObject(workspaceName);
                    Marshal.ReleaseComObject(inmemWor);
                    Marshal.ReleaseComObject(oFeatureClass);
                }
                catch { }
                GC.Collect();
            }
            return oFeatureLayer;
        }

        private void toolStripMenuItem_FileQuery_Click(object sender, EventArgs e)
        {
            FileQueryMode();
        }

      

        private void toolStripMenuItem_MapQuery_Click(object sender, EventArgs e)
        {
            MapQueryMode();
        }

       

        private void ToolStripMenuItem_VillagePic_Click(object sender, EventArgs e)
        {
            VillagePicForm vVillagePicForm = new VillagePicForm();
            vVillagePicForm.TownshipFeatureLayer = m_TownshipFeatureLayer;
            vVillagePicForm.VillageCommitteeFeatureLayer = m_VillageCommitteeFeatureLayer;
            vVillagePicForm.VillageFeatureLayer = m_VillageFeatureLayer;
            vVillagePicForm.Main_Form = this;
            vVillagePicForm.ShowDialog();
        }

        public void locationVillage(string VillageCommittee, string villageName)
        {
            QueryFilterClass vQueryFilter = new QueryFilterClass();

            //vQueryFilter.WhereClause = string.Format("村委会_dwg = '{0}'", VillageCommittee);
            //IFeatureCursor vSerachResult_VillageCommittee = m_VillageCommitteeFeatureLayer.Search(vQueryFilter, true);
            //IFeature vFeature_VillageCommittee = vSerachResult_VillageCommittee.NextFeature();

            vQueryFilter.WhereClause = string.Format("text = '{0}'", villageName);
            IFeatureCursor vSerachResult_Village = m_VillageFeatureLayer.Search(vQueryFilter, true);
            IFeature vFeature_Village = vSerachResult_Village.NextFeature();

            if (vFeature_Village != null)
            {
                axMapControl1.Extent = vFeature_Village.Shape.Envelope;
                //axMapControl1.Refresh();
                //axMapControl1.FlashShape(vFeature_Village.Shape);
            }

            //if (vFeature_Village != null)
            //{
            //    //axMapControl1.Map.SelectFeature(m_VillageFeatureLayer, vFeature);

            //    //ESRI.ArcGIS.Geometry.Point centerpoint = ESRI.ArcGIS.Geometry.GetCenterPoint(geo);
            //    //Map1.CenterAt(centerpoint);
            //    axMapControl1.FlashShape(vFeature_Village.Shape);
            //    IPoint vPoint = new PointClass();
            //    vPoint.X = vFeature_Village.Shape.Envelope.XMin;
            //    vPoint.Y = vFeature_Village.Shape.Envelope.YMin;
            //    axMapControl1.CenterAt(vPoint);
            //    //axMapControl1.Extent = axMapControl1.FullExtent;
            //}
            
        }

        public void OutPic(string FileName)
        {
            IActiveView vAciteView;
            IExport vExport;
            IPrintAndExport vPrintAndExport;
            int vOutputResolution = 300;
            vAciteView = axMapControl1.ActiveView;
            vExport = new ExportJPEGClass();
            vPrintAndExport = new PrintAndExportClass();
            vExport.ExportFileName = FileName;
            vPrintAndExport.Export(vAciteView, vExport, vOutputResolution, true, null);
        }

        private void axMapControl1_OnKeyDown(object sender, IMapControlEvents2_OnKeyDownEvent e)
        {
            //switch (e.keyCode)
            //{
            //    case (int)System.Windows.Forms.Keys.Up:
            //        PanMap(0d, 0.5d);
            //        break;
            //    case (int)System.Windows.Forms.Keys.Down:
            //        PanMap(0d, -0.5d);
            //        break;
            //    case (int)System.Windows.Forms.Keys.Left:
            //        PanMap(-0.5d, 0d);
            //        break;
            //    case (int)System.Windows.Forms.Keys.Right:
            //        PanMap(0.5d, 0d);
            //        break;
            //}
        }

        private void PanMap(double ratioX, double ratioY)
        {
            //Pans map by amount specified given in a fraction of the extent e.g. rationX=0.5, pan right by half a screen   
            IEnvelope envelope = axMapControl1.Extent;
            double h = envelope.Width;
            double w = envelope.Height;
            envelope.Offset(h * ratioX, w * ratioY);
            axMapControl1.Extent = envelope;
        }


        public void Buffer(IFeatureLayer FeatureLayer, IFeature Feature, int Size, IMap Map)
        {
            IGeometry vGeo = Feature.Shape;
            ITopologicalOperator vIPTO = (ITopologicalOperator)vGeo;
            IGeometry vGeoBuffer = vIPTO.Buffer(Size);

            ISpatialFilter vSpatialFilter = new SpatialFilter();
            vSpatialFilter.Geometry = vGeoBuffer;
            vSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIndexIntersects;

            IFeatureSelection vFeatSelect = (IFeatureSelection)FeatureLayer;
            vFeatSelect.SelectFeatures(vSpatialFilter, esriSelectionResultEnum.esriSelectionResultNew, false);
        }

        private void ToolStripMenuItem_Pic_Browse_Click(object sender, EventArgs e)
        {
            MapToolsForm vMapToolsForm = new MapToolsForm();
            vMapToolsForm.axMapControl1 = axMapControl1;
            vMapToolsForm.Show();
        }

        private void ToolStripMenuItem_Pic_Statistics_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItem_FeatureSelect_Click(object sender, EventArgs e)
        {
            featureSelectMode();
        }

        void featureSelectMode()
        {
            toolStripMenuItem_FeatureSelect.Checked = true;

            toolStripMenuItem_MapQuery.Checked = !toolStripMenuItem_FeatureSelect.Checked;
            ToolStripMenuItem_Pic_Map.Checked = !toolStripMenuItem_FeatureSelect.Checked;

            toolStripMenuItem_FileQuery.Checked = !toolStripMenuItem_FeatureSelect.Checked;
            ToolStripMenuItem_Doc_FileQuery.Checked = !toolStripMenuItem_FeatureSelect.Checked;

            toolStripMenuItem_SpaceAnalyze.Checked = !toolStripMenuItem_FeatureSelect.Checked;
            ToolStripMenuItem_Pic_Buffer.Checked = !toolStripMenuItem_FeatureSelect.Checked;
        }
        void MapQueryMode()
        {
            toolStripMenuItem_MapQuery.Checked = true;
            ToolStripMenuItem_Pic_Map.Checked = true;

            toolStripMenuItem_FeatureSelect.Checked = !toolStripMenuItem_MapQuery.Checked;

            toolStripMenuItem_FileQuery.Checked = !toolStripMenuItem_MapQuery.Checked;
            ToolStripMenuItem_Doc_FileQuery.Checked = !toolStripMenuItem_MapQuery.Checked;

            toolStripMenuItem_SpaceAnalyze.Checked = !toolStripMenuItem_MapQuery.Checked;
            ToolStripMenuItem_Pic_Buffer.Checked = !toolStripMenuItem_MapQuery.Checked;
        }


        void SpaceAnalyzeMode()
        {
            toolStripMenuItem_SpaceAnalyze.Checked = true;
            ToolStripMenuItem_Pic_Buffer.Checked = true;

            toolStripMenuItem_FeatureSelect.Checked = !toolStripMenuItem_SpaceAnalyze.Checked;

            toolStripMenuItem_MapQuery.Checked = !toolStripMenuItem_SpaceAnalyze.Checked;
            ToolStripMenuItem_Pic_Map.Checked = !toolStripMenuItem_SpaceAnalyze.Checked;

            toolStripMenuItem_FileQuery.Checked = !toolStripMenuItem_SpaceAnalyze.Checked;
            ToolStripMenuItem_Doc_FileQuery.Checked = !toolStripMenuItem_SpaceAnalyze.Checked;
        }

        void FileQueryMode()
        {
            toolStripMenuItem_FileQuery.Checked = true;
            ToolStripMenuItem_Doc_FileQuery.Checked = true;

            toolStripMenuItem_MapQuery.Checked = !toolStripMenuItem_FileQuery.Checked;
            ToolStripMenuItem_Pic_Map.Checked = !toolStripMenuItem_FileQuery.Checked;

            toolStripMenuItem_FeatureSelect.Checked = !toolStripMenuItem_FileQuery.Checked;

            toolStripMenuItem_SpaceAnalyze.Checked = !toolStripMenuItem_FileQuery.Checked;
            ToolStripMenuItem_Pic_Buffer.Checked = !toolStripMenuItem_FileQuery.Checked;
        }

        private void toolStripMenuItem_SpaceAnalyze_Click(object sender, EventArgs e)
        {
            SpaceAnalyzeMode();
        }



        private void ToolStripMenuItem_Doc_FileQuery_Click(object sender, EventArgs e)
        {
            FileQueryMode();
            viewFiles();
        }

        #region 标注
        /// <summary>
        /// 标注图层
        /// </summary>
        /// <param name="pFeaturelayer"></param>
        /// <param name="sLableField"></param>
        /// <param name="pRGB"></param>
        /// <param name="size"></param>
        /// <param name="angleField"></param>
        public void EnableFeatureLayerLabel( string LayerName, string sLableField, 
            IRgbColor pRGB, int size)
        {

            for( int i=0;i< axMapControl1.LayerCount;i++)
            {
                ILayer vLayer = axMapControl1.get_Layer(i);
                IFeatureLayer vFeatureLayer = vLayer as IFeatureLayer;
                string vLayerName = fixLayerName(vFeatureLayer);
                if (LayerName== vLayerName)
                {
                    IFeatureLayer pFeaturelayer = vFeatureLayer;
                    //判断图层是否为空
                    if (pFeaturelayer == null)
                        return;
                    IGeoFeatureLayer pGeoFeaturelayer = (IGeoFeatureLayer)pFeaturelayer;
                    IAnnotateLayerPropertiesCollection pAnnoLayerPropsCollection;
                    pAnnoLayerPropsCollection = pGeoFeaturelayer.AnnotationProperties;
                    pAnnoLayerPropsCollection.Clear();

                    //stdole.IFontDisp  pFont; //字体
                    ITextSymbol pTextSymbol;

                    //pFont.Name = "新宋体";
                    //pFont.Size = 9;
                    //未指定字体颜色则默认为黑色
                    if (pRGB == null)
                    {
                        pRGB = new RgbColorClass();
                        pRGB.Red = 0;
                        pRGB.Green = 0;
                        pRGB.Blue = 0;
                    }

                    pTextSymbol = new TextSymbolClass();
                    pTextSymbol.Color = (IColor)pRGB;
                    pTextSymbol.Size = size; //标注大小

                    IBasicOverposterLayerProperties4 pBasicOverposterlayerProps4 = new BasicOverposterLayerPropertiesClass();
                    switch (pFeaturelayer.FeatureClass.ShapeType)//判断图层类型
                    {
                        case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
                            pBasicOverposterlayerProps4.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolygon;
                            break;
                        case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
                            pBasicOverposterlayerProps4.FeatureType = esriBasicOverposterFeatureType.esriOverposterPoint;
                            break;
                        case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
                            pBasicOverposterlayerProps4.FeatureType = esriBasicOverposterFeatureType.esriOverposterPolyline;
                            break;
                    }
                    pBasicOverposterlayerProps4.PointPlacementMethod = esriOverposterPointPlacementMethod.esriRotationField;
                    //pBasicOverposterlayerProps4.RotationField = angleField;

                    ILabelEngineLayerProperties pLabelEnginelayerProps = new LabelEngineLayerPropertiesClass();
                    pLabelEnginelayerProps.Expression = "[" + sLableField + "]";
                    pLabelEnginelayerProps.Symbol = pTextSymbol;
                    pLabelEnginelayerProps.BasicOverposterLayerProperties = pBasicOverposterlayerProps4 as IBasicOverposterLayerProperties;
                    pAnnoLayerPropsCollection.Add((IAnnotateLayerProperties)pLabelEnginelayerProps);
                    pGeoFeaturelayer.DisplayAnnotation = true;//很重要，必须设置 
                    axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
                    break;
                }
            }
            //axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null); }
        }


        public void DisableFeatureLayerLabel(string LayerName)
        {

            for (int i = 0; i < axMapControl1.LayerCount; i++)
            {
                ILayer vLayer = axMapControl1.get_Layer(i);
                IFeatureLayer vFeatureLayer = vLayer as IFeatureLayer;
                string vLayerName = fixLayerName(vFeatureLayer);
                if (LayerName == vLayerName)
                {
                    IFeatureLayer pFeaturelayer = vFeatureLayer;
                    //判断图层是否为空
                    if (pFeaturelayer == null)
                        return;
                    IGeoFeatureLayer pGeoFeaturelayer = (IGeoFeatureLayer)pFeaturelayer;
                    pGeoFeaturelayer.DisplayAnnotation = false;//很重要，必须设置 
                    axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null);
                    break;
                }
            }
            //axMapControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewBackground, null, null); }
        }
        #endregion

        /// <summary>
        /// 改变层的索引
        /// </summary>
        /// <param name="FromIndex"></param>
        /// <param name="ToIndex"></param>
        public void ChangeLayerIndex(int FromIndex,int ToIndex )
        {

            FromIndex += 2;
            ToIndex += 2;
            axMapControl1.MoveLayerTo(FromIndex, ToIndex);
            axMapControl1.Refresh();
        }

        private void ToolStripMenuItem_Pic_Import_Click(object sender, EventArgs e)
        {
            OpenFileDialog vOpenFileDialog = new OpenFileDialog();
            vOpenFileDialog.Filter = "shp files (*.shp)|*.shp";
            vOpenFileDialog.Multiselect = false;
            if ( vOpenFileDialog.ShowDialog() == DialogResult.OK )
            {
                string vShpFile = vOpenFileDialog.FileName;
                int vLateIndex = vShpFile.LastIndexOf('\\');
                string vFilePath = vShpFile.Substring(0, vLateIndex);
                string vFileName = vShpFile.Substring(vLateIndex + 1);
                axMapControl1.AddShapeFile(vFilePath, vFileName);
                string vLayerName = vFileName.Substring(0, vFileName.LastIndexOf('.'));
                ILayer vLayer =  GetLayerFromName(vLayerName);
                IEnvelope envelope = vLayer.AreaOfInterest;
                axMapControl1.Extent = envelope;
                axMapControl1.Refresh();
            }
        }

        public ILayer GetLayerFromName( string LayerName )
        {
            ILayer vLayer = null;
            for( int i=0;i<axMapControl1.LayerCount;i++)
            {
                vLayer = axMapControl1.get_Layer(i);
                IFeatureLayer vFeatureLayer = vLayer as IFeatureLayer;
                if (vFeatureLayer.Name == LayerName)
                    break;
            }
            return vLayer;
        }
    }
}
