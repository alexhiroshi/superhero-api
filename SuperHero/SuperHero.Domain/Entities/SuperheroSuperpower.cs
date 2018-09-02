namespace SuperHero.Domain.Entities
{
    public class SuperheroSuperpower : Base
    {
        public int SuperheroId { get; private set; }
        public int SuperpowerId { get; private set; }
        public virtual Superhero Superhero { get; private set; }
        public virtual Superpower Superpower { get; private set; }
    }
}
