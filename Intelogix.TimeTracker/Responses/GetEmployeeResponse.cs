using Intelogix.TimeTracker.Dtos;

namespace Intelogix.TimeTracker.Responses
{
    public class GetEmployeeResponse:ErrorDto
    {
        public ReadEmployeeDto Employee { get; set; }
    }
}
