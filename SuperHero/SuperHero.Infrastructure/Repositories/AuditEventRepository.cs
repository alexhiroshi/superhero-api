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
    public class AuditEventRepository : Repository<AuditEvent>, IAuditEventRepository
    {
        public readonly DbConnection _connection;

        public AuditEventRepository(SuperheroContext context)
            : base(context)
        {
            _connection = context.Database.GetDbConnection();
        }

        public async Task<IEnumerable<AuditEvent>> GetAllAsync(int skip = 0, int take = 20)
        {
            return await _connection
                .QueryAsync<AuditEvent>(@"SELECT
                                            Id
                                            , Entity
                                            , EntityId
                                            , Username
                                            , Action
                                        FROM AuditEvent
                                            ORDER BY DateCreated DESC
                                            OFFSET (@skip * @take)
                                            ROWS FETCH NEXT @take ROWS ONLY",
                                      new { skip, take });
        }
    }
}
