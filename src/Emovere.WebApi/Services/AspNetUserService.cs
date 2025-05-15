using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Emovere.WebApi.Services
{
    public sealed class AspNetUserService(IHttpContextAccessor accessor) : IAspNetUserService
    {
        public Guid GetUserId()
        {
            if (!IsAuthenticated())
                return Guid.Empty;

            var claim = accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(claim))
                claim = accessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            return claim is null ? Guid.Empty : Guid.Parse(claim);
        }

        public string GetUserEmail()
        {
            var username = accessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
            return !string.IsNullOrEmpty(username) ? username : string.Empty;
        }

        public bool IsAuthenticated()
            => accessor.HttpContext?.User.Identity is { IsAuthenticated: true };

        public bool IsInRole(string role)
            => accessor.HttpContext is not null && accessor.HttpContext.User.IsInRole(role);
    }
}