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
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            ConfigFile vConfigFile = new ConfigFile();
            textBox_ServerAddress.Text = vConfigFile.RemotingServerAddress;
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            ConfigFile vConfigFile = new ConfigFile();
            vConfigFile.RemotingServerAddress = textBox_ServerAddress.Text;
            vConfigFile.Save();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
