using FluentAssertions.Equivalency;
using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Api.Controllers.Tests;

[ExcludeFromCodeCoverage]
public class ActorsControllerTests
{
    private ActorsController _actorsController = null!;

    private Mock<IActorService> _mockActorService = null!;

    private Mock<IMapper> _mockMapper = null!;

    private static EquivalencyAssertionOptions<ActorDto> ExcludeProperties(EquivalencyAssertionOptions<ActorDto> options)
    {
        return options;
    }

    [SetUp]
    public void Setup()
    {
        _mockActorService = new Mock<IActorService>();
        _mockMapper = new Mock<IMapper>();

        _actorsController = new ActorsController(_mockActorService.Object, _mockMapper.Object)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsOkWithListOfActors()
    {
        // Arrange
        var actorsResponseExpected = ActorMother.GetActorList();
        var actorsDtoExpected = ActorDtoMother.GetActorList();
        var queryRequest = QueryRequestMother.GetEmptyQueryRequest();

        _mockMapper.Setup(x => x.Map<List<ActorDto>>(It.IsAny<List<Actor>>())).Returns(actorsDtoExpected);
        _mockActorService.Setup(x => x.GetAll()).ReturnsAsync(actorsResponseExpected);

        // Act
        var response = await _actorsController.Get(queryRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var usersDtoResponse = response!.Value as List<ActorDto>;
        usersDtoResponse.Should().NotBeNull();
        usersDtoResponse.Should().BeEquivalentTo(actorsDtoExpected, ExcludeProperties);

        _mockActorService.Verify(x => x.GetAll(), Times.Once());
    }

    [TestCase(null, null, null, null, null, null, null)]
    public async Task Get_WithQueryRequest_ReturnsOkWithFilteredActors(int? pageNumber, int? pageSize, string? searchString, string? columnName, FilterOptions? filterOptions, string? filterValue, SortOrders? sortOrders)
    {
        // Arrange
        var actorListResponseExpected = ActorMother.GetActorList();
        var filterParams = FilterParamsMother.GetFilterParams(columnName!, filterOptions ?? FilterOptions.Contains, filterValue!);
        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, sortOrders ?? SortOrders.Asc);
        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, searchString, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<Actor>.Create(actorListResponseExpected, queryRequest);
        var actorsDtoResponseExpected = ActorDtoMother.GetActorList();
        var paginationDataResponseExpected = JsonConvert.SerializeObject(queryResultExpected.PaginationData);

        _mockMapper.Setup(x => x.Map<List<ActorDto>>(It.IsAny<List<Actor>>())).Returns(actorsDtoResponseExpected);
        _mockActorService.Setup(x => x.GetByQueryRequestAsync(queryRequest)).ReturnsAsync(queryResultExpected);

        // Act
        var response = await _actorsController.Get(queryRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var usersDtoResponse = response!.Value as List<ActorDto>;
        usersDtoResponse.Should().NotBeNull();
        usersDtoResponse.Should().BeEquivalentTo(actorsDtoResponseExpected);
        var paginationData = _actorsController.ControllerContext.HttpContext.Response.Headers[PaginationConst.DefaultPaginationHeader].ToString();
        paginationData.Should().NotBeNull();
        paginationData.Should().BeEquivalentTo(paginationDataResponseExpected);

        _mockActorService.Verify(x => x.GetByQueryRequestAsync(It.IsAny<QueryRequest>()), Times.Once());
    }

    [Test]
    public async Task Get_WhenIdIsValid_ReturnsOkWithActor()
    {
        // Arrange
        var actorResponseExpected = ActorMother.ClaimantActor();
        var actorDtoResponseExpected = ActorDtoMother.ClaimantActor();
        var id = actorResponseExpected.Id;

        _mockMapper.Setup(x => x.Map<ActorDto>(It.IsAny<Actor>())).Returns(actorDtoResponseExpected);
        _mockActorService.Setup(x => x.GetById(id)).ReturnsAsync(actorResponseExpected);

        // Act
        var response = await _actorsController.Get(id) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var userDtoResponse = response!.Value as ActorDto;
        userDtoResponse.Should().NotBeNull();
        userDtoResponse.Should().BeEquivalentTo(actorDtoResponseExpected, ExcludeProperties);

        _mockActorService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once());
    }

    [Test]
    public async Task Post_WhenActorIsValid_ReturnsCreatedWithActor()
    {
        // Arrange
        var actorDtoRequest = ActorDtoMother.ClaimantActor();
        actorDtoRequest.Id = 0;
        var actorDtoResponseExpected = ActorDtoMother.ClaimantActor();
        var actorResponseExpected = ActorMother.ClaimantActor();

        _mockMapper.Setup(x => x.Map<ActorDto>(It.IsAny<Actor>())).Returns(actorDtoResponseExpected);
        _mockActorService.Setup(x => x.Create(It.IsAny<Actor>())).ReturnsAsync(actorResponseExpected);

        // Act
        var response = await _actorsController.Post(actorDtoRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status201Created);
        var userDtoResponse = response!.Value as ActorDto;
        userDtoResponse.Should().NotBeNull();
        userDtoResponse.Should().BeEquivalentTo(actorDtoResponseExpected, ExcludeProperties);

        _mockActorService.Verify(x => x.Create(It.IsAny<Actor>()), Times.Once());
    }

    [Test]
    public async Task Put_WhenActorIsValid_ReturnsOkWithUpdatedActor()
    {
        // Arrange
        var actorDtoRequest = ActorDtoMother.ClaimantActor();
        var actorDtoResponseExpected = ActorDtoMother.ClaimantActor();
        var actorResponseExpected = ActorMother.ClaimantActor();

        _mockMapper.Setup(x => x.Map<ActorDto>(It.IsAny<Actor>())).Returns(actorDtoResponseExpected);
        _mockActorService.Setup(x => x.Edit(It.IsAny<Actor>())).ReturnsAsync(actorResponseExpected);

        // Act
        var response = await _actorsController.Put(actorDtoRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var userDtoResponse = response!.Value as ActorDto;
        userDtoResponse.Should().NotBeNull();
        userDtoResponse.Should().BeEquivalentTo(actorDtoResponseExpected, ExcludeProperties);

        _mockActorService.Verify(x => x.Edit(It.IsAny<Actor>()), Times.Once());
    }

    [Test]
    public async Task Delete_WhenIdIsValid_ReturnsNoContent()
    {
        // Arrange
        const int id = 1;

        // Act
        var response = await _actorsController.Delete(id) as NoContentResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status204NoContent);

        _mockActorService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
    }
}