using DeviceManagmentApi.DTOs;

namespace DeviceManagmentApi.Services;

public interface IAuthenticationService
{
    Task<AccountReadDto?> Authenticate(string login, string password);
}