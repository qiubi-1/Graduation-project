using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
namespace Account.Controllers
{
    public class RolesController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Roles
        public ActionResult Index(string name = "")
        {

            List<Roles> list = db.Roles.Where(p=>p.Name.Contains(name)|| name == "").ToList();
            ViewBag.name = name;
            return View(list);
        }
        [HttpPost]
        public ActionResult Add(Roles roles)
        {
            db.Roles.Add(roles);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int RRoleMenusID)
        {
            R_Role_Menus rrm = db.R_Role_Menus.Find(RRoleMenusID);
            db.R_Role_Menus.Remove(rrm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            Roles roles = db.Roles.Find(id);
            ViewBag.roles = roles;
            List<Menus> menus = db.Menus.ToList();
            ViewBag.menus = menus;
            List<R_Role_Menus> list = db.R_Role_Menus.Where(p => p.RoleID == id).ToList();
            ViewBag.list = list;
            return View();
        }
        [HttpPost]
        public ActionResult Edit(int id, List<int> roles)
        {
            var r = db.R_Role_Menus.Where(p => p.RoleID == id);
            foreach (var item in r)
            {
                db.R_Role_Menus.Remove(item);
            }
            if (roles != null)
            {
                foreach (var item in roles)
                {
                    R_Role_Menus rrm = new R_Role_Menus()
                    {
                        RoleID = id,
                        MenuID = item
                    };
                    db.R_Role_Menus.Add(rrm);
                }
                db.SaveChanges();
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Remove(int id)
        {
            //根据角色id查出角色菜单关系表，是否有菜单绑定
            //var rrm = db.R_Role_Menus.Where(p => p.RoleID == id).ToList();
            //根据角色id查出用户角色关系表，是否有用户绑定
            //var rur = db.R_User_Roles.Where(p => p.RoleID == id).ToList();
            //if (rrm.Count > 0)
            //{
            //    return Content("<script>alert('这个角色具有管理菜单的权限，无法删除');history.go(-1)</script>");
            //}
            //else if (rur.Count > 0)
            //{
            //    return Content("<script>alert('这个角色正在被用户使用，无法删除');history.go(-1)</script>");
            //}
            Roles roles = db.Roles.Find(id);
            if (roles.R_Role_Menus.Count > 0)
            {
                return Content("<script>alert('这个角色具有管理菜单的权限，无法删除');history.go(-1)</script>");
            }
            else if(roles.R_User_Roles.Count> 0)
            {
                return Content("<script>alert('这个角色正在被用户使用，无法删除');history.go(-1)</script>");
            }
            else
            {
                db.Roles.Remove(roles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }   
        }
    }
}