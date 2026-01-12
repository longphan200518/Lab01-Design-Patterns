using Microsoft.AspNetCore.Mvc;
using Lab01.Services;

namespace Lab01.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoggerController : ControllerBase
    {
        private readonly ILoggerService _logger;

        public LoggerController(ILoggerService logger)
        {
            _logger = logger;
            _logger.Log($"LoggerController initialized - Instance HashCode: {_logger.GetHashCode()}");
        }

        [HttpPost("log")]
        public IActionResult AddLog([FromBody] string message)
        {
            _logger.Log(message);
            return Ok(new { message = "Log added successfully", logCount = _logger.GetLogs().Count });
        }

        [HttpGet("logs")]
        public IActionResult GetAllLogs()
        {
            var logs = _logger.GetLogs();
            return Ok(new 
            { 
                totalLogs = logs.Count,
                instanceHashCode = _logger.GetHashCode(),
                logs = logs 
            });
        }
    }
}
