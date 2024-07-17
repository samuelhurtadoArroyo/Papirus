namespace Papirus.WebApi.Application.Services.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class PersonServiceTests
{
    private PersonService personService = null!;

    private Mock<IPersonRepository> mockPersonRepository = null!;

    [SetUp]
    public void Setup()
    {
        mockPersonRepository = new Mock<IPersonRepository>();
        personService = new PersonService(mockPersonRepository.Object);
    }

    [Test]
    public async Task Create_WhenPersonIsValid_ReturnsCreatedPerson()
    {
        // Arrange
        var personResponseExpected = PersonMother.NaturalCCPerson();
        var personRequest = PersonMother.NaturalCCPerson();
        personResponseExpected.Id = 0;

        mockPersonRepository.Setup(x => x.AddAsync(personRequest)).ReturnsAsync(personResponseExpected);

        // Act
        var personResult = await personService.Create(personRequest);

        // Asserts
        personResult.Should().NotBeNull();
        personResult.Should().BeEquivalentTo(personResponseExpected);

        mockPersonRepository.Verify(x => x.AddAsync(It.IsAny<Person>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenPersonExists_DeletesPersonSuccessfully()
    {
        // Arrange
        var personRequest = PersonMother.NaturalCCPerson();
        var personResponseExpected = PersonMother.NaturalCCPerson();
        int id = personRequest.Id;

        mockPersonRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(personResponseExpected);
        mockPersonRepository.Setup(x => x.RemoveAsync(personRequest)).Verifiable();

        // Act
        await personService.Delete(id);

        // Asserts
        mockPersonRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockPersonRepository.Verify(x => x.RemoveAsync(It.IsAny<Person>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenPersonDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 0;
        Person personResponseExpected = null!;

        mockPersonRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(personResponseExpected);

        // Act
        Func<Task> action = async () => await personService.Delete(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockPersonRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockPersonRepository.Verify(x => x.RemoveAsync(It.IsAny<Person>()), Times.Never);
    }

    [Test]
    public async Task Edit_WhenPersonIsValid_UpdatesPersonSuccessfully()
    {
        // Arrange
        var personResponseExpected = PersonMother.NaturalCCPerson();
        var personRequest = personResponseExpected;
        var personResponse = PersonMother.NaturalCCPerson();
        int id = personResponse.Id;

        mockPersonRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(personResponse);
        mockPersonRepository.Setup(x => x.UpdateAsync(personRequest)).ReturnsAsync(personResponseExpected);

        // Act
        var firmResult = await personService.Edit(personRequest);

        // Asserts
        firmResult.Should().NotBeNull();
        firmResult.Should().BeEquivalentTo(personResponseExpected);
        mockPersonRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockPersonRepository.Verify(x => x.UpdateAsync(It.IsAny<Person>()), Times.Once);
    }

    [Test]
    public async Task Edit_WhenPersonDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        Person personResponseExpected = null!;
        var personRequest = PersonMother.NaturalCCPerson();
        int id = personRequest.Id;

        mockPersonRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(personResponseExpected);

        // Act
        Func<Task> action = async () => await personService.Edit(personRequest);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockPersonRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockPersonRepository.Verify(x => x.UpdateAsync(It.IsAny<Person>()), Times.Never);
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsAllPersons()
    {
        // Arange
        var personListReponseExpected = PersonMother.GetPersonList(CommonConst.MinCount);

        mockPersonRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(personListReponseExpected);

        // Act
        var resultList = await personService.GetAll() as List<Person>;

        //Asserts
        resultList.Should().NotBeNull();
        resultList.Should().BeEquivalentTo(personListReponseExpected);

        mockPersonRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Test]
    public async Task GetById_WhenPersonExists_ReturnsPerson()
    {
        // Arrange
        var personResponseExpected = PersonMother.NaturalCCPerson();
        int id = personResponseExpected.Id;

        mockPersonRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(personResponseExpected);

        // Act
        var personResult = await personService.GetById(id);

        // Asserts
        personResult.Should().NotBeNull();
        personResult.Should().BeEquivalentTo(personResponseExpected);
        mockPersonRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetById_WhenPersonDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 0;
        Person personResponseExpected = null!;

        mockPersonRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(personResponseExpected);

        // Act
        Func<Task> action = async () => await personService.GetById(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockPersonRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetByQueryRequestAsync_WhenCalled_ReturnsFilteredPersons()
    {
        var queryRequest = QueryRequestMother.DefaultQueryRequest();
        var personListReponseExpected = PersonMother.GetPersonList(CommonConst.MinCount);
        var paginationDataExpected = PaginationDataMother.DefaultPaginationData();
        var queryResultExpected = QueryResultMother<Person>.Create(personListReponseExpected, paginationDataExpected);

        mockPersonRepository.Setup(x => x.GetByQueryRequestAsync(queryRequest)).ReturnsAsync(queryResultExpected);

        // Act
        var queryResult = await personService.GetByQueryRequestAsync(queryRequest);

        //Asserts
        queryResult.Should().NotBeNull();
        queryResult.Should().BeEquivalentTo(queryResultExpected);

        mockPersonRepository.Verify(x => x.GetByQueryRequestAsync(It.IsAny<QueryRequest>()), Times.Once);
    }
}