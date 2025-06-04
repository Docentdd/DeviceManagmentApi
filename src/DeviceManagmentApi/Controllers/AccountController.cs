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
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<Account> _passwordHasher;

        public AccountController(AppDbContext context, PasswordHasher<Account> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        // GET: api/Account
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            return await _context.Accounts.Include(a => a.Role).ToListAsync();
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccount(int id)
        {
            var account = await _context.Accounts.Include(a => a.Role).FirstOrDefaultAsync(a => a.Id == id);

            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // PUT: api/Account/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, Account updatedAccount)
        {
            if (id != updatedAccount.Id)
            {
                return BadRequest();
            }

            var existingAccount = await _context.Accounts.FindAsync(id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            existingAccount.Login = updatedAccount.Login;
            existingAccount.RoleId = updatedAccount.RoleId;

            // Hash password if provided (not null or empty)
            if (!string.IsNullOrEmpty(updatedAccount.PasswordHash))
            {
                var hashedPassword = _passwordHasher.HashPassword(existingAccount, updatedAccount.PasswordHash);
                existingAccount.PasswordHash = hashedPassword;
            }

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var account = await _context.Accounts
                .SingleOrDefaultAsync(a => a.Username == loginDto.Username);

            if (account == null)
                return Unauthorized("Invalid username or password.");

            var verificationResult = _passwordHasher.VerifyHashedPassword(account, account.PasswordHash, loginDto.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid username or password.");
            // modify

            return Ok("Login successful.");
        }
        // POST: api/Account
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(Account account)
        {
            // Hash the password before saving
            account.PasswordHash = _passwordHasher.HashPassword(account, account.PasswordHash);
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
        }

        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}