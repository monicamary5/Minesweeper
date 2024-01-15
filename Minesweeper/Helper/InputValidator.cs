using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Minesweeper
{
    public static class InputValidator
    {
        private const string LocationInputPattern = "^([A-Z]|([A-G])\\2)([0-9]|1[0-7])$";

        #region Grid Size Validation 
        /// <summary>
        /// Invalid Grid Size input validation
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsValidGridSizeInput(string input)
        {
            int result;
            int.TryParse(input, out result);
            return result > 0;
        }

        /// <summary>
        /// Grid Size input Minimum Validation
        /// Minimum grid size should be 3
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        
        public static bool IsGridSizeMinimum(string input)
        {
            return int.Parse(input) < 3;
        }

        /// <summary>
        /// Grid Size input Maximum Validation
        /// Minimum grid size should be less than or Equal to 10
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsGridSizeMaximum(string input)
        {
            return int.Parse(input) > 10;
        }

        #endregion

        #region No of Mines Input Validation
        /// <summary>
        /// Invalid No of mines user input validation 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsValidNoOfMinesInput(string input)
        {
            int result;
            int.TryParse(input, out result);
            return result > 0;
        }

        /// <summary>
        /// Maximum number is 35% of total squares validation
        /// </summary>
        /// <param name="gridInput"></param>
        /// <param name="minesinput"></param>
        /// <returns></returns>
        public static bool IsNoOfMinesInputMaximum(int gridInput, string minesinput)
        {
            var maxPercentageMines = gridInput * gridInput * 0.35; //eg: 4x4x0.35 formula
            return int.Parse(minesinput) > Math.Round(maxPercentageMines);
        }

        /// <summary>
        /// No of Mines user input is zero validation, There must be at least 1 mine.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNoOfMinesInputZero(string input)
        {
            return input == GameInstruction.Zero;
        }

        #endregion

        #region Location Validation

        /// <summary>
        /// User input Location Validation
        /// Location is valid if userinput is A1, A2, B1, B2 etc
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsValidLocationInput(string input)
        {
            return Regex.IsMatch(input, LocationInputPattern);
        }

        #endregion
    }
}