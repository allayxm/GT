using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MXKJ.DBMiddleWareLib;

namespace JXDL.ManageEFModel
{
    [TableAttrib("Symbol", "ID")]
    public struct SymbolEFModel
    {
        [ColumnAttrib("ID")]
        /// <summary>
        /// 自增长ID(主键)
        /// </summary>
        public int? ID { get; set; }

        [ColumnAttrib("LayerName")]
        /// <summary>
        /// 图层名称
        /// </summary>
        public string LayerName { get; set; }

        [ColumnAttrib("Feature")]
        /// <summary>
        /// 要素名称
        /// </summary>
        public string Feature { get; set; }

        [ColumnAttrib("Code")]
        /// <summary>
        /// 要素代码
        /// </summary>
        public string Code { get; set; }

        [ColumnAttrib("Symbol")]
        /// <summary>
        /// 符号名称
        /// </summary>
        public string Symbol { get; set; }
    }
}
