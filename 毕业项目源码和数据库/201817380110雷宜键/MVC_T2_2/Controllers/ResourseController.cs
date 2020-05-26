using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_T2_2.Controllers
{
    public class ResourseController : Controller
    {
        // GET: Resourse
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 文件模型绑定
        /// </summary>
        /// <param name="resourceFile">获取由客户端上传的单个文件</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase resourceFile)
        {
            if (resourceFile!=null)
            {
                if (resourceFile.ContentLength>0 && resourceFile.ContentLength<4194304)
                {
                    string fileName = Path.GetFileName(resourceFile.FileName);
                    string suffix = fileName.Substring(fileName.LastIndexOf(".") + 1);
                    if (suffix=="gif"||suffix=="jpg"||suffix=="png")
                    {
                        resourceFile.SaveAs(Server.MapPath("~/UploadFile/" + fileName));               
                        ViewBag.Message = "文件上传成功！";
                        ViewBag.ImgSrc = fileName;
                    }
                    
                    
                }
                else
                {
                    ViewBag.Message = "文件大小不符要求！";
                }
            }
            else
            {
                ViewBag.Message = "请上传文件！";
            }
            return View();
        }
    }
}