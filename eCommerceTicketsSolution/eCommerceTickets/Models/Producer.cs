﻿using System.ComponentModel.DataAnnotations;

namespace eCommerceTickets.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        public string ProfilePicture { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }

        //Relationships
        public IEnumerable<Movie> Movies { get; set; }

    }
}
