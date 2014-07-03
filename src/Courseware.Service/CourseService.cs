using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.Model;

namespace Courseware.Service
{
    public abstract class CourseService
    {
        public abstract IEnumerable<CourseModel> LoadCourses();

        public abstract IEnumerable<QuizModel> LoadQuiz(int course_id);

        public abstract void SaveProgress(int user_id, CourseModel model);

        public abstract void SaveCourse(CourseModel model);

        public abstract void DelCourse(CourseModel model);

        public abstract void SaveQuiz(QuizModel model);

        public abstract void DelQuiz(QuizModel model);
    }
}
