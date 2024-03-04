using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eCommerceShared
{
    public class ProductVariant
    {
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [JsonIgnore]

        public Product Product { get; set; }

        public int ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public ProductType ProductType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginalPrice { get; set; } = 0;

        public bool Visible { get; set; } = true;

        public bool Deleted { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
