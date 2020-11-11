using System.Threading.Tasks;
using Domain.Commands;
using Domain.Queries;

namespace Core.Abstractions.Services
{
    public interface IMemberService
    {
        Task<CreateMemberCommandResult> CreateMemberCommandHandler(CreateMemberCommand command);
        Task<UpdateMemberCommandResult> UpdateMemberCommandHandler(UpdateMemberCommand command);
        Task<GetAllMembersQueryResult> GetAllMembersQueryHandler();
    }
}