namespace Papirus.WebApi.Api.Controllers;

[Authorize]
[Route("api/v1.0/[controller]")]
[ApiController]
public class FirmsController : ControllerBase
{
    private readonly IFirmService _firmService;

    private readonly IMapper _mapper;

    public FirmsController(IFirmService firmService, IMapper mapper)
    {
        _firmService = firmService;
        _mapper = mapper;
    }

    // GET: api/<FirmsController>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FirmDto>))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromQuery] QueryRequest queryRequest)
    {
        List<Firm> itemsResult;

        if (queryRequest.PageNumber != null
           || queryRequest.PageSize != null
           || queryRequest.SearchString != null
           || queryRequest.FilterParams != null
           || queryRequest.SortingParams != null
           )
        {
            var queryResult = await _firmService.GetByQueryRequestAsync(queryRequest);

            itemsResult = queryResult.Items;

            Response.Headers.Append("PaginationData", value: JsonConvert.SerializeObject(queryResult.PaginationData));

            return Ok(_mapper.Map<List<FirmDto>>(itemsResult));
        }

        itemsResult = (await _firmService.GetAll() as List<Firm>)!;
        return Ok(_mapper.Map<List<FirmDto>>(itemsResult));
    }

    // GET api/<FirmsController>/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FirmDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(int id)
    {
        var itemResult = await _firmService.GetById(id);
        return Ok(_mapper.Map<FirmDto>(itemResult));
    }

    // POST api/<FirmsController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FirmDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post([FromBody] FirmDto firmDto)
    {
        var itemResult = await _firmService.Create(_mapper.Map<FirmDto, Firm>(firmDto)!);
        var resourceUrl = $"{Request.Path}/{itemResult.Id}";
        return Created(resourceUrl, _mapper.Map<FirmDto>(itemResult));
    }

    // PUT api/<FirmsController>/5
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FirmDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Put([FromBody] FirmDto firmDto)
    {
        var itemResult = await _firmService.Edit(_mapper.Map<Firm>(firmDto)!);
        return Ok(_mapper.Map<FirmDto>(itemResult));
    }

    // DELETE api/<FirmsController>/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        await _firmService.Delete(id);
        return NoContent();
    }
}