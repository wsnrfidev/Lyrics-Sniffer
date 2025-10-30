using System;
using System.IO;

namespace CliLyricsSniffer.Core.Utils
{
    public static class PathHelper
    {
        public static string GetBasePath()
        {
            return AppContext.BaseDirectory;
        }

        public static string GetConfigPath(string fileName = "appsettings.json")
        {
            string local = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            if (File.Exists(local)) return local;

            string exePath = Path.Combine(GetBasePath(), fileName);
            if (File.Exists(exePath)) return exePath;

            return fileName;
        }

        public static string DetectProjectRoot()
        {
            var dir = Directory.GetCurrentDirectory();
            while (dir != null)
            {
                if (Directory.GetFiles(dir, "*.csproj").Length > 0)
                    return dir;

                dir = Directory.GetParent(dir)?.FullName;
            }
            return Directory.GetCurrentDirectory();
        }
    }
}
