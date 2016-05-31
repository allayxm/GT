using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JXDL.Manage.Models.Admin;
using JXDL.ManageEFModel;
using JXDL.ManageBusiness;
using System.Web.Security;
using MXKJ.DBMiddleWareLib;

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
            vModel.OnlineUsersNumber = vUserManage.OnlineUsersInfo().Length;
            return View(vModel);
        }

        #region 更改管理员密码
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
        #endregion

        #region 注销
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region 用户管理
        public ActionResult UserList()
        {
            UserListViewModel vModel = new UserListViewModel();
            vModel.UserList = new List<UserViewModel>();
            UserManage vUsersManage = new UserManage();
            UsersEF[] vUsers = vUsersManage.GetAllNormalUsers();
            foreach( UsersEF vTempUser in vUsers )
            {
                UserViewModel vUserModel = new UserViewModel();
                vUserModel.ID = DBConvert.ToInt32(  vTempUser.ID ).Value;
                vUserModel.UserName = vTempUser.UserName;
                vUserModel.LateLoginTime = vTempUser.LateLoginTime;
                vUserModel.PowerName = UserManage.ConverPowerIDToName( DBConvert.ToInt32( vTempUser.Power ).Value);
                vModel.UserList.Add(vUserModel);
            }
            return View(vModel);
        }

        public ActionResult EditUser(int UserID)
        {
            UserManage vUserMange = new UserManage();
            EditUserViewModel vUserModel = new EditUserViewModel();
            UsersEF vUserInfo = vUserMange.GetUserInfo(UserID);
            vUserModel.ID = vUserInfo.ID.Value;
            vUserModel.UserName = vUserInfo.UserName;
            vUserModel.Password = vUserInfo.Password;
            vUserModel.Power = vUserInfo.Power.Value;
            vUserModel.PowerList = createPowerSelectList();
            return View(vUserModel);
        }

        [HttpPost]
        public ActionResult EditUser(EditUserViewModel Model)
        {
            UserManage vUserMange = new UserManage();
            bool vResult = vUserMange.UpdateUser(Model.ID, Model.Password, Model.Power);
            if (vResult)
                return RedirectToAction("UserList", "Admin");
            else
            {
                ModelState.AddModelError("", "更新用户信息失败");
                return View(Model);
            }
            
        }

        public bool DeleteUsers(string UsersValue)
        {
            bool vResult = false;
            UserManage vUserMange = new UserManage();
            string[] vUserArray = UsersValue.Split('|');
            foreach( string vUser in vUserArray )
            {
                int vUserID = int.Parse( vUser );
                vResult = vUserMange.DeleteUses(vUserID);
                if (!vResult)
                    break;
            }
            return vResult;
        }

        public ActionResult AddUser()
        {
            AddUserViewModel vModel = new AddUserViewModel();
            vModel.PowerList = createPowerSelectList();
            return View(vModel);
        }

        [HttpPost]
        public ActionResult AddUser(AddUserViewModel Model)
        {
            UserManage vUserMange = new UserManage();
            UsersEF vUserInfo =  vUserMange.GetUserInfo(Model.UserName);
            if (vUserInfo.ID==null || vUserInfo.ID == 0)
            {

                if (vUserMange.AddUser(Model.UserName, Model.Password, Model.Power))
                    return RedirectToAction("UserList", "Admin");
                else
                {
                    ModelState.AddModelError("", "添加用户失败");
                    Model.PowerList = createPowerSelectList();
                    return View(Model);
                }
            }
            else
            {
                ModelState.AddModelError("", "用户名重复");
                Model.PowerList = createPowerSelectList();
                return View(Model);
            }
        }
        #endregion

        #region 在线用户
        public ActionResult OnlineUsers()
        {
            OnlineUsersViewModel vModel = new OnlineUsersViewModel();
            vModel.UserList = new List<UserViewModel>();
            
            UserManage vUsersManage = new UserManage();
            UsersEF[] vUsers = vUsersManage.OnlineUsersInfo();
            foreach (UsersEF vTempUser in vUsers)
            {
                UserViewModel vUserModel = new UserViewModel();
                vUserModel.ID = DBConvert.ToInt32(vTempUser.ID).Value;
                vUserModel.UserName = vTempUser.UserName;
                vUserModel.LateLoginTime = vTempUser.LateLoginTime;
                vUserModel.PowerName = UserManage.ConverPowerIDToName(DBConvert.ToInt32(vTempUser.Power).Value);
                vModel.UserList.Add(vUserModel);
            }
            return View(vModel);
        }
        #endregion

        #region 用户操作日志
        public ActionResult Log()
        {
            LogListViewModel vModel = new LogListViewModel();
            vModel.LogList = new List<LogViewModel>();
            UserOperateLog vUserOperateLog = new UserOperateLog();
            LogEF[] vLogList = vUserOperateLog.GetAllLog();
            foreach( LogEF log in vLogList)
            {
                LogViewModel vLogModel = new LogViewModel();
                vLogModel.ID = log.ID.Value;
                vLogModel.UserID = log.UserID.Value;
                vLogModel.UserName = log.UserName;
                vLogModel.Type = log.Type;
                vLogModel.OperateTime = log.OperateTime.Value;
                vModel.LogList.Add(vLogModel);
            }
            return View(vModel);
        }
        #endregion

        #region 地图服务配置
        public ActionResult MapServerConfig()
        {
            MapServerConfigViewModel vModel = new MapServerConfigViewModel();
            MapServer vMapServer = new MapServer();
            SystemConfigEF vMapConfigEF =  vMapServer.GetMapConfig();
            vModel.MapServerAddress = vMapConfigEF.ItemValue1;
            vModel.MapServerName = vMapConfigEF.ItemValue2;
            return View(vModel);
        }

        [HttpPost]
        public ActionResult MapServerConfig(MapServerConfigViewModel Model)
        {
            MapServer vMapServer = new MapServer();
            if (vMapServer.SetMapConfig(Model.MapServerAddress, Model.MapServerName))
                ModelState.AddModelError("","地图服务设置成功" );
            else
                ModelState.AddModelError("", "地图服务设置失败");
            return View(Model);
        }
        #endregion

        List<SelectListItem> createPowerSelectList()
        {
            List<SelectListItem> vSelectList = new List<SelectListItem> {
                new SelectListItem() {  Text="材民", Value="1"},
                new SelectListItem() {  Text="材委会", Value="2"},
                new SelectListItem() {  Text="政府及城建部门", Value="3"}

            };
            return vSelectList;
        }
    }
}