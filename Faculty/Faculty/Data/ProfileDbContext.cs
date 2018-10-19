using Faculty.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Data
{
    public class ProfileDbContext:DbContext
    {
        public ProfileDbContext(DbContextOptions<ProfileDbContext> options):base(options)
        {

        }

        public DbSet<Profile> Profiles { get; set; }
    }
}
