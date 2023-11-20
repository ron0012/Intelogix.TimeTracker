using Microsoft.Extensions.Logging;
using Intelogix.TimeTracker.Core.Mappings;
using Intelogix.TimeTracker.Core.Mappings.Profiles;
using Intelogix.TimeTracker.Data.Models;
using Intelogix.TimeTracker.Dtos;
using Intelogix.TimeTracker.Repository.UnitOfWork;
using Intelogix.TimeTracker.Requests;
using Intelogix.TimeTracker.Responses;

namespace Intelogix.TimeTracker.Core.Services.EmployeeService
{
    public class EmployeeServiceManager:IEmployeeServiceManager
    {
        private readonly ITimeTrackerUnitOfWork _timeTrackerUnitOfWork;
        private readonly ILogger<EmployeeServiceManager> _logger;
        public EmployeeServiceManager(ITimeTrackerUnitOfWork timeTrackerUnitOfWork, ILogger<EmployeeServiceManager> logger)
        {
            _timeTrackerUnitOfWork = timeTrackerUnitOfWork;
            _logger = logger;
        }
        public async Task<SearchEmployeeResponse> SearchAsync(SearchEmployeeRequest request)
        {
            try
            {
                var employees = await _timeTrackerUnitOfWork.EmployeeRepository.GetAllAsync(x => x.TimeSheets);

                //Filter
                if (request.IsActive != null)
                {
                    employees = employees.Where(x => x.IsActive == request.IsActive);
                }

                //Search
                if (!string.IsNullOrWhiteSpace(request.Query))
                {
                    employees = employees.Where(x => x.Id.ToString().Contains(request.Query) || x.Name.Contains(request.Query));
                }

                int resultCount = employees.Count();

                //Paging
                if (request.Skip != null && request.Take != null)
                {
                    employees = employees.Skip(request.Skip.Value).Take(request.Take.Value);
                }

                var response = TimeTrackerMapper.Map<IEnumerable<Employee>, SearchEmployeeResponse>(employees, new EmployeeProfile());
                response.Result = resultCount;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<GetEmployeeResponse> GetAsync(GetEmployeeRequest request)
        {
            try
            {
                var employee = await _timeTrackerUnitOfWork.EmployeeRepository.GetIncludeAsync(x => x.Id == request.EmployeeId, x => x.TimeSheets);
                if (employee == null)
                    return new GetEmployeeResponse
                    {
                        Code = 404,
                        Message = "No recods found!"
                    };

                return new GetEmployeeResponse
                {
                    Employee = TimeTrackerMapper.Map<Employee,ReadEmployeeDto>(employee,new EmployeeProfile()),
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
        public async Task<CreateEmployeeResponse> CreateAsync(CreateEmployeeRequest request)
        {
            try
            {
                var employee = TimeTrackerMapper.Map<CreateEmployeeDto,Employee>(request.Employee,new EmployeeProfile());
                await _timeTrackerUnitOfWork.EmployeeRepository.AddAsync(employee);
                await _timeTrackerUnitOfWork.SaveChangesAsync();
                return new CreateEmployeeResponse
                {
                    EmployeeId =employee.Id
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
        public async Task<UpdateEmployeeResponse> UpdateAsync(UpdateEmployeeRequest request)
        {
            try
            {
                var employee = TimeTrackerMapper.Map<UpdateEmployeeDto, Employee>(request.Employee, new EmployeeProfile());
                _timeTrackerUnitOfWork.EmployeeRepository.Update(employee);
                await _timeTrackerUnitOfWork.SaveChangesAsync();
                return new UpdateEmployeeResponse();               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
        public async Task<CreateTimeSheetResponse> CreateTimeSheetAsync(CreateTimeSheetRequest request)
        {
            try
            {
                var timeSheet = TimeTrackerMapper.Map<CreateTimeSheetDto,TimeSheet>(request.TimeSheet, new EmployeeProfile());
                await _timeTrackerUnitOfWork.TimeSheetRepository.AddAsync(timeSheet);
                await _timeTrackerUnitOfWork.SaveChangesAsync();
                return new CreateTimeSheetResponse();
               

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
