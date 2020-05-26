using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RouteTest
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //配置分页路由
            routes.MapRoute(
                name: "Page",
                url: "admin/{controller}/{action}/{pageIndex}/{pageSize}",
                defaults: new { controller = "Teacher", action = "Index"}
                //defaults: new { controller = "Teacher", action = "Index", pageIndex = 2, pageSize = 6 }
            );
            //常规匹配路由
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            
        }
    }
}
