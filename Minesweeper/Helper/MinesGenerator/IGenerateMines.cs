namespace Minesweeper
{
    /// <summary>
    /// Interface to Generate Mines
    /// </summary>
    public interface IGenerateMines
    {
        public void PlaceMines(int numberOfMines, Board board);
    }
}