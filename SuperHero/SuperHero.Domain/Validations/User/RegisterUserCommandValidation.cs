using System;
using SuperHero.Domain.Commands.User;

namespace SuperHero.Domain.Validations.User
{
    public class RegisterUserCommandValidation : UserValidation<RegisterUserCommand>
    {
        public RegisterUserCommandValidation()
        {
            ValidateUsername();
            ValidatePassword();
            ValidateRoles();
        }
    }
}
