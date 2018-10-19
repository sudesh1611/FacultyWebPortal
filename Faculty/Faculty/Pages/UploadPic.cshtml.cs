using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Faculty.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Faculty.Pages
{
    public class UploadPicModel : PageModel
    {
        private readonly ProfileDbContext profileDbContext;

        public UploadPicModel(ProfileDbContext pDP)
        {
            profileDbContext = pDP;
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("File Not Selected");

            string emaiID = User.FindFirst(ClaimTypes.Email).Value;
            var profile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.LoginEmailID == emaiID);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", profile.ID.ToString() + file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            profile.ProfilePic = "images/" + profile.ID.ToString() + file.FileName;
            profileDbContext.Profiles.Update(profile);
            await profileDbContext.SaveChangesAsync();
            return RedirectToPage("/EditProfile");
        }
    }
}