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
    public class AdminDashboardModel : PageModel
    {
        private readonly ProfileDbContext profileDbContext;
        public Profile CurrentProfile { set; get; }

        public AdminDashboardModel(ProfileDbContext pdb)
        {
            profileDbContext = pdb;
            CurrentProfile = new Profile();
        }

        public async Task OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
        }
    }
}