using System;
using System.IO;
using System.Net;
using Yasai.Resources;

namespace Yasai.Debug.Logging
{
    public sealed class Logger
    {
        private string logPath;
        private bool writeFile;
        private bool writeConsole;
        
        public Logger(string logName, bool writeFile = true, bool writeConsole = true)
        {
            logPath = Path.Combine(PrefHelper.HomeDirectory, logName);
            this.writeFile = writeFile;
            this.writeConsole = writeConsole;
            
            // make the file if it doesn't already exist
            if (!File.Exists(logPath))
                File.CreateText(logPath).Close();
        }
        
        public void Log(string message, LogLevel level)
        {
            string msg = $"{DateTime.UtcNow} [{level.ToString()}]: {message}";
            
            if (writeFile)
                File.AppendAllLines(logPath, new[] { msg });
            
            if (writeConsole)
                Console.WriteLine(msg);
        }

        public void LogInfo(string message) => Log(message, LogLevel.Info);
        public void LogWarning(string message) => Log(message, LogLevel.Warning);
        public void LogError(string message) => Log(message, LogLevel.Error);
        public void LogDebug(string message) => Log(message, LogLevel.Debug);
    }
}