using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperHero.Domain.Entities;

namespace SuperHero.Domain.Repositories
{
    public interface ISuperheroRepository : IRepository<Superhero>
    {
        Task<Superhero> GetByNameAsync(string name);
        Task<Superhero> GetAsync(Guid id);
        Task<IEnumerable<Superhero>> GetAllAsync(int skip = 0, int take = 20);
        Task<IEnumerable<Superhero>> GetByProtectionArea(Guid protectionAreaId);
    }
}
