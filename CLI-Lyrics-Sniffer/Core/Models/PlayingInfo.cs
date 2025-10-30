namespace CliLyricsSniffer.Core.Models
{
    public class PlayingInfo
    {
        public string TrackId { get; set; } = "";
        public string Title { get; set; } = "";
        public string Artist { get; set; } = "";
        public int DurationMs { get; set; }
        public int ProgressMs { get; set; }
        public bool IsPlaying { get; set; }
    }
}
