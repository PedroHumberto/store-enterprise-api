using Microsoft.AspNetCore.Identity;
using StoreEnterpriseApp.Identity.API.Models;
using System.Security.Claims;

namespace StoreEnterpriseApp.Identity.API.Services
{
    public interface ITokenService
    {
        public Task<ClaimsIdentity> GetClaimsUserAsync(IList<Claim> claims, IdentityUser user, IList<string> userRoles);

        public string TokenCodifier(ClaimsIdentity identityClaims);

        public UserResponseLogin TokenResponse(string encodedToken, IdentityUser user, IEnumerable<Claim> claims);

    }
}
