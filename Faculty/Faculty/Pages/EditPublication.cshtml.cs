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
    public class EditPublicationModel : PageModel
    {
        private readonly PublicationDbContext phdStudentsDbContext;
        private readonly ProfileDbContext profileDbContext;
        public Profile CurrentProfile { set; get; }

        [BindProperty]
        public Publications Publication { get; set; }

        [TempData]
        public int ID { set; get; }

        public EditPublicationModel(PublicationDbContext phd, ProfileDbContext prdb)
        {
            phdStudentsDbContext = phd;
            profileDbContext = prdb;
            CurrentProfile = new Profile();
            Publication = new Publications();
        }

        public async Task OnGetAsync(int? id)
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            ID = (int)id;
            if (User.Identity.IsAuthenticated)
            {
                Publication = await phdStudentsDbContext.Publications.SingleOrDefaultAsync(m => m.ID == id);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    if (ID == 0)
                    {
                        ID = Publication.ID;
                    }
                    var tempUser = await phdStudentsDbContext.Publications.SingleOrDefaultAsync(m => m.ID == ID);
                    tempUser.PublicationTitle = Publication.PublicationTitle;
                    tempUser.PublicationYear = Publication.PublicationYear;
                    tempUser.PublicationLink = Publication.PublicationLink;
                    tempUser.PublicationType = Publication.PublicationType;
                    phdStudentsDbContext.Publications.Update(tempUser);
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