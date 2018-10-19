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
using Newtonsoft.Json;

namespace Faculty.Pages
{
    public class AddNewCourseModel : PageModel
    {
        private readonly CourseDbContext courseDbContext;
        private readonly ProfileDbContext profileDbContext;

        [BindProperty]
        public Course course { get; set; }

        public AddNewCourseModel(CourseDbContext phd, ProfileDbContext prdb)
        {
            courseDbContext = phd;
            profileDbContext = prdb;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Error");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                    var supervisor = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.LoginEmailID == emaiID);
                    course.SupervisorID = supervisor.ID;
                    var tempString = course.TeachingAssistants.Split(',').ToList();
                    course.TeachingAssistants = JsonConvert.SerializeObject(tempString);
                    await courseDbContext.Courses.AddAsync(course);
                    await courseDbContext.SaveChangesAsync();
                    return RedirectToPage("/EditCoursesPage");
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