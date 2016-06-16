using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JXDL.Manage.Models.Admin
{
    public class MapServerConfigViewModel
    {
        [Display(Name ="服务器地址")]
        [Required]
        public string MapServerAddress { get; set; }

        [Display(Name = "数据库名称")]
        [Required]
        public string MapDBName { get; set; }

        [Display(Name = "数据库用户名")]
        [Required]
        public string DBUserName { get; set; }

        [Display(Name = "数据库密码")]
        [Required]
        public string DBPassword { get; set; }

    }
}