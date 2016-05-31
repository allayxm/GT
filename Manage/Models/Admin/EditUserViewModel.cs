using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JXDL.Manage.Models.Admin
{
    public class EditUserViewModel
    {
        public List<System.Web.Mvc.SelectListItem> PowerList { get; set; }

        public int ID { get; set; }
        
        [Display(Name = "用户名")]
        public String UserName { get; set; }
        [Required]
        [Display(Name = "新密码")]
        public String Password { get; set; }
        public int Power { get; set; }
        
    }
}