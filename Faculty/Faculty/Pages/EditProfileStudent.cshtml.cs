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
    public class EditProfileStudentModel : PageModel
    {
        [BindProperty]
        public Student NewStudent { set; get; }

        public Profile CurrentProfile { set; get; }

        private readonly ProfileDbContext profileDbContext;

        private readonly StudentDbContext studentDbContext;

        public EditProfileStudentModel(ProfileDbContext pdp,StudentDbContext ppd)
        {
            profileDbContext = pdp;
            studentDbContext = ppd;
            CurrentProfile = new Profile();
        }

        public async Task OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            if (User.Identity.IsAuthenticated && User.IsInRole("Student"))
            {
                string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                NewStudent = await studentDbContext.Students.SingleOrDefaultAsync(m => m.EmailID == emaiID);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Student"))
            {
                if (ModelState.IsValid)
                {
                    string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                    var student = await studentDbContext.Students.SingleOrDefaultAsync(m => m.EmailID == emaiID);
                    student.Year = NewStudent.Year;
                    studentDbContext.Students.Update(student);
                    await studentDbContext.SaveChangesAsync();
                    return RedirectToPage("/Index");
                }
                else
                {
                    return Page();
                }
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
    }
}