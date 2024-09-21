using System.Security.Claims;

namespace DatingApp.Common.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            var username = user.FindFirstValue(ClaimTypes.Name)
                ?? throw new Exception("Cannot get username from token");

            return username;
        }



        public static string? FindFirstValue(this ClaimsPrincipal principal, string claimType)
        {
            var claim = principal.FindFirst(claimType);
            return claim?.Value;
        }
    }
}
