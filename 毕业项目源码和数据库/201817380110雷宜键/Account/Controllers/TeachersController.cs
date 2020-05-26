using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
namespace Account.Controllers
{
    public class TeachersController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Teachers
        public ActionResult Index()
        {
            List<Teacher> list = db.Teacher.ToList();
            return View(list);
        }
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(Teacher tea)
        {
            if (ModelState.IsValid)
            {
                db.Teacher.Add(tea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        
        public ActionResult Edit(int id)
        {
            Teacher tea = db.Teacher.Find(id);
            return View(tea);
        }
        [HttpPost]
        public ActionResult Edit(Teacher tea)
        {
            db.Entry(tea).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int TeacherID)
        {
            Teacher tea = db.Teacher.Find(TeacherID);
            db.Teacher.Remove(tea);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int TeacherID)
        {
            Teacher tea = db.Teacher.Find(TeacherID);
            return View(tea);
        }
        [HttpPost]
        public ActionResult DeleteAll()
        {
            //获得参数
            string teacher = Request["deleteList"];
            string[] list = { };
            list = teacher.Split(',');
            for (int i = 0; i < list.Length; i++)
            {
                int id = int.Parse(list[i]);
                Teacher tea= db.Teacher.Find(id);
                db.Teacher.Remove(tea);
            }
            int count= db.SaveChanges();
            if (count>0)
            {
                return Content("1");
            }
            else
            {
                return Content("0");
            }
            
        }
    }
}