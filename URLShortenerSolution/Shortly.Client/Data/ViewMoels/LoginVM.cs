using System.ComponentModel.DataAnnotations;

namespace Shortly.Client.Data.ViewMoels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email address is required")]
        //[EmailAddress(ErrorMessage ="Invalid Email address")]
        [RegularExpression(@"^\S+@\S+\.\S+$", ErrorMessage = "Invalid Email address")]
        //[CustomEmailValidator(ErrorMessage = "Email address is not valid (custom)")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters")]
        public string EmailPassword { get; set; }
    }
}
