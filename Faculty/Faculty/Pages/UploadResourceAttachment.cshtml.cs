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
    public class UploadResourceAttachmentModel : PageModel
    {
        private readonly CourseResourceDbContext courseResourceDbContext;

        public UploadResourceAttachmentModel(CourseResourceDbContext cdb)
        {
            courseResourceDbContext = cdb;
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            var resource = courseResourceDbContext.Resources.Last();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", resource.SubmissionDirectoryLink, "resource" + file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            resource.ResourceLink = Path.Combine(resource.SubmissionDirectoryLink, "resource" + file.FileName);
            courseResourceDbContext.Resources.Update(resource);
            await courseResourceDbContext.SaveChangesAsync();
            return RedirectToPage("/EditCourseResourcesPage");
        }
    }
}