using System;
using SuperHero.Domain.Validations.Superhero;

namespace SuperHero.Domain.Commands.Superhero
{
    public class RegisterSuperheroCommand : SuperheroCommand
    {
        public RegisterSuperheroCommand(string name, string alias, Guid protectionAreaId, string username)
        {
            Name = name;
            Alias = alias;
            ProtectionAreaId = protectionAreaId;
            Username = username;
            DateCreated = DateTime.Now;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterSuperheroCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
