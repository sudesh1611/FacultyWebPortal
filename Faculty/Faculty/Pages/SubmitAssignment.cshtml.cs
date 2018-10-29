using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Faculty.Data;
using Faculty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Faculty.Pages
{
    public class SubmitAssignmentModel : PageModel
    {
        private readonly SubmissionDbContext submissionDbContext;
        private readonly AssignmentDbContext assignmentDbContext;
        private readonly CourseDbContext courseDbContext;
        private readonly StudentDbContext studentDbContext;

        public Assignment CurrentAssignment { set; get; }
        public Course CurrentCourse { get; set; }

        public bool CanUpload { get; set; }

        private Assignment GlobalAssignment { get; set; }

        public SubmitAssignmentModel(SubmissionDbContext sdb,AssignmentDbContext adb,CourseDbContext cdb, IOptions<Assignment> globalAssignment,StudentDbContext sdbb)
        {
            submissionDbContext = sdb;
            assignmentDbContext = adb;
            courseDbContext = cdb;
            studentDbContext = sdbb;
            CurrentCourse = new Course();
            CurrentAssignment = new Assignment();
            GlobalAssignment = globalAssignment.Value;
            CanUpload = false;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            GlobalAssignment.ID = (int)id;
            var AssignmentID = GlobalAssignment.ID;
            CurrentAssignment = await assignmentDbContext.Assignments.SingleOrDefaultAsync(m => m.ID == AssignmentID);
            if(CurrentAssignment==null)
            {
                return RedirectToPage("/Error");
            }
            else
            {
                CurrentCourse = await courseDbContext.Courses.SingleOrDefaultAsync(m => m.ID == CurrentAssignment.CourseID);
                if(CurrentCourse==null)
                {
                    return RedirectToPage("/Error");
                }
                if(User.Identity.IsAuthenticated && User.IsInRole("Student"))
                {
                    string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                    var Student = await studentDbContext.Students.SingleOrDefaultAsync(m => m.EmailID == emaiID);
                    try
                    {
                        var StudentApproved = JsonConvert.DeserializeObject<List<int>>(CurrentCourse.StudentsApproved);
                        if (StudentApproved.Any())
                        {
                            foreach (var StudentID in StudentApproved)
                            {
                                if (StudentID == Student.ID)
                                {
                                    CanUpload = true;
                                    return Page();
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return Page();
                    }
                }
                return Page();
            }
        }
    }
}