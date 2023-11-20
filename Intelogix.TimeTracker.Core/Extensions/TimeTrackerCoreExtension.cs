using Microsoft.Extensions.DependencyInjection;
using Intelogix.TimeTracker.Core.Services.EmployeeService;

namespace Intelogix.TimeTracker.Core.Extensions
{
    public static class TimeTrackerCoreExtension
    {
        public static IServiceCollection AddTimeTrackerCoreService(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeServiceManager>();
            return services;
        }
    }
}
