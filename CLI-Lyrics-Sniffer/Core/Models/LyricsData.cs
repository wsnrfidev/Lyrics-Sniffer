using System.Collections.Generic;

namespace CliLyricsSniffer.Core.Models
{
    public class LyricsData
    {
        public List<LrcLine> Lines { get; set; } = new();
        public string PlainText { get; set; } = "";
    }
}
