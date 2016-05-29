using System;
using MXKJ.DBMiddleWareLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JXDL.ManageEFModel
{
    [TableAttrib("SystemConfig", "ID")]
    public struct SystemConfigEF
    {
        [ColumnAttrib("ID")]
        public int ID { get; set; }
        [ColumnAttrib("ItemName")]
        public String ItemName { get; set; }
        [ColumnAttrib("ItemValue1")]
        public String ItemValue1 { get; set; }
        [ColumnAttrib("ItemValue2")]
        public String ItemValue2 { get; set; }
    }

}
