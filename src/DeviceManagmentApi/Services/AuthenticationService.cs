using DeviceManagmentApi.DAL;
using DeviceManagmentApi.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using DeviceManagementApi.Models;

namespace DeviceManagmentApi.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly AppDbContext _context;

    public AuthenticationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AccountReadDto?> Authenticate(string login, string password)
    {
        var account = await _context.Accounts
            .Include(a => a.Role)
            .FirstOrDefaultAsync(a => a.Login == login);

        if (account == null || !VerifyPassword(password, account.PasswordHash, account.PasswordSalt))
            return null;

        return new AccountReadDto
        {
            Id = account.Id,
            Login = account.Login,
            RoleName = account.Role?.Name ?? string.Empty
        };
    }

    private bool VerifyPassword(string password, string storedHash, byte[] salt)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
        var computedHash = Convert.ToBase64String(pbkdf2.GetBytes(32));
        return computedHash == storedHash;
    }
}