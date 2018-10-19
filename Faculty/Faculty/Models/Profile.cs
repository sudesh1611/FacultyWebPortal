using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Models
{
    public class Profile
    {
        public int ID { get; set; }

        [Required(ErrorMessage = " Full Name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = " Designation is required")]
        public string Designation { get; set; }

        [Required(ErrorMessage = " Department is required")]
        public string Department { get; set; }

        [Required(ErrorMessage = " College is required")]
        public string College { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "Entered Email Address is not valid")]
        public string LoginEmailID { get; set; }

        [Required(ErrorMessage = " Email is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "Entered Email Address is not valid")]
        public string EmailID { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MaxLength(10, ErrorMessage = "Entered Mobile Number is not Valid")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered Mobile Number format is not valid")]
        public string MobileNumber { get; set; }

        public string GoogleScholarLink { get; set; }

        public string UnderGraduateDegreeDetails { get; set; }

        public string PostGraduateDegreeDetails { get; set; }

        public string DoctoratesDegreeDetails { get; set; }

        [Required(ErrorMessage = " Areas of Interest is required")]
        public string AreasOfInterest { get; set; }

        public string Achievements { get; set; }

        public string ShortBio { get; set; }

        public string ProfilePic { get; set; }

        public string ProfessionalExperience { get; set; }

    }
}
