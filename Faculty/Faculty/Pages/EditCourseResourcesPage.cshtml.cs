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
    public class EditCourseResourcesPageModel : PageModel
    {
        private readonly CourseResourceDbContext courseResourceDbContext;
        private readonly ProfileDbContext profileDbContext;
        public Profile CurrentProfile { set; get; }
        public List<CourseResource> ResourcesList { set; get; }

        public EditCourseResourcesPageModel(CourseResourceDbContext cdb, ProfileDbContext pdb)
        {
            courseResourceDbContext = cdb;
            profileDbContext = pdb;
            CurrentProfile = new Profile();
            ResourcesList = new List<CourseResource>();
        }

        public async Task OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            if (User.Identity.IsAuthenticated)
            {
                ResourcesList = await courseResourceDbContext.Resources.ToListAsync();
                if(ResourcesList!=null)
                {
                    ResourcesList.Reverse();
                }
            }
        }
    }
}