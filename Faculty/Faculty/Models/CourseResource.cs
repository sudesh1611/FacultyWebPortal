using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Models
{
    public class CourseResource
    {
        public int ID { get; set; }

        public int CourseID { get; set; }

        public string Date { get; set; }

        public string ResourceTitle { get; set; }

        public string ResourceLink { get; set; }
    }
}
