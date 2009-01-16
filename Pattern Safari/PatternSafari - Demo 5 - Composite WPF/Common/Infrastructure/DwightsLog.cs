using System.IO;

namespace Common.Infrastructure
{
    public class DwightsLog : ILog
    {
        public void Write(string message)
        {
            File.AppendAllText("C:\\users\\paul\\desktop\\log.txt", message);
        }
    }
}