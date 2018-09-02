using SuperHero.Domain.Commands.ProtectionArea;

namespace SuperHero.Domain.Validations.Superhero
{
    public class RegisterProtectionAreaCommandValidation : ProtectionAreaValidation<RegisterProtectionAreaCommand>
    {
        public RegisterProtectionAreaCommandValidation()
        {
            ValidateName();
        }
    }
}