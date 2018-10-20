using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Data;
using Faculty.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Faculty.Pages
{
    public class UploadAssignmentModel : PageModel
    {
        private readonly SubmissionDbContext submissionDbContext;
        private readonly AssignmentDbContext assignmentDbContext;

        [TempData]
        public int CourseID { get; set; }

        public UploadAssignmentModel(SubmissionDbContext sdb,AssignmentDbContext asb)
        {
            submissionDbContext = sdb;
            assignmentDbContext = asb;
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");
            int ID = CourseID;
            if(ID<1)
            {
                return RedirectToPage("/Error");
            }

            var assignment = assignmentDbContext.Assignments.SingleOrDefault(m => m.ID == ID);
            var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot",assignment.SubmissionDirectoryLink,file.FileName);

            if(System.IO.File.Exists(path))
            {
                return Content("File already exists. Change the file name");
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var now = DateTime.Now;
            Submission newSubmission = new Submission()
            {
                SubmissionLink = Path.Combine(assignment.SubmissionDirectoryLink, file.FileName),
                AssignmentID = ID,
                DateTime = now.TimeOfDay.ToString() + " - " + now.ToShortDateString()
            };
            await submissionDbContext.Submissions.AddAsync(newSubmission);
            await submissionDbContext.SaveChangesAsync();
            return RedirectToPage("/UploadAssignmentAttachmentPage");
        }
    }
}