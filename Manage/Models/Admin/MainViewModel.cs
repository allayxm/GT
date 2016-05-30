using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXDL.Manage.Models.Admin
{
    public class MainViewModel
    {
        public int OnlineUsersNumber { get; set; }
        public string UserName { get; set; }
        public DateTime LoginLateTime { get; set; }
    }
}