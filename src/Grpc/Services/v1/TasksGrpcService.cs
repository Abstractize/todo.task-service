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

        Console.WriteLine($"Fetching tasks for user {userId} starting from week beginning {weekStart.ToUniversalTime().ToString("o")}");

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
            FirstActivity = (await _taskLogManager.GetFirstActivityAsync(userId)).ExecutionAtUtc.ToUniversalTime().ToString("o"),
            LastActivity = (await _taskLogManager.GetLastActivityAsync(userId)).ExecutionAtUtc.ToUniversalTime().ToString("o")
        };

        foreach (var task in taskList.Tasks)
        {
            Console.WriteLine($"Task ID: {task.Id}, Title: {task.Title}, Completed: {task.IsCompleted}, Created At: {task.CreatedAt}");
        }

        return taskList;
    }
}

