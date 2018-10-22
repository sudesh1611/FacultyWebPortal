using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class LoginModel : PageModel
    {
        private readonly UserDbContext userDbContext;
        private readonly ProfileDbContext profileDbContext;
        public Profile CurrentProfile { set; get; }

        public LoginModel(UserDbContext pdc,ProfileDbContext pdp)
        {
            userDbContext = pdc;
            profileDbContext = pdp;
            CurrentProfile = new Profile();
        }

        public class InputModel
        {
            [Required]
            public string EmailID { get; set; }

            [Required]
            public string Password { get; set; }
        }

        [BindProperty]
        public InputModel LoginInput { get; set; }

        public async Task OnGetAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                LoginInput.EmailID = LoginInput.EmailID.ToLower();
                var user = await userDbContext.Users.SingleOrDefaultAsync(m => m.EmailID == LoginInput.EmailID);
                if(user!=null)
                {
                    if(user.Password==LoginInput.Password)
                    {
                        var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name,user.FullName),
                                new Claim(ClaimTypes.GivenName,"Admin"),
                                new Claim(ClaimTypes.Email,user.EmailID),
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
                        return RedirectToPage("/AdminDashboard");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Login Attempt!");
                        return Page();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt!");
                    return Page();
                }
            }
            else
            {
                return Page();
            }
        }
    }
} 