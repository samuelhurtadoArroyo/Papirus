namespace Papirus.WebApi.Api.Controllers;

[Authorize]
[Route("api/v1.0/[controller]")]
[ApiController]
public class BusinessLineController : ControllerBase
{
    private readonly IBusinessLineService _businessLineService;

    public BusinessLineController(IBusinessLineService businessLineService)
    {
        _businessLineService = businessLineService;
    }

    /// <summary>
    /// Retrieves list of business lines.
    /// </summary>
    /// <returns>A list of business line entities.</returns>
    /// <remarks>
    /// This endpoint returns all business lines.
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BusinessLineDto>))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<BusinessLineDto>>> GetAll()
    {
        var results = await _businessLineService.GetAllAsync();
        return Ok(results);
    }
}