using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Intelogix.TimeTracker.Core.Services.EmployeeService;
using Intelogix.TimeTracker.Requests;

namespace Intelogix.TimeTracker.Api.Employees
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeServiceManager _employeeServiceManager;
        public EmployeesController(IEmployeeServiceManager employeeServiceManager)
        {
            _employeeServiceManager = employeeServiceManager;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]int id)
        {
            var result = await _employeeServiceManager.GetAsync(new GetEmployeeRequest
            {
                EmployeeId = id
            });
            if(result.Code != null)
                return StatusCode(result.Code.Value, result.Message);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> SearchAsync([FromQuery] bool? isActive=null, [FromQuery] string? query = null, [FromQuery]int? skip = null, [FromQuery]int?take=null)
        {
            var result = await _employeeServiceManager.SearchAsync(new SearchEmployeeRequest
            {
                IsActive = isActive,
                Query = query,
                Skip = skip,
                Take = take
            });
            if (result.Code != null)
                return StatusCode(result.Code.Value, result.Message);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEmployeeRequest request)
        {
            var result = await _employeeServiceManager.CreateAsync(request);
            if (result.Code != null)
                return StatusCode(result.Code.Value, result.Message);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]UpdateEmployeeRequest request)
        {
            var result = await _employeeServiceManager.UpdateAsync(request);
            if (result.Code != null)
                return StatusCode(result.Code.Value, result.Message);
            return Ok(result);
        }
        [HttpPost("timesheet")]
        public async Task<IActionResult> CreateTimeSheetAsync([FromBody] CreateTimeSheetRequest request)
        {
            var result = await _employeeServiceManager.CreateTimeSheetAsync(request);   
            if (result.Code != null)
                return StatusCode(result.Code.Value, result.Message);
            return Ok(result);
        }
    }
}
        