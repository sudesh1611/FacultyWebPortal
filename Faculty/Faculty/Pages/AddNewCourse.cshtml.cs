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
        public Profile CurrentProfile { set; get; }

        [BindProperty]
        public Course course { get; set; }

        public AddNewCourseModel(CourseDbContext phd, ProfileDbContext prdb)
        {
            courseDbContext = phd;
            profileDbContext = prdb;
            CurrentProfile = new Profile();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            if (!User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                return RedirectToPage("/Error");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                List<int> StudentsApproved = new List<int>();
                List<int> StudentsApplied = new List<int>();
                List<int> StudentsDeclined = new List<int>();
                if (ModelState.IsValid)
                {
                    string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                    var supervisor = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.LoginEmailID == emaiID);
                    course.SupervisorID = supervisor.ID;
                    List<string> tempString = new List<string>();
                    if(!String.IsNullOrEmpty(course.TeachingAssistants))
                    {
                        tempString = course.TeachingAssistants.Split(';').ToList();
                    }
                    course.TeachingAssistants = JsonConvert.SerializeObject(tempString);
                    course.StudentsApproved = JsonConvert.SerializeObject(StudentsApproved);
                    course.StudentRegistered = JsonConvert.SerializeObject(StudentsApplied);
                    course.StudentDeclined = JsonConvert.SerializeObject(StudentsDeclined);
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