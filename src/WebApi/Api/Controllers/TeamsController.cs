namespace Papirus.WebApi.Api.Controllers;

[Authorize]
[Route("api/v1.0/[controller]")]
[ApiController]
public class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService;

    private readonly ITeamMemberService _teamMemberService;

    private readonly IMapper _mapper;

    public TeamsController(ITeamService teamService, ITeamMemberService teamMemberService, IMapper mapper)
    {
        _teamService = teamService;
        _teamMemberService = teamMemberService;
        _mapper = mapper;
    }

    // GET: api/<TeamsController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamDto>))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery] QueryRequest queryRequest)
    {
        List<Team> itemsResult;

        if (queryRequest.PageNumber != null
           || queryRequest.PageSize != null
           || queryRequest.SearchString != null
           || queryRequest.FilterParams != null
           || queryRequest.SortingParams != null
           )
        {
            var queryResult = await _teamService.GetByQueryRequestAsync(queryRequest);

            itemsResult = queryResult.Items;

            Response.Headers.Append(PaginationConst.DefaultPaginationHeader, value: JsonConvert.SerializeObject(queryResult.PaginationData));

            return Ok(_mapper.Map<List<TeamDto>>(itemsResult));
        }

        itemsResult = (await _teamService.GetAll() as List<Team>)!;
        return Ok(_mapper.Map<List<TeamDto>>(itemsResult));
    }

    // GET api/<TeamsController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        var itemResult = await _teamService.GetById(id);
        return Ok(_mapper.Map<TeamDto>(itemResult));
    }

    // GET api/<TeamsController>/5/members
    [HttpGet("{id}/members")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TeamMemberDto>))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMembers(int id)
    {
        var itemsResult = await _teamMemberService.GetByTeamId(id);
        return Ok(_mapper.Map<List<TeamMemberDto>>(itemsResult));
    }

    // POST api/<TeamsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TeamDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] TeamDto teamDto)
    {
        var itemResult = await _teamService.Create(_mapper.Map<TeamDto, Team>(teamDto)!);
        var resourceUrl = $"{Request.Path}/{itemResult.Id}";
        return Created(resourceUrl, _mapper.Map<TeamDto>(itemResult));
    }

    // PUT api/<TeamsController>/5
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TeamDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put([FromBody] TeamDto teamDto)
    {
        var itemResult = await _teamService.Edit(_mapper.Map<Team>(teamDto)!);
        return Ok(_mapper.Map<TeamDto>(itemResult));
    }

    // DELETE api/<TeamsController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        await _teamService.Delete(id);
        return NoContent();
    }
}