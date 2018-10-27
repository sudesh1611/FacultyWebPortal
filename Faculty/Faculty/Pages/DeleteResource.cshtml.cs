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
    public class DeleteResourceModel : PageModel
    {
        private readonly CourseResourceDbContext courseResourceDbContext;

        public DeleteResourceModel(CourseResourceDbContext cdb)
        {
            courseResourceDbContext = cdb;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            int ID = (int)id;
            if(User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                var x = await courseResourceDbContext.Resources.SingleOrDefaultAsync(m => m.ID == ID);
                if (x != null)
                {
                    courseResourceDbContext.Resources.Remove(x);
                    await courseResourceDbContext.SaveChangesAsync();
                    return RedirectToPage("/EditCourseResourcesPage");
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