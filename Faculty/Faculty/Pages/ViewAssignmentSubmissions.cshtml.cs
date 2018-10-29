﻿using System;
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
    public class ViewAssignmentSubmissionsModel : PageModel
    {
        private readonly SubmissionDbContext submissionDbContext;
        public List<Submission> SubmissionList { get; set; }
        private readonly ProfileDbContext profileDbContext;
        public Profile CurrentProfile { set; get; }
        [TempData]
        public int ID { get; set; }

        public ViewAssignmentSubmissionsModel(SubmissionDbContext sdb, ProfileDbContext pDbContext)
        {
            submissionDbContext = sdb;
            SubmissionList = new List<Submission>();
            profileDbContext = pDbContext;
            CurrentProfile = new Profile();
        }

        public async Task OnGetAsync(int? id)
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            ID = (int)id;
            var tempList = await submissionDbContext.Submissions.ToListAsync();
            foreach (var submission in tempList)
            {
                if(submission.AssignmentID==ID)
                {
                    SubmissionList.Add(submission);
                }
            }
            SubmissionList.OrderBy(m => m.RollNumber);
        }
    }
}