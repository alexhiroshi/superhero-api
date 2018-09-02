using System;
using FluentValidation;
using SuperHero.Domain.Commands.AuditEvent;

namespace SuperHero.Domain.Validations.AuditEvent
{
    public class AuditEventValidation<T> : AbstractValidator<T> where T : AuditEventCommand
    {
        protected void ValidateId()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid audit event");
        }

        protected void ValidateEntity()
        {
            RuleFor(x => x.Entity)
                .NotEmpty()
                .NotNull()
                .WithMessage("Entity is required")
                .Length(1, 100)
                .WithMessage("The name must be 1-100 characters");
        }

        protected void ValidateEntityId()
        {
            RuleFor(x => x.EntityId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid entity id");
        }

        protected void ValidateUsername()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .NotNull()
                .WithMessage("Username is required")
                .Length(1, 100)
                .WithMessage("The username must be 1-100 characters");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
