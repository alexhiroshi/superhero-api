using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperHero.Domain.Auth;
using SuperHero.Domain.Bus;
using SuperHero.Domain.Commands.User;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Notifications;
using SuperHero.Domain.Repositories;

namespace SuperHero.Domain.CommandHandlers
{
    public class UserCommandHandler: CommandHandler<User>,
        INotificationHandler<RegisterUserCommand>,
        INotificationHandler<RemoveUserCommand>,
        INotificationHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediatorHandler _bus;
        private readonly IHashString _hashString;

        public UserCommandHandler(IUserRepository userRepository,
                                  IMediatorHandler bus,
                                  IHashString hashString,
                                  INotificationHandler<DomainNotification> notifications)
            : base(userRepository, bus, notifications)
        {
            _userRepository = userRepository;
            _hashString = hashString;
            _bus = bus;
        }

        public async Task Handle(RegisterUserCommand notification, CancellationToken cancellationToken)
        {
            if (!notification.IsValid())
            {
                NotifyValidationErrors(notification);
                return;
            }

            if (await _userRepository.GetByUsernameAsync(notification.Username) != null)
            {
                await _bus.RaiseEvent(new DomainNotification(notification.MessageType, "There's already a user with this username"));
                return;
            }

            var userId = Guid.NewGuid();
            var userRoles = new List<UserRole>();
            foreach (var role in notification.UserRoles)
                userRoles.Add(new UserRole(Guid.NewGuid(), userId, role.RoleId));

            var user = new User(userId,
                                notification.Username,
                                _hashString.Generate(notification.Password),
                                userRoles,
                                DateTime.Now);

            _userRepository.Add(user);

            if (await CommitAsync()) { }
        }

        public async Task Handle(RemoveUserCommand notification, CancellationToken cancellationToken)
        {
            if (!notification.IsValid())
            {
                NotifyValidationErrors(notification);
                return;
            }

            _userRepository.Remove(notification.Id);

            if (await CommitAsync()) { }
        }

        public async Task Handle(UpdateUserCommand notification, CancellationToken cancellationToken)
        {
            if (!notification.IsValid())
            {
                NotifyValidationErrors(notification);
                return;
            }

            var existingUser = await _userRepository.GetByUsernameAsync(notification.Username);
            if (existingUser != null && existingUser.Id != notification.Id)
            {
                await _bus.RaiseEvent(new DomainNotification(notification.MessageType, "There's already a user with this username"));
                return;
            }

            existingUser = await _userRepository.GetAsync(notification.Id);
            var userRoles = new List<UserRole>();
            foreach (var role in notification.UserRoles)
                userRoles.Add(new UserRole(Guid.NewGuid(), notification.Id, role.RoleId));

            var user = new User(notification.Id,
                                existingUser.Username,
                                _hashString.Generate(notification.Password),
                                userRoles,
                                existingUser.DateCreated);

            _userRepository.Update(user);

            if (await CommitAsync()) { }
        }
    }
}
