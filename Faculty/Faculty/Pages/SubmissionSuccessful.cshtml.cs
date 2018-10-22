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
    public class SubmissionSuccessfulModel : PageModel
    {
        private readonly AssignmentDbContext assignmentDbContext;
        private readonly CourseDbContext courseDbContext;

        public Course CurrentCourse { get; set; }
        private Assignment GlobalAssignment { get; set; }

        public SubmissionSuccessfulModel(AssignmentDbContext adb, CourseDbContext cdb, IOptions<Assignment> globalAssignment)
        {
            assignmentDbContext = adb;
            courseDbContext = cdb;
            CurrentCourse = new Course();
            GlobalAssignment = globalAssignment.Value;
        }
        
        public async Task OnGetAsync()
        {
            int ID = GlobalAssignment.ID;
            Assignment assignment = await assignmentDbContext.Assignments.SingleOrDefaultAsync(m => m.ID == ID);
            if(assignment == null)
            {
                RedirectToPage("/Index");
            }
            else
            {
                CurrentCourse = await courseDbContext.Courses.SingleOrDefaultAsync(m => m.ID == assignment.CourseID);
            }
        }
    }
}