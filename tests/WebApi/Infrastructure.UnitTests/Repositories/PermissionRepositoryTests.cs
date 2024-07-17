using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Infrastructure.Repositories.Tests;

[ExcludeFromCodeCoverage]
public class PermissionRepositoryTests
{
    private List<Permission> _permissionList = null!;

    private PermissionRepository _permissionRepository = null!;

    private Mock<AppDbContext> _mockAppDbContext = null!;

    [SetUp]
    public void SetUp()
    {
        _permissionList = PermissionMother.GetPermissionList();
        _mockAppDbContext = new Mock<AppDbContext>();
        _mockAppDbContext.Setup(x => x.Set<Permission>()).ReturnsDbSet(_permissionList);
        _mockAppDbContext.Setup(x => x.Permissions).ReturnsDbSet(_permissionList);
        _mockAppDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        _permissionRepository = new PermissionRepository(_mockAppDbContext.Object);
    }

    [Test]
    public async Task AddAsync_WhenCalled_AddsPermissionSuccessfully()
    {
        // Arrange
        Permission permissionResponseExpected = PermissionMother.GetDemandasPermission();
        var permissionRequest = permissionResponseExpected;

        // Act
        var permissionResult = await _permissionRepository.AddAsync(permissionRequest);

        // Assert
        permissionResult.Should().NotBeNull();
        permissionResult.Should().BeEquivalentTo(permissionResponseExpected);

        _mockAppDbContext.Verify(x => x.Set<Permission>(), Times.Once);
        _mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task FindAsync_WhenCalled_ReturnsMatchingPermissions()
    {
        // Arrange
        var permissionListResponseExpected = PermissionMother.GetPermissionList().Where(x => x.Id > 1);

        // Act
        var permissionListResult = await _permissionRepository.FindAsync(x => x.Id > 1);

        // Asserts
        permissionListResult.Should().NotBeNull();
        permissionListResult.Should().BeEquivalentTo(permissionListResponseExpected);

        _mockAppDbContext.Verify(x => x.Set<Permission>(), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_WhenCalled_ReturnsAllPermissions()
    {
        // Arrange
        var permissionListResponseExpected = PermissionMother.GetPermissionList();
        _mockAppDbContext.Setup(x => x.Set<Permission>()).ReturnsDbSet(permissionListResponseExpected);

        // Act
        var permissionListResult = await _permissionRepository.GetAllAsync();

        // Asserts
        permissionListResult.Should().NotBeNull();
        permissionListResult.Should().BeEquivalentTo(permissionListResponseExpected);

        _mockAppDbContext.Verify(x => x.Set<Permission>(), Times.Once);
    }

    [Test]
    public async Task GetAllByUserAsync_WhenCalled_ReturnsAllPermissionsByUserId()
    {
        // Arrange
        List<Permission> permissionExpected = PermissionMother.GetPermissionList().Where(d => d.RolePermissions.Any(x => x.Role.Users.Any(u => u.Id == 1))).ToList();

        // Act
        var permissionListResult = await _permissionRepository.GetAllByUser(userId: 1);

        // Asserts
        permissionListResult.Should().NotBeNull();
        permissionListResult.Should().BeEquivalentTo(permissionExpected);

        _mockAppDbContext.Verify(x => x.Permissions, Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_WhenCalled_ReturnsExpectedPermission()
    {
        // Arrange
        var permissionResponseExpected = PermissionMother.GetDemandasPermission();
        int id = permissionResponseExpected.Id;

        // Act
        var permissionResult = await _permissionRepository.GetByIdAsync(id);

        // Assert
        permissionResult.Should().NotBeNull();
        permissionResult.Should().BeEquivalentTo(permissionResponseExpected);

        _mockAppDbContext.Verify(x => x.Set<Permission>(), Times.Once);
    }

    [Test]
    public async Task RemoveAsync_WhenCalled_RemovesPermissionSuccessfully()
    {
        // Arrange
        var permissionResponseExpected = PermissionMother.GetDemandasPermission();
        var permissionRequest = permissionResponseExpected;

        // Act
        await _permissionRepository.RemoveAsync(permissionRequest);

        // Assert
        _mockAppDbContext.Verify(x => x.Set<Permission>().Remove(It.IsAny<Permission>()), Times.Once);
        _mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_WhenCalled_UpdatesPermissionSuccessfully()
    {
        // Arrange
        var permissionResponseExpected = PermissionMother.GetDemandasPermission();
        var permissionRequest = permissionResponseExpected;

        // Act
        var permissionResult = await _permissionRepository.UpdateAsync(permissionRequest);

        // Assert
        permissionResult.Should().NotBeNull();
        permissionResult.Should().BeEquivalentTo(permissionResponseExpected);

        _mockAppDbContext.Verify(x => x.Set<Permission>().Update(It.IsAny<Permission>()), Times.Once);
        _mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Ignore("Due date")]
    [TestCase(null, null, null, null, null, null, null)]
    public async Task GetByQueryRequestAsync_WhenCalledWithParameters_ReturnsExpectedPermissions(int? pageNumber, int? pageSize, string? searchString, string? columnName, FilterOptions? filterOptions, string? filterValue, SortOrders? sortOrders)
    {
        // Arrange
        var permissionListResponseExpected = PermissionMother.GetPermissionList(CommonConst.MaxCount);
        var filterParams = FilterParamsMother.GetFilterParams(columnName!, filterOptions ?? FilterOptions.IsNotEmpty, filterValue!);
        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, sortOrders ?? SortOrders.Asc);
        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, searchString, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<Permission>.Create(permissionListResponseExpected, queryRequest);

        _mockAppDbContext.Setup(x => x.Set<Permission>()).ReturnsDbSet(permissionListResponseExpected);

        // Act
        var queryResult = await _permissionRepository.GetByQueryRequestAsync(queryRequest);

        // Asserts
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().NotBeNull();
        queryResult.PaginationData.Should().NotBeNull();
        queryResult.Items.Should().BeEquivalentTo(queryResultExpected.Items);
        queryResult.PaginationData.Should().BeEquivalentTo(queryResultExpected.PaginationData);

        _mockAppDbContext.Verify(x => x.Set<Permission>(), Times.Once);
    }
}