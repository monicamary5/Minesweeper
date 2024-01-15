using Minesweeper;
using Xunit;

namespace MinesweeperTests
{
    public class GameTest
    {
         [Fact]
         public void SetUpShould_CreateA4by4HiddenBoard_WhenInputDifficultyLevelIs4()
         {
             const string gridSizeLevelInput = "4";
             const string noOfMinesInput = "4";
             var input = new MockInput(new[]{ gridSizeLevelInput, noOfMinesInput });
             var output = new MockOutput();
             var minesGenerator = new MockMinesGenerator();
             var game = new Game(input, output, minesGenerator);
             game.CreateBoard();
             var result = game.Board.ToString();
             const string expectedResult = "  1 2 3 4 \n" +
                                           "A . . . . \n" + 
                                           "B . . . . \n" + 
                                           "C . . . . \n" + 
                                           "D . . . . \n";
             
             Assert.Equal(expectedResult, result);
         }        
         
         [Fact]
         public void GameShould_RevealEntireBoardAndWinTheGame_WhenInputLocationMatchesAllHintLocations()
         {
             const string gridSizeLevelInput = "3";
             const string noOfMinesInput = "3";
             var input = new MockInput(new[]{ gridSizeLevelInput, noOfMinesInput, "B0", "B1", "B2", "C1", "C2", "C0"});
             var output = new MockOutput();
             var minesGenerator = new MockMinesGenerator();
             var game = new Game(input, output, minesGenerator);
             game.CreateBoard();
             game.Play();
             var result = game.Board.ToString();
            const string expectedResult = "  1 2 3 \n" +
                                          "A * * * \n" +
                                          "B 2 3 2 \n" +
                                          "C 0 0 0 \n";
             Assert.Equal(expectedResult, result);
             Assert.Equal(GameState.Win, game.State);
         }
         
         [Fact]
         public void GameShould_RevealEntireBoardAndLoseTheGame_WhenInputLocationMatchesMineLocation()
         {
             const string gridSizeLevelInput = "4";
             const string noOfMinesInput = "4";
             var input = new MockInput(new[]{ gridSizeLevelInput, noOfMinesInput,"A0" });
             var output = new MockOutput();
             var minesGenerator = new MockMinesGenerator();
             var game = new Game(input, output, minesGenerator);
             game.CreateBoard();
             game.Play();
             var result = game.Board.ToString();
             const string expectedResult = "  1 2 3 4 \n" +
                                           "A * * * * \n" +
                                           "B 2 3 3 2 \n" +
                                           "C 0 0 0 0 \n" +
                                           "D 0 0 0 0 \n";

             
             Assert.Equal(expectedResult, result);
             Assert.Equal(GameState.Lose, game.State);
         }
    }
}