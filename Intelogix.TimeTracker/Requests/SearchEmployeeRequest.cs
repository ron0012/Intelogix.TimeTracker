namespace Intelogix.TimeTracker.Requests
{
    public class SearchEmployeeRequest
    {
        public int? Take { get; set; }
        public int? Skip { get; set; }
        public string? Query { get; set; }
        public bool? IsActive { get; set; }
    }
}
