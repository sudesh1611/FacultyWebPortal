using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Faculty.Pages
{
    public class ApproveModel : PageModel
    {
        private readonly CourseDbContext courseDbContext;

        public ApproveModel(CourseDbContext cdb)
        {
            courseDbContext = cdb;
        }

        public async Task<IActionResult> OnGetAsync(int? id,int? course)
        {
            int ID = (int)id;
            int CourseID = (int)course;
            if(User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                var Course = await courseDbContext.Courses.SingleOrDefaultAsync(m => m.ID == CourseID);
                if(Course!=null)
                {
                    List<int> Listofstu = new List<int>();
                    Listofstu = JsonConvert.DeserializeObject<List<int>>(Course.StudentsApproved);
                    Listofstu.Add(ID);
                    Course.StudentsApproved = JsonConvert.SerializeObject(Listofstu);
                    List<int> Listofstu2 = new List<int>();
                    Listofstu2 = JsonConvert.DeserializeObject<List<int>>(Course.StudentRegistered);
                    Listofstu2.Remove(ID);
                    Course.StudentRegistered = JsonConvert.SerializeObject(Listofstu2);
                    List<int> Listofstu3 = new List<int>();
                    Listofstu3 = JsonConvert.DeserializeObject<List<int>>(Course.StudentDeclined);
                    Listofstu3.Remove(ID);
                    Course.StudentDeclined = JsonConvert.SerializeObject(Listofstu3);
                    courseDbContext.Courses.Update(Course);
                    await courseDbContext.SaveChangesAsync();
                    return RedirectToPage("/ViewRegistrationAdmin", new { id = CourseID });
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