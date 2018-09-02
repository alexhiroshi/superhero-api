using System;
using SuperHero.Domain.Validations.Superhero;

namespace SuperHero.Domain.Commands.Superhero
{
    public class UpdateSuperheroCommand : SuperheroCommand
    {
        public UpdateSuperheroCommand(Guid id, string name, string alias, Guid protectionAreaId, string username)
        {
            Id = id;
            Name = name;
            Alias = alias;
            ProtectionAreaId = protectionAreaId;
            Username = username;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateSuperheroCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
