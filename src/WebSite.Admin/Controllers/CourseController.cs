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
        public ActionResult Edit(int? id)
        {
            CourseModel model = null;
            if (id.HasValue && id.Value > 0)
            {
                model = ServiceFacade.CourseSvc.LoadCourse(id.Value);
                if (model != null)
                {
                    model.Type = model.Type.Trim();
                }
            }
            else
            {
                model = new CourseModel { Cid = -1, CourseName = "新课件", Limit = 60, Type = "ppt文档" };
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

    }
}
