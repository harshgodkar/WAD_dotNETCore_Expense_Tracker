using System;
using System.ComponentModel.DataAnnotations;


namespace ExpenseTracker.Models
{
    public class UserProfile
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please Enter Your First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Last Name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password")]
        [MinLength(4, ErrorMessage = "Password is too SHORT...")]
        public string Password { get; set; }
    }
}
