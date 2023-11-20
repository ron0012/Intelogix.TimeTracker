using Intelogix.TimeTracker.Requests;
using Intelogix.TimeTracker.Responses;

namespace Intelogix.TimeTracker.Core.Services.EmployeeService
{
    public interface IEmployeeServiceManager
    {
        Task<SearchEmployeeResponse> SearchAsync(SearchEmployeeRequest request);
        Task<GetEmployeeResponse> GetAsync(GetEmployeeRequest request);
        Task<CreateEmployeeResponse> CreateAsync(CreateEmployeeRequest request);
        Task<UpdateEmployeeResponse> UpdateAsync(UpdateEmployeeRequest request);
        Task<CreateTimeSheetResponse> CreateTimeSheetAsync(CreateTimeSheetRequest request);
    }
}
