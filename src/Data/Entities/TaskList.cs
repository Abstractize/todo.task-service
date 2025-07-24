using Data.Common.Entity;

namespace Data.Entities;

public class TaskList : AuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<TaskItem> Tasks { get; set; } = [];
}