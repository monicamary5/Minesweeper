using System.Linq;

namespace Minesweeper
{
    public static class WinLoseChecker
    {
        /// <summary>
        /// Method to verify that  all hints are Revealed by user
        /// </summary>
        /// <param name="currentBoard"></param>
        /// <returns></returns>
        public static bool IsWinningConditionWhenAllHintsAreRevealed(Board currentBoard)
        {
            var hints = currentBoard.Squares.Where(item => !item.IsMine);
            return hints.All(item => item.IsRevealed);
        }

        /// <summary>
        /// Method to verify that any mine location is revealed by user
        /// </summary>
        /// <param name="currentBoard"></param>
        /// <returns></returns>
        public static bool IsLosingConditionWhenOneMineIsRevealed(Board currentBoard)
        {
            var mines = currentBoard.Squares.Where(item => item.IsMine);
            return mines.Any(item => item.IsRevealed);
        }
    }
}