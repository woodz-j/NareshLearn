using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace NareshLearn.Api.Auth
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool TryGetUserId(this ClaimsPrincipal user, out Guid userId)
        {
            // Try common claim types in order
            var value =
                user.FindFirstValue(JwtRegisteredClaimNames.Sub) ??
                user.FindFirstValue(ClaimTypes.NameIdentifier) ??
                user.FindFirstValue("sub");

            return Guid.TryParse(value, out userId);
        }
    }
}
