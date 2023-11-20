using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Refit;
using Intelogix.TimeTracker.Clients;
using Intelogix.TimeTracker.Web;
using Intelogix.TimeTracker.Web.Services.AuthService;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage(config =>
{
    config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    config.JsonSerializerOptions.WriteIndented = false;
});
builder.Services.AddRefitClient<IAuthServiceClient>()
           .ConfigureHttpClient(x =>
           {
               x.BaseAddress = new Uri("https://localhost:7018/");
           });
builder.Services.AddRefitClient<IEmployeeServiceClient>()
           .ConfigureHttpClient(x =>
           {
               x.BaseAddress = new Uri("https://localhost:7018/");
           });
builder.Services.AddScoped<IAuthServiceManager, AuthServiceManager>();
await builder.Build().RunAsync();
