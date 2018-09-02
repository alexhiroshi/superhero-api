using System.Threading.Tasks;
using SuperHero.Domain.Entities;

namespace SuperHero.Domain.Services
{
    public interface IAuditEventService
    {
        Task Subscribe(AuditEvent auditEvent);
    }
}
