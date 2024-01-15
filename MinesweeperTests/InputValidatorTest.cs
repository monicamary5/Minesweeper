using Minesweeper;
using Xunit;

namespace MinesweeperTests
{
    public class InputValidatorTest
    {
        [Theory]
        [InlineData("10")]
        [InlineData("3")]
        public void IsValidGridSizeInputShould_ReturnTrue_WhenInputIsAnIntegerLargerThan2AndLessThan10(string input)
        {
            var isInteger = InputValidator.IsValidGridSizeInput(input);
            var isNotMinimumSize = InputValidator.IsGridSizeMinimum(input);
            var isNotMaximumSize = InputValidator.IsGridSizeMaximum(input);
            var result = isInteger && !isNotMinimumSize && !isNotMaximumSize;
            Assert.True(result);
        }

        [Theory]
        [InlineData("11")]
        [InlineData("100")]
        [InlineData("1000")]
        public void IsValidGridSizeInputShould_ReturnFalse_WhenInputIsAnIntegerAndGreaterThan10(string input)
        {
            var result = !InputValidator.IsGridSizeMaximum(input);
            Assert.False(result);
        }

        [Theory]
        [InlineData("0")]
        [InlineData("-1")]
        [InlineData("-100")]
        public void IsValidGridSizeInputShould_ReturnFalse_WhenInputIsAnIntegerLessOrEqualTo2(string input)
        {
            var result = !InputValidator.IsGridSizeMinimum(input);
            Assert.False(result);
        }
        
        [Theory]
        [InlineData("Lan")]
        [InlineData("0,0")]
        [InlineData("00,0")]
        [InlineData("1.2")]
        public void IsValidGridSizeInputShould_ReturnFalse_WhenInputIsNotAnInteger(string input)
        {
            var result = InputValidator.IsValidGridSizeInput(input);
            Assert.False(result);
        }

        [Theory]
        [InlineData("var")]
        [InlineData("19.0")]
        public void IsValidNoOfMinesInputShould_ReturnFalse_WhenInputIsNotAnInteger(string input)
        {
            var result = InputValidator.IsValidNoOfMinesInput(input);
            Assert.False(result);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("3")]
        public void IsValidNoOfMinesInputShould_ReturnTrue_WhenInputIsAnInteger(string input)
        {
            var result = InputValidator.IsValidNoOfMinesInput(input);
            Assert.True(result);
        }

        [Theory]
        [InlineData("0")]
        public void IsValidNoOfMinesInputShould_ReturnTrue_WhenInputIsZero(string input)
        {
            var result = InputValidator.IsNoOfMinesInputZero(input);
            Assert.True(result);
        }

        [Theory]
        [InlineData(4,"7")]
        [InlineData(3,"9")]
        public void IsNoOfMinesInputMaximumShould_ReturnTrue_WhenMineInputMaxOf35Per(int gridSize,string input)
        {
            var result = InputValidator.IsNoOfMinesInputMaximum(gridSize,input);
            Assert.True(result);
        }

        [Theory]
        [InlineData(4, "4")]
        [InlineData(3, "3")]
        public void IsNoOfMinesInputMaximumShould_ReturnFalse_WhenMineInputMaxOf35Per(int gridSize, string mineinput)
        {
            var result = InputValidator.IsNoOfMinesInputMaximum(gridSize, mineinput);
            Assert.False(result);
        }

        [Theory]
        [InlineData("A0")]
        [InlineData("B1")]
        [InlineData("C2")]
        public void IsValidLocationInputShould_ReturnTrue_WhenInputIsFormatIsCorrect(string input)
        {
            var result = InputValidator.IsValidLocationInput(input);
            Assert.True(result);
        }
        
        [Theory]
        [InlineData("0")]
        [InlineData("5.5")]
        [InlineData("7&28")]
        [InlineData("1,2")]
        public void IsValidLocationInputShould_ReturnFalse_WhenInputFormatIsWrong(string input)
        {
            var result = InputValidator.IsValidLocationInput(input);
            Assert.False(result);
        }
    }
}