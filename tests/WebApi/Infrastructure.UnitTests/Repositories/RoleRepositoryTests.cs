using Papirus.WebApi.Domain.Define.Enums;
using Papirus.WebApi.Domain.Dtos;

namespace Papirus.WebApi.Infrastructure.Repositories.Tests;

[ExcludeFromCodeCoverage]
public class RoleRepositoryTests
{
    private List<Role> roleList = null!;

    private RoleRepository roleRepository = null!;

    private Mock<AppDbContext> mockAppDbContext = null!;

    [SetUp]
    public void SetUp()
    {
        roleList = RoleMother.GetRoleList();
        mockAppDbContext = new Mock<AppDbContext>();
        mockAppDbContext.Setup(x => x.Set<Role>()).ReturnsDbSet(roleList);
        mockAppDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        roleRepository = new RoleRepository(mockAppDbContext.Object);
    }

    [Test]
    public async Task AddAsync_WhenCalled_AddsRoleSuccessfully()
    {
        // Arrange
        Role roleResponseExpected = RoleMother.DefaultAdministratorRole();
        var roleRequest = roleResponseExpected;

        // Act
        var roleResult = await roleRepository.AddAsync(roleRequest);

        // Assert
        roleResult.Should().NotBeNull();
        roleResult.Should().BeEquivalentTo(roleResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Role>(), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task FindAsync_WhenCalled_ReturnsMatchingRoles()
    {
        // Arrange
        var roleListResponseExpected = RoleMother.GetRoleList().Where(x => x.Id > 1);

        // Act
        var roleListResult = await roleRepository.FindAsync(x => x.Id > 1);

        // Asserts
        roleListResult.Should().NotBeNull();
        roleListResult.Should().BeEquivalentTo(roleListResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Role>(), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_WhenCalled_ReturnsAllRoles()
    {
        // Arrange
        var roleListResponseExpected = RoleMother.GetRoleList();

        // Act
        var roleListResult = await roleRepository.GetAllAsync();

        // Asserts
        roleListResult.Should().NotBeNull();
        roleListResult.Should().BeEquivalentTo(roleListResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Role>(), Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_WhenCalled_ReturnsExpectedRole()
    {
        // Arrange
        var roleResponseExpected = RoleMother.DefaultAdministratorRole();
        int id = roleResponseExpected.Id;

        // Act
        var roleResult = await roleRepository.GetByIdAsync(id);

        // Assert
        roleResult.Should().NotBeNull();
        roleResult.Should().BeEquivalentTo(roleResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Role>(), Times.Once);
    }

    [Test]
    public async Task RemoveAsync_WhenCalled_RemovesRoleSuccessfully()
    {
        // Arrange
        var roleResponseExpected = RoleMother.DefaultAdministratorRole();
        var roleRequest = roleResponseExpected;

        // Act
        await roleRepository.RemoveAsync(roleRequest);

        // Assert
        mockAppDbContext.Verify(x => x.Set<Role>().Remove(It.IsAny<Role>()), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpUpdateAsync_WhenCalled_UpdatesRoleSuccessfullydate_Success()
    {
        // Arrange
        var roleResponseExpected = RoleMother.DefaultAdministratorRole();
        var roleRequest = roleResponseExpected;

        // Act
        var roleResult = await roleRepository.UpdateAsync(roleRequest);

        // Assert
        roleResult.Should().NotBeNull();
        roleResult.Should().BeEquivalentTo(roleResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Role>().Update(It.IsAny<Role>()), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Ignore("Due date")]
    [TestCase(null, null, null, null, null, null, null)]
    [TestCase(1, null, null, null, null, null, null)]
    [TestCase(null, 10, null, null, null, null, null)]
    [TestCase(1, 10, "RoleName1", null, null, null, null)]
    [TestCase(1, 10, null, "Name", FilterOptions.StartsWith, "Role", null)]
    [TestCase(1, 10, null, "Name", FilterOptions.EndsWith, "1", null)]
    [TestCase(1, 10, null, "Name", FilterOptions.Contains, "Role", null)]
    [TestCase(1, 10, null, "Name", FilterOptions.DoesNotContain, "NOTCONTAINS", null)]
    [TestCase(1, 10, null, "Name", FilterOptions.IsEmpty, null, null)]
    [TestCase(1, 10, null, "Name", FilterOptions.IsNotEmpty, null, null)]
    [TestCase(1, 10, null, "Id", FilterOptions.IsGreaterThan, "10", null)]
    [TestCase(1, 10, null, "Id", FilterOptions.IsGreaterThanOrEqualTo, "10", null)]
    [TestCase(1, 10, null, "Id", FilterOptions.IsLessThan, "10", null)]
    [TestCase(1, 10, null, "Id", FilterOptions.IsLessThanOrEqualTo, "10", null)]
    [TestCase(1, 10, null, "Id", FilterOptions.IsEqualTo, "1", null)]
    [TestCase(1, 10, null, "Id", FilterOptions.IsNotEqualTo, "1", null)]
    [TestCase(1, 10, null, "Name", null, null, SortOrders.Asc)]
    [TestCase(1, 10, null, "Name", null, null, SortOrders.Desc)]
    public async Task GetByQueryRequestAsync_WhenCalledWithParameters_ReturnsExpectedRoles(int? pageNumber, int? pageSize, string? searchString, string? columnName, FilterOptions? filterOptions, string? filterValue, SortOrders? sortOrders)
    {
        // Arrange
        var roleListResponseExpected = RoleMother.GetRoleList(CommonConst.MaxCount);
        var filterParams = FilterParamsMother.GetFilterParams(columnName!, filterOptions ?? FilterOptions.IsNotEmpty, filterValue!);
        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, sortOrders ?? SortOrders.Asc);
        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, searchString, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<Role>.Create(roleListResponseExpected, queryRequest);

        mockAppDbContext.Setup(x => x.Set<Role>()).ReturnsDbSet(roleListResponseExpected);

        // Act
        var queryResult = await roleRepository.GetByQueryRequestAsync(queryRequest);

        // Asserts
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().NotBeNull();
        queryResult.PaginationData.Should().NotBeNull();
        queryResult.Items.Should().BeEquivalentTo(queryResultExpected.Items);
        queryResult.PaginationData.Should().BeEquivalentTo(queryResultExpected.PaginationData);

        mockAppDbContext.Verify(x => x.Set<Role>(), Times.Once);
    }

    [Ignore("Due date")]
    [Test]
    public async Task GetByQueryRequestAsync_WhenCalledWithMultipleFilterParameters_ReturnsExpectedRoles()
    {
        // Arrange
        const int pageNumber = 1;
        const int pageSize = 10;
        const string columnName = "Name";
        var roleListResponseExpected = RoleMother.GetRoleList(CommonConst.MaxCount);

        List<FilterParams> filterParams =
        [
            FilterParamsMother.Create(columnName!, FilterOptions.IsNotEmpty, ""),
            FilterParamsMother.Create(columnName!, FilterOptions.Contains, "Role"),
        ];

        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, SortOrders.Asc);

        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, null, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<Role>.Create(roleListResponseExpected, queryRequest);

        mockAppDbContext.Setup(x => x.Set<Role>()).ReturnsDbSet(roleListResponseExpected);

        // Act
        var queryResult = await roleRepository.GetByQueryRequestAsync(queryRequest);

        // Asserts
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().NotBeNull();
        queryResult.PaginationData.Should().NotBeNull();
        queryResult.Items.Should().BeEquivalentTo(queryResultExpected.Items);
        queryResult.PaginationData.Should().BeEquivalentTo(queryResultExpected.PaginationData);

        mockAppDbContext.Verify(x => x.Set<Role>(), Times.Once);
    }

    [Ignore("Due date")]
    [TestCase("Id", SortOrders.Asc, "Name", SortOrders.Asc)]
    [TestCase("Id", SortOrders.Asc, "Name", SortOrders.Desc)]
    [TestCase("Id", SortOrders.Desc, "Name", SortOrders.Asc)]
    [TestCase("Id", SortOrders.Desc, "Name", SortOrders.Desc)]
    public async Task GetByQueryRequestAsync_WhenCalledWithMultipleSortingParameters_ReturnsExpectedRoles(string column1, SortOrders sortOrders1, string column2, SortOrders sortOrders2)
    {
        // Arrange
        const int pageNumber = 1;
        const int pageSize = 10;
        const string columnName = "Name";
        const string filterValue = "Role";

        var roleListResponseExpected = RoleMother.GetRoleList(CommonConst.MaxCount);

        var filterParams = FilterParamsMother.GetFilterParams(columnName!, FilterOptions.Contains, filterValue!);

        List<SortingParams> sortingParams = [
            SortingParamsMother.Create(column1, sortOrders1),
            SortingParamsMother.Create(column2, sortOrders2)
        ];

        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, null, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<Role>.Create(roleListResponseExpected, queryRequest);

        mockAppDbContext.Setup(x => x.Set<Role>()).ReturnsDbSet(roleListResponseExpected);

        // Act
        var queryResult = await roleRepository.GetByQueryRequestAsync(queryRequest);

        // Asserts
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().NotBeNull();
        queryResult.PaginationData.Should().NotBeNull();
        queryResult.Items.Should().BeEquivalentTo(queryResultExpected.Items);
        queryResult.PaginationData.Should().BeEquivalentTo(queryResultExpected.PaginationData);

        mockAppDbContext.Verify(x => x.Set<Role>(), Times.Once);
    }
}