using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Data;
using Faculty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Faculty.Pages
{
    public class AddNewResourceModel : PageModel
    {
        private readonly CourseResourceDbContext courseResourceDbContext;
        private readonly CourseDbContext courseDbContext;
        private readonly ProfileDbContext profileDbContext;
        public Profile CurrentProfile { set; get; }
        public List<Course> CoursesList { get; set; }

        [BindProperty]
        public CourseResource newResource { set; get; }

        public AddNewResourceModel(CourseResourceDbContext adb, CourseDbContext cdb, ProfileDbContext pdb)
        {
            courseResourceDbContext = adb;
            courseDbContext = cdb;

            CoursesList = new List<Course>();
            newResource = new CourseResource();

            profileDbContext = pdb;
            CurrentProfile = new Profile();
        }

        public async Task OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            if (User.Identity.IsAuthenticated)
            {

                CoursesList = await courseDbContext.Courses.ToListAsync();
                if (CoursesList != null)
                {
                    CoursesList.Reverse();
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    CourseResource addResource = new CourseResource();
                    addResource.ResourceTitle = newResource.ResourceTitle;
                    addResource.CourseID = newResource.CourseID;
                    var course = await courseDbContext.Courses.SingleOrDefaultAsync(m => m.ID == addResource.CourseID);
                    addResource.CourseName = course.CourseCode + " - " + course.CourseSemester + " " + course.CourseYear;
                    addResource.ResourceLink = String.Empty;
                    await courseResourceDbContext.Resources.AddAsync(addResource);
                    await courseResourceDbContext.SaveChangesAsync();
                    return RedirectToPage("/UploadResourceAttachmentPage");
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