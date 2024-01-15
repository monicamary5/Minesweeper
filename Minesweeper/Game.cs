using System.Text.Json.Serialization;
using System;

namespace Minesweeper
{
    //Have used Single Responsibility Solid Principle
    //Also achieved OOPs concepts like abstraction and encapsulation etc
    public class Game
    {
        private readonly IInput _input;
        //private readonly IInput _noOfMinesinput;
       // private readonly IInput _locationinput;
        private readonly IOutput _output;
        private readonly IGenerateMines _minesGenerator;

        //logger for log the details
        Logtrack logger = new Logtrack();
        public GameState State { get; private set; }
        public Board Board { get; private set; }
        
        public Game(IInput input, IOutput output, IGenerateMines minesGenerator)
        {
            _input = input;
            _output = output;
            _minesGenerator = minesGenerator;
            State = GameState.Unknown;
        }

        #region CreateBoard
        /*
         Create Board method is used to Create a board after input like Gride Size and number of mines 
         1. Board creation for game
         2. mines generation based on input 
         3. Displaying the generated board with mines hidden
         */

        public void CreateBoard()
        {
            try
            {
                logger.Log("Create Board");
                var gridSizeValue = SetGridSizeValue();

                //grid size value is greater than minimum value then proceed further for other inputs and board creation
                if (gridSizeValue > 0)
                {
                    var size = gridSizeValue;
                    var numberOfMinesValue = SetNoOfMinesValue(gridSizeValue);

                    if (numberOfMinesValue > 0)
                    {
                        logger.Log("Create Board numberOfMinesValue => " + numberOfMinesValue);
                        var numberOfMines = numberOfMinesValue;
                        Board = Board.CreateEmptyBoard(size);
                        _minesGenerator.PlaceMines(numberOfMines, Board);
                        HintGenerator.SetHints(Board);
                        _output.Write(GameInstruction.DisplayCurrentBoardMessage);
                        DisplayBoard();
                    }
                    else
                    {
                        //exception case
                        _output.Write(GameInstruction.SomethingWentWrong);
                    }
                }
                else
                {
                    //exception case
                    _output.Write(GameInstruction.SomethingWentWrong);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Log($"Exception occurred in CreateBoard: {ex.Message}");
            }
        }

        #endregion

        #region DisplayBoard

        //method to Display generated board in console
        private void DisplayBoard()
        {
            _output.Write(Board.ToString());
        }

        #endregion

        #region SetGridSizeValue

        /* 1. SetGridSizeValue Method is used to get the input parameters like grid Size
          2. Validating the input paramters like grid Size
       */

        private int SetGridSizeValue()
        {
            try
            {
                _output.Write(GameInstruction.WelcomeMessage); // For Welcome to minesweeper
                _output.Write(""); // for empty space 

                var gridSizeInput = _input.Ask(GameInstruction.InputGridSizeValueMessage);

                logger.Log($"GridSizeInput in SetGridSizeValue: {gridSizeInput}");

                string gridvalidationMessage = "";

                while ((gridvalidationMessage = GridSizeInputIsNotValid(gridSizeInput)) != GameInstruction.InputValidMessage)
                {
                    logger.Log($"Validation failed in SetGridSizeValue: {gridvalidationMessage}");
                    _output.Write(gridvalidationMessage);
                    gridSizeInput = _input.Ask(GameInstruction.InputGridSizeValueMessage);
                }

                var gridSizeIntValue = int.Parse(gridSizeInput);
                return gridSizeIntValue;
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Log($"Exception occurred in SetGridSizeValue: {ex.Message}");
                return 0;
            }
        }
        #endregion

        #region Grid Size input Validation

        /* Validating below Grid Size input Value and based on conditions like  
            1. Grid size input should be integer
            2. Grid size maximum value is 10 and minimum value is 2
        */

        private static string GridSizeInputIsNotValid(string gridSizeInput)
        {
            if (!InputValidator.IsValidGridSizeInput(gridSizeInput))
            {
                return GameInstruction.InputNotValidMessage;
            }
            else if(InputValidator.IsGridSizeMinimum(gridSizeInput))
            {
                return GameInstruction.MinimumSizeOfGridMessage;
            }
            else if (InputValidator.IsGridSizeMaximum(gridSizeInput))
            {
                return GameInstruction.MaximumSizeOfGridMessage;
            }
            return GameInstruction.InputValidMessage;
        }
        #endregion

        #region SetNoOfMinesValue

        /* 1. SetGridSizeValue Method is used to get the input parameters like grid Size
          2. Validating the input paramters like grid Size
       */
        private int SetNoOfMinesValue(int gridSizeValue)
        {
            try
            {
                var noOfMinesInput = _input.Ask(GameInstruction.InputNoOfMinesValueMessage);
                logger.Log($"No Of Mines Input in SetNoOfMinesValue: {noOfMinesInput}");
                string minesvalidationMessage = "";

                while ((minesvalidationMessage = NoOfMinesInputIsNotValid(gridSizeValue, noOfMinesInput)) != GameInstruction.InputValidMessage)
                {
                    logger.Log($"Validation failed in SetNoOfMinesValue: {minesvalidationMessage}");
                    _output.Write(minesvalidationMessage);
                    noOfMinesInput = _input.Ask(GameInstruction.InputNoOfMinesValueMessage);
                }

                var noOfMinesIntValue = int.Parse(noOfMinesInput);
                return noOfMinesIntValue;
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Log($"Exception occurred in SetNoOfMinesValue: {ex.Message}");
                return 0;
            }
        }
        #endregion

        #region Grid Size input Validation

        /* Validating below Grid Size input Value and based on conditions like  
           1. Grid size input should be integer
           2. Grid size maximum value is 10 and minimum value is 2
       */

        private static string NoOfMinesInputIsNotValid(int gridSizeInput, string noOfMinesInput)
        {
            if(InputValidator.IsNoOfMinesInputZero(noOfMinesInput))
            {
                return GameInstruction.AtleastOneMineMessage;
            }
            if (!InputValidator.IsValidNoOfMinesInput(noOfMinesInput))
            {
                return GameInstruction.InputNotValidMessage;
            }
            else if (InputValidator.IsNoOfMinesInputMaximum(gridSizeInput, noOfMinesInput))
            {
                return GameInstruction.MaximumNoOfMinesMessage;
            }
            return GameInstruction.InputValidMessage;
        }
        #endregion

        #region GamePlay

        //Method to validate the move and decide the Game over or Won
        public void Play()
        {
            try
            {
                while (BoardIsNotRevealed())
                {
                    var newLocation = CreateLocationBasedOnInput();

                    if (newLocation != null)
                    {

                        RevealTheSquareIfLocationIsOnBoard(newLocation);

                        if (WinLoseChecker.IsLosingConditionWhenOneMineIsRevealed(Board))
                        {
                            _output.Write(GameInstruction.GameOverMessage);
                            Board.RevealAllSquares();
                            State = GameState.Lose;
                            logger.Log($"Play GameState is => " + "Lose");
                            _output.Write(GameInstruction.ResultMessage + State);
                        }
                        else if (WinLoseChecker.IsWinningConditionWhenAllHintsAreRevealed(Board))
                        {
                            _output.Write(GameInstruction.WonMessage);
                            Board.RevealAllSquares();
                            State = GameState.Win;
                            logger.Log($"Play GameState is  => " + "Win");
                            _output.Write(GameInstruction.ResultMessage + State);
                        }

                        _output.Write(GameInstruction.DisplayCurrentBoardMessage);
                        DisplayBoard();
                    }
                    else
                    {
                        logger.Log($"Exception occurred in Play");
                        _output.Write(GameInstruction.SomethingWentWrong);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Log($"Exception occurred in Play: {ex.Message}");
            }
        }

        #endregion

        #region Reveal Board Based on Location input

        // Method to reveal the board based on location input
        private void RevealTheSquareIfLocationIsOnBoard(Location newLocation)
        {
            if (Board.HasLocation(newLocation))
            {
                Board.RevealOneSquare(newLocation);
            }
            else
            {
                _output.Write(GameInstruction.WrongLocationMessage);
            }
        }

        #endregion

        #region Create Location
        private Location CreateLocationBasedOnInput()
        {
            try
            {
                var locationInput = _input.Ask(GameInstruction.InputLocationValueMessage);
                while (LocationInputIsNotValid(locationInput))
                {
                    _output.Write(GameInstruction.InputNotValidMessage);
                    locationInput = _input.Ask(GameInstruction.InputLocationValueMessage);
                }
                logger.Log($"Method LocationBasedOnInput Method");
                var newLocation = InputParser.CreateLocationBasedOnInput(locationInput);
                return newLocation;
            }
            catch (Exception ex)
            {
                // Log the exception
                logger.Log($"Exception occurred in CreateLocationBasedOnInput: {ex.Message}");
                return null;
            }
        }

        #endregion

        #region Location Input Validation
        private static bool LocationInputIsNotValid(string locationInput)
        {
            return !InputValidator.IsValidLocationInput(locationInput);
        }
        #endregion

        #region BoardIsNotRevealed
        private bool BoardIsNotRevealed()
        {
            return !Board.IsRevealed;
        }

        #endregion

    }
}