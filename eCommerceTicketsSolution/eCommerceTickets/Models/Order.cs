using System.ComponentModel.DataAnnotations;

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

        public List<OrderItem> OrderItems { get; set; }
        
    }
}
