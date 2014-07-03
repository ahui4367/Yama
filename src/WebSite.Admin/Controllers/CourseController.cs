using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Service.Facade;
using View.Model;
using WebCore.Service;

namespace WebSite.Admin.Controllers
{
    public class CourseController : Controller
    {
        //
        // GET: /Course/

        public ActionResult Index()
        {
            return View();
        }

        // GET: /Course/List
        public ActionResult List(PagingModel paging)
        {
            IEnumerable<CourseModel> result = new List<CourseModel>();
            var list = ServiceFacade.CourseSvc.LoadCourses();
            if (list != null && list.Count() > 0)
            {
                result = list.Skip((paging.Page -1) * paging.Rows).Take(paging.Rows);
            }

            return Json(new { total = list.Count(), rows = result.ToArray() }, JsonRequestBehavior.AllowGet);
        }

        // GET: /Course/Edit
        public ActionResult Edit(int? course_id)
        {
            CourseModel model = null;
            if (course_id.HasValue && course_id.Value > 0)
            {
                model = ServiceFacade.CourseSvc.LoadCourse(course_id.Value);
            }
            else
            {
                model = new CourseModel { Cid = -1, CourseName = "新课件" };
            }
            return View(model);
        }

        public ActionResult Save(CourseModel model)
        {
            int dotIndex = model.Media.LastIndexOf('.');
            if (dotIndex > -1) 
            {
                model.Category = model.Media.Substring(0, dotIndex);
            }

            try
            {
                ServiceFacade.CourseSvc.SaveCourse(model);
                return Json(new { error = "", courseid = model.Cid }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = "保存课件失败，" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Uploady(HttpPostedFileBase file)
        {
            if (file == null || string.IsNullOrEmpty(file.FileName) || file.ContentLength < 1)
            {
                return this.HttpNotFound();
            }
            try
            {
                Guid key = Guid.NewGuid();
                FileInfo fi = new FileInfo(file.FileName);
                var newfile = key.ToString() + fi.Extension;
                file.SaveAs(Path.Combine(AppConfig.UploadRoot, newfile));

                return Json(new { error = "", file =  newfile}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Converty(string file)
        {
            int mediatype = 0;
            string fullPath = Path.Combine(AppConfig.UploadRoot, file);
            FileInfo fi = new FileInfo(fullPath);
            List<string> output = new List<string>();
            if (fi.Exists)
            {
                if (".ppt".Equals(fi.Extension, StringComparison.OrdinalIgnoreCase))
                {
                    output = PptConverter.PPT(fi.FullName, ImageFormat.Jpeg);
                }
                else if (".pptx".Equals(fi.Extension, StringComparison.OrdinalIgnoreCase))
                {
                    output = PptConverter.PPTX(fi.FullName, ImageFormat.Jpeg, 0);
                }
                else if (".mp4".Equals(fi.Extension, StringComparison.OrdinalIgnoreCase))
                {
                    VideoConverter.Convert(fi.FullName);
                    mediatype = 1;
                }
                return Json(new { error = "", type = mediatype, count = output.Count }, JsonRequestBehavior.AllowGet);
            }
            else 
            {
                return Json(new { error = "未找到源文件!" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
