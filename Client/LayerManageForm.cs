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
using JXDL.IntrefaceStruct;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;

namespace JXDL.Client
{
    public partial class LayerManageForm : Form
    {
        public LayerStruct[] Layers { get; set; }

        public MainForm VMainForm { get; set; }
        public LayerManageForm()
        {
            InitializeComponent();
        }

        private void LayerManageForm_Load(object sender, EventArgs e)
        {
            if (Layers != null)
            {
                Layers = Layers.OrderBy(m => m.Order).ToArray();
                initTreeView();
            }
        }

        void initTreeView()
        {
            if (Layers != null)
            {
                treeView_Layers.Nodes.Clear();
                foreach (LayerStruct vTempLayer in Layers)
                {
                    TreeNode vNewNode = new TreeNode();
                    string vType = CommonUnit.ConvertLayerType(vTempLayer.Type.Value);
                    vNewNode.SelectedImageIndex = vTempLayer.Type ?? 0;
                    vNewNode.ImageIndex = vTempLayer.Type??0;
                    vNewNode.Text = vTempLayer.Expository+"("+vType+")";
                    vNewNode.Tag = vTempLayer.ID;
                    vNewNode.Name = vTempLayer.ID.ToString();
                    vNewNode.Checked = vTempLayer.IsView;
                    
                    if (vTempLayer.Color != -1)
                        vNewNode.ForeColor = Color.FromArgb( vTempLayer.Color );
                    treeView_Layers.Nodes.Add(vNewNode);
                }
                if ( treeView_Layers.Nodes.Count > 0 )
                    treeView_Layers.SelectedNode = treeView_Layers.Nodes[0];
            }
        }

        //string convertLayerType( int type)
        //{
        //    string vResult = "";
        //    switch (type)
        //    {
        //        case 0:
        //            vResult = "点";
        //            break;
        //        case 1:
        //            vResult = "线";
        //            break;
        //        case 2:
        //            vResult = "面";
        //            break;
        //    }
        //    return vResult;
        //}

        private void button_Save_Click(object sender, EventArgs e)
        {
            //foreach( ListViewItem vTempItem in listView_Layer.Items )
            //{
            //    int vID = int.Parse( vTempItem.SubItems[1].Text );
            //    Layers.Where(m => m.ID == vID).FirstOrDefault().IsView = vTempItem.Checked;
            //    Layers.Where(m => m.ID == vID).FirstOrDefault().Color = vTempItem.SubItems[4].BackColor.ToArgb();
            //}
            saveConfigFile();
            DialogResult = DialogResult.OK;
            Close();
        }

        void saveConfigFile()
        {
            //Dictionary<string, int> vLayerConfig = new Dictionary<string, int>();
            //foreach( LayerStruct vTempLayer in Layers )
            //{
            //    if ( vTempLayer.Color != 0 )
            //    {
            //        vLayerConfig.Add(vTempLayer.Name, vTempLayer.Color);
            //    }
            //}
            ConfigFile vConfigFile = new ConfigFile();
            vConfigFile.LayerConfig = Layers;
            vConfigFile.Save();
        }

        private void listView_Layer_DoubleClick(object sender, EventArgs e)
        {
          
        }

