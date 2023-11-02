using eCommerceTickets.Data;
using eCommerceTickets.Data.Services;
using eCommerceTickets.Models;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceTicketsTest.Mocks
{
    public class CinemaServiceTest
    {
        [Fact]
        public async void CinemaService_GetAllCinemas()
        {
            // Arrange
            var cinemasMock = MockData.GetMockListOfCinemas().AsQueryable().BuildMockDbSet();
            var dbContextMock = new Mock<AppDbContext>();
            dbContextMock.Setup(m => m.Cinemas).Returns(cinemasMock.Object);

            // Act
            CinemasServices cinemasServices = new(dbContextMock.Object);
            var cinemas = await cinemasServices.GetAll();

            // Assert
            Assert.NotNull(cinemas);
            Assert.Equal(5, cinemas.Count());
        }

    }
}
