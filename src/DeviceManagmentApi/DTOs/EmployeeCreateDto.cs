namespace DeviceManagmentApi.DTOs
{
    public class EmployeeCreateDto
    {
        public DateTime HireDate { get; set; }
        public int PersonId { get; set; }
        public int PositionId { get; set; }
    }
}