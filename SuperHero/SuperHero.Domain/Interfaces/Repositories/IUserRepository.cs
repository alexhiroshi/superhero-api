using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Models;

namespace SuperHero.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllAsync(int skip = 0, int take = 20);
        Task<UserModel> ValidateUserAsync(string username, string password);
    }
}
