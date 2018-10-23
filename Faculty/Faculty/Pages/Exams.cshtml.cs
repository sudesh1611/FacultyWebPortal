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
    public class ExamsModel : PageModel
    {
        private readonly CourseDbContext courseDbContext;

        public Course CurrentCourse { get; set; }

        public ExamsModel(CourseDbContext cdb)
        {
            courseDbContext = cdb;
            CurrentCourse = new Course();
        }

        public async Task OnGetAsync(int? id)
        {
            int ID = (int)id;
            CurrentCourse = await courseDbContext.Courses.SingleOrDefaultAsync(m => m.ID == ID);
        }
    }
}