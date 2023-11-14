using eCommerceTickets.Data;
using eCommerceTickets.Data.Services;
using eCommerceTicketsTest.InMemory.Data;
using eCommerceTicketsTest.InMemory.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceTicketsTest.InMemory.Tests
{
    [Collection("AppDbContextFixture")]
    public class CinemaServiceTest
    {
        private AppDbContextFixture _appDbContextFixture;

        public CinemaServiceTest(AppDbContextFixture appDbContextFixture)
        {
            _appDbContextFixture = appDbContextFixture;
        }

        [Fact]
        public async void CinemaService_GetAllCinemas()
        {
            // Arrange
            CinemasServices cinemasServices = new(_appDbContextFixture.appDbContext);

            // Acts
            var cinemas = await cinemasServices.GetAll();

            // Asserts
            Assert.NotNull(cinemas);
            Assert.Equal(5, cinemas.Count());
        }


    }
}