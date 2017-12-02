using AspNet.Security.OpenIdConnect.Primitives;
using System;
using System.Security.Claims;


namespace MusicLover.WebApp.Server.Extensions
{
    public static class IdentityUserExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var id = principal.FindFirst(OpenIdConnectConstants.Claims.Subject)?.Value;

            return id;
        }
    }
}
