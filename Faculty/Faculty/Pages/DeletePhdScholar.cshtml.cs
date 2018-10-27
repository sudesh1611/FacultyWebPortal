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
        private readonly ProfileDbContext profileDbContext;
        public Profile CurrentProfile { set; get; }

        [BindProperty]
        public PhdStudents Student { get; set; }

        [TempData]
        public int ID { set; get; }

        public DeletePhdScholarModel(PhdStudentsDbContext phd, ProfileDbContext pDb)
        {
            phdStudentsDbContext = phd;
            profileDbContext = pDb;
            CurrentProfile = new Profile();
        }

        public async Task OnGetAsync(int? id)
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            ID = (int)id;
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                Student = await phdStudentsDbContext.PhdStudents.SingleOrDefaultAsync(m => m.ID == id);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                if (ModelState.IsValid)
                {
                    if(ID==0)
                    {
                        ID = Student.ID;
                    }
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