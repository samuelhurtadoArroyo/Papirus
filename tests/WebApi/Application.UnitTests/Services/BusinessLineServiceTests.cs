using AutoMapper;
using Papirus.WebApi.Application.Dtos;

namespace Papirus.WebApi.Application.Services.Tests;

[ExcludeFromCodeCoverage]
public class BusinessLineServiceTests
{
    private IBussinessLineService _service = null!;

    private Mock<IBusinessLineRepository> _mockRepository = null!;

    private Mock<IMapper> _mockMapper = null!;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IBusinessLineRepository>();
        _mockMapper = new Mock<IMapper>();

        _service = new IBussinessLineService(_mockRepository.Object, _mockMapper.Object);
    }

    [Test]
    public async Task GetAllAsync_WhenResultsFound_ReturnsBusinessLineDtos()
    {
        // Arrange
        var entities = new List<BusinessLine>
        {
            new() { Id = 1, Name = "Bancolombia S.A" }
        };
        var dtos = new List<BusinessLineDto>
        {
            new() { Id = 1, Name = "Bancolombia S.A" }
        };

        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);
        _mockMapper.Setup(m => m.Map<IEnumerable<BusinessLineDto>>(entities)).Returns(dtos);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(dtos);
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<BusinessLineDto>>(entities), Times.Once);
    }

    [Test]
    public async Task GetAllAsync_WhenNoResultsFound_ReturnsEmptyList()
    {
        // Arrange
        var emptyEntityList = new List<BusinessLine>();
        var emptyDtoList = new List<BusinessLineDto>();

        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(emptyEntityList);
        _mockMapper.Setup(m => m.Map<IEnumerable<BusinessLineDto>>(emptyEntityList)).Returns(emptyDtoList);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        result.Should().BeEmpty();
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<BusinessLineDto>>(emptyEntityList), Times.Once);
    }
}