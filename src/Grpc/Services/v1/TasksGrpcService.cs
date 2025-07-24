using Grpc.Core;
using Analytics.Protos;
using Managers.Contracts;
using Microsoft.AspNetCore.Authorization;
using Services.Common.Identity;
using System.Globalization;

namespace Grpc.Services.v1;

[Authorize]
public class TasksGrpcService(ITaskItemManager taskItemManager, ITaskLogManager taskLogManager, IIdentityService identityService) : TaskAnalyticsService.TaskAnalyticsServiceBase
{
    private readonly IIdentityService _identityService = identityService;
    private readonly ITaskItemManager _taskItemManager = taskItemManager;
    private readonly ITaskLogManager _taskLogManager = taskLogManager;

    public override async Task<TaskList> GetUserTasks(GetTasksRequest request, ServerCallContext context)
    {
        Guid userId = _identityService.UserId ??
            throw new RpcException(new Status(StatusCode.Unauthenticated, "User is not authenticated"));
        IEnumerable<Models.TaskItem> tasks = await _taskItemManager.GetAsync(userId);

        TaskList taskList = new();
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
        Console.WriteLine($"==> {_identityService.UserId} requested weekly tasks for {request.DayUtc}");
        Guid userId = _identityService.UserId ??
            throw new RpcException(new Status(StatusCode.Unauthenticated, "User is not authenticated"));
        DateTime weekStartUtc = DateTime.Parse(request.DayUtc, null, DateTimeStyles.RoundtripKind);

        Console.WriteLine($"==> Parsed to Week Start: {weekStartUtc.Kind} {weekStartUtc.ToString("o", CultureInfo.InvariantCulture)}");

        IEnumerable<Models.TaskItem> tasks = await _taskItemManager.GetWeeklyTasksAsync(userId, weekStartUtc);

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

        return taskList;
    }
}

