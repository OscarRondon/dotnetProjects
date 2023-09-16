using System.ComponentModel.DataAnnotations;

namespace eCommerceTickets.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        public string ProfilePicture { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }
    }
}
