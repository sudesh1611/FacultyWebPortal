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
    public class ViewAssignmentModel : PageModel
    {
        private readonly AssignmentDbContext assignmentDbContext;
        private readonly CourseDbContext courseDbContext;

        public Assignment CurrentAssignment { set; get; }
        public Course CurrentCourse { set; get; }
        public bool IfDue { set; get; }

        public ViewAssignmentModel(AssignmentDbContext adb, CourseDbContext cdb)
        {
            assignmentDbContext = adb;
            courseDbContext = cdb;
            CurrentAssignment = new Assignment();
            CurrentCourse = new Course();
            IfDue = false;
        }

        public async Task OnGetAsync(int? id)
        {
            int ID = (int)id;
            CurrentAssignment = await assignmentDbContext.Assignments.SingleOrDefaultAsync(m => m.ID == ID);
            if(CurrentAssignment!=null)
            {
                CurrentCourse = await courseDbContext.Courses.SingleOrDefaultAsync(m => m.ID == CurrentAssignment.CourseID);
                if ((CurrentAssignment.DeadlineYear <= DateTime.Now.Year) && (CurrentAssignment.DeadLineMonth <= DateTime.Now.Month) && (CurrentAssignment.DeadlineDay <= DateTime.Now.Day) && ((DateTime.Parse(CurrentAssignment.DeadlineTime)).Ticks < DateTime.Now.Ticks))
                {
                    IfDue = false;
                }
                else
                {
                    IfDue = true;
                }
            }
        }
    }
}