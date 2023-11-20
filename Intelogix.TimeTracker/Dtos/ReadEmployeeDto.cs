namespace Intelogix.TimeTracker.Dtos
{
    public class ReadEmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<ReadTimeSheetDto> TimeSheets { get; set; }
    }
}
