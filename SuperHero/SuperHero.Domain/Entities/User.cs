using System;
using System.Collections.Generic;

namespace SuperHero.Domain.Entities
{
    public class User : Base
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public virtual ICollection<UserRole> UserRoles { get; private set; }

        protected User() { }

        public User(Guid id, string username, string password, List<UserRole> userRoles, DateTime dateCreated)
        {
            Id = id;
            Username = username;
            Password = password;
            UserRoles = userRoles;
            DateCreated = dateCreated;
        }
    }
}
