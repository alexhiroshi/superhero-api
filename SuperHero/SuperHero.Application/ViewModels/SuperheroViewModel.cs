using System;
using System.Collections.Generic;

namespace SuperHero.Application.ViewModels
{
    public class SuperheroViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public Guid ProtectionAreaId { get; set; }
        public string Username { get; set; }
        public List<SuperpowerViewModel> Superpower { get; set; }
    }
}
