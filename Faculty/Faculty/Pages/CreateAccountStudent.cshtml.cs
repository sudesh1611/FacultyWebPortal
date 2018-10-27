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
using Newtonsoft.Json;

namespace Faculty.Pages
{
    public class CreateAccountStudentModel : PageModel
    {

        [BindProperty]
        public Student NewStudent { get; set; }

        private readonly StudentDbContext studentDbContext;
        private readonly ProfileDbContext profileDbContext;
        public Profile CurrentProfile { set; get; }

        public CreateAccountStudentModel(StudentDbContext uDb, ProfileDbContext pDb)
        {
            studentDbContext = uDb;
            profileDbContext = pDb;
            CurrentProfile = new Profile();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            return RedirectToPage("/NotFound");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                NewStudent.EmailID = NewStudent.EmailID.ToLower();
                var stu = await studentDbContext.Students.SingleOrDefaultAsync(m => m.EmailID == NewStudent.EmailID);
                List<int> CoursesRegisteres = new List<int>();
                List<int> CoursesApproved = new List<int>();
                if (stu == null)
                {
                    Student newStu = new Student()
                    {
                        FullName = NewStudent.FullName,
                        EmailID = NewStudent.EmailID,
                        Password = NewStudent.Password,
                        ConfirmPassword = NewStudent.ConfirmPassword,
                        RollNumber = NewStudent.RollNumber,
                        ProfilePic = String.Empty,
                        Degree = NewStudent.Degree,
                        Year = NewStudent.Year,
                        CourseRegistered=JsonConvert.SerializeObject(CoursesRegisteres.ToString()),
                        CourseApproved = JsonConvert.SerializeObject(CoursesApproved.ToString())
                    };

                    await studentDbContext.Students.AddAsync(newStu);
                    await studentDbContext.SaveChangesAsync();

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name,newStu.FullName),
                                new Claim(ClaimTypes.Role,"Student"),
                                new Claim(ClaimTypes.Email,newStu.EmailID),
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
                    return RedirectToPage("/StudentDashboard");
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
    }
}