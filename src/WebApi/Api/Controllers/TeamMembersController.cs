namespace Papirus.WebApi.Api.Controllers;

[Authorize]
[Route("api/v1.0/[controller]")]
[ApiController]
public class TeamMembersController : ControllerBase
{
    private readonly ITeamMemberService _teamMemberService;

    private readonly IMapper _mapper;

    public TeamMembersController(ITeamMemberService teamMemberService, IMapper mapper)
    {
        _teamMemberService = teamMemberService;
        _mapper = mapper;
    }

    // GET: api/<TeamMembersController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamMemberDto>))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery] QueryRequest queryRequest)
    {
        List<TeamMember> itemsResult;

        if (queryRequest.PageNumber != null
           || queryRequest.PageSize != null
           || queryRequest.SearchString != null
           || queryRequest.FilterParams != null
           || queryRequest.SortingParams != null
           )
        {
            var queryResult = await _teamMemberService.GetByQueryRequestAsync(queryRequest);

            itemsResult = queryResult.Items;

            Response.Headers.Append(PaginationConst.DefaultPaginationHeader, value: JsonConvert.SerializeObject(queryResult.PaginationData));

            return Ok(_mapper.Map<List<TeamMemberDto>>(itemsResult));
        }

        itemsResult = (await _teamMemberService.GetAll() as List<TeamMember>)!;
        return Ok(_mapper.Map<List<TeamMemberDto>>(itemsResult));
    }

    // GET api/<TeamMembersController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamMemberDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        var itemResult = await _teamMemberService.GetById(id);
        return Ok(_mapper.Map<TeamMemberDto>(itemResult));
    }

    // POST api/<TeamMembersController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TeamMemberDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] TeamMemberDto teammemberDto)
    {
        var itemResult = await _teamMemberService.Create(_mapper.Map<TeamMemberDto, TeamMember>(teammemberDto)!);
        var resourceUrl = $"{Request.Path}/{itemResult.Id}";
        return Created(resourceUrl, _mapper.Map<TeamMemberDto>(itemResult));
    }

    // PUT api/<TeamMembersController>/5
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamMemberDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put([FromBody] TeamMemberDto teammemberDto)
    {
        var itemResult = await _teamMemberService.Edit(_mapper.Map<TeamMember>(teammemberDto)!);
        return Ok(_mapper.Map<TeamMemberDto>(itemResult));
    }

    // DELETE api/<TeamMembersController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        await _teamMemberService.Delete(id);
        return NoContent();
    }
}