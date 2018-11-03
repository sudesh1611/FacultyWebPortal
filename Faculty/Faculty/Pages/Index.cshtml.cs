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
    public class IndexModel : PageModel
    {
        private readonly ProfileDbContext profileDbContext;
        private readonly AssignmentDbContext assignmentDbContext;
        private readonly CourseDbContext courseDbContext;
        public Profile CurrentProfile { set; get; }
        public List<Assignment> AssignmentsList { get; set; }
        public List<Course> CoursesList { get; set; }

        private readonly NoticeDbContext noticeDbContext;

        public List<Notice> NoticesList { get; set; }

        public int Counter { get; set; }

        public IndexModel(ProfileDbContext pdb, NoticeDbContext ndb,AssignmentDbContext adb,CourseDbContext cdb)
        {
            profileDbContext = pdb;
            noticeDbContext = ndb;
            assignmentDbContext = adb;
            courseDbContext = cdb;
            CoursesList = new List<Course>();
            CurrentProfile = new Profile();
            NoticesList = new List<Notice>();
            AssignmentsList = new List<Assignment>();
        }

        public async Task OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            NoticesList = await noticeDbContext.Notices.ToListAsync();
            if (NoticesList != null)
            {
                NoticesList.OrderByDescending(m => m.NoticeDate);
            }
            var tempList = await assignmentDbContext.Assignments.ToListAsync();
            if (AssignmentsList != null)
            {
                foreach (var assignment in tempList)
                {

                    if ((assignment.DeadlineYear <= DateTime.Now.Year) && (assignment.DeadLineMonth <= DateTime.Now.Month) && (assignment.DeadlineDay <= DateTime.Now.Day) && ((DateTime.Parse(assignment.DeadlineTime)).Ticks < DateTime.Now.Ticks))
                    {
                        
                    }
                    else
                    {
                        AssignmentsList.Add(assignment);
                    }
                }
            }
            CoursesList = await courseDbContext.Courses.ToListAsync();
            if(CoursesList!=null)
            {
                CoursesList.OrderByDescending(m => m.CourseYear);
            }

        }

    }
}