        private void listView_Layer_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int vID = int.Parse(e.Item.SubItems[1].Text);
            string vLayerName = Layers.Where(m => m.ID == vID).FirstOrDefault().Name;
            //Layers.Where(m => m.ID == vID).FirstOrDefault().IsView = e.Item.Checked;
           VMainForm.ChangeLayerVisible(vLayerName, e.Item.Checked);
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void treeView_Layers_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int vID = (int)e.Node.Tag;
            LayerStruct vLayer =  Layers.Where(m => m.ID == vID).FirstOrDefault();
            if (vLayer.Type == 3)
                tabControl_Layer.Enabled = false;
            else
                tabControl_Layer.Enabled = true;
            if ( vLayer != null )
            {
                textBox_Name.Text = vLayer.Name;
                textBox_Expository.Text = vLayer.Expository;
                textBox_Type.Text = CommonUnit.ConvertLayerType(vLayer.Type.Value);
                if (vLayer.Color != -1)
                    label_Color.BackColor = Color.FromArgb(vLayer.Color);
                else
                    label_Color.BackColor = Color.White;
               

                checkBox_Annotation.Checked = vLayer.ShowAnnotation;

                string[] vColumnArray = VMainForm.GetColumns(vLayer.Name);
                comboBox_Label.DataSource = vColumnArray;
                //comboBox_Label.Text = vLayer.AnnotationField;
                textBox_Express.Text = vLayer.AnnotationField;

                if ( vLayer.AnnotationFontColor != -1)
                    label_AnnotationColor.BackColor = Color.FromArgb(vLayer.AnnotationFontColor);
                else
                    label_AnnotationColor.BackColor = Color.White;

                comboBox_FontSize.Text = vLayer.AnnotationFontSize.ToString();

                //层透明度
                trackBar_Transparency.Value = vLayer.Transparency;
                label_Transparency.Text = string.Format("{0}%", vLayer.Transparency);

                //表数据
                ILayer layer =  VMainForm.GetLayerFromName(vLayer.Name);
                IFeatureLayer vFeatureLayer = layer as IFeatureLayer;
                DataTable vTable = CommonUnit.CreateFeaturesTableStruct(vFeatureLayer.FeatureClass);
                int vFeatureCount = vFeatureLayer.FeatureClass.FeatureCount(null);
                IFeatureCursor vFeatureCursor = vFeatureLayer.FeatureClass.Search(null, true);
                IFeature vFeature = vFeatureCursor.NextFeature();
                while(vFeature != null )
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


                button_Apply.Enabled = true;


            }
        }

        private void textBox_Color_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void treeView_Layers_AfterCheck(object sender, TreeViewEventArgs e)
        {
            int vID = (int)e.Node.Tag;
            LayerStruct vLayer = Layers.Where(m => m.ID == vID).FirstOrDefault();
            if (vLayer != null)
            {
                VMainForm.ChangeLayerVisible(vLayer.Name, e.Node.Checked);
                vLayer.IsView = e.Node.Checked;
            }
        }

        private void label_Color_DoubleClick(object sender, EventArgs e)
        {
            changeLayerColor();
        }

        void changeLayerColor()
        {
            ColorDialog vColorDialog = new ColorDialog();
            if (vColorDialog.ShowDialog() == DialogResult.OK)
            {
                label_Color.BackColor = vColorDialog.Color;
                label_Color.Tag = vColorDialog.Color.ToArgb();
                button_Apply.Enabled = true;
            }
        }

        private void button_Apply_Click(object sender, EventArgs e)
        {
            int vID = int.Parse(treeView_Layers.SelectedNode.Name);
            LayerStruct vLayer = Layers.Where(m => m.ID == vID).FirstOrDefault();
            string vLayerName = vLayer.Name;

            int vLayerColor = label_Color.Tag == null ? 0 : (int)label_Color.Tag;
            if (vLayerColor != 0)
            {
                VMainForm.ChangeLayerColor(vLayerName, vLayerColor);
                vLayer.Color = vLayerColor;
                treeView_Layers.SelectedNode.ForeColor = Color.FromArgb(vLayerColor);
                label_Color.BackColor = treeView_Layers.SelectedNode.ForeColor;
            }

            vLayer.ShowAnnotation = checkBox_Annotation.Checked;
            // string vAnnotationField = comboBox_Label.Text;
            string vAnnotationField = textBox_Express.Text;
            vLayer.AnnotationField = vAnnotationField;

            int vAnnotationColor = label_AnnotationColor.Tag == null ? -1 : (int)label_AnnotationColor.Tag;
            vLayer.AnnotationFontColor = vAnnotationColor;
            IRgbColor vRgbColor = vAnnotationColor == -1 ? null : CommonUnit.ColorToIRgbColor(Color.FromArgb(vAnnotationColor));

            int vFontSize = int.Parse(comboBox_FontSize.Text);
            vLayer.AnnotationFontSize = vFontSize;
            if (vLayer.ShowAnnotation)
                VMainForm.EnableFeatureLayerLabel(vLayerName, vAnnotationField, vRgbColor, vFontSize);
            else
                VMainForm.DisableFeatureLayerLabel(vLayerName);

            //改变图层透明度
            short vTransparency = Convert.ToInt16( trackBar_Transparency.Value );
            vLayer.Transparency = vTransparency;
            VMainForm.ChangeLayerTransparency(vLayerName,vTransparency);

            button_Apply.Enabled = false;
        }

