using Papirus.WebApi.Domain.Define.Enums;
using System.Linq.Dynamic.Core;

namespace Papirus.WebApi.Infrastructure.Repositories.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class ActorRepositoryTests
{
    private List<Actor> actorList = null!;

    private ActorRepository actorRepository = null!;

    private Mock<AppDbContext> mockAppDbContext = null!;

    private static EquivalencyAssertionOptions<Actor> ExcludeProperties(EquivalencyAssertionOptions<Actor> options)
    {
        return options;
    }

    [SetUp]
    public void SetUp()
    {
        actorList = ActorMother.GetActorList();
        mockAppDbContext = new Mock<AppDbContext>();
        mockAppDbContext.Setup(x => x.Set<Actor>()).ReturnsDbSet(actorList);
        mockAppDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        actorRepository = new ActorRepository(mockAppDbContext.Object);
    }

    [Test]
    public async Task AddAsync_WhenCalled_AddsActorSuccessfully()
    {
        // Arrange
        Actor actorResponseExpected = ActorMother.ClaimantActor();
        var actorRequest = actorResponseExpected;

        // Act
        var userResult = await actorRepository.AddAsync(actorRequest);

        // Assert
        userResult.Should().NotBeNull();
        userResult.Should().BeEquivalentTo(actorResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Actor>(), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task FindAsync_WhenCalled_ReturnsMatchingActors()
    {
        // Arrange
        var actorListResponseExpected = ActorMother.GetActorList().Where(x => x.Id > 1);

        // Act
        var actorListResult = await actorRepository.FindAsync(x => x.Id > 1);

        // Asserts
        actorListResult.Should().NotBeNull();
        actorListResult.Should().BeEquivalentTo(actorListResponseExpected, ExcludeProperties);

        mockAppDbContext.Verify(x => x.Set<Actor>(), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_WhenCalled_ReturnsAllActors()
    {
        // Arrange
        var actorListResponseExpected = ActorMother.GetActorList();

        // Act
        var actorListResult = await actorRepository.GetAllAsync();

        // Asserts
        actorListResult.Should().NotBeNull();
        actorListResult.Should().BeEquivalentTo(actorListResponseExpected, ExcludeProperties);

        mockAppDbContext.Verify(x => x.Set<Actor>(), Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_WhenCalled_ReturnsExpectedActor()
    {
        // Arrange
        var actorResponseExpected = ActorMother.ClaimantActor();
        int id = actorResponseExpected.Id;

        // Act
        var actorResult = await actorRepository.GetByIdAsync(id);

        // Assert
        actorResult.Should().NotBeNull();
        actorResult.Should().BeEquivalentTo(actorResponseExpected, ExcludeProperties);

        mockAppDbContext.Verify(x => x.Set<Actor>(), Times.Once);
    }

    [Test]
    public async Task RemoveAsync_WhenCalled_RemovesActorSuccessfully()
    {
        // Arrange
        var actorResponseExpected = ActorMother.ClaimantActor();
        var actorRequest = actorResponseExpected;

        // Act
        await actorRepository.RemoveAsync(actorRequest);

        // Assert
        mockAppDbContext.Verify(x => x.Set<Actor>().Remove(It.IsAny<Actor>()), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_WhenCalled_UpdatesActorSuccessfully()
    {
        // Arrange
        var actorResponseExpected = ActorMother.ClaimantActor();
        var actorRequest = actorResponseExpected;

        // Act
        var userResult = await actorRepository.UpdateAsync(actorRequest);

        // Assert
        userResult.Should().NotBeNull();
        userResult.Should().BeEquivalentTo(actorResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Actor>().Update(It.IsAny<Actor>()), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Ignore("Due date")]
    [TestCase(1, 10, null, "ActorTypeId", FilterOptions.IsEqualTo, "1", SortOrders.Asc)]
    public async Task GetByQueryRequestAsync_WhenCalledWithParameters_ReturnsExpectedActors(
        int? pageNumber, int? pageSize, string? searchString, string? columnName, FilterOptions? filterOptions, string? filterValue, SortOrders? sortOrders)
    {
        // Arrange
        var actorListResponseExpected = ActorMother.GetActorList(CommonConst.MaxCount);
        var filterParams = FilterParamsMother.GetFilterParams(columnName!, filterOptions ?? FilterOptions.IsNotEmpty, filterValue!);
        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, sortOrders ?? SortOrders.Asc);
        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, searchString, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<Actor>.Create(actorListResponseExpected, queryRequest);

        mockAppDbContext.Setup(x => x.Set<Actor>()).ReturnsDbSet(actorListResponseExpected);

        // Act
        var queryResult = await actorRepository.GetByQueryRequestAsync(queryRequest);

        // Asserts
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().NotBeNull();
        queryResult.PaginationData.Should().NotBeNull();
        queryResult.Items.Should().BeEquivalentTo(queryResultExpected.Items);
        queryResult.PaginationData.Should().BeEquivalentTo(queryResultExpected.PaginationData);

        mockAppDbContext.Verify(x => x.Set<Actor>(), Times.Once);
    }
}