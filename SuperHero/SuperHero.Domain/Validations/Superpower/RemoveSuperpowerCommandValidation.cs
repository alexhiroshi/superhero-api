using SuperHero.Domain.Commands.Superpower;

namespace SuperHero.Domain.Validations.Superpower
{
    public class RemoveSuperpowerCommandValidation : SuperpowerValidation<RemoveSuperpowerCommand>
    {
        public RemoveSuperpowerCommandValidation()
        {
            ValidateId();
        }
    }
}
