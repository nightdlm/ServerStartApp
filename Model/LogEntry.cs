namespace WpfAppAi.Model
{
    public class LogEntry
    {
        public DateTime Time { get; set; } = DateTime.Now;

        public string FormattedTime => Time.ToString("yyyy-MM-dd HH:mm:ss:fff");

        public string ServerName { get; set; } = "";

        public string Message { get; set; } = "";
    }
}
