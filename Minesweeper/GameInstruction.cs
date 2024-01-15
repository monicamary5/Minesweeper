namespace Minesweeper
{
    /// <summary>
    /// Mine Sweeper Application constant Values handled in one Static class
    /// </summary>
    public static class GameInstruction
    {
        public const string WelcomeMessage = "Welcome to Minesweeper!";

        public const string DisplayCurrentBoardMessage = "Here is your minefield:";

        public const string InputGridSizeValueMessage = "Enter the size of the grid (e.g. 4 for a 4x4 grid):";

        public const string InputNoOfMinesValueMessage = "Enter the number of mines to place on the grid (maximum is 35% of the total squares) :";

        public const string InputLocationValueMessage = "Select a square to reveal (e.g. A0):"; 

        public const string ResultMessage = "Result: ";

        public const string Zero = "0";

        public const string WonMessage = "Congratulations, you have won the game!";

        public const string GameOverMessage = "Oh no, you detonated a mine! Game over.";

        public const string InputNotValidMessage = "Incorrect Input !";

        public const string WrongLocationMessage = "The Location is not on the board.";

        public const string MinimumSizeOfGridMessage = "Minimum size of grid is 2.";

        public const string MaximumSizeOfGridMessage = "Maximum size of grid is 10.";

        public const string MaximumNoOfMinesMessage = "Maximum number is 35% of total squares";

        public const string AtleastOneMineMessage = "There must be at least 1 mine.";

        public const string InputValidMessage = "Input is valid.";

        public const string SomethingWentWrong = "Something went wrong.....!";
    }
}