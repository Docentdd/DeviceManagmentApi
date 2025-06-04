namespace DeviceManagmentApi.DTOs
{
    public class DeviceReadDto
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string DeviceTypeName { get; set; } = null!;
    }
}