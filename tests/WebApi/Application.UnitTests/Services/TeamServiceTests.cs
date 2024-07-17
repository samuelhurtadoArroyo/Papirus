namespace Papirus.WebApi.Application.Services.Tests;

[ExcludeFromCodeCoverage]
public class TeamServiceTests
{
    private TeamService teamService = null!;

    private Mock<ITeamRepository> mockTeamRepository = null!;

    [SetUp]
    public void Setup()
    {
        mockTeamRepository = new Mock<ITeamRepository>();
        teamService = new TeamService(mockTeamRepository.Object);
    }

    [Test]
    public async Task Create_WhenTeamIsValid_ReturnsCreatedTeam()
    {
        // Arrange
        var teamResponseExpected = TeamMother.DefaultCautelasTeam();
        var teamRequest = TeamMother.DefaultCautelasTeam();
        teamRequest.Id = 0;

        mockTeamRepository.Setup(x => x.AddAsync(teamRequest)).ReturnsAsync(teamResponseExpected);

        // Act
        var teamResult = await teamService.Create(teamRequest);

        // Asserts
        teamResult.Should().NotBeNull();
        teamResult.Should().BeEquivalentTo(teamResponseExpected);

        mockTeamRepository.Verify(x => x.AddAsync(It.IsAny<Team>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenTeamExists_DeletesTeamSuccessfully()
    {
        // Arrange
        var teamRequest = TeamMother.DefaultCautelasTeam();
        var teamResponseExpected = TeamMother.DefaultCautelasTeam();
        int id = teamResponseExpected.Id;

        mockTeamRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(teamResponseExpected);
        mockTeamRepository.Setup(x => x.RemoveAsync(teamRequest)).Verifiable();

        // Act
        await teamService.Delete(id);

        // Asserts
        mockTeamRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockTeamRepository.Verify(x => x.RemoveAsync(It.IsAny<Team>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenTeamDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 0;
        Team teamResponseExpected = null!;

        mockTeamRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(teamResponseExpected);

        // Act
        Func<Task> action = async () => await teamService.Delete(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockTeamRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockTeamRepository.Verify(x => x.RemoveAsync(It.IsAny<Team>()), Times.Never);
    }

    [Test]
    public async Task Edit_WhenTeamIsValid_UpdatesTeamSuccessfully()
    {
        // Arrange
        var teamResponseExpected = TeamMother.DefaultCautelasTeam();
        var teamRequest = teamResponseExpected;
        var teamResponse = TeamMother.DefaultCautelasTeam();
        int id = teamResponseExpected.Id;

        mockTeamRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(teamResponse);
        mockTeamRepository.Setup(x => x.UpdateAsync(teamRequest)).ReturnsAsync(teamResponseExpected);

        // Act
        var teamResult = await teamService.Edit(teamRequest);

        // Asserts
        teamResult.Should().NotBeNull();
        teamResult.Should().BeEquivalentTo(teamResponseExpected);
        mockTeamRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockTeamRepository.Verify(x => x.UpdateAsync(It.IsAny<Team>()), Times.Once);
    }

    [Test]
    public async Task Edit_WhenTeamDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 100;
        Team teamResponseExpected = null!;
        var teamRequest = TeamMother.DefaultNotFoundTeam();

        mockTeamRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(teamResponseExpected);

        // Act
        Func<Task> action = async () => await teamService.Edit(teamRequest);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockTeamRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockTeamRepository.Verify(x => x.UpdateAsync(It.IsAny<Team>()), Times.Never);
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsAllTeams()
    {
        // Arange
        var teamListReponseExpected = TeamMother.GetTeamList();

        mockTeamRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(teamListReponseExpected);

        // Act
        var resultList = await teamService.GetAll() as List<Team>;

        //Asserts
        resultList.Should().NotBeNull();
        resultList.Should().BeEquivalentTo(teamListReponseExpected);

        mockTeamRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Test]
    public async Task GetById_WhenTeamExists_ReturnsTeam()
    {
        // Arrange
        const int id = 1;
        var teamResponseExpected = TeamMother.DefaultCautelasTeam();

        mockTeamRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(teamResponseExpected);

        // Act
        var teamResult = await teamService.GetById(id);

        // Asserts
        teamResult.Should().NotBeNull();
        teamResult.Should().BeEquivalentTo(teamResponseExpected);
        mockTeamRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetById_WhenTeamDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 100;
        Team teamResponseExpected = null!;

        mockTeamRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(teamResponseExpected);

        // Act
        Func<Task> action = async () => await teamService.GetById(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockTeamRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetByQueryRequestAsync_WhenCalled_ReturnsFilteredTeams()
    {
        var queryRequest = QueryRequestMother.DefaultQueryRequest();
        var teamListReponseExpected = TeamMother.GetTeamList();
        var paginationDataExpected = PaginationDataMother.DefaultPaginationData();
        var queryResultExpected = QueryResultMother<Team>.Create(teamListReponseExpected, paginationDataExpected);

        mockTeamRepository.Setup(x => x.GetByQueryRequestAsync(queryRequest)).ReturnsAsync(queryResultExpected);

        // Act
        var queryResult = await teamService.GetByQueryRequestAsync(queryRequest);

        //Asserts
        queryResult.Should().NotBeNull();
        queryResult.Should().BeEquivalentTo(queryResultExpected);

        mockTeamRepository.Verify(x => x.GetByQueryRequestAsync(It.IsAny<QueryRequest>()), Times.Once);
    }
}