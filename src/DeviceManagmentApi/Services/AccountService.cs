using DeviceManagementApi.Models;
using DeviceManagmentApi.DAL;
using DeviceManagmentApi.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

namespace DeviceManagmentApi.Services;

public class AccountService : IAccountService
{
    private readonly AppDbContext _context;

    public AccountService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AccountReadDto>> GetAll()
    {
        var accounts = await _context.Account.Include(a => a.Role).ToListAsync();

        return accounts.Select(a => new AccountReadDto
        {
            Id = a.Id,
            Login = a.Login,
            RoleName = a.Role.Name
        });
    }

    public async Task<AccountReadDto?> GetById(int id)
    {
        var account = await _context.Account.Include(a => a.Role)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (account == null) return null;

        return new AccountReadDto
        {
            Id = account.Id,
            Login = account.Login,
            RoleName = account.Role.Name
        };
    }

    public async Task<AccountReadDto> Create(AccountCreateDto dto)
    {
        var passwordHasher = new PasswordHasher<Account>();
        var account = new Account
        {
            Login = dto.Login,
            Password = passwordHasher.HashPassword(null, dto.Password),
            RoleId = dto.RoleId,
        };

        _context.Account.Add(account);
        await _context.SaveChangesAsync();

        var role = await _context.Role.FindAsync(account.RoleId);

        return new AccountReadDto
        {
            Id = account.Id,
            Login = account.Login,
            RoleName = role?.Name ?? ""
        };
    }

    public async Task<bool> Update(int id, AccountUpdateDto dto)
    {
        var account = await _context.Account.FindAsync(id);
        if (account == null) return false;

        account.Login = dto.Login;
        account.RoleId = dto.RoleId;
        

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var account = await _context.Account.FindAsync(id);
        if (account == null) return false;

        _context.Account.Remove(account);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<AccountReadDto?> Authenticate(string login, string password)
    {
        var account = await _context.Account.Include(a => a.Role)
            .FirstOrDefaultAsync(a => a.Login == login);

        return new AccountReadDto
        {
            Id = account.Id,
            Login = account.Login,
            RoleName = account.Role.Name
        };
    }
}