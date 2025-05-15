namespace Emovere.WebApi.Services
{
    public interface IAspNetUserService
    {
        string GetUserEmail();

        Guid GetUserId();

        bool IsAuthenticated();

        bool IsInRole(string role);
    }
}