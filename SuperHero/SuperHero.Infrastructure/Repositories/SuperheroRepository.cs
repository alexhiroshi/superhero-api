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
    public class SuperheroRepository : Repository<Superhero>, ISuperheroRepository
    {
        public readonly DbConnection _connection;

        public SuperheroRepository(SuperheroContext context)
            : base(context)
        {
            _connection = context.Database.GetDbConnection();
        }

        public async Task<Superhero> GetByNameAsync(string name)
        {
            return await _connection
                .QueryFirstOrDefaultAsync<Superhero>("SELECT TOP 1 Id, Name, Alias, ProtectionAreaId, DateCreated FROM Superhero WHERE Name = @name", new { name });
        }

        public async Task<Superhero> GetAsync(Guid id)
        {
            return await _connection
                .QueryFirstOrDefaultAsync<Superhero>("SELECT TOP 1 Id, Name, Alias, ProtectionAreaId, DateCreated FROM Superhero WHERE Id = @id", new { id });
        }

        public async Task<IEnumerable<Superhero>> GetAllAsync(int skip = 0, int take = 20)
        {
            return await _connection
                .QueryAsync<Superhero>(@"SELECT
                                            Id
                                            , Name
                                            , Alias
                                            , ProtectionAreaId
                                            , DateCreated
                                        FROM Superhero
                                            ORDER BY Name
                                            OFFSET (@skip * @take)
                                            ROWS FETCH NEXT @take ROWS ONLY",
                                      new { skip, take });
        }

        public async Task<IEnumerable<Superhero>> GetByProtectionArea(Guid protectionAreaId)
        {
            return await _connection
                .QueryAsync<Superhero>(@"SELECT
                                            Id
                                            , Name
                                            , Alias
                                            , ProtectionAreaId
                                        FROM Superhero
                                        WHERE ProtectionAreaId = @protectionAreaId
                                            ORDER BY Name",
                                   new { protectionAreaId });
        }
    }
}