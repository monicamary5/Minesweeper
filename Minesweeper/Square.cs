namespace Minesweeper
{
    public class Square
    {
        /// <summary>
        /// Bool Variable to identify whether square is Mine
        /// </summary>
        public bool IsMine { get; private set; }

        /// <summary>
        /// Bool Variable to identify whether square is Revealed
        /// </summary>
        public bool IsRevealed { get; set; }

        /// <summary>
        /// Variable to get and set the Value for non Mine squares 
        /// </summary>
        public int Hint { get; set; }

        public Location Location { get;}

        /// <summary>
        /// Parameterized Constructor is used to set the bool Variables for revealing and is mine during intialization
        /// </summary>
        /// <param name="location"></param>
        public Square(Location location)
        {
            IsRevealed = false;
            Location = location;
            IsMine = false;
            
        }

        /// <summary>
        /// Method to reveal the square by updating the IsRevealed bool var to true
        /// </summary>
        public void Reveal()
        {
            IsRevealed = true;
        }

        /// <summary>
        /// Method to set the Mine by updating IsMine bool variable to true
        /// </summary>
        public void SetMine()
        {
            IsMine = true;
        }

        /// <summary>
        /// Method is to set the placeholder for mine and hint for the square in the board 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (!IsRevealed) return ".";
            return IsMine ? "*" : Hint.ToString();
        }
    }
}