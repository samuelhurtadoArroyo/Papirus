namespace Papirus.WebApi.Api.Controllers;

[Authorize]
[Route("api/v1.0/[controller]")]
[ApiController]
public class PermissionsController : ControllerBase
{
    private readonly IPermissionService _permissionService;

    private readonly IMapper _mapper;

    public PermissionsController(IPermissionService permissionService, IMapper mapper)
    {
        _permissionService = permissionService;
        _mapper = mapper;
    }

    // GET: api/<PermissionsController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PermissionDto>))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery] QueryRequest queryRequest)
    {
        List<Permission> itemsResult;

        if (queryRequest.PageNumber != null
           || queryRequest.PageSize != null
           || queryRequest.SearchString != null
           || queryRequest.FilterParams != null
           || queryRequest.SortingParams != null
           )
        {
            var queryResult = await _permissionService.GetByQueryRequestAsync(queryRequest);

            itemsResult = queryResult.Items;

            Response.Headers.Append("PaginationData", value: JsonConvert.SerializeObject(queryResult.PaginationData));

            return Ok(_mapper.Map<List<PermissionDto>>(itemsResult));
        }

        itemsResult = (await _permissionService.GetAll() as List<Permission>)!;
        return Ok(_mapper.Map<List<PermissionDto>>(itemsResult));
    }

    // GET api/<PermissionsController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PermissionDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        var itemResult = await _permissionService.GetById(id);
        return Ok(_mapper.Map<PermissionDto>(itemResult));
    }

    // POST api/<PermissionsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PermissionDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] PermissionDto permissionDto)
    {
        var itemResult = await _permissionService.Create(_mapper.Map<PermissionDto, Permission>(permissionDto)!);
        var resourceUrl = $"{Request.Path}/{itemResult.Id}";
        return Created(resourceUrl, _mapper.Map<PermissionDto>(itemResult));
    }

    // PUT api/<PermissionsController>/5
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PermissionDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put([FromBody] PermissionDto permissionDto)
    {
        var itemResult = await _permissionService.Edit(_mapper.Map<Permission>(permissionDto)!);
        return Ok(_mapper.Map<PermissionDto>(itemResult));
    }

    // DELETE api/<PermissionsController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        await _permissionService.Delete(id);
        return NoContent();
    }

    // GET: api/<PermissionsController>
    [HttpGet("GetByUser")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PermissionDto>))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByUser()
    {
        return Ok(_mapper.Map<List<PermissionDto>>(await _permissionService.GetAllPermissionsByUserAsync()));
    }
}