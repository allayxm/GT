using System;
using MXKJ.DBMiddleWareLib;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JXDL.ManageEFModel
{
    [TableAttrib("Users", "ID")]
    public struct UsersEF
    {
        [ColumnAttrib("ID")]
        public int ID { get; set; }
        [ColumnAttrib("UserName")]
        public String UserName { get; set; }
        [ColumnAttrib("Password")]
        public String Password { get; set; }
        [ColumnAttrib("Token")]
        public String Token { get; set; }
        [ColumnAttrib("LateLoginTime")]
        public DateTime? LateLoginTime { get; set; }
        [ColumnAttrib("IsUse")]
        public bool IsUse { get; set; }
        [ColumnAttrib("Power")]
        public int Power { get; set; }
    }
}
