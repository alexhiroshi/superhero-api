using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperHero.Application.ViewModels;

namespace SuperHero.Application
{
    public interface ISuperheroAppService : IDisposable
    {
        Task Add(SuperheroViewModel superpower);
        Task<SuperheroViewModel> GetAsync(Guid id);
        Task<IEnumerable<SuperheroViewModel>> GetAllAsync(int skip = 0, int take = 20);
        Task Update(SuperheroViewModel superpower);
        Task Remove(Guid id);
    }
}
