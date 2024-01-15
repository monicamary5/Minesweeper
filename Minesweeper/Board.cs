using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweeper
{
    //We have achieved the Polymorphism oops concept called Method Overidding in this class  
    public class Board
    {
        public int Length { get; }
        public IList<Square> Squares { get; }
        public bool IsRevealed { get; private set; }

        /// <summary>
        /// logger for log the details
        /// </summary>
        Logtrack logger = new Logtrack();

        #region Board Private Constructor

        /// <summary>
        /// Constructor stores the length(grid size) for accessing this value by entire class
        /// create squares based on grid size length input value.
        /// </summary>
        /// <param name="length"></param>

        private Board(int length)
        {
            Length = length;
            Squares = CreateSquares();
            IsRevealed = false;
        }

        #endregion

        #region Create Squares

        /// <summary>
        /// This method is used to create squares using X and Y Values Based on Grid Size length
        /// </summary>
        private IList<Square> CreateSquares()
        {
            var squares = new List<Square>();

            if (Length == 0)
            {
                squares = null;
            }
            else
            {
                for (var xValue = 0; xValue < Length; xValue++)
                {
                    for (var yValue = 0; yValue < Length; yValue++)
                    {
                        var location = new Location(xValue, yValue);
                        var square = new Square(location);
                        squares.Add(square);
                    }
                }
            }
            return squares;
        }
        #endregion

        #region Create Empty Board

        /// <summary>
        /// this method is to create Empty board after first input value for Grid Size, No of Mines received from User
        /// </summary>
        
        public static Board CreateEmptyBoard(int size)
        {
            return new Board(size);
        }

        #endregion

        #region Reveal All Squares

        /// <summary>
        /// Method is used to reveal all squares based on Win or Lose
        /// </summary>
        
        public void RevealAllSquares()
        {
            foreach (var square in Squares)
            {
                square.IsRevealed = true;
            }
        
            IsRevealed = true;
        }

        #endregion

        #region Reveal One Square
        /// <summary>
        /// Method gets executed based on user location input to reveal the square
        /// </summary>
        /// <param name="location"></param>
        public void RevealOneSquare(Location location)
        {
            var square = GetSquare(location);
            logger.Log("RevealOneSquare X Value => " + square.Location.X + " RevealOneSquare Y Value => " + square.Location.Y + " isRevealed => " + square.IsRevealed + "isMine =>  "+ square.IsMine);
            square.IsRevealed = true;
        }

        #endregion

        #region Get Square
        /// <summary>
        /// Method is used for retriving the exact square based on location for revealing to user
        /// </summary>
        /// <param name="location"></param>
        /// <returns>Square</returns>
        public Square GetSquare(Location location)
        {
            var square = Squares.SingleOrDefault(item => item.Location.Equals(location));
            return square;
        }
        #endregion

        #region ToString Method 
        /// <summary>
        /// Inbuild toString method has been overidden and customize it based on requirement
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var message = " ";
            message += GenerateYAxisHeader();
            message += "\n";
            for (var xValue = 0; xValue < Length; xValue++)
            {
                message += XAxisAlphabetInBoard(xValue);
                for (var yValue = 0; yValue < Length; yValue++)
                {
                    var location = new Location(xValue, yValue);
                    message += GetSquare(location).ToString();
                    message += " ";
                }
        
                message += "\n";
            }

            return message;
        }

        #endregion

        #region Get Neighbours
        public IEnumerable<Square> GetNeighbours(Square square)
        {
            var squareX = square.Location.X;
            var squareY = square.Location.Y;

            var neighbours = GetNeighboursWithinThreeByThreeAreaToList(squareX, squareY);
            return neighbours;
        }
        #endregion

        #region Get Neighbours Within Three By Three Area ToList
        /// <summary>
        /// based on each mine square getting the neighbours within 3x3 matrix area
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        private IEnumerable<Square> GetNeighboursWithinThreeByThreeAreaToList(int centerX, int centerY)
        {
            try
            {
                var neighbours = new List<Square>();

                for (var deltaX = -1; deltaX <= 1; deltaX++)
                {
                    for (var deltaY = -1; deltaY <= 1; deltaY++)
                    {
                        if (deltaX == 0 && deltaY == 0) continue;
                        var neighbourXValue = centerX + deltaX;
                        var neighbourYValue = centerY + deltaY;
                        var neighbourLocation = new Location(neighbourXValue, neighbourYValue);

                        if (HasLocation(neighbourLocation))
                        {
                            var neighbour = GetSquare(neighbourLocation);
                            neighbours.Add(neighbour);
                        }
                    }
                }

                return neighbours;
            }
            catch(Exception ex)
            {
                logger.Log($"Exception occurred in GetNeighboursWithinThreeByThreeAreaToList: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region Has Location
        /// <summary>
        /// method to indentify the location is valid based on grid size
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool HasLocation(Location location)
        {
            return location.X < Length && location.X >= 0 && location.Y < Length && location.Y >= 0;
        }
        #endregion

        #region Generate X Axis Alphabets in Board
        /// <summary>
        /// Generate X Axis Alphabets in Board based on converting the int to Alphabets
        /// </summary>
        /// <returns></returns>
        public string XAxisAlphabetInBoard(int currentXValue)
        {
            string output = "";
            if (currentXValue == 10) { currentXValue = 0; } else { currentXValue += 1; }
            
            int current = currentXValue % 10;
            currentXValue /= 10;
            
            if (current == 0)
                current = 10;
            output = (char)((char)'A' + (current - 1)) + output;
            output += " ";
            return output;
        }
        #endregion

        #region Generate Y Axis Header in Board
        /// <summary>
        /// Generating the Board Header value for Y axis s
        /// </summary>
        public string GenerateYAxisHeader()
        {
            string yHeader = " ";
            for(var i = 1; i<= Length; i++)
            {
                yHeader += i;
                yHeader += " ";
            }
            return yHeader;
        }
        #endregion


    }
}