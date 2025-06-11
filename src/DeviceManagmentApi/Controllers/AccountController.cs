using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DeviceManagementApi.Models;
using DeviceManagmentApi.DAL;
using DeviceManagmentApi.DTOs;
using Microsoft.AspNetCore.Identity.Data;

namespace DeviceManagmentApi
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<Account> _passwordHasher;
    
        public AccountController(AppDbContext context, PasswordHasher<Account> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            try
            {
                return await _context.Account.Include(a => a.Role).ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            try
            {
                var account = await _context.Account.Include(a => a.Role).FirstOrDefaultAsync(a => a.Id == id);
    
                if (account == null)
                {
                    return NotFound();
                }
    
                return account;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account updatedAccount)
        {
            try
            {
                if (id != updatedAccount.Id)
                {
                    return BadRequest();
                }
    
                var existingAccount = await _context.Account.FindAsync(id);
                if (existingAccount == null)
                {
                    return NotFound();
                }
    
                existingAccount.Login = updatedAccount.Login;
                existingAccount.RoleId = updatedAccount.RoleId;
    
                if (!string.IsNullOrEmpty(updatedAccount.Password))
                {
                    var hashedPassword = _passwordHasher.HashPassword(existingAccount, updatedAccount.Password);
                    existingAccount.Password = hashedPassword;
                }
    
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
    
                var account = await _context.Account
                    .SingleOrDefaultAsync(a => a.Username == loginDto.Username);
    
                if (account == null)
                    return Unauthorized("Invalid username or password.");
    
                var verificationResult =
                    _passwordHasher.VerifyHashedPassword(account, account.Password, loginDto.Password);
    
                if (verificationResult == PasswordVerificationResult.Failed)
                    return Unauthorized("Invalid username or password.");
    
                return Ok("Login successful.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            try
            {
                account.Password = _passwordHasher.HashPassword(account, account.Password);
                _context.Account.Add(account);
                await _context.SaveChangesAsync();
    
                return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            try
            {
                var account = await _context.Account.FindAsync(id);
                if (account == null)
                {
                    return NotFound();
                }
    
                _context.Account.Remove(account);
                await _context.SaveChangesAsync();
    
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    
        private bool AccountExists(int id)
        {
            return _context.Account.Any(e => e.Id == id);
        }
    }
}