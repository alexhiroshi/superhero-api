using System;
using System.Collections.Generic;

namespace SuperHero.Application.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<UserRoleViewModel> UserRoles { get; set; }
    }
}
