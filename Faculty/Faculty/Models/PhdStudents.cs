using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Models
{
    public class PhdStudents
    {
        public int ID { get; set; }

        public int SupervisorID { get; set; }

        public string StudentName { get; set; }

        public string ThesisTitle { get; set; }

        public string DegreeStatus { get; set; }

        public string DegreeCompletion { get; set; }

        public string College { get; set; }
    }
}
