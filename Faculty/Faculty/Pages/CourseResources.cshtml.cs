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
    public class CourseResourcesModel : PageModel
    {
        private readonly CourseResourceDbContext courseResourceDbContext;
        private readonly CourseDbContext courseDbContext;

        public List<CourseResource> CourseResources { get; set; }
        public Course CurrentCourse { get; set; }

        public CourseResourcesModel(CourseResourceDbContext adb, CourseDbContext cdb)
        {
            courseResourceDbContext = adb;
            courseDbContext = cdb;
            CourseResources = new List<CourseResource>();
            CurrentCourse = new Course();
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            int ID = (int)id;
            CurrentCourse = await courseDbContext.Courses.SingleOrDefaultAsync(m => m.ID == ID);
            if (CurrentCourse != null)
            {
                var tempList = await courseResourceDbContext.Resources.ToListAsync();
                foreach (var resource in tempList)
                {
                    if (resource.CourseID == ID)
                    {
                        CourseResources.Add(resource);
                    }
                }
                return Page();
            }
            else
            {
                return RedirectToPage("/NotFound");
            }
        }
    }
}