using System.Collections.Generic;
using System.Threading.Tasks;
using SuperHero.Application.ViewModels;

namespace SuperHero.Application
{
    public interface IAuditEventAppService
    {
        Task Add(AuditEventViewModel auditEvent);
        Task<IEnumerable<AuditEventViewModel>> GetAllAsync(int skip = 0, int take = 20);
    }
}
