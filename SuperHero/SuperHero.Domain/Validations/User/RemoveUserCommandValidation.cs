using System;
using SuperHero.Domain.Commands.User;

namespace SuperHero.Domain.Validations.User
{
	public class RemoveUserCommandValidation : UserValidation<RemoveUserCommand>
    {
        public RemoveUserCommandValidation()
        {
            ValidateId();
        }
    }
}
