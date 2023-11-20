using Intelogix.TimeTracker.Dtos;

namespace Intelogix.TimeTracker.Responses
{
    public class AuthResponse:ErrorDto
    {
        public string Token { get; set; }
        public DateTime Expiry { get; set; }
        public string UserName { get;set; }
        public int Id { get; set; }

    }
}
