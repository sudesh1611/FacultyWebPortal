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
    public class EditAssignmentDeadlineModel : PageModel
    {
        private readonly AssignmentDbContext assignmentDbContext;
        private readonly ProfileDbContext profileDbContext;
        public Profile CurrentProfile { set; get; }

        [BindProperty]
        public Assignment newAssignment { get; set; }

        public int ID { set; get; }

        public EditAssignmentDeadlineModel(AssignmentDbContext phd, ProfileDbContext pDb)
        {
            assignmentDbContext = phd;
            newAssignment = new Assignment();
            profileDbContext = pDb;
            CurrentProfile = new Profile();
        }

        public async Task OnGetAsync(int? id)
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            ID = (int)id;
            if (User.Identity.IsAuthenticated)
            {
                newAssignment = await assignmentDbContext.Assignments.SingleOrDefaultAsync(m => m.ID == id);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    if (ID == 0)
                    {
                        ID = newAssignment.ID;
                    }
                    var tempUser = await assignmentDbContext.Assignments.SingleOrDefaultAsync(m => m.ID == ID);
                    tempUser.DeadlineDay = newAssignment.DeadlineDay;
                    tempUser.DeadLineMonth = newAssignment.DeadLineMonth;
                    tempUser.DeadlineTime = newAssignment.DeadlineTime + ":00:00";
                    tempUser.DeadlineYear = newAssignment.DeadlineYear;
                    assignmentDbContext.Assignments.Update(tempUser);
                    await assignmentDbContext.SaveChangesAsync();
                    return RedirectToPage("/EditAssignmentsPage");
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