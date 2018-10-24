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
    public class ResearchScholarsModel : PageModel
    {
        private readonly ProfileDbContext profileDbContext;
        private readonly PhdStudentsDbContext phdStudentsDbContext;
        public Profile CurrentProfile { set; get; }

        public List<PhdStudents> PhdStudentsList { get; set; }

        public ResearchScholarsModel(ProfileDbContext pdc, PhdStudentsDbContext cdc)
        {
            profileDbContext = pdc;
            phdStudentsDbContext = cdc;
            CurrentProfile = new Profile();
        }

        public async Task OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            {
                var Supervisor = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
                PhdStudentsList = new List<PhdStudents>();
                PhdStudentsList = await phdStudentsDbContext.PhdStudents.ToListAsync();
                if(PhdStudentsList!=null)
                {
                    PhdStudentsList.OrderBy(m => m.DegreeCompletion);
                }
                
            }
        }
    }
}