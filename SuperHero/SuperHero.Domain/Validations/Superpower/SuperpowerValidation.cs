using System;
using FluentValidation;
using SuperHero.Domain.Commands.Superpower;

namespace SuperHero.Domain.Validations.Superpower
{
    public class SuperpowerValidation<T> : AbstractValidator<T> where T : SuperpowerCommand
    {
        protected void ValidateId()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid Superpower");
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

        protected void ValidateDescription()
        {
            RuleFor(x => x.Description)
                .MaximumLength(200)
                .WithMessage("The description must be a maximum 200 characters");
        }
    }
}
