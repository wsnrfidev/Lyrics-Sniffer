using System;
using System.IO;

namespace CliLyricsSniffer.Utils
{
    public static class PathHelper
    {
        
        public static string DetectProjectRoot()
        {
            string current = Directory.GetCurrentDirectory();

            for (int i = 0; i < 10; i++) 
            {
                if (File.Exists(Path.Combine(current, "appsettings.json")) &&
                    Directory.GetFiles(current, "*.csproj").Length > 0)
                {
                    return current;
                }

                var parent = Directory.GetParent(current);
                if (parent == null) break;
                current = parent.FullName;
            }

            throw new DirectoryNotFoundException(
                "[ERROR] Project path not found.\nPlease ensure the .csproj and appsettings.json exist."
            );
        }

        public static string GetAppSettingsPath()
        {
            string root = DetectProjectRoot();
            string path = Path.Combine(root, "appsettings.json");

            if (!File.Exists(path))
                throw new FileNotFoundException($"appsettings.json not found at {path}");

            return path;
        }
    }
}
