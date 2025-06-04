namespace DeviceManagmentApi.DTOs
{
    public class AccountUpdateDto
    {
        public string Username { get; set; } = null!;
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public string Login { get; set; }
        public object EmployeeId { get; set; }
    }
}