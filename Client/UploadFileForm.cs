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
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace JXDL.Client
{
  
    public partial class UploadFileForm : Form
    {

        public IFeatureLayer TownshipFeatureLayer { get; set; }
        /// <summary>
        /// 村委会要素
        /// </summary>
        public IFeatureLayer VillageCommitteeFeatureLayer { get; set; }
        /// <summary>
        /// 自然村要素
        /// </summary>
        public IFeatureLayer VillageFeatureLayer { get; set; }


        public UploadFileForm()
        {
            InitializeComponent();
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            
            Close();
        }

        private void button_Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog vOpenFileDialog = new OpenFileDialog();
            vOpenFileDialog.Multiselect = false;
            if ( vOpenFileDialog.ShowDialog()== DialogResult.OK )
            {
                string[] vSelectedFiles = vOpenFileDialog.FileNames;
                if ( vSelectedFiles.Length > 0 )
                {
                    textBox_File.Text = vSelectedFiles[0];
                }
            }
            vOpenFileDialog.Dispose();
        }

        private void button_Upload_Click(object sender, EventArgs e)
        {
            RemoteInterface vRemoteInterface = new RemoteInterface();
            int vUserID = Program.LoginUserInfo.ID.Value;
            string vToken = Program.LoginUserInfo.Token;

            string vAuthor = "";
            if (textBox_Author.Text != "")
                vAuthor = textBox_Author.Text;
            else
            {
                MessageBox.Show("请输入作者名称", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string vFilePath = "";
            if (textBox_File.Text != "" && System.IO.File.Exists(textBox_File.Text))
                vFilePath = textBox_File.Text;
            else
            {
                MessageBox.Show("请选择文件或者选择的文件不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string vAreaCode = "";
            string vUnitName = "";
            getSelectedAreaCode(ref vAreaCode,ref vUnitName );
            if (vAreaCode == "")
            {
                MessageBox.Show("请选择需要上传文件的单位", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool vResult = vRemoteInterface.UploadFile(Program.LoginUserInfo.ID.Value, Program.LoginUserInfo.Token,
                vFilePath,vAuthor,vAreaCode,vUnitName);
            if (vResult)
            {
                MessageBox.Show("文件上传成功", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox_File.Text = "";
            }
            else
                MessageBox.Show("文件上传失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void getSelectedAreaCode( ref string AreaCode,ref string UnitName )
        {
            string vTownship = (string)comboBox_Township.Text;
            if (vTownship!=null && vTownship != "" && vTownship != "请选择" )
            {
                AreaCode = ( (ComboBoxListItem)comboBox_Township.SelectedItem).Key ;
                UnitName = ((ComboBoxListItem)comboBox_Township.SelectedItem).Value;
            }

            string vVillageCommittee = (string)comboBox_VillageCommittee.Text;
            if (vVillageCommittee!=null && vVillageCommittee != "" &&  vVillageCommittee != "请选择" )
            {
                AreaCode = ( (ComboBoxListItem)comboBox_Township.SelectedItem ).Key;
                UnitName = ((ComboBoxListItem)comboBox_Township.SelectedItem).Value;
            }

            string vVillage = (string)comboBox_Village.Text;
            if (vVillage!=null && vVillage!="" && vVillage != "请选择")
            {
                AreaCode = ( (ComboBoxListItem)comboBox_Village.SelectedItem ).Key;
                UnitName = ((ComboBoxListItem)comboBox_Village.SelectedItem).Value;
            }
        }

        private void UploadFileForm_Load(object sender, EventArgs e)
        {
            ComboBoxListItem[] vTownshipDict = getTownshipDict();
            foreach (var vTempTownship in vTownshipDict)
            {
                comboBox_Township.Items.Add(vTempTownship);
                comboBox_Township.SelectedIndex = 0;
            }
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

        ComboBoxListItem[] getTownshipDict()
        {
            List<ComboBoxListItem> vTownshipList = new List<ComboBoxListItem>();
            vTownshipList.Add( new ComboBoxListItem("请选择", "请选择"));
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
                    vNewItme.Key  = vFeature.get_Value(vXZDMIndex).ToString();
                    vNewItme.Value = vFeature.get_Value(VNameIndex).ToString();
                    vTownshipList.Add(vNewItme);
                    vFeature = vFeatures.NextFeature();
                }
            }
            return vTownshipList.ToArray();
        }

        private void comboBox_Township_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxListItem vSelectedItem = (ComboBoxListItem)comboBox_Township.SelectedItem;
            if (vSelectedItem.Value!= "请选择")
            {
                string vCode = vSelectedItem.Key;
                ComboBoxListItem[] vVillageCommitteeList = getVillageCommitteeDict(vCode);
                comboBox_VillageCommittee.Items.Clear();
                foreach (ComboBoxListItem vTempVillageCommittee in vVillageCommitteeList)
                {
                    comboBox_VillageCommittee.Items.Add(vTempVillageCommittee);
                    comboBox_VillageCommittee.SelectedIndex = 0;
                }
            }
        }

        private void comboBox_VillageCommittee_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxListItem vSelectedItem = (ComboBoxListItem)comboBox_VillageCommittee.SelectedItem;
            if (vSelectedItem.Value != "请选择")
            {
                string vCode = vSelectedItem.Key;
                ComboBoxListItem[] vVillageList = getVillageDict(vCode);
                comboBox_Village.Items.Clear();
                foreach (ComboBoxListItem vVillage in vVillageList)
                {
                    comboBox_Village.Items.Add(vVillage);
                    comboBox_Village.SelectedIndex = 0;
                }
            }
        }
    }
}
