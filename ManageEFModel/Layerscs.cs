using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MXKJ.DBMiddleWareLib;

namespace JXDL.ManageEFModel
{
    [TableAttrib("Layers", "ID")]
    public struct LayersEFModel
    {
        [ColumnAttrib("ID")]
        public int? ID { get; set; }
        [ColumnAttrib("classify")]
        public String classify { get; set; }
        [ColumnAttrib("Name")]
        public String Name { get; set; }
        [ColumnAttrib("Expository")]
        public String Expository { get; set; }
        [ColumnAttrib("Type")]
        public int? Type { get; set; }
    }

}
