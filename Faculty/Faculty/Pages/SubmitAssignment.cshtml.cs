using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Faculty.Pages
{
    public class SubmitAssignmentModel : PageModel
    {
        private readonly SubmissionDbContext submissionDbContext;

        [TempData]
        public int CourseID { get; set; }

        public SubmitAssignmentModel(SubmissionDbContext sdb)
        {
            submissionDbContext = sdb;
        }

        public async Task OnGetAsync(int? id)
        {
            CourseID = (int)id;
        }
    }
}