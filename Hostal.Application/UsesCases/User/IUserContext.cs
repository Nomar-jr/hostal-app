namespace Hostal.Application.UsesCases.User;

public interface IUserContext
{
    public CurrentUser GetCurrentUser();
}