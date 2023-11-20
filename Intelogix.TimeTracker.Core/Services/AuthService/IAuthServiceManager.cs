using Intelogix.TimeTracker.Requests;
using Intelogix.TimeTracker.Responses;

namespace Intelogix.TimeTracker.Core.Services.AuthService
{
    public interface  IAuthServiceManager
    {
        Task<AuthResponse> AuthAsync(AuthRequest request);
    }
}
