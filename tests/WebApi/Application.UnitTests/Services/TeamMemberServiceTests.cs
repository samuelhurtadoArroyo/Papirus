namespace Papirus.WebApi.Application.Services.Tests;

[ExcludeFromCodeCoverage]
public class TeamMemberServiceTests
{
    private TeamMemberService _teamMemberService = null!;

    private Mock<ITeamMemberRepository> _mockTeamMemberRepository = null!;

    [SetUp]
    public void Setup()
    {
        _mockTeamMemberRepository = new Mock<ITeamMemberRepository>();
        _teamMemberService = new TeamMemberService(_mockTeamMemberRepository.Object);
    }

    [Test]
    public async Task Create_WhenTeamMemberIsValid_ReturnsCreatedTeamMember()
    {
        // Arrange
        var teamMemberResponseExpected = TeamMemberMother.DefaultTeamMemberLeader();
        var teamMemberRequest = TeamMemberMother.DefaultTeamMemberLeader();
        teamMemberRequest.Id = 0;

        _mockTeamMemberRepository.Setup(x => x.AddAsync(teamMemberRequest)).ReturnsAsync(teamMemberResponseExpected);

        // Act
        var teamMemberResult = await _teamMemberService.Create(teamMemberRequest);

        // Asserts
        teamMemberResult.Should().NotBeNull();
        teamMemberResult.Should().BeEquivalentTo(teamMemberResponseExpected);

        _mockTeamMemberRepository.Verify(x => x.AddAsync(It.IsAny<TeamMember>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenTeamMemberExists_DeletesTeamMemberSuccessfully()
    {
        // Arrange
        var teamMemberRequest = TeamMemberMother.DefaultTeamMemberLeader();
        var teamMemberResponseExpected = TeamMemberMother.DefaultTeamMemberLeader();
        int id = teamMemberResponseExpected.Id;

        _mockTeamMemberRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(teamMemberResponseExpected);
        _mockTeamMemberRepository.Setup(x => x.RemoveAsync(teamMemberRequest)).Verifiable();

        // Act
        await _teamMemberService.Delete(id);

        // Asserts
        _mockTeamMemberRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockTeamMemberRepository.Verify(x => x.RemoveAsync(It.IsAny<TeamMember>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenTeamMemberDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 0;
        TeamMember teamMemberResponseExpected = null!;

        _mockTeamMemberRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(teamMemberResponseExpected);

        // Act
        Func<Task> action = async () => await _teamMemberService.Delete(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        _mockTeamMemberRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockTeamMemberRepository.Verify(x => x.RemoveAsync(It.IsAny<TeamMember>()), Times.Never);
    }

    [Test]
    public async Task Edit_WhenTeamMemberIsValid_UpdatesTeamMemberSuccessfully()
    {
        // Arrange
        var teamMemberResponseExpected = TeamMemberMother.DefaultTeamMemberLeader();
        var teamMemberRequest = teamMemberResponseExpected;
        var teamMemberResponse = TeamMemberMother.DefaultTeamMemberLeader();
        int id = teamMemberResponseExpected.Id;

        _mockTeamMemberRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(teamMemberResponse);
        _mockTeamMemberRepository.Setup(x => x.UpdateAsync(teamMemberRequest)).ReturnsAsync(teamMemberResponseExpected);

        // Act
        var teamMemberResult = await _teamMemberService.Edit(teamMemberRequest);

        // Asserts
        teamMemberResult.Should().NotBeNull();
        teamMemberResult.Should().BeEquivalentTo(teamMemberResponseExpected);
        _mockTeamMemberRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockTeamMemberRepository.Verify(x => x.UpdateAsync(It.IsAny<TeamMember>()), Times.Once);
    }

    [Test]
    public async Task Edit_WhenTeamMemberDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 100;
        TeamMember teamMemberResponseExpected = null!;
        var teamMemberRequest = TeamMemberMother.DefaultNotFoundTeamMember();

        _mockTeamMemberRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(teamMemberResponseExpected);

        // Act
        Func<Task> action = async () => await _teamMemberService.Edit(teamMemberRequest);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        _mockTeamMemberRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockTeamMemberRepository.Verify(x => x.UpdateAsync(It.IsAny<TeamMember>()), Times.Never);
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsAllTeamMembers()
    {
        // Arange
        var teamMemberListReponseExpected = TeamMemberMother.GetTeamMemberList();

        _mockTeamMemberRepository.Setup(x => x.GetAllIncludingAsync(tm => tm.Member)).ReturnsAsync(teamMemberListReponseExpected);

        // Act
        var resultList = await _teamMemberService.GetAll() as List<TeamMember>;

        //Asserts
        resultList.Should().NotBeNull();
        resultList.Should().BeEquivalentTo(teamMemberListReponseExpected);

        _mockTeamMemberRepository.Verify(x => x.GetAllIncludingAsync(tm => tm.Member), Times.Once);
    }

    [Test]
    public async Task GetById_WhenTeamMemberExists_ReturnsTeamMember()
    {
        // Arrange
        const int id = 1;
        var teamMemberResponseExpected = TeamMemberMother.DefaultTeamMemberLeader();

        _mockTeamMemberRepository.Setup(x => x.GetByIdIncludingAsync(id, tm => tm.Member)).ReturnsAsync(teamMemberResponseExpected);

        // Act
        var teamMemberResult = await _teamMemberService.GetById(id);

        // Asserts
        teamMemberResult.Should().NotBeNull();
        teamMemberResult.Should().BeEquivalentTo(teamMemberResponseExpected);
        _mockTeamMemberRepository.Verify(x => x.GetByIdIncludingAsync(id, tm => tm.Member), Times.Once);
    }

    [Test]
    public async Task GetById_WhenTeamMemberDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 100;
        TeamMember teamMemberResponseExpected = null!;

        _mockTeamMemberRepository.Setup(x => x.GetByIdIncludingAsync(id, tm => tm.Member)).ReturnsAsync(teamMemberResponseExpected);

        // Act
        Func<Task> action = async () => await _teamMemberService.GetById(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        _mockTeamMemberRepository.Verify(x => x.GetByIdIncludingAsync(id, tm => tm.Member), Times.Once);
    }

    [Test]
    public async Task GetByQueryRequestAsync_WhenCalled_ReturnsFilteredTeamMembers()
    {
        var queryRequest = QueryRequestMother.DefaultQueryRequest();
        var teamMemberListReponseExpected = TeamMemberMother.GetTeamMemberList();
        var paginationDataExpected = PaginationDataMother.DefaultPaginationData();
        var queryResultExpected = QueryResultMother<TeamMember>.Create(teamMemberListReponseExpected, paginationDataExpected);

        _mockTeamMemberRepository.Setup(x => x.GetByQueryRequestAsync(queryRequest)).ReturnsAsync(queryResultExpected);

        // Act
        var queryResult = await _teamMemberService.GetByQueryRequestAsync(queryRequest);

        //Asserts
        queryResult.Should().NotBeNull();
        queryResult.Should().BeEquivalentTo(queryResultExpected);

        _mockTeamMemberRepository.Verify(x => x.GetByQueryRequestAsync(It.IsAny<QueryRequest>()), Times.Once);
    }

    [Test]
    public async Task GetByTeamId_WhenCalled_ReturnsTeamMembers()
    {
        // Arrange
        var teamId = 1;
        var teamMembersResponseExpected = TeamMemberMother.GetTeamMemberList();

        _mockTeamMemberRepository.Setup(x => x.GetByTeamIdAsync(teamId)).ReturnsAsync(teamMembersResponseExpected);

        // Act
        var teamMembersResult = await _teamMemberService.GetByTeamId(teamId);

        // Asserts
        teamMembersResult.Should().NotBeNull();
        teamMembersResult.Should().BeEquivalentTo(teamMembersResponseExpected);
        _mockTeamMemberRepository.Verify(x => x.GetByTeamIdAsync(It.IsAny<int>()), Times.Once);
    }
}