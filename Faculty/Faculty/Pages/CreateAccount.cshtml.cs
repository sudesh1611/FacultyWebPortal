using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Faculty.Data;
using Faculty.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Faculty.Pages
{
    public class CreateAccountModel : PageModel
    {
        [BindProperty]
        public User NewUser { get; set; }

        private readonly UserDbContext userDbContext;
        private readonly ProfileDbContext profileDbContext;
        public Profile CurrentProfile { set; get; }

        public CreateAccountModel(UserDbContext uDb,ProfileDbContext pDb)
        {
            userDbContext = uDb;
            profileDbContext = pDb;
            CurrentProfile = new Profile();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            if(CurrentProfile!=null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            if (CurrentProfile != null)
            {
                return RedirectToPage("/NotFound");
            }
            //if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                if(ModelState.IsValid)
                {
                    NewUser.EmailID = NewUser.EmailID.ToLower();
                    Models.User user = await userDbContext.Users.SingleOrDefaultAsync(m => m.EmailID == NewUser.EmailID);
                    if(user==null)
                    {
                        Profile newProfile = new Profile()
                        {
                            LoginEmailID = NewUser.EmailID,
                            FullName = NewUser.FullName,
                            Designation = String.Empty,
                            Department = String.Empty,
                            College = String.Empty,
                            EmailID = String.Empty,
                            MobileNumber = String.Empty,
                            GoogleScholarLink = String.Empty,
                            UnderGraduateDegreeDetails = String.Empty,
                            PostGraduateDegreeDetails = String.Empty,
                            DoctoratesDegreeDetails = String.Empty,
                            AreasOfInterest = String.Empty,
                            Achievements = String.Empty,
                            ShortBio = String.Empty,
                            ProfilePic = String.Empty,
                            ProfessionalExperience = String.Empty
                        };
                        await profileDbContext.Profiles.AddAsync(newProfile);
                        await userDbContext.Users.AddAsync(NewUser);
                        await profileDbContext.SaveChangesAsync();
                        await userDbContext.SaveChangesAsync();

                        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                        var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name,NewUser.FullName),
                                new Claim(ClaimTypes.Role,"Admin"),
                                new Claim(ClaimTypes.Email,NewUser.EmailID),
                            };
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.AddMonths(1)
                        };
                        await HttpContext.SignInAsync(
                                CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(claimsIdentity),
                                authProperties);
                        return RedirectToPage("/EditProfile");
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Email ID already Registered");
                        return Page();
                    }
                }
                else
                {
                    return Page();
                }
            }
            //else
            //{
            //    return RedirectToPage("/Index");
            //}
        }
    }
}