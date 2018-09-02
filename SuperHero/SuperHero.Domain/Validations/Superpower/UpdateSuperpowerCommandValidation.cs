using SuperHero.Domain.Commands.Superpower;

namespace SuperHero.Domain.Validations.Superpower
{
	public class UpdateSuperpowerCommandValidation : SuperpowerValidation<UpdateSuperpowerCommand>
    {
        public UpdateSuperpowerCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidateDescription();
        }
    }
}
