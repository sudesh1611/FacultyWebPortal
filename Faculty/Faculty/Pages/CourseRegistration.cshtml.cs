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
using Newtonsoft.Json;

namespace Faculty.Pages
{
    public class CourseRegistrationModel : PageModel
    {
        private readonly ProfileDbContext profileDbContext;
        private readonly CourseDbContext courseDbContext;
        private readonly StudentDbContext studentDbContext;
        public Profile CurrentProfile { get; set; }
        public Course CurrentCourse { get; set; }
        public Student CurrentStudent { get; set; }
        public string CourseStatus { get; set; }
        public bool IfOver { get; set; }
        public CourseRegistrationModel(ProfileDbContext pdb, CourseDbContext cdb, StudentDbContext sdb)
        {
            profileDbContext = pdb;
            courseDbContext = cdb;
            studentDbContext = sdb;
            CurrentProfile = new Profile();
            CourseStatus = "Apply";
            IfOver = false;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);

            int ID = (int)id;
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            CurrentCourse = await courseDbContext.Courses.SingleOrDefaultAsync(m => m.ID == ID);
            if (CurrentCourse != null)
            {

                if ((CurrentCourse.DeadlineYear <= DateTime.Now.Year) && (CurrentCourse.DeadLineMonth <= DateTime.Now.Month) && (CurrentCourse.DeadlineDay <= DateTime.Now.Day) && ((DateTime.Parse(CurrentCourse.DeadlineTime)).Ticks < DateTime.Now.Ticks))
                {
                    IfOver = true;
                    return Page();
                }
                if (User.Identity.IsAuthenticated && User.IsInRole("Student"))
                {
                    string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                    CurrentStudent = await studentDbContext.Students.SingleOrDefaultAsync(m => m.EmailID == emaiID);
                    if (CurrentStudent != null)
                    {
                        try
                        {
                            var StudentApproved = JsonConvert.DeserializeObject<List<int>>(CurrentCourse.StudentsApproved);
                            if (StudentApproved.Any())
                            {
                                foreach (var StudentID in StudentApproved)
                                {
                                    if (StudentID == CurrentStudent.ID)
                                    {
                                        CourseStatus = "Approved";
                                        return Page();
                                    }
                                }
                            }
                            var StudentApplied = JsonConvert.DeserializeObject<List<int>>(CurrentCourse.StudentRegistered);
                            if (StudentApplied.Any())
                            {
                                foreach (var StudentID in StudentApplied)
                                {
                                    if (StudentID == CurrentStudent.ID)
                                    {
                                        CourseStatus = "Registered";
                                        return Page();
                                    }
                                }
                            }
                            var StudentDeclined = JsonConvert.DeserializeObject<List<int>>(CurrentCourse.StudentDeclined);
                            if (StudentDeclined.Any())
                            {
                                foreach (var StudentID in StudentDeclined)
                                {
                                    if (StudentID == CurrentStudent.ID)
                                    {
                                        CourseStatus = "Declined";
                                        return Page();
                                    }
                                }
                            }
                            CourseStatus = "Apply";
                            return Page();
                        }
                        catch (Exception)
                        {
                            CourseStatus = "Apply";
                            return Page();
                        }
                    }
                    else
                    {
                        return RedirectToPage("/Error");
                    }
                }
                else
                {
                    return Page();
                }
            }
            else
            {
                return RedirectToPage("/NotFound");
            }

        }
    }
}