using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    
    namespace DeviceManagmentApi.Controllers;
    
    [ApiController]
    [Route("api/deviceTypeController")]
    public class DeviceTypesController : ControllerBase
    {
        private readonly ILogger<DeviceTypesController> _logger;
    
        public DeviceTypesController(ILogger<DeviceTypesController> logger)
        {
            _logger = logger;
        }
    
        [HttpGet("types")]
        public IActionResult GetDeviceTypes()
        {
            try
            {
                var deviceTypes = new List<string> { "Type1", "Type2" };
                return Ok(deviceTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching device types");
                return StatusCode(500, "Internal server error");
            }
        }
    }