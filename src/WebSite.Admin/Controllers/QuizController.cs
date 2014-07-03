using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Facade;
using View.Model;

namespace WebSite.Admin.Controllers
{
    public class QuizController : Controller
    {
        //
        // GET: /Quiz/List

        public ActionResult List(int? course_id)
        {
            if (course_id.HasValue)
            {
                var quiz = ServiceFacade.CourseSvc.LoadQuiz(course_id.Value);

                quiz = quiz ?? new List<QuizModel>();
                return Json(new { total = quiz.Count(), rows = quiz.ToArray() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { total = 0, rows = new QuizModel[] { } }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Save(QuizModel model)
        {
            try
            {
                ServiceFacade.CourseSvc.SaveQuiz(model);
                return Json(new { error = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = "保存答题失败！" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int quizID)
        {
            try
            {
                ServiceFacade.CourseSvc.DelQuiz(new QuizModel { QuizID = quizID });
                return Json(new { error = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = "删除答题失败！" + ex.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { error = "" }, JsonRequestBehavior.AllowGet);
        }
    }
}
