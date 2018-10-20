using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Data;
using Faculty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Faculty.Pages
{
    
    public class AddNewAssignmentModel : PageModel
    {
        private readonly AssignmentDbContext assignmentDbContext;
        private readonly CourseDbContext courseDbContext;
        public List<Course> CoursesList { get; set; }

        [BindProperty]
        public Assignment newAssignment { set; get; }

        public AddNewAssignmentModel(AssignmentDbContext adb,CourseDbContext cdb)
        {
            assignmentDbContext = adb;
            courseDbContext = cdb;
            CoursesList = new List<Course>();
            newAssignment = new Assignment();
        }

        public async Task OnGetAsync()
        {
            if(User.Identity.IsAuthenticated)
            {
                
                CoursesList = await courseDbContext.Courses.ToListAsync();
                if(CoursesList!=null)
                {
                    CoursesList.Reverse();
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(User.Identity.IsAuthenticated)
            {
                if(ModelState.IsValid)
                {
                    Assignment addAssignment = new Assignment();
                    addAssignment.AssignmentDescription = newAssignment.AssignmentDescription;
                    addAssignment.AssignmentTitle = newAssignment.AssignmentTitle;
                    addAssignment.CourseID = newAssignment.CourseID;
                    var course = await courseDbContext.Courses.SingleOrDefaultAsync(m => m.ID == addAssignment.CourseID);
                    addAssignment.CourseName = course.CourseCode + " - " + course.CourseSemester + " " + course.CourseYear;
                    addAssignment.DeadlineDay = newAssignment.DeadlineDay;
                    addAssignment.DeadLineMonth = newAssignment.DeadLineMonth;
                    addAssignment.DeadlineTime = newAssignment.DeadlineTime + ":00:00";
                    addAssignment.DeadlineYear = newAssignment.DeadlineYear;
                    addAssignment.AttachmentFulLink = String.Empty;
                    addAssignment.SubmissionDirectoryLink = String.Empty;
                    await assignmentDbContext.Assignments.AddAsync(addAssignment);
                    await assignmentDbContext.SaveChangesAsync();
                    return RedirectToPage("/UploadAssignmentAttachmentPage");
                }
                else
                {
                    return Page();
                }
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }
    }
}