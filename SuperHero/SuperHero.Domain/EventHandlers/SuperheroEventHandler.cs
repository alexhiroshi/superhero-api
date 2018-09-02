using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperHero.Domain.Cache;
using SuperHero.Domain.Events.Superhero;

namespace SuperHero.Domain.EventHandlers
{
    public class SuperheroEventHandler : INotificationHandler<SuperheroUpdatedEvent>,
                                         INotificationHandler<SuperheroRemovedEvent>
    {
        private readonly ICacheManager _cacheManager;

        public SuperheroEventHandler(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task Handle(SuperheroUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await _cacheManager.UpdateAsync($"superheroGetAsync_{notification.Id}", notification);
        }

        public async Task Handle(SuperheroRemovedEvent notification, CancellationToken cancellationToken)
        {
            await _cacheManager.RemoveAsync($"superheroGetAsync_{notification.Id}");
        }
    }
}
