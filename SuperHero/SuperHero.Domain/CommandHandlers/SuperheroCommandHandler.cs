using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperHero.Domain.Bus;
using SuperHero.Domain.Commands.Superhero;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Enums;
using SuperHero.Domain.Events.Superhero;
using SuperHero.Domain.Notifications;
using SuperHero.Domain.Repositories;
using SuperHero.Domain.Services;

namespace SuperHero.Domain.CommandHandlers
{
    public class SuperheroCommandHandler : CommandHandler<Superhero>,
        INotificationHandler<RegisterSuperheroCommand>,
        INotificationHandler<RemoveSuperheroCommand>,
        INotificationHandler<UpdateSuperheroCommand>
    {
        private readonly ISuperheroRepository _superheroRepository;
        private readonly IProtectionAreaRepository _protectionAreaRepository;
        private readonly IMediatorHandler _bus;
        private readonly IAuditEventService _auditEventService;

        public SuperheroCommandHandler(ISuperheroRepository superheroRepository,
                                       IProtectionAreaRepository protectionAreaRepository,
                                       IMediatorHandler bus,
                                       INotificationHandler<DomainNotification> notifications,
                                       IAuditEventService auditEventService)
            : base(superheroRepository, bus, notifications)
        {
            _superheroRepository = superheroRepository;
            _protectionAreaRepository = protectionAreaRepository;
            _bus = bus;
            _auditEventService = auditEventService;
        }

        public async Task Handle(RegisterSuperheroCommand notification, CancellationToken cancellationToken)
        {
            if (!notification.IsValid())
            {
                NotifyValidationErrors(notification);
                return;
            }

            List<string> validationErrors = new List<string>();
            if (await _superheroRepository.GetByNameAsync(notification.Name) != null)
                validationErrors.Add("There's already a superhero with this name");

            if (await _protectionAreaRepository.GetAsync(notification.ProtectionAreaId) == null)
                validationErrors.Add("Invalid protection area id");

            if (validationErrors.Count > 0)
            {
                foreach (var error in validationErrors)
                    await _bus.RaiseEvent(new DomainNotification(notification.MessageType, error));
                return;
            }

            var superhero = new Superhero(Guid.NewGuid(),
                                          notification.Name,
                                          notification.Alias,
                                          notification.ProtectionAreaId,
                                          DateTime.Now);

            _superheroRepository.Add(superhero);

            if (await CommitAsync())
            {
                await _auditEventService.Subscribe(new AuditEvent(
                    Guid.NewGuid(),
                    nameof(Superhero),
                    superhero.Id,
                    notification.Username,
                    AuditEventAction.Create
                ));
            }
        }

        public async Task Handle(RemoveSuperheroCommand notification, CancellationToken cancellationToken)
        {
            if (!notification.IsValid())
            {
                NotifyValidationErrors(notification);
                return;
            }

            _superheroRepository.Remove(notification.Id);

            if (await CommitAsync())
            {
                var superheroEvent = new SuperheroRemovedEvent(notification.Id);
                await _bus.RaiseEvent(superheroEvent);

                await _auditEventService.Subscribe(new AuditEvent(
                    Guid.NewGuid(),
                    nameof(Superhero),
                    notification.Id,
                    notification.Username,
                    AuditEventAction.Remove
                ));
            }
        }

        public async Task Handle(UpdateSuperheroCommand notification, CancellationToken cancellationToken)
        {
            if (!notification.IsValid())
            {
                NotifyValidationErrors(notification);
                return;
            }

            List<string> validationErrors = new List<string>();
            var existingSuperhero = await _superheroRepository.GetByNameAsync(notification.Name);
            if (existingSuperhero != null && existingSuperhero.Id != notification.Id)
                validationErrors.Add("There's already a superhero with this name");

            if (await _protectionAreaRepository.GetAsync(notification.ProtectionAreaId) == null)
                validationErrors.Add("Invalid protection area id");

            if (validationErrors.Count > 0)
            {
                foreach (var error in validationErrors)
                    await _bus.RaiseEvent(new DomainNotification(notification.MessageType, error));
                return;
            }

            existingSuperhero = await _superheroRepository.GetAsync(notification.Id);
            var superhero = new Superhero(notification.Id,
                                          notification.Name,
                                          notification.Alias,
                                          notification.ProtectionAreaId,
                                          existingSuperhero.DateCreated);

            _superheroRepository.Update(superhero);

            if (await CommitAsync())
            {
                var superheroEvent = new SuperheroUpdatedEvent(superhero.Id, superhero.Name, superhero.Alias);
                await _bus.RaiseEvent(superheroEvent);

                await _auditEventService.Subscribe(new AuditEvent(
                    Guid.NewGuid(),
                    nameof(Superhero),
                    superhero.Id,
                    notification.Username,
                    AuditEventAction.Update
                ));
            }
        }
    }
}
