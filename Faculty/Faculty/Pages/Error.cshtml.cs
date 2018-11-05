using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Data;
using Faculty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Faculty.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        private readonly ProfileDbContext profileDbContext;
        public Profile CurrentProfile { set; get; }
        public bool AccountPresent { get; set; }
        public ErrorModel(ProfileDbContext pdb)
        {
            profileDbContext = pdb;
            CurrentProfile = new Profile();
            AccountPresent = true;
        }

        public async Task OnGetAsync()
        {
            AccountPresent = true;
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            if(CurrentProfile==null)
            {
                AccountPresent = false;
            }
        }
    }
}
