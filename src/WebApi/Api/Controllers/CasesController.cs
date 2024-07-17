namespace Papirus.WebApi.Api.Controllers;

[ExcludeFromCodeCoverage]
[Authorize]
[Route("api/v1.0/[controller]")]
[ApiController]
public class CasesController : ControllerBase
{
    private readonly ICaseService _caseService;

    private readonly ICaseDocumentFieldValueService _caseDocumentFieldValueService;

    private readonly IMapper _mapper;

    public CasesController(ICaseService caseService, ICaseDocumentFieldValueService caseDocumentFieldValueService, IMapper mapper)
    {
        _caseService = caseService;
        _caseDocumentFieldValueService = caseDocumentFieldValueService;
        _mapper = mapper;
    }

    // GET: api/<CasesController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CaseDto>))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery] QueryRequest queryRequest)
    {
        List<Case> itemsResult;

        if (queryRequest.PageNumber != null
           || queryRequest.PageSize != null
           || queryRequest.SearchString != null
           || queryRequest.FilterParams != null
           || queryRequest.SortingParams != null
           )
        {
            var queryResult = await _caseService.GetByQueryRequestAsync(queryRequest);

            itemsResult = queryResult.Items;

            Response.Headers.Append("PaginationData", value: JsonConvert.SerializeObject(queryResult.PaginationData));

            return Ok(_mapper.Map<List<CaseDto>>(itemsResult));
        }

        itemsResult = (await _caseService.GetAll() as List<Case>)!;
        return Ok(_mapper.Map<List<CaseDto>>(itemsResult));
    }

    // GET api/<CasesController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaseDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        var itemResult = await _caseService.GetById(id);

        return Ok(_mapper.Map<CaseDto>(itemResult));
    }

    // POST api/<CasesController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CaseDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] CaseDto caseDto)
    {
        var itemResult = await _caseService.Create(_mapper.Map<CaseDto, Case>(caseDto)!);
        var resourceUrl = $"{Request.Path}/{itemResult.Id}";
        return Created(resourceUrl, _mapper.Map<CaseDto>(itemResult));
    }

    // PUT api/<CasesController>/5
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaseDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put([FromBody] CaseDto caseDto)
    {
        var itemResult = await _caseService.Edit(_mapper.Map<Case>(caseDto)!);
        return Ok(_mapper.Map<CaseDto>(itemResult));
    }

    // DELETE api/<CasesController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        await _caseService.Delete(id);
        return NoContent();
    }

    #region get email preview

    // GET api/<CasesController>/5/EmailPreview
    [HttpGet("{id}/EmailPreview")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmailPreviewDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetEmailPreview(int id)
    {
        var itemCase = await _caseService.GetById(id);
        EmailPreviewDto emailPreviewDto = new()
        {
            Case = _mapper.Map<CaseDto>(itemCase),
            CaseDocuments = await _caseDocumentFieldValueService.GetByCaseIdAsync(id)
        };
        return Ok(emailPreviewDto);
    }

    #endregion get email preview

    // POST api/<CasesController>/updatebusinessline
    [HttpPost("updatebusinessline")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CaseDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateBusinessLine([FromBody] UpdateBusinessLineInputDto dto)
    {
        var itemResult = await _caseService.UpdateBusinessLineAsync(dto.Id, dto.BusinessLineId);
        return Ok(_mapper.Map<CaseDto>(itemResult));
    }

    // GET api/<CasesController>/caseByIdWithAssignment/5
    [HttpGet("caseByIdWithAssignment/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaseWithAssignmentDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCaseByIdWithAssignment(int id)
    {
        var itemResult = await _caseService.GetCaseByIdWithAssignmentAsync(id);
        CaseWithAssignmentDto dto = new()
        {
            Case = _mapper.Map<CaseDto>(itemResult),
            Assignment = _mapper.Map<CaseAssignmentDto>(itemResult.Assignment)
        };
        return Ok(dto);
    }

    // GET api/<CasesController>/caseAllWithAssignment
    [HttpGet("caseAllWithAssignment")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CaseWithAssignmentDto>))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllWithAssignment()
    {
        var itemResult = await _caseService.GetCaseAllWithAssignmentAsync();

        var listResult = itemResult.Select(item => new CaseWithAssignmentDto
        {
            Case = _mapper.Map<CaseDto>(item),
            Assignment = _mapper.Map<CaseAssignmentDto>(item.Assignment)
        }).ToList();

        return Ok(listResult);
    }

    //GET api/<CasesController>/guardianships
    [HttpGet("guardianships")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GuardianshipDto>))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Getguardianships([FromQuery] QueryRequest queryRequest)
    {
        List<GuardianshipDto> itemsResult;

        if (queryRequest.PageNumber != null
           || queryRequest.PageSize != null
           || queryRequest.SearchString != null
           || queryRequest.FilterParams != null
           || queryRequest.SortingParams != null
           )
        {
            var queryResult = await _caseService.GetGuardianshipsAsync(queryRequest);

            itemsResult = _mapper.Map<List<GuardianshipDto>>(queryResult.Items);

            Response.Headers.Append("PaginationData", value: JsonConvert.SerializeObject(queryResult.PaginationData));

            return Ok(itemsResult);
        }

        itemsResult = (await _caseService.GetGuardianshipsAsync())!;
        return Ok(itemsResult);
    }
}