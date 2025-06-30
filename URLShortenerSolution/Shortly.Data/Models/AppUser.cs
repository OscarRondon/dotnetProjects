using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Data.Models
{
    public class AppUser: IdentityUser  // Inherits from IdentityUser to leverage ASP.NET Core Identity features
    {
        public string? FullName { get; set; }
        public List<Url> Urls { get; set; }

        public AppUser()
        {
            Urls = new List<Url>();
        }
    }
}
