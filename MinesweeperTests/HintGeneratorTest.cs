using System.Linq;
using Minesweeper;
using Xunit;

namespace MinesweeperTests
{
    public class HintGeneratorTest
    {
        [Fact]
        public void SetHintsShould_setHintTo0_WhenThereIsOnly1MineSquare()
        {
            var mineGenerator = new RandomMinesGenerator();
            var board = Board.CreateEmptyBoard(1);
            mineGenerator.PlaceMines(1, board);

            HintGenerator.SetHints(board);
            foreach (var item in board.Squares)
            {
                Assert.Equal(0,item.Hint);
            }
            
        }
        
        [Fact]
        public void SetHintsShould_setHintTo0_WhenThereIs1Square()
        {
            var mineGenerator = new RandomMinesGenerator();
            var board = Board.CreateEmptyBoard(1);
            mineGenerator.PlaceMines(0, board);
        
            HintGenerator.SetHints(board);
            foreach (var item in board.Squares)
            {
                Assert.Equal(0,item.Hint);
            }
        }
        
        [Fact]
        public void SetHintsShould_setHintsTo0_WhenThereIsNoMine_InASize2Board()
        {
            var mineGenerator = new RandomMinesGenerator();
            var board = Board.CreateEmptyBoard(2);
            mineGenerator.PlaceMines(0, board);
            HintGenerator.SetHints(board);
            
            foreach (var item in board.Squares)
            {
                Assert.Equal(0, item.Hint);
            }
        }
        
        [Fact]
        public void SetHintsShould_Set8HintsWithValue1_WhenThereIs1Mine_InTheMiddleOfASize3Board()
        {
            var board = Board.CreateEmptyBoard(3);
            var centerLocation = new Location(1,1);
            var square = board.GetSquare(centerLocation);
            square.SetMine();
            
            HintGenerator.SetHints(board);

            var squaresWithHintValueOne = board.Squares.Where(item => item.Hint == 1);
            Assert.Equal(8, squaresWithHintValueOne.Count());
        }
        
        [Fact]
        public void SetHintsShould_Set3HintsWithValue1_WhenThereIs1Mine_InTheTopLeftCornerOfASize3Board()
        {
            var board = Board.CreateEmptyBoard(3);
            var topLeft = new Location(0,0);
            var square = board.GetSquare(topLeft);
            square.SetMine();
            
            HintGenerator.SetHints(board);

            var squaresWithHintValueOne = board.Squares.Where(item => item.Hint == 1);
            var squaresWithHintValueZero = board.Squares.Where(item => item.Hint == 0);
            Assert.Equal(3, squaresWithHintValueOne.Count());
            Assert.Equal(6, squaresWithHintValueZero.Count());
        }
        
        [Fact]
        public void SetHintsShould_Set3HintsWithValue3_WhenThereAre2Mines_InTheTopLineOfASize3Board()
        {
            var board = Board.CreateEmptyBoard(3);
            var topLeft = new Location(0,0);
            var topLeftSquare = board.GetSquare(topLeft);
            topLeftSquare.SetMine();
            var topRight = new Location(0,1);
            var topRightSquare = board.GetSquare(topRight);
            topRightSquare.SetMine();
            
            HintGenerator.SetHints(board);

            var squaresWithHintValueTwo = board.Squares.Where(item => item.Hint == 2);
            Assert.Equal(2, squaresWithHintValueTwo.Count());
        }
    }
}