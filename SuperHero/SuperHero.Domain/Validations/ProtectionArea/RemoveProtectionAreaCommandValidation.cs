using SuperHero.Domain.Commands.ProtectionArea;

namespace SuperHero.Domain.Validations.Superhero
{
    public class RemoveProtectionAreaCommandValidation : ProtectionAreaValidation<RemoveProtectionAreaCommand>
    {
        public RemoveProtectionAreaCommandValidation()
        {
            ValidateId();
        }
    }
}