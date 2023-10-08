using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceTickets.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is require")]
        [Range(minimum: 1, maximum: 1000000, ErrorMessage = "The Quantity must have a value")]
        public int Quantity { get; set; }

        [Display(Name = "Ammount")]
        [Required(ErrorMessage = "Ammount is require")]
        [Range(minimum:1, maximum:1000000, ErrorMessage ="The Ammount must have a value")]
        public double Ammount { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        public Movie Movie {  get; set; }
    }
}
