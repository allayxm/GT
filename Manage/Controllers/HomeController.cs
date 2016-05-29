using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JXDL.ManageBusiness;

namespace JXDL.Manage.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            UserManage vUserManage = new UserManage();
            return View();
        }
    }
}