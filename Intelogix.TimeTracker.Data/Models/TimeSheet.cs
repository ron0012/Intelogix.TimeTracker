namespace Intelogix.TimeTracker.Data.Models
{
    public class TimeSheet
    {
        public int Id { get; set; }
        public int UserIdFk { get; set; }
        public Employee Employee { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime ClockOut { get; set; }
    }
}
