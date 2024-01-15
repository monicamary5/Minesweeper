using Minesweeper;
using Xunit;

namespace MinesweeperTests
{
    public class InputParserTest
    {
        [Theory]
        [InlineData(1,1, "B1")]
        [InlineData(2,1, "C1")]
        public void CreateLocationBasedOnInputShould_ReturnLocationWithCorrectPropertyValue_BasedOnInput(int xValue, int yValue, string input)
        {
            var result = InputParser.CreateLocationBasedOnInput(input);
            Assert.Equal(xValue, result.X);
            Assert.Equal(yValue, result.Y);
        }
    }
}