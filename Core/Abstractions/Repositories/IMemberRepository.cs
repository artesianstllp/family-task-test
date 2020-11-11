using System;
using Domain.DataModels;

namespace Core.Abstractions.Repositories
{
    public interface IMemberRepository : IBaseRepository<Guid, Member, IMemberRepository> { }
}