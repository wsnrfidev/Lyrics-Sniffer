using CliLyricsSniffer.Core.Models;
using CliLyricsSniffer.Core.Render;
using CliLyricsSniffer.Core.Services;
using CliLyricsSniffer.Core.Utils;
using CliLyricsSniffer.Utils;
using System.Text;

namespace CliLyricsSniffer
{
    class Program
    {
        private static readonly CancellationTokenSource _cts = new();
        private static readonly string Separator = "===========================================";

        static async Task<int> Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();
            ConsoleHelper.EnableAnsi();

            try
            {
                var root = PathHelper.DetectProjectRoot();
                Directory.SetCurrentDirectory(root);
                ConsoleHelper.PrintCentered($"[AUTO] Project root detected: {root}", 0);
            }
            catch
            {
                ConsoleHelper.PrintCentered("[AUTO] Running from current directory...", 0);
            }

            var config = AppConfig.Load();
            Theme.Apply();

            ConsoleHelper.PrintHeader("Waiting Spotify Authorization...");

            var token = await SpotifyAuth.AuthorizeAsync(config.SpotifyClientId, config.RedirectUri);
            if (token == null)
            {
                Theme.Warn("❌ Authorization failed!");
                return 1;
            }

            var spotify = new SpotifyClient(token);
            var lyricsSvc = new LyricsService();
            var renderer = new KaraokeRenderer();

            PlayingInfo? npPrev = null;
            string? currentTrack = null;

            ConsoleHelper.PrintHeader("Connected - Listening to Spotify...\n");

            int lastWidth = Console.WindowWidth;
            int lastHeight = Console.WindowHeight;

            while (!_cts.IsCancellationRequested)
            {

                if (Console.WindowWidth != lastWidth || Console.WindowHeight != lastHeight)
                {
                    Console.Clear();
                    lastWidth = Console.WindowWidth;
                    lastHeight = Console.WindowHeight;
                    if (npPrev != null)
                        ConsoleHelper.PrintHeader($"🎵 {npPrev.Title} — {npPrev.Artist}");
                }


                var np = await spotify.GetCurrentAsync();
                if (np == null)
                {
                    ConsoleHelper.PrintCentered("🎧 No music playing…", 2);
                    await Task.Delay(800);
                    continue;
                }


                if (npPrev == null || npPrev.TrackId != np.TrackId)
                {
                    currentTrack = np.TrackId;

                    for (int i = 0; i < 3; i++)
                    {
                        Console.Clear();
                        ConsoleHelper.PrintHeader($"🎵 {np.Title} — {np.Artist}");
                        ConsoleHelper.PrintCentered($"Transitioning{i switch { 0 => ".", 1 => "..", _ => "..." }}", 2);
                        await Task.Delay(90);
                    }

                    Console.Clear();
                    ConsoleHelper.PrintHeader($"🎵 {np.Title} — {np.Artist}");

                    var lyrics = await lyricsSvc.GetLyricsAsync(np.Artist, np.Title);
                    if (lyrics == null)
                    {
                        Theme.Warn("(Lyrics Not Found)");
                        await Task.Delay(1500);
                        npPrev = np;
                        continue;
                    }


                    renderer.Start(lyrics, spotify);
                }


                ConsoleHelper.PrintCentered($"⏱️ {ConsoleHelper.FormatTime(np.DurationMs)} | Offset {ConsoleHelper.FormatTime(np.ProgressMs)}", 2);
                ConsoleHelper.PrintCentered(ConsoleHelper.Bar(np.ProgressMs, np.DurationMs, 50), 3);
                ConsoleHelper.PrintCentered(np.IsPlaying ? "\x1b[1m▶ PLAYING\x1b[0m" : "\x1b[1m⏸ PAUSED\x1b[0m", 4);

                npPrev = np;
                await Task.Delay(200);
            }

            return 0;
        }
    }
}
