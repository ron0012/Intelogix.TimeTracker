using Microsoft.AspNetCore.Mvc;
using Intelogix.TimeTracker.Core.Services.AuthService;
using Intelogix.TimeTracker.Requests;

namespace Intelogix.TimeTracker.Api.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServiceManager _authServiceManager;
        public AuthController(IAuthServiceManager authServiceManager)
        {
            _authServiceManager = authServiceManager;
        }
        [HttpPost]
        public async Task<IActionResult> AuthAsync([FromBody] AuthRequest request)
        {
            var result = await _authServiceManager.AuthAsync(request);
            if (result.Code != null)
                return StatusCode(result.Code.Value, result.Message);
            return Ok(result);
        }

    }
}
