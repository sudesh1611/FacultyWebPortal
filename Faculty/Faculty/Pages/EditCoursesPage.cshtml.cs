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

namespace Faculty.Pages
{
    public class EditCoursesPageModel : PageModel
    {
        private readonly ProfileDbContext profileDbContext;
        private readonly CourseDbContext courseDbContext;
        public Profile CurrentProfile { set; get; }

        public List<Course> CoursesList { get; set; }

        public EditCoursesPageModel(ProfileDbContext pdc,CourseDbContext cdc)
        {
            profileDbContext = pdc;
            courseDbContext = cdc;
            CurrentProfile = new Profile();
        }

        public async Task OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                var Supervisor = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.LoginEmailID == emaiID);
                CoursesList = new List<Course>();
                var tempList = await courseDbContext.Courses.ToListAsync();
                foreach (var item in tempList)
                {
                    if(item.SupervisorID==Supervisor.ID)
                    {
                        CoursesList.Add(item);
                    }
                }
                CoursesList.Reverse();
            }
        }
    }
}