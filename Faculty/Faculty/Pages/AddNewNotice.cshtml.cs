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
    public class AddNewNoticeModel : PageModel
    {
        private readonly NoticeDbContext noticeDbContext;
        private readonly ProfileDbContext profileDbContext;

        [BindProperty]
        public Notice CurrentNotice { get; set; }

        public Profile CurrentProfile { get; set; }

        public AddNewNoticeModel(NoticeDbContext bdb, ProfileDbContext pdb)
        {
            noticeDbContext = bdb;
            profileDbContext = pdb;
            CurrentProfile = new Profile();
        }

        public async Task OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                if(ModelState.IsValid)
                {
                    Notice newNotice = new Notice()
                    {
                        NoticeShortTitle = CurrentNotice.NoticeShortTitle,
                        NoticeDescription = CurrentNotice.NoticeDescription,
                        NoticeDate = DateTime.Now.ToShortDateString()
                    };
                    await noticeDbContext.Notices.AddAsync(newNotice);
                    await noticeDbContext.SaveChangesAsync();
                    return RedirectToPage("/EditNoticesPage");
                }
                else
                {
                    return Page();
                }
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }
    }
}