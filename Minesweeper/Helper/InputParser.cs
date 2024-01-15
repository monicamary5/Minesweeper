using System;

namespace Minesweeper
{
    public static class InputParser
    {
        /// <summary>
        /// Converting the user Location input value from A0, A1 to coordinates like (0,0), (0,1)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
       public static Location CreateLocationBasedOnInput(string input)
       {
            var locationXInput = GetLocationXInputString(input);
            var locationYInput = GetLocationYInputString(input);
            var locationXValue = (char.ToUpper(Convert.ToChar(locationXInput)) - 64) - 1;
            var locationYValue = int.Parse(locationYInput);
            var newLocation = new Location(locationXValue, locationYValue);
            return newLocation;
        }

        /// <summary>
        /// Coordinates Y axis substring function for conversion from A0 to Coordinates (0,0)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
       private static string GetLocationYInputString(string input)
       {
            return input.Substring(1,1);
       }

        /// <summary>
        /// Coordinates X axis substring function for conversion from A0 to Coordinates (0,0)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string GetLocationXInputString(string input)
       {
            return input.Substring(0,1);
       }
    }
}