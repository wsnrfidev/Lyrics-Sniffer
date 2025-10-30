using System;
using Microsoft.Win32;

namespace CliLyricsSniffer.Core.Utils
{
    public static class Theme
    {
        private static bool _isLight;

        static Theme()
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                _isLight = ((int?)key?.GetValue("AppsUseLightTheme") ?? 0) == 1;
            }
            catch { _isLight = false; }
        }

        public static void Apply()
        {
            Console.ForegroundColor = _isLight ? ConsoleColor.Black : ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
        }

        public static void Warn(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(msg);
            Console.ForegroundColor = _isLight ? ConsoleColor.Black : ConsoleColor.White;
        }
    }
}
