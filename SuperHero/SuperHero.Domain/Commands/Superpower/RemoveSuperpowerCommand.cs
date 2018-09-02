using System;
using SuperHero.Domain.Validations.Superpower;

namespace SuperHero.Domain.Commands.Superpower
{
    public class RemoveSuperpowerCommand : SuperpowerCommand
    {
        public RemoveSuperpowerCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveSuperpowerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
