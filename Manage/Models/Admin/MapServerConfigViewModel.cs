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
        [Display(Name = "服务器名称")]
        [Required]
        public string MapServerName { get; set; }
    }
}