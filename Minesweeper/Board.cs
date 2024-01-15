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

        //logger for log the details
        Logtrack logger = new Logtrack();

        #region Board Private Constructor

        //Constructor stores the length(grid size) for accessing this value by entire class
        //create squares based on grid size length input value.
        private Board(int length)
        {
            Length = length;
            Squares = CreateSquares();
            IsRevealed = false;
        }

        #endregion

        #region Create Squares
        // This method is used to create squares using X and Y Values Based on Grid Size length
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

        //this method is to create Empty board after first input value for Grid Size, No of Mines received from User
        public static Board CreateEmptyBoard(int size)
        {
            return new Board(size);
        }

        #endregion

        #region Reveal All Squares

        //Method is used to reveal all squares based on Win or Lose 
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

        //Method gets executed based on user location input to reveal the square
        public void RevealOneSquare(Location location)
        {
            var square = GetSquare(location);
            logger.Log("RevealOneSquare X Value => " + square.Location.X + " RevealOneSquare Y Value => " + square.Location.Y + " isRevealed => " + square.IsRevealed + "isMine =>  "+ square.IsMine);
            square.IsRevealed = true;
        }

        #endregion

        #region Get Square

        //Method is used for retriving the exact square based on location for revealing to user
        public Square GetSquare(Location location)
        {
            var square = Squares.SingleOrDefault(item => item.Location.Equals(location));
            return square;
        }
        #endregion

        #region ToString Method 

        //Inbuild toString method has been overidden and customize it based on requirement
        public override string ToString()
        {
            var message = "";
            for (var xValue = 0; xValue < Length; xValue++)
            {
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

        //based on each mine square getting the neighbours within 3x3 matrix area
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

        //method to indentify the location is valid based on grid size
        public bool HasLocation(Location location)
        {
            return location.X < Length && location.X >= 0 && location.Y < Length && location.Y >= 0;
        }
        #endregion
    }
}