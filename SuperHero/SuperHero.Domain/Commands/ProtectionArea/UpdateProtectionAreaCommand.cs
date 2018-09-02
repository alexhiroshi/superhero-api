using System;
using SuperHero.Domain.Validations.Superhero;

namespace SuperHero.Domain.Commands.ProtectionArea
{
    public class UpdateProtectionAreaCommand : ProtectionAreaCommand
    {
        public UpdateProtectionAreaCommand(Guid id, string name, double? latitude, double? longitude, double? radius)
        {
            Id = id;
            Name = name;
            Lat = latitude;
            Long = longitude;
            Radius = radius;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateProtectionAreaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
