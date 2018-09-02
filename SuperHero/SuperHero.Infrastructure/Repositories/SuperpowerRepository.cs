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
    public class SuperpowerRepository : Repository<Superpower>, ISuperpowerRepository
    {
        public readonly DbConnection _connection;

        public SuperpowerRepository(SuperheroContext context)
            : base(context)
        {
            _connection = context.Database.GetDbConnection();
        }

        public async Task<Superpower> GetAsync(Guid id)
        {
            return await _connection
                .QueryFirstOrDefaultAsync<Superpower>("SELECT TOP 1 Id, Name, Description, DateCreated FROM Superpower WHERE Id = @id", new { id });
        }

        public async Task<Superpower> GetByNameAsync(string name)
        {
            return await _connection
                .QueryFirstOrDefaultAsync<Superpower>("SELECT TOP 1 Id, Name, Description, DateCreated FROM Superpower WHERE Name = @name", new { name });
        }

        public async Task<IEnumerable<Superpower>> GetAllAsync(int skip = 0, int take = 20)
        {
            return await _connection
                .QueryAsync<Superpower>(@"SELECT
                                            Id
                                            , Name
                                            , Description
                                            , DateCreated
                                        FROM Superpower
                                            ORDER BY Name
                                            OFFSET (@skip * @take)
                                            ROWS FETCH NEXT @take ROWS ONLY",
                                      new { skip, take });
        }
    }
}