using System;

namespace SuperHero.Domain.Events
{
	public abstract class Event : Message
	{
		public DateTime Timestamp { get; private set; }

		protected Event()
		{
			Timestamp = DateTime.Now;
		}
	}
}
