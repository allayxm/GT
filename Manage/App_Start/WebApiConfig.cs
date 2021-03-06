﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Manage
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DeleteApi",
                routeTemplate: "api/{controller}/{id}/UserID/UserName",
                defaults: new { id = RouteParameter.Optional, UserID = RouteParameter.Optional, UserName = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
