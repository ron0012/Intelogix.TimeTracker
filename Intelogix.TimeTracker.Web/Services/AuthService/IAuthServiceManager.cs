namespace Intelogix.TimeTracker.Web.Services.AuthService
{
    public interface IAuthServiceManager
    {
        Task<string?> GetTokenAsync();
        Task<bool> IsAuthenticatedAsync();
        Task SignInAsync(string username, string password, string path = "/");
        Task SignOutAsync();
        Task<string?> GetNameAsync();
    }
}
