using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Intelogix.TimeTracker.Common.Security;
using Intelogix.TimeTracker.Repository.UnitOfWork;
using Intelogix.TimeTracker.Requests;
using Intelogix.TimeTracker.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Intelogix.TimeTracker.Core.Services.AuthService
{
    public class AuthServiceManager:IAuthServiceManager
    {
        private readonly ITimeTrackerUnitOfWork _timeTrackerUnitOfWork;
        private readonly ILogger<AuthServiceManager> _logger;
        private readonly IConfiguration _configuration;
        public AuthServiceManager(ITimeTrackerUnitOfWork timeTrackerUnitOfWork, ILogger<AuthServiceManager> logger,IConfiguration configuration) {
        _logger = logger;
        _timeTrackerUnitOfWork = timeTrackerUnitOfWork;
         _configuration = configuration;
        }
        public async Task<AuthResponse> AuthAsync(AuthRequest request)
        {
            try
            {
                string seckey = _configuration["Security:Key"];



                var user = await _timeTrackerUnitOfWork.UserRepository.GetAsync(x => x.UserName.ToLower() == request.UserName.ToLower());
                
                if (user == null)
                    return new AuthResponse { Code = 401, Message = "Invalid Username!" };
                if(AesCrypto.Decrypt(user.Password, seckey) != request.Password)
                    return new AuthResponse { Code = 401, Message = "Invalid Password!" };

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var expiry = DateTime.UtcNow.AddMinutes(30);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                           new Claim("id", user.Id.ToString()), // 0 = Id
                           new Claim(ClaimTypes.Name,user.Name) //2 = Branch 
                    }),
                    Expires = expiry,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return new AuthResponse
                {
                    Token = jwtToken,
                    Expiry = expiry,
                    Id = user.Id,
                    UserName = user.UserName
                };
            }
            catch (Exception ex) { 
             _logger.LogError(ex.Message, ex);
                throw;            
            }
        }
    }
}
