using DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Server.Managers.Storages.RolesManager;
using Server.Managers.Storages.UserRoleManager;
using Server.Models.UserAccount;
using Server.Models.UserRoles;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static Server.Services.UserService.UserMapperService;

namespace Server.Services.Foundation.JwtService
{
    public partial class JwtService : IJwtService
    {
        public readonly IConfiguration configuration;

        public readonly UserManager<User> _userManager;
        public readonly IRolesManager RoleManager;
        public readonly IUserRoleManager userRoleManager;


        public JwtService(IConfiguration configuration, UserManager<User> _userManager, IRolesManager RoleManager, IUserRoleManager userRoleManager)
        {
            this.configuration = configuration;
            this._userManager = _userManager;
            this.RoleManager = RoleManager;
            this.userRoleManager = userRoleManager;

        }


        public string GenerateTokenRefresh()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public string Generitedtoken(User user, List<Role> userRoles)
        {
            var keysecurity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credential = new SigningCredentials(keysecurity, SecurityAlgorithms.HmacSha256);
            string RoleName = null;

            var claim = new[]
            {

                new Claim(ClaimTypes.Name, user.Email), // NOTE: this will be the "UserAccount.Identity.Name" value
				new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.Email),




            };
            var list = claim.ToList();


            // int k = 3;

            foreach (var item in userRoles)
            {
                list.Add(new Claim(ClaimTypes.Role, item.Name));
                //  claim.Append(new Claim(ClaimTypes.Role, item.Name));
                //claim[k] = new Claim(ClaimTypes.Role, item.Name);
                // k++;
            }
            var claim2 = list.ToArray();




            var token = new JwtSecurityToken(configuration["Jwt:Issuer"], configuration["Jwt:Audience"], claim2, expires: DateTime.Now.AddMinutes(720), signingCredentials: credential);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal GetClaimPrincepale(string Token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"])),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(Token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        public async Task<JwtDto> UpdateRefreshToken(JwtDto jwtDto)
        =>
          await TryCatch(async () =>
            {
                ValidateEntry(jwtDto);
                var Claims = GetClaimPrincepale(jwtDto.Token);
                ValidateClaims(Claims);
                var User = await this._userManager.FindByEmailAsync(Claims.Identity?.Name);
                ValidateUserIsNull(User);
                ValidateRefreshToken(User, jwtDto.RefreshToken);
                var UserRoles = await this.userRoleManager.GetUserRolesById(User.Id);
                ValidateListUserRolesIsNull(UserRoles);
                var list = UserRoles.Cast<UserRole>().ToList();
                List<Role> listItem = new List<Role>();
                foreach (var Item in list)
                {
                    var role = await this.RoleManager.GetRolesById(Item.RoleId);
                    listItem.Add(role);
                }
                var Token = Generitedtoken(User, listItem);
                var refreshToken = GenerateTokenRefresh();
                DateTime DateExpire = DateTime.Now.AddDays(7);
                var NewUser = AppendRfToken(User, refreshToken, DateExpire);
                var IdentityResult = await this._userManager.UpdateAsync(NewUser);
                ValidateIdentityToken(IdentityResult);
                return new JwtDto
                {
                    Token = Token,
                    RefreshToken = refreshToken
                };
            });









    }
}
