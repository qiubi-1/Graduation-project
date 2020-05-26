using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Account.Filter
{
    /// <summary>
    /// 特性类 过滤器 类命名规则：名+Attribute
    /// 需要引用System.Web.Mvc命名空间
    /// 需要继承ActionFilterAttribute
    /// </summary>
    public class LoginAttribute:ActionFilterAttribute
    {
        //正在发送请求时，进行响应
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //如果获得请求中Session["user"]里的值为空，就跳转到登录界面
            if (HttpContext.Current.Session["user"]==null)
            {
                //筛选器捕获请求的结果，地址转向
                filterContext.Result = new RedirectResult("/Home/Index");
                //HttpContext.Current.Response.Redirect("/Home/Index");
            }
            
        }
    }
}