using System;
using SuperHero.Domain.Enums;

namespace SuperHero.Application.ViewModels
{
    public class AuditEventViewModel
    {
        public string Entity { get; set; }
        public Guid EntityId { get; set; }
        public string Username { get; set; }
        public AuditEventAction Action { get; set; }
    }
}
