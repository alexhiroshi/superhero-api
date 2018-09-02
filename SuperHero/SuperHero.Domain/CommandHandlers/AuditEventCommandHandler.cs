using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperHero.Domain.Bus;
using SuperHero.Domain.Commands.AuditEvent;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Notifications;
using SuperHero.Domain.Repositories;

namespace SuperHero.Domain.CommandHandlers
{
    public class AuditEventCommandHandler: CommandHandler<AuditEvent>,
        INotificationHandler<RegisterAuditEventCommand>
    {
        private readonly IAuditEventRepository _auditEventRepository;
        private readonly IMediatorHandler _bus;

        public AuditEventCommandHandler(IAuditEventRepository auditEventRepository,
                                        IMediatorHandler bus,
                                        INotificationHandler<DomainNotification> notifications)
            : base(auditEventRepository, bus, notifications)
        {
            _auditEventRepository = auditEventRepository;
            _bus = bus;
        }

        public async Task Handle(RegisterAuditEventCommand notification, CancellationToken cancellationToken)
        {
            if (!notification.IsValid())
            {
                NotifyValidationErrors(notification);
                return;
            }

            var auditEvent = new AuditEvent(Guid.NewGuid(),
                                                notification.Entity,
                                                notification.EntityId,
                                                notification.Username,
                                                notification.Action);

            _auditEventRepository.Add(auditEvent);

            if (await CommitAsync()) { }
        }
    }
}
