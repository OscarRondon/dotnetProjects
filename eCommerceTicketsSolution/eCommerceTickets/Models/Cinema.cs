using eCommerceTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eCommerceTickets.Models
{
    public class Cinema: IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Logo")]
        public string Logo { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Decrption")]
        public string Description { get; set; }

        //Relationships
        public IEnumerable<Movie> Movies { get; set; }
    }
}
