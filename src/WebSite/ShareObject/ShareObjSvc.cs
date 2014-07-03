using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluorineFx;
using Service.Facade;
using View.Model;
using WebSite.ShareObject;

namespace WebSite.ShareObject
{
    [RemotingService]
    public class ShareObjSvc
    {
        public ShareObjSvc()
        {
        }

        public CourseModel[] LoadCourses()
        {
            List<CourseModel> qs = new List<CourseModel>();
            var list = ServiceFacade.CourseSvc.LoadCourses();
            if (list != null && list.Count() > 0)
            {
                qs = list.Select(d => new CourseModel
                { 
                    CourseID = d.Cid,
                    CourseName = d.CourseName,
                    Category = d.Category,
                    MediaType = d.Type,
                    Media = d.Media,
                    Count = d.Rank,
                    CourseLimit = d.Limit
                }).ToList();
            }

            return qs.ToArray();
        }

        public QuestionModel[] LoadQuestionsById(int course_id)
        {
            List<QuestionModel> result = new List<QuestionModel>();

            var list = ServiceFacade.CourseSvc.LoadQuiz(course_id);
            if (list != null && list.Count() > 0)
            {
                result = list.OrderBy(d=>d.PageNo).OrderBy(d=>d.Seq).Select(d => new QuestionModel 
                { 
                    QuestionID = d.QuizID,
                    QuestionTitle = d.Question,
                    QuestionAnswer = d.Answer,
                    QuestionOptions = new string[] { d.Option1, d.Option2, d.Option3, d.Option4 },
                    QuestionType = d.QuizType,
                    PageIndex = d.PageNo,
                }).ToList();
            }

            return result.ToArray();
        }

        public int SignIn(string usr, string pass)
        {
            var model = ServiceFacade.UserSvc.Login(new LoginModel { UserCode = usr, Password = pass });
            if (model == null || model.Uid < 1)
            {
                return -1;
            }
            return 0;
        }

        public int SaveScore(int user_id, int course_id, string progress)
        {
            int result = -1;

            if (!string.IsNullOrEmpty(progress))
            {
                result = 0;
            }

            return result;
        }
    }
}