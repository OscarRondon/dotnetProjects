using eCommerceTickets.Data;
using eCommerceTickets.Data.Services;
using eCommerceTickets.Models;
using eCommerceTicketsTest.Mocks.Data;
using eCommerceTicketsTest.Mocks.Utilities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace eCommerceTicketsTest.Mocks.Tests
{
    public class CinemaServiceTest
    {
        [Fact]
        public async void CinemaService_GetAllCinemas()
        {
            // Arrange

            var cinemasMockData = MockData.GetMockListOfCinemas().AsQueryable();

            var cinemaMockDbSet = new Mock<DbSet<Cinema>>();
            cinemaMockDbSet.As<IAsyncEnumerable<Cinema>>().Setup(m => m.GetAsyncEnumerator(default)).Returns(new MockAsyncEnumerator<Cinema>(cinemasMockData.GetEnumerator()));
            cinemaMockDbSet.As<IQueryable<Cinema>>().Setup(m => m.Provider).Returns(new MockAsyncQueryProvider<Cinema>(cinemasMockData.Provider));
            cinemaMockDbSet.As<IQueryable<Cinema>>().Setup(m => m.Expression).Returns(cinemasMockData.Expression);
            cinemaMockDbSet.As<IQueryable<Cinema>>().Setup(m => m.ElementType).Returns(cinemasMockData.ElementType);
            cinemaMockDbSet.As<IQueryable<Cinema>>().Setup(m => m.GetEnumerator()).Returns(cinemasMockData.GetEnumerator());

            var dbContextMock = new Mock<AppDbContext>();
            dbContextMock.Setup(m => m.Set<Cinema>()).Returns(cinemaMockDbSet.Object);
            //dbContextMock.Setup(m => m.Cinemas).Returns(cinemaMockDbSet.Object);

            // Act
            CinemasServices cinemasServices = new(dbContextMock.Object);
            var cinemas = await cinemasServices.GetAll();

            // Assert
            Assert.NotNull(cinemas);
            Assert.Equal(5, cinemas.Count());
        }

    }
}
