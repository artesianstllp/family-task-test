using Domain.ViewModel;
using System.Collections.Generic;

namespace Domain.Queries
{
    public class GetAllTaskQueryResult
    {
        public IEnumerable<TaskVm> Payload { get; set; }
    }
}
