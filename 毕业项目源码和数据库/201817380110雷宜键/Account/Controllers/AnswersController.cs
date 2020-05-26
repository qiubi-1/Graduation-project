using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Account.Models;
using PagedList;
namespace Account.Controllers
{
    public class AnswersController : Controller
    {
        AccountDBEntities db = new AccountDBEntities();
        // GET: Answers
        public ActionResult Index(int PaperID, int StuID)
        {
            Answer answer = db.Answer.FirstOrDefault(a => a.PaperID == PaperID && a.StuID == StuID);
            return View(answer);
        }
        [HttpPost]
        public void Index(int AnswerID, int TopicID, string DetailAnswer)
        {
            var result = db.Detail.FirstOrDefault(p => p.TopicID == TopicID && p.AnswerID == AnswerID);
            result.DetailAnswer = DetailAnswer;
            db.SaveChanges();
        }
        public ActionResult MyAnswer()
        {
            Student stu = Session["Student"] as Student;
            List<Answer> answers = db.Answer.Where(p => p.StuID == stu.StuID).ToList();
            ViewBag.answers = answers;
            return View();
        }
        public ActionResult AnswerDetail(int PaperID,int StuID)
        {
            //第一步 填写答题信息
            Answer answer = new Answer()
            {
                PaperID = PaperID,  //试卷ID
                StuID = StuID,      //学生ID
                TeacherID = 1,      //老师ID
                AnswerScore = 0,    //考试成绩，默认为0，老师审卷完再修改统分
                AnswerTime = DateTime.Now,  //开始答题时间，默认是系统时间
                SubmitTime = null,          //学生点击提交试卷的时间
                BatchTime = null,           //老师点击批改试卷的时间
                AnswerState = 0             //0 考试中，1 未审卷 ，2 已审卷
            };
            db.Answer.Add(answer);
            db.SaveChanges();

            //第二步 答题卡
            //查找试卷对应的所有题目
            List<Topic> list = db.Topic.Where(p => p.PaperID == PaperID).ToList();
            foreach (var item in list)
            {
                Detail detail = new Detail()
                {
                    AnswerID = answer.AnswerID,
                    TopicID = item.TopicID,
                    DetailAnswer = ""
                };
                db.Detail.Add(detail);
            }
            db.SaveChanges();

            return RedirectToAction("Index", "Answers", new { PaperID = PaperID, StuID = StuID });
        }
        public ActionResult SubmitPaper(int AnswerID)
        {
            Answer answer = db.Answer.Find(AnswerID);
            answer.SubmitTime = DateTime.Now;
            answer.AnswerState = 1;
            db.SaveChanges();
            return RedirectToAction("IndexStu","Papers");
        }
        public ActionResult TeAnswer(int? page)
        {
            Teacher tea = Session["teacher"] as Teacher;
            //获取对应的Answer
            List<Answer> list = db.Answer.Where(p => p.TeacherID == tea.TeacherID).ToList();
            //当前页，合并运算符
            int pageNumber = page ?? 1;
            //每页显示的行数
            int pageSize = 10;
            //将我们的集合转成分页的集合
            IPagedList<Answer> answers = list.ToPagedList(pageNumber, pageSize);
            ViewBag.answers = answers;
            //将分页处理后的列表传给View 
            return View();
        }
        public ActionResult TeAnswerDetail(int AnswerID)
        {
            Answer answer = db.Answer.Find(AnswerID);
            return View(answer);
        }
        public ActionResult Verify(int AnswerID)
        {
            Answer answer = db.Answer.Find(AnswerID);
            answer.AnswerState = 2;
            db.SaveChanges();
            return RedirectToAction("TeAnswer");
        }
    }
}