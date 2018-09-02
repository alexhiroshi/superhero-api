using System;
using FluentValidation;
using SuperHero.Domain.Commands.ProtectionArea;

namespace SuperHero.Domain.Validations.Superhero
{
    public class ProtectionAreaValidation<T> : AbstractValidator<T> where T : ProtectionAreaCommand
    {
        protected void ValidateId()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid protection area");
        }

        protected void ValidateName()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Name is required")
                .Length(1, 100)
                .WithMessage("The name must be 1-100 characters");
        }
    }
}
