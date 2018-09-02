using System;
using System.Linq;
using FluentValidation;
using SuperHero.Domain.Commands.User;

namespace SuperHero.Domain.Validations.User
{
    public class UserValidation<T> : AbstractValidator<T> where T : UserCommand
    {
        protected void ValidateId()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid user");
        }

        protected void ValidateUsername()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .NotNull()
                .WithMessage("Username is required")
                .Length(1, 100)
                .WithMessage("The username must be 1-100 characters");
        }

        protected void ValidatePassword()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(8)
                .WithMessage("The password must contain a minimum of 8 characters");
        }

        protected void ValidateRoles()
        {
            RuleFor(x => x.UserRoles)
                .Must((arg) => arg.Any())
                .WithMessage("Select one or more roles");
        }
    }
}
