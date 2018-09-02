using System;
using System.Collections.Generic;

namespace SuperHero.Domain.Entities
{
    public class ProtectionArea : Base
    {
        public string Name { get; private set; }
        public double? Lat { get; private set; }
        public double? Long { get; private set; }
        public double? Radius { get; private set; }
        public virtual ICollection<Superhero> Superhero { get; private set; } = new HashSet<Superhero>();

        protected ProtectionArea() { }

        public ProtectionArea(Guid id, string name, double? latitude, double? longitude, double? radius)
        {
            Id = id;
            Name = name;
            Lat = latitude;
            Long = longitude;
            Radius = radius;
        }
    }
}
