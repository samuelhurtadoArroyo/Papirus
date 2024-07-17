using MockQueryable.Moq;
using Papirus.WebApi.Domain.Define.Enums;
using Papirus.WebApi.Domain.Dtos;

namespace Papirus.WebApi.Infrastructure.Repositories.Tests;

[ExcludeFromCodeCoverage]
public class TeamMemberRepositoryTests
{
    private List<TeamMember> _teamMemberList = null!;

    private TeamMemberRepository _teamMemberRepository = null!;

    private Mock<AppDbContext> _mockAppDbContext = null!;

    private Mock<DbSet<TeamMember>> _mockTeamMemberDbSet = null!;

    [SetUp]
    public void SetUp()
    {
        _teamMemberList = TeamMemberMother.GetTeamMemberList();
        _mockTeamMemberDbSet = _teamMemberList.AsQueryable().BuildMockDbSet();

        _mockAppDbContext = new Mock<AppDbContext>();
        _mockAppDbContext.Setup(x => x.TeamMembers).Returns(_mockTeamMemberDbSet.Object);
        _mockAppDbContext.Setup(x => x.Set<TeamMember>()).Returns(_mockTeamMemberDbSet.Object);
        _mockAppDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        _teamMemberRepository = new TeamMemberRepository(_mockAppDbContext.Object);
    }

