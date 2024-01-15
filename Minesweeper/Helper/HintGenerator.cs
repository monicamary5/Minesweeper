using System.Collections.Generic;
using System.Linq;

namespace Minesweeper
{
    public static class HintGenerator
    {
        #region Set Hints

        /// <summary>
        /// Method to set the values i.e., Hints for non mine fields
        /// </summary>
        /// <param name="board"></param>
        public static void SetHints(Board board)
        {
            if (BoardSizeIsTooSmall(board)) return;
            if (BoardSizeIsMaximum(board)) return;
            if (BoardDoesNotContainAnyMine(board)) return;

            //GetMines method to get the All mines location in Board
            var mines = GetMines(board);

            foreach (var item in mines)
            {
                IncrementAllNeighboursHintValueByOne(board, item);
            }
        }

        #endregion

        #region Get Mines

        /// <summary>
        /// Method to get the Mines location in Board using the IsMine Variable
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        private static IEnumerable<Square> GetMines(Board board)
        {
            return board.Squares.Where(item => item.IsMine);
        }

        #endregion

        #region IncrementAllNeighboursHintValueByOne

        /// <summary>
        /// Method is used to find the neighbours location based on Mines position and increment value by 1.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="item"></param>
        private static void IncrementAllNeighboursHintValueByOne(Board board, Square item)
        {
            var neighbours = board.GetNeighbours(item);
            if (neighbours != null)
            {
                foreach (var neighbour in neighbours)
                {
                    neighbour.Hint += 1;
                }
            }
        }

        #endregion

        #region Board Size Is Too Small Condition check Method

        /// <summary>
        /// Board Size Is Too Small Condition check Method
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        private static bool BoardSizeIsTooSmall(Board board)
        {
            return board.Length < 3;
        }
        #endregion

        #region BoardDoesNotContainAnyMine

        /// <summary>
        /// Board Does Not Contain Any Mine validation method
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        private static bool BoardDoesNotContainAnyMine(Board board)
        {
            return !board.Squares.Any(square => square.IsMine);
        }
        #endregion

        #region Board Size Is Maximum

        /// <summary>
        /// Board Size Is Maximum Validation 
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        private static bool BoardSizeIsMaximum(Board board)
        {
            return board.Length > 10;
        }
        #endregion

    }
}