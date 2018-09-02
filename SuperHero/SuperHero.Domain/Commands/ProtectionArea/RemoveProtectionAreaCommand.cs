using System;
using SuperHero.Domain.Validations.Superhero;

namespace SuperHero.Domain.Commands.ProtectionArea
{
    public class RemoveProtectionAreaCommand : ProtectionAreaCommand
    {
        public RemoveProtectionAreaCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveProtectionAreaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
