namespace SuperHero.Domain.Commands.Superpower
{
    public abstract class SuperpowerCommand : Command
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
    }
}
