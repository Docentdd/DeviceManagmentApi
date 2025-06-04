namespace DeviceManagmentApi.DTOs
{
    public class EmployeeReadDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string PositionName { get; set; } = null!;
        public DateTime HireDate { get; set; }
    }
}