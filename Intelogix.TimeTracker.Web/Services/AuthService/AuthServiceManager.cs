using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Intelogix.TimeTracker.Clients;
using Intelogix.TimeTracker.Requests;
using Intelogix.TimeTracker.Responses;

namespace Intelogix.TimeTracker.Web.Services.AuthService
{
    public class AuthServiceManager: IAuthServiceManager
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IAuthServiceClient _authServiceClient;
        private readonly NavigationManager _navigationManager;
        private const string Key = "x-auth-token";
        public AuthServiceManager(ILocalStorageService localStorageService, IAuthServiceClient authServiceClient,NavigationManager navigationManager)
        {
            _localStorageService = localStorageService;
            _authServiceClient = authServiceClient;
            _navigationManager = navigationManager;
            
        }
        public async Task<string?> GetTokenAsync()
        {
            var token = await _localStorageService.GetItemAsync<AuthResponse?>(Key);
            if (token == null)
                return null;
            if (DateTime.UtcNow > token.Expiry)
                _navigationManager.NavigateTo("/login");
            return $"Bearer {token.Token}";
        }
        public async Task<string?> GetNameAsync()
        {
            var name = await _localStorageService.GetItemAsync<AuthResponse?>(Key);
            if (name == null)
                return null;
            return name.UserName;
        }
        public async Task<bool> IsAuthenticatedAsync()
        {
            if (!await _localStorageService.ContainKeyAsync(Key))
                return false;
            var token = await _localStorageService.GetItemAsync<AuthResponse?>(Key);
            if (token == null)
                return false;
            if (DateTime.UtcNow > token.Expiry)
                return false;
            return true;
        }
        public async Task SignInAsync(string  username, string password,string path ="/")
        {
            try
            {
                //Todo:encrypt password
                var response = await _authServiceClient.AuthAsync(new AuthRequest
                {
                    UserName = username,
                    Password = password
                });
                await _localStorageService.SetItemAsync(Key, response);
                _navigationManager.NavigateTo(path);
            }
            catch { }
            
        }
        public async Task SignOutAsync()
        {
            await _localStorageService.RemoveItemAsync(Key);
            _navigationManager.NavigateTo("/login");
        }
    }
}
