namespace DeviceManagmentApi.DTOs
{
    public class DeviceCreateDto
    {
        public string SerialNumber { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int DeviceTypeId { get; set; }
    }
}