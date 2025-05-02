using System.ComponentModel.DataAnnotations;

namespace Shortly.Client.Data.ViewMoels
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="The full name is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email address is required")]
        [RegularExpression(@"^\S+@\S+\.\S+$", ErrorMessage = "Invalid Email address")]
        //[CustomEmailValidator(ErrorMessage = "Email address is not valid (custom)")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
