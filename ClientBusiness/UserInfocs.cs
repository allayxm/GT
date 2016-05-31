using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JXDL.ClientBusiness
{
    public struct UserInfo
    {
        public int? ID { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String Token { get; set; }
        public DateTime? LateLoginTime { get; set; }
        public bool? IsUse { get; set; }
        public int? Power { get; set; }
    }
}
