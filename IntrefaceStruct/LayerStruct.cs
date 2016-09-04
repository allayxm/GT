using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JXDL.IntrefaceStruct
{
    public class LayerStruct
    {
        
        public int? ID { get; set; }
        
        public String classify { get; set; }
        
        public String Name { get; set; }
        
        public String Expository { get; set; }
        
        public int? Type { get; set; }

        public bool IsView { get; set; } = true;

        public int Color { get; set; } = -1;

    }
}
