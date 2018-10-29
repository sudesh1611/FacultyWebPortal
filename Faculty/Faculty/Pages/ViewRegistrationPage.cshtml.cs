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
    public class ViewRegistrationPageModel : PageModel
    {
        private readonly CourseDbContext courseDbContext;
        private readonly ProfileDbContext profileDbContext;
        public Profile CurrentProfile { set; get; }
        public List<Course> CoursesList { set; get; }

        public ViewRegistrationPageModel(CourseDbContext cdb, ProfileDbContext pdb, StudentDbContext sdb)
        {
            courseDbContext = cdb;
            profileDbContext = pdb;
        }

        public async Task OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                CoursesList = await courseDbContext.Courses.ToListAsync();
                if(CoursesList!=null)
                {
                    CoursesList.OrderByDescending(m => m.CourseYear);
                }
            }
        }
    }
}