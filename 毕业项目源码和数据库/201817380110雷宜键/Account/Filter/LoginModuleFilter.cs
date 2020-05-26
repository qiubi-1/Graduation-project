using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Account.Filter
{
    public class LoginModuleFilter : IHttpModule
    {
        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            //AcquireRequestState 能获取Session，Session可用
            context.AcquireRequestState += Context_AcquireRequestState;
        }
        /// <summary>
        /// 处理事件（拦截登录的过滤器）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Context_AcquireRequestState(object sender, EventArgs e)
        {
            //获得应用请求
            HttpApplication app = sender as HttpApplication;
            //可以点到4个内置对象，获取有关当前请求的http特定信息
            //四个内置对象：request response session server
            HttpContext context = app.Context;
            //获得游览器端请求的url路径
            string Url = context.Request.Url.ToString();
            //不过滤css/js/jpg/png/fonts
            if (Url.ToLower().Contains("css") || Url.ToLower().Contains("js") ||
                Url.ToLower().Contains("jpg") || Url.ToLower().Contains("png")||
                Url.ToLower().Contains("fonts"))
            {
                return;
            }
            else
            {
                //将请求的地址转成小写，判断是否包含登录路径（/home/index）
                if (!Url.ToLower().Contains("/home/index"))
                {
                    //判断是否登录
                    if (context.Session["user"]==null)
                    {
                        //发送地址到游览器端请求到登录地址
                        context.Response.Redirect("/Home/Index");
                    }
                }
            }
            
        }
    }
}