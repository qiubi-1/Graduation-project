using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RouteTest.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index(int pageIndex,int pageSize)
        {
            ViewBag.pageIndex = pageIndex;
            ViewBag.pageSize = pageSize;
            return View();
        }
    }
}