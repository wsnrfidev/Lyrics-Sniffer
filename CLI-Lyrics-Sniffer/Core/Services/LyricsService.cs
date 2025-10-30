using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using CliLyricsSniffer.Core.Models;
using CliLyricsSniffer.Core.Utils;
using CliLyricsSniffer.Core.Services;

namespace CliLyricsSniffer.Core.Services
{
    public class LyricsService
    {
        private static readonly HttpClient _http = new HttpClient();

        public async Task<LyricsData?> GetLyricsAsync(string artist, string title)
        {
            try
            {
                string url =
                    $"https://lrclib.net/api/get?artist_name={Uri.EscapeDataString(artist)}" +
                    $"&track_name={Uri.EscapeDataString(title)}";

                var res = await _http.GetAsync(url);
                if (!res.IsSuccessStatusCode) return null;

                var json = await res.Content.ReadAsStringAsync();
                var jo = JObject.Parse(json);

                var synced = jo["syncedLyrics"]?.ToString();
                if (string.IsNullOrWhiteSpace(synced)) return null;

                var lines = LrcParser.Parse(synced);
                return new LyricsData
                {
                    Lines = lines,
                    PlainText = jo["plainLyrics"]?.ToString() ?? ""
                };
            }
            catch (Exception ex)
            {
                Theme.Warn($"Lyrics error: {ex.Message}");
                return null;
            }
        }
    }
}
