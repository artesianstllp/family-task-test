using System.Threading.Tasks;
using Domain.Commands;
using Domain.Queries;

namespace Core.Abstractions.Services
{
    public interface ITaskService
    {
        Task<CreateTaskCommandResult> CreateTaskCommandHandler(CreateTaskCommand command);
        Task<UpdateTaskCommandResult> UpdateTaskCommandHandler(UpdateTaskCommand command);
        Task<GetAllTaskQueryResult> GetAllTasksQueryHandler();
        Task<GetAllTaskQueryResult> GetAllTasksWithMemberAvatarQueryHandler();
    }
}