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
        
        /// <summary>
        /// 图层类型 0:点 1:线 2:面 3:栅格 4:临时图层
        /// </summary>
        public int? Type { get; set; }

        public bool IsView { get; set; } = true;

        public int Color { get; set; } = -1;

        public int Order { get; set; }

        /// <summary>
        /// 显示标注
        /// </summary>
        public bool ShowAnnotation { get; set; } = false;

        /// <summary>
        /// 标注字体大小
        /// </summary>
        public int AnnotationFontSize { get; set; } = 13;

        /// <summary>
        /// 标注字体颜色
        /// </summary>
        public int AnnotationFontColor { get; set; } = -1;

        public string AnnotationField { get; set; }

        /// <summary>
        /// 是否是影像图层
        /// </summary>
        public bool IsRaster { get; set; } = false;

        /// <summary>
        /// 透明度
        /// </summary>
        public short Transparency { get; set; } = 0;

    }
}
