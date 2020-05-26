using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MVC_T2_1.Controllers
{
    public class TimeController : Controller
    {
        // GET: Time
        public ActionResult Index()
        {           
            
            return View();
        }
        public ActionResult Test()
        {
            //ViewDate、ViewBag是同一个集合，可以互用，不能跨动作（方法、视同）读取
            ViewData["date"] = "ViewData";
            ViewBag.time = "ViewBag_time";
            //TempData:1、能跨动作（方法、视同）读取。2、临时的数据，只能读取一次，用完就销毁
            TempData["content"] = "TempData";
            return View();
        }
        public ActionResult Index1()
        {

            return View();
        }
    }
}