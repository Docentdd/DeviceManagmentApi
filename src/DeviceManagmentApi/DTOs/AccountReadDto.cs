namespace DeviceManagmentApi.DTOs
{
    public class AccountReadDto
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}