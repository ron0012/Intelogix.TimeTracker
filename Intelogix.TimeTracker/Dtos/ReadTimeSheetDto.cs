namespace Intelogix.TimeTracker.Dtos
{
    public class ReadTimeSheetDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime ClockOut { get; set; }
    }
}
