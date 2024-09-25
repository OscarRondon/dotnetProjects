using Xunit;
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

        [Fact]
        [Trait("Mock", "CinemaService")]
        public void ExampleTest_With_Group_Attribute()
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

        /*
         * In this scenario the test it will runs 2 times each for inline data attribute
         */
        [Theory]
        [InlineData(1, true)]
        [InlineData(2, false)]
        public void ExampleTest_With_Multiple_Inputs_Attribute(int value, bool expected)
        {
            // Arrange

            var list = new List<string>();

            // Act
            var result = value == 1 ? true : false;
            // Assert
            Assert.Equal(expected, result);
        }

        /*
         * In this scenario the test it will runs 2 times each for shareble data
         */
        [Theory]
        [MemberData(nameof(TestDataShared.TupleData), MemberType = typeof(TestDataShared))]
        public void ExampleTest_With_Multiple_Inputs_Attribute_Shared_Data(int value, bool expected)
        {
            // Arrange

            var list = new List<string>();

            // Act
            var result = value == 1 ? true : false;
            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(TestDataShared.ExternalTupleData), MemberType = typeof(TestDataShared))]
        public void ExampleTest_With_Multiple_Inputs_Attribute_Shared_ExternalData(int value, bool expected)
        {
            // Arrange

            var list = new List<string>();

            // Act
            var result = value == 1 ? true : false;
            // Assert
            Assert.Equal(expected, result);
        }
    }

    public static class TestDataShared
    {
        public static IEnumerable<object[]> TupleData
        {
            get
            {
                yield return new object[] { 1, true };
                yield return new object[] { 2, false };
            }
        }

        public static IEnumerable<object[]> ExternalTupleData
        {
            get
            {
                var allLines = eCommerceTicketsTest.Properties.Resources.ExternalTestData.Split('\n');
                var splited = allLines.Select(x => 
                { 
                    var lineSplit = x.Split(','); 
                    return new object[] { int.Parse(lineSplit[0]), bool.Parse(lineSplit[1]) }; 
                });
                return splited;
            }
        }
    }
}