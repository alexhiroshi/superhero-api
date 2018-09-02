using System;
using FluentValidation;
using SuperHero.Domain.Commands.Superhero;

namespace SuperHero.Domain.Validations.Superhero
{
    public class SuperheroValidation<T> : AbstractValidator<T> where T : SuperheroCommand
    {
        protected void ValidateId()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid Superhero");
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

        protected void ValidateAlias()
        {
            RuleFor(x => x.Alias)
                .Length(1, 100)
                .WithMessage("The alias must be 1-100 characters");
        }

        protected void ValidateProtectionAreaId()
        {
            RuleFor(x => x.ProtectionAreaId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid Protection Area");
        }
    }
}
