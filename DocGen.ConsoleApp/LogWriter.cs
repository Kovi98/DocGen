using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DocGen.ConsoleApp
{
    public class LogWriter
    {
        private string _path = string.Empty;
        private string _logFile = string.Empty;
        private string _logErrorFile = string.Empty;
        private StringBuilder sb = new StringBuilder();
        private StringBuilder sbError = new StringBuilder();

        public LogWriter(string path)
        {
            _path = path;
            _logFile = "log-" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".txt";
            _logErrorFile = "log-Error-" + DateTime.Now.ToString("yyyyMMddTHHmmss") + ".txt";
        }

        public void Log(string text, LogType type = LogType.Information)
        {
            string head = "[" + DateTime.Now.ToString() + "] [" + type.ToString() + "]";
            sb.AppendLine(head + " " + text);
            WriteToFile();
            if (type == LogType.Error)
            {
                sbError.AppendLine(head + " " + text);
                WriteToErrorFile();
            }
        }

        public void WriteToFile()
        {
            try
            {
                File.WriteAllText(_path + "\\" + _logFile, sb.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void WriteToErrorFile()
        {
            try
            {
                File.WriteAllText(_path + "\\" + _logErrorFile, sbError.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public enum LogType
        {
            Information = 1,
            Warning,
            Error
        }
    }
}
