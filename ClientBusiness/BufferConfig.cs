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
        public string Expository { get; set; }
        public int Distance { get; set; } = 10;
        public string BufferLayerName { get; set; }
        public bool IsSelect { get; set; } = true;
        public List<ListViewItem> SelectedLayers { get; set; } = new List<ListViewItem>();
        public List<ListViewItem> AnalyzeLayers { get; set; } = new List<ListViewItem>();
        public List<DataTable> AnalyzeLayers_Detail { get; set; } = new List<DataTable>();

    }
}
