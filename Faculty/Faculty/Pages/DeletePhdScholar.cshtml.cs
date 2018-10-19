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
    public class DeletePhdScholarModel : PageModel
    {
        private readonly PhdStudentsDbContext phdStudentsDbContext;

        [BindProperty]
        public PhdStudents Student { get; set; }

        [TempData]
        public int ID { set; get; }

        public DeletePhdScholarModel(PhdStudentsDbContext phd)
        {
            phdStudentsDbContext = phd;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ID = (int)id;
            if (User.Identity.IsAuthenticated)
            {
                Student = await phdStudentsDbContext.PhdStudents.SingleOrDefaultAsync(m => m.ID == id);
                if (Student == null)
                {
                    return RedirectToPage("/Error");
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var tempUser = await phdStudentsDbContext.PhdStudents.SingleOrDefaultAsync(m => m.ID == ID);
                    phdStudentsDbContext.PhdStudents.Remove(tempUser);
                    await phdStudentsDbContext.SaveChangesAsync();
                    return RedirectToPage("/EditPhdScholarsPage");
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