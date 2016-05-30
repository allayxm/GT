using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXDL.Manage.Models.Admin
{
    public class UserListViewModel
    {
        public List<UserViewModel> UserList { get; set; }
    }

    public class UserViewModel
    {
        public int ID { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String Token { get; set; }
        public DateTime? LateLoginTime { get; set; }
        public bool IsUse { get; set; }
        public int Power { get; set; }
        public string PowerName { get; set; }
    }
}