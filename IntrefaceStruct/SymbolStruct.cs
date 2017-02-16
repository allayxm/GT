using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JXDL.IntrefaceStruct
{
    public class SymbolStruct
    {
        /// <summary>
        /// 自增长ID(主键)
        /// </summary>
        public int? ID { get; set; }
        /// <summary>
        /// 图层名称
        /// </summary>
        public string LayerName { get; set; }
        /// <summary>
        /// 要素名称
        /// </summary>
        public string Feature { get; set; }
        /// <summary>
        /// 要素代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 符号名称
        /// </summary>
        public string Symbol { get; set; }
    }
}
