using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluorineFx;

namespace WebSite.ShareObject
{
    [TransferObject]
    public class CourseModel
    {
        public int CourseID { get; set; }

        public string CourseName { get; set; }

        public int CourseLimit { get; set; }

        public string Category { get; set; }

        public string MediaType { get; set; }

        public string Media { get; set; }

        public int Count { get; set; }

        public QuestionModel[] Questions { get; set; }
    }

    [TransferObject]
    public class QuestionModel
    {
        public int QuestionID { get; set; }

        public string QuestionTitle { get; set; }

        public int QuestionAnswer { get; set; }

        public int QuestionType { get; set; }

        public string[] QuestionOptions { get; set; }

        public int PageIndex { get; set; }

        public string QuestionMyAnswer { get; set; }
    }
}