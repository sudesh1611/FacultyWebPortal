using Faculty.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Data
{
    public class SubmissionDbContext:DbContext
    {
        public SubmissionDbContext(DbContextOptions<SubmissionDbContext> options):base(options)
        {

        }

        public DbSet<Submission> Submissions { get; set; }
    }
}
