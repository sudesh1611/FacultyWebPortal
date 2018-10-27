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
    public class AddNewPublicationModel : PageModel
    {
        private readonly PublicationDbContext phdStudentsDbContext;
        private readonly ProfileDbContext profileDbContext;
        public Profile CurrentProfile { set; get; }

        [BindProperty]
        public Publications Publication { get; set; }

        public AddNewPublicationModel(PublicationDbContext phd, ProfileDbContext prdb)
        {
            phdStudentsDbContext = phd;
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
                if (ModelState.IsValid)
                {
                    await phdStudentsDbContext.Publications.AddAsync(Publication);
                    await phdStudentsDbContext.SaveChangesAsync();
                    return RedirectToPage("/EditPublicationsPage");
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