using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperHero.Domain.Bus;
using SuperHero.Domain.Commands.Superpower;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Events.Superpower;
using SuperHero.Domain.Notifications;
using SuperHero.Domain.Repositories;

namespace SuperHero.Domain.CommandHandlers
{
    public class SuperpowerCommandHandler : CommandHandler<Superpower>,
        INotificationHandler<RegisterSuperpowerCommand>,
        INotificationHandler<RemoveSuperpowerCommand>,
        INotificationHandler<UpdateSuperpowerCommand>
    {
        private readonly ISuperpowerRepository _superpowerRepository;
        private readonly IMediatorHandler _bus;

        public SuperpowerCommandHandler(ISuperpowerRepository superpowerRepository,
                                        IMediatorHandler bus,
                                        INotificationHandler<DomainNotification> notifications)
            : base(superpowerRepository, bus, notifications)
        {
            _superpowerRepository = superpowerRepository;
            _bus = bus;
        }

        public async Task Handle(RegisterSuperpowerCommand notification, CancellationToken cancellationToken)
        {
            if (!notification.IsValid())
            {
                NotifyValidationErrors(notification);
                return;
            }

            if (await _superpowerRepository.GetByNameAsync(notification.Name) != null)
            {
                await _bus.RaiseEvent(new DomainNotification(notification.MessageType, "There's already a super hero with this name"));
                return;
            }

            var superpower = new Superpower(Guid.NewGuid(),
                                            notification.Name,
                                            notification.Description,
                                           DateTime.Now);

            _superpowerRepository.Add(superpower);

            if (await CommitAsync()) { }
        }

        public async Task Handle(RemoveSuperpowerCommand notification, CancellationToken cancellationToken)
        {
            if (!notification.IsValid())
            {
                NotifyValidationErrors(notification);
                return;
            }

            _superpowerRepository.Remove(notification.Id);

            if (await CommitAsync())
            {
                var superpowerEvent = new SuperpowerRemovedEvent(notification.Id);
                await _bus.RaiseEvent(superpowerEvent);
            }
        }

        public async Task Handle(UpdateSuperpowerCommand notification, CancellationToken cancellationToken)
        {
            if (!notification.IsValid())
            {
                NotifyValidationErrors(notification);
                return;
            }

            var existingSuperpower = await _superpowerRepository.GetByNameAsync(notification.Name);
            if (existingSuperpower != null && existingSuperpower.Id != notification.Id)
            {
                await _bus.RaiseEvent(new DomainNotification(notification.MessageType, "There's already a superpower with this name"));
                return;
            }

            existingSuperpower = await _superpowerRepository.GetAsync(notification.Id);
            var superpower = new Superpower(notification.Id,
                                            notification.Name,
                                            notification.Description,
                                            existingSuperpower.DateCreated);

            _superpowerRepository.Update(superpower);

            if (await CommitAsync())
            {
                var superpowerEvent = new SuperpowerUpdatedEvent(superpower.Id, superpower.Name, superpower.Description);
                await _bus.RaiseEvent(superpowerEvent);
            }
        }
    }
}
