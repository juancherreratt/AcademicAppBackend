using Infrastructure.Identity.Models;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Identity.Interfaces
{
    public interface ITokenService
    {
        Task<JwtSecurityToken> CreateJwtAsync(AppUser user);
    }
}
