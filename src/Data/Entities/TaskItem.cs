using Data.Common.Entity;

namespace Data.Entities;

public class TaskItem : AuditableEntity
{
    public string Title { get; set; } = string.Empty;

    // Completion
    public bool IsCompleted => CompletedAtUtc.HasValue;
    public Guid? CompletedBy { get; set; }
    public DateTime? CompletedAtUtc { get; set; }

    // Relationships
    public Guid TaskListId { get; set; }
    public TaskList? TaskList { get; set; }
}