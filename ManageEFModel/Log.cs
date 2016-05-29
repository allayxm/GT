using System;
using MXKJ.DBMiddleWareLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JXDL.ManageEFModel
{
    [TableAttrib("Log", "ID")]
    public struct Log
    {
        [ColumnAttrib("ID")]
        public int ID { get; set; }
        [ColumnAttrib("UserID")]
        public int? UserID { get; set; }
        [ColumnAttrib("UserName")]
        public String UserName { get; set; }
        [ColumnAttrib("Type")]
        public String Type { get; set; }
        [ColumnAttrib("OperateTime")]
        public DateTime? OperateTime { get; set; }
    }
}
