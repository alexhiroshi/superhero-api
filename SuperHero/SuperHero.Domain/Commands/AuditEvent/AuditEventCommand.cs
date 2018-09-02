using System;
using SuperHero.Domain.Enums;

namespace SuperHero.Domain.Commands.AuditEvent
{
    public abstract class AuditEventCommand : Command
    {
        public string Entity { get; protected set; }
        public Guid EntityId { get; protected set; }
        public string Username { get; protected set; }
        public AuditEventAction Action { get; protected set; }
    }
}
