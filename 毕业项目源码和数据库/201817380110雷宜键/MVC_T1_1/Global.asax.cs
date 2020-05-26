using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVC_T1_1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //过滤——App_Start的FilterConfig
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //配置路由——App_Start的RouteConfig
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //将文件捆绑，将设定的某种文件捆成一捆，App_Start的BundleConfig
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
