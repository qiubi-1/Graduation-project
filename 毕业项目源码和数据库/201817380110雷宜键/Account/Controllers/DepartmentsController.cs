using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
using Account.Filter;
namespace Account.Controllers
{
    [Login]
    public class DepartmentsController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Departments
        public ActionResult Index()
        {
            List<Departments> list = db.Departments.ToList();
            return View(list);
        }
        public ActionResult Edit(int id)
        {
            Departments d = db.Departments.Find(id);           
            return View(d);
        }
        [HttpPost]
        public ActionResult Edit(Departments d)
        {
            db.Entry(d).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            Departments d = db.Departments.Find(id);
            if (d.UserInfos.Count>0)
            {
                return Content("<script>alert('这个部门正在被用户使用，无法删除');history.go(-1)</script>");
            }
            db.Departments.Remove(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}