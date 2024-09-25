using eCommerceTickets.Data;
using eCommerceTickets.Data.Services;
using eCommerceTickets.Models;
using eCommerceTicketsTest.Mocks.Data;
using eCommerceTicketsTest.Mocks.Utilities;
using Microsoft.EntityFrameworkCore;
using MockQueryable;
using MockQueryable.Moq;
using Moq;
using Xunit.Abstractions;

namespace eCommerceTicketsTest.Mocks.Tests
{
    public class CinemaServiceTest
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public CinemaServiceTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;

            _testOutputHelper.WriteLine("Constructor");
        }

        [Fact]
        [Trait("Mock", "CinemaService")]
        public async void CinemaService_GetAllCinemas()
        {
            _testOutputHelper.WriteLine("CinemaService_GetAllCinemas");

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

        [Fact]
        [Trait("Mock", "CinemaService")]
        public async void CinemaService_GetCinemaById()
        {
            //This methos uses MockQueryable.Moq
            _testOutputHelper.WriteLine("CinemaService_GetCinemaById");

            // Arrange
            var cinemasMockData = MockData.GetMockListOfCinemas().BuildMock().BuildMockDbSet();
            var dbContextMock = new Mock<AppDbContext>();
            dbContextMock.Setup(m => m.Set<Cinema>()).Returns(cinemasMockData.Object);


            // Act
            CinemasServices cinemasServices = new(dbContextMock.Object);
            var cinema = await cinemasServices.GetById(3);

            // Assert
            Assert.NotNull(cinema);
            Assert.Equal("Cinema 3", cinema.Name);
        }

    }
}
