namespace Papirus.WebApi.Api.Controllers;

[Authorize]
[Route("api/v1.0/[controller]")]
[ApiController]
public class ActorsController : ControllerBase
{
    private readonly IActorService _actorService;

    private readonly IMapper _mapper;

    public ActorsController(IActorService actorService, IMapper mapper)
    {
        _actorService = actorService;
        _mapper = mapper;
    }

    // GET: api/<ActorsController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ActorDto>))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery] QueryRequest queryRequest)
    {
        List<Actor> itemsResult;

        if (queryRequest.PageNumber != null
           || queryRequest.PageSize != null
           || queryRequest.SearchString != null
           || queryRequest.FilterParams != null
           || queryRequest.SortingParams != null
           )
        {
            var queryResult = await _actorService.GetByQueryRequestAsync(queryRequest);

            itemsResult = queryResult.Items;

            Response.Headers.Append("PaginationData", value: JsonConvert.SerializeObject(queryResult.PaginationData));

            return Ok(_mapper.Map<List<ActorDto>>(itemsResult));
        }

        itemsResult = (await _actorService.GetAll() as List<Actor>)!;
        return Ok(_mapper.Map<List<ActorDto>>(itemsResult));
    }

    // GET api/<ActorsController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActorDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        var itemResult = await _actorService.GetById(id);
        return Ok(_mapper.Map<ActorDto>(itemResult));
    }

    // POST api/<ActorsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ActorDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] ActorDto actorDto)
    {
        var itemResult = await _actorService.Create(_mapper.Map<ActorDto, Actor>(actorDto)!);
        var resourceUrl = $"{Request.Path}/{itemResult.Id}";
        return Created(resourceUrl, _mapper.Map<ActorDto>(itemResult));
    }

    // PUT api/<ActorsController>/5
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActorDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put([FromBody] ActorDto actorDto)
    {
        var itemResult = await _actorService.Edit(_mapper.Map<Actor>(actorDto)!);
        return Ok(_mapper.Map<ActorDto>(itemResult));
    }

    // DELETE api/<ActorsController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        await _actorService.Delete(id);
        return NoContent();
    }
}