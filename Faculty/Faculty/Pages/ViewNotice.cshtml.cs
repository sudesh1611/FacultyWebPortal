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
    public class ViewNoticeModel : PageModel
    {
        private readonly ProfileDbContext profileDbContext;
        private readonly NoticeDbContext noticeDbContext;
        public Profile CurrentProfile { set; get; }
        public Notice CurrentNotice { get; set; }

        public ViewNoticeModel(ProfileDbContext pdb, NoticeDbContext phddb)
        {
            profileDbContext = pdb;
            noticeDbContext = phddb;
            CurrentProfile = new Profile();
            CurrentNotice = new Notice();
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            int ID = (int)id;
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            CurrentNotice = await noticeDbContext.Notices.SingleOrDefaultAsync(m => m.ID == ID);
            if (CurrentNotice == null)
            {
                return RedirectToPage("/NotFound");
            }
            else
            {
                return Page();
            }

        }
    }
}