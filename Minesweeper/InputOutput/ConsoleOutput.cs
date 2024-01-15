using System;

namespace Minesweeper
{
    /// <summary>
    /// Console Output Class which inherits the IOutput Interface
    /// </summary>
    public class ConsoleOutput : IOutput
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
        
    }
}