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
    public class ViewAssignmentAdminModel : PageModel
    {
        private readonly AssignmentDbContext assignmentDbContext;

        [BindProperty]
        public Assignment newAssignment { get; set; }

        [TempData]
        public int ID { set; get; }

        public ViewAssignmentAdminModel(AssignmentDbContext phd)
        {
            assignmentDbContext = phd;
            newAssignment = new Assignment();
        }

        public async Task OnGetAsync(int? id)
        {
            ID = (int)id;
            if (User.Identity.IsAuthenticated)
            {
                newAssignment = await assignmentDbContext.Assignments.SingleOrDefaultAsync(m => m.ID == id);
            }
        }
    }
}