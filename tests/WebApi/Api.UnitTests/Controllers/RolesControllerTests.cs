using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Api.Controllers.Tests;

[ExcludeFromCodeCoverage]
public class RolesControllerTests
{
    private RolesController _rolesController = null!;

    private Mock<IRoleService> _mockRoleService = null!;

    private IMapper mapper = null!;

    [SetUp]
    public void Setup()
    {
        _mockRoleService = new Mock<IRoleService>();
        mapper = MapperCreator.CreateMapper();

        _rolesController = new RolesController(_mockRoleService.Object, mapper)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsOkWithListOfRoles()
    {
        // Arrange
        var rolesResponseExpected = RoleMother.GetRoleList();
        var rolesDtoExpected = RoleDtoMother.GetRoleList();
        var queryRequest = QueryRequestMother.GetEmptyQueryRequest();

        _mockRoleService.Setup(x => x.GetAll()).ReturnsAsync(rolesResponseExpected);

        // Act
        var response = await _rolesController.Get(queryRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var rolesDtoResponse = response!.Value as List<RoleDto>;
        rolesDtoResponse.Should().NotBeNull();
        rolesDtoResponse.Should().BeEquivalentTo(rolesDtoExpected);

        _mockRoleService.Verify(x => x.GetAll(), Times.Once());
    }

    [TestCase(null, null, null, null, null, null, null)]
    public async Task Get_WithQueryRequest_ReturnsOkWithFilteredRoles(int? pageNumber, int? pageSize, string? searchString, string? columnName, FilterOptions? filterOptions, string? filterValue, SortOrders? sortOrders)
    {
        // Arrange
        var roleListResponseExpected = RoleMother.GetRoleList();
        var filterParams = FilterParamsMother.GetFilterParams(columnName!, filterOptions ?? FilterOptions.Contains, filterValue!);
        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, sortOrders ?? SortOrders.Asc);
        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, searchString, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<Role>.Create(roleListResponseExpected, queryRequest);
        var rolesDtoResponseExpected = mapper.Map<List<RoleDto>>(queryResultExpected.Items);
        var paginationDataResponseExpected = JsonConvert.SerializeObject(queryResultExpected.PaginationData);

        _mockRoleService.Setup(x => x.GetByQueryRequestAsync(queryRequest)).ReturnsAsync(queryResultExpected);

        // Act
        var response = await _rolesController.Get(queryRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var rolesDtoResponse = response!.Value as List<RoleDto>;
        rolesDtoResponse.Should().NotBeNull();
        rolesDtoResponse.Should().BeEquivalentTo(rolesDtoResponseExpected);
        var paginationData = _rolesController.ControllerContext.HttpContext.Response.Headers[PaginationConst.DefaultPaginationHeader].ToString();
        paginationData.Should().NotBeNull();
        paginationData.Should().BeEquivalentTo(paginationDataResponseExpected);

        _mockRoleService.Verify(x => x.GetByQueryRequestAsync(It.IsAny<QueryRequest>()), Times.Once());
    }

    [Test]
    public async Task Get_WhenIdIsValid_ReturnsOkWithRole()
    {
        // Arrange
        var roleResponseExpected = RoleMother.DefaultBasicRole();
        var roleDtoResponseExpected = RoleDtoMother.DefaultBasicRole();
        var id = roleResponseExpected.Id;

        _mockRoleService.Setup(x => x.GetById(id)).ReturnsAsync(roleResponseExpected);

        // Act
        var response = await _rolesController.Get(id) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var roleDtoResponse = response!.Value as RoleDto;
        roleDtoResponse.Should().NotBeNull();
        roleDtoResponse.Should().BeEquivalentTo(roleDtoResponseExpected);

        _mockRoleService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once());
    }

    [Test]
    public async Task Post_WhenRoleIsValid_ReturnsCreatedWithRole()
    {
        // Arrange
        var roleDtoRequest = RoleDtoMother.DefaultBasicRole();
        roleDtoRequest.Id = 0;
        var roleDtoResponseExpected = RoleDtoMother.DefaultBasicRole();
        var roleResponseExpected = RoleMother.DefaultBasicRole();

        _mockRoleService.Setup(x => x.Create(It.IsAny<Role>())).ReturnsAsync(roleResponseExpected);

        // Act
        var response = await _rolesController.Post(roleDtoRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status201Created);
        var roleDtoResponse = response!.Value as RoleDto;
        roleDtoResponse.Should().NotBeNull();
        roleDtoResponse.Should().BeEquivalentTo(roleDtoResponseExpected);

        _mockRoleService.Verify(x => x.Create(It.IsAny<Role>()), Times.Once());
    }

    [Test]
    public async Task Put_WhenRoleIsValid_ReturnsOkWithUpdatedRole()
    {
        // Arrange
        var roleDtoRequest = RoleDtoMother.DefaultBasicRole();
        var roleDtoResponseExpected = RoleDtoMother.DefaultBasicRole();
        var roleResponseExpected = RoleMother.DefaultBasicRole();

        _mockRoleService.Setup(x => x.Edit(It.IsAny<Role>())).ReturnsAsync(roleResponseExpected);

        // Act
        var response = await _rolesController.Put(roleDtoRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var roleDtoResponse = response!.Value as RoleDto;
        roleDtoResponse.Should().NotBeNull();
        roleDtoResponse.Should().BeEquivalentTo(roleDtoResponseExpected);

        _mockRoleService.Verify(x => x.Edit(It.IsAny<Role>()), Times.Once());
    }

    [Test]
    public async Task Delete_WhenIdIsValid_ReturnsNoContent()
    {
        // Arrange
        const int id = 1;

        // Act
        var response = await _rolesController.Delete(id) as NoContentResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status204NoContent);

        _mockRoleService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
    }
}