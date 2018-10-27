using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Faculty.Data;
using Faculty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Faculty.Pages
{
    public class EditProfileModel : PageModel
    {
        
        public class TempProfile
        {
            [Required(ErrorMessage = " Full Name is required")]
            public string FullName { get; set; }

            [Required(ErrorMessage = " Designation is required")]
            public string Designation { get; set; }

            [Required(ErrorMessage = " Department is required")]
            public string Department { get; set; }

            [Required(ErrorMessage = " College is required")]
            public string College { get; set; }

            [Required(ErrorMessage = " Email is required")]
            [DataType(DataType.EmailAddress)]
            [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "Entered Email Address is not valid")]
            public string EmailID { get; set; }

            [DataType(DataType.PhoneNumber)]
            [MaxLength(10, ErrorMessage = "Entered Mobile Number is not Valid")]
            [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered Mobile Number format is not valid")]
            public string MobileNumber { get; set; }

            [Required(ErrorMessage = " This field is required")]
            public string GoogleScholarLink { get; set; }

            [Required(ErrorMessage = " Areas of Interest are required")]
            public string AreasOfInterest { get; set; }

            [Required(ErrorMessage = " Achievements are required")]
            public string Achievements { get; set; }

            [Required(ErrorMessage = " Short Bio is required")]
            public string ShortBio { get; set; }

            public string ProfilePic { get; set; }

            [Required(ErrorMessage = " This field is required")]
            public string ProfessionalExperience { get; set; }

            [Required(ErrorMessage = " This field is required")]
            public string UGDegree { get; set; }

            [Required(ErrorMessage = " This field is required")]
            public string UGCollege { get; set; }

            [Required(ErrorMessage = " This field is required")]
            public string UGCompletionYear { get; set; }

            [Required(ErrorMessage = " This field is required")]
            public string PGDegree { get; set; }

            [Required(ErrorMessage = " This field is required")]
            public string PGCollege { get; set; }

            [Required(ErrorMessage = " This field is required")]
            public string PGCompletionYear { get; set; }

            [Required(ErrorMessage = " This field is required")]
            public string PhDCollege { get; set; }

            [Required(ErrorMessage = " This field is required")]
            public string PhDCompletionYear { get; set; }

        }

        [BindProperty]
        public TempProfile MyProfile { set; get; }

        public Profile CurrentProfile { set; get; }

        private readonly ProfileDbContext profileDbContext;

        public EditProfileModel(ProfileDbContext pDbContext)
        {
            profileDbContext = pDbContext;
            CurrentProfile = new Profile();
        }

        public async Task OnGetAsync()
        {
            CurrentProfile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.ID == 1);
            MyProfile = new TempProfile();
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                var Profile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.LoginEmailID == emaiID);
                MyProfile.ProfilePic = Profile.ProfilePic;
                MyProfile.College = Profile.College;
                MyProfile.Department = Profile.Department;
                MyProfile.Designation = Profile.Designation;
                MyProfile.EmailID = Profile.EmailID;
                MyProfile.Achievements = Profile.Achievements;
                MyProfile.AreasOfInterest = Profile.AreasOfInterest;
                MyProfile.FullName = Profile.FullName;
                MyProfile.GoogleScholarLink = Profile.GoogleScholarLink;
                MyProfile.MobileNumber = Profile.MobileNumber;
                MyProfile.PGCollege = String.Empty;
                MyProfile.PGCompletionYear = String.Empty;
                MyProfile.PGDegree = String.Empty;
                MyProfile.PhDCollege = String.Empty;
                MyProfile.PhDCompletionYear = String.Empty;
                MyProfile.ProfessionalExperience = Profile.ProfessionalExperience;
                MyProfile.ProfilePic = Profile.ProfilePic;
                MyProfile.ShortBio = Profile.ShortBio;
                MyProfile.UGCollege = String.Empty; ;
                MyProfile.UGCompletionYear = String.Empty;
                MyProfile.UGDegree = String.Empty;
                if (String.IsNullOrEmpty(Profile.ProfilePic))
                {
                    MyProfile.ProfilePic = "images/profile-pic.png";
                }
                if (!String.IsNullOrEmpty(Profile.UnderGraduateDegreeDetails))
                {
                    var tempString1 = Profile.UnderGraduateDegreeDetails.Split(';');
                    var tempString2 = Profile.PostGraduateDegreeDetails.Split(';');
                    var tempString3 = Profile.DoctoratesDegreeDetails.Split(';');
                    MyProfile.UGDegree = tempString1[0];
                    MyProfile.UGCollege = tempString1[1];
                    MyProfile.UGCompletionYear = tempString1[2];
                    MyProfile.PGDegree = tempString2[0];
                    MyProfile.PGCollege = tempString2[1];
                    MyProfile.PGCompletionYear = tempString2[2];
                    MyProfile.PhDCollege = tempString3[0];
                    MyProfile.PhDCompletionYear = tempString3[1];
                }
                if (!String.IsNullOrEmpty(Profile.AreasOfInterest))
                {
                    var tempList = JsonConvert.DeserializeObject<List<string>>(Profile.AreasOfInterest);
                    MyProfile.AreasOfInterest = String.Empty;
                    for (int i = 0; i <= tempList.Count - 2; i++)
                    {
                        MyProfile.AreasOfInterest = MyProfile.AreasOfInterest + tempList[i] + ";";
                    }
                    MyProfile.AreasOfInterest = MyProfile.AreasOfInterest + tempList[tempList.Count - 1];
                }
                if (!String.IsNullOrEmpty(Profile.Achievements))
                {
                    var tempList = JsonConvert.DeserializeObject<List<string>>(Profile.Achievements);
                    MyProfile.Achievements = String.Empty;
                    for (int i = 0; i <= tempList.Count - 2; i++)
                    {
                        MyProfile.Achievements = MyProfile.Achievements + tempList[i] + ";";
                    }
                    MyProfile.Achievements = MyProfile.Achievements + tempList[tempList.Count - 1];
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                if (ModelState.IsValid)
                {
                    string emaiID = User.FindFirst(ClaimTypes.Email).Value;
                    var Profile = await profileDbContext.Profiles.SingleOrDefaultAsync(m => m.LoginEmailID == emaiID);
                    Profile.EmailID = MyProfile.EmailID.ToLower();
                    Profile.UnderGraduateDegreeDetails = MyProfile.UGDegree + ";" + MyProfile.UGCollege + ";" + MyProfile.UGCompletionYear;
                    Profile.PostGraduateDegreeDetails = MyProfile.PGDegree + ";" + MyProfile.PGCollege + ";" + MyProfile.PGCompletionYear;
                    Profile.DoctoratesDegreeDetails = MyProfile.PhDCollege + ";" + MyProfile.PhDCompletionYear;

                    List<string> tempString = MyProfile.AreasOfInterest.Split(';').ToList();
                    Profile.AreasOfInterest = JsonConvert.SerializeObject(tempString);

                    tempString = MyProfile.Achievements.Split(',').ToList();
                    Profile.Achievements = JsonConvert.SerializeObject(tempString);

                    Profile.College = MyProfile.College;
                    Profile.Department = MyProfile.Department;
                    Profile.Designation = MyProfile.Designation;
                    Profile.EmailID = MyProfile.EmailID;
                    Profile.FullName = MyProfile.FullName;
                    Profile.GoogleScholarLink = MyProfile.GoogleScholarLink;
                    Profile.MobileNumber = MyProfile.MobileNumber;
                    Profile.ProfessionalExperience = MyProfile.ProfessionalExperience;
                    Profile.ShortBio = MyProfile.ShortBio;


                    profileDbContext.Profiles.Update(Profile);
                    await profileDbContext.SaveChangesAsync();
                    return RedirectToPage("/Index");
                }
                else
                {
                    return Page();
                }
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
    }
}