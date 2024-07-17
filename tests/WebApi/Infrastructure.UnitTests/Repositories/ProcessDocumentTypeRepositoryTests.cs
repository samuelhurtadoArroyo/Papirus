namespace Papirus.WebApi.Infrastructure.Repositories.Tests;

[ExcludeFromCodeCoverage]
public class ProcessDocumentTypeRepositoryTests
{
    private List<ProcessDocumentType> _processDocumentTypeList = null!;

    private Mock<AppDbContext> _mockAppDbContext = null!;

    private Mock<DbSet<ProcessDocumentType>> _mockDbSet = null!;

    private ProcessDocumentTypeRepository _repository = null!;

    [SetUp]
    public void SetUp()
    {
        _processDocumentTypeList = [];
        _mockAppDbContext = new Mock<AppDbContext>();
        _mockDbSet = new Mock<DbSet<ProcessDocumentType>>();
        _mockAppDbContext.Setup(x => x.Set<ProcessDocumentType>()).ReturnsDbSet(_processDocumentTypeList);
        _mockAppDbContext.Setup(x => x.ProcessDocumentTypes).ReturnsDbSet(_processDocumentTypeList);
        _mockAppDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        _repository = new ProcessDocumentTypeRepository(_mockAppDbContext.Object);
    }

    [Test]
    public async Task GetAllAsync_ReturnsProcessDocumentTypeList()
    {
        // Arrange

        // Act
        var result = await _repository.GetAllAsync() as List<ProcessDocumentType>;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_processDocumentTypeList);
    }

    [Test]
    public async Task AddProcessDocumentTypeAsync_WhenCalled_AddsProcessDocumentType()
    {
        // Arrange
        var processDocumentType = new ProcessDocumentType();
        _mockAppDbContext.Setup(m => m.ProcessDocumentTypes).Returns(_mockDbSet.Object);

        // Act
        var result = await _repository.AddAsync(processDocumentType);

        // Assert
        result.Should().Be(processDocumentType);
        _mockAppDbContext.Verify(x => x.Set<ProcessDocumentType>(), Times.Once);
        _mockAppDbContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
    }

    [Test]
    public async Task UpdateProcessDocumentTypeAsync_WhenCalled_UpdatesProcessDocumentType()
    {
        // Arrange
        var processDocumentType = new ProcessDocumentType();
        _mockAppDbContext.Setup(m => m.ProcessDocumentTypes).Returns(_mockDbSet.Object);

        // Act
        var result = await _repository.UpdateAsync(processDocumentType);

        // Assert
        result.Should().Be(processDocumentType);
        _mockAppDbContext.Verify(x => x.Set<ProcessDocumentType>().Update(It.IsAny<ProcessDocumentType>()), Times.Once);
        _mockAppDbContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
    }

    [Test]
    public async Task DeleteProcessDocumentTypeAsync_WhenCalled_DeleteProcessDocumentType()
    {
        // Arrange
        var processDocumentType = new ProcessDocumentType();
        _mockAppDbContext.Setup(m => m.ProcessDocumentTypes).Returns(_mockDbSet.Object);

        // Act
        await _repository.RemoveAsync(processDocumentType);

        // Assert
        _mockAppDbContext.Verify(x => x.Set<ProcessDocumentType>().Remove(It.IsAny<ProcessDocumentType>()), Times.Once);
        _mockAppDbContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
    }
}