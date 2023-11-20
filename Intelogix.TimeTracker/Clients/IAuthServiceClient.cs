using Refit;
using Intelogix.TimeTracker.Requests;
using Intelogix.TimeTracker.Responses;

namespace Intelogix.TimeTracker.Clients
{
    public interface IAuthServiceClient
    {
        [Post("/api/auth")]
        Task<AuthResponse> AuthAsync([Body]AuthRequest request);
    }
}
