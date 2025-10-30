using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using CliLyricsSniffer.Core.Models;
using CliLyricsSniffer.Core.Services;

namespace CliLyricsSniffer.Core.Render
{
    public class KaraokeRenderer
    {
        private CancellationTokenSource? _cts;

        public void Start(LyricsData data, SpotifyClient spotify)
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            _ = RenderLoopAsync(data, spotify, _cts.Token);
        }

        private async Task RenderLoopAsync(LyricsData data, SpotifyClient spotify, CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                var now = await spotify.GetCurrentAsync();
                if (now == null) { await Task.Delay(300, ct); continue; }

                int width = Console.WindowWidth;
                int height = Console.WindowHeight;
                int centerY = Math.Max(6, height / 2);


                int idx = data.Lines.FindLastIndex(l => l.TimeMs <= now.ProgressMs);
                if (idx < 0) idx = 0;

  
                for (int y = -2; y <= 2; y++)
                {
                    Console.SetCursorPosition(0, centerY + y);
                    Console.Write(new string(' ', width));
                }

                for (int off = -2; off <= 2; off++)
                {
                    int i = idx + off;
                    string txt = i >= 0 && i < data.Lines.Count ? CleanText(data.Lines[i].Text) : "";
                    bool active = off == 0;

                    string render = active ? $"• {txt} •" : txt;
                    int left = Math.Max(0, (width - render.Length) / 2);
                    Console.SetCursorPosition(left, centerY + off);

                    if (active)
                    {                       
                        Console.Write("\x1b[1m");
                        Console.Write(render);
                        Console.Write("\x1b[0m");
                    }
                    else
                    {
                        Console.Write(render);
                    }
                }

                await Task.Delay(now.IsPlaying ? 120 : 300, ct);
            }
        }

        private string CleanText(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return "";
            s = Regex.Replace(s, "<.*?>", "");   // buang tag HTML
            s = Regex.Replace(s, @"\s+", " ");   // rapihin spasi
            return s.Trim();
        }
    }
}
