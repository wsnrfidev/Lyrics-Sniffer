using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace CliLyricsSniffer.Core.Utils
{
    public class AppConfig
    {
        public string SpotifyClientId { get; set; } = "";
        public string RedirectUri { get; set; } = "";

        public static AppConfig Load()
        {
            var file = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            if (!File.Exists(file))
                return new AppConfig();

            var jo = JObject.Parse(File.ReadAllText(file));
            return new AppConfig
            {
                SpotifyClientId = jo.SelectToken("Spotify.ClientId")?.ToString() ?? "",
                RedirectUri = jo.SelectToken("Spotify.RedirectUri")?.ToString() ?? ""
            };
        }
    }
}
