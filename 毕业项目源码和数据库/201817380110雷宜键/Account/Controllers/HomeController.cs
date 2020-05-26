using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
namespace Account.Controllers
{
    public class HomeController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        public ActionResult Index()
        {        
            return View();
        }
        [HttpPost]
        public ActionResult Index(string Account, string Pwd)
        {
            //根据账号和密码查找用户表中唯一用户
            UserInfos user = db.UserInfos.Where(p => p.Account == Account && p.Pwd == Pwd).SingleOrDefault();
            if (user!=null)
            {
                //存在，即将查找的账号存入到Session对象中
                Session["user"] = user;
                //将视图作为查询对象v_User_Role_Menu，过滤，根据当前用户的ID，Distinct去除集合中的重复行
                List<v_User_Role_Menu> list = db.v_User_Role_Menu.Where(p=>p.UserID==user.ID).Distinct().ToList();
                Session["v_User_Role_Menu"] = list;
                return RedirectToAction("Page");
            }
            else
            {
                //不存在，返回登录界面
                return Content("<script>alert('账号或密码不正确！');history.go(-1)</script>");
            }            
        }
        public ActionResult ExamIndex()
        {
            return View();
        }
        public ActionResult Page()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Exit()
        {
            Session["user"] = null;
            return RedirectToAction("Index");    
        }
        
        public ActionResult LoginTeacher()
        {

            return View();
        }
        [HttpPost]
        public ActionResult LoginTeacher(string TeacherLoginName,string TeacherLoginPwd)
        {
            if (ModelState.IsValid)
            {
                Teacher tea = db.Teacher.Where(p => p.TeacherLoginName == TeacherLoginName && p.TeacherLoginPwd == TeacherLoginPwd).SingleOrDefault();
                if (tea!=null)
                {
                    Session["Teacher"] = tea;
                    return RedirectToAction("ExamIndex", "Home");
                }
                else
                {
                    ModelState.AddModelError("flag", "用户名或账号输入错误");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult LoginStudent()
        {
             return View();
        }
        [HttpPost]
        public ActionResult LoginStudent(string StuLoginName,string StuLoginPwd)
        {
            Student stu = db.Student.Where(p => p.StuLoginName == StuLoginName && p.StuLoginPwd == StuLoginPwd).SingleOrDefault();
            if (stu != null)
            {
                Session["Student"] = stu;
                return RedirectToAction("ExamIndex", "Home");
            }
            else
            {
                ModelState.AddModelError("flag", "用户名或账号输入错误");
                return View();
            }
        }
        public ActionResult Exit_1()
        {
            Session["Teacher"] = null;
            Session["Student"] = null;
            return RedirectToAction("ExamIndex");
        }

        public ActionResult AjaxLogin()
        {
            return View();
        }
        /// <summary>
        /// ajax验证
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        [HttpPost]
        public string CheckLoginName(string LoginName)
        {
            Student stu = db.Student.FirstOrDefault(p => p.StuLoginName == LoginName);
            if (stu==null)
            {
                return "false";
            }
            else
            {
                return "true";
            }
        }
    }
}