using eCommerceTickets.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceTicketsTest.Mocks.Tests
{
    public class MovieServiceTest
    {
        public MovieServiceTest()
        {
            
        }

        [Fact]
        [Trait("Mock", "MovieService")]
        public void MovieServcie_GetMovieById()
        {
            // Arrange
            var dbContext = new Mock<AppDbContext>();

            // Act

            // Assert
        }
    }
}
