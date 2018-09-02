namespace SuperHero.Domain.Auth
{
    public interface IHashString
    {
        string Generate(string text);
    }
}
