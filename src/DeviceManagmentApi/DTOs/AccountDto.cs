namespace DeviceManagmentApi.DTOs;

public class AccountDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string RoleName { get; set; } = null!;
}