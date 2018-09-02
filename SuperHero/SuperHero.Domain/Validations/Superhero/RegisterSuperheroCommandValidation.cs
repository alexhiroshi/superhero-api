using SuperHero.Domain.Commands.Superhero;

namespace SuperHero.Domain.Validations.Superhero
{
    public class RegisterSuperheroCommandValidation : SuperheroValidation<RegisterSuperheroCommand>
    {
        public RegisterSuperheroCommandValidation()
        {
            ValidateName();
            ValidateAlias();
            ValidateProtectionAreaId();
        }
    }
}