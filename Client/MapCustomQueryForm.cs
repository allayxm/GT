using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using JXDL.ClientBusiness;
using JXDL.IntrefaceStruct;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JXDL.Client
{
    public partial class MapCustomQueryForm : Form
    {
        public MainForm VMainForm { get; set; }
        public int[] ObjectIDArray { get; set; }
        public string LayerName { get; set; }
        public MapCustomQueryForm()
        {
            InitializeComponent();
        }

        private void MapCustomQueryForm_Load(object sender, EventArgs e)
        {
            init();
        }

        void init()
        {
            treeView_Layer.Nodes.Clear();
            foreach ( LayerStruct vTempLayer in VMainForm.m_Layers)
            {
                string vNodeName = string.Format("图层:【{0}】 要素类型:【{1}】", vTempLayer.Expository, CommonUnit.ConvertLayerType(vTempLayer.Type ?? 0));
                treeView_Layer.Nodes.Add(vTempLayer.Name, vNodeName, vTempLayer.Type ?? 0);
            } 
        }

        private void treeView_Layer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string vLyaerName = e.Node.Name;
            ILayer layer = VMainForm.GetLayerFromName(vLyaerName);
            IFeatureLayer vFeatureLayer = layer as IFeatureLayer;
            DataTable vTable = CommonUnit.CreateFeaturesTableStruct(vFeatureLayer.FeatureClass);
            int vFeatureCount = vFeatureLayer.FeatureClass.FeatureCount(null);
            IFeatureCursor vFeatureCursor = vFeatureLayer.FeatureClass.Search(null, true);
            IFeature vFeature = vFeatureCursor.NextFeature();
            while (vFeature != null)
            {
                DataRow vNewRow = vTable.NewRow();
                for (int j = 0; j < vTable.Columns.Count; j++)
                {

                    if (vFeatureLayer.FeatureClass.Fields.Field[j].Name != "Shape")
                    {
                        object vFieldValue = vFeature.get_Value(j);
                        vNewRow[vFeatureLayer.FeatureClass.Fields.Field[j].Name] = vFieldValue;
                    }
                }
                vTable.Rows.Add(vNewRow);
                vFeature = vFeatureCursor.NextFeature();
            }
            vTable.AcceptChanges();
            dataGridView_Data.DataSource = vTable;
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
                        string vCellValue = vTempRow.Cells[i].Value == null ? "":vTempRow.Cells[i].Value.ToString(); ;
                        if ( vCellValue != "" && vCellValue.IndexOf(vKeyWord) != -1)
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

        private void button_Exit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button_Location_Click(object sender, EventArgs e)
        {
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
    }
}
