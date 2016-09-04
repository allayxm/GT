using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using JXDL.ClientBusiness;
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
    public partial class VillagePicForm : Form
    {
        public VillagePicForm()
        {
            InitializeComponent();
        }

        public MainForm Main_Form { get; set; }

        public IFeatureLayer TownshipFeatureLayer { get; set; }
        /// <summary>
        /// 村委会要素
        /// </summary>
        public IFeatureLayer VillageCommitteeFeatureLayer { get; set; }
        /// <summary>
        /// 自然村要素
        /// </summary>
        public IFeatureLayer VillageFeatureLayer { get; set; }


        private void button_Search_Click(object sender, EventArgs e)
        {
            if (comboBox_Village.Text!="" && comboBox_Village.Text != "请选择")
            {
                
                //MainForm vMainForm = (MainForm)this.Parent;
                Main_Form.locationVillage(comboBox_VillageCommittee.Text, comboBox_Village.Text);
            }
            else
            {
                MessageBox.Show("请选择自然村", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        ComboBoxListItem[] getTownshipDict()
        {
            List<ComboBoxListItem> vTownshipList = new List<ComboBoxListItem>();
            vTownshipList.Add(new ComboBoxListItem("请选择", "请选择"));
            Dictionary<string, string> vTownshipDict = new Dictionary<string, string>();
            if (TownshipFeatureLayer != null)
            {
                IQueryFilter vQueryFilter = new QueryFilterClass();
                var vFeatures = TownshipFeatureLayer.FeatureClass.Search(vQueryFilter, true);
                IFeature vFeature = vFeatures.NextFeature();
                while (vFeature != null)
                {
                    ComboBoxListItem vNewItme = new ComboBoxListItem();
                    int vXZDMIndex = vFeature.Fields.FindField("XZDM");
                    int VNameIndex = vFeature.Fields.FindField("街道");
                    vNewItme.Name = vFeature.get_Value(VNameIndex).ToString();
                    vNewItme.Value = vFeature.get_Value(vXZDMIndex).ToString();
                    vTownshipList.Add(vNewItme);
                    vFeature = vFeatures.NextFeature();
                }
            }
            return vTownshipList.ToArray();
        }

        ComboBoxListItem[] getVillageCommitteeDict(string townshipCode)
        {
            List<ComboBoxListItem> vVillageCommitteeDict = new List<ComboBoxListItem>();
            vVillageCommitteeDict.Add(new ComboBoxListItem("请选择", "请选择"));
            if (VillageCommitteeFeatureLayer != null)
            {
                IQueryFilter vQueryFilter = new QueryFilterClass();
                vQueryFilter.WhereClause = (string.Format("XZDM = '{0}'", townshipCode));
                IFeatureCursor vFeatureCursor = VillageCommitteeFeatureLayer.FeatureClass.Search(vQueryFilter, true);
                IFeature vFeature = vFeatureCursor.NextFeature();
                while (vFeature != null)
                {
                    int vXZDMIndex = vFeature.Fields.FindField("CWHDM");
                    int VNameIndex = vFeature.Fields.FindField("村委会_dwg");
                    string vXZDM = vFeature.get_Value(vXZDMIndex).ToString();
                    string vName = vFeature.get_Value(VNameIndex).ToString();
                    vVillageCommitteeDict.Add(new ComboBoxListItem(vXZDM, vName));
                    vFeature = vFeatureCursor.NextFeature();

                }
            }
            return vVillageCommitteeDict.ToArray();
        }
        ComboBoxListItem[] getVillageDict(string villageCommitteeCode)
        {
            List<ComboBoxListItem> vVillageDict = new List<ComboBoxListItem>();
            vVillageDict.Add(new ComboBoxListItem("请选择", "请选择"));
            if (VillageFeatureLayer != null)
            {
                IQueryFilter vQueryFilter = new QueryFilterClass();
                vQueryFilter.WhereClause = (string.Format("CWHDM = '{0}'", villageCommitteeCode));
                IFeatureCursor vFeatureCursor = VillageFeatureLayer.FeatureClass.Search(vQueryFilter, true);
                IFeature vFeature = vFeatureCursor.NextFeature();
                while (vFeature != null)
                {
                    int vXZDMIndex = vFeature.Fields.FindField("ZRCDM");
                    int VNameIndex = vFeature.Fields.FindField("Text");
                    string vXZDM = vFeature.get_Value(vXZDMIndex).ToString();
                    string vName = vFeature.get_Value(VNameIndex).ToString();
                    vVillageDict.Add(new ComboBoxListItem(vXZDM, vName));
                    vFeature = vFeatureCursor.NextFeature();
                }
            }
            return vVillageDict.ToArray();
        }

        private void VillagePicForm_Load(object sender, EventArgs e)
        {
            ComboBoxListItem[] vTownshipDict = getTownshipDict();
            foreach (var vTempTownship in vTownshipDict)
            {
                comboBox_Township.Items.Add(vTempTownship);
                comboBox_Township.SelectedIndex = 0;
            }
        }

        private void comboBox_VillageCommittee_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxListItem vSelectedItem = (ComboBoxListItem)comboBox_VillageCommittee.SelectedItem;
            if (vSelectedItem.Value != "请选择")
            {
                string vCode = vSelectedItem.Value;
                ComboBoxListItem[] vVillageList = getVillageDict(vCode);
                comboBox_Village.Items.Clear();
                foreach (ComboBoxListItem vVillage in vVillageList)
                {
                    comboBox_Village.Items.Add(vVillage);
                    comboBox_Village.SelectedIndex = 0;
                }
            }
        }

        private void comboBox_Township_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxListItem vSelectedItem = (ComboBoxListItem)comboBox_Township.SelectedItem;
            if (vSelectedItem.Value != "请选择")
            {
                string vCode = vSelectedItem.Value;
                ComboBoxListItem[] vVillageCommitteeList = getVillageCommitteeDict(vCode);
                comboBox_VillageCommittee.Items.Clear();
                foreach (ComboBoxListItem vTempVillageCommittee in vVillageCommitteeList)
                {
                    comboBox_VillageCommittee.Items.Add(vTempVillageCommittee);
                    comboBox_VillageCommittee.SelectedIndex = 0;
                }
            }
        }

        private void button_OutPic_Click(object sender, EventArgs e)
        {
            SaveFileDialog vSaveFileDialog = new SaveFileDialog();
            vSaveFileDialog.Filter = "Image Files(*.JPG)|*.JPG|All files (*.*)|*.*";
            if ( vSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (  vSaveFileDialog.FileName != "" )
                    Main_Form.OutPic(vSaveFileDialog.FileName);
            }
        }
    }
}
