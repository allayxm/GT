using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JXDL.ManageBusiness;
using JXDL.Manage.Models.Home;
using JXDL.ManageEFModel;
using System.Web.Security;

namespace JXDL.Manage.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            UserManage vUserManage = new UserManage();
            //ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Index(IndexViewModel Model, string returnUrl)
        {
            AdminManage vAdminManage = new AdminManage();
            AdminUserEF vAdminInfo = vAdminManage.Login(Model.UserName, Model.Password);
            if (vAdminInfo.ID > 0)
            {
                /////////////////////////////////////////////////////////////////Form验证////////////////////////////////////
                Session["UserInfo"] = vAdminInfo;
                FormsAuthentication.RedirectFromLoginPage(vAdminInfo.UserName, false);
                FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(1, vAdminInfo.UserName, DateTime.Now, DateTime.Now.AddMinutes(30), false, "Admin", "/");
                string HashTicket = FormsAuthentication.Encrypt(Ticket);
                HttpCookie UserCookie = new HttpCookie(FormsAuthentication.FormsCookieName, HashTicket);
                HttpContext.Response.Cookies.Add(UserCookie);
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (returnUrl != "" && returnUrl != null)
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Main", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "用户名或密码错误");
                return View();
            }
        }
    }
}