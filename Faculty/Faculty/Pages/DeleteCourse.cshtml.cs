using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Data;
using Faculty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Faculty.Pages
{
    public class DeleteCourseModel : PageModel
    {
        private readonly CourseDbContext courseDbContext;
        private readonly ProfileDbContext profileDbContext;
        public Profile CurrentProfile { set; get; }

        [BindProperty]
        public Course course { get; set; }

        [TempData]
        public int ID { set; get; }

        public DeleteCourseModel(CourseDbContext phd, ProfileDbContext pDb)
        {
            courseDbContext = phd;
            course = new Course();
            profileDbContext = pDb;
            CurrentProfile = new Profile();
        }

        public async Task OnGetAsync(int? id)
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            ID = (int)id;
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                course = await courseDbContext.Courses.SingleOrDefaultAsync(m => m.ID == id);
                if (!String.IsNullOrEmpty(course.TeachingAssistants))
                {
                    var tempList = new List<string>();
                    tempList = JsonConvert.DeserializeObject<List<string>>(course.TeachingAssistants);
                    course.TeachingAssistants = String.Empty;
                    if (tempList.Count > 0)
                    {
                        for (int i = 0; i <= tempList.Count - 2; i++)
                        {
                            course.TeachingAssistants = course.TeachingAssistants + tempList[i] + ";";
                        }
                        course.TeachingAssistants = course.TeachingAssistants + tempList[tempList.Count - 1];
                    }
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                if (ModelState.IsValid)
                {
                    if (ID == 0)
                    {
                        ID = course.ID;
                    }
                    var tempUser = await courseDbContext.Courses.SingleOrDefaultAsync(m => m.ID == ID);
                    courseDbContext.Courses.Remove(tempUser);
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