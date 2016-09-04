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

        public string LayerName { get; set; }
        public int[] ObjectIDArray { get; set; } 

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
                    string vNodeName = string.Format("图层:【{0}】 要素类型:【{1}】", vLayer.Expository, CommonUnit.ConvertLayerType(vLayer.Type ?? 0));
                    treeView_Layer.Nodes.Add(TempDict.Key, vNodeName, vLayer.Type ?? 0);
                }
            }
        }

        private void treeView_Layer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string vKey = e.Node.Name;
            if (  SelectFeatures[vKey].Count > 0 )
            {
                DataTable vTable = createFeaturesTableStruct(SelectFeatures[vKey][0]);
                for( int i=0;i< SelectFeatures[vKey].Count;i++)
                {
                    DataRow vNewRow = vTable.NewRow();
                    for( int j=0;j< vTable.Columns.Count;j++)
                    {

                        if (SelectFeatures[vKey][i].Fields.Field[j].Name != "Shape")
                        {
                            object vFieldValue = SelectFeatures[vKey][i].get_Value(j);
                            vNewRow[j] = vFieldValue;
                        }
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
                if (vField.AliasName != "Shape")
                    vTable.Columns.Add(vField.AliasName, CommonUnit.ConvertFeaturesFieldType(vField.Type));
            }
            vTable.AcceptChanges();
            return vTable;
        }

        private void button_Query_Click(object sender, EventArgs e)
        {
            string vKeyWord = textBox_KeyWord.Text;
            if (vKeyWord != "")
            {
                int vOK = 0;
                foreach (DataGridViewRow vTempRow in dataGridView_Data.Rows)
                {
                    bool vVisible = false;
                    for (int i = 0; i < dataGridView_Data.Columns.Count; i++)
                    {
                        string vCellValue = vTempRow.Cells[i].Value.ToString()??"";
                        if (vCellValue!="" && vCellValue.IndexOf(vKeyWord) != -1)
                        {
                            vVisible = true;
                            vOK++;
                            break;
                        }
                    }
                    vTempRow.Visible = vVisible;
                }
                if (vOK > 0)
                    button_Location.Enabled = true;
                else
                    button_Location.Enabled = false;
            }
            else
            {
                foreach (DataGridViewRow vTempRow in dataGridView_Data.Rows)
                {
                    vTempRow.Visible = true;
                }
                button_Location.Enabled = false;
            }
        }

        private void button_Location_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            List<int> vObjectList = new List<int>();
            foreach (DataGridViewRow vTempRow in dataGridView_Data.Rows)
            {
                if ( vTempRow.Visible )
                {
                    int vObjectID = (int)vTempRow.Cells["ObjectID"].Value;
                    vObjectList.Add(vObjectID);
                }
            }
            ObjectIDArray = vObjectList.ToArray();
            LayerName = treeView_Layer.SelectedNode.Name;
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
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
