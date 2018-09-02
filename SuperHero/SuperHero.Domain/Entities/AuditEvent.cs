using System;
using SuperHero.Domain.Enums;

namespace SuperHero.Domain.Entities
{
    public class AuditEvent : Base
    {
        public string Entity { get; private set; }
        public Guid EntityId { get; private set; }
        public string Username { get; private set; }
        public int Action { get; private set; }

        protected AuditEvent() { }

        public AuditEvent(Guid id, string entity, Guid entityId, string username, AuditEventAction action)
        {
            Id = id;
            Entity = entity;
            EntityId = entityId;
            Username = username;
            Action = (int)action;
        }
    }
}
