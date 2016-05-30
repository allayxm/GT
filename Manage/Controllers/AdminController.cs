using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JXDL.Manage.Models.Admin;
using JXDL.ManageEFModel;
using JXDL.ManageBusiness;
using System.Web.Security;

namespace JXDL.Manage.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Main()
        {
            MainViewModel vModel = new MainViewModel();
            AdminUserEF vUserInfo = (AdminUserEF)Session["UserInfo"];
            vModel.UserName = vUserInfo.UserName;
            vModel.LoginLateTime = vModel.LoginLateTime;
            UserManage vUserManage = new UserManage();
            vModel.OnlineUsersNumber = vUserManage.OnlineUsersInfo().Count;
            return View(vModel);
        }


        public ActionResult ChangePassword()
        {
            ChangePasswordViewModel vModel = new ChangePasswordViewModel();
            return View(vModel);
        }


        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel vModel)
        {
            AdminManage vAdminManage = new AdminManage();
            bool vResult = vAdminManage.ChangePassword("Admin", vModel.OldPassword, vModel.NewPassword);
            if (vResult)
                ModelState.AddModelError("", "密码更新成功");
            else
                ModelState.AddModelError("", "密码更新失败");
            return View(vModel);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult UserList()
        {

            return View();
        }

    }
}