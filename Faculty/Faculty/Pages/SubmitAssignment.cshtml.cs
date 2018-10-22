using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Data;
using Faculty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Faculty.Pages
{
    public class SubmitAssignmentModel : PageModel
    {
        private readonly SubmissionDbContext submissionDbContext;
        private readonly AssignmentDbContext assignmentDbContext;
        private readonly CourseDbContext courseDbContext;
        public Assignment CurrentAssignment { set; get; }
        public Course CurrentCourse { get; set; }

        private Assignment GlobalAssignment { get; set; }

        public SubmitAssignmentModel(SubmissionDbContext sdb,AssignmentDbContext adb,CourseDbContext cdb, IOptions<Assignment> globalAssignment)
        {
            submissionDbContext = sdb;
            assignmentDbContext = adb;
            courseDbContext = cdb;
            CurrentCourse = new Course();
            CurrentAssignment = new Assignment();
            GlobalAssignment = globalAssignment.Value;
        }

        public async Task OnGetAsync(int? id)
        {
            GlobalAssignment.ID = (int)id;
            var AssignmentID = GlobalAssignment.ID;
            CurrentAssignment = await assignmentDbContext.Assignments.SingleOrDefaultAsync(m => m.ID == AssignmentID);
            if(CurrentAssignment==null)
            {
                RedirectToPage("/Error");
            }
            else
            {
                CurrentCourse = await courseDbContext.Courses.SingleOrDefaultAsync(m => m.ID == CurrentAssignment.CourseID);
                if(CurrentCourse==null)
                {
                    RedirectToPage("/Error");
                }
            }
        }
    }
}