using eCommerceTickets.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceTicketsTest.InMemory.Tests
{
    public class MovieServiceTest
    {
        private static DbContextOptions<AppDbContext> dbContextptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("eCommerceTicketsTest").Options;

        [Fact]
        public void MovieServcie_GetMovieById()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
