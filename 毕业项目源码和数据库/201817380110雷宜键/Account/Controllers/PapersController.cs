using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
namespace Account.Controllers
{
    public class PapersController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Papers
        public ActionResult Index()
        {
            List<Paper> p = db.Paper.ToList();
            return View(p);
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Paper p)
        {
            if (ModelState.IsValid)
            {
                db.Paper.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Edit(int PaperID)
        {
            Paper p = db.Paper.Find(PaperID);
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(Paper p)
        {
            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int PaperID)
        {
            Paper p = db.Paper.Find(PaperID);
            List<Topic> list = db.Topic.Where(k => k.PaperID == PaperID).ToList();
            ViewBag.list = list;
            ViewBag.p = p;
            return View();
        }
        public ActionResult IndexStu()
        {
            List<Paper> paper = db.Paper.ToList();
            
            return View(paper);
        }
        public ActionResult BeginAnswer(int PaperID)
        { 

            return RedirectToAction("AnswerDetail", "Answers",new {id= PaperID });
        }
    }
}