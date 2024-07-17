using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Infrastructure.Repositories.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class UserRepositoryTests
{
    private List<User> userList = null!;

    private UserRepository userRepository = null!;

    private Mock<AppDbContext> mockAppDbContext = null!;

    private static EquivalencyAssertionOptions<User> ExcludeProperties(EquivalencyAssertionOptions<User> options)
    {
        options.Excluding(t => t.RegistrationDate);
        return options;
    }

    [SetUp]
    public void SetUp()
    {
        userList = UserMother.GetUserList();
        mockAppDbContext = new Mock<AppDbContext>();
        mockAppDbContext.Setup(x => x.Set<User>()).ReturnsDbSet(userList);
        mockAppDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        userRepository = new UserRepository(mockAppDbContext.Object);
    }

    [Test]
    public async Task AddAsync_WhenCalled_AddsUserSuccessfully()
    {
        // Arrange
        User userResponseExpected = UserMother.BasicUser();
        var userRequest = userResponseExpected;

        // Act
        var userResult = await userRepository.AddAsync(userRequest);

        // Assert
        userResult.Should().NotBeNull();
        userResult.Should().BeEquivalentTo(userResponseExpected);

        mockAppDbContext.Verify(x => x.Set<User>(), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task FindAsync_WhenCalled_ReturnsMatchingUsers()
    {
        // Arrange
        var userListResponseExpected = UserMother.GetUserList().Where(x => x.Id > 1);

        // Act
        var userListResult = await userRepository.FindAsync(x => x.Id > 1);

        // Asserts
        userListResult.Should().NotBeNull();
        userListResult.Should().BeEquivalentTo(userListResponseExpected, ExcludeProperties);

        mockAppDbContext.Verify(x => x.Set<User>(), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_WhenCalled_ReturnsAllUsers()
    {
        // Arrange
        var userListResponseExpected = UserMother.GetUserList();

        // Act
        var userListResult = await userRepository.GetAllAsync();

        // Asserts
        userListResult.Should().NotBeNull();
        userListResult.Should().BeEquivalentTo(userListResponseExpected, ExcludeProperties);

        mockAppDbContext.Verify(x => x.Set<User>(), Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_WhenCalled_ReturnsExpectedUser()
    {
        // Arrange
        var userResponseExpected = UserMother.BasicUser();
        int id = userResponseExpected.Id;

        // Act
        var userResult = await userRepository.GetByIdAsync(id);

        // Assert
        userResult.Should().NotBeNull();
        userResult.Should().BeEquivalentTo(userResponseExpected, ExcludeProperties);

        mockAppDbContext.Verify(x => x.Set<User>(), Times.Once);
    }

    [Test]
    public async Task RemoveAsync_WhenCalled_RemovesUserSuccessfully()
    {
        // Arrange
        var userResponseExpected = UserMother.BasicUser();
        var userRequest = userResponseExpected;

        // Act
        await userRepository.RemoveAsync(userRequest);

        // Assert
        mockAppDbContext.Verify(x => x.Set<User>().Remove(It.IsAny<User>()), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_WhenCalled_UpdatesUserSuccessfully()
    {
        // Arrange
        var userResponseExpected = UserMother.BasicUser();
        var userRequest = userResponseExpected;

        // Act
        var userResult = await userRepository.UpdateAsync(userRequest);

        // Assert
        userResult.Should().NotBeNull();
        userResult.Should().BeEquivalentTo(userResponseExpected);

        mockAppDbContext.Verify(x => x.Set<User>().Update(It.IsAny<User>()), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Ignore("Due date")]
    [TestCase(1, 10, null, "Email", null, null, SortOrders.Asc)]
    public async Task GetByQueryRequestAsync_WhenCalledWithParameters_ReturnsExpectedUsers(int? pageNumber, int? pageSize, string? searchString, string? columnName, FilterOptions? filterOptions, string? filterValue, SortOrders? sortOrders)
    {
        // Arrange
        var userListResponseExpected = UserMother.GetUserList(CommonConst.MaxCount);
        var filterParams = FilterParamsMother.GetFilterParams(columnName!, filterOptions ?? FilterOptions.IsNotEmpty, filterValue!);
        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, sortOrders ?? SortOrders.Asc);
        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, searchString, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<User>.Create(userListResponseExpected, queryRequest);

        mockAppDbContext.Setup(x => x.Set<User>()).ReturnsDbSet(userListResponseExpected);

        // Act
        var queryResult = await userRepository.GetByQueryRequestAsync(queryRequest);

        // Asserts
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().NotBeNull();
        queryResult.PaginationData.Should().NotBeNull();
        queryResult.Items.Should().BeEquivalentTo(queryResultExpected.Items);
        queryResult.PaginationData.Should().BeEquivalentTo(queryResultExpected.PaginationData);

        mockAppDbContext.Verify(x => x.Set<User>(), Times.Once);
    }

    [Test]
    public async Task FindByEmailAsync_WhenCalled_ReturnsExpectedUser()
    {
        // Arrange
        var userExpected = UserMother.BasicUser();
        var email = userExpected.Email;
        mockAppDbContext.Setup(x => x.Users).ReturnsDbSet(userList);

        // Act
        var userResult = await userRepository.FindByEmailAsync(email);

        // Assert
        userResult.Should().NotBeNull();
        userResult.Should().BeEquivalentTo(userExpected, ExcludeProperties);

        mockAppDbContext.Verify(x => x.Users, Times.Once);
    }
}