using System;
using System.Collections.Generic;

namespace SuperHero.Domain.Entities
{
    public class Role : Base
    {
        public string Name { get; private set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

        protected Role() {}

        public Role(Guid id, string name, DateTime dateCreated)
        {
            Id = id;
            Name = name;
            DateCreated = dateCreated;
        }
    }
}