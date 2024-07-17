using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Api.Controllers.Tests;

[ExcludeFromCodeCoverage]
public class TeamsControllerTests
{
    private TeamsController _teamsController = null!;

    private Mock<ITeamService> _mockTeamService = null!;

    private Mock<ITeamMemberService> _mockTeamMemberService = null!;

    private IMapper _mapper = null!;

    [SetUp]
    public void Setup()
    {
        _mockTeamService = new Mock<ITeamService>();
        _mockTeamMemberService = new Mock<ITeamMemberService>();
        _mapper = MapperCreator.CreateMapper();

        _teamsController = new TeamsController(_mockTeamService.Object, _mockTeamMemberService.Object, _mapper)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsOkWithListOfTeams()
    {
        // Arrange
        var teamsResponseExpected = TeamMother.GetTeamList();
        var teamsDtoExpected = TeamDtoMother.GetTeamList();
        var queryRequest = QueryRequestMother.GetEmptyQueryRequest();

        _mockTeamService.Setup(x => x.GetAll()).ReturnsAsync(teamsResponseExpected);

        // Act
        var response = await _teamsController.Get(queryRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var teamsDtoResponse = response!.Value as List<TeamDto>;
        teamsDtoResponse.Should().NotBeNull();
        teamsDtoResponse.Should().BeEquivalentTo(teamsDtoExpected);

        _mockTeamService.Verify(x => x.GetAll(), Times.Once());
    }

    [TestCase(null, null, null, null, null, null, null)]
    public async Task Get_WithQueryRequest_ReturnsOkWithFilteredTeams(int? pageNumber, int? pageSize, string? searchString, string? columnName, FilterOptions? filterOptions, string? filterValue, SortOrders? sortOrders)
    {
        // Arrange
        var teamListResponseExpected = TeamMother.GetTeamList();
        var filterParams = FilterParamsMother.GetFilterParams(columnName!, filterOptions ?? FilterOptions.Contains, filterValue!);
        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, sortOrders ?? SortOrders.Asc);
        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, searchString, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<Team>.Create(teamListResponseExpected, queryRequest);
        var teamsDtoResponseExpected = _mapper.Map<List<TeamDto>>(queryResultExpected.Items);
        var paginationDataResponseExpected = JsonConvert.SerializeObject(queryResultExpected.PaginationData);

        _mockTeamService.Setup(x => x.GetByQueryRequestAsync(queryRequest)).ReturnsAsync(queryResultExpected);

        // Act
        var response = await _teamsController.Get(queryRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var teamsDtoResponse = response!.Value as List<TeamDto>;
        teamsDtoResponse.Should().NotBeNull();
        teamsDtoResponse.Should().BeEquivalentTo(teamsDtoResponseExpected);
        var paginationData = _teamsController.ControllerContext.HttpContext.Response.Headers[PaginationConst.DefaultPaginationHeader].ToString();
        paginationData.Should().NotBeNull();
        paginationData.Should().BeEquivalentTo(paginationDataResponseExpected);

        _mockTeamService.Verify(x => x.GetByQueryRequestAsync(It.IsAny<QueryRequest>()), Times.Once());
    }

    [Test]
    public async Task GetById_Success()
    {
        // Arrange
        var teamResponseExpected = TeamMother.DefaultCautelasTeam();
        var teamDtoResponseExpected = TeamDtoMother.DefaultCautelasTeam();
        var id = teamResponseExpected.Id;

        _mockTeamService.Setup(x => x.GetById(id)).ReturnsAsync(teamResponseExpected);

        // Act
        var response = await _teamsController.Get(id) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var teamDtoResponse = response!.Value as TeamDto;
        teamDtoResponse.Should().NotBeNull();
        teamDtoResponse.Should().BeEquivalentTo(teamDtoResponseExpected);

        _mockTeamService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once());
    }

    [Test]
    public async Task Get_WhenIdIsValid_ReturnsOkWithTeam()
    {
        // Arrange
        var teamId = 1;
        var teamMembersResponseExpected = TeamMemberMother.GetTeamMemberList();
        var teamMembersDtoExpected = _mapper.Map<List<TeamMemberDto>>(teamMembersResponseExpected);

        _mockTeamMemberService.Setup(x => x.GetByTeamId(teamId)).ReturnsAsync(teamMembersResponseExpected);

        // Act
        var response = await _teamsController.GetMembers(teamId) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var teamMembersDtoResponse = response!.Value as List<TeamMemberDto>;
        teamMembersDtoResponse.Should().NotBeNull();
        teamMembersDtoResponse.Should().BeEquivalentTo(teamMembersDtoExpected);

        _mockTeamMemberService.Verify(x => x.GetByTeamId(teamId), Times.Once());
    }

    [Test]
    public async Task Post_WhenTeamIsValid_ReturnsCreatedWithTeam()
    {
        // Arrange
        var teamDtoRequest = TeamDtoMother.DefaultCautelasTeam();
        teamDtoRequest.Id = 0;
        var teamDtoResponseExpected = TeamDtoMother.DefaultCautelasTeam();
        var teamResponseExpected = TeamMother.DefaultCautelasTeam();

        _mockTeamService.Setup(x => x.Create(It.IsAny<Team>())).ReturnsAsync(teamResponseExpected);

        // Act
        var response = await _teamsController.Post(teamDtoRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status201Created);
        var teamDtoResponse = response!.Value as TeamDto;
        teamDtoResponse.Should().NotBeNull();
        teamDtoResponse.Should().BeEquivalentTo(teamDtoResponseExpected);

        _mockTeamService.Verify(x => x.Create(It.IsAny<Team>()), Times.Once());
    }

    [Test]
    public async Task Put_WhenTeamIsValid_ReturnsOkWithUpdatedTeam()
    {
        // Arrange
        var teamDtoRequest = TeamDtoMother.DefaultCautelasTeam();
        var teamDtoResponseExpected = TeamDtoMother.DefaultCautelasTeam();
        var teamResponseExpected = TeamMother.DefaultCautelasTeam();

        _mockTeamService.Setup(x => x.Edit(It.IsAny<Team>())).ReturnsAsync(teamResponseExpected);

        // Act
        var response = await _teamsController.Put(teamDtoRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var teamDtoResponse = response!.Value as TeamDto;
        teamDtoResponse.Should().NotBeNull();
        teamDtoResponse.Should().BeEquivalentTo(teamDtoResponseExpected);

        _mockTeamService.Verify(x => x.Edit(It.IsAny<Team>()), Times.Once());
    }

    [Test]
    public async Task Delete_WhenIdIsValid_ReturnsNoContent()
    {
        // Arrange
        const int id = 1;

        // Act
        var response = await _teamsController.Delete(id) as NoContentResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status204NoContent);

        _mockTeamService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
    }
}