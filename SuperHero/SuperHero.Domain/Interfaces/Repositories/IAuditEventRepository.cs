using System.Collections.Generic;
using System.Threading.Tasks;
using SuperHero.Domain.Entities;

namespace SuperHero.Domain.Repositories
{
    public interface IAuditEventRepository : IRepository<AuditEvent>
    {
        Task<IEnumerable<AuditEvent>> GetAllAsync(int skip = 0, int take = 20);
    }
}
