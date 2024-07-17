namespace Papirus.WebApi.Api.Controllers;

[Authorize]
[Route("api/v1.0/[controller]")]
[ApiController]
public class CaseDocumentFieldValueController : ControllerBase
{
    private readonly ICaseDocumentFieldValueService _caseService;

    public CaseDocumentFieldValueController(ICaseDocumentFieldValueService caseService)
    {
        _caseService = caseService;
    }

    /// <summary>
    /// Retrieves case document field values filtered by CaseId. Optionally filters by document type when provided.
    /// </summary>
    /// <param name="caseId">The identifier of the case for which document field values are being retrieved.</param>
    /// <param name="documentTypeId">Optional identifier of the document type to further filter the case document field values.</param>
    /// <returns>A list of case document field values. If no matching data is found, returns a 404 Not Found.</returns>
    /// <remarks>
    /// This endpoint returns all case document field values associated with the specified `caseId`.
    /// If a `documentTypeId` is also provided, the results are further filtered to only include those entries
    /// that match both the `caseId` and the `documentTypeId`. If either `caseId` or `documentTypeId` does not
    /// result in any data, a Not Found response is returned. This method is intended to support flexible data retrieval
    /// where users can specify a broader or more narrow scope based on their needs.
    /// </remarks>
    [HttpGet("{caseId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CaseDocumentFieldValueDto>))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ActionResult), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<CaseDocumentFieldValueDto>>> GetByCaseId(int caseId, [FromQuery] int? documentTypeId)
    {
        if (documentTypeId is not null)
        {
            var resultCaseAndDocumentType = await _caseService.GetByCaseIdAndDocumentTypeIdAsync(caseId, documentTypeId);
            if (resultCaseAndDocumentType?.Any() != true)
            {
                return NotFound();
            }
            return Ok(resultCaseAndDocumentType);
        }
        var results = await _caseService.GetByCaseIdAsync(caseId);
        return Ok(results);
    }

    // Put api/<CasesController>/
    [HttpPut()]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put([FromBody] List<UpdateCaseDocumentFieldValueDto> updateDtos)
    {
        var result = await _caseService.UpdateCaseDocumentFieldValues(updateDtos);
        if (result)
            return NoContent();
        return NotFound();
    }
}