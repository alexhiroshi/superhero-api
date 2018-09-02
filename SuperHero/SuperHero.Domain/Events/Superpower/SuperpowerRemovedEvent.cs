using System;

namespace SuperHero.Domain.Events.Superpower
{
    public class SuperpowerRemovedEvent : Event
    {
        public Guid Id { get; private set; }

        public SuperpowerRemovedEvent(Guid id)
        {
            Id = id;
        }
    }
}
