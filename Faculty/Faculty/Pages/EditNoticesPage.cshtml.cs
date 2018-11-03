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
    public class EditNoticesPageModel : PageModel
    {
        private readonly ProfileDbContext profileDbContext;
        private readonly NoticeDbContext noticeDbContext;
        public Profile CurrentProfile { set; get; }

        public List<Notice> NoticesList { get; set; }

        public EditNoticesPageModel(ProfileDbContext pdb, NoticeDbContext phddb)
        {
            profileDbContext = pdb;
            noticeDbContext = phddb;
            CurrentProfile = new Profile();
        }

        public async Task OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {

                NoticesList = new List<Notice>();
                NoticesList = await noticeDbContext.Notices.ToListAsync();
                if(NoticesList != null)
                {
                    NoticesList.OrderByDescending(m => m.NoticeDate);
                }
            }
        }
    }
}