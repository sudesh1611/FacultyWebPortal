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
    public class EditPublicationsPageModel : PageModel
    {
        private readonly ProfileDbContext profileDbContext;
        private readonly PublicationDbContext publicationDbContext;
        public Profile CurrentProfile { set; get; }

        public List<Publications> PublicationsList { get; set; }

        public EditPublicationsPageModel(ProfileDbContext pdb, PublicationDbContext ppd)
        {
            profileDbContext = pdb;
            publicationDbContext = ppd;
            CurrentProfile = new Profile();
            PublicationsList = new List<Publications>();
        }

        public async Task OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                PublicationsList = await publicationDbContext.Publications.ToListAsync();
                if(PublicationsList!=null)
                {
                    PublicationsList.OrderBy(m => m.PublicationYear);
                    PublicationsList.Reverse();
                }
            }
        }
    }
}