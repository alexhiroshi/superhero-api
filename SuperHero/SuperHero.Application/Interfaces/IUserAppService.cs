using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperHero.Application.ViewModels;

namespace SuperHero.Application
{
    public interface IUserAppService : IDisposable
    {
        Task Add(UserViewModel user);
        Task<UserViewModel> GetAsync(Guid id);
        Task<IEnumerable<UserViewModel>> GetAllAsync(int skip = 0, int take = 20);
        Task Update(UserViewModel user);
        Task Remove(Guid id);
        Task<string> Login(string username, string password);
    }
}
