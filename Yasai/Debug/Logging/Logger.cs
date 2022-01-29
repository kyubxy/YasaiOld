using System;
using System.IO;
using System.Linq;
using Yasai.Resources;

namespace Yasai.Debug.Logging
{
    public sealed class Logger
    {
        private const int MAX_FILE_LENGTH = 1000;
        
        private string logPath => Path.Combine (PrefHelper.HomeDirectory, logName);
        private string logName;
        private bool writeFile;
        private bool writeConsole;

        private int fileLength;
        
        public Logger(string logName, bool writeFile = true, bool writeConsole = true)
        {
            this.logName = logName;
            this.writeFile = writeFile;
            this.writeConsole = writeConsole;
            
            // make the file if it doesn't already exist
            if (!File.Exists(logPath))
                File.CreateText(logPath).Close();

            fileLength = (int)(new FileInfo(logPath).Length);
            performCull();
        }
        
        public void Log(string message, LogLevel level)
        {
            string msg = $"{DateTime.Now} [{level.ToString()}]: {message}";

            // TODO: use bitwise operations to specify custom
            // verbosity levels
            if (writeFile && level != LogLevel.Debug) {
                performCull();
                File.AppendAllLines(logPath, new[] { msg });
                fileLength++;
            }
            
            if (writeConsole)
                Console.WriteLine(msg);
        }

        /// <summary>
        /// prevent files from getting too large
        /// </summary>
        void performCull()
        {
            if (fileLength > MAX_FILE_LENGTH)
            {
                var amountToCull = fileLength - MAX_FILE_LENGTH;
                var file = File.ReadAllLines(logPath);
                var trimmed = file.Skip(amountToCull).ToList();
                File.WriteAllLines(logPath, trimmed);
                LogDebug ($"culled {amountToCull} from {logPath}");
                fileLength = 0;
            }
        }

        public void LogInfo(string message) => Log(message, LogLevel.Info);
        public void LogWarning(string message) => Log(message, LogLevel.Warning);
        public void LogError(string message) => Log(message, LogLevel.Error);
        public void LogDebug(string message) => Log(message, LogLevel.Debug);
    }
}