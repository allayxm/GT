using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JXDL.Manage.Models.Admin
{
    public class AddUserViewModel
    {
        public List<System.Web.Mvc.SelectListItem> PowerList { get; set; }
        public int ID { get; set; }
        [Required]
        [Display(Name = "用户名")]
        public String UserName { get; set; }
        [Required]
        [Display(Name = "新密码")]
        public String Password { get; set; }

        [Required]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "请重新确认新密码")]
        public string ConfirmNewPassword { get; set; }

        public int Power { get; set; }
    }
}