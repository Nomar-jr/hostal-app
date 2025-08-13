using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Hostal.Application.UsesCases.User;

public class UserContext(IHttpContextAccessor httpContextAccessor): IUserContext
{
    public CurrentUser GetCurrentUser()
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user == null)
        {
            throw new InvalidOperationException("No user found");
        }

        if (user.Identity is not {IsAuthenticated:true})
        {
            return null;
        }
        var userId = user.FindFirst(c=> c.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
        return new CurrentUser(userId, email, roles);
    }
}