using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Api.Controllers.Tests;

[ExcludeFromCodeCoverage]
public class PermissionsControllerTests
{
    private PermissionsController _permissionsController = null!;

    private Mock<IPermissionService> _mockPermissionService = null!;

    private Mock<ICurrentUserService> _mockCurrentUserService = null!;

    private IMapper _mapper = null!;

    [SetUp]
    public void Setup()
    {
        _mockPermissionService = new Mock<IPermissionService>();
        _mockCurrentUserService = new Mock<ICurrentUserService>();
        _mapper = MapperCreator.CreateMapper();

        _permissionsController = new PermissionsController(_mockPermissionService.Object, _mapper)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsOkWithListOfPermissions()
    {
        // Arrange
        var permissionsResponseExpected = PermissionMother.GetPermissionList();
        var permissionsDtoExpected = PermissionDtoMother.GetPermissionList();
        var queryRequest = QueryRequestMother.GetEmptyQueryRequest();

        _mockPermissionService.Setup(x => x.GetAll()).ReturnsAsync(permissionsResponseExpected);

        // Act
        var response = await _permissionsController.Get(queryRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var permissionsDtoResponse = response!.Value as List<PermissionDto>;
        permissionsDtoResponse.Should().NotBeNull();
        permissionsDtoResponse.Should().BeEquivalentTo(permissionsDtoExpected);

        _mockPermissionService.Verify(x => x.GetAll(), Times.Once());
    }

    [TestCase(null, null, null, null, null, null, null)]
    public async Task Get_WithQueryRequest_ReturnsOkWithFilteredPermissions(int? pageNumber, int? pageSize, string? searchString, string? columnName, FilterOptions? filterOptions, string? filterValue, SortOrders? sortOrders)
    {
        // Arrange
        var permissionListResponseExpected = PermissionMother.GetPermissionList();
        var filterParams = FilterParamsMother.GetFilterParams(columnName!, filterOptions ?? FilterOptions.Contains, filterValue!);
        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, sortOrders ?? SortOrders.Asc);
        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, searchString, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<Permission>.Create(permissionListResponseExpected, queryRequest);
        var permissionsDtoResponseExpected = _mapper.Map<List<PermissionDto>>(queryResultExpected.Items);
        var paginationDataResponseExpected = JsonConvert.SerializeObject(queryResultExpected.PaginationData);

        _mockPermissionService.Setup(x => x.GetByQueryRequestAsync(queryRequest)).ReturnsAsync(queryResultExpected);

        // Act
        var response = await _permissionsController.Get(queryRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var permissionsDtoResponse = response!.Value as List<PermissionDto>;
        permissionsDtoResponse.Should().NotBeNull();
        permissionsDtoResponse.Should().BeEquivalentTo(permissionsDtoResponseExpected);
        var paginationData = _permissionsController.ControllerContext.HttpContext.Response.Headers[PaginationConst.DefaultPaginationHeader].ToString();
        paginationData.Should().NotBeNull();
        paginationData.Should().BeEquivalentTo(paginationDataResponseExpected);

        _mockPermissionService.Verify(x => x.GetByQueryRequestAsync(It.IsAny<QueryRequest>()), Times.Once());
    }

    [Test]
    public async Task Get_WhenIdIsValid_ReturnsOkWithPermission()
    {
        // Arrange
        var permissionResponseExpected = PermissionMother.GetDemandasPermission();
        var permissionDtoResponseExpected = PermissionDtoMother.GetDemandasPermission();
        var id = permissionResponseExpected.Id;

        _mockPermissionService.Setup(x => x.GetById(id)).ReturnsAsync(permissionResponseExpected);

        // Act
        var response = await _permissionsController.Get(id) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var permissionDtoResponse = response!.Value as PermissionDto;
        permissionDtoResponse.Should().NotBeNull();
        permissionDtoResponse.Should().BeEquivalentTo(permissionDtoResponseExpected);

        _mockPermissionService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once());
    }

    [Test]
    public async Task Post_WhenPermissionIsValid_ReturnsCreatedWithPermission()
    {
        // Arrange
        var permissionDtoRequest = PermissionDtoMother.GetDemandasPermission();
        permissionDtoRequest.Id = 0;
        var permissionDtoResponseExpected = PermissionDtoMother.GetDemandasPermission();
        var permissionResponseExpected = PermissionMother.GetDemandasPermission();

        _mockPermissionService.Setup(x => x.Create(It.IsAny<Permission>())).ReturnsAsync(permissionResponseExpected);

        // Act
        var response = await _permissionsController.Post(permissionDtoRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status201Created);
        var permissionDtoResponse = response!.Value as PermissionDto;
        permissionDtoResponse.Should().NotBeNull();
        permissionDtoResponse.Should().BeEquivalentTo(permissionDtoResponseExpected);

        _mockPermissionService.Verify(x => x.Create(It.IsAny<Permission>()), Times.Once());
    }

    [Test]
    public async Task Put_WhenPermissionIsValid_ReturnsOkWithUpdatedPermission()
    {
        // Arrange
        var permissionDtoRequest = PermissionDtoMother.GetDemandasPermission();
        var permissionDtoResponseExpected = PermissionDtoMother.GetDemandasPermission();
        var permissionResponseExpected = PermissionMother.GetDemandasPermission();

        _mockPermissionService.Setup(x => x.Edit(It.IsAny<Permission>())).ReturnsAsync(permissionResponseExpected);

        // Act
        var response = await _permissionsController.Put(permissionDtoRequest) as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var permissionDtoResponse = response!.Value as PermissionDto;
        permissionDtoResponse.Should().NotBeNull();
        permissionDtoResponse.Should().BeEquivalentTo(permissionDtoResponseExpected);

        _mockPermissionService.Verify(x => x.Edit(It.IsAny<Permission>()), Times.Once());
    }

    [Test]
    public async Task Delete_WhenIdIsValid_ReturnsNoContent()
    {
        // Arrange
        const int id = 1;

        // Act
        var response = await _permissionsController.Delete(id) as NoContentResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status204NoContent);

        _mockPermissionService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
    }

    [Test]
    public async Task GetAll_ByUser_Success()
    {
        // Arrange
        var permissionsResponseExpected = PermissionMother.GetPermissionList();
        var permissionsDtoExpected = PermissionDtoMother.GetPermissionList();

        _mockPermissionService.Setup(x => x.GetAllPermissionsByUserAsync()).ReturnsAsync(permissionsResponseExpected);
        _mockCurrentUserService.Setup(x => x.GetCurrentUserIdAsync()).ReturnsAsync(1);

        // Act
        var response = await _permissionsController.GetByUser() as ObjectResult;

        // Asserts
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be(StatusCodes.Status200OK);
        var permissionsDtoResponse = response!.Value as List<PermissionDto>;
        permissionsDtoResponse.Should().NotBeNull();
        permissionsDtoResponse.Should().BeEquivalentTo(permissionsDtoExpected);

        _mockPermissionService.Verify(x => x.GetAllPermissionsByUserAsync(), Times.Once());
    }
}