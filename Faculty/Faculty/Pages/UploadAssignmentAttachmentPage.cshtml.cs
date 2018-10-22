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
    public class UploadAssignmentAttachmentPageModel : PageModel
    {
        private readonly AssignmentDbContext assignmentDbContext;
        private readonly ProfileDbContext profileDbContext;
        public Assignment CurrentAssignment { set; get; }
        public Profile CurrentProfile { set; get; }

        public UploadAssignmentAttachmentPageModel(AssignmentDbContext adb, ProfileDbContext pDbContext)
        {
            assignmentDbContext = adb;
            CurrentAssignment = new Assignment();
            CurrentAssignment = adb.Assignments.Last();
            profileDbContext = pDbContext;
            CurrentProfile = new Profile();
        }


        public async Task OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            if (User.Identity.IsAuthenticated)
            {
                CurrentAssignment = await assignmentDbContext.Assignments.LastOrDefaultAsync();
                var newAssignment = CurrentAssignment;
                if(CurrentAssignment!=null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Assignments", "Assignment" + CurrentAssignment.ID);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    newAssignment.SubmissionDirectoryLink = Path.Combine("Assignments", "Assignment" + CurrentAssignment.ID);
                    assignmentDbContext.Assignments.Update(newAssignment);
                    await assignmentDbContext.SaveChangesAsync();
                }
            }
        }
    }
}