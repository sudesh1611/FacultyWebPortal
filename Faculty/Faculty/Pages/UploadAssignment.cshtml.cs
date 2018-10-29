using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Faculty.Data;
using Faculty.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Faculty.Pages
{
    public class UploadAssignmentModel : PageModel
    {
        private readonly SubmissionDbContext submissionDbContext;
        private readonly AssignmentDbContext assignmentDbContext;
        private readonly StudentDbContext studentDbContext;

        private Assignment GlobalAssignment { get; set; }

        public UploadAssignmentModel(SubmissionDbContext sdb,AssignmentDbContext asb, IOptions<Assignment> globalAssignment,StudentDbContext sdbb)
        {
            submissionDbContext = sdb;
            assignmentDbContext = asb;
            studentDbContext = sdbb;
            GlobalAssignment = globalAssignment.Value;
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("Student"))
            {
                if (file == null || file.Length == 0)
                    return Content("file not selected");
                int ID = GlobalAssignment.ID;
                if (ID < 1)
                {
                    return RedirectToPage("/Error");
                }

                var assignment = assignmentDbContext.Assignments.SingleOrDefault(m => m.ID == ID);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", assignment.SubmissionDirectoryLink, file.FileName);

                if (System.IO.File.Exists(path))
                {
                    //ToDo Make A new Page
                    return Content("File name already exists. Change the file name and try again!!");
                }

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var now = DateTime.Now;
                string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                var Student = await studentDbContext.Students.SingleOrDefaultAsync(m => m.EmailID == emaiID);
                Submission newSubmission = new Submission()
                {
                    SubmissionLink = Path.Combine(assignment.SubmissionDirectoryLink, file.FileName),
                    AssignmentID = ID,
                    DateTime = now.ToShortTimeString() + " - " + now.ToShortDateString(),
                    SubmittedBy=Student.FullName,
                    RollNumber=Student.RollNumber
                };
                await submissionDbContext.Submissions.AddAsync(newSubmission);
                await submissionDbContext.SaveChangesAsync();
                return RedirectToPage("/SubmissionSuccessful");
            }
            else
            {
                return RedirectToPage("/Login");
            }
        }
    }
}