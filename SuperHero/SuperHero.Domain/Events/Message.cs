using System;
using MediatR;

namespace SuperHero.Domain.Events
{
    public abstract class Message : INotification, IRequest
	{
		public string MessageType { get; protected set; }
		public Guid AggregateId { get; protected set; }

		protected Message()
		{
			MessageType = GetType().Name;
		}
    }
}
