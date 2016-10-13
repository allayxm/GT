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

        public MainForm VMainForm { get; set; }

        LayerStruct[] m_Layers;
        private void MapQueryForm_Load(object sender, EventArgs e)
        {
            RemoteInterface vRemoteInterface = new RemoteInterface();
            m_Layers = vRemoteInterface.GetLayers();
            InitFeatureLayers();   
        }

        public void InitFeatureLayers()
        {
            treeView_Layer.Nodes.Clear();
            foreach ( var TempDict in SelectFeatures )
            {
                LayerStruct vLayer = m_Layers.Where(m => m.Name == TempDict.Key).FirstOrDefault();
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
                DataTable vTable = CommonUnit.CreateFeaturesTableStruct(SelectFeatures[vKey][0]);
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

        //DataTable createFeaturesTableStruct(IFeature feature)
        //{
        //    DataTable vTable = new DataTable();
        //    for( int i=0;i<feature.Fields.FieldCount;i++ )
        //    {
        //        IField vField = feature.Fields.get_Field(i);
        //        if (vField.AliasName != "Shape")
        //            vTable.Columns.Add(vField.AliasName, CommonUnit.ConvertFeaturesFieldType(vField.Type));
        //    }
        //    vTable.AcceptChanges();
        //    return vTable;
        //}

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
                    CurrencyManager cm = (CurrencyManager)BindingContext[vTempRow.DataGridView.DataSource];//
                    cm.SuspendBinding(); //挂起，这行必需有
                    vTempRow.Visible = vVisible;
                    cm.ResumeBinding();//继续，这行必需有
                }
                //if (vOK > 0)
                //    button_Location.Enabled = true;
                //else
                //    button_Location.Enabled = false;
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
            //DialogResult = DialogResult.Yes;
            if (dataGridView_Data.SelectedRows != null)
            {
                List<int> vObjectList = new List<int>();
                foreach (DataGridViewRow vTempRow in dataGridView_Data.SelectedRows)
                {
                    if (vTempRow.Visible)
                    {
                        int vObjectID = (int)vTempRow.Cells["ObjectID"].Value;
                        vObjectList.Add(vObjectID);
                    }
                }
                ObjectIDArray = vObjectList.ToArray();
                LayerName = treeView_Layer.SelectedNode.Name;
                VMainForm.SelectFeatures(ObjectIDArray, LayerName);
            }
            else
            {
                MessageBox.Show("请选择需要定位的要素", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void treeView_Layer_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true; //我这里用默认颜色即可，只需要在TreeView失去焦点时选中节点仍然突显
            //return;

            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                //演示为绿底白字
                e.Graphics.FillRectangle(Brushes.DarkBlue, e.Node.Bounds);

                Font nodeFont = e.Node.NodeFont;
                if (nodeFont == null) nodeFont = ((TreeView)sender).Font;
                e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.White, Rectangle.Inflate(e.Bounds, 2, 0));
            }
            else
            {
                e.DrawDefault = true;
            }

            if ((e.State & TreeNodeStates.Focused) != 0)
            {
                using (Pen focusPen = new Pen(Color.Black))
                {
                    focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    Rectangle focusBounds = e.Node.Bounds;
                    focusBounds.Size = new Size(focusBounds.Width - 1,
                    focusBounds.Height - 1);
                    e.Graphics.DrawRectangle(focusPen, focusBounds);
                }
            }
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
