using System;

namespace SuperHero.Domain.Events.Superhero
{
    public class SuperheroRemovedEvent : Event
    {
        public Guid Id { get; private set; }

        public SuperheroRemovedEvent(Guid id)
        {
            Id = id;
        }
    }
}
