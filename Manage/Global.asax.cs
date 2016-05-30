using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.Optimization;
using System.Web.SessionState;
using System.Web.Http;
using System.Security.Principal;
using MXKJ.DBMiddleWareLib;
using JXDL.Manage.App_Start;

namespace Manage
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BasicDBClass.DataSource = System.Web.Configuration.WebConfigurationManager.AppSettings["DBServerName"];
            BasicDBClass.DBName = System.Web.Configuration.WebConfigurationManager.AppSettings["DBName"];
            BasicDBClass.UserID = System.Web.Configuration.WebConfigurationManager.AppSettings["DBUser"];
            BasicDBClass.Password = System.Web.Configuration.WebConfigurationManager.AppSettings["DBPassword"];
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var app = sender as HttpApplication;

            if (app.Context.User != null)
            {
                var user = app.Context.User;
                var identity = user.Identity as FormsIdentity;

                // We could explicitly construct an Principal object with roles info using System.Security.Principal.GenericPrincipal
                var principalWithRoles = new GenericPrincipal(identity, identity.Ticket.UserData.Split(','));

                // Replace the user object
                app.Context.User = principalWithRoles;

            }
        }
    }
}