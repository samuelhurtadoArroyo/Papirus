namespace Papirus.WebApi.Application.Services.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class ProcessDocumentTypesServiceTests
{
    private Mock<IProcessDocumentTypeRepository> _mockRepository = null!;

    private ProcessDocumentTypesService _processDocumentTypesService = null!;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IProcessDocumentTypeRepository>();
        _processDocumentTypesService = new ProcessDocumentTypesService(_mockRepository.Object);
    }

    [Test]
    public async Task Create_WhenModelIsValid_ReturnsProcessDocumentType()
    {
        // Arrange
        ProcessDocumentType processDocumentType = new()
        {
            ProcessId = 1,
            DocumentTypeId = 1,
            Mandatory = true,
            DocOrder = 0,
            ProcessTemplateId = 1,
        };

        _mockRepository.Setup(x => x.AddAsync(processDocumentType)).ReturnsAsync(processDocumentType);

        // Act
        var result = await _processDocumentTypesService.Create(processDocumentType);

        // Asserts
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(processDocumentType);
        result.Id.Should().Be(It.IsAny<int>());

        _mockRepository.Verify(x => x.AddAsync(It.IsAny<ProcessDocumentType>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenIdIsValid_ReturnsDeletesSuccessfully()
    {
        // Arrange
        const int processDocumentTypeId = 1;
        ProcessDocumentType processDocumentType = new()
        {
            Id = processDocumentTypeId,
            ProcessId = 1,
            DocumentTypeId = 1,
            Mandatory = true,
            DocOrder = 0,
            ProcessTemplateId = 1,
        };

        _mockRepository.Setup(x => x.GetByIdAsync(processDocumentTypeId)).ReturnsAsync(processDocumentType);
        _mockRepository.Setup(x => x.RemoveAsync(processDocumentType)).Verifiable();

        // Act
        await _processDocumentTypesService.Delete(processDocumentTypeId);

        // Asserts
        _mockRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockRepository.Verify(x => x.RemoveAsync(It.IsAny<ProcessDocumentType>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenIdIsNotValid_ReturnsNotFoundException()
    {
        // Arrange
        const int processDocumentTypeId = 0;
        ProcessDocumentType processDocumentType = null!;

        _mockRepository.Setup(x => x.GetByIdAsync(processDocumentTypeId)).ReturnsAsync(processDocumentType);

        // Act
        Func<Task> action = async () => await _processDocumentTypesService.Delete(processDocumentTypeId);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={processDocumentTypeId} Not Found");

        // Asserts
        _mockRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockRepository.Verify(x => x.RemoveAsync(It.IsAny<ProcessDocumentType>()), Times.Never);
    }

    [Test]
    public async Task Edit_WhenModelIsValid_ReturnsProcessDocumentType()
    {
        // Arrange
        const int processDocumentTypeId = 1;
        ProcessDocumentType processDocumentType = new()
        {
            Id = processDocumentTypeId,
            ProcessId = 1,
            DocumentTypeId = 1,
            Mandatory = true,
            DocOrder = 0,
            ProcessTemplateId = 1,
        };

        _mockRepository.Setup(x => x.GetByIdAsync(processDocumentTypeId)).ReturnsAsync(processDocumentType);
        _mockRepository.Setup(x => x.UpdateAsync(processDocumentType)).ReturnsAsync(processDocumentType);

        // Act
        var result = await _processDocumentTypesService.Edit(processDocumentType);

        // Asserts
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(processDocumentType);
        result.Id.Should().Be(processDocumentTypeId);

        _mockRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockRepository.Verify(x => x.UpdateAsync(It.IsAny<ProcessDocumentType>()), Times.Once);
    }

    [Test]
    public async Task Edit_WhenModelIdIsNotValid_ReturnsNotFoundException()
    {
        // Arrange
        const int processDocumentTypeId = 0;
        ProcessDocumentType processDocumentTypeExpected = null!;
        ProcessDocumentType processDocumentType = new()
        {
            Id = processDocumentTypeId,
            ProcessId = 1,
            DocumentTypeId = 1,
            Mandatory = true,
            DocOrder = 0,
            ProcessTemplateId = 1,
        };

        _mockRepository.Setup(x => x.GetByIdAsync(processDocumentTypeId)).ReturnsAsync(processDocumentTypeExpected);

        // Act
        Func<Task> action = async () => await _processDocumentTypesService.Edit(processDocumentType);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={processDocumentType.Id} Not Found");

        // Asserts
        _mockRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockRepository.Verify(x => x.UpdateAsync(It.IsAny<ProcessDocumentType>()), Times.Never);
    }

    [Test]
    public async Task GetAll_ReturnsListOfProcessDocumentType()
    {
        // Arrange
        const int processDocumentTypeId = 1;

        List<ProcessDocumentType> list = [new()
        {
            Id = processDocumentTypeId,
            ProcessId = 1,
            DocumentTypeId = 1,
            Mandatory = true,
            DocOrder = 0,
            ProcessTemplateId = 1,
        }];

        _mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(list);

        // Act
        var result = await _processDocumentTypesService.GetAll() as List<ProcessDocumentType>;

        // Asserts
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(list);
        result?.Count.Should().Be(list.Count);

        _mockRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Test]
    public async Task GetById_WhenIdIsValid_ReturnsProcessDocumentType()
    {
        // Arrange
        const int processDocumentTypeId = 1;
        ProcessDocumentType processDocumentType = new()
        {
            Id = processDocumentTypeId,
            ProcessId = 1,
            DocumentTypeId = 1,
            Mandatory = true,
            DocOrder = 0,
            ProcessTemplateId = 1,
        };

        _mockRepository.Setup(x => x.GetByIdAsync(processDocumentTypeId)).ReturnsAsync(processDocumentType);

        // Act
        var result = await _processDocumentTypesService.GetById(processDocumentTypeId);

        // Asserts
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(processDocumentType);
        result.Id.Should().Be(processDocumentTypeId);

        _mockRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Test]
    public async Task GetById_WhenIdIsNotValid_ReturnsNotFoundException()
    {
        // Arrange
        const int processDocumentTypeId = 0;
        ProcessDocumentType processDocumentType = null!;

        _mockRepository.Setup(x => x.GetByIdAsync(processDocumentTypeId)).ReturnsAsync(processDocumentType);

        // Act
        Func<Task> action = async () => await _processDocumentTypesService.GetById(processDocumentTypeId);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={processDocumentTypeId} Not Found");

        // Asserts
        _mockRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }
}