using System;

namespace Minesweeper
{
    /// <summary>
    /// Console Input Class which inherits the IInput Interface
    /// </summary>
    public class ConsoleInput : IInput
    {
        public string Ask(string question)
        {
            Console.WriteLine(question);
                return Console.ReadLine();
            }
    }
}