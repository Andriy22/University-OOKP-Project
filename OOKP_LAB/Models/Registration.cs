using DataAnnotationsExtensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace OOKP_LAB.Models
{
    public class Registration
    {
        [Email(ErrorMessage = "Please input coorect email")]
        [Required(ErrorMessage = "Please input email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please input birth date")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Please input password")]
        [MinLength(6, ErrorMessage = "Min length 6")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please input full name")]
        public string FullName { get; set; }
        [Compare("Password", ErrorMessage = "Password must compare")]
        public string ConfirmPassword { get; set; }
    }
}
