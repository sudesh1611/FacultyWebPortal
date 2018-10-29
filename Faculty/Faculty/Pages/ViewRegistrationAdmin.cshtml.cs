using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Faculty.Data;
using Faculty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Faculty.Pages
{
    public class ViewRegistrationAdminModel : PageModel
    {
        private readonly CourseDbContext courseDbContext;
        private readonly ProfileDbContext profileDbContext;
        private readonly StudentDbContext studentDbContext;
        public Profile CurrentProfile { set; get; }

        public Course CurrentCourse { set; get; }

        public List<Student> ApprovedStudentList { get; set; }
        public List<Student> DeclinedStudentList { get; set; }
        public List<Student> AppliedStudentList { get; set; }


        public ViewRegistrationAdminModel(CourseDbContext cdb,ProfileDbContext pdb,StudentDbContext sdb)
        {
            courseDbContext = cdb;
            profileDbContext = pdb;
            studentDbContext = sdb;
            CurrentCourse = new Course();
            CurrentProfile = new Profile();
            ApprovedStudentList = new List<Student>();
            DeclinedStudentList = new List<Student>();
            AppliedStudentList = new List<Student>();
        }

        public async Task OnGetAsync(int?id)
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            int ID = (int)id;
            if(User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                var StudentList = await studentDbContext.Students.ToListAsync();
                CurrentCourse = await courseDbContext.Courses.SingleOrDefaultAsync(m => m.ID == ID);
                if(CurrentCourse!=null)
                {
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
                }
            }
        }
    }
}