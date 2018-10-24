using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Faculty.Pages
{
    public class DeletePublicationModel : PageModel
    {
        private readonly PublicationDbContext publicationDbContext;

        public DeletePublicationModel(PublicationDbContext pdb)
        {
            publicationDbContext = pdb;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            int ID = (int)id;
            if (User.Identity.IsAuthenticated)
            {
                var x = await publicationDbContext.Publications.SingleOrDefaultAsync(m => m.ID == ID);
                if (x != null)
                {
                    publicationDbContext.Publications.Remove(x);
                    await publicationDbContext.SaveChangesAsync();
                    return RedirectToPage("/EditPublicationsPage");
                }
                else
                {
                    return RedirectToPage("/Error");
                }
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }
    }
}