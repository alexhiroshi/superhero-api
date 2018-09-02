using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperHero.Domain.Cache;
using SuperHero.Domain.Events.Superpower;

namespace SuperHero.Domain.EventHandlers
{
    public class SuperpowerEventHandler: INotificationHandler<SuperpowerUpdatedEvent>,
                                         INotificationHandler<SuperpowerRemovedEvent>
    {
        private readonly ICacheManager _cacheManager;

        public SuperpowerEventHandler(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task Handle(SuperpowerUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await _cacheManager.UpdateAsync($"superpowerGetAsync_{notification.Id}", notification);
        }

        public async Task Handle(SuperpowerRemovedEvent notification, CancellationToken cancellationToken)
        {
            await _cacheManager.RemoveAsync($"superpowerGetAsync_{notification.Id}");
        }
    }
}
