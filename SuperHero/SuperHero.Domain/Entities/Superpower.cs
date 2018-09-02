using System;
using System.Collections.Generic;

namespace SuperHero.Domain.Entities
{
    public class Superpower : Base
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public virtual ICollection<SuperheroSuperpower> SuperheroSuperpower { get; private set; } = new HashSet<SuperheroSuperpower>();

        protected Superpower() {}

        public Superpower(Guid id, string name, string description, DateTime dateCreated)
        {
            Id = id;
            Name = name;
            Description = description;
            DateCreated = dateCreated;
        }
    }
}
