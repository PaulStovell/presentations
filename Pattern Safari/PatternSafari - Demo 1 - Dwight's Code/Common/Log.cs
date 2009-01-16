using System;
using System.IO;

namespace Common
{
    public static class Log
    {
        public static void Write(string message)
        {
            File.AppendAllText("C:\\users\\paul\\desktop\\log.txt", message + Environment.NewLine);
        }
    }
}