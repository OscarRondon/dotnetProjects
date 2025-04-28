using System.ComponentModel.DataAnnotations;

namespace Shortly.Client.Data.ViewMoels
{
    public class PostUrlVM
    {
        [Required(ErrorMessage = "URL is required")]
        [RegularExpression("^http(s)?://([\\w-]+.)+[\\w-]+(/[\\w- ./?%&=])?$", ErrorMessage = "The URL format is not valid.")]
        public string Url { get; set; }
    }
}
