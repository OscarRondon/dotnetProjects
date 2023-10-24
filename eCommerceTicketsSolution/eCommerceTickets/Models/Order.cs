using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceTickets.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is require")]
        public string Email { get; set; }

        [Display(Name = "UserId")]
        [Required(ErrorMessage = "UserId is require")]
        public string UserId {  get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public List<OrderItem> OrderItems { get; set; }
        
    }
}
