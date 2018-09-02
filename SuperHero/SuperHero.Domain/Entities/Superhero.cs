using System;
using System.Collections.Generic;

namespace SuperHero.Domain.Entities
{
    public class Superhero : Base
    {
        public string Name { get; private set; }
        public string Alias { get; private set; }
        public Guid ProtectionAreaId { get; private set; }
        public virtual ProtectionArea ProtectionArea { get; private set; }
        public virtual ICollection<SuperheroSuperpower> SuperheroSuperpower { get; private set; } = new HashSet<SuperheroSuperpower>();

        protected Superhero() {}

        public Superhero(Guid id, string name, string alias, Guid protectionAreaId, DateTime dateCreated)
        {
            Id = id;
            Name = name;
            Alias = alias;
            ProtectionAreaId = protectionAreaId;
            DateCreated = dateCreated;
        }
    }
}
