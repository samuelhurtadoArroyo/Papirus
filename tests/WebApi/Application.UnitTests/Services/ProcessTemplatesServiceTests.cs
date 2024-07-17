namespace Papirus.WebApi.Application.Services.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class ProcessTemplatesServiceTests
{
    private Mock<IProcessTemplatesRepository> _mockRepository = null!;

    private ProcessTemplatesService _processTemplatesService = null!;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IProcessTemplatesRepository>();
        _processTemplatesService = new ProcessTemplatesService(_mockRepository.Object);
    }

    [Test]
    public async Task Create_WhenModelIsValid_ReturnsProcessTemplate()
    {
        // Arrange
        ProcessTemplate processTemplate = new()
        {
            FirmId = 1,
            ProcessTypeId = 1,
            ProcessId = 1,
            SubProcessId = 1,
            FileName = "",
            FilePath = ""
        };

        _mockRepository.Setup(x => x.AddAsync(processTemplate)).ReturnsAsync(processTemplate);

        // Act
        var result = await _processTemplatesService.Create(processTemplate);

        // Asserts
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(processTemplate);
        result.Id.Should().Be(It.IsAny<int>());

        _mockRepository.Verify(x => x.AddAsync(It.IsAny<ProcessTemplate>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenIdIsValid_ReturnsDeletesSuccessfully()
    {
        // Arrange
        const int processTemplateId = 1;
        ProcessTemplate processTemplate = new()
        {
            FirmId = 1,
            ProcessTypeId = 1,
            ProcessId = 1,
            SubProcessId = 1,
            FileName = "",
            FilePath = ""
        };

        _mockRepository.Setup(x => x.GetByIdAsync(processTemplateId)).ReturnsAsync(processTemplate);
        _mockRepository.Setup(x => x.RemoveAsync(processTemplate)).Verifiable();

        // Act
        await _processTemplatesService.Delete(processTemplateId);

        // Asserts
        _mockRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockRepository.Verify(x => x.RemoveAsync(It.IsAny<ProcessTemplate>()), Times.Once);
    }

    [Test]
    public async Task Delete_WhenIdIsNotValid_ReturnsNotFoundException()
    {
        // Arrange
        const int processTemplateId = 0;
        ProcessTemplate processTemplate = null!;

        _mockRepository.Setup(x => x.GetByIdAsync(processTemplateId)).ReturnsAsync(processTemplate);

        // Act
        Func<Task> action = async () => await _processTemplatesService.Delete(processTemplateId);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={processTemplateId} Not Found");

        // Asserts
        _mockRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockRepository.Verify(x => x.RemoveAsync(It.IsAny<ProcessTemplate>()), Times.Never);
    }

    [Test]
    public async Task Edit_WhenModelIsValid_ReturnsProcessTemplate()
    {
        // Arrange
        const int processTemplateId = 1;
        ProcessTemplate processTemplate = new()
        {
            Id = processTemplateId,
            FirmId = 1,
            ProcessTypeId = 1,
            ProcessId = 1,
            SubProcessId = 1,
            FileName = "",
            FilePath = ""
        };

        _mockRepository.Setup(x => x.GetByIdAsync(processTemplateId)).ReturnsAsync(processTemplate);
        _mockRepository.Setup(x => x.UpdateAsync(processTemplate)).ReturnsAsync(processTemplate);

        // Act
        var result = await _processTemplatesService.Edit(processTemplate);

        // Asserts
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(processTemplate);
        result.Id.Should().Be(processTemplateId);

        _mockRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockRepository.Verify(x => x.UpdateAsync(It.IsAny<ProcessTemplate>()), Times.Once);
    }

    [Test]
    public async Task Edit_WhenModelIdIsNotValid_ReturnsNotFoundException()
    {
        // Arrange
        const int processDocumentTypeId = 0;
        ProcessTemplate processDocumentTypeExpected = null!;
        ProcessTemplate processDocumentType = new()
        {
            Id = processDocumentTypeId,
            ProcessId = 1
        };

        _mockRepository.Setup(x => x.GetByIdAsync(processDocumentTypeId)).ReturnsAsync(processDocumentTypeExpected);

        // Act
        Func<Task> action = async () => await _processTemplatesService.Edit(processDocumentType);
        await action.Should().ThrowAsync<NotFoundException>().WithMessage($"The Id={processDocumentType.Id} Not Found");

        // Asserts
        _mockRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
        _mockRepository.Verify(x => x.UpdateAsync(It.IsAny<ProcessTemplate>()), Times.Never);
    }

    [Test]
    public async Task GetAll_ReturnsListOfProcessTemplate()
    {
        // Arrange
        const int processTemplateId = 1;

        List<ProcessTemplate> list = [new()
        {
            Id = processTemplateId,
            FirmId = 1,
            ProcessTypeId = 1,
            ProcessId = 1,
            SubProcessId = 1,
            FileName = "",
            FilePath = ""
        }];

        _mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(list);

        // Act
        var result = await _processTemplatesService.GetAll() as List<ProcessTemplate>;

        // Asserts
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(list);
        result?.Count.Should().Be(list.Count);

        _mockRepository.Verify(x => x.GetAllAsync(), Times.Once);
    }

    [Test]
    public async Task GetById_WhenIdIsValid_ReturnsProcessTemplate()
    {
        // Arrange
        const int processTemplateId = 1;
        ProcessTemplate processTemplate = new()
        {
            Id = processTemplateId,
            FirmId = 1,
            ProcessTypeId = 1,
            ProcessId = 1,
            SubProcessId = 1,
            FileName = "",
            FilePath = ""
        };

        _mockRepository.Setup(x => x.GetByIdAsync(processTemplateId)).ReturnsAsync(processTemplate);

        // Act
        var result = await _processTemplatesService.GetById(processTemplateId);

        // Asserts
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(processTemplate);
        result.Id.Should().Be(processTemplateId);

        _mockRepository.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once);
    }
}