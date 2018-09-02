using System;

namespace SuperHero.Domain.Commands.Superhero
{
    public abstract class SuperheroCommand : Command
    {
        public string Name { get; protected set; }
        public string Alias { get; protected set; }
        public Guid ProtectionAreaId { get; protected set; }
        public string Username { get; protected set; }
    }
}
