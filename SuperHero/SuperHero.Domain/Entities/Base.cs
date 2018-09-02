using System;

namespace SuperHero.Domain.Entities
{
    public class Base
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
