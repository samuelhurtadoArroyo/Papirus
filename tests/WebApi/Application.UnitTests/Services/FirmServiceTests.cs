namespace Papirus.WebApi.Application.Services.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class FirmServiceTests
{
    private FirmService firmService = null!;

    private Mock<IFirmRepository> mockFirmRepository = null!;

    [SetUp]
    public void Setup()
    {
        mockFirmRepository = new Mock<IFirmRepository>();
        firmService = new FirmService(mockFirmRepository.Object);
    }

    [Test]
    public async Task Create_WhenFirmIsValid_ReturnsCreatedFirm()
    {
        // Arrange
        var firmResponseExpected = FirmMother.DefaultFirm();
        var firmRequest = FirmMother.DefaultFirm();
        firmRequest.Id = 0;

        mockFirmRepository.Setup(x => x.AddAsync(firmRequest)).ReturnsAsync(firmResponseExpected);

        // Act
        var firmResult = await firmService.Create(firmRequest);

        // Asserts
        firmResult.Should().NotBeNull();
        firmResult.Should().BeEquivalentTo(firmResponseExpected);

        mockFirmRepository.Verify(x => x.AddAsync(It.IsAny<Firm>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenFirmExists_DeletesFirmSuccessfully()
    {
        // Arrange
        var firmRequest = FirmMother.DefaultFirm();
        var firmResponseExpected = FirmMother.DefaultFirm();
        int id = firmRequest.Id;

        mockFirmRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(firmResponseExpected);
        mockFirmRepository.Setup(x => x.RemoveAsync(firmRequest)).Verifiable();

        // Act
        await firmService.Delete(id);

        // Asserts
        mockFirmRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockFirmRepository.Verify(x => x.RemoveAsync(It.IsAny<Firm>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenCaseDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 0;
        Firm firmResponseExpected = null!;

        mockFirmRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(firmResponseExpected);

        // Act
        Func<Task> action = async () => await firmService.Delete(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockFirmRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockFirmRepository.Verify(x => x.RemoveAsync(It.IsAny<Firm>()), Times.Never);
    }

    [Test]
    public async Task Edit_WhenCaseIsValid_UpdatesCaseSuccessfully()
    {
        // Arrange
        const int id = 2;
        var firmResponseExpected = FirmMother.Create(id, "Firm name", Guid.Parse("7f1719b5-0b39-4d77-abe3-f02fdd955bdc"));
        var firmRequest = firmResponseExpected;
        var firmResponse = FirmMother.DefaultFirm(); // 1-Firm name

        mockFirmRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(firmResponse);
        mockFirmRepository.Setup(x => x.UpdateAsync(firmRequest)).ReturnsAsync(firmResponseExpected);

        // Act
        var firmResult = await firmService.Edit(firmRequest);

        // Asserts
        firmResult.Should().NotBeNull();
        firmResult.Should().BeEquivalentTo(firmResponseExpected);
        mockFirmRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockFirmRepository.Verify(x => x.UpdateAsync(It.IsAny<Firm>()), Times.Once);
    }

    [Test]
    public async Task Edit_WhenCaseDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 100;
        Firm firmResponseExpected = null!;
        var firmRequest = FirmMother.Create(id, "Not Found Firm", Guid.Parse("e4f5b935-7b77-4d43-86a3-d9be2e68a737"));

        mockFirmRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(firmResponseExpected);

        // Act
        Func<Task> action = async () => await firmService.Edit(firmRequest);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockFirmRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        mockFirmRepository.Verify(x => x.UpdateAsync(It.IsAny<Firm>()), Times.Never);
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsAllCases()
    {
        // Arange
        var firmListReponseExpected = FirmMother.GetFirmList(CommonConst.MinCount);

        mockFirmRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(firmListReponseExpected);

        // Act
        var resultList = await firmService.GetAll() as List<Firm>;

        //Asserts
        resultList.Should().NotBeNull();
        resultList.Should().BeEquivalentTo(firmListReponseExpected);

        mockFirmRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Test]
    public async Task GetById_WhenCaseExists_ReturnsCase()
    {
        // Arrange
        const int id = 1;
        var firmResponseExpected = FirmMother.DefaultFirm();

        mockFirmRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(firmResponseExpected);

        // Act
        var firmResult = await firmService.GetById(id);

        // Asserts
        firmResult.Should().NotBeNull();
        firmResult.Should().BeEquivalentTo(firmResponseExpected);
        mockFirmRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetById_WhenCaseDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 100;
        Firm firmResponseExpected = null!;

        mockFirmRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(firmResponseExpected);

        // Act
        Func<Task> action = async () => await firmService.GetById(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        mockFirmRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetByQueryRequestAsync_WhenCalled_ReturnsFilteredCases()
    {
        var queryRequest = QueryRequestMother.DefaultQueryRequest();
        var firmListReponseExpected = FirmMother.GetFirmList(CommonConst.MinCount);
        var paginationDataExpected = PaginationDataMother.DefaultPaginationData();
        var queryResultExpected = QueryResultMother<Firm>.Create(firmListReponseExpected, paginationDataExpected);

        mockFirmRepository.Setup(x => x.GetByQueryRequestAsync(queryRequest)).ReturnsAsync(queryResultExpected);

        // Act
        var queryResult = await firmService.GetByQueryRequestAsync(queryRequest);

        //Asserts
        queryResult.Should().NotBeNull();
        queryResult.Should().BeEquivalentTo(queryResultExpected);

        mockFirmRepository.Verify(x => x.GetByQueryRequestAsync(It.IsAny<QueryRequest>()), Times.Once);
    }
}