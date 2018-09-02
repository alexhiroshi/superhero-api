using System;
using SuperHero.Domain.Validations.Superpower;

namespace SuperHero.Domain.Commands.Superpower
{
    public class UpdateSuperpowerCommand : SuperpowerCommand
    {
        public UpdateSuperpowerCommand(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateSuperpowerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
