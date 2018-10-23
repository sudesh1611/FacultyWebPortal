using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Data;
using Faculty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Faculty.Pages
{
    public class AssignmentsModel : PageModel
    {
        private readonly AssignmentDbContext assignmentDbContext;
        private readonly CourseDbContext courseDbContext;

        public List<Assignment> PendingAssignments { get; set; }
        public List<Assignment> OldAssignments { get; set; }
        public Course CurrentCourse { get; set; }

        public AssignmentsModel(AssignmentDbContext adb, CourseDbContext cdb)
        {
            assignmentDbContext = adb;
            courseDbContext = cdb;
            PendingAssignments = new List<Assignment>();
            OldAssignments = new List<Assignment>();
            CurrentCourse = new Course();
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            int ID = (int)id;
            CurrentCourse = await courseDbContext.Courses.SingleOrDefaultAsync(m => m.ID == ID);
            if(CurrentCourse!=null)
            {
                var tempList = await assignmentDbContext.Assignments.ToListAsync();
                foreach (var assignment in tempList)
                {
                    if (assignment.CourseID == ID)
                    {
                        if ((assignment.DeadlineYear <= DateTime.Now.Year) && (assignment.DeadLineMonth <= DateTime.Now.Month) && (assignment.DeadlineDay <= DateTime.Now.Day) && ((DateTime.Parse(assignment.DeadlineTime)).Ticks < DateTime.Now.Ticks))
                        {
                            OldAssignments.Add(assignment);
                        }
                        else
                        {
                            PendingAssignments.Add(assignment);
                        }
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