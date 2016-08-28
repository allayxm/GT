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
        public LayerManageForm()
        {
            InitializeComponent();
        }

        private void LayerManageForm_Load(object sender, EventArgs e)
        {
            if (Layers != null)
            {
                foreach(LayerStruct vTempLayer in Layers)
                {
                    ListViewItem vNewItem = new ListViewItem();
                    vNewItem.Checked = vTempLayer.IsView;
                    vNewItem.SubItems.Add(vTempLayer.ID.ToString());
                    vNewItem.SubItems.Add( vTempLayer.Expository);
                    vNewItem.SubItems.Add( CommonUnit.ConvertLayerType(vTempLayer.Type.Value));
                    listView_Layer.Items.Add(vNewItem);
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
            foreach( ListViewItem vTempItem in listView_Layer.Items )
            {
                int vID = int.Parse( vTempItem.SubItems[1].Text );
                Layers.Where(m => m.ID == vID).FirstOrDefault().IsView = vTempItem.Checked;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
