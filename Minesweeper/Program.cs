using System;

namespace Minesweeper
{
    class Program
    {
        /// <summary>
        /// Main Method for Starting the Game, Mines generator and Playing the game 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
           var input = new ConsoleInput();
           var output = new ConsoleOutput();
           var minesGenerator = new RandomMinesGenerator();
           var game = new Game(input, output, minesGenerator);
           
               game.CreateBoard();
               game.Play();
            
        }
    }
}