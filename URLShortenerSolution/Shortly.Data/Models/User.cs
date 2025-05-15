using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string Email { get; set; }
        public List<Url> Urls { get; set; }

        public User()
        {
            Urls = new List<Url>();
        }
    }
}
