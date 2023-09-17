using System.ComponentModel.DataAnnotations;

namespace eCommerceTickets.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }
        public string Lgo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Relationships
        public IEnumerable<Movie> Movies { get; set; }
    }
}
