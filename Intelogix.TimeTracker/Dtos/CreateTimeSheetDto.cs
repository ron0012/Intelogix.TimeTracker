namespace Intelogix.TimeTracker.Dtos
{
    public class CreateTimeSheetDto
    {
        public int EmployeeId { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime ClockOut { get; set; }
    }
}
