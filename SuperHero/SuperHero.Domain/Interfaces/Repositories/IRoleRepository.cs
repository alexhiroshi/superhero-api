using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperHero.Domain.Entities;

namespace SuperHero.Domain.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<IEnumerable<Role>> GetAllAsync(int skip = 0, int take = 20);
    }
}
