using Papirus.WebApi.Domain.Define.Enums;

namespace Papirus.WebApi.Infrastructure.Repositories.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class PersonRepositoryTests
{
    private List<Person> personList = null!;

    private PersonRepository personRepository = null!;

    private Mock<AppDbContext> mockAppDbContext = null!;

    private static EquivalencyAssertionOptions<Person> ExcludeProperties(EquivalencyAssertionOptions<Person> options)
    {
        return options;
    }

    [SetUp]
    public void SetUp()
    {
        personList = PersonMother.GetPersonList();
        mockAppDbContext = new Mock<AppDbContext>();
        mockAppDbContext.Setup(x => x.Set<Person>()).ReturnsDbSet(personList);
        mockAppDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        personRepository = new PersonRepository(mockAppDbContext.Object);
    }

    [Test]
    public async Task AddAsync_WhenCalled_AddsPersonSuccessfully()
    {
        // Arrange
        Person personResponseExpected = PersonMother.NaturalCCPerson();
        var personRequest = personResponseExpected;

        // Act
        var userResult = await personRepository.AddAsync(personRequest);

        // Assert
        userResult.Should().NotBeNull();
        userResult.Should().BeEquivalentTo(personResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Person>(), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task FindAsync_WhenCalled_ReturnsMatchingPersons()
    {
        // Arrange
        var personListResponseExpected = PersonMother.GetPersonList().Where(x => x.Id > 1);

        // Act
        var personListResult = await personRepository.FindAsync(x => x.Id > 1);

        // Asserts
        personListResult.Should().NotBeNull();
        personListResult.Should().BeEquivalentTo(personListResponseExpected, ExcludeProperties);

        mockAppDbContext.Verify(x => x.Set<Person>(), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_WhenCalled_ReturnsAllPersons()
    {
        // Arrange
        var personListResponseExpected = PersonMother.GetPersonList();

        // Act
        var personListResult = await personRepository.GetAllAsync();

        // Asserts
        personListResult.Should().NotBeNull();
        personListResult.Should().BeEquivalentTo(personListResponseExpected, ExcludeProperties);

        mockAppDbContext.Verify(x => x.Set<Person>(), Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_WhenCalled_ReturnsExpectedPerson()
    {
        // Arrange
        var personResponseExpected = PersonMother.NaturalCCPerson();
        int id = personResponseExpected.Id;

        // Act
        var personResult = await personRepository.GetByIdAsync(id);

        // Assert
        personResult.Should().NotBeNull();
        personResult.Should().BeEquivalentTo(personResponseExpected, ExcludeProperties);

        mockAppDbContext.Verify(x => x.Set<Person>(), Times.Once);
    }

    [Test]
    public async Task RemoveAsync_WhenCalled_RemovesPersonSuccessfully()
    {
        // Arrange
        var personRequest = PersonMother.NaturalCCPerson();

        // Act
        await personRepository.RemoveAsync(personRequest);

        // Assert
        mockAppDbContext.Verify(x => x.Set<Person>().Remove(It.IsAny<Person>()), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_WhenCalled_UpdatesPersonSuccessfully()
    {
        // Arrange
        var personResponseExpected = PersonMother.NaturalCCPerson();
        var personRequest = personResponseExpected;

        // Act
        var personResult = await personRepository.UpdateAsync(personRequest);

        // Assert
        personResult.Should().NotBeNull();
        personResult.Should().BeEquivalentTo(personResponseExpected);

        mockAppDbContext.Verify(x => x.Set<Person>().Update(It.IsAny<Person>()), Times.Once);
        mockAppDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Ignore("Due date")]
    [TestCase(null, null, null, null, null, null, null)]
    public async Task GetByQueryRequestAsync_WhenCalledWithParameters_ReturnsExpectedPersons(int? pageNumber, int? pageSize, string? searchString, string? columnName, FilterOptions? filterOptions, string? filterValue, SortOrders? sortOrders)
    {
        // Arrange
        var personListResponseExpected = PersonMother.GetPersonList(CommonConst.MaxCount);
        var filterParams = FilterParamsMother.GetFilterParams(columnName!, filterOptions ?? FilterOptions.IsNotEmpty, filterValue!);
        var sortingParams = SortingParamsMother.GetSortingParams(columnName!, sortOrders ?? SortOrders.Asc);
        var queryRequest = QueryRequestMother.Create(pageNumber, pageSize, searchString, filterParams, sortingParams);
        var queryResultExpected = QueryResultMother<Person>.Create(personListResponseExpected, queryRequest);

        mockAppDbContext.Setup(x => x.Set<Person>()).ReturnsDbSet(personListResponseExpected);

        // Act
        var queryResult = await personRepository.GetByQueryRequestAsync(queryRequest);

        // Asserts
        queryResult.Should().NotBeNull();
        queryResult.Items.Should().NotBeNull();
        queryResult.PaginationData.Should().NotBeNull();
        queryResult.Items.Should().BeEquivalentTo(queryResultExpected.Items);
        queryResult.PaginationData.Should().BeEquivalentTo(queryResultExpected.PaginationData);

        mockAppDbContext.Verify(x => x.Set<Person>(), Times.Once);
    }

    [Test]
    public async Task FindByIdentificationAsync_WhenCalled_ReturnsExpectedPerson()
    {
        // Arrange
        var personExpected = PersonMother.NaturalCCPerson();
        var identificationTypeId = (IdentificationTypeId)personExpected.IdentificationTypeId;
        var identificationNumber = personExpected.IdentificationNumber;
        mockAppDbContext.Setup(x => x.People).ReturnsDbSet(personList);

        // Act
        var personResult = await personRepository.FindByIdentificationAsync(identificationTypeId, identificationNumber);

        // Assert
        personResult.Should().NotBeNull();
        personResult.Should().BeEquivalentTo(personExpected, ExcludeProperties);

        mockAppDbContext.Verify(x => x.People, Times.Once);
    }
}