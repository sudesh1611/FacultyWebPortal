﻿using System;
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
    public class StudentDashboardModel : PageModel
    {
        public Profile CurrentProfile { set; get; }

        private readonly ProfileDbContext profileDbContext;

        public StudentDashboardModel(ProfileDbContext pdp)
        {
            profileDbContext = pdp;
            CurrentProfile = new Profile();
        }

        public async Task OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
        }
    }
}