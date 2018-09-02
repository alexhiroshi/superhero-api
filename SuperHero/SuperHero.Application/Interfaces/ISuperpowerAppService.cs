using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperHero.Application.ViewModels;

namespace SuperHero.Application
{
    public interface ISuperpowerAppService : IDisposable
    {
        Task Add(SuperpowerViewModel superhero);
        Task<SuperpowerViewModel> GetAsync(Guid id);
        Task<IEnumerable<SuperpowerViewModel>> GetAllAsync(int skip = 0, int take = 20);
        Task Update(SuperpowerViewModel superhero);
        Task Remove(Guid id);
    }
}
