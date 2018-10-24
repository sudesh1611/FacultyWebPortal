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
    public class AllPublicationsModel : PageModel
    {
        private readonly ProfileDbContext profileDbContext;
        private readonly PublicationDbContext publicationsDbContext;
        public Profile CurrentProfile { set; get; }

        public List<Publications> PublicationsList { get; set; }

        public AllPublicationsModel(ProfileDbContext pdc, PublicationDbContext cdc)
        {
            profileDbContext = pdc;
            publicationsDbContext = cdc;
            CurrentProfile = new Profile();
        }

        public async Task OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            {
                var Supervisor = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
                PublicationsList = new List<Publications>();
                PublicationsList = await publicationsDbContext.Publications.ToListAsync();
                if(PublicationsList!=null)
                {
                    PublicationsList.OrderBy(m => m.PublicationYear);
                    PublicationsList.Reverse();
                }
            }
        }
    }
}