using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Faculty.Pages
{
    public class LoginModel : PageModel
    {

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
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                LoginInput.EmailID = LoginInput.EmailID.ToLower();
                if(LoginInput.EmailID=="sudesh1611@gmail.com")
                {
                    if(LoginInput.Password=="$ude$h1611")
                    {
                        var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name,"Sudesh Kumar"),
                                new Claim(ClaimTypes.GivenName,"Admin"),
                                new Claim(ClaimTypes.Email,"sudesh1611@gmail.com"),
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