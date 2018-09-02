using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperHero.Application.ViewModels;

namespace SuperHero.Application
{
    public interface IRoleAppService : IDisposable
    {
        Task<IEnumerable<RoleViewModel>> GetAllAsync(int skip = 0, int take = 20);
    }
}
