using Intelogix.TimeTracker.Dtos;

namespace Intelogix.TimeTracker.Responses
{
    public class CreateEmployeeResponse:ErrorDto
    {
        public int EmployeeId { get; set; }
    }
}
