using System;

namespace SuperHero.Domain.Events.Superhero
{
    public class SuperheroUpdatedEvent : Event
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Alias { get; private set; }

        public SuperheroUpdatedEvent(Guid id, string name, string alias)
        {
            Id = id;
            Name = name;
            Alias = alias;
        }
    }
}
