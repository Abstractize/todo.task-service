using Data.Context;
using Data.Entities;
using Data.Repositories.Contracts;

namespace Data.Repositories.Implementation;

internal class TaskItemRepository(DatabaseContext context) : BaseRepository<TaskItem>(context), ITaskItemRepository
{ }