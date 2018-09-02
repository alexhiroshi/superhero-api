using System.Threading.Tasks;
using MediatR;
using SuperHero.Domain.Bus;
using SuperHero.Domain.Commands;
using SuperHero.Domain.Events;

namespace SuperHero.CrossCutting.Bus
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            return Publish(@event);
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return Publish(command);
        }

        private Task Publish<T>(T command) where T : Message
        {
            return _mediator.Publish(command);
        }
    }
}
