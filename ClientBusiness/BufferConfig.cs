using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace JXDL.ClientBusiness
{
    public class BufferConfig
    {
        public string LayerName { get; set; }
        public int LayerType { get; set; }
        public int distance { get; set; } = 10;
        public List<ListViewItem> SelectedLayers { get; set; } = new List<ListViewItem>();
        public List<ListViewItem> AnalyzeLayers { get; set; } = new List<ListViewItem>();
        public List<DataTable> AnalyzeLayers_Detail { get; set; } = new List<DataTable>();

    }
}
