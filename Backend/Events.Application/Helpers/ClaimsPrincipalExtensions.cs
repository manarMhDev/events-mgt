

using Events.Domain.Entities;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace Events.Application.Helpers
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetLoggedInUserId(this ClaimsPrincipal principal)
        {
            try
            {
                if (principal == null)
                    throw new ArgumentNullException(nameof(principal));
                if (principal.HasClaim(claim => claim.Type == "UserId"))
                {
                    var loggedInUserId =  principal.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                    return loggedInUserId;
                }
                return "";
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
