using PokeApi.Models;
using System.Security.Claims;

namespace PokeApi.Interfaces
{
    public interface IJWTManagerRepository
    {
        Tokens GenerateJWT(string username);
        Tokens GenerateRefreshToken(string username);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
