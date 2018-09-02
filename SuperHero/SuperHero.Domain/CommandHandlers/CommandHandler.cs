using System.Threading.Tasks;
using MediatR;
using SuperHero.Domain.Bus;
using SuperHero.Domain.Commands;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Notifications;
using SuperHero.Domain.Repositories;

namespace SuperHero.Domain.CommandHandlers
{
    public class CommandHandler<T> where T : Base
    {
        private readonly IRepository<T> _repo;
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(IRepository<T> repo,
                              IMediatorHandler bus,
                              INotificationHandler<DomainNotification> notifications)
        {
            _repo = repo;
            _bus = bus;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected void NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
        }

        public async Task<bool> CommitAsync()
        {
            if (_notifications.HasNotifications()) return false;
            var commandResponse = _repo.SaveChanges();
            if (new CommandResponse(commandResponse > 0).Success) return true;

            await _bus.RaiseEvent(new DomainNotification("Commit", "Failed to save the data."));
            return false;
        }
    }
}
