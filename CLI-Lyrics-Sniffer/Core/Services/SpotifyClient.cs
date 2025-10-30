using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using CliLyricsSniffer.Core.Models;

namespace CliLyricsSniffer.Core.Services
{
    public class SpotifyClient
    {
        private readonly HttpClient _http = new HttpClient();
        private DateTime _lastSync = DateTime.UtcNow;
        private int _lastProgressMs = 0;
        private const int ResyncThresholdMs = 500;

        public SpotifyClient(string token)
        {
            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<PlayingInfo?> GetCurrentAsync()
        {
            try
            {
                var res = await _http.GetAsync("https://api.spotify.com/v1/me/player/currently-playing");
                if (!res.IsSuccessStatusCode)
                    return null;

                var json = await res.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(json))
                    return null;

                var jo = JObject.Parse(json);
                if (jo["item"] == null)
                    return null;

                var info = new PlayingInfo
                {
                    TrackId = jo["item"]?["id"]?.ToString() ?? "",
                    Title = jo["item"]?["name"]?.ToString() ?? "",
                    Artist = jo["item"]?["artists"]?[0]?["name"]?.ToString() ?? "",
                    DurationMs = jo["item"]?["duration_ms"]?.Value<int?>() ?? 0,
                    ProgressMs = jo["progress_ms"]?.Value<int?>() ?? 0,
                    IsPlaying = jo["is_playing"]?.Value<bool?>() ?? false
                };

                ApplyResync(info);
                return info;
            }
            catch
            {
                return null;
            }
        }

        private void ApplyResync(PlayingInfo info)
        {
            var now = DateTime.UtcNow;
            double delta = (now - _lastSync).TotalMilliseconds;
            int expected = _lastProgressMs + (int)delta;

            if (Math.Abs(info.ProgressMs - expected) > ResyncThresholdMs)
            {
                _lastProgressMs = info.ProgressMs;
                _lastSync = now;
            }
            else
            {
                _lastProgressMs = expected;
            }
        }
    }
}
