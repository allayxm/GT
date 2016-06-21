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
                string vAreaStrCode = "";
                foreach ( string vTempAreaCode in AreaCodeArray)
                {
                    vAreaStrCode += vTempAreaCode + "|";
                }
                if (vAreaStrCode != "")
                    vAreaStrCode += vAreaStrCode.Remove(vAreaStrCode.Length - 1);
                RemoteInterface vRemoteInterface = new RemoteInterface();
                FileInfo[] vFileInfoArray = vRemoteInterface.GetFiles(AreaCodeArray);
            }
        }
    }
}
