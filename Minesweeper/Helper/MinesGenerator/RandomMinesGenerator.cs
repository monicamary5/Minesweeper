using System;
using System.Collections.Generic;
using System.Linq;

namespace Minesweeper
{
    //For Mines generate randomly used Abstraction oops concept
    public class RandomMinesGenerator : IGenerateMines
    {
        
        private readonly Random _random = new Random();

        #region Place Mines in Board
        public void PlaceMines(int numberOfMines, Board board)
        {
            var selectedSquares = RandomlySelectSquares(numberOfMines, board);
            PlaceMineOnEachOfTheSelectedSquares(selectedSquares);
        }

        #endregion

        #region Place Mine On Each Of The Selected Squares
        /// <summary>
        /// Method for placing the mines randomly 
        /// </summary>
        /// <param name="selectedSquares"></param>
        private static void PlaceMineOnEachOfTheSelectedSquares(IEnumerable<Square> selectedSquares)
        {
            foreach (var square in selectedSquares)
            {
                square.SetMine();
            }
        }

        #endregion

        #region Randomly Select Squares based on No of Mines input and Board
        /// <summary>
        /// Method to randomly select the squares for placing the mines in board
        /// </summary>
        /// <param name="number"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        private IEnumerable<Square> RandomlySelectSquares(int number, Board board)
        {
            return board.Squares.OrderBy(x => _random.Next()).Take(number);
        }
        #endregion
    }
}