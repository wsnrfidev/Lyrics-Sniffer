using System;
using System.Runtime.InteropServices;

namespace CliLyricsSniffer.Core.Utils
{
    public static class ConsoleHelper
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetConsoleMode(IntPtr hConsoleHandle, out int lpMode);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetConsoleMode(IntPtr hConsoleHandle, int dwMode);
        const int STD_OUTPUT_HANDLE = -11;
        const int ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

        public static void EnableAnsi()
        {
            try
            {
                var handle = GetStdHandle(STD_OUTPUT_HANDLE);
                if (GetConsoleMode(handle, out int mode))
                    SetConsoleMode(handle, mode | ENABLE_VIRTUAL_TERMINAL_PROCESSING);
            }
            catch { }
        }

        public static string Bar(int currentMs, int totalMs, int width = 40)
        {
            if (totalMs <= 0) totalMs = 1;
            double p = Math.Clamp(currentMs / (double)totalMs, 0, 1);
            int filled = (int)Math.Round(p * width);
            return "│" + new string('─', filled) + new string(' ', Math.Max(0, width - filled)) + "│";
        }

        public static string FormatTime(int ms)
        {
            var t = TimeSpan.FromMilliseconds(Math.Max(0, ms));
            return t.ToString(t.Hours > 0 ? "hh\\:mm\\:ss" : "mm\\:ss");
        }

        public static void PrintHeader(string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            PrintCentered(title, 0);
            PrintCentered("===========================================", 1);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintCentered(string text, int yOffset)
        {
            int width = Console.WindowWidth;
            int left = Math.Max(0, (width - text.Length) / 2);
            Console.SetCursorPosition(left, yOffset);
            Console.Write(text.PadRight(width - left));
        }
    }
}
