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
using JXDL.ClientBusiness;

namespace JXDL.Client
{
    public partial class BufferForm : Form
    {
        public BufferForm()
        {
            InitializeComponent();
        }

        public Dictionary<string, BufferConfig> BufferLayers = new Dictionary<string, BufferConfig>();

        Dictionary<string, DataTable> m_AnalyzeResult = new Dictionary<string, DataTable>();

        public LayerStruct[] Layers { get; set; } 


        public MainForm VMainForm { get; set; }


        BufferConfig getBufferConfig(string LayerName)
        {
            return BufferLayers.Where(m => m.Key == LayerName).FirstOrDefault().Value;
        }

        private void button_Analysis_Click(object sender, EventArgs e)
        {
            //DataTable vTable =  (DataTable)dataGridView_Layers.DataSource;
            //BufferLayers.Clear();
            //foreach ( DataRow vTempRow in vTable.Rows)
            //{
            //    bool vSelected = (bool)vTempRow["选择"];
            //    if (vSelected)
            //    {
            //        string vLayerName = (string)vTempRow["图层名称"];
            //        int vDistance = (int)vTempRow["缓冲距离"];
            //        BufferLayers.Add(vLayerName, vDistance);
            //    }
            //}
            if (listView_Layers.Items.Count != 0)
            {

                //清空原有已分析数据
                foreach (var vTempLayers in BufferLayers)
                {
                    vTempLayers.Value.AnalyzeLayers.Clear();
                    vTempLayers.Value.AnalyzeLayers_Detail.Clear();
                }

                VMainForm.DeleteAllBufferLayers();
                short vTransparency = Convert.ToInt16(trackBar_Transparency.Value);
                string vInfo = VMainForm.CreateBufferLayerEx(BufferLayers, vTransparency);

                //显示当前节点的数据
                if (treeView_FeatureLayers.SelectedNode != null)
                    showLayerData(treeView_FeatureLayers.SelectedNode);
            }
            else
                MessageBox.Show("请选择需要进行分析图层","信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //textBox_Info.Text = "";
            //textBox_Info.Text = vInfo;
            //loadBufferLayers();
        }


        //void loadBufferLayers()
        //{
        //    listView_BufferLayers.Items.Clear();
        //    foreach (var vTempLayer in VMainForm.m_BufferLayers)
        //    {
        //        ListViewItem vNewItem = new ListViewItem();
        //        vNewItem.Checked = vTempLayer.IsView;
        //        vNewItem.SubItems.Add(vTempLayer.Name);
        //        listView_BufferLayers.Items.Add(vNewItem);
                
        //    }
        //}
       

        private void button_Exit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

       

        private void BufferForm_Load(object sender, EventArgs e)
        {
            //toolStripMenuItem_Delete.Click += ToolStripMenuItem_Delete_Click;
            //initSelectedLayers();
            initTreeView();
            initcomboBox_Layers();

            //loadBufferLayers();
        }

        ComboBoxListItem[] m_ComboBoxLayerItmes;
        void initcomboBox_Layers()
        {
            m_ComboBoxLayerItmes = new ComboBoxListItem[VMainForm.m_Layers.Count];
            for (int i=0;i< VMainForm.m_Layers.Count; i++ )
            {
                ComboBoxListItem vNewItme = new ComboBoxListItem()
                {
                    Name = string.Format("{0}({1})", VMainForm.m_Layers[i].Expository,CommonUnit.ConvertLayerType(VMainForm.m_Layers[i].Type??0)) ,
                    Value = VMainForm.m_Layers[i].Name
                };
                m_ComboBoxLayerItmes[i] = vNewItme;
            }
            comboBox_Layers.Items.AddRange(m_ComboBoxLayerItmes);
        }

        public void initTreeView()
        {
            treeView_FeatureLayers.Nodes.Clear();
            foreach (var vTempDict in BufferLayers)
            {
                
                TreeNode vNewNode = new TreeNode()
                {
                    Checked = vTempDict.Value.IsSelect,
                    Name = vTempDict.Key,
                    ImageIndex = getLayerType(vTempDict.Key),
                    Text = string.Format("{0}({1})", getLayerAliasName(vTempDict.Key), CommonUnit.ConvertLayerType(getLayerType(vTempDict.Key))),
                    SelectedImageIndex = getLayerType(vTempDict.Key)
                };
                vTempDict.Value.Expository = vNewNode.Text;
                treeView_FeatureLayers.Nodes.Add(vNewNode);
            }
            if (treeView_FeatureLayers.Nodes.Count > 0)
                treeView_FeatureLayers.SelectedNode = treeView_FeatureLayers.Nodes[0];
        }

        //public void initSelectedLayers()
        //{
        //    DataTable vTable = createTableStruct();
        //    foreach (var vTempDict in BufferLayers)
        //    {
        //        DataRow vNewRow = vTable.NewRow();
        //        vNewRow["选择"] = true;
        //        vNewRow["图层名称"] = vTempDict.Key;
        //        vNewRow["图层别名"] = getLayerAliasName(vTempDict.Key);
        //        vNewRow["图层类型"] = getLayerType(vTempDict.Key);
        //        vNewRow["缓冲距离"] = vTempDict.Value;
        //        vTable.Rows.Add(vNewRow);
        //    }
        //    vTable.AcceptChanges();
        //    dataGridView_Layers.AutoGenerateColumns = false;
        //    dataGridView_Layers.DataSource = vTable;
        //}

        //private void ToolStripMenuItem_Delete_Click(object sender, EventArgs e)
        //{
        //    if ( listView_BufferLayers.SelectedItems.Count>0 )
        //    {
                
        //        foreach ( ListViewItem vItem in listView_BufferLayers.SelectedItems)
        //        {
        //            string vLayerName = vItem.SubItems[1].Text;
        //            VMainForm.DeleteLayer(vLayerName);
        //            listView_BufferLayers.Items.Remove(vItem);
        //        }
        //    }
        //}

        int getLayerType(string layerName)
        {
            int vResult =0;
            LayerStruct vLayer = Layers.Where(m => m.Name == layerName).FirstOrDefault();
            if (vLayer!=null)
                vResult = vLayer.Type.Value;
            return vResult;
        }
        string getLayerAliasName( string layerName)
        {
            string vResult = "";
            switch( layerName)
            {
                case "村委会":
                case "自然村":
                case "乡镇街道":
                    vResult = layerName;
                    break;
                default:
                    vResult = Layers.Where(m => m.Name == layerName).FirstOrDefault().Expository;
                    break;
            }
            return vResult;
        }

        DataTable createTableStruct()
        {
            DataTable vTable = new DataTable();
            vTable.Columns.AddRange(new DataColumn[] {
                new DataColumn("选择",typeof(bool)),
                new DataColumn("图层名称",typeof(string)),
                new DataColumn("图层别名",typeof(string)),
                new DataColumn("图层类型",typeof(string)),
                new DataColumn("缓冲距离",typeof(int)),
            });
            vTable.AcceptChanges();
            return vTable;
        }

        private void listView_BufferLayers_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            string vLayerName = e.Item.SubItems[1].Text;
            VMainForm.ChangeLayerVisible(vLayerName, e.Item.Checked);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListViewItem vNewItem1 = new ListViewItem();
            vNewItem1.Checked = true;
            vNewItem1.Text = "11";

            ListViewItem vNewItem2 = new ListViewItem();
            vNewItem2.Checked = true;
            vNewItem2.Text = "22";

            //listView_BufferLayers.Items.Add(vNewItem1);
            //listView_BufferLayers.Items.Add(vNewItem2);

        }

