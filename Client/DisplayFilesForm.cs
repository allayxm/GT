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
    public partial class DisplayFilesForm : Form
    {
        public string[] AreaCodeArray { get; set; }
        public DisplayFilesForm()
        {
            InitializeComponent();
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DisplayFilesForm_Load(object sender, EventArgs e)
        {
            if (AreaCodeArray!=null )
            {
                RemoteInterface vRemoteInterface = new RemoteInterface(Program.LoginUserInfo.ID.Value,Program.LoginUserInfo.UserName,Program.LoginUserInfo.Token);
                FileInfo[] vFileInfoArray = vRemoteInterface.GetFiles(AreaCodeArray);
                if (vFileInfoArray!=null )
                {
                    dataGridView_FileList.AutoGenerateColumns = false;
                    dataGridView_FileList.DataSource = vFileInfoArray;
                
                }
            }
        }

        private void dataGridView_FileList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if ( e.ColumnIndex == 4)
            {
                FileInfo[] vDatsSource = (FileInfo[])dataGridView_FileList.DataSource;
                int vFileID = vDatsSource[e.RowIndex].ID;
                string vFileName = vDatsSource[e.RowIndex].FileName;
                ConfigFile vConfigFile = new ConfigFile();
                string vDownloadPath = string.Format(@"{0}\{1}", vConfigFile.DownloadPath, vFileName);
                RemoteInterface vRemoteInterface = new RemoteInterface(Program.LoginUserInfo.ID.Value,Program.LoginUserInfo.UserName,Program.LoginUserInfo.Token);
                if ( vRemoteInterface.DownloadFile(vFileID, vDownloadPath) )
                {
                    if ( MessageBox.Show("文件下载成功，是否打开?", "信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK )
                    {
                        System.Diagnostics.Process.Start(vDownloadPath);
                    }
                }
                else
                {
                    MessageBox.Show("文件下载失败!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
