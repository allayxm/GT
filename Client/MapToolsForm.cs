using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JXDL.Client
{
    public partial class MapToolsForm : Form
    {
        public ESRI.ArcGIS.Controls.AxMapControl axMapControl1 { get; set; }
        public MapToolsForm()
        {
            InitializeComponent();
        }

        private void MapToolsForm_Load(object sender, EventArgs e)
        {
            axToolbarControl1.SetBuddyControl(axMapControl1);
        }

        //private const int WM_NCHITTEST = 0x84;
        //private const int HTCLIENT = 0x1;
        //private const int HTCAPTION = 0x2;

        //protected override void WndProc(ref Message m)
        //{
        //    // 引用消息ID(ref Message ID)
        //    switch (m.Msg)
        //    {
        //        case WM_NCHITTEST:
        //            base.WndProc(ref m);
        //            if ((int)m.Result == HTCLIENT)
        //                m.Result = (IntPtr)HTCAPTION;
        //            return;
        //    }
        //    base.WndProc(ref m);
        //}
    }
}
