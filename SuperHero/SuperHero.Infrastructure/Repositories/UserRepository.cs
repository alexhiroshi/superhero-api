using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Slapper;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Models;
using SuperHero.Domain.Repositories;
using SuperHero.Infrastructure.Context;

namespace SuperHero.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public readonly DbConnection _connection;

        public UserRepository(SuperheroContext context)
            : base(context)
        {
            _connection = context.Database.GetDbConnection();
        }

        public async Task<User> GetAsync(Guid id)
        {
            var user = await _connection
                .QueryFirstOrDefaultAsync<User>(@"SELECT TOP 1
                                                    U.Id
                                                    , U.Username
                                                    , U.DateCreated
                                                FROM [User] U
                                                WHERE U.Id = @id",
                                                new { id });

            return user;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var user = await _connection
                .QueryFirstOrDefaultAsync<User>(@"SELECT TOP 1
                                                    U.Id
                                                    , U.Username
                                                    , U.DateCreated
                                                FROM [User] U
                                                WHERE U.Username = @username",
                                                new { username });

            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync(int skip = 0, int take = 20)
        {
            var users = await _connection
                            .QueryAsync<User>(@"SELECT
                                                    U.Id
                                                    , U.Username
                                                    , U.DateCreated
                                                FROM [User] U
                                                    ORDER BY Namename
                                                    OFFSET (@skip * @take)
                                                    ROWS FETCH NEXT @take ROWS ONLY",
                                            new { skip, take });

            return users;
        }

        public async Task<UserModel> ValidateUserAsync(string username, string password)
        {
            var user = await _connection
                .QueryFirstOrDefaultAsync<object>(@"SELECT TOP 1
                                                    U.Id
                                                    , U.Username
                                                    , U.DateCreated
                                                    , R.Id Roles_Id
                                                    , R.Name Roles_Name
                                                FROM [User] U
                                                    INNER JOIN [UserRole] UR ON UR.UserId = U.Id
                                                    INNER JOIN [Role] R ON R.Id = UR.RoleId
                                                WHERE Username = @username AND Password = @password",
                                                new { username, password });

            AutoMapper.Configuration.AddIdentifier(typeof(UserModel), "Id");
            AutoMapper.Configuration.AddIdentifier(typeof(RoleModel), "Id");

            return AutoMapper.MapDynamic<UserModel>(user) as UserModel;
        }
    }
}