        private void treeView_FeatureLayers_AfterSelect(object sender, TreeViewEventArgs e)
        {
            showLayerData(e.Node);
        }

        void showLayerData( TreeNode node )
        {
            BufferConfig vBufferConfig = getBufferConfig(node.Name);
            string vLayerName = string.Format("{0}_Buffer", node.Name);
            textBox_Name.Text = vBufferConfig.LayerName;
            textBox_Type.Text = CommonUnit.ConvertLayerType(getLayerType(vBufferConfig.LayerName));
            numericUpDown_Distance.Value = vBufferConfig.Distance;

            listView_Layers.Items.Clear();
            listView_Layers.Items.AddRange(vBufferConfig.SelectedLayers.ToArray());

            listView_Buffer.Items.Clear();
            listView_Buffer.Items.AddRange(vBufferConfig.AnalyzeLayers.ToArray());
            //listBox_Buffer.Items.AddRange(vBufferConfig.AnalyzeLayers.ToArray());

            dataGridView_Analyze.DataSource = null;
        }

        private void treeView_FeatureLayers_DrawNode(object sender, DrawTreeNodeEventArgs e)
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

        private void button_Add_Click(object sender, EventArgs e)
        {
            BufferConfig vBufferConfig = getBufferConfig(treeView_FeatureLayers.SelectedNode.Name );
            ComboBoxListItem vSelectedItem = (ComboBoxListItem)comboBox_Layers.SelectedItem;
            foreach( ListViewItem vTempItem in vBufferConfig.SelectedLayers )
            {
                if (vTempItem.Name == vSelectedItem.Value)
                {
                    MessageBox.Show("已存在相同的图层请重新选择", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            ListViewItem vNewItem = new ListViewItem()
            {
                Name = vSelectedItem.Value,
                Text = vSelectedItem.Name,
            };
            vBufferConfig.SelectedLayers.Add(vNewItem);
            listView_Layers.Clear();
            listView_Layers.Items.AddRange(vBufferConfig.SelectedLayers.ToArray());
            listView_Layers.Refresh();
        }

        private void listBox_Buffer_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void numericUpDown_Distance_ValueChanged(object sender, EventArgs e)
        {
            BufferConfig vBufferConfig = getBufferConfig( treeView_FeatureLayers.SelectedNode.Name );
            vBufferConfig.Distance = Convert.ToInt32( numericUpDown_Distance.Value );
        }

        private void button_Remove_Click(object sender, EventArgs e)
        {
            if (listView_Layers.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择需要删除的图层", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            BufferConfig vBufferConfig = getBufferConfig( treeView_FeatureLayers.SelectedNode.Name );
            vBufferConfig.SelectedLayers.Remove(listView_Layers.SelectedItems[0]);
            listView_Layers.Items.Remove(listView_Layers.SelectedItems[0]);
        }

        private void treeView_FeatureLayers_AfterCheck(object sender, TreeViewEventArgs e)
        {
            BufferConfig vBufferConfig = getBufferConfig(e.Node.Name);
            vBufferConfig.IsSelect = e.Node.Checked;
            if (vBufferConfig.BufferLayerName!=null && vBufferConfig.BufferLayerName != "")
                VMainForm.ChangeLayerVisible(vBufferConfig.BufferLayerName, vBufferConfig.IsSelect);
        }

        private void listView_Buffer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView_Buffer.SelectedItems.Count > 0)
            {
                ListViewItem vSelectedItem = (ListViewItem)listView_Buffer.SelectedItems[0];
                BufferConfig vBufferConfig = getBufferConfig(treeView_FeatureLayers.SelectedNode.Name);
                foreach (DataTable vTable in vBufferConfig.AnalyzeLayers_Detail)
                {
                    if (vTable.TableName == vSelectedItem.Name)
                    {
                        dataGridView_Analyze.DataSource = vTable;
                        break;
                    }
                }
            }
        }

        private void BufferForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            VMainForm.DeleteAllBufferLayers();
        }

        private void checkBox_Select_CheckedChanged(object sender, EventArgs e)
        {
            foreach( TreeNode vTempNode in treeView_FeatureLayers.Nodes)
            {
                vTempNode.Checked = checkBox_Select.Checked;
            }
        }

        private void trackBar_Transparency_ValueChanged(object sender, EventArgs e)
        {
            label_Transparency.Text = string.Format("{0}%", trackBar_Transparency.Value);
        }
    }
}
