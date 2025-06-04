namespace DeviceManagmentApi.Models
{
    public class DeviceEmployee
    {
        public int DeviceId { get; set; }
        public Device Device { get; set; } = null!;

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}