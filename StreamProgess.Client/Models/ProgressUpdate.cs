namespace StreamProgess.Shared;

public class ProgressUpdate
{
    public int Percentage { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
