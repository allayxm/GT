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
    public partial class UploadFileForm : Form
    {
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
            string[] vAuthor = new string[] { textBox_Author.Text };
            string vAreaCode = "";
            
            vRemoteInterface.UploadFile(,,,);
        }

        string getSelectedAreaCode()
        {
            string vAreaCode = "";
            if ( (string)comboBox_Township.SelectedValue!="" )
            {
                vAreaCode = (string)comboBox_Township.SelectedValue;
            }

            if ()
        }
    }
}
