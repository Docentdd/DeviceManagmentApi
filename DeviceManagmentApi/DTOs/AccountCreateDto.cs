namespace DeviceManagmentApi.DTOs
{
    public class AccountCreateDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }
        public string Login { get; set; } = null!;
        public object EmployeeId { get; set; }
    }
}