using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaumansGateLibrary.Battle
{
    public static class GameLog
    {
        static string Log = "";
        static int LogCount = 0;
        public static int GetLogCount() { return LogCount; }
        public static string LogInfo() { return Log; }
        public static string GetLastLine()
        {
            string[] Lines = LogInfo().Trim().Split('\n');
            return Lines[Lines.Length - 1];
        }
        public static void AddLine(string message)
        {
            LogCount++;
            Log += LogCount + ". " + message;
        }
        public static void Clear() { Log = ""; }
    }
}
