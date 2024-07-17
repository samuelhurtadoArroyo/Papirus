using Papirus.WebApi.Application.Interfaces;
using SharpCompress;
using System.Linq.Expressions;

namespace Papirus.WebApi.Application.Services.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class PermissionServiceTests
{
    private PermissionService permissionService = null!;

    private Mock<IPermissionRepository> mockPermissionRepository = null!;

    private Mock<ICurrentUserService> mockCurrentServiceService = null!;

    [SetUp]
    public void Setup()
    {
        mockPermissionRepository = new Mock<IPermissionRepository>();
        mockCurrentServiceService = new Mock<ICurrentUserService>();
        permissionService = new PermissionService(mockPermissionRepository.Object, mockCurrentServiceService.Object);
    }

    [Test]
    public async Task Create_WhenPermissionIsValid_ReturnsCreatedPermission()
    {
        // Arrange
        var permissionResponseExpected = PermissionMother.GetDemandasPermission();
        var permissionRequest = PermissionMother.GetDemandasPermission();
        permissionRequest.Id = 0;

        mockPermissionRepository.Setup(x => x.AddAsync(permissionRequest)).ReturnsAsync(permissionResponseExpected);

        // Act
        var permissionResult = await permissionService.Create(permissionRequest);

        // Asserts
        permissionResult.Should().NotBeNull();
        permissionResult.Should().BeEquivalentTo(permissionResponseExpected);

        mockPermissionRepository.Verify(x => x.AddAsync(It.IsAny<Permission>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenPermissionExists_DeletesPermissionSuccessfully()
    {
        // Arrange
        var permissionRequest = PermissionMother.GetDemandasPermission();
        var permissionResponseExpected = PermissionMother.GetDemandasPermission();
        int id = permissionRequest.Id;

        mockPermissionRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(permissionResponseExpected);
        mockPermissionRepository.Setup(x => x.RemoveAsync(permissionRequest)).Verifiable();

        // Act
        await permissionService.Delete(id);

        // Asserts
        mockPermissionRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockPermissionRepository.Verify(x => x.RemoveAsync(It.IsAny<Permission>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenPermissionDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 0;
        Permission permissionResponseExpected = null!;

        mockPermissionRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(permissionResponseExpected);

        // Act
        Func<Task> action = async () => await permissionService.Delete(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockPermissionRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockPermissionRepository.Verify(x => x.RemoveAsync(It.IsAny<Permission>()), Times.Never);
    }

    [Test]
    public async Task Edit_WhenPersonIsValid_UpdatesPersonSuccessfully()
    {
        // Arrange
        const int id = 2;
        var permissionResponseExpected = PermissionMother.Create(id, "Permission name", "Permission description", "labelcode");
        var permissionRequest = permissionResponseExpected;
        var permissionResponse = PermissionMother.GetDemandasPermission(); // 1-Permission name

        mockPermissionRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(permissionResponse);
        mockPermissionRepository.Setup(x => x.UpdateAsync(permissionRequest)).ReturnsAsync(permissionResponseExpected);

        // Act
        var permissionResult = await permissionService.Edit(permissionRequest);

        // Asserts
        permissionResult.Should().NotBeNull();
        permissionResult.Should().BeEquivalentTo(permissionResponseExpected);
        mockPermissionRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockPermissionRepository.Verify(x => x.UpdateAsync(It.IsAny<Permission>()), Times.Once);
    }

    [Test]
    public async Task Edit_WhenPermissionDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 100;
        Permission permissionResponseExpected = null!;
        var permissionRequest = PermissionMother.Create(id, "Not Found Permission", "Not Found Permission Description", "labelcode");

        mockPermissionRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(permissionResponseExpected);

        // Act
        Func<Task> action = async () => await permissionService.Edit(permissionRequest);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockPermissionRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockPermissionRepository.Verify(x => x.UpdateAsync(It.IsAny<Permission>()), Times.Never);
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsAllPermissions()
    {
        // Arange
        var permissionListReponseExpected = PermissionMother.GetPermissionList(CommonConst.MinCount);

        mockPermissionRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(permissionListReponseExpected);

        // Act
        var resultList = await permissionService.GetAll() as List<Permission>;

        //Asserts
        resultList.Should().NotBeNull();
        resultList.Should().BeEquivalentTo(permissionListReponseExpected);

        mockPermissionRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Test]
    public async Task GetById_WhenPermissionExists_ReturnsPermission()
    {
        // Arrange
        const int id = 1;
        var permissionResponseExpected = PermissionMother.GetDemandasPermission();

        mockPermissionRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(permissionResponseExpected);

        // Act
        var permissionResult = await permissionService.GetById(id);

        // Asserts
        permissionResult.Should().NotBeNull();
        permissionResult.Should().BeEquivalentTo(permissionResponseExpected);
        mockPermissionRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetById_WhenPermissionDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 100;
        Permission permissionResponseExpected = null!;

        mockPermissionRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(permissionResponseExpected);

        // Act
        Func<Task> action = async () => await permissionService.GetById(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockPermissionRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetByQueryRequestAsync_WhenCalled_ReturnsFilteredPermissions()
    {
        var queryRequest = QueryRequestMother.DefaultQueryRequest();
        var permissionListReponseExpected = PermissionMother.GetPermissionList(CommonConst.MinCount);
        var paginationDataExpected = PaginationDataMother.DefaultPaginationData();
        var queryResultExpected = QueryResultMother<Permission>.Create(permissionListReponseExpected, paginationDataExpected);

        mockPermissionRepository.Setup(x => x.GetByQueryRequestAsync(queryRequest)).ReturnsAsync(queryResultExpected);

        // Act
        var queryResult = await permissionService.GetByQueryRequestAsync(queryRequest);

        //Asserts
        queryResult.Should().NotBeNull();
        queryResult.Should().BeEquivalentTo(queryResultExpected);

        mockPermissionRepository.Verify(x => x.GetByQueryRequestAsync(It.IsAny<QueryRequest>()), Times.Once);
    }

    [Test]
    public async Task GetAllPermissionsByUserAsync_WhenCalled_ReturnsAllPermissions()
    {
        // Arange
        const int idReponseExpected = 1;
        var permissionListReponseExpected = PermissionMother.GetPermissionList(CommonConst.MinCount);

        mockCurrentServiceService.Setup(x => x.GetCurrentUserIdAsync()).ReturnsAsync(idReponseExpected);
        mockPermissionRepository.Setup(x => x.GetAllByUser(idReponseExpected)).ReturnsAsync(permissionListReponseExpected);

        // Act
        var resultList = await permissionService.GetAllPermissionsByUserAsync() as List<Permission>;

        //Asserts
        resultList.Should().NotBeNull();
        resultList.Should().BeEquivalentTo(permissionListReponseExpected);

        mockCurrentServiceService.Verify(x => x.GetCurrentUserIdAsync(), Times.Once);
        mockPermissionRepository.Verify(x => x.GetAllByUser(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task HasPermission_WhenValidId_ReturnsTrue()
    {
        // Arange
        const int userId = 1;
        var permissionListReponseExpected = PermissionMother.GetPermissionList(CommonConst.MinCount);

        mockPermissionRepository.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Permission, bool>>>())).ReturnsAsync(permissionListReponseExpected);

        // Act
        var resultList = await permissionService.HasPermission(userId, "Tutelas");

        //Asserts
        resultList.Should().NotNull();
        resultList.Should().Be(true);

        mockPermissionRepository.Verify(x => x.FindAsync(It.IsAny<Expression<Func<Permission, bool>>>()), Times.Once);
    }
}