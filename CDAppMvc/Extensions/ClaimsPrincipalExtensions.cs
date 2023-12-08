using System.Security.Claims;

namespace CDAppMvc.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var userId = claimsPrincipal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier);
        return userId.Value;
    }
}