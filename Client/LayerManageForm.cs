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
                //foreach(LayerStruct vTempLayer in Layers)
                //{
                //    ListViewItem vNewItem = new ListViewItem();
                //    vNewItem.UseItemStyleForSubItems = false;
                //    vNewItem.Checked = vTempLayer.IsView;
                //    vNewItem.SubItems.Add(vTempLayer.ID.ToString());
                //    vNewItem.SubItems.Add( vTempLayer.Expository);
                //    vNewItem.SubItems.Add( CommonUnit.ConvertLayerType(vTempLayer.Type.Value));
                //    if (vTempLayer.Color != -1)
                //    {
                //        Color vColor = Color.FromArgb(vTempLayer.Color);
                //        vNewItem.SubItems.Add("", vColor, vColor, new Font("宋体", 13));
                //    }
                //    else
                //    {
                //        vNewItem.SubItems.Add("");
                //    }
                //    //ListViewItem.ListViewSubItem vSubItem = new ListViewItem.ListViewSubItem()
                //    //{
                //    //    BackColor = Color.Red
                //    //};
                    
                //    listView_Layer.Items.Add(vNewItem);
                //}
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
            Dictionary<string, int> vLayerConfig = new Dictionary<string, int>();
            foreach( LayerStruct vTempLayer in Layers )
            {
                if ( vTempLayer.Color != 0 )
                {
                    vLayerConfig.Add(vTempLayer.Name, vTempLayer.Color);
                }
            }
            ConfigFile vConfigFile = new ConfigFile();
            vConfigFile.LayerColor = vLayerConfig;
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
            if ( vLayer != null )
            {
                textBox_Name.Text = vLayer.Name;
                textBox_Expository.Text = vLayer.Expository;
                textBox_Type.Text = CommonUnit.ConvertLayerType(vLayer.Type.Value);
                if (vLayer.Color != -1)
                    label_Color.BackColor = Color.FromArgb(vLayer.Color);
                else
                    label_Color.BackColor = Color.White;
                string[] vColumnArray = VMainForm.GetColumns(vLayer.Name);
                comboBox_Label.DataSource = vColumnArray;
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
            ColorDialog vColorDialog = new ColorDialog();
            if (vColorDialog.ShowDialog() == DialogResult.OK)
            {
                label_Color.BackColor = vColorDialog.Color;
                int vID = (int)treeView_Layers.SelectedNode.Tag;
                LayerStruct vLayer = Layers.Where(m => m.ID == vID).FirstOrDefault();
                string vLayerName = vLayer.Name;
                VMainForm.ChangeLayerColor(vLayerName, vColorDialog.Color.ToArgb());
                vLayer.Color = vColorDialog.Color.ToArgb();
                treeView_Layers.SelectedNode.ForeColor = vColorDialog.Color;
            }
        }
    }
}
