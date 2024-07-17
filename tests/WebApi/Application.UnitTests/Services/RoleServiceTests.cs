namespace Papirus.WebApi.Application.Services.Tests;

[ExcludeFromCodeCoverage]
public class RoleServiceTests
{
    private RoleService roleService = null!;

    private Mock<IRoleRepository> mockRoleRepository = null!;

    [SetUp]
    public void Setup()
    {
        mockRoleRepository = new Mock<IRoleRepository>();
        roleService = new RoleService(mockRoleRepository.Object);
    }

    [Test]
    public async Task Create_WhenRoleIsValid_ReturnsCreatedRole()
    {
        // Arrange
        var roleResponseExpected = RoleMother.DefaultAdministratorRole();
        var roleRequest = RoleMother.DefaultAdministratorRole();
        roleRequest.Id = 0;

        mockRoleRepository.Setup(x => x.AddAsync(roleRequest)).ReturnsAsync(roleResponseExpected);

        // Act
        var roleResult = await roleService.Create(roleRequest);

        // Asserts
        roleResult.Should().NotBeNull();
        roleResult.Should().BeEquivalentTo(roleResponseExpected);

        mockRoleRepository.Verify(x => x.AddAsync(It.IsAny<Role>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenRoleExists_DeletesRoleSuccessfully()
    {
        // Arrange
        var roleRequest = RoleMother.DefaultAdministratorRole();
        var roleResponseExpected = RoleMother.DefaultAdministratorRole();
        int id = roleResponseExpected.Id;

        mockRoleRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(roleResponseExpected);
        mockRoleRepository.Setup(x => x.RemoveAsync(roleRequest)).Verifiable();

        // Act
        await roleService.Delete(id);

        // Asserts
        mockRoleRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockRoleRepository.Verify(x => x.RemoveAsync(It.IsAny<Role>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenRoleDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 0;
        Role roleResponseExpected = null!;

        mockRoleRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(roleResponseExpected);

        // Act
        Func<Task> action = async () => await roleService.Delete(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockRoleRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockRoleRepository.Verify(x => x.RemoveAsync(It.IsAny<Role>()), Times.Never);
    }

    [Test]
    public async Task Edit_WhenRoleIsValid_UpdatesRoleSuccessfully()
    {
        // Arrange
        var roleResponseExpected = RoleMother.DefaultBasicRole();
        var roleRequest = roleResponseExpected;
        var roleResponse = RoleMother.DefaultBasicRole();
        int id = roleResponseExpected.Id;

        mockRoleRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(roleResponse);
        mockRoleRepository.Setup(x => x.UpdateAsync(roleRequest)).ReturnsAsync(roleResponseExpected);

        // Act
        var roleResult = await roleService.Edit(roleRequest);

        // Asserts
        roleResult.Should().NotBeNull();
        roleResult.Should().BeEquivalentTo(roleResponseExpected);
        mockRoleRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockRoleRepository.Verify(x => x.UpdateAsync(It.IsAny<Role>()), Times.Once);
    }

    [Test]
    public async Task Edit_WhenRoleDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 100;
        Role roleResponseExpected = null!;
        var roleRequest = RoleMother.DefaultNotFoundRole();

        mockRoleRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(roleResponseExpected);

        // Act
        Func<Task> action = async () => await roleService.Edit(roleRequest);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockRoleRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockRoleRepository.Verify(x => x.UpdateAsync(It.IsAny<Role>()), Times.Never);
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsAllRoles()
    {
        // Arange
        var roleListReponseExpected = RoleMother.GetRoleList();

        mockRoleRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(roleListReponseExpected);

        // Act
        var resultList = await roleService.GetAll() as List<Role>;

        //Asserts
        resultList.Should().NotBeNull();
        resultList.Should().BeEquivalentTo(roleListReponseExpected);

        mockRoleRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Test]
    public async Task GetById_WhenRoleExists_ReturnsRole()
    {
        // Arrange
        const int id = 1;
        var roleResponseExpected = RoleMother.DefaultAdministratorRole();

        mockRoleRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(roleResponseExpected);

        // Act
        var roleResult = await roleService.GetById(id);

        // Asserts
        roleResult.Should().NotBeNull();
        roleResult.Should().BeEquivalentTo(roleResponseExpected);
        mockRoleRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetById_WhenRoleDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 100;
        Role roleResponseExpected = null!;

        mockRoleRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(roleResponseExpected);

        // Act
        Func<Task> action = async () => await roleService.GetById(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockRoleRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetByQueryRequestAsync_WhenCalled_ReturnsFilteredRoles()
    {
        var queryRequest = QueryRequestMother.DefaultQueryRequest();
        var roleListReponseExpected = RoleMother.GetRoleList();
        var paginationDataExpected = PaginationDataMother.DefaultPaginationData();
        var queryResultExpected = QueryResultMother<Role>.Create(roleListReponseExpected, paginationDataExpected);

        mockRoleRepository.Setup(x => x.GetByQueryRequestAsync(queryRequest)).ReturnsAsync(queryResultExpected);

        // Act
        var queryResult = await roleService.GetByQueryRequestAsync(queryRequest);

        //Asserts
        queryResult.Should().NotBeNull();
        queryResult.Should().BeEquivalentTo(queryResultExpected);

        mockRoleRepository.Verify(x => x.GetByQueryRequestAsync(It.IsAny<QueryRequest>()), Times.Once);
    }
}