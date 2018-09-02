using SuperHero.Domain.Models;

namespace SuperHero.Domain.Auth
{
    public interface IJwtTokenHandler
    {
        string Generate(UserModel user);
    }
}
