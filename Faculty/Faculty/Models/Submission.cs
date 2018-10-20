using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Models
{
    public class Submission
    {
        public int ID { get; set; }

        public int AssignmentID { get; set; }

        public string DateTime { get; set; }

        public string SubmissionLink { get; set; }
    }
}
