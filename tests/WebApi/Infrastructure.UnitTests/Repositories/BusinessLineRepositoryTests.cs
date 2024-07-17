namespace Papirus.WebApi.Infrastructure.Repositories.Tests;

[ExcludeFromCodeCoverage]
public class BusinessLineRepositoryTests
{
    private List<BusinessLine> _processDocumentTypeList = null!;

    private Mock<AppDbContext> _mockAppDbContext = null!;

    private Mock<DbSet<BusinessLine>> _mockDbSet = null!;

    private BusinessLineRepository _repository = null!;

    [SetUp]
    public void SetUp()
    {
        _processDocumentTypeList = [];
        _mockAppDbContext = new Mock<AppDbContext>();
        _mockDbSet = new Mock<DbSet<BusinessLine>>();
        _mockAppDbContext.Setup(x => x.Set<BusinessLine>()).ReturnsDbSet(_processDocumentTypeList);
        _mockAppDbContext.Setup(x => x.BusinessLines).ReturnsDbSet(_processDocumentTypeList);
        _mockAppDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        _repository = new BusinessLineRepository(_mockAppDbContext.Object);
    }

    [Test]
    public async Task GetAllAsync_ReturnsBusinessLineList()
    {
        // Arrange

        // Act
        var result = await _repository.GetAllAsync() as List<BusinessLine>;

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_processDocumentTypeList);
    }

    [Test]
    public async Task AddBusinessLineAsync_WhenCalled_AddsBusinessLine()
    {
        // Arrange
        var businessLine = new BusinessLine();
        _mockAppDbContext.Setup(m => m.BusinessLines).Returns(_mockDbSet.Object);

        // Act
        await _repository.AddAsync(businessLine);

        // Assert
        _mockAppDbContext.Verify(x => x.Set<BusinessLine>(), Times.Once);
        _mockAppDbContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
    }

    [Test]
    public async Task UpdateBusinessLineAsync_WhenCalled_UpdatesBusinessLine()
    {
        // Arrange
        var businessLine = new BusinessLine();
        _mockAppDbContext.Setup(m => m.BusinessLines).Returns(_mockDbSet.Object);

        // Act
        await _repository.UpdateAsync(businessLine);

        // Assert
        _mockAppDbContext.Verify(x => x.Set<BusinessLine>().Update(It.IsAny<BusinessLine>()), Times.Once);
        _mockAppDbContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
    }

    [Test]
    public async Task DeleteBusinessLineAsync_WhenCalled_DeleteBusinessLine()
    {
        // Arrange
        var businessLine = new BusinessLine();
        _mockAppDbContext.Setup(m => m.BusinessLines).Returns(_mockDbSet.Object);

        // Act
        await _repository.RemoveAsync(businessLine);

        // Assert
        _mockAppDbContext.Verify(x => x.Set<BusinessLine>().Remove(It.IsAny<BusinessLine>()), Times.Once);
        _mockAppDbContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
    }
}