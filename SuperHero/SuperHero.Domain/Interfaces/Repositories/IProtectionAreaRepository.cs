using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperHero.Domain.Entities;

namespace SuperHero.Domain.Repositories
{
    public interface IProtectionAreaRepository : IRepository<ProtectionArea>
    {
        Task<ProtectionArea> GetAsync(Guid id);
        Task<IEnumerable<ProtectionArea>> GetAllAsync(int skip = 0, int take = 20);
    }
}
