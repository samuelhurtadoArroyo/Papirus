namespace Papirus.WebApi.Application.Services.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class ActorServiceTests
{
    private ActorService actorService = null!;

    private Mock<IActorRepository> mockActorRepository = null!;

    [SetUp]
    public void Setup()
    {
        mockActorRepository = new Mock<IActorRepository>();
        actorService = new ActorService(mockActorRepository.Object);
    }

    [Test]
    public async Task Create_WhenActorIsValid_ReturnsCreatedActor()
    {
        // Arrange
        var actorResponseExpected = ActorMother.ClaimantActor();
        var actorRequest = ActorMother.ClaimantActor();
        actorResponseExpected.Id = 0;

        mockActorRepository.Setup(x => x.AddAsync(actorRequest)).ReturnsAsync(actorResponseExpected);

        // Act
        var userResult = await actorService.Create(actorRequest);

        // Asserts
        userResult.Should().NotBeNull();
        userResult.Should().BeEquivalentTo(actorResponseExpected);

        mockActorRepository.Verify(x => x.AddAsync(It.IsAny<Actor>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenActorExists_DeletesActorSuccessfully()
    {
        // Arrange
        var actorRequest = ActorMother.ClaimantActor();
        var actorResponseExpected = ActorMother.ClaimantActor();
        int id = actorRequest.Id;

        mockActorRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(actorResponseExpected);
        mockActorRepository.Setup(x => x.RemoveAsync(actorRequest)).Verifiable();

        // Act
        await actorService.Delete(id);

        // Asserts
        mockActorRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockActorRepository.Verify(x => x.RemoveAsync(It.IsAny<Actor>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenActorDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 0;
        Actor actorResponseExpected = null!;

        mockActorRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(actorResponseExpected);

        // Act
        Func<Task> action = async () => await actorService.Delete(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockActorRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockActorRepository.Verify(x => x.RemoveAsync(It.IsAny<Actor>()), Times.Never);
    }

    [Test]
    public async Task Edit_WhenActorIsValid_UpdatesActorSuccessfully()
    {
        // Arrange
        var actorResponseExpected = ActorMother.ClaimantActor();
        var actorRequest = actorResponseExpected;
        var actorResponse = ActorMother.ClaimantActor();
        int id = actorResponse.Id;

        mockActorRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(actorResponse);
        mockActorRepository.Setup(x => x.UpdateAsync(actorRequest)).ReturnsAsync(actorResponseExpected);

        // Act
        var actorResult = await actorService.Edit(actorRequest);

        // Asserts
        actorResult.Should().NotBeNull();
        actorResult.Should().BeEquivalentTo(actorResponseExpected);
        mockActorRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockActorRepository.Verify(x => x.UpdateAsync(It.IsAny<Actor>()), Times.Once);
    }

    [Test]
    public async Task Edit_WhenActorDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        Actor actorResponseExpected = null!;
        var actorRequest = ActorMother.ClaimantActor();
        int id = actorRequest.Id;

        mockActorRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(actorResponseExpected);

        // Act
        Func<Task> action = async () => await actorService.Edit(actorRequest);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockActorRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockActorRepository.Verify(x => x.UpdateAsync(It.IsAny<Actor>()), Times.Never);
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsAllActors()
    {
        // Arange
        var actorListReponseExpected = ActorMother.GetActorList(CommonConst.MinCount);

        mockActorRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(actorListReponseExpected);

        // Act
        var resultList = await actorService.GetAll() as List<Actor>;

        //Asserts
        resultList.Should().NotBeNull();
        resultList.Should().BeEquivalentTo(actorListReponseExpected);

        mockActorRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Test]
    public async Task GetById_WhenActorExists_ReturnsActor()
    {
        // Arrange
        var actorResponseExpected = ActorMother.ClaimantActor();
        int id = actorResponseExpected.Id;

        mockActorRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(actorResponseExpected);

        // Act
        var actorResult = await actorService.GetById(id);

        // Asserts
        actorResult.Should().NotBeNull();
        actorResult.Should().BeEquivalentTo(actorResponseExpected);
        mockActorRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetById_WhenActorDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 0;
        Actor actorResponseExpected = null!;

        mockActorRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(actorResponseExpected);

        // Act
        Func<Task> action = async () => await actorService.GetById(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockActorRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetByQueryRequestAsync_WhenCalled_ReturnsFilteredActors()
    {
        var queryRequest = QueryRequestMother.DefaultQueryRequest();
        var actorListReponseExpected = ActorMother.GetActorList(CommonConst.MinCount);
        var paginationDataExpected = PaginationDataMother.DefaultPaginationData();
        var queryResultExpected = QueryResultMother<Actor>.Create(actorListReponseExpected, paginationDataExpected);

        mockActorRepository.Setup(x => x.GetByQueryRequestAsync(queryRequest)).ReturnsAsync(queryResultExpected);

        // Act
        var queryResult = await actorService.GetByQueryRequestAsync(queryRequest);

        //Asserts
        queryResult.Should().NotBeNull();
        queryResult.Should().BeEquivalentTo(queryResultExpected);

        mockActorRepository.Verify(x => x.GetByQueryRequestAsync(It.IsAny<QueryRequest>()), Times.Once);
    }
}