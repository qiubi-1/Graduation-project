using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;

namespace Account.Controllers
{
    public class TopicsController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Topics
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create(int PaperID)
        {
            Paper p = db.Paper.Find(PaperID);
            ViewBag.p = p;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Topic t)
        {
            if (ModelState.IsValid)
            {
                db.Topic.Add(t);
                db.SaveChanges();
                return RedirectToAction("Index", "Papers");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Delete(int TopicID)
        {
            Topic topic = db.Topic.Find(TopicID);

            return View(topic);
        }
        [HttpPost]
        public ActionResult Remove(int TopicID)
        {
            Topic topic = db.Topic.Find(TopicID);
            db.Topic.Remove(topic);
            db.SaveChanges();
            return RedirectToAction("Index", "Papers");
        }
        public ActionResult Edit(int TopicID)
        {
            Topic topic = db.Topic.Find(TopicID);
            List<Paper> p = db.Paper.ToList();
            ViewBag.p = p;
            return View(topic);
        }
        [HttpPost]
        public ActionResult Edit(Topic topic)
        {
            db.Entry(topic).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Papers");
        }
    }
}