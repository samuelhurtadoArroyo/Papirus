using AutoMapper;
using Papirus.Tests.Common.Mapping;
using Papirus.WebApi.Application.Interfaces;

namespace Papirus.WebApi.Application.Services.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class CaseServiceTests
{
    private CaseService _caseService = null!;

    private Mock<ICaseRepository> _mockCaseRepository = null!;

    private Mock<ICurrentUserService> _mockCurrentUserService = null!;

    private IMapper _mapper = null!;

    [SetUp]
    public void Setup()
    {
        _mockCaseRepository = new Mock<ICaseRepository>();
        _mockCurrentUserService = new Mock<ICurrentUserService>();
        _mapper = MapperCreator.CreateMapper();
        _caseService = new CaseService(_mockCaseRepository.Object, _mapper, _mockCurrentUserService.Object);
    }

    [Test]
    public async Task Create_WhenCaseIsValid_ReturnsCreatedCase()
    {
        // Arrange
        var caseResponseExpected = CaseMother.DemandCase();
        var caseRequest = CaseMother.DemandCase();
        caseResponseExpected.Id = 0;

        _mockCaseRepository.Setup(x => x.AddAsync(caseRequest)).ReturnsAsync(caseResponseExpected);

        // Act
        var caseResult = await _caseService.Create(caseRequest);

        // Asserts
        caseResult.Should().NotBeNull();
        caseResult.Should().BeEquivalentTo(caseResponseExpected);

        _mockCaseRepository.Verify(x => x.AddAsync(It.IsAny<Case>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenCaseExists_DeletesCaseSuccessfully()
    {
        // Arrange
        var caseRequest = CaseMother.DemandCase();
        var caseResponseExpected = CaseMother.DemandCase();
        int id = caseRequest.Id;

        _mockCaseRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(caseResponseExpected);
        _mockCaseRepository.Setup(x => x.RemoveAsync(caseRequest)).Verifiable();

        // Act
        await _caseService.Delete(id);

        // Asserts
        _mockCaseRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockCaseRepository.Verify(x => x.RemoveAsync(It.IsAny<Case>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenCaseDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 0;
        Case caseResponseExpected = null!;

        _mockCaseRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(caseResponseExpected);

        // Act
        Func<Task> action = async () => await _caseService.Delete(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        _mockCaseRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockCaseRepository.Verify(x => x.RemoveAsync(It.IsAny<Case>()), Times.Never);
    }

    [Test]
    public async Task Edit_WhenCaseIsValid_UpdatesCaseSuccessfully()
    {
        // Arrange
        var caseResponseExpected = CaseMother.DemandCase();
        var caseRequest = caseResponseExpected;
        var caseResponse = CaseMother.DemandCase();
        int id = caseResponse.Id;

        _mockCaseRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(caseResponse);
        _mockCaseRepository.Setup(x => x.UpdateAsync(caseRequest)).ReturnsAsync(caseResponseExpected);

        // Act
        var firmResult = await _caseService.Edit(caseRequest);

        // Asserts
        firmResult.Should().NotBeNull();
        firmResult.Should().BeEquivalentTo(caseResponseExpected);
        _mockCaseRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockCaseRepository.Verify(x => x.UpdateAsync(It.IsAny<Case>()), Times.Once);
    }

    [Test]
    public async Task Edit_WhenCaseDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        Case caseResponseExpected = null!;
        var caseRequest = CaseMother.DemandCase();
        int id = caseRequest.Id;

        _mockCaseRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(caseResponseExpected);

        // Act
        Func<Task> action = async () => await _caseService.Edit(caseRequest);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        _mockCaseRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockCaseRepository.Verify(x => x.UpdateAsync(It.IsAny<Case>()), Times.Never);
    }

    [Test]
    public async Task GetAll_WhenCalled_ReturnsAllCases()
    {
        // Arange
        var caseListReponseExpected = CaseMother.GetCaseList(CommonConst.MinCount);

        _mockCaseRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(caseListReponseExpected);

        // Act
        var resultList = await _caseService.GetAll() as List<Case>;

        //Asserts
        resultList.Should().NotBeNull();
        resultList.Should().BeEquivalentTo(caseListReponseExpected);

        _mockCaseRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Test]
    public async Task GetById_WhenCaseExists_ReturnsCase()
    {
        // Arrange
        var caseResponseExpected = CaseMother.DemandCase();
        int id = caseResponseExpected.Id;

        _mockCaseRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(caseResponseExpected);

        // Act
        var caseResult = await _caseService.GetById(id);

        // Asserts
        caseResult.Should().NotBeNull();
        caseResult.Should().BeEquivalentTo(caseResponseExpected);
        _mockCaseRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetById_WhenCaseDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int id = 0;
        Case caseResponseExpected = null!;

        _mockCaseRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(caseResponseExpected);

        // Act
        Func<Task> action = async () => await _caseService.GetById(id);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        _mockCaseRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetByQueryRequestAsync_WhenCalled_ReturnsFilteredCases()
    {
        var queryRequest = QueryRequestMother.DefaultQueryRequest();
        var caseListReponseExpected = CaseMother.GetCaseList(CommonConst.MinCount);
        var paginationDataExpected = PaginationDataMother.DefaultPaginationData();
        var queryResultExpected = QueryResultMother<Case>.Create(caseListReponseExpected, paginationDataExpected);

        _mockCaseRepository.Setup(x => x.GetByQueryRequestAsync(queryRequest)).ReturnsAsync(queryResultExpected);

        // Act
        var queryResult = await _caseService.GetByQueryRequestAsync(queryRequest);

        //Asserts
        queryResult.Should().NotBeNull();
        queryResult.Should().BeEquivalentTo(queryResultExpected);

        _mockCaseRepository.Verify(x => x.GetByQueryRequestAsync(It.IsAny<QueryRequest>()), Times.Once);
    }

    [Test]
    public async Task UpdateBusinessLine_WhenCaseIsValid_UpdatesCaseSuccessfully()
    {
        // Arrange
        const int businessLineId = 2;
        var caseResponseExpected = CaseMother.DemandCase();
        caseResponseExpected.BusinessLineId = businessLineId;
        var caseResponse = CaseMother.DemandCase();
        int id = caseResponse.Id;

        _mockCaseRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(caseResponse);
        _mockCaseRepository.Setup(x => x.UpdateAsync(caseResponse)).ReturnsAsync(caseResponseExpected);

        // Act
        var firmResult = await _caseService.UpdateBusinessLineAsync(caseResponse.Id, businessLineId);

        // Asserts
        firmResult.Should().NotBeNull();
        firmResult.Should().BeEquivalentTo(caseResponseExpected);
        _mockCaseRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockCaseRepository.Verify(x => x.UpdateAsync(It.IsAny<Case>()), Times.Once);
    }

    [Test]
    public async Task UpdateBusinessLine_WhenCaseDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        const int businessLineId = 2;
        Case caseResponseExpected = null!;
        var caseRequest = CaseMother.DemandCase();
        int id = caseRequest.Id;

        _mockCaseRepository.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(caseResponseExpected);

        // Act
        Func<Task> action = async () => await _caseService.UpdateBusinessLineAsync(caseRequest.Id, businessLineId);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={id} Not Found");

        // Asserts
        _mockCaseRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockCaseRepository.Verify(x => x.UpdateAsync(It.IsAny<Case>()), Times.Never);
    }
}