        private void label_AnnotationColor_DoubleClick(object sender, EventArgs e)
        {
            changeAnnotationColor();
        }

        void changeAnnotationColor()
        {
            ColorDialog vColorDialog = new ColorDialog();
            if (vColorDialog.ShowDialog() == DialogResult.OK)
            {
                label_AnnotationColor.BackColor = vColorDialog.Color;
                label_AnnotationColor.Tag = vColorDialog.Color.ToArgb();
                button_Apply.Enabled = true;
            }
        } 

        private void treeView_Layers_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode moveNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");

            //根据鼠标坐标确定要移动到的目标节点
            Point pt;
            TreeNode targeNode;
            pt = ((TreeView)(sender)).PointToClient(new Point(e.X, e.Y));
            targeNode = this.treeView_Layers.GetNodeAt(pt);

            //如果目标节点无子节点则添加为同级节点,反之添加到下级节点的未端
            TreeNode NewMoveNode = (TreeNode)moveNode.Clone();
            if (targeNode!=null &&  targeNode.Nodes.Count == 0)
            {
                treeView_Layers.Nodes.Insert(targeNode.Index, NewMoveNode);
            }
            else
            {
                targeNode.Nodes.Insert(targeNode.Nodes.Count, NewMoveNode);
            }
            //更新当前拖动的节点选择
            treeView_Layers.SelectedNode = NewMoveNode;
            //展开目标节点,便于显示拖放效果
            targeNode.Expand();
            //移除拖放的节点
            moveNode.Remove();

            //修正Order字段中的值
            int vID = int.Parse(treeView_Layers.SelectedNode.Name);
            LayerStruct vLayer = Layers.Where(m => m.ID == vID).FirstOrDefault();
            int vFromIndex = vLayer.Order;
            int vIndex = VMainForm.GetLayerIndexFromName(vLayer.Name);
            vLayer.Order = treeView_Layers.SelectedNode.Index;
            int vToIndex = vLayer.Order;
            //VMainForm.axmap
            for ( int i= treeView_Layers.SelectedNode.Index+1;i< treeView_Layers.Nodes.Count;i++)
            {
                vID = int.Parse(treeView_Layers.Nodes[i].Name);
                vLayer = Layers.Where(m => m.ID == vID).FirstOrDefault();
                vLayer.Order = treeView_Layers.Nodes[i].Index;
            }
            VMainForm.ChangeLayerIndex(vFromIndex, vToIndex);
        }

        private void treeView_Layers_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode"))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private void treeView_Layers_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private void treeView_Layers_DrawNode(object sender, DrawTreeNodeEventArgs e)
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

        private void checkBox_Annotation_CheckedChanged(object sender, EventArgs e)
        {
            button_Apply.Enabled = true;
        }

        private void comboBox_Label_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_Apply.Enabled = true;
        }

        private void comboBox_FontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_Apply.Enabled = true;
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            if (textBox_Express.Text=="")
                textBox_Express.Text += string.Format("[{0}]", comboBox_Label.Text);
            else
                textBox_Express.Text += string.Format("+[{0}]", comboBox_Label.Text);
        }

        private void trackBar_Transparency_ValueChanged(object sender, EventArgs e)
        {
            label_Transparency.Text = string.Format("{0}%", trackBar_Transparency.Value);

            button_Apply.Enabled = true;
        }
    }
}
