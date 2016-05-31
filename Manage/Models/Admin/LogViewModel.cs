using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXDL.Manage.Models.Admin
{
    public class LogListViewModel
    {
        public List<LogViewModel> LogList { get; set; }
    }

    public class LogViewModel
    {
        public int ID { get; set; }
        public int UserID { get; set; } 
        public string UserName { get; set; }
        public string Type { get; set; }
        public DateTime OperateTime { get; set; }
    }
}