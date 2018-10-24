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
    public class UploadResourceAttachmentPageModel : PageModel
    {
        private readonly CourseResourceDbContext courseResourceDbContext;
        private readonly ProfileDbContext profileDbContext;
        public CourseResource CurrentResource { set; get; }
        public Profile CurrentProfile { set; get; }

        public UploadResourceAttachmentPageModel(CourseResourceDbContext adb, ProfileDbContext pDbContext)
        {
            courseResourceDbContext = adb;
            CurrentResource = new CourseResource();
            CurrentResource = adb.Resources.Last();
            profileDbContext = pDbContext;
            CurrentProfile = new Profile();
        }

        public async Task OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            if (User.Identity.IsAuthenticated)
            {
                CurrentResource = await courseResourceDbContext.Resources.LastOrDefaultAsync();
                var newResource = CurrentResource;
                if (CurrentResource != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Resources", "Resource" + CurrentResource.ID);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    newResource.SubmissionDirectoryLink = Path.Combine("Resources", "Resource" + CurrentResource.ID);
                    courseResourceDbContext.Resources.Update(newResource);
                    await courseResourceDbContext.SaveChangesAsync();
                }
            }
        }
    }
}