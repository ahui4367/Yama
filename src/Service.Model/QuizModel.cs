﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Model
{
    public class QuizModel
    {
        public int QuizID { get; set; }

        public string Tag { get; set; }

        public string Question { get; set; }

        public string Option1 { get; set; }

        public string Option2 { get; set; }

        public string Option3 { get; set; }

        public string Option4 { get; set; }

        public int QuizType { get; set; }

        public string QuizTypeName { get; set; }

        public int Seq { get; set; }

        public int Answer { get; set; }

        public string DisplayAnswer 
        { 
            get 
            {
                string result = string.Empty;
                switch (Answer)
                {
                    case 1:
                        result = "A";
                        break;
                    case 2:
                        result = "B";
                        break;
                    case 3:
                        result = "C";
                        break;
                    case 4:
                        result = "D";
                        break;
                }
                return result;
            } 
        }

        public string Created { get; set; }

        public string LastModified { get; set; }
    }
}
