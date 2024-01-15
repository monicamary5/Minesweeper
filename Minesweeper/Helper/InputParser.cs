using System;

namespace Minesweeper
{
    public static class InputParser
    {
       public static Location CreateLocationBasedOnInput(string input)
       {
            var locationXInput = GetLocationXInputString(input);
            var locationYInput = GetLocationYInputString(input);
            var locationXValue = (char.ToUpper(Convert.ToChar(locationXInput)) - 64) - 1;
            var locationYValue = int.Parse(locationYInput);
            var newLocation = new Location(locationXValue, locationYValue);
            return newLocation;
        }

       private static string GetLocationYInputString(string input)
       {
            return input.Substring(1,1);
       }

       private static string GetLocationXInputString(string input)
       {
            return input.Substring(0,1);
       }
    }
}