using DeviceManagmentApi.DTOs;
namespace DeviceManagmentApi.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountReadDto>> GetAll();
        Task<AccountReadDto?> GetById(int id);
        Task<AccountReadDto> Create(AccountCreateDto dto);
        Task<bool> Update(int id, AccountUpdateDto dto);
        Task<bool> Delete(int id);
        Task<AccountReadDto?> Authenticate(string login, string password);
    }
}