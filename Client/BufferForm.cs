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

        public Dictionary<string, int> BufferLayers { get; set; } = new Dictionary<string, int>();
        public LayerStruct[] Layers { get; set; } 


        private void button_Analysis_Click(object sender, EventArgs e)
        {
            DataTable vTable =  (DataTable)dataGridView_Layers.DataSource;
            BufferLayers.Clear();
            foreach ( DataRow vTempRow in vTable.Rows)
            {
                bool vSelected = (bool)vTempRow["选择"];
                if (vSelected)
                {
                    string vLayerName = (string)vTempRow["图层名称"];
                    int vDistance = (int)vTempRow["缓冲距离"];
                    BufferLayers.Add(vLayerName, vDistance);
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }

       

        private void button_Exit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BufferForm_Load(object sender, EventArgs e)
        {
            DataTable vTable = createTableStruct();
            foreach(var vTempDict in BufferLayers)
            {
                DataRow vNewRow = vTable.NewRow();
                vNewRow["选择"] = true;
                vNewRow["图层名称"] = vTempDict.Key;
                vNewRow["图层别名"] = getLayerAliasName(vTempDict.Key);
                vNewRow["图层类型"] = getLayerType(vTempDict.Key);
                vNewRow["缓冲距离"] = vTempDict.Value;
                vTable.Rows.Add(vNewRow);
            }
            vTable.AcceptChanges();
            dataGridView_Layers.AutoGenerateColumns = false;
            dataGridView_Layers.DataSource = vTable;
        }

        string getLayerType(string layerName)
        {
            string vResult = "";
            switch (layerName)
            {
                case "村委会":
                case "自然村":
                case "乡镇街道":
                    vResult = "面";
                    break;
                default:
                    int vType = Layers.Where(m => m.Name == layerName).FirstOrDefault().Type.Value;
                    vResult = CommonUnit.ConvertLayerType(vType);
                    break;
            }
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
    }
}
