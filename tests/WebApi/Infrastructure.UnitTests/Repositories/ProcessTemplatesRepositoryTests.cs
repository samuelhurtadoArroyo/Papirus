namespace Papirus.WebApi.Infrastructure.Repositories.Tests;

[ExcludeFromCodeCoverage]
public class ProcessTemplatesRepositoryTests
{
    private List<ProcessTemplate> _processDocumentTypeList = null!;

    private Mock<AppDbContext> _mockAppDbContext = null!;

    private Mock<DbSet<ProcessTemplate>> _mockDbSet = null!;

    private ProcessTemplatesRepository _repository = null!;

    [SetUp]
    public void SetUp()
    {
        _processDocumentTypeList = [];
        _mockAppDbContext = new Mock<AppDbContext>();
        _mockDbSet = new Mock<DbSet<ProcessTemplate>>();
        _mockAppDbContext.Setup(x => x.Set<ProcessTemplate>()).ReturnsDbSet(_processDocumentTypeList);
        _mockAppDbContext.Setup(x => x.ProcessTemplates).ReturnsDbSet(_processDocumentTypeList);
        _mockAppDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        _repository = new ProcessTemplatesRepository(_mockAppDbContext.Object);
    }

    [Test]
    public async Task GetAllAsync_ReturnsProcessDocumentTypeList()
    {
        // Arrange

        // Act
        var result = await _repository.GetAllAsync() as List<ProcessTemplate>;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_processDocumentTypeList);
    }

    [Test]
    public async Task AddProcessDocumentTypeAsync_WhenCalled_AddsProcessDocumentType()
    {
        // Arrange
        var processTemplate = new ProcessTemplate();
        _mockAppDbContext.Setup(m => m.ProcessTemplates).Returns(_mockDbSet.Object);

        // Act
        var result = await _repository.AddAsync(processTemplate);

        // Assert
        result.Should().Be(processTemplate);
        _mockAppDbContext.Verify(x => x.Set<ProcessTemplate>(), Times.Once);
        _mockAppDbContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
    }

    [Test]
    public async Task UpdateProcessDocumentTypeAsync_WhenCalled_UpdatesProcessDocumentType()
    {
        // Arrange
        var processTemplate = new ProcessTemplate();
        _mockAppDbContext.Setup(m => m.ProcessTemplates).Returns(_mockDbSet.Object);

        // Act
        var result = await _repository.UpdateAsync(processTemplate);

        // Assert
        result.Should().Be(processTemplate);
        _mockAppDbContext.Verify(x => x.Set<ProcessTemplate>().Update(It.IsAny<ProcessTemplate>()), Times.Once);
        _mockAppDbContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
    }

    [Test]
    public async Task DeleteProcessDocumentTypeAsync_WhenCalled_DeleteProcessDocumentType()
    {
        // Arrange
        var processTemplate = new ProcessTemplate();
        _mockAppDbContext.Setup(m => m.ProcessTemplates).Returns(_mockDbSet.Object);

        // Act
        await _repository.RemoveAsync(processTemplate);

        // Assert
        _mockAppDbContext.Verify(x => x.Set<ProcessTemplate>().Remove(It.IsAny<ProcessTemplate>()), Times.Once);
        _mockAppDbContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
    }
}