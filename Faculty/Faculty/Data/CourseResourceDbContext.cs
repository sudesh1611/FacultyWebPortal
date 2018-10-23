using Faculty.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Data
{
    public class CourseResourceDbContext:DbContext
    {
        public CourseResourceDbContext(DbContextOptions<CourseResourceDbContext> options):base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }
    }
}
