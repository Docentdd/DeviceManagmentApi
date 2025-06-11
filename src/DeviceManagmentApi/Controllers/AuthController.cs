using DeviceManagementApi.Models;
using DeviceManagmentApi.DAL;
using DeviceManagmentApi.DTOs;
using DeviceManagmentApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManagmentApi.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IAuthenticationService _authenticationService;
    private readonly PasswordHasher<Account> _passwordHasher = new PasswordHasher<Account>();
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Login) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest("Invalid login request.");
            }
    
            var account = await _authenticationService.Authenticate(loginDto.Login, loginDto.Password);
    
            if (account == null)
            {
                return Unauthorized("Invalid login or password.");
            }
    
            return Ok(account);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during login.");
            return StatusCode(500, "An internal server error occurred.");
        }
    }
}