using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.DataModels;
using Domain.ViewModel;

namespace Core.Abstractions.Repositories
{
    public interface ITaskRepository : IBaseRepository<Guid, Tasks, ITaskRepository>
    {
        Task<IEnumerable<TaskVm>> GetTasksWithMemberAvatar(CancellationToken cancellationToken = default);
    }
}