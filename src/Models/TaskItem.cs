namespace Models;

public record TaskItem
{
    public Guid? Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;

    public DateTime CreatedAtUtc { get; set; }

    public Guid TaskListId { get; set; }
}