using System;
using System.Collections.Generic;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Validations.User;

namespace SuperHero.Domain.Commands.User
{
    public class RegisterUserCommand : UserCommand
    {
        public RegisterUserCommand(string username, string password, ICollection<UserRole> userRoles)
        {
            Username = username;
            Password = password;
            UserRoles = userRoles;
            DateCreated = DateTime.Now;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterUserCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
