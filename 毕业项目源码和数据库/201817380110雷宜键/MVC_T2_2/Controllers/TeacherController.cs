using MVC_T2_2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace MVC_T2_2.Controllers
{
    public class TeacherController : Controller
    {
        ExamDBEntities db = new ExamDBEntities();
        // GET: Teacher
        public ActionResult Index()
        {
            //view()传参
            List<Teacher> list = db.Teacher.ToList();
            //return View(list);
            //ViewBag.list = list;
            ViewData["list"] = list;
            return View();
        }
        [HttpPost]
        public ActionResult Index(string name)
        {
            var obj = db.Teacher.Where(p => p.TeacherName.Contains(name));
            List<Teacher> list = obj.ToList();
            ViewData["list"] = list;
            return View();
        }
        //[HttpPost]
        //public ActionResult Index(string teaName, string teaLoginName)
        //{
        //    //LinQ方法
        //    //List<Teacher> list = db.Teacher.Where(p => p.TeacherName.Contains(teaName) || teaName == "").
        //    //Where(p => p.TeacherLoginName.Contains(teaLoginName) || teaLoginName == "").ToList();

        //    //LinQ语句
        //    List<Teacher> list = (from tea in db.Teacher where tea.TeacherName.Contains(teaName) &&
        //    (tea.TeacherLoginName == teaLoginName || tea.TeacherLoginName == "")
        //    select tea).ToList();
        //    ViewData["list"] = list;
        //    return View();
        //}

        public ActionResult ShowInfo()
        {

            //获得请求地址的参数id
            string id = Request["id"];
            //根据id查数据库中存储的对象
            Teacher teacher = db.Teacher.Find(int.Parse(id));
            ViewBag.teacher = teacher;
            return View(teacher);
        }
        public ActionResult Delete(string id)
        {
            //获得对象
            Teacher teacher = db.Teacher.Find(int.Parse(id));
            //移除
            db.Teacher.Remove(teacher);
            //执行
            db.SaveChanges();
            //转向到Index()动作
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }
        //在asp.net mvc框架中，为了限制某个action(动作-方法)只能接受httpPost的请求、httpGet的请求
        //[httpPost]:限制action只能接受httpPost的请求，对于httpGet的请求则提示404找不到页面
        //如果Action前没有[httpPost]，也没有[httpGet]，则两种方式的请求都接收
        [HttpPost]
        public ActionResult Add(Teacher teacher)
        {
            db.Teacher.Add(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Teacher tea = db.Teacher.Find(id);
            return View(tea);
        }
        /// <summary>
        /// DefaultModelBinder类自动实现默认模型绑定器
        /// 通过四种途径获得绑定值：
        /// Request.Form：获得表单提交的值
        /// RouteData.Values：获得路由的值
        /// Request.QueryString：获得URL的值
        /// Request.Files：获得上传文件
        /// </summary>
        /// <param name="newTea"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Teacher newTea)
        {
            //Teacher newtea = new Teacher();
            //if (Request.Form.Count>0)
            //{
            //    newtea.TeacherID = int.Parse(Request.Form["TeacherID"]);
            //    newtea.TeacherLoginName = Request.Form["TeacherLoginName"];
            //    newtea.TeacherLoginPwd = Request.Form["TeacherLoginPwd"];
            //    newtea.TeacherName = Request.Form["TeacherName"];
            //    newtea.TeacherEmail = Request.Form["TeacherEmail"];
            //    newtea.TeacherPhone = Request.Form["TeacherPhone"];
            //}
            Teacher oldtea = db.Teacher.Find(newTea.TeacherID);
            oldtea.TeacherLoginName = newTea.TeacherLoginName;
            oldtea.TeacherLoginPwd = newTea.TeacherLoginPwd;
            oldtea.TeacherName = newTea.TeacherName;
            oldtea.TeacherEmail = newTea.TeacherEmail;
            oldtea.TeacherPhone = newTea.TeacherPhone;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Show()
        {
            //注意：如果传输字符串的参数，需要model:""
            //return View(model:"查询数据成功");
            List<Teacher> list = db.Teacher.ToList();

            return View("Index", list);
        }


    }
}