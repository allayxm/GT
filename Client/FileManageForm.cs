using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using JXDL.ClientBusiness;
using JXDL.IntrefaceStruct;

namespace JXDL.Client
{
    public partial class FileManageForm : Form
    {
        public FileManageForm()
        {
            InitializeComponent();
        }

        public IFeatureLayer TownshipFeatureLayer { get; set; }
        /// <summary>
        /// 村委会要素
        /// </summary>
        public IFeatureLayer VillageCommitteeFeatureLayer { get; set; }
        /// <summary>
        /// 自然村要素
        /// </summary>
        public IFeatureLayer VillageFeatureLayer { get; set; }

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
                    vNewItme.Name = vFeature.get_Value(vXZDMIndex).ToString();
                    vNewItme.Value = vFeature.get_Value(VNameIndex).ToString();
                    vTownshipList.Add(vNewItme);
                    vFeature = vFeatures.NextFeature();
                }
            }
            return vTownshipList.ToArray();
        }


        private void button_Query_Click(object sender, EventArgs e)
        {
            string vTownship = comboBox_Township.Text != "请选择" ? ((ComboBoxListItem)comboBox_Township.SelectedItem).Name : "" ;
            string vVillageCommittee = comboBox_VillageCommittee.Items.Count >0&& comboBox_VillageCommittee .Text !="请选择"? ((ComboBoxListItem)comboBox_VillageCommittee.SelectedItem).Name:"";
            string vVillage = comboBox_Village.Items.Count > 0&& comboBox_Village.Text!="请选择" ? ((ComboBoxListItem)comboBox_Village.SelectedItem).Name : "";
            string vAuthor = textBox_Author.Text;
            string vFileName = textBox_File.Text;
            if (vTownship != "" || vVillageCommittee!="" || vVillage!="" || vAuthor != "" || vFileName!="")
            {
                RemoteInterface vRemoteInterface = new RemoteInterface(Program.LoginUserInfo.ID.Value, Program.LoginUserInfo.UserName,Program.LoginUserInfo.Token);
                DataTable vQueryReuslt = vRemoteInterface.QueryFile(vTownship, vVillageCommittee, vVillage, vAuthor, vFileName);
                if (vQueryReuslt != null && vQueryReuslt.Rows.Count > 0)
                    dataGridView_FileList.DataSource = vQueryReuslt;
                else
                {
                    dataGridView_FileList.DataSource = null;
                    MessageBox.Show("没有符合条件的数据", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("请至少选择一个查询条件","错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView_FileList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void FileManageForm_Load(object sender, EventArgs e)
        {
            ComboBoxListItem[] vTownshipDict = getTownshipDict();
            switch ( Program.LoginUserInfo.Power.Value )
            {
                case 2:
                case 3:
                    Column_Delete.Visible = true;
                    break;
                default:
                    Column_Delete.Visible = false;
                    break;
            }
            foreach (var vTempTownship in vTownshipDict)
            {
                comboBox_Township.Items.Add(vTempTownship);
                comboBox_Township.SelectedIndex = 0;
            }
            dataGridView_FileList.DataError += DataGridView_FileList_DataError;
            dataGridView_FileList.AutoGenerateColumns = false;
        }

        private void comboBox_VillageCommittee_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxListItem vSelectedItem = (ComboBoxListItem)comboBox_VillageCommittee.SelectedItem;
            if (vSelectedItem.Value != "请选择")
            {
                string vCode = vSelectedItem.Name;
                ComboBoxListItem[] vVillageList = getVillageDict(vCode);
                comboBox_Village.Items.Clear();
                foreach (ComboBoxListItem vVillage in vVillageList)
                {
                    comboBox_Village.Items.Add(vVillage);
                    comboBox_Village.SelectedIndex = 0;
                }
            }
        }

        private void comboBox_Village_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_Township_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxListItem vSelectedItem = (ComboBoxListItem)comboBox_Township.SelectedItem;
            if (vSelectedItem.Value != "请选择")
            {
                string vCode = vSelectedItem.Name;
                ComboBoxListItem[] vVillageCommitteeList = getVillageCommitteeDict(vCode);
                comboBox_VillageCommittee.Items.Clear();
                foreach (ComboBoxListItem vTempVillageCommittee in vVillageCommitteeList)
                {
                    comboBox_VillageCommittee.Items.Add(vTempVillageCommittee);
                    comboBox_VillageCommittee.SelectedIndex = 0;
                }
            }
        }

        private void dataGridView_FileList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                RemoteInterface vRemoteInterface = new RemoteInterface(Program.LoginUserInfo.ID.Value, Program.LoginUserInfo.UserName, Program.LoginUserInfo.Token);
                DataTable vDatsSource = (DataTable)dataGridView_FileList.DataSource;
                int vFileID = (int)vDatsSource.Rows[e.RowIndex]["ID"];
                string vFileName = (string)vDatsSource.Rows[e.RowIndex]["FileName"];
                if (e.ColumnIndex == 4)
                {
                    ConfigFile vConfigFile = new ConfigFile();
                    string vDownloadPath = string.Format(@"{0}\{1}", vConfigFile.DownloadPath, vFileName);

                    if (vRemoteInterface.DownloadFile(vFileID, vDownloadPath))
                    {
                        if (MessageBox.Show("文件下载成功，是否打开?", "信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            System.Diagnostics.Process.Start(vDownloadPath);
                        }
                    }
                    else
                    {
                        MessageBox.Show("文件下载失败!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                if (e.ColumnIndex == 5)
                {
                    if (MessageBox.Show("是否确认删除", "信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        if (vRemoteInterface.DeleteFile(vFileID, vFileName))
                        {
                            dataGridView_FileList.Rows.RemoveAt(e.RowIndex);
                        }
                        else
                        {
                            MessageBox.Show("删除文件失败!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}
