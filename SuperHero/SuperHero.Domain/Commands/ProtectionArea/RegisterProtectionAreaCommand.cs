using System;
using SuperHero.Domain.Validations.Superhero;

namespace SuperHero.Domain.Commands.ProtectionArea
{
    public class RegisterProtectionAreaCommand : ProtectionAreaCommand
    {
        public RegisterProtectionAreaCommand(string name, double? latitude, double? longitude, double? radius)
        {
            Name = name;
            Lat = latitude;
            Long = longitude;
            Radius = radius;
            DateCreated = DateTime.Now;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterProtectionAreaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
