namespace Minesweeper
{
    public class Square
    {
        //Bool Variable to identify whether square is Mine
        public bool IsMine { get; private set; }

        //Bool Variable to identify whether square is Revealed
        public bool IsRevealed { get; set; }

        //Variable to get and set the Value for non Mine squares 
        public int Hint { get; set; }

        public Location Location { get;}

        //Parameterized Constructor is used to set the bool Variables for revealing and is mine during intialization
        public Square(Location location)
        {
            IsRevealed = false;
            Location = location;
            IsMine = false;
            
        }
        
        //Method to reveal the square by updating the IsRevealed bool var to true
        public void Reveal()
        {
            IsRevealed = true;
        }

        //Method to set the Mine by updating IsMine bool variable to true
        public void SetMine()
        {
            IsMine = true;
        }

        //Method is to set the placeholder for mine and hint for the square in the board 
        public override string ToString()
        {
            if (!IsRevealed) return ".";
            return IsMine ? "*" : Hint.ToString();
        }
    }
}