using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperHero.Domain.Bus;
using SuperHero.Domain.Commands.ProtectionArea;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Notifications;
using SuperHero.Domain.Repositories;

namespace SuperHero.Domain.CommandHandlers
{
    public class ProtectionAreaCommandHandler : CommandHandler<ProtectionArea>,
        INotificationHandler<RegisterProtectionAreaCommand>,
        INotificationHandler<RemoveProtectionAreaCommand>,
        INotificationHandler<UpdateProtectionAreaCommand>
    {
        private readonly ISuperheroRepository _superheroRepository;
        private readonly IProtectionAreaRepository _protectionAreaRepository;
        private readonly IMediatorHandler _bus;

        public ProtectionAreaCommandHandler(ISuperheroRepository superheroRepository,
                                            IProtectionAreaRepository protectionAreaRepository,
                                            IMediatorHandler bus,
                                            INotificationHandler<DomainNotification> notifications)
            : base(protectionAreaRepository, bus, notifications)
        {
            _superheroRepository = superheroRepository;
            _protectionAreaRepository = protectionAreaRepository;
            _bus = bus;
        }

        public async Task Handle(RegisterProtectionAreaCommand notification, CancellationToken cancellationToken)
        {
            if (!notification.IsValid())
            {
                NotifyValidationErrors(notification);
                return;
            }

            var protectionArea = new ProtectionArea(Guid.NewGuid(),
                                                    notification.Name,
                                                    notification.Lat,
                                                    notification.Long,
                                                    notification.Radius);

            _protectionAreaRepository.Add(protectionArea);

            if (await CommitAsync()) { }
        }

        public async Task Handle(RemoveProtectionAreaCommand notification, CancellationToken cancellationToken)
        {
            if (!notification.IsValid())
            {
                NotifyValidationErrors(notification);
                return;
            }

            if (await _superheroRepository.GetByProtectionArea(notification.Id) != null)
            {
                await _bus.RaiseEvent(new DomainNotification(notification.MessageType, "Unable to remove the protection area, it is used by one or more superheroes"));
                return;
            }

            _protectionAreaRepository.Remove(notification.Id);

            await CommitAsync();
        }

        public async Task Handle(UpdateProtectionAreaCommand notification, CancellationToken cancellationToken)
        {
            if (!notification.IsValid())
            {
                NotifyValidationErrors(notification);
                return;
            }

            var protectionArea = new ProtectionArea(notification.Id,
                                                    notification.Name,
                                                    notification.Lat,
                                                    notification.Long,
                                                    notification.Radius);

            _protectionAreaRepository.Update(protectionArea);

            await CommitAsync();
        }
    }
}
