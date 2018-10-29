using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Faculty.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Faculty.Pages
{
    public class EnrollModel : PageModel
    {
        private readonly CourseDbContext courseDbContext;
        private readonly StudentDbContext studentDbContext;

        public EnrollModel(CourseDbContext cdb, StudentDbContext sdb)
        {
            courseDbContext = cdb;
            studentDbContext = sdb;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            int ID = (int)id;
            var Course = await courseDbContext.Courses.SingleOrDefaultAsync(m => m.ID == ID);
            if (Course != null)
            {
                if (User.Identity.IsAuthenticated && User.IsInRole("Student"))
                {
                    string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                    var Student = await studentDbContext.Students.SingleOrDefaultAsync(m => m.EmailID == emaiID);
                    var AppliedList = JsonConvert.DeserializeObject<List<int>>(Course.StudentRegistered);
                    AppliedList.Add(Student.ID);
                    Course.StudentRegistered = JsonConvert.SerializeObject(AppliedList);
                    courseDbContext.Courses.Update(Course);
                    await courseDbContext.SaveChangesAsync();
                    return RedirectToPage("/CourseRegistration", new { id = Course.ID });
                }
                else
                {
                    return RedirectToPage("/Error");
                }
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }
    }
}