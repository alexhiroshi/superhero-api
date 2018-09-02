using SuperHero.Domain.Commands.ProtectionArea;

namespace SuperHero.Domain.Validations.Superhero
{
    public class UpdateProtectionAreaCommandValidation : ProtectionAreaValidation<UpdateProtectionAreaCommand>
    {
        public UpdateProtectionAreaCommandValidation()
        {
            ValidateId();
            ValidateName();
        }
    }
}