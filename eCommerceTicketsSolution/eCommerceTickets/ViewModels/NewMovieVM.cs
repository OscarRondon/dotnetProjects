using eCommerceTickets.Models.Enums;
using eCommerceTickets.Models;
using eCommerceTickets.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eCommerceTickets.ViewModels
{
    public class NewMovieVM
    {

        [Display(Name = "Movie Name")]
        [Required(ErrorMessage ="Movie Name is require")]
        public string Name { get; set; }

        [Display(Name = "Movie Description")]
        [Required(ErrorMessage = "Movie Description is require")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        public double Price { get; set; }

        [Display(Name = "Movie Poster")]
        public string Image { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Category")]
        public MovieCategory MovieCategory { get; set; }

        //Relationships
        [Display(Name = "Select Actor(s)")]
        public IEnumerable<int> ActorIds { get; set; }

        [Display(Name = "Select a Cinema")]
        public int CinemaId { get; set; }

        [Display(Name = "Select a Producer")]
        public int ProducerId { get; set; }
    }
}