    [Test]
    public async Task AddAsync_WhenCalled_AddsTeamMemberSuccessfully()
    {
        // Arrange
        var teammemberResponseExpected = TeamMemberMother.DefaultTeamMemberLeader();
        var teammemberRequest = teammemberResponseExpected;

        // Act
        var teammemberResult = await _teamMemberRepository.AddAsync(teammemberRequest);

        // Assert
        teammemberResult.Should().NotBeNull();
        teammemberResult.Should().BeEquivalentTo(teammemberResponseExpected);

        _mockAppDbContext.Verify(x => x.Set<TeamMember>(), Times.Once);
        _mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task AddTeamMemberAsync_WhenCalled_AddsTeamMemberSuccessfully()
    {
        // Arrange
        var teammemberResponseExpected = TeamMemberMother.DefaultTeamMemberLeader();
        var teammemberRequest = teammemberResponseExpected;

        // Act
        await _teamMemberRepository.AddTeamMemberAsync(teammemberRequest);

        // Assert
        _mockTeamMemberDbSet.Verify(m => m.AddAsync(It.IsAny<TeamMember>(), default), Times.Once());
        _mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task FindAsync_WhenCalled_ReturnsMatchingTeamMembers()
    {
        // Arrange
        var teammemberListResponseExpected = TeamMemberMother.GetTeamMemberList().Where(x => x.Id > 1);

        // Act
        var teammemberListResult = await _teamMemberRepository.FindAsync(x => x.Id > 1);

        // Asserts
        teammemberListResult.Should().NotBeNull();
        teammemberListResult.Should().BeEquivalentTo(teammemberListResponseExpected);

        _mockAppDbContext.Verify(x => x.Set<TeamMember>(), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_WhenCalled_ReturnsAllTeamMembers()
    {
        // Arrange
        var teammemberListResponseExpected = TeamMemberMother.GetTeamMemberList();

        // Act
        var teammemberListResult = await _teamMemberRepository.GetAllAsync();

        // Asserts
        teammemberListResult.Should().NotBeNull();
        teammemberListResult.Should().BeEquivalentTo(teammemberListResponseExpected);

        _mockAppDbContext.Verify(x => x.Set<TeamMember>(), Times.Once);
    }

    [Test]
    public async Task GetAllIncludingAsync_WhenCalled_ReturnsAllTeamMembers()
    {
        // Arrange
        var teammemberListResponseExpected = TeamMemberMother.GetTeamMemberList();

        // Act
        var teammemberListResult = await _teamMemberRepository.GetAllIncludingAsync();

        // Asserts
        teammemberListResult.Should().NotBeNull();
        teammemberListResult.Should().BeEquivalentTo(teammemberListResponseExpected);

        _mockAppDbContext.Verify(x => x.Set<TeamMember>(), Times.Once);
    }

    [Test]
    public async Task GetAllTeamMembersAsync_WhenCalled_ReturnsAllTeamMembers()
    {
        // Arrange
        var teammemberListResponseExpected = TeamMemberMother.GetTeamMemberList();

        // Act
        var teammemberListResult = await _teamMemberRepository.GetAllTeamMembersAsync();

        // Asserts
        teammemberListResult.Should().NotBeNull();
        teammemberListResult.Should().BeEquivalentTo(teammemberListResponseExpected);

        _mockAppDbContext.Verify(x => x.Set<TeamMember>(), Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_WhenCalled_ReturnsExpectedTeamMember()
    {
        // Arrange
        var teammemberResponseExpected = TeamMemberMother.DefaultTeamMemberLeader();
        int id = teammemberResponseExpected.Id;

        _mockTeamMemberDbSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .ReturnsAsync((object[] ids) => _teamMemberList.Find(t => t.Id == (int)ids[0]));

        // Act
        var teammemberResult = await _teamMemberRepository.GetByIdAsync(id);

        // Assert
        teammemberResult.Should().NotBeNull();
        teammemberResult.Should().BeEquivalentTo(teammemberResponseExpected);

        _mockAppDbContext.Verify(x => x.Set<TeamMember>(), Times.Once);
    }

    [Test]
    public async Task GetByIdIncludingAsync_WhenCalled_ReturnsExpectedTeamMember()
    {
        // Arrange
        var teammemberResponseExpected = TeamMemberMother.DefaultTeamMemberLeader();
        int id = teammemberResponseExpected.Id;

        _mockTeamMemberDbSet.Setup(m => m.FindAsync(It.IsAny<object[]>()))
            .ReturnsAsync((object[] ids) => _teamMemberList.Find(t => t.Id == (int)ids[0]));

        // Act
        var teammemberResult = await _teamMemberRepository.GetByIdIncludingAsync(id, x => x.Team);

        // Assert
        teammemberResult.Should().NotBeNull();
        teammemberResult.Should().BeEquivalentTo(teammemberResponseExpected);

        _mockAppDbContext.Verify(x => x.Set<TeamMember>(), Times.Once);
        _mockAppDbContext.Verify(x => x.Set<TeamMember>().AsQueryable(), Times.Once);
    }

    [Test]
    public async Task RemoveAsync_WhenCalled_RemovesTeamMemberSuccessfully()
    {
        // Arrange
        var teammemberResponseExpected = TeamMemberMother.DefaultTeamMemberLeader();
        var teammemberRequest = teammemberResponseExpected;

        // Act
        await _teamMemberRepository.RemoveAsync(teammemberRequest);

        // Assert
        _mockAppDbContext.Verify(x => x.Set<TeamMember>().Remove(It.IsAny<TeamMember>()), Times.Once);
        _mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task DeleteTeamMemberAsync_WhenCalled_RemovesTeamMemberSuccessfully()
    {
        // Arrange
        var teammemberRequest = TeamMemberMother.DefaultTeamMemberLeader();

        // Act
        await _teamMemberRepository.DeleteTeamMemberAsync(teammemberRequest.Id);

        // Assert
        _mockAppDbContext.Verify(x => x.TeamMembers.Remove(It.IsAny<TeamMember>()), Times.Once);
        _mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_WhenCalled_UpdatesTeamMemberSuccessfully()
    {
        // Arrange
        var teammemberResponseExpected = TeamMemberMother.DefaultTeamMemberLeader();
        var teammemberRequest = teammemberResponseExpected;

        // Act
        var teammemberResult = await _teamMemberRepository.UpdateAsync(teammemberRequest);

        // Assert
        teammemberResult.Should().NotBeNull();
        teammemberResult.Should().BeEquivalentTo(teammemberResponseExpected);

        _mockAppDbContext.Verify(x => x.Set<TeamMember>().Update(It.IsAny<TeamMember>()), Times.Once);
        _mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateTeamMemberAsync_WhenCalled_UpdatesTeamMemberSuccessfully()
    {
        // Arrange
        var teammemberResponseExpected = TeamMemberMother.DefaultTeamMemberLeader();
        var teammemberRequest = teammemberResponseExpected;

        // Act
        await _teamMemberRepository.UpdateTeamMemberAsync(teammemberRequest);

        // Assert
        _mockTeamMemberDbSet.Verify(m => m.Update(It.IsAny<TeamMember>()), Times.Once());
        _mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Ignore("Due date")]
    [TestCase(null, null, null, null, null, null, null)]
    [TestCase(1, null, null, null, null, null, null)]
    [TestCase(null, 10, null, null, null, null, null)]
    [TestCase(1, 10, null, "Id", FilterOptions.IsGreaterThan, "10", null)]
    [TestCase(1, 10, null, "Id", FilterOptions.IsGreaterThanOrEqualTo, "10", null)]
    [TestCase(1, 10, null, "Id", FilterOptions.IsLessThan, "10", null)]
    [TestCase(1, 10, null, "Id", FilterOptions.IsLessThanOrEqualTo, "10", null)]
    [TestCase(1, 10, null, "Id", FilterOptions.IsEqualTo, "1", null)]
    [TestCase(1, 10, null, "Id", FilterOptions.IsNotEqualTo, "1", null)]
    public async Task GetByQueryRequestAsync_WhenCalledWithParameters_ReturnsExpectedTeamMembers(int? pageNumber, int? pageSize, string? searchString, string? columnName, FilterOptions? filterOptions, string? filterValue, SortOrders? sortOrders)
    {
        // Arrange
        var teammemberListResponseExpected = TeamMemberMother.GetTeamMemberList(CommonConst.MaxCount);
        var filterParams = FilterParamsMother.GetFilterParams(columnName!, filterOptions ?? FilterOptions.IsNotEmpty, filterValue!);
        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, sortOrders ?? SortOrders.Asc);
        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, searchString, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<TeamMember>.Create(teammemberListResponseExpected, queryRequest);

        _mockAppDbContext.Setup(x => x.Set<TeamMember>()).ReturnsDbSet(teammemberListResponseExpected);

        // Act
        var queryResult = await _teamMemberRepository.GetByQueryRequestAsync(queryRequest);

        // Asserts
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().NotBeNull();
        queryResult.PaginationData.Should().NotBeNull();
        queryResult.Items.Should().BeEquivalentTo(queryResultExpected.Items);
        queryResult.PaginationData.Should().BeEquivalentTo(queryResultExpected.PaginationData);

        _mockAppDbContext.Verify(x => x.Set<TeamMember>(), Times.Once);
    }

    [Ignore("Due date")]
    [Test]
    public async Task GetByQueryRequestAsync_WhenCalledWithMultipleFilterParameters_ReturnsExpectedTeamMembers()
    {
        // Arrange
        const int pageNumber = 1;
        const int pageSize = 10;
        const string columnName = "Id";
        var teammemberListResponseExpected = TeamMemberMother.GetTeamMemberList(CommonConst.MaxCount);

        var filterParams = new List<FilterParams>
        {
            FilterParamsMother.Create(columnName!, FilterOptions.IsNotEmpty, ""),
            FilterParamsMother.Create(columnName!, FilterOptions.Contains, "1"),
        };

        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, SortOrders.Asc);

        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, null, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<TeamMember>.Create(teammemberListResponseExpected, queryRequest);

        _mockAppDbContext.Setup(x => x.Set<TeamMember>()).ReturnsDbSet(teammemberListResponseExpected);

        // Act
        var queryResult = await _teamMemberRepository.GetByQueryRequestAsync(queryRequest);

        // Asserts
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().NotBeNull();
        queryResult.PaginationData.Should().NotBeNull();
        queryResult.Items.Should().BeEquivalentTo(queryResultExpected.Items);
        queryResult.PaginationData.Should().BeEquivalentTo(queryResultExpected.PaginationData);

        _mockAppDbContext.Verify(x => x.Set<TeamMember>(), Times.Once);
    }

    [Ignore("Due date")]
    [TestCase("Id", SortOrders.Asc, "Name", SortOrders.Asc)]
    [TestCase("Id", SortOrders.Asc, "Name", SortOrders.Desc)]
    [TestCase("Id", SortOrders.Desc, "Name", SortOrders.Asc)]
    [TestCase("Id", SortOrders.Desc, "Name", SortOrders.Desc)]
    public async Task GetByQueryRequestAsync_WhenCalledWithMultipleSortingParameters_ReturnsExpectedTeamMembers(string column1, SortOrders sortOrders1, string column2, SortOrders sortOrders2)
    {
        // Arrange
        const int pageNumber = 1;
        const int pageSize = 10;
        const string columnName = "Id";
        const string filterValue = "1";

        var teammemberListResponseExpected = TeamMemberMother.GetTeamMemberList(CommonConst.MaxCount);

        var filterParams = FilterParamsMother.GetFilterParams(columnName!, FilterOptions.Contains, filterValue!);

        var sortingParams = new List<SortingParams>
        {
            SortingParamsMother.Create(column1, sortOrders1),
            SortingParamsMother.Create(column2, sortOrders2)
        };

        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, null, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<TeamMember>.Create(teammemberListResponseExpected, queryRequest);

        _mockAppDbContext.Setup(x => x.Set<TeamMember>()).ReturnsDbSet(teammemberListResponseExpected);

        // Act
        var queryResult = await _teamMemberRepository.GetByQueryRequestAsync(queryRequest);

        // Asserts
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().NotBeNull();
        queryResult.PaginationData.Should().NotBeNull();
        queryResult.Items.Should().BeEquivalentTo(queryResultExpected.Items);
        queryResult.PaginationData.Should().BeEquivalentTo(queryResultExpected.PaginationData);

        _mockAppDbContext.Verify(x => x.Set<TeamMember>(), Times.Once);
    }

    [Test]
    public async Task GetByTeamIdAsync_WhenCalled_ReturnsExpectedTeamMembers()
    {
        // Arrange
        const int teamId = 1;
        var teamMembersResponseExpected = _teamMemberList.Where(tm => tm.TeamId == teamId).ToList();

        // Act
        var teamMembersResult = await _teamMemberRepository.GetByTeamIdAsync(teamId);

        // Assert
        teamMembersResult.Should().NotBeNull();
        teamMembersResult.Should().BeEquivalentTo(teamMembersResponseExpected);
        _mockAppDbContext.Verify(x => x.TeamMembers, Times.Once);
    }

    [Test]
    public async Task GetTeamMembersByTeamIdAsync_WhenCalled_ReturnsExpectedTeamMembers()
    {
        // Arrange
        const int teamId = 1;
        var teamMembersResponseExpected = _teamMemberList.Where(tm => tm.TeamId == teamId).ToList();

        // Act
        var teamMembersResult = await _teamMemberRepository.GetTeamMembersByTeamIdAsync(teamId);

        // Assert
        teamMembersResult.Should().NotBeNull();
        teamMembersResult.Should().BeEquivalentTo(teamMembersResponseExpected);
        _mockAppDbContext.Verify(x => x.TeamMembers, Times.Once);
    }
}