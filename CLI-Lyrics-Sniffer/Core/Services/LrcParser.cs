using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using CliLyricsSniffer.Core.Models;

namespace CliLyricsSniffer.Core.Services
{
    public static class LrcParser
    {
        private static readonly Regex _rx =
            new(@"\[(\d+):(\d{2})(?:\.(\d{1,2}))?\](.*)", RegexOptions.Compiled);

        public static List<LrcLine> Parse(string lrc)
        {
            var list = new List<LrcLine>();
            using var sr = new StringReader(lrc);
            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                var m = _rx.Match(line);
                if (!m.Success) continue;

                int mm = int.Parse(m.Groups[1].Value);
                int ss = int.Parse(m.Groups[2].Value);
                int cs = m.Groups[3].Success ? int.Parse(m.Groups[3].Value.PadRight(2, '0')) : 0;

                list.Add(new LrcLine
                {
                    TimeMs = (mm * 60 + ss) * 1000 + cs * 10,
                    Text = m.Groups[4].Value.Trim()
                });
            }
            return list;
        }
    }
}
