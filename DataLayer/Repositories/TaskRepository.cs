using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Abstractions.Repositories;
using Domain.DataModels;
using Domain.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class TaskRepository : BaseRepository<Guid, Tasks, TaskRepository>, ITaskRepository
    {
        private readonly FamilyTaskContext _db;

        public TaskRepository(FamilyTaskContext context) : base(context)
        {
            _db = context;
        }

        ITaskRepository IBaseRepository<Guid, Tasks, ITaskRepository>.NoTrack()
        {
            return base.NoTrack();
        }

        ITaskRepository IBaseRepository<Guid, Tasks, ITaskRepository>.Reset()
        {
            return base.Reset();
        }

        // Method to fetch all tasks with member avatar added in Task Repo for code brevity
        public async Task<IEnumerable<TaskVm>> GetTasksWithMemberAvatar(CancellationToken cancellationToken = default)
        {
            return await (from task in _db.Tasks
                join member in _db.Members on task.AssignedToId equals member.Id into Assigned
                from assignedMember in Assigned.DefaultIfEmpty()
                select new TaskVm
                {
                    Id = task.Id,
                    Subject = task.Subject,
                    IsComplete = task.IsComplete,
                    AssignedToId = task.AssignedToId,
                    Avatar = assignedMember.Avatar ?? ""
                }).ToListAsync(cancellationToken);
        }
    }
}