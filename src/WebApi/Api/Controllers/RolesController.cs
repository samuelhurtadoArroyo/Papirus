namespace Papirus.WebApi.Api.Controllers;

[Authorize]
[Route("api/v1.0/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    private readonly IMapper _mapper;

    public RolesController(IRoleService roleService, IMapper mapper)
    {
        _roleService = roleService;
        _mapper = mapper;
    }

    // GET: api/<RolesController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RoleDto>))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery] QueryRequest queryRequest)
    {
        List<Role> itemsResult;

        if (queryRequest.PageNumber != null
           || queryRequest.PageSize != null
           || queryRequest.SearchString != null
           || queryRequest.FilterParams != null
           || queryRequest.SortingParams != null
           )
        {
            var queryResult = await _roleService.GetByQueryRequestAsync(queryRequest);

            itemsResult = queryResult.Items;

            Response.Headers.Append(PaginationConst.DefaultPaginationHeader, value: JsonConvert.SerializeObject(queryResult.PaginationData));

            return Ok(_mapper.Map<List<RoleDto>>(itemsResult));
        }

        itemsResult = (await _roleService.GetAll() as List<Role>)!;
        return Ok(_mapper.Map<List<RoleDto>>(itemsResult));
    }

    // GET api/<RolesController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        var itemResult = await _roleService.GetById(id);
        return Ok(_mapper.Map<RoleDto>(itemResult));
    }

    // POST api/<RolesController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RoleDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] RoleDto roleDto)
    {
        var itemResult = await _roleService.Create(_mapper.Map<RoleDto, Role>(roleDto)!);
        var resourceUrl = $"{Request.Path}/{itemResult.Id}";
        return Created(resourceUrl, _mapper.Map<RoleDto>(itemResult));
    }

    // PUT api/<RolesController>/5
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put([FromBody] RoleDto roleDto)
    {
        var itemResult = await _roleService.Edit(_mapper.Map<Role>(roleDto)!);
        return Ok(_mapper.Map<RoleDto>(itemResult));
    }

    // DELETE api/<RolesController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        await _roleService.Delete(id);
        return NoContent();
    }
}