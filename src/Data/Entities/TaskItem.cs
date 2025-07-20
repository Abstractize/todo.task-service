namespace Data.Entities;

public class TaskItem
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    // Completion
    public bool IsCompleted => CompletedAtUtc.HasValue;
    public Guid? CompletedBy { get; set; }
    public DateTime? CompletedAtUtc { get; set; }

    // Creation
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    // Update
    public Guid UpdatedBy { get; set; }
    public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;

    // Deletion
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedAtUtc { get; set; }

    // Relationships
    public Guid TaskListId { get; set; }
    public TaskList? TaskList { get; set; }
}