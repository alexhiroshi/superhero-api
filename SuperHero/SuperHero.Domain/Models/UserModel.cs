using System;
using System.Collections.Generic;

namespace SuperHero.Domain.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<RoleModel> Roles { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
