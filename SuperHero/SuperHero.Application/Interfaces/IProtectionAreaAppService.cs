using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperHero.Application.ViewModels;

namespace SuperHero.Application
{
    public interface IProtectionAreaAppService : IDisposable
    {
        Task Add(ProtectionAreaViewModel protectionArea);
        Task<ProtectionAreaViewModel> GetAsync(Guid id);
        Task<IEnumerable<ProtectionAreaViewModel>> GetAllAsync(int skip = 0, int take = 20);
        Task Update(ProtectionAreaViewModel protectionArea);
        Task Remove(Guid id);

    }
}
