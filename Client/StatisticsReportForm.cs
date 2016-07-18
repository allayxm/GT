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
using JXDL.IntrefaceStruct;

namespace JXDL.Client
{
    public partial class StatisticsReportForm : Form
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

        public StatisticsReportForm()
        {
            InitializeComponent();
        }

        ComboBoxListItem[] getVillageDict(string villageCommitteeCode)
        {
            List<ComboBoxListItem> vVillageDict = new List<ComboBoxListItem>();
            //vVillageDict.Add(new ComboBoxListItem("请选择", "请选择"));
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
            vVillageCommitteeDict.Add(new ComboBoxListItem("0", "全部村委会"));
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
            vTownshipList.Add(new ComboBoxListItem("0", "全部乡镇"));
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

        private void comboBox_VillageCommittee_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ComboBoxListItem vSelectedItem = (ComboBoxListItem)comboBox_VillageCommittee.SelectedItem;
            //if (vSelectedItem.Value != "请选择")
            //{
            //    string vCode = vSelectedItem.Key;
            //    ComboBoxListItem[] vVillageList = getVillageDict(vCode);
            //    comboBox_Village.Items.Clear();
            //    foreach (ComboBoxListItem vVillage in vVillageList)
            //    {
            //        comboBox_Village.Items.Add(vVillage);
            //        comboBox_Village.SelectedIndex = 0;
            //    }
            //}
        }

        private void StatisticsReportForm_Load(object sender, EventArgs e)
        {
            ComboBoxListItem[] vTownshipDict = getTownshipDict();
            foreach (var vTempTownship in vTownshipDict)
            {
                comboBox_Township.Items.Add(vTempTownship);
                comboBox_Township.SelectedIndex = 0;
            }
        }

        private void button_Statistics_Click(object sender, EventArgs e)
        {
            string vTitle = "";
            string vAreaCodeStr = "";
            string vTownshipValue = ((ComboBoxListItem)comboBox_Township.SelectedItem).Value;
            string vVillageCommitteeValue = ((ComboBoxListItem)comboBox_VillageCommittee.SelectedItem).Value;
            if (vTownshipValue=="0")
            {
                vTitle = "丰城市资料上传统计";
                foreach (ComboBoxListItem vTempItem in comboBox_Township.Items)
                {
                    if (vTempItem.Value != "0")
                        vAreaCodeStr += string.Format( "{0},{1}|",vTempItem.Value,vTempItem.Name );
                }
            }
            else if (vVillageCommitteeValue== "0")
            {
                vTitle =  string.Format( "{0}资料上传统计", ((ComboBoxListItem)comboBox_Township.SelectedItem).Name);
                foreach ( ComboBoxListItem vTempItem in comboBox_VillageCommittee.Items )
                {
                    if (vTempItem.Value!="0")
                        vAreaCodeStr += string.Format("{0},{1}|", vTempItem.Value, vTempItem.Name); ;
                }
            }
            else
            {
                vTitle = string.Format("{0}资料上传统计", ((ComboBoxListItem)comboBox_VillageCommittee.SelectedItem).Name);
                ComboBoxListItem[] vVillageDict = getVillageDict(vVillageCommitteeValue);
                foreach (ComboBoxListItem vTempItem in vVillageDict)
                {
                    vAreaCodeStr += string.Format("{0},{1}|", vTempItem.Value, vTempItem.Name); ;
                }
            }
            if (vAreaCodeStr != "")
            {
                vAreaCodeStr = vAreaCodeStr.Remove(vAreaCodeStr.Length - 1);
                RemoteInterface vRemoteInterface = new RemoteInterface();
                var vStatisticsResut = vRemoteInterface.FileNumberStatistics(vAreaCodeStr).Where(m => m.FileNumber > 0);
                //DataTable vTable = convertToDataTable(vStatisticsResut);
                if (vStatisticsResut.Count() > 0)
                {
                    chart_Statistics.DataSource = vStatisticsResut;
                    chart_Statistics.Series[0].XValueMember = "UnitName";
                    chart_Statistics.Series[0].YValueMembers = "FileNumber";
                    chart_Statistics.Titles[0].Text = vTitle;
                    chart_Statistics.DataBind();
                }
                else
                {
                    chart_Statistics.DataSource = new DataTable();
                    chart_Statistics.Titles[0].Text = vTitle;
                    chart_Statistics.DataBind();
                    MessageBox.Show("选择的行政区域没有上传资料", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                chart_Statistics.DataSource = new DataTable();
                chart_Statistics.Titles[0].Text = vTitle;
                chart_Statistics.DataBind();
                MessageBox.Show("选择的行政区域没有数据","信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        DataTable convertToDataTable(FileNumberStatisticsStruct[] StatisticsData)
        {
            DataTable vTable = new DataTable();
            vTable.Columns.Add("AreaCode", typeof(string));
            vTable.Columns.Add("UnitName", typeof(string));
            vTable.Columns.Add("FileNumber", typeof(int));
            vTable.AcceptChanges();
            foreach(FileNumberStatisticsStruct vTempData in StatisticsData)
            {
                DataRow vNewRow = vTable.NewRow();
                vNewRow["AreaCode"] = vTempData.AreaCode;
                vNewRow["UnitName"] = insertEnter(vTempData.UnitName);
                vNewRow["FileNumber"] = vTempData.FileNumber;
                vTable.Rows.Add(vNewRow);
            }
            vTable.AcceptChanges();
            return vTable;
        }

        string insertEnter(string strValue)
        {
            string vResult = "";
            foreach ( char vTempChar in strValue)
            {
                vResult += vTempChar + "\r";
            }
            return vResult;
        }
    }
}
