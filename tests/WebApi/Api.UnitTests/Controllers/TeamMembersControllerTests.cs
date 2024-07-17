using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Api.Controllers.Tests;

[ExcludeFromCodeCoverage]
public class TeamMembersControllerTests
{
    private TeamMembersController _teammembersController = null!;

    private Mock<ITeamMemberService> _mockTeamMemberService = null!;

    private IMapper _mapper = null!;

    [SetUp]
    public void Setup()
    {
        _mockTeamMemberService = new Mock<ITeamMemberService>();
        _mapper = MapperCreator.CreateMapper();

        _teammembersController = new TeamMembersController(_mockTeamMemberService.Object, _mapper)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsOkWithListOfTeamMembers()
    {
        // Arrange
        var teammembersResponseExpected = TeamMemberMother.GetTeamMemberList();
        var teammembersDtoExpected = TeamMemberDtoMother.GetTeamMemberList();
        var queryRequest = QueryRequestMother.GetEmptyQueryRequest();

        _mockTeamMemberService.Setup(x => x.GetAll()).ReturnsAsync(teammembersResponseExpected);

        // Act
        var response = await _teammembersController.Get(queryRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var teammembersDtoResponse = response!.Value as List<TeamMemberDto>;
        teammembersDtoResponse.Should().NotBeNull();
        teammembersDtoResponse.Should().BeEquivalentTo(teammembersDtoExpected);

        _mockTeamMemberService.Verify(x => x.GetAll(), Times.Once());
    }

    [TestCase(null, null, null, null, null, null, null)]
    public async Task Get_WithQueryRequest_ReturnsOkWithFilteredTeamMembers(int? pageNumber, int? pageSize, string? searchString, string? columnName, FilterOptions? filterOptions, string? filterValue, SortOrders? sortOrders)
    {
        // Arrange
        var teammemberListResponseExpected = TeamMemberMother.GetTeamMemberList();
        var filterParams = FilterParamsMother.GetFilterParams(columnName!, filterOptions ?? FilterOptions.Contains, filterValue!);
        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, sortOrders ?? SortOrders.Asc);
        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, searchString, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<TeamMember>.Create(teammemberListResponseExpected, queryRequest);
        var teammembersDtoResponseExpected = _mapper.Map<List<TeamMemberDto>>(queryResultExpected.Items);
        var paginationDataResponseExpected = JsonConvert.SerializeObject(queryResultExpected.PaginationData);

        _mockTeamMemberService.Setup(x => x.GetByQueryRequestAsync(queryRequest)).ReturnsAsync(queryResultExpected);

        // Act
        var response = await _teammembersController.Get(queryRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var teammembersDtoResponse = response!.Value as List<TeamMemberDto>;
        teammembersDtoResponse.Should().NotBeNull();
        teammembersDtoResponse.Should().BeEquivalentTo(teammembersDtoResponseExpected);
        var paginationData = _teammembersController.ControllerContext.HttpContext.Response.Headers[PaginationConst.DefaultPaginationHeader].ToString();
        paginationData.Should().NotBeNull();
        paginationData.Should().BeEquivalentTo(paginationDataResponseExpected);

        _mockTeamMemberService.Verify(x => x.GetByQueryRequestAsync(It.IsAny<QueryRequest>()), Times.Once());
    }

    [Test]
    public async Task Get_WhenIdIsValid_ReturnsOkWithTeamMember()
    {
        // Arrange
        var teammemberResponseExpected = TeamMemberMother.DefaultTeamMemberLeader();
        var teammemberDtoResponseExpected = TeamMemberDtoMother.DefaultTeamMemberLeader();
        var id = teammemberResponseExpected.Id;

        _mockTeamMemberService.Setup(x => x.GetById(id)).ReturnsAsync(teammemberResponseExpected);

        // Act
        var response = await _teammembersController.Get(id) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var teammemberDtoResponse = response!.Value as TeamMemberDto;
        teammemberDtoResponse.Should().NotBeNull();
        teammemberDtoResponse.Should().BeEquivalentTo(teammemberDtoResponseExpected);

        _mockTeamMemberService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once());
    }

    [Test]
    public async Task Post_WhenTeamMemberIsValid_ReturnsCreatedWithTeamMember()
    {
        // Arrange
        var teammemberDtoRequest = TeamMemberDtoMother.DefaultTeamMemberLeader();
        teammemberDtoRequest.Id = 0;
        var teammemberDtoResponseExpected = TeamMemberDtoMother.DefaultTeamMemberLeader();
        var teammemberResponseExpected = TeamMemberMother.DefaultTeamMemberLeader();

        _mockTeamMemberService.Setup(x => x.Create(It.IsAny<TeamMember>())).ReturnsAsync(teammemberResponseExpected);

        // Act
        var response = await _teammembersController.Post(teammemberDtoRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status201Created);
        var teammemberDtoResponse = response!.Value as TeamMemberDto;
        teammemberDtoResponse.Should().NotBeNull();
        teammemberDtoResponse.Should().BeEquivalentTo(teammemberDtoResponseExpected);

        _mockTeamMemberService.Verify(x => x.Create(It.IsAny<TeamMember>()), Times.Once());
    }

    [Test]
    public async Task Put_WhenTeamMemberIsValid_ReturnsOkWithUpdatedTeamMember()
    {
        // Arrange
        var teammemberDtoRequest = TeamMemberDtoMother.DefaultTeamMemberLeader();
        var teammemberDtoResponseExpected = TeamMemberDtoMother.DefaultTeamMemberLeader();
        var teammemberResponseExpected = TeamMemberMother.DefaultTeamMemberLeader();

        _mockTeamMemberService.Setup(x => x.Edit(It.IsAny<TeamMember>())).ReturnsAsync(teammemberResponseExpected);

        // Act
        var response = await _teammembersController.Put(teammemberDtoRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var teammemberDtoResponse = response!.Value as TeamMemberDto;
        teammemberDtoResponse.Should().NotBeNull();
        teammemberDtoResponse.Should().BeEquivalentTo(teammemberDtoResponseExpected);

        _mockTeamMemberService.Verify(x => x.Edit(It.IsAny<TeamMember>()), Times.Once());
    }

    [Test]
    public async Task Delete_WhenIdIsValid_ReturnsNoContent()
    {
        // Arrange
        const int id = 1;

        // Act
        var response = await _teammembersController.Delete(id) as NoContentResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status204NoContent);

        _mockTeamMemberService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
    }
}