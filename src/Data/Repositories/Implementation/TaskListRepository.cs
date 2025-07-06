using Data.Context;
using Data.Entities;
using Data.Repositories.Contracts;

namespace Data.Repositories.Implementation;

internal class TaskListRepository(DatabaseContext context) : BaseRepository<TaskList>(context), ITaskListRepository
{ }