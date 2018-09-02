using SuperHero.Domain.Commands.User;

namespace SuperHero.Domain.Validations.User
{
    public class UpdateUserCommandValidation : UserValidation<UpdateUserCommand>
    {
        public UpdateUserCommandValidation()
        {
            ValidateId();
            ValidateUsername();
            ValidateRoles();
        }
    }
}
