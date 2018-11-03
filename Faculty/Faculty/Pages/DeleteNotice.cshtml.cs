using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Faculty.Pages
{
    public class DeleteNoticeModel : PageModel
    {
        private readonly NoticeDbContext noticeDbContext;

        public DeleteNoticeModel(NoticeDbContext phddb)
        {
            noticeDbContext = phddb;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                int ID = (int)id;
                var CurrentNotice = await noticeDbContext.Notices.SingleOrDefaultAsync(m => m.ID == ID);
                if (CurrentNotice == null)
                {
                    return RedirectToPage("/NotFound");
                }
                else
                {
                    noticeDbContext.Notices.Remove(CurrentNotice);
                    await noticeDbContext.SaveChangesAsync();
                    return RedirectToPage("/EditNoticesPage");
                }
            }
            else
            {
                return RedirectToPage("/Error");
            }

        }
    }
}