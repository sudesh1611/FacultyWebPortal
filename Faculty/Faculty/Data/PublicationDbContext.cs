using Faculty.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Data
{
    public class PublicationDbContext:DbContext
    {
        public PublicationDbContext(DbContextOptions<PublicationDbContext> options):base(options)
        {

        }

        public DbSet<Publications> Publications { set; get; }
    }
}
