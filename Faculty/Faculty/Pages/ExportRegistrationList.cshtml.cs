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
using Newtonsoft.Json;

namespace Faculty.Pages
{
    public class ExportRegistrationListModel : PageModel
    {
        private readonly CourseDbContext courseDbContext;
        private readonly StudentDbContext studentDbContext;
        public Course CurrentCourse { set; get; }
        public List<Student> ApprovedStudentList { get; set; }
        public List<Student> DeclinedStudentList { get; set; }
        public List<Student> AppliedStudentList { get; set; }
        private IConverter _converter;


        public ExportRegistrationListModel(CourseDbContext cdb, StudentDbContext sdb, IConverter converter)
        {
            courseDbContext = cdb;
            studentDbContext = sdb;
            _converter = converter;
            CurrentCourse = new Course();
            ApprovedStudentList = new List<Student>();
            DeclinedStudentList = new List<Student>();
            AppliedStudentList = new List<Student>();
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            int ID = (int)id;
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                CurrentCourse = await courseDbContext.Courses.SingleOrDefaultAsync(m => m.ID == ID);
                if (CurrentCourse == null)
                {
                    return RedirectToPage("/NotFound");
                }
                var StudentList = await studentDbContext.Students.ToListAsync();
                var tempList1 = JsonConvert.DeserializeObject<List<int>>(CurrentCourse.StudentRegistered);
                foreach (var item in tempList1)
                {
                    AppliedStudentList.Add(StudentList.Single(m => m.ID == item));
                }
                var tempList2 = JsonConvert.DeserializeObject<List<int>>(CurrentCourse.StudentsApproved);
                foreach (var item in tempList2)
                {
                    ApprovedStudentList.Add(StudentList.Single(m => m.ID == item));
                }
                var tempList3 = JsonConvert.DeserializeObject<List<int>>(CurrentCourse.StudentDeclined);
                foreach (var item in tempList3)
                {
                    DeclinedStudentList.Add(StudentList.Single(m => m.ID == item));
                }

                var sb = new StringBuilder();
                sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>");

                sb.Append(@"<div class='header'><h1>Course Registrations</h1></div>");
                sb.AppendFormat(@"<div class='subheading'><h3>{0}: {1} - {2} Semester {3}</h3></div>",CurrentCourse.CourseCode,CurrentCourse.CourseTitle,CurrentCourse.CourseSemester,CurrentCourse.CourseYear);
                sb.Append(@"<div class='mainheading'><h4>Approved Students List</h4></div>");
                sb.Append(@"<table align='center'>
                                    <tr>
                                        <th>S.No.</th>
                                        <th>Roll Number</th>
                                        <th>Name</th>
                                        <th>Degree</th>
                                        <th>Year</th>
                                    </tr>");
                int counter = 1;
                foreach (var students in ApprovedStudentList)
                {
                    sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                  </tr>", counter, students.RollNumber,students.FullName,students.Degree,students.Year);
                    counter++;
                }
                sb.Append(@"
                                </table>");
                sb.Append(@"<br/><br/><div class='mainheading'><h4>Declined Students List</h4></div>");
                sb.Append(@"<table align='center'>
                                    <tr>
                                        <th>S.No.</th>
                                        <th>Roll Number</th>
                                        <th>Name</th>
                                        <th>Degree</th>
                                        <th>Year</th>
                                    </tr>");
                counter = 1;
                foreach (var students in DeclinedStudentList)
                {
                    sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                  </tr>", counter, students.RollNumber, students.FullName, students.Degree, students.Year);
                    counter++;
                }
                sb.Append(@"
                                </table>");
                sb.Append(@"<br/><br/><div class='mainheading'><h4>Pending Approval Students List</h4></div>");
                sb.Append(@"<table align='center'>
                                    <tr>
                                        <th>S.No.</th>
                                        <th>Roll Number</th>
                                        <th>Name</th>
                                        <th>Degree</th>
                                        <th>Year</th>
                                    </tr>");
                counter = 1;
                foreach (var students in AppliedStudentList)
                {
                    sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                  </tr>", counter, students.RollNumber, students.FullName, students.Degree, students.Year);
                    counter++;
                }
                sb.Append(@"
                                </table>");
                sb.Append(@"
                            </body>
                        </html>");


                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = CurrentCourse.CourseCode+": "+CurrentCourse.CourseTitle+" - "+CurrentCourse.CourseSemester+" Semester "+CurrentCourse.CourseYear
                    //Out = @"D:\PDFCreator\Employee_Report.pdf"
                };

                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = sb.ToString(),
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = CurrentCourse.CourseCode + ": " + CurrentCourse.CourseTitle + " - " + CurrentCourse.CourseSemester + " Semester " + CurrentCourse.CourseYear },
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