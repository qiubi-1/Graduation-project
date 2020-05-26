using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_T2_2.Models;
namespace MVC_T2_2.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string userName,string userPwd)
        {
            ViewBag.UN = userName;
            ViewBag.PW = userPwd;

            return View();
        }
        public ActionResult IsLike()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IsLike(List<string> like)
        {
            ViewBag.GetLike = like;
            return View();
        }
        public ActionResult Buy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Buy(List<Person> person)
        {
            ViewBag.List = person;
            return View();
        }
    }
}