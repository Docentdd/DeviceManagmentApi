namespace DeviceManagmentApi.DTOs
{
    public class DeviceUpdateDto
    {
        public string SerialNumber { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int DeviceTypeId { get; set; }
    }
}