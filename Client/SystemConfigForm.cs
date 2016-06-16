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

        public int SelectColor { get; set; }

        private void button_SelectColor_Click(object sender, EventArgs e)
        {
            ColorDialog vColorDialog = new ColorDialog();
            if ( vColorDialog.ShowDialog() == DialogResult.OK )
            {
                label_Color.BackColor = vColorDialog.Color;
                label_Color.Tag = vColorDialog.Color.ToArgb();
            }
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SelectColor = (int)label_Color.Tag;
            Close();
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SysteConfigForm_Load(object sender, EventArgs e)
        {
            ConfigFile vConfigFile = new ConfigFile();
            label_Color.Tag = vConfigFile.MapBackgroundColor;
            label_Color.BackColor = Color.FromArgb(vConfigFile.MapBackgroundColor);
        }
    }
}
