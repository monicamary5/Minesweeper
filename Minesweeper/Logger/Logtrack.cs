using System;
using System.IO;


namespace Minesweeper
{
    public class Logtrack
    {
        private string logFilePath;

        public Logtrack()
        {
            
            // Get the current project directory
            string projectPath = Directory.GetCurrentDirectory();

            // Create a "Logs" folder within the project directory if it doesn't exist
            string logFolderPath = @"C:\Logs";
            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }

            // Generate a unique log file name based on the current date and time
            string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".log";

            // Set the log file path
            logFilePath = Path.Combine(logFolderPath, fileName);
        }

        public void Log(string message)
        {
            // Append the log message to the log file
            File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}\n");
        }

    }

}

