using SuperHero.Domain.Commands.Superhero;

namespace SuperHero.Domain.Validations.Superhero
{
    public class UpdateSuperheroCommandValidation : SuperheroValidation<UpdateSuperheroCommand>
    {
        public UpdateSuperheroCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidateAlias();
            ValidateProtectionAreaId();
        }
    }
}
