using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
namespace Account.Controllers
{
    public class StudentsController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Students
        public ActionResult Index()
        {
            List<Student> list = db.Student.ToList();
            return View(list);
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Student stu)
        {
            if (ModelState.IsValid)
            {
                Student s = db.Student.SingleOrDefault(p => p.StuLoginName == stu.StuLoginName);
                if (s==null)
                {
                    db.Student.Add(stu);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("<script>alert('登录名已存在')</script>");
                }
                
            }
            else
            {
                return View();
            }
            
        }
        public ActionResult Edit(int StuID)
        {
            Student stu = db.Student.Find(StuID);
            return View(stu);
        }
        [HttpPost]
        public ActionResult Edit(Student stu)
        {
            db.Entry(stu).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int StuID)
        {
            Student stu = db.Student.Find(StuID);
            return View(stu);
        }
        public ActionResult Delete(int StuID)
        {
            Student stu = db.Student.Find(StuID);
            db.Student.Remove(stu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}