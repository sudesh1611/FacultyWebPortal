using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Models
{
    public class Student
    {
        public int ID { get; set; }

        [Required(ErrorMessage = " Full Name is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = " Email is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = "Entered Email Address is not valid")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = " Roll Number is required")]
        public string RollNumber { get; set; }

        [Required(ErrorMessage = " Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = " This field is required")]
        public string Degree { get; set; }

        [Required(ErrorMessage = " This field is required")]
        public string Year { get; set; }

    }
}
