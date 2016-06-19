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
    public class ComboBoxListItem
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public ComboBoxListItem(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public ComboBoxListItem()
        {

        }

        public override string ToString()
        {
            return Value;
        }
    }
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

            string vAreaCode = getSelectedAreaCode();
            if (vAreaCode == "")
            {
                MessageBox.Show("请选择需要上传文件的单位", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            vRemoteInterface.UploadFile(Program.LoginUserInfo.ID.Value, Program.LoginUserInfo.Token,
                vFilePath,vAuthor,vAreaCode);
            MessageBox.Show("文件上传成功", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        string getSelectedAreaCode()
        {
            string vAreaCode = "";

            string vTownship = (string)comboBox_Township.SelectedValue;
            if (vTownship != "" )
            {
                vAreaCode = (string)comboBox_Township.SelectedValue;
            }

            string vVillageCommittee = (string)comboBox_VillageCommittee.SelectedValue;
            if ( vVillageCommittee != "" )
            {
                vAreaCode = (string)comboBox_Township.SelectedValue;
            }

            string vVillage = (string)comboBox_Village.SelectedValue;
            if ( vVillage != "")
            {
                vAreaCode = (string)comboBox_Village.SelectedValue;
            }
            return vAreaCode;
        }

        private void UploadFileForm_Load(object sender, EventArgs e)
        {
            ComboBoxListItem[] vTownshipDict = getTownshipDict();
            foreach (var vTempTownship in vTownshipDict)
                comboBox_Township.Items.Add(vTempTownship);

        }

        Dictionary<string, string> getVillageDict(string villageCommitteeCode)
        {
            Dictionary<string, string> vVillageDict = new Dictionary<string, string>();
            if (VillageFeatureLayer != null)
            {
                for (int i = 0; i < VillageFeatureLayer.FeatureClass.Fields.FieldCount; i++)
                {
                    //var vFeatures = m_VillageFeatureLayer.FeatureClass.GetFeatures(null, true);
                    IQueryFilter vQueryFilter = new QueryFilterClass();
                    vQueryFilter.WhereClause = (string.Format("CWHDM = '{0}'", villageCommitteeCode));
                    IFeatureCursor vFeatureCursor = VillageFeatureLayer.FeatureClass.Search(vQueryFilter, true);
                    IFeature vFeature = vFeatureCursor.NextFeature();
                    while (vFeature != null)
                    {
                        int vXZDMIndex = vFeature.Fields.FindField("Text");
                        int VNameIndex = vFeature.Fields.FindField("Text");
                        string vXZDM = vFeature.get_Value(vXZDMIndex).ToString();
                        string vName = vFeature.get_Value(VNameIndex).ToString();
                        vVillageDict.Add(vXZDM, vName);
                    }
                }
            }
            return vVillageDict;
        }

        Dictionary<string, string> getVillageCommitteeDict(string townshipCode)
        {
            Dictionary<string, string> vVillageCommitteeDict = new Dictionary<string, string>();
            if (VillageCommitteeFeatureLayer != null)
            {

                for (int i = 0; i < VillageCommitteeFeatureLayer.FeatureClass.Fields.FieldCount; i++)
                {
                    //var vFeatures = m_VillageCommitteeFeatureLayer.FeatureClass.GetFeatures(null, true);
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
                        vVillageCommitteeDict.Add(vXZDM, vName);
                    }
                }
            }
            return vVillageCommitteeDict;
        }

        ComboBoxListItem[] getTownshipDict()
        {
            List<ComboBoxListItem> vTownshipList = new List<ComboBoxListItem>();
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
    }
}
