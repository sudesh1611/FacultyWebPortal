using Faculty.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Data
{
    public class NoticeDbContext:DbContext
    {
        public NoticeDbContext(DbContextOptions<NoticeDbContext> options):base(options)
        {

        }

        public DbSet<Notice> Notices { get; set; }
    }
}
