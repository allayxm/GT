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

        public int Order { get; set; }

        public bool ShowAnnotation { get; set; } = false;

        public int AnnotationFontSize { get; set; } = 13;

        public int AnnotationFontColor { get; set; } = -1;

        public string AnnotationField { get; set; }

    }
}
