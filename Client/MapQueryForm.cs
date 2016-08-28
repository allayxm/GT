using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using JXDL.IntrefaceStruct;
using JXDL.ClientBusiness;

namespace JXDL.Client
{
    public partial class MapQueryForm : Form
    {
        public MapQueryForm()
        {
            InitializeComponent();
        }

        public Dictionary<string, List<IFeature>> SelectFeatures = new Dictionary<string, List<IFeature>>();

        private void MapQueryForm_Load(object sender, EventArgs e)
        {
            init();   
        }

        void init()
        {
            RemoteInterface vRemoteInterface = new RemoteInterface();
            LayerStruct[] vLayers = vRemoteInterface.GetLayers();
            foreach ( var TempDict in SelectFeatures )
            {
                LayerStruct vLayer = vLayers.Where(m => m.Name == TempDict.Key).FirstOrDefault();
                if (vLayer != null)
                {
                    string vNodeName = string.Format("要素名称:{0} 要素类型:{1}", vLayer.Name, CommonUnit.ConvertLayerType(vLayer.Type ?? 0));
                    treeView_Layer.Nodes.Add(TempDict.Key, vNodeName);
                }
            }
        }

        private void treeView_Layer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string vKey = (string)e.Node.Tag;
            if (  SelectFeatures[vKey].Count > 0 )
            {
                DataTable vTable = createFeaturesTableStruct(SelectFeatures[vKey][0]);
                for( int i=0;i< SelectFeatures[vKey].Count;i++)
                {
                    DataRow vNewRow = vTable.NewRow();
                    for( int j=0;j< vTable.Columns.Count;j++)
                    {
                        object vFieldValue = SelectFeatures[vKey][i].get_Value(j);
                        vNewRow[j] = vFieldValue;
                    }
                    vTable.Rows.Add(vNewRow);
                }
                vTable.AcceptChanges();
                dataGridView_Data.DataSource = vTable;
            }
        }

        DataTable createFeaturesTableStruct(IFeature feature)
        {
            DataTable vTable = new DataTable();
            for( int i=0;i<feature.Fields.FieldCount;i++ )
            {
                IField vField = feature.Fields.get_Field(i);
                vTable.Columns.Add(vField.AliasName, CommonUnit.ConvertFeaturesFieldType(vField.Type));
            }
            vTable.AcceptChanges();
            return vTable;
        }

        //Dictionary<string,LayerStruct> getLayersData()
        // {
        //     Dictionary<string, string[]> vResult = new Dictionary<string, string[]>();

        //     vLayers.Select(m=>m.Name)
        //     foreach (LayerStruct vTempLayer in )
        //     return vResult;
        // }
    }
}
