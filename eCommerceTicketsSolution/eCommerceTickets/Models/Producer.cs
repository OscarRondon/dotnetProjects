﻿using eCommerceTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eCommerceTickets.Models
{
    public class Producer : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Profile Picture")]
        public string ProfilePicture { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Biography")]
        public string Bio { get; set; }

        //Relationships
        public IEnumerable<Movie> Movies { get; set; }

    }
}
