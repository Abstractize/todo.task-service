using Grpc.Core;
using Analytics.Protos;
using Managers.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace Grpc.Services.v1;

[Authorize]
public class TasksGrpcService(ITaskItemManager taskItemManager, ITaskLogManager taskLogManager) : TaskAnalyticsService.TaskAnalyticsServiceBase
{
    private readonly ITaskItemManager _taskItemManager = taskItemManager;
    private readonly ITaskLogManager _taskLogManager = taskLogManager;

    public override async Task<TaskList> GetUserTasks(GetTasksRequest request, ServerCallContext context)
    {
        Guid userId = Guid.Parse(request.UserId);
        IEnumerable<Models.TaskItem> tasks = await _taskItemManager.GetAsync(userId);

        var taskList = new TaskList();
        taskList.Tasks.AddRange(tasks.Select(t => new TaskItem
        {
            Id = t.Id.ToString(),
            Title = t.Title,
            IsCompleted = t.IsCompleted,
        }));

        return taskList;
    }

    public override async Task<TaskList> GetUserTasksOfWeek(GetWeeklyTasksRequest request, ServerCallContext context)
    {
        Guid userId = Guid.Parse(request.UserId);
        DateTime weekStart = DateTime.Parse(request.WeekStartDateUtc);

        IEnumerable<Models.TaskItem> tasks = await _taskItemManager.GetWeeklyTasksAsync(userId, weekStart);

        TaskList taskList = new()
        {
            Tasks = {
                tasks.Select(t => new TaskItem
                {
                    Id = t.Id.ToString(),
                    Title = t.Title,
                    IsCompleted = t.IsCompleted,
                    CreatedAt = t.CreatedAtUtc.ToUniversalTime().ToString("o") // ISO 8601 format
                }).ToList()
            },
            FirstActivity = (await _taskLogManager.GetFirstActivityAsync(userId)).ExecutionAt.ToUniversalTime().ToString("o"),
            LastActivity = (await _taskLogManager.GetLastActivityAsync(userId)).ExecutionAt.ToUniversalTime().ToString("o")
        };

        return taskList;
    }
}

