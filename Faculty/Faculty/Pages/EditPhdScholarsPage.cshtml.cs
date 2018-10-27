﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Faculty.Data;
using Faculty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Faculty.Pages
{
    public class EditPhdScholarsPageModel : PageModel
    {
        private readonly ProfileDbContext profileDbContext;
        private readonly PhdStudentsDbContext phdStudentsDbContext;
        public Profile CurrentProfile { set; get; }

        public List<PhdStudents> phdStudentsList { get; set; }

        public EditPhdScholarsPageModel(ProfileDbContext pdb,PhdStudentsDbContext phddb)
        {
            profileDbContext = pdb;
            phdStudentsDbContext = phddb;
            CurrentProfile = new Profile();
        }

        public async Task OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                var Supervisor = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.LoginEmailID == emaiID);
                phdStudentsList = new List<PhdStudents>();
                var tempList = await phdStudentsDbContext.PhdStudents.ToListAsync();
                foreach (var item in tempList)
                {
                    if(item.SupervisorID==Supervisor.ID)
                    {
                        phdStudentsList.Add(item);
                    }
                }
            }
        }
    }
}