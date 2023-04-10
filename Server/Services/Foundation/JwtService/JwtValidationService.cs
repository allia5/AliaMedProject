using DTO;
using Microsoft.AspNetCore.Identity;
using Server.Models.Doctor.Exceptions;
using Server.Models.Exceptions;
using Server.Models.UserAccount;
using Server.Models.UserRoles;
using System.Security.Claims;

namespace Server.Services.Foundation.JwtService
{
    public partial class JwtService
    {
        public void ValidateEntry(JwtDto jwtDto)
        {
            if (jwtDto != null)
            {
                if (jwtDto.Token == null)
                {
                    throw new NullException(nameof(jwtDto.Token));
                }
                else if (jwtDto.RefreshToken == null)
                {
                    throw new NullException(nameof(jwtDto.RefreshToken));
                }
            }
            else
            {
                throw new NullException(nameof(jwtDto));
            }
        }
        public void ValidateClaims(ClaimsPrincipal Claims)
        {
            if (Claims == null)
            {
                throw new IdentityTokenException(nameof(Claims));
            }
        }
        public void ValidateUserIsNull(User user)
        {
            if (user == null)
            {
                throw new NullException(nameof(user));
            }
        }
        public void ValidateRefreshToken(User user, string RefreshToken)
        {
            if (string.Compare(user.RefreshToken, RefreshToken) != 0 || user.DateExpireRefreshToken < DateTime.UtcNow)
            {
                throw new InvalidException(nameof(RefreshToken), user, nameof(user));
            }
        }
        public void ValidateListUserRolesIsNull(IQueryable UserRoles)
        {
            var list = UserRoles.Cast<UserRole>().ToList();
            if (list.Count() == 0)
            {
                throw new NullDataStorageException(nameof(UserRoles));
            }


        }
        public void ValidateIdentityToken(IdentityResult identityResult)
        {
            if (identityResult.Succeeded == false)
            {
                throw new IdentityTokenException(nameof(identityResult));
            }
        }
    }
}
