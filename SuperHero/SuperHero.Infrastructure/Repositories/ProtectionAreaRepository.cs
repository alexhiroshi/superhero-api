using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Repositories;
using SuperHero.Infrastructure.Context;
using SuperHero.Infrastructure.Repositories;

namespace SuperHero.Infrastructure
{
    public class ProtectionAreaRepository : Repository<ProtectionArea>, IProtectionAreaRepository
    {
        public readonly DbConnection _connection;

        public ProtectionAreaRepository(SuperheroContext context)
            : base(context)
        {
            _connection = context.Database.GetDbConnection();
        }

        public async Task<ProtectionArea> GetAsync(Guid id)
        {
            return await _connection
                .QueryFirstOrDefaultAsync<ProtectionArea>(@"SELECT TOP 1
                                                        Id
                                                        , Name
                                                        , Lat
                                                        , Long
                                                        , Radius
                                                        FROM ProtectionArea
                                                        WHERE Id = @id", new { id });
        }

        public async Task<IEnumerable<ProtectionArea>> GetAllAsync(int skip = 0, int take = 20)
        {
            return await _connection
                .QueryAsync<ProtectionArea>(@"SELECT
                                            Id
                                            , Name
                                            , Lat
                                            , Long
                                            , Radius
                                        FROM ProtectionArea
                                            ORDER BY Name
                                            OFFSET (@skip * @take)
                                            ROWS FETCH NEXT @take ROWS ONLY",
                                      new { skip, take });
        }
    }
}