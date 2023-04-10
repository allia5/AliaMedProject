using DTO;
using Server.Models.UserAccount;
using Server.Models.UserRoles;
using System.Security.Claims;

namespace Server.Services.Foundation.JwtService
{
    public interface IJwtService
    {
        public string GenerateTokenRefresh();
        public string Generitedtoken(User user, List<Role> userRoles);
        public ClaimsPrincipal GetClaimPrincepale(string Token);
        public Task<JwtDto> UpdateRefreshToken(JwtDto jwtDto);
    }
}
