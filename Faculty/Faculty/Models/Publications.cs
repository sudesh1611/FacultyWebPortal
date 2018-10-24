using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Models
{
    public class Publications
    {
        public int ID { get; set; }

        public string PublicationTitle { get; set; }

        public string PublicationType { get; set; }

        public string PublicationLink { get; set; }

        public string PublicationYear { get; set; }
    }
}
