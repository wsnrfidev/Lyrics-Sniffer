using System.IO;
using Newtonsoft.Json.Linq;
using CliLyricsSniffer.Core.Utils;

namespace CliLyricsSniffer.Core.Models
{
    public class AppConfig
    {
        public string SpotifyClientId { get; set; } = "";
        public string RedirectUri { get; set; } = "";

        public static AppConfig Load()
        {
            string file = PathHelper.GetConfigPath("appsettings.json");
            if (!File.Exists(file))
            {
                Console.WriteLine("⚠️ appsettings.json not found!");
                return new AppConfig();
            }

            var jo = JObject.Parse(File.ReadAllText(file));
            return new AppConfig
            {
                SpotifyClientId = jo.SelectToken("Spotify.ClientId")?.ToString() ?? "",
                RedirectUri = jo.SelectToken("Spotify.RedirectUri")?.ToString() ?? ""
            };
        }
    }
}
