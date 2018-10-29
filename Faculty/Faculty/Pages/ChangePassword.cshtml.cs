using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class ChangePasswordModel : PageModel
    {

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string OldPassword { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; }

            [Required]
            [Compare("NewPassword", ErrorMessage = "Passwords don't match")]
            [DataType(DataType.Password)]
            public string ConfirmNewPassword { get; set; }
        }

        [BindProperty]
        public InputModel InputData { get; set; }

        private readonly ProfileDbContext profileDbContext;
        private readonly UserDbContext userDbContext;
        private readonly StudentDbContext studentDbContext;
        public Profile CurrentProfile { set; get; }
        public ChangePasswordModel(ProfileDbContext pdb,UserDbContext udp,StudentDbContext sdb)
        {
            profileDbContext = pdb;
            userDbContext = udp;
            studentDbContext = sdb;
            CurrentProfile = new Profile();
        }

        public async Task OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(User.Identity.IsAuthenticated)
            {
                if(ModelState.IsValid && User.IsInRole("Admin"))
                {
                    var user = await userDbContext.Users.SingleOrDefaultAsync(m => m.ID == 1);
                    if(user.Password==InputData.OldPassword)
                    {
                        user.Password = InputData.NewPassword;
                        user.ConfirmPassword = InputData.ConfirmNewPassword;
                        userDbContext.Users.Update(user);
                        await userDbContext.SaveChangesAsync();
                        return RedirectToPage("/AdminDashboard");
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Old Password is not correct");
                        return Page();
                    }
                }
                else if(ModelState.IsValid && User.IsInRole("Student"))
                {
                    string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                    var user = await studentDbContext.Students.SingleOrDefaultAsync(m => m.EmailID == emaiID);
                    if (user.Password == InputData.OldPassword)
                    {
                        user.Password = InputData.NewPassword;
                        user.ConfirmPassword = InputData.ConfirmNewPassword;
                        studentDbContext.Students.Update(user);
                        await studentDbContext.SaveChangesAsync();
                        return RedirectToPage("/StudentDashboard");
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Old Password is not correct");
                        return Page();
                    }
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