using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StuSystem.Models;

namespace StuSystem.Controllers
{
    public class StudentController : Controller
    {
        StuSystemEntities db = new StuSystemEntities();
        // GET: Student
        public ActionResult Index()
        {
            List<Student> list = db.Student.Include("Grades1").ToList();          
            return View(list);
        }
        
        public ActionResult Register()
        {
            List<SelectListItem> list= db.Grades.Select(p => new SelectListItem()
            {
                Text = p.GradesName,
                Value = p.GradesID.ToString()
            }).ToList();
            ViewBag.Grade = list;
            return View();
        }
        [HttpPost]
        public ActionResult Register(Student stu)
        {
            if (ModelState.IsValid)
            {
                db.Student.Add(stu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                List<SelectListItem> list = db.Grades.Select(p => new SelectListItem()
                {
                    Text = p.GradesName,
                    Value = p.GradesID.ToString()
                }).ToList();
                ViewBag.Grade = list;
                return View();
            }           
        }

    }
}