using Papirus.WebApi.Domain.Define.Enums;
using Papirus.WebApi.Domain.Dtos;

namespace Papirus.WebApi.Infrastructure.Repositories.Tests;

[ExcludeFromCodeCoverage]
public class TeamRepositoryTests
{
    private List<Team> teamList = null!;

    private TeamRepository teamRepository = null!;

    private Mock<AppDbContext> mockAppDbContext = null!;

    [SetUp]
    public void SetUp()
    {
        teamList = TeamMother.GetTeamList();
        mockAppDbContext = new Mock<AppDbContext>();
        mockAppDbContext.Setup(x => x.Set<Team>()).ReturnsDbSet(teamList);
        mockAppDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        teamRepository = new TeamRepository(mockAppDbContext.Object);
    }

    [Test]
    public async Task AddAsync_WhenCalled_AddsTeamSuccessfully()
    {
        // Arrange
        Team teamResponseExpected = TeamMother.DefaultCautelasTeam();
        var teamRequest = teamResponseExpected;

        // Act
        var teamResult = await teamRepository.AddAsync(teamRequest);

        // Assert
        teamResult.Should().NotBeNull();
        teamResult.Should().BeEquivalentTo(teamResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Team>(), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task FindAsync_WhenCalled_ReturnsMatchingTeams()
    {
        // Arrange
        var teamListResponseExpected = TeamMother.GetTeamList().Where(x => x.Id > 1);

        // Act
        var teamListResult = await teamRepository.FindAsync(x => x.Id > 1);

        // Asserts
        teamListResult.Should().NotBeNull();
        teamListResult.Should().BeEquivalentTo(teamListResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Team>(), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_WhenCalled_ReturnsAllTeams()
    {
        // Arrange
        var teamListResponseExpected = TeamMother.GetTeamList();

        // Act
        var teamListResult = await teamRepository.GetAllAsync();

        // Asserts
        teamListResult.Should().NotBeNull();
        teamListResult.Should().BeEquivalentTo(teamListResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Team>(), Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_WhenCalled_ReturnsExpectedTeam()
    {
        // Arrange
        var teamResponseExpected = TeamMother.DefaultCautelasTeam();
        int id = teamResponseExpected.Id;

        // Act
        var teamResult = await teamRepository.GetByIdAsync(id);

        // Assert
        teamResult.Should().NotBeNull();
        teamResult.Should().BeEquivalentTo(teamResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Team>(), Times.Once);
    }

    [Test]
    public async Task RemoveAsync_WhenCalled_RemovesTeamSuccessfully()
    {
        // Arrange
        var teamResponseExpected = TeamMother.DefaultCautelasTeam();
        var teamRequest = teamResponseExpected;

        // Act
        await teamRepository.RemoveAsync(teamRequest);

        // Assert
        mockAppDbContext.Verify(x => x.Set<Team>().Remove(It.IsAny<Team>()), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_WhenCalled_UpdatesTeamSuccessfully()
    {
        // Arrange
        var teamResponseExpected = TeamMother.DefaultCautelasTeam();
        var teamRequest = teamResponseExpected;

        // Act
        var teamResult = await teamRepository.UpdateAsync(teamRequest);

        // Assert
        teamResult.Should().NotBeNull();
        teamResult.Should().BeEquivalentTo(teamResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Team>().Update(It.IsAny<Team>()), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Ignore("Due date")]
    [TestCase(null, null, null, null, null, null, null)]
    [TestCase(1, null, null, null, null, null, null)]
    [TestCase(null, 10, null, null, null, null, null)]
    [TestCase(1, 10, "TeamName1", null, null, null, null)]
    [TestCase(1, 10, null, "Name", FilterOptions.StartsWith, "Team", null)]
    [TestCase(1, 10, null, "Name", FilterOptions.EndsWith, "1", null)]
    [TestCase(1, 10, null, "Name", FilterOptions.Contains, "Team", null)]
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
    public async Task GetByQueryRequestAsync_WhenCalledWithParameters_ReturnsExpectedTeams(int? pageNumber, int? pageSize, string? searchString, string? columnName, FilterOptions? filterOptions, string? filterValue, SortOrders? sortOrders)
    {
        // Arrange
        var teamListResponseExpected = TeamMother.GetTeamList(CommonConst.MaxCount);
        var filterParams = FilterParamsMother.GetFilterParams(columnName!, filterOptions ?? FilterOptions.IsNotEmpty, filterValue!);
        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, sortOrders ?? SortOrders.Asc);
        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, searchString, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<Team>.Create(teamListResponseExpected, queryRequest);

        mockAppDbContext.Setup(x => x.Set<Team>()).ReturnsDbSet(teamListResponseExpected);

        // Act
        var queryResult = await teamRepository.GetByQueryRequestAsync(queryRequest);

        // Asserts
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().NotBeNull();
        queryResult.PaginationData.Should().NotBeNull();
        queryResult.Items.Should().BeEquivalentTo(queryResultExpected.Items);
        queryResult.PaginationData.Should().BeEquivalentTo(queryResultExpected.PaginationData);

        mockAppDbContext.Verify(x => x.Set<Team>(), Times.Once);
    }

    [Ignore("Due date")]
    [Test]
    public async Task GetByQueryRequestAsync_WhenCalledWithMultipleFilterParameters_ReturnsExpectedTeams()
    {
        // Arrange
        const int pageNumber = 1;
        const int pageSize = 10;
        const string columnName = "Name";
        var teamListResponseExpected = TeamMother.GetTeamList(CommonConst.MaxCount);

        List<FilterParams> filterParams =
        [
            FilterParamsMother.Create(columnName!, FilterOptions.IsNotEmpty, ""),
            FilterParamsMother.Create(columnName!, FilterOptions.Contains, "Team"),
        ];

        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, SortOrders.Asc);

        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, null, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<Team>.Create(teamListResponseExpected, queryRequest);

        mockAppDbContext.Setup(x => x.Set<Team>()).ReturnsDbSet(teamListResponseExpected);

        // Act
        var queryResult = await teamRepository.GetByQueryRequestAsync(queryRequest);

        // Asserts
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().NotBeNull();
        queryResult.PaginationData.Should().NotBeNull();
        queryResult.Items.Should().BeEquivalentTo(queryResultExpected.Items);
        queryResult.PaginationData.Should().BeEquivalentTo(queryResultExpected.PaginationData);

        mockAppDbContext.Verify(x => x.Set<Team>(), Times.Once);
    }

    [Ignore("Due date")]
    [TestCase("Id", SortOrders.Asc, "Name", SortOrders.Asc)]
    [TestCase("Id", SortOrders.Asc, "Name", SortOrders.Desc)]
    [TestCase("Id", SortOrders.Desc, "Name", SortOrders.Asc)]
    [TestCase("Id", SortOrders.Desc, "Name", SortOrders.Desc)]
    public async Task GetByQueryRequestAsync_WhenCalledWithMultipleSortingParameters_ReturnsExpectedTeams(string column1, SortOrders sortOrders1, string column2, SortOrders sortOrders2)
    {
        // Arrange
        const int pageNumber = 1;
        const int pageSize = 10;
        const string columnName = "Name";
        const string filterValue = "Team";

        var teamListResponseExpected = TeamMother.GetTeamList(CommonConst.MaxCount);

        var filterParams = FilterParamsMother.GetFilterParams(columnName!, FilterOptions.Contains, filterValue!);

        List<SortingParams> sortingParams = [
            SortingParamsMother.Create(column1, sortOrders1),
            SortingParamsMother.Create(column2, sortOrders2)
        ];

        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, null, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<Team>.Create(teamListResponseExpected, queryRequest);

        mockAppDbContext.Setup(x => x.Set<Team>()).ReturnsDbSet(teamListResponseExpected);

        // Act
        var queryResult = await teamRepository.GetByQueryRequestAsync(queryRequest);

        // Asserts
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().NotBeNull();
        queryResult.PaginationData.Should().NotBeNull();
        queryResult.Items.Should().BeEquivalentTo(queryResultExpected.Items);
        queryResult.PaginationData.Should().BeEquivalentTo(queryResultExpected.PaginationData);

        mockAppDbContext.Verify(x => x.Set<Team>(), Times.Once);
    }
}