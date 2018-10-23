using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Data;
using Faculty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Faculty.Pages
{
    public class CourseModel : PageModel
    {
        private readonly CourseDbContext courseDbContext;
        private readonly AssignmentDbContext assignmentDbContext;
        private readonly ProfileDbContext profileDbContext;

        public Course CurrentCourse { set; get; }
        public Profile CurrentProfile { get; set; }
        public List<Assignment> AssignmentList { set; get; }
        public List<string> TAs { get; set; }

        public CourseModel(CourseDbContext cdb,AssignmentDbContext adb,ProfileDbContext pdb)
        {
            courseDbContext = cdb;
            assignmentDbContext = adb;
            profileDbContext = pdb;
            CurrentCourse = new Course();
            CurrentProfile = new Profile();
            AssignmentList = new List<Assignment>();
            TAs = new List<string>();
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            int ID = (int)id;
            CurrentCourse = await courseDbContext.Courses.SingleOrDefaultAsync(m => m.ID == ID);
            if(CurrentCourse!=null)
            {
                CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == CurrentCourse.SupervisorID);
                TAs = JsonConvert.DeserializeObject<List<string>>(CurrentCourse.TeachingAssistants);
                var tempList = await assignmentDbContext.Assignments.ToListAsync();
                foreach (var assignment in tempList)
                {
                    if(assignment.CourseID==ID)
                    {
                        AssignmentList.Add(assignment);
                    }
                }
                return Page();
            }
            else
            {
                return RedirectToPage("/NotFound");
            }
        }
    }
}