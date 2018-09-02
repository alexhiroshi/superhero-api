using SuperHero.Domain.Commands.AuditEvent;

namespace SuperHero.Domain.Validations.AuditEvent
{
    public class RegisterAuditEventCommandValidation : AuditEventValidation<RegisterAuditEventCommand>
    {
        public RegisterAuditEventCommandValidation()
        {
            ValidateEntity();
            ValidateEntityId();
            ValidateUsername();
        }
    }
}
