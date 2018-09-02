using System;
using SuperHero.Domain.Validations.Superpower;

namespace SuperHero.Domain.Commands.Superpower
{
    public class RegisterSuperpowerCommand : SuperpowerCommand
    {
        public RegisterSuperpowerCommand(string name, string description)
        {
            Name = name;
            Description = description;
            DateCreated = DateTime.Now;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterSuperpowerCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
