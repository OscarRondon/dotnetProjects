
namespace eCommerceTicketsTest
{
    public class Example
    {
        [Fact]
        public void ExampleTest()
        {
            // Arrange

            var list = new List<string>();

            // Act
            list.Add("Test Value1");

            // Assert
            Assert.NotEmpty(list);

            var text = list.FirstOrDefault();
            Assert.Equal("Test Value1", text);
        }
    }
}