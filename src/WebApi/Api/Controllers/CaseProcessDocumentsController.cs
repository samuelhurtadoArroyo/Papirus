namespace Papirus.WebApi.Api.Controllers;

[Authorize]
[Route("api/v1.0/[controller]")]
[ApiController]
public class CaseProcessDocumentsController : ControllerBase
{
    private readonly ICaseProcessDocumentService _caseProcessDocumentService;

    public CaseProcessDocumentsController(ICaseProcessDocumentService caseProcessDocumentService)
    {
        _caseProcessDocumentService = caseProcessDocumentService;
    }

    //// GET api/<CaseProcessDocumentsController>?caseId=3
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CaseProcessDocumentDto>))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByCaseId([FromQuery] int caseId)
    {
        var itemsResult = await _caseProcessDocumentService.GetByCaseId(caseId);
        return Ok(itemsResult);
    }

    //// GET api/<CaseProcessDocumentsController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaseProcessDocumentDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var itemResult = await _caseProcessDocumentService.GetById(id);
        return Ok(itemResult);
    }
}