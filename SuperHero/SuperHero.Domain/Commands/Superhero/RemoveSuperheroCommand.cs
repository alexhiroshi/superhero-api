using System;
using SuperHero.Domain.Validations.Superhero;

namespace SuperHero.Domain.Commands.Superhero
{
    public class RemoveSuperheroCommand : SuperheroCommand
    {
        public RemoveSuperheroCommand(Guid id, string username)
        {
            Id = id;
            Username = username;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveSuperheroCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
