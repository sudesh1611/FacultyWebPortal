using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Faculty.Data;
using Faculty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Faculty.Pages
{
    public class EditPhdScholarModel : PageModel
    {
        private readonly PhdStudentsDbContext phdStudentsDbContext;
        private readonly ProfileDbContext profileDbContext;

        [BindProperty]
        public PhdStudents Student { get; set; }

        [TempData]
        public int ID { set; get; }

        public EditPhdScholarModel(PhdStudentsDbContext phd,ProfileDbContext pro)
        {
            phdStudentsDbContext = phd;
            profileDbContext = pro;
            Student = new PhdStudents();
        }

        public async Task OnGetAsync(int? id)
        {
            ID = (int)id;
            if (User.Identity.IsAuthenticated)
            {
                Student = await phdStudentsDbContext.PhdStudents.SingleOrDefaultAsync(m => m.ID == id);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var tempUser = await phdStudentsDbContext.PhdStudents.SingleOrDefaultAsync(m => m.ID == ID);
                    tempUser.College = Student.College;
                    tempUser.DegreeCompletion = Student.DegreeCompletion;
                    tempUser.DegreeStatus = Student.DegreeStatus;
                    tempUser.StudentName = Student.StudentName;
                    tempUser.ThesisTitle = Student.ThesisTitle;
                    phdStudentsDbContext.PhdStudents.Update(tempUser);
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