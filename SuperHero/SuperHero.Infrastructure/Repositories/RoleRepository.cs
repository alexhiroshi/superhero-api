using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Repositories;
using SuperHero.Infrastructure.Context;

namespace SuperHero.Infrastructure.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public readonly DbConnection _connection;

        public RoleRepository(SuperheroContext context)
            : base(context)
        {
            _connection = context.Database.GetDbConnection();
        }

        public async Task<IEnumerable<Role>> GetAllAsync(int skip = 0, int take = 20)
        {
            return await _connection
                            .QueryAsync<Role>(@"SELECT
                                                Id
                                                , Name
                                                , DateCreated
                                            FROM Role
                                                ORDER BY Name
                                                OFFSET (@skip * @take)
                                                ROWS FETCH NEXT @take ROWS ONLY",
                                        new { skip, take });
        }
    }
}
