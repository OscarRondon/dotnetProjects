using eCommerceTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eCommerceTickets.Models
{
    public class Actor: IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is require")]
        public string ProfilePicture { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage ="Full Name is require")]
        [StringLength(50, MinimumLength = 3, ErrorMessage ="Full Name must be between 3 and 50 chars")]
        public string FullName { get; set; }

        [Display(Name = "Biography")]
        public string Bio { get; set; }

        //Relationships
        public IEnumerable<Actor_Movie> ?ActorsMovies { get; set; }
    }
}
