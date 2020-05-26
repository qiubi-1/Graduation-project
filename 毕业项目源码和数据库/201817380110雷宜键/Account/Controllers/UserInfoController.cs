using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Account.Models;
using System.Collections;

namespace Account.Controllers
{
    public class UserInfoController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: UserInfo
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="departmentID">部门查询</param>
        /// <param name="name">用户名模糊查询</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageCount">每页显示的行数</param>
        /// <returns></returns>
        public ActionResult Index(int departmentID=0,string name="",int pageIndex=1,int pageCount=5)
        {
            //获得下拉框的集合
            List<Departments> dlist = db.Departments.ToList();
            ViewBag.dlist = dlist;
            //获得所有的行数
            int tatalCount = db.UserInfos.OrderBy(p => p.ID)
                .Where(p => (departmentID == 0 || p.DepartmentID == departmentID) && (name == "" || p.Name.Contains(name)))
                .Count();
            //获得总页数 Ceiling()向上取整
            double tatalPage = Math.Ceiling(tatalCount / (double)pageCount);
            //获得用户表，先按照主键正序排列，条件过滤，转成集合
            //Skip()跳过指定数量的元素，返回剩下的集合
            //Take()从剩下的集合数中，从第一条开始获取数量的集合
            List<UserInfos> list = db.UserInfos.OrderBy(p=>p.ID)
                .Where(p=> (departmentID == 0||p.DepartmentID == departmentID) && (name == "" || p.Name.Contains(name)))
                .Skip((pageIndex-1)*pageCount).Take(pageCount).ToList();
            //列表加载的同时，将条件存储并在对应控件显示
            ViewBag.departmentID = departmentID;
            ViewBag.name = name;
            //当前页
            ViewBag.pageIndex = pageIndex;
            //每页行数
            ViewBag.pageCount = pageCount;
            //所有行数
            ViewBag.tatalCount = tatalCount;
            //总页数
            ViewBag.tatalPage = tatalPage;
            return View(list);
        }
        public ActionResult Add()
        {           
            ViewBag.dlist = db.Departments.ToList();
            ViewBag.list = db.Roles.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Add(UserInfos user, HttpPostedFileBase Photo, int[] roles) 
        {
            if (ModelState.IsValid)
            {
                foreach (var item in roles)
                {
                    R_User_Roles rur = new R_User_Roles();
                    rur.RoleID = item;
                    rur.UserID = user.ID;
                    db.R_User_Roles.Add(rur);
                }                        
                if (Photo != null)
                {
                    if (Photo.ContentLength > 0 && Photo.ContentLength < 4194304)
                    {
                        string fileName = Path.GetFileName(Photo.FileName);
                        //string suffix = fileName.Substring(fileName.LastIndexOf(".") + 1);
                        if (fileName.EndsWith("gif") || fileName.EndsWith("jpg") || fileName.EndsWith("png"))
                        {
                            Photo.SaveAs(Server.MapPath("~/Content/images/images/users/" + fileName));
                            user.Photo = fileName;
                        }
                    }
                    else
                    {
                        return Content("<script>alert('上传的文件必须是图片')</script>");
                    }
                }
                else
                {
                    return Content("<script>alert('未获取上传的文件')</script>");
                }
                db.UserInfos.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.dlist = db.Departments.ToList();
                ViewBag.list = db.Roles.ToList();
                return View();
            }
            
        }
        public ActionResult Edit(int UserID)
        {
            //获得用户信息
            ViewBag.user = db.UserInfos.Find(UserID);
            //获得所有的部门
            ViewBag.dlist = db.Departments.ToList();
            //获得所有的角色
            ViewBag.list = db.Roles.ToList();
            //获得用户绑定的角色
            ViewBag.Rur = db.R_User_Roles.Where(p => p.UserID == UserID).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Edit(UserInfos user, HttpPostedFileBase EPhoto, int[] roles)
        {
            //处理图片
            if (EPhoto != null)
            {
                if (EPhoto.ContentLength > 0 && EPhoto.ContentLength < 4194304)
                {
                    string fileName = Path.GetFileName(EPhoto.FileName);
                    //string suffix = fileName.Substring(fileName.LastIndexOf(".") + 1);
                    if (fileName.EndsWith("gif") || fileName.EndsWith("jpg") || fileName.EndsWith("png"))
                    {
                        EPhoto.SaveAs(Server.MapPath("~/Content/images/images/users/" + fileName));
                        user.Photo = fileName;
                    }
                }
            }
            //修改用户信息
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            //用户与角色的关系
            var r = db.R_User_Roles.Where(p => p.UserID == user.ID);
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
                        UserID = user.ID,
                        RoleID = roleID
                    };
                    db.R_User_Roles.Add(rur);
                }
                db.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }
        public ActionResult Remove(int UserID)
        {
            UserInfos user = db.UserInfos.Find(UserID);
            if (user.R_User_Roles.Count>0)
            {
                return Content("<script>alert('这个用户具有角色权限');history.go(-1)</script>");
            }
            else
            {
                db.UserInfos.Remove(user);
                db.SaveChanges();           
            }
            return RedirectToAction("Index");

        }
        public void DeleteAll(string deleteList)
        {
            deleteList = deleteList.Substring(0, deleteList.Length - 1);
            string[] list = deleteList.Split('.');
            foreach (var item in list)
            {
                int id = int.Parse(item);
                Teacher tea = db.Teacher.FirstOrDefault(p => p.TeacherID == id);
                db.Teacher.Remove(tea);
            }
            db.SaveChanges();          
        }
    }
}