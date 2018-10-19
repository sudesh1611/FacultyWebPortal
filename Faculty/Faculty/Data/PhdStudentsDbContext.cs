using Faculty.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Data
{
    public class PhdStudentsDbContext:DbContext
    {
        public PhdStudentsDbContext(DbContextOptions<PhdStudentsDbContext> options):base(options)
        {

        }

        public DbSet<PhdStudents> PhdStudents { get; set; }
    }
}
