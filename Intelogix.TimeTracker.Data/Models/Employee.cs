namespace Intelogix.TimeTracker.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }     
        public bool IsActive { get; set; }
        public ICollection<TimeSheet> TimeSheets { get; set;}
    }
}
