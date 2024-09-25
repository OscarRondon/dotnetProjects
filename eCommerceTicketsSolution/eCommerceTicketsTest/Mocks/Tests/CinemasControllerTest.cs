

using eCommerceTickets.Controllers;
using eCommerceTickets.Data;
using eCommerceTickets.Data.Services;
using eCommerceTickets.Models;
using eCommerceTicketsTest.Mocks.Data;
using Microsoft.AspNetCore.Mvc;
using MockQueryable;
using MockQueryable.Moq;
using Moq;

namespace eCommerceTicketsTest.Mocks.Tests
{
    public class CinemasControllerTest
    {
        private readonly Mock<AppDbContext> _dbContext;
        private readonly CinemasServices _service;
        public CinemasControllerTest()
        {
            _dbContext = GetMockDbContext();
            _service = new CinemasServices(_dbContext.Object);
        }
        [Fact]
        public async void GET_Index()
        {
            //Arrange
            var _cinemasController = new CinemasController(_service);

            // Act
            var result = await _cinemasController.Index();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Cinema>>((result as ViewResult).ViewData.Model);
            Assert.Equal(5, model.Count());

        }

        [Fact]
        public async void POST_Edit()
        {
            //Arrange
            var _cinemasController = new CinemasController(_service);

            // Act
            var result = await _cinemasController.Edit(3, new Cinema() {Name = "Test"});

            // Assert
            Assert.NotNull(result);

        }

        private Mock<AppDbContext> GetMockDbContext()
        {
            var cinemasMockData = MockData.GetMockListOfCinemas().BuildMock().BuildMockDbSet();

            var dbContextMock = new Mock<AppDbContext>();
            dbContextMock.Setup(m => m.Set<Cinema>()).Returns(cinemasMockData.Object);
            dbContextMock.Setup(m => m.Update<Cinema>(It.IsAny<Cinema>()));

            return dbContextMock;
        }
        
    }
}
