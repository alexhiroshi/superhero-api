using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperHero.Domain.Entities;

namespace SuperHero.Domain.Repositories
{
    public interface ISuperpowerRepository : IRepository<Superpower>
    {
        Task<Superpower> GetAsync(Guid id);
        Task<Superpower> GetByNameAsync(string name);
        Task<IEnumerable<Superpower>> GetAllAsync(int skip = 0, int take = 20);
    }
}
