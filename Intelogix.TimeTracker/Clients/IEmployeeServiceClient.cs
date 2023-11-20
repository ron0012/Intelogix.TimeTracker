using Refit;
using Intelogix.TimeTracker.Requests;
using Intelogix.TimeTracker.Responses;

namespace Intelogix.TimeTracker.Clients
{
    public interface IEmployeeServiceClient
    {
        [Get("/api/employees")]
        Task<SearchEmployeeResponse> SearchAsync([Header("Authorization")]string token,
            [Query]string? query = null,
            [Query]bool? isActive = null,
            [Query]int? skip =null,
            [Query]int? take =null
            );
        [Get("/api/employees/{id}")]
        Task<GetEmployeeResponse> GetAsync([Header("Authorization")] string token,int id);
        [Post("/api/employees")]
        Task<CreateEmployeeResponse> CreateAsync([Header("Authorization")] string token,[Body]CreateEmployeeRequest request);
        [Put("/api/employees")]
        Task<UpdateEmployeeResponse> UpdateAsync([Header("Authorization")] string token,[Body]UpdateEmployeeRequest request);
        [Post("/api/employees/timesheet")]
        Task<CreateTimeSheetResponse> CreateTimeSheetAsync([Header("Authorization")] string token,[Body]CreateTimeSheetRequest reques);
    }
}
