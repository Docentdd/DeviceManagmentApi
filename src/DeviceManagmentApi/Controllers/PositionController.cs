using DeviceManagmentApi.DAL;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeviceManagmentApi.Controllers;
[ApiController]
[Route("api/positions")]
public class PositionController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<RolesController> _logger;
    
    public PositionController(AppDbContext context, ILogger<RolesController> logger)
    {
        _logger = logger;
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> GetPositions()
    {
        try
        {
            var positions = await _context.Position.Select(p => new { p.Id, p.Name }).ToListAsync();
            return Ok(positions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching positions");
            return StatusCode(500, ex.Message);
        }
        
    }
}