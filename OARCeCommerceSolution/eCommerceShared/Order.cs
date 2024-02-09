using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceShared
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        [Column(TypeName = "decimal()")]
        public decimal TotalPrice {  get; set; }
        public List<OrderItem> OrderItems { get; set;}

    }
}
