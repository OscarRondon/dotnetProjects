using eCommerceTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceTicketsTest.Mocks.Data
{
    public class MockData
    {
        public static IEnumerable<Cinema> GetMockListOfCinemas()
        {
            var cinemas = new List<Cinema>()
            {
                new Cinema()
                {
                    Id = 1,
                    Name = "Cinema 1",
                    Logo = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                    Description = "This is the description of the first cinema"
                 },
                new Cinema()
                {
                    Id = 2,
                    Name = "Cinema 2",
                    Logo = "http://dotnethow.net/images/cinemas/cinema-2.jpeg",
                    Description = "This is the description of the first cinema"
                },
                new Cinema()
                {
                    Id = 3,
                    Name = "Cinema 3",
                    Logo = "http://dotnethow.net/images/cinemas/cinema-3.jpeg",
                    Description = "This is the description of the third cinema"
                },
                new Cinema()
                {
                    Id = 4,
                    Name = "Cinema 4",
                    Logo = "http://dotnethow.net/images/cinemas/cinema-4.jpeg",
                    Description = "This is the description of the first cinema"
                },
                new Cinema()
                {
                    Id = 5,
                    Name = "Cinema 5",
                    Logo = "http://dotnethow.net/images/cinemas/cinema-5.jpeg",
                    Description = "This is the description of the first cinema"
                }
            };

            return cinemas;
        }
    }
}
