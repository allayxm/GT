using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JXDL.Manage.Models.Admin
{
    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name ="原始密码")]
        public string OldPassword { get; set; }
        [Required]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }
        [Required]
        [Display(Name = "确认密码")]
        [Compare("NewPassword", ErrorMessage = "请重新确认新密码")]
        public string ConfirmNewPassword { get; set; }
    }
}