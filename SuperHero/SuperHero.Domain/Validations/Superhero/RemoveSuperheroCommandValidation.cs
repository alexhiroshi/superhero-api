using SuperHero.Domain.Commands.Superhero;

namespace SuperHero.Domain.Validations.Superhero
{
    public class RemoveSuperheroCommandValidation : SuperheroValidation<RemoveSuperheroCommand>
    {
        public RemoveSuperheroCommandValidation()
        {
            ValidateId();
        }
    }
}
