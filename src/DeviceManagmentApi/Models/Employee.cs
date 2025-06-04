namespace DeviceManagmentApi.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public DateTime HireDate { get; set; }

        // Foreign keys
        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;

        public int PositionId { get; set; }
        public Position Position { get; set; } = null!;

        // Navigation
        public ICollection<DeviceEmployee>? DeviceEmployees { get; set; }
    }
}