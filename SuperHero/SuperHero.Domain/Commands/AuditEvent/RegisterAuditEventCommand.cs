using System;
using SuperHero.Domain.Enums;
using SuperHero.Domain.Validations.AuditEvent;

namespace SuperHero.Domain.Commands.AuditEvent
{
    public class RegisterAuditEventCommand : AuditEventCommand
    {
        public RegisterAuditEventCommand(string entity, Guid entityId, string username, AuditEventAction action)
        {
            Entity = entity;
            EntityId = entityId;
            Username = username;
            Action = action;
            DateCreated = DateTime.Now;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterAuditEventCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
