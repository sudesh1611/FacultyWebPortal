using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Models
{
    public class Notice
    {
        public int ID { get; set; }

        [Required]
        public string NoticeShortTitle { get; set; }

        [Required]
        public string NoticeDescription { get; set; }

        public string NoticeDate { get; set; }
    }
}
