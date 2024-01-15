using System.Collections.Generic;
using System.Linq;

namespace Minesweeper
{
    public static class HintGenerator
    {
        #region Set Hints

        //Method to set the values i.e., Hints for non mine fields
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

        //Method to get the Mines location in Board using the IsMine Variable
        private static IEnumerable<Square> GetMines(Board board)
        {
            return board.Squares.Where(item => item.IsMine);
        }

        #endregion

        #region Increment All Neighbours Hint Value By One

        //Method is used to find the neighbours location based on Mines position and increment value by 1.
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
        private static bool BoardSizeIsTooSmall(Board board)
        {
            return board.Length < 3;
        }
        #endregion

        #region Board Does Not Contain Any Mine check Method
        private static bool BoardDoesNotContainAnyMine(Board board)
        {
            return !board.Squares.Any(square => square.IsMine);
        }
        #endregion

        #region Board Size Is Maximum
        private static bool BoardSizeIsMaximum(Board board)
        {
            return board.Length > 10;
        }
        #endregion

    }
}