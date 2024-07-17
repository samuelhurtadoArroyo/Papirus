namespace Papirus.WebApi.Api.Controllers;

[Route("api/v1.0/[controller]")]
[ApiController]
public class HealthCheckController : ControllerBase
{
    // GET: api/<HealthCheckController>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        return Ok("Healthy");
    }
}