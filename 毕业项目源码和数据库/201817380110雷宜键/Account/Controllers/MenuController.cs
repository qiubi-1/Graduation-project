using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
namespace Account.Controllers
{
    public class MenuController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Menu
        public ActionResult Index(string name = "")
        {
            List<Menus> list = db.Menus.Where(p=>p.Name.Contains(name)||name=="").ToList();
            ViewBag.name = name;
            return View(list);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Menus menus)
        {
            db.Menus.Add(menus);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int ID)
        {
            Menus menus = db.Menus.Find(ID);
            //Menus menus1 = db.Menus.Where(p=>p.ID==ID).SingleOrDefault();
            //Menus menus2 = db.Menus.SingleOrDefault(p => p.ID == ID);
            return View(menus);
        }
        [HttpPost]
        public ActionResult Edit(Menus menus)
        {           
            db.Entry(menus).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Remove(int id)
        {
            List<R_Role_Menus> rrm = db.R_Role_Menus.Where(p => p.MenuID == id).ToList();
            if (rrm.Count>0)
            {
                return Content("<script>alert('请解除该菜单的角色绑定，再删除该菜单');history.go(-1)</script>");
            }
            Menus menus = db.Menus.Find(id);
            db.Menus.Remove(menus);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}