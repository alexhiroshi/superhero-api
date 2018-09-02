using System;

namespace SuperHero.Application.ViewModels
{
    public class ProtectionAreaViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double? Long { get; set; }
        public double? Lat { get; set; }
        public double? Radius { get; set; }
    }
}
