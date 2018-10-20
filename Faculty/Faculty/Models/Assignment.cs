using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Models
{
    public class Assignment
    {
        public int ID { get; set; }

        public int CourseID { get; set; }

        public string CourseName { get; set; }

        public string AssignmentTitle { get; set; }

        public string AssignmentDescription { get; set; }

        public int DeadlineDay { get; set; }

        public int DeadLineMonth { get; set; }

        public int DeadlineYear { get; set; }

        public string DeadlineTime { get; set; }

        public string AttachmentFulLink { get; set; }

        public string SubmissionDirectoryLink { get; set; }
    }
}
