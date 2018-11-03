using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using Faculty.Data;
using Faculty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Faculty.Pages
{
    public class ExportAssignmentSubmissionsModel : PageModel
    {
        private readonly SubmissionDbContext submissionDbContext;
        private readonly AssignmentDbContext assignmentDbContext;
        public List<Submission> SubmissionsLists { set; get; }
        public Assignment assignment { get; set; }
        private IConverter _converter;

        public ExportAssignmentSubmissionsModel(SubmissionDbContext asd, AssignmentDbContext adb, IConverter converter)
        {
            submissionDbContext = asd;
            assignmentDbContext = adb;
            _converter = converter;
            SubmissionsLists = new List<Submission>();
            assignment = new Assignment();
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                int ID = (int)id;
                assignment = await assignmentDbContext.Assignments.SingleOrDefaultAsync(m => m.ID == ID);
                if (assignment == null)
                {
                    return RedirectToPage("/NotFound");
                }
                var tempList = await submissionDbContext.Submissions.ToListAsync();
                foreach (var submission in tempList)
                {
                    if (submission.AssignmentID == ID)
                    {
                        SubmissionsLists.Add(submission);
                    }
                }
                if (SubmissionsLists != null)
                {
                    SubmissionsLists.OrderBy(m => m.RollNumber);
                }
                var sb = new StringBuilder();
                sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>");

                sb.AppendFormat(@"<div class='header'><h2>Assignment Title: {0}</h2></div>", assignment.AssignmentTitle);
                sb.Append(@"<table align='center'>
                                    <tr>
                                        <th>S.No.</th>
                                        <th>Roll Number</th>
                                        <th>Name</th>
                                        <th>Submission Date & Time</th>
                                    </tr>");
                int counter = 1;
                foreach (var submission in SubmissionsLists)
                {
                    sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                  </tr>",counter, submission.RollNumber, submission.SubmittedBy, submission.DateTime);
                    counter++;
                }
                sb.Append(@"
                                </table>
                            </body>
                        </html>");


                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = assignment.AssignmentTitle + " Submissions"
                    //Out = @"D:\PDFCreator\Employee_Report.pdf"
                };

                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = sb.ToString(),
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Line = true,Center=assignment.CourseName+" Submissions" },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Page [page] of [toPage]" }
                };

                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };

                var file = _converter.Convert(pdf);
                return File(file, "application/pdf");

            }
            else
            {
                return RedirectToPage("/Login");
            }
        }
    }
}