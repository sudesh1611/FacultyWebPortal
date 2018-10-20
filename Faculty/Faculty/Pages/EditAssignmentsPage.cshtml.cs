using System;
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
    public class EditAssignmentsPageModel : PageModel
    {
        private readonly AssignmentDbContext assignmentDbContext;

        public EditAssignmentsPageModel(AssignmentDbContext asd)
        {
            assignmentDbContext = asd;
            OldAssignmentsList = new List<Assignment>();
            OngoingAssignmentsList = new List<Assignment>();
        }

        public List<Assignment> OldAssignmentsList { get; set; }
        public List<Assignment> OngoingAssignmentsList { get; set; }

        public async Task OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var AssignmentsList = await assignmentDbContext.Assignments.ToListAsync();
                
                if(AssignmentsList!=null)
                {
                    foreach (var assignment in AssignmentsList)
                    {
                        if( (assignment.DeadlineYear<=DateTime.Now.Year) && (assignment.DeadLineMonth<=DateTime.Now.Month) && (assignment.DeadlineDay<=DateTime.Now.Day) && ((DateTime.Parse(assignment.DeadlineTime)).Ticks<DateTime.Now.Ticks))
                        {
                            OldAssignmentsList.Add(assignment);
                        }
                        else
                        {
                            OngoingAssignmentsList.Add(assignment);
                        }

                    }
                    OldAssignmentsList.Reverse();
                    OngoingAssignmentsList.Reverse();
                }
            }
        }

    }
}