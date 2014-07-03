using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Access;
using DbModel.AspnetDb;
using View.Model;
using YesHJ.Fx.Pattern;

namespace Courseware.Service.Impl
{
    public class CourseServiceImpl : CourseService
    {
        public RepositoryFactory DbFactory
        {
            get
            {
                return ServiceLocator.Current.Find<RepositoryFactory>();
            }
        }

        public override IEnumerable<CourseModel> LoadCourses()
        {
            IEnumerable<CourseModel> result = new List<CourseModel>();
            using (var repo = DbFactory.Create<Course_T>())
            {
                result = repo.GetAll().Select(c => new CourseModel 
                {
                    Cid = c.Cid,
                    CourseName = c.Cname,
                    Limit = c.Limit,
                    Type = c.Type,
                    Category = c.Category,
                    Rank = c.Rank,
                    Media = c.Media,
                    Created = c.Created.ToString("yyyy-MM-dd"),
                    Lastmodified = c.Lastmodified.ToString("yyyy-MM-dd"),
                }).ToList();
            }

            return result;
        }

        public override IEnumerable<QuizModel> LoadQuiz(int course_id)
        {
            IEnumerable<QuizModel> result = new List<QuizModel>();

            using (var repo = DbFactory.Create<Quiz_T>())
            {
                result = repo.GetFiltered("cid=" + course_id)
                    .Select(q => new QuizModel 
                    { 
                        QuizID = q.Qid,
                        CourseID = q.Cid,
                        Question = q.Quiz,
                        Option1 = q.Op1,
                        Option2 = q.Op2,
                        Option3 = q.Op3,
                        Option4 = q.Op4,
                        PageNo = q.Pageno,
                        QuizType = q.Type,
                        Seq = q.Seq,
                        Answer = q.Answer,
                        Created = q.Created,
                        LastModified = q.Lastmodified,
                    })
                    .ToList();
            }

            return result;
        }

        public override void SaveProgress(int user_id, CourseModel model)
        {
        }

        public override void SaveCourse(CourseModel model)
        {
            using (var repo = DbFactory.Create<Course_T>())
            {
                if (model.Cid > 0)
                {
                    repo.Save(new Course_T 
                    {
                        Cid = model.Cid,
                        Cname = model.CourseName,
                        Category = model.Category,
                        Limit = model.Limit,
                        Media = model.Media,
                        Rank = model.Rank,
                        Type = model.Type,
                    });
                }
                else
                {
                    int id = repo.Add(new Course_T 
                    {
                        Cname = model.CourseName,
                        Category = model.Category,
                        Limit = model.Limit,
                        Media = model.Media,
                        Rank = model.Rank,
                        Type = model.Type,
                    });
                    model.Cid = id;
                }
            }
        }

        public override void DelCourse(CourseModel model)
        {
            using (var repo = DbFactory.Create<Course_T>())
            {
                repo.Remove(new Course_T { Cid = model.Cid });
            }
        }

        public override void SaveQuiz(QuizModel model)
        {
            using (var repo = DbFactory.Create<Quiz_T>())
            {
                if (model.QuizID > 0)
                {
                    repo.Save(new Quiz_T 
                    {
                        Qid = model.QuizID,
                        Quiz = model.Question,
                        Pageno = model.PageNo,
                        Type = model.QuizType,
                        Op1 = model.Option1,
                        Op2 = model.Option2,
                        Op3 = model.Option3,
                        Op4 = model.Option4,
                        Answer = model.Answer,
                    });
                }
                else
                {
                    repo.Add(new Quiz_T
                    {
                        Quiz = model.Question,
                        Pageno = model.PageNo,
                        Type = model.QuizType,
                        Op1 = model.Option1,
                        Op2 = model.Option2,
                        Op3 = model.Option3,
                        Op4 = model.Option4,
                        Answer = model.Answer,
                    });
                }
            }
        }

        public override void DelQuiz(QuizModel model)
        {
            using (var repo = DbFactory.Create<Quiz_T>())
            {
                repo.Remove(new Quiz_T { Qid = model.QuizID });
            }
        }

        public override CourseModel LoadCourse(int course_id)
        {
            using (var repo = DbFactory.Create<Course_T>())
            {
                var item = repo.GetFiltered("cid=" + course_id).FirstOrDefault();
                if (item != null && item.Cid > 0)
                {
                    return new CourseModel
                    {
                        Cid = item.Cid,
                        CourseName = item.Cname,
                        Category = item.Category,
                        Limit = item.Limit,
                        Media = item.Media,
                        Type = item.Type,
                        Rank = item.Rank,
                        Created = item.Created.ToString("yyyy-MM-dd"),
                        Lastmodified = item.Lastmodified.ToString("yyyy-MM-dd")
                    };
                }
                else
                {
                    return new CourseModel();
                }
            }
        }
    }
}
