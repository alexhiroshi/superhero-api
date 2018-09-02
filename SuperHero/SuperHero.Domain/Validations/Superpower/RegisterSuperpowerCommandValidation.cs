using SuperHero.Domain.Commands.Superpower;

namespace SuperHero.Domain.Validations.Superpower
{
    public class RegisterSuperpowerCommandValidation : SuperpowerValidation<RegisterSuperpowerCommand>
    {
        public RegisterSuperpowerCommandValidation()
        {
            ValidateName();
            ValidateDescription();
        }
    }
}
