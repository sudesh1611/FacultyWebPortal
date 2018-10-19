using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Models
{
    public class Course
    {
        public int ID { get; set; }

        public int SupervisorID { get; set; }

        public string CourseCode { get; set; }

        public string CourseTitle { get; set; }

        public string CourseSemester { get; set; }

        public string CourseYear { get; set; }

        public string Instructor { get; set; }

        public string CourseDescription { get; set; }

        public string TeachingAssistants { get; set; }

        public string LecturesTiming { get; set; }
    }
}
