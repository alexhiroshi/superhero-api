namespace SuperHero.Domain.Commands.ProtectionArea
{
    public abstract class ProtectionAreaCommand : Command
    {
        public string Name { get; protected set; }
        public double? Lat { get; protected set; }
        public double? Long { get; protected set; }
        public double? Radius { get; protected set; } 
    }
}
