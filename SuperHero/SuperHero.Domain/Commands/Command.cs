using System;
using FluentValidation.Results;
using SuperHero.Domain.Events;

namespace SuperHero.Domain.Commands
{
    public abstract class Command : Message
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        public abstract bool IsValid();

        protected Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
