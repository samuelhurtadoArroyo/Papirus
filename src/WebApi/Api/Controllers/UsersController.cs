namespace Papirus.WebApi.Api.Controllers;

[Authorize]
[Route("api/v1.0/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    private readonly IAuthenticationService _authenticationService;

    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IAuthenticationService authenticationService, IMapper mapper)
    {
        _userService = userService;
        _authenticationService = authenticationService;
        _mapper = mapper;
    }

    // GET: api/<UsersController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDto>))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery] QueryRequest queryRequest)
    {
        List<User> itemsResult;

        if (queryRequest.PageNumber != null
           || queryRequest.PageSize != null
           || queryRequest.SearchString != null
           || queryRequest.FilterParams != null
           || queryRequest.SortingParams != null
           )
        {
            var queryResult = await _userService.GetByQueryRequestAsync(queryRequest);

            itemsResult = queryResult.Items;

            Response.Headers.Append("PaginationData", value: JsonConvert.SerializeObject(queryResult.PaginationData));

            return Ok(_mapper.Map<List<UserDto>>(itemsResult));
        }

        itemsResult = (await _userService.GetAll() as List<User>)!;
        return Ok(_mapper.Map<List<UserDto>>(itemsResult));
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        var itemResult = await _userService.GetById(id);
        return Ok(_mapper.Map<UserDto>(itemResult));
    }

    // POST api/<UsersController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] UserInputDto userDto)
    {
        var firmIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "firmId")
            ?? throw new InvalidOperationException("The login in user has no FirmId assigned");

        var user = _mapper.Map<User>(userDto);
        var frimId = int.Parse(firmIdClaim.Value);

        var registeredUser = await _authenticationService.Register(user, userDto.Password, frimId);

        var registeredUserDto = _mapper.Map<UserDto>(registeredUser);

        return CreatedAtAction(nameof(Get), new { id = registeredUser.Id }, registeredUserDto);
    }

    // PUT api/<UsersController>/5
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    [ExcludeFromCodeCoverage(Justification = "Needs to be refactored")]
    public async Task<IActionResult> Put([FromBody] UserDto userDto)
    {
        var itemResult = await _userService.Edit(_mapper.Map<User>(userDto)!);
        return Ok(_mapper.Map<UserDto>(itemResult));
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.Delete(id);
        return NoContent();
    }
}