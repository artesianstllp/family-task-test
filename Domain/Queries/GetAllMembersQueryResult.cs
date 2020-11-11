using System.Collections.Generic;
using Domain.ViewModel;

namespace Domain.Queries
{
    public class GetAllMembersQueryResult
    {
        public IEnumerable<MemberVm> Payload { get; set; }
    }

}
