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
    public partial class SystemConfigForm : Form
    {
        public SystemConfigForm()
        {
            InitializeComponent();
        }

        public int MapBackgroundColor { get; set; }
        public int TownshipBackgroundColor { get; set; }
        public int VillageCommitteeBackgroundColor { get; set; }
        public int VillageBackgroundColor { get; set; }
      
        public string DownlaodPath { get; set; }

        private void button_SelectColor_Click(object sender, EventArgs e)
        {
            ColorDialog vColorDialog = new ColorDialog();
            if (vColorDialog.ShowDialog() == DialogResult.OK)
            {
                label_MapColor.BackColor = vColorDialog.Color;
                label_MapColor.Tag = vColorDialog.Color.ToArgb();
            }
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            MapBackgroundColor = (int)label_MapColor.Tag;
            TownshipBackgroundColor = (int)label_TownshipColor.Tag;
            VillageCommitteeBackgroundColor = (int)label_VillageCommitteeColor.Tag;
            VillageBackgroundColor = (int)label_VillageColor.Tag;
            if (textBox_DownloadPath.Text != "")
            {
                DownlaodPath = textBox_DownloadPath.Text;
                if (DownlaodPath[DownlaodPath.Length - 1] == '\\')
                    DownlaodPath = DownlaodPath.Remove(DownlaodPath.Length - 1);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("请选择文件下载路径", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SysteConfigForm_Load(object sender, EventArgs e)
        {
            ConfigFile vConfigFile = new ConfigFile();

            label_MapColor.Tag = vConfigFile.MapBackgroundColor;
            label_MapColor.BackColor = Color.FromArgb(vConfigFile.MapBackgroundColor);

            label_TownshipColor.Tag = vConfigFile.TownshipBackgroundColor;
            label_TownshipColor.BackColor = Color.FromArgb(vConfigFile.TownshipBackgroundColor);

            label_VillageCommitteeColor.Tag = vConfigFile.VillageCommitteeBackgroundColor;
            label_VillageCommitteeColor.BackColor = Color.FromArgb(vConfigFile.VillageCommitteeBackgroundColor);

            label_VillageColor.Tag = vConfigFile.VillageBackgroundColor;
            label_VillageColor.BackColor = Color.FromArgb(vConfigFile.VillageBackgroundColor);

            textBox_DownloadPath.Text = DownlaodPath;
        }

        private void button_Browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog vFolderBrowserDialog = new FolderBrowserDialog();
            if (vFolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBox_DownloadPath.Text = vFolderBrowserDialog.SelectedPath;
            }
        }

        private void button_TownshipColor_Click(object sender, EventArgs e)
        {
            ColorDialog vColorDialog = new ColorDialog();
            if (vColorDialog.ShowDialog() == DialogResult.OK)
            {
                label_TownshipColor.BackColor = vColorDialog.Color;
                label_TownshipColor.Tag = vColorDialog.Color.ToArgb();
            }
        }

        private void button_VillageCommitteeColor_Click(object sender, EventArgs e)
        {
            ColorDialog vColorDialog = new ColorDialog();
            if (vColorDialog.ShowDialog() == DialogResult.OK)
            {
                label_VillageCommitteeColor.BackColor = vColorDialog.Color;
                label_VillageCommitteeColor.Tag = vColorDialog.Color.ToArgb();
            }
        }

        private void button_VillageColor_Click(object sender, EventArgs e)
        {
            ColorDialog vColorDialog = new ColorDialog();
            if (vColorDialog.ShowDialog() == DialogResult.OK)
            {
                label_VillageColor.BackColor = vColorDialog.Color;
                label_VillageColor.Tag = vColorDialog.Color.ToArgb();
            }
        }

      
    }
}
