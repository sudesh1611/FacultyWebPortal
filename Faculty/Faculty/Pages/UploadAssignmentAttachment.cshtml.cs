using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Faculty.Pages
{
    public class UploadAssignmentAttachmentModel : PageModel
    {
        private readonly AssignmentDbContext assignmentDbContext;

        public UploadAssignmentAttachmentModel(AssignmentDbContext adb)
        {
            assignmentDbContext = adb;
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var assignment = assignmentDbContext.Assignments.Last();
            var path = Path.Combine(assignment.SubmissionDirectoryLink, "attachment"+file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            assignment.AttachmentFulLink = path;
            assignmentDbContext.Assignments.Update(assignment);
            await assignmentDbContext.SaveChangesAsync();
            return RedirectToPage("/UploadAssignmentAttachmentPage");
        }
    }
}