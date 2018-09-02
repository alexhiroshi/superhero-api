using System;
using System.Collections.Generic;
using SuperHero.Domain.Entities;

namespace SuperHero.Domain.Commands.User
{
    public abstract class UserCommand : Command
    {
        public string Username { get; protected set; }
        public string Password { get; protected set; }
        public ICollection<UserRole> UserRoles { get; protected set; }
    }
}
