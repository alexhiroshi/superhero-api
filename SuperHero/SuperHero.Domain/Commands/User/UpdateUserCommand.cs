using System;
using System.Collections.Generic;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Validations.User;

namespace SuperHero.Domain.Commands.User
{
	public class UpdateUserCommand : UserCommand
    {
        public UpdateUserCommand(Guid id, string password, ICollection<UserRole> userRoles)
        {
            Id = id;
            Password = password;
            UserRoles = userRoles;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
