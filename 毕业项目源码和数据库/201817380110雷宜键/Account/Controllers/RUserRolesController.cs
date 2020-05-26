using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
namespace Account.Controllers
{

    public class RUserRolesController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: RUserRoles
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Delete(int? RUserRolesID)
        {
            //获得删除的对象
            R_User_Roles r = db.R_User_Roles.Find(RUserRolesID);
            //EF remove删除对象
            db.R_User_Roles.Remove(r);
            //从数据库中删除对象
            db.SaveChanges();
            return RedirectToAction("Index", "UserInfo");
        }
        public ActionResult SetRole(int UserID)
        {
            //通过用户ID查到用户信息，并存到ViewBag中
            UserInfos user = db.UserInfos.Find(UserID);
            ViewBag.user = user;
            //通过用户ID查到用户对应的角色关系，并存到ViewBag中
            List<R_User_Roles> r = db.R_User_Roles.Where(p=>p.UserID==UserID).ToList();
            ViewBag.r = r;
            //获得所有的角色,并存到ViewBag中
            List<Roles> list = db.Roles.ToList();
            ViewBag.list = list;
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID">获得视图中的用户ID</param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetRole(int? UserID,List<int> roles)
        {
            //先通过用户ID去清除所有与该用户相关的角色关系
            var r = db.R_User_Roles.Where(p => p.UserID == UserID);
            foreach (var item in r)
            {
                db.R_User_Roles.Remove(item);
            }
            db.SaveChanges();
            if (roles!=null)
            {
                //添加新的角色关系
                foreach (var roleID in roles)
                {
                    //角色ID和用户ID组成新的R_User_Roles对象
                    R_User_Roles rur = new R_User_Roles()
                    {
                        UserID = UserID,
                        RoleID = roleID
                    };
                    db.R_User_Roles.Add(rur);
                }
                db.SaveChanges();
            }
            
            
            return RedirectToAction("Index", "UserInfo");
        }
    }
}