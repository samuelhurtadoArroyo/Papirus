namespace Papirus.WebApi.Application.Services.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class UserServiceTests
{
    private UserService userService = null!;

    private Mock<IUserRepository> mockUserRepository = null!;

    [SetUp]
    public void Setup()
    {
        mockUserRepository = new Mock<IUserRepository>();
        userService = new UserService(mockUserRepository.Object);
    }

    [Test]
    public async Task Delete_WhenUserExists_DeletesUserSuccessfully()
    {
        // Arrange
        var userRequest = UserMother.BasicUser();
        var userResponseExpected = UserMother.BasicUser();
        int id = userRequest.Id;

        mockUserRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(userResponseExpected);
        mockUserRepository.Setup(x => x.RemoveAsync(userRequest)).Verifiable();

        // Act
        await userService.Delete(id);

        // Asserts
        mockUserRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockUserRepository.Verify(x => x.RemoveAsync(It.IsAny<User>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenUserDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 0;
        User userResponseExpected = null!;

        mockUserRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(userResponseExpected);

        // Act
        Func<Task> action = async () => await userService.Delete(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockUserRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockUserRepository.Verify(x => x.RemoveAsync(It.IsAny<User>()), Times.Never);
    }

    [Test]
    public async Task Edit_WhenUserIsValid_UpdatesUserSuccessfully()
    {
        // Arrange
        var userResponseExpected = UserMother.BasicUser();
        var userRequest = userResponseExpected;
        var userResponse = UserMother.BasicUser();
        int id = userResponse.Id;

        mockUserRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(userResponse);
        mockUserRepository.Setup(x => x.UpdateAsync(userRequest)).ReturnsAsync(userResponseExpected);

        // Act
        var firmResult = await userService.Edit(userRequest);

        // Asserts
        firmResult.Should().NotBeNull();
        firmResult.Should().BeEquivalentTo(userResponseExpected);
        mockUserRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockUserRepository.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Once);
    }

    [Test]
    public async Task Edit_WhenUserDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        User userResponseExpected = null!;
        var userRequest = UserMother.BasicUser();
        int id = userRequest.Id;

        mockUserRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(userResponseExpected);

        // Act
        Func<Task> action = async () => await userService.Edit(userRequest);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockUserRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockUserRepository.Verify(x => x.UpdateAsync(It.IsAny<User>()), Times.Never);
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsAllUsers()
    {
        // Arange
        var firmListReponseExpected = UserMother.GetUserList(CommonConst.MinCount);

        mockUserRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(firmListReponseExpected);

        // Act
        var resultList = await userService.GetAll() as List<User>;

        //Asserts
        resultList.Should().NotBeNull();
        resultList.Should().BeEquivalentTo(firmListReponseExpected);

        mockUserRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Test]
    public async Task GetById_WhenUserExists_ReturnsUser()
    {
        // Arrange
        var userResponseExpected = UserMother.AdminUser();
        int id = userResponseExpected.Id;

        mockUserRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(userResponseExpected);

        // Act
        var firmResult = await userService.GetById(id);

        // Asserts
        firmResult.Should().NotBeNull();
        firmResult.Should().BeEquivalentTo(userResponseExpected);
        mockUserRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetById_WhenUserDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 0;
        User userResponseExpected = null!;

        mockUserRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(userResponseExpected);

        // Act
        Func<Task> action = async () => await userService.GetById(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockUserRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetByQueryRequestAsync_WhenCalled_ReturnsFilteredUsers()
    {
        var queryRequest = QueryRequestMother.DefaultQueryRequest();
        var firmListReponseExpected = UserMother.GetUserList(CommonConst.MinCount);
        var paginationDataExpected = PaginationDataMother.DefaultPaginationData();
        var queryResultExpected = QueryResultMother<User>.Create(firmListReponseExpected, paginationDataExpected);

        mockUserRepository.Setup(x => x.GetByQueryRequestAsync(queryRequest)).ReturnsAsync(queryResultExpected);

        // Act
        var queryResult = await userService.GetByQueryRequestAsync(queryRequest);

        //Asserts
        queryResult.Should().NotBeNull();
        queryResult.Should().BeEquivalentTo(queryResultExpected);

        mockUserRepository.Verify(x => x.GetByQueryRequestAsync(It.IsAny<QueryRequest>()), Times.Once);
    }
}