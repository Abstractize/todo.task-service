namespace Models;

public record TaskItemLog
{
    public LogAction Action { get; set; }
    public DateTime ExecutionAtUtc { get; set; }
}

public enum LogAction
{
    Created,
    Updated,
    Completed,
    Deleted
}