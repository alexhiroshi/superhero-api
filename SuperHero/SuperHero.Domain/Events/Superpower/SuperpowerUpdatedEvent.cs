using System;

namespace SuperHero.Domain.Events.Superpower
{
    public class SuperpowerUpdatedEvent : Event
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public SuperpowerUpdatedEvent(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
