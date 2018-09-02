using System;

namespace SuperHero.Domain.Entities
{
    public class UserRole
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid RoleId { get; private set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }

        protected UserRole() {}

        public UserRole(Guid id, Guid userId, Guid roleId)
        {
            Id = id;
            UserId = userId;
            RoleId = roleId;
        }
    }
}
