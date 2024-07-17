using Papirus.WebApi.Domain.Define.Enums;
using System.Security.Claims;

namespace Papirus.WebApi.Api.Controllers.Tests;

[ExcludeFromCodeCoverage]
public class UsersControllerTests
{
    private UsersController _usersController = null!;

    private Mock<IUserService> _mockUserService = null!;

    private Mock<IAuthenticationService> _mockAuthenticationService = null!;

    private IMapper _mapper = null!;

    [SetUp]
    public void Setup()
    {
        _mockUserService = new Mock<IUserService>();
        _mockAuthenticationService = new Mock<IAuthenticationService>();
        _mapper = MapperCreator.CreateMapper();

        var headers = new HeaderDictionary();

        var response = new Mock<HttpResponse>();
        response.SetupGet(r => r.Headers).Returns(headers);

        var httpContext = new Mock<HttpContext>();
        httpContext.SetupGet(hc => hc.Response).Returns(response.Object);

        _usersController = new UsersController(_mockUserService.Object, _mockAuthenticationService.Object, _mapper)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = httpContext.Object
            }
        };

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new ("firmId", "1"),
            new ("roleId", "1")
        }));

        httpContext.Setup(hc => hc.User).Returns(claimsPrincipal);
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsOkWithListOfUsers()
    {
        // Arrange
        var usersResponseExpected = UserMother.GetUserList();
        var usersDtoExpected = UserDtoMother.GetUserList();
        var queryRequest = QueryRequestMother.GetEmptyQueryRequest();

        _mockUserService.Setup(x => x.GetAll()).ReturnsAsync(usersResponseExpected);

        // Act
        var response = await _usersController.Get(queryRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var usersDtoResponse = response!.Value as List<UserDto>;
        usersDtoResponse.Should().NotBeNull();
        usersDtoResponse.Should().BeEquivalentTo(usersDtoExpected);

        _mockUserService.Verify(x => x.GetAll(), Times.Once());
    }

    [TestCase(null, null, null, null, null, null, null)]
    public async Task Get_WithQueryRequest_ReturnsOkWithFilteredUsers(int? pageNumber, int? pageSize, string? searchString, string? columnName, FilterOptions? filterOptions, string? filterValue, SortOrders? sortOrders)
    {
        // Arrange
        var userListResponseExpected = UserMother.GetUserList();
        var filterParams = FilterParamsMother.GetFilterParams(columnName!, filterOptions ?? FilterOptions.Contains, filterValue!);
        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, sortOrders ?? SortOrders.Asc);
        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, searchString, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<User>.Create(userListResponseExpected, queryRequest);
        var usersDtoResponseExpected = _mapper.Map<List<UserDto>>(queryResultExpected.Items);
        var paginationDataResponseExpected = JsonConvert.SerializeObject(queryResultExpected.PaginationData);

        _mockUserService.Setup(x => x.GetByQueryRequestAsync(queryRequest)).ReturnsAsync(queryResultExpected);

        // Act
        var response = await _usersController.Get(queryRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var usersDtoResponse = response!.Value as List<UserDto>;
        usersDtoResponse.Should().NotBeNull();
        usersDtoResponse.Should().BeEquivalentTo(usersDtoResponseExpected);
        var paginationData = _usersController.ControllerContext.HttpContext.Response.Headers[PaginationConst.DefaultPaginationHeader].ToString();
        paginationData.Should().NotBeNull();
        paginationData.Should().BeEquivalentTo(paginationDataResponseExpected);

        _mockUserService.Verify(x => x.GetByQueryRequestAsync(It.IsAny<QueryRequest>()), Times.Once());
    }

    [Test]
    public async Task Get_WhenIdIsValid_ReturnsOkWithUser()
    {
        // Arrange
        var userResponseExpected = UserMother.BasicUser();
        var userDtoResponseExpected = UserDtoMother.BasicUser();
        var id = userResponseExpected.Id;

        _mockUserService.Setup(x => x.GetById(id)).ReturnsAsync(userResponseExpected);

        // Act
        var response = await _usersController.Get(id) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var userDtoResponse = response!.Value as UserDto;
        userDtoResponse.Should().NotBeNull();
        userDtoResponse.Should().BeEquivalentTo(userDtoResponseExpected);

        _mockUserService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once());
    }

    [Test]
    public async Task Post_WhenUserIsValid_ReturnsCreatedWithUser()
    {
        // Arrange
        var userDtoRequest = UserInputDtoMother.BasicValidUser();
        var userDtoResponseExpected = UserDtoMother.BasicUser();
        var userResponseExpected = UserMother.BasicUser();

        _mockAuthenticationService.Setup(x => x.Register(It.IsAny<User>(), userDtoRequest.Password, It.IsAny<int>())).ReturnsAsync(userResponseExpected);

        // Act
        var response = await _usersController.Post(userDtoRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status201Created);
        var userDtoResponse = response!.Value as UserDto;
        userDtoResponse.Should().NotBeNull();
        userDtoResponse.Should().BeEquivalentTo(userDtoResponseExpected);

        _mockAuthenticationService.Verify(x => x.Register(It.IsAny<User>(), userDtoRequest.Password, It.IsAny<int>()), Times.Once());
    }

    [Test]
    public async Task Delete_WhenIdIsValid_ReturnsNoContent()
    {
        // Arrange
        const int id = 1;

        // Act
        var response = await _usersController.Delete(id) as NoContentResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status204NoContent);

        _mockUserService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
    }
}