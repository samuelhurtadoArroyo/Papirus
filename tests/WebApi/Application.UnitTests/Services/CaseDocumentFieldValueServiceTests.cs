using AutoMapper;
using Papirus.WebApi.Application.Dtos;
using SharpCompress;
using System.Linq.Expressions;

namespace Papirus.WebApi.Application.Services.Tests;

[ExcludeFromCodeCoverage]
public class CaseDocumentFieldValueServiceTests
{
    private CaseDocumentFieldValueService _service = null!;

    private Mock<ICaseDocumentFieldValueRepository> _mockRepository = null!;

    private Mock<IMapper> _mockMapper = null!;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<ICaseDocumentFieldValueRepository>();
        _mockMapper = new Mock<IMapper>();

        _service = new CaseDocumentFieldValueService(_mockRepository.Object, _mockMapper.Object);
    }

    [Test]
    public async Task GetByCaseIdAndDocumentTypeIdAsync_WhenCaseAndDocumentTypeExist_ReturnsCaseDocumentFieldValueDtos()
    {
        // Arrange
        const int caseId = 1;
        const int documentTypeId = 2;
        var entities = new List<CaseDocumentFieldValue>
        {
            new() { Id = 1, CaseId = caseId, DocumentTypeId = documentTypeId }
        };
        var dtos = new List<CaseDocumentFieldValueDto>
        {
            new() { Id = 1, CaseId = caseId, DocumentTypeId = documentTypeId }
        };

        _mockRepository.Setup(r => r.GetByCaseIdAndDocumentTypeIdAsync(caseId, documentTypeId)).ReturnsAsync(entities);
        _mockMapper.Setup(m => m.Map<IEnumerable<CaseDocumentFieldValueDto>>(entities)).Returns(dtos);

        // Act
        var result = await _service.GetByCaseIdAndDocumentTypeIdAsync(caseId, documentTypeId);

        // Assert
        result.Should().BeEquivalentTo(dtos);
        _mockRepository.Verify(r => r.GetByCaseIdAndDocumentTypeIdAsync(caseId, documentTypeId), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<CaseDocumentFieldValueDto>>(entities), Times.Once);
    }

    [Test]
    public async Task GetByCaseIdAsync_WhenCaseExists_ReturnsCaseDocumentFieldValueDtos()
    {
        // Arrange
        const int caseId = 1;

        var entities = new List<CaseDocumentFieldValue>
        {
            new() { Id = 1, CaseId = caseId }
        };

        var dtos = new List<CaseDocumentFieldValueDto>
        {
            new() { Id = 1, CaseId = caseId }
        };

        _mockRepository.Setup(r => r.GetByCaseIdAsync(caseId)).ReturnsAsync(entities);
        _mockMapper.Setup(m => m.Map<IEnumerable<CaseDocumentFieldValueDto>>(entities)).Returns(dtos);

        // Act
        var result = await _service.GetByCaseIdAsync(caseId);

        // Assert
        result.Should().BeEquivalentTo(dtos);
        _mockRepository.Verify(r => r.GetByCaseIdAsync(caseId), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<CaseDocumentFieldValueDto>>(entities), Times.Once);
    }

    [Test]
    public async Task GetByCaseIdAsync_WhenNoResultsFound_ReturnsEmptyList()
    {
        // Arrange
        const int caseId = 1;
        var emptyEntityList = new List<CaseDocumentFieldValue>();
        var emptyDtoList = new List<CaseDocumentFieldValueDto>();

        _mockRepository.Setup(r => r.GetByCaseIdAsync(caseId)).ReturnsAsync(emptyEntityList);
        _mockMapper.Setup(m => m.Map<IEnumerable<CaseDocumentFieldValueDto>>(emptyEntityList)).Returns(emptyDtoList);

        // Act
        var result = await _service.GetByCaseIdAsync(caseId);

        // Assert
        result.Should().BeEmpty();
        _mockRepository.Verify(r => r.GetByCaseIdAsync(caseId), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<CaseDocumentFieldValueDto>>(emptyEntityList), Times.Once);
    }

    [Test]
    public async Task GetByCaseIdAndDocumentTypeIdAsync_WhenNoResultsFound_ReturnsEmptyList()
    {
        // Arrange
        const int caseId = 1;
        const int documentTypeId = 2;
        var emptyEntityList = new List<CaseDocumentFieldValue>();
        var emptyDtoList = new List<CaseDocumentFieldValueDto>();

        _mockRepository.Setup(r => r.GetByCaseIdAndDocumentTypeIdAsync(caseId, documentTypeId)).ReturnsAsync(emptyEntityList);
        _mockMapper.Setup(m => m.Map<IEnumerable<CaseDocumentFieldValueDto>>(emptyEntityList)).Returns(emptyDtoList);

        // Act
        var result = await _service.GetByCaseIdAndDocumentTypeIdAsync(caseId, documentTypeId);

        // Assert
        result.Should().BeEmpty();
        _mockRepository.Verify(r => r.GetByCaseIdAndDocumentTypeIdAsync(caseId, documentTypeId), Times.Once);
        _mockMapper.Verify(m => m.Map<IEnumerable<CaseDocumentFieldValueDto>>(emptyEntityList), Times.Once);
    }

    [Test]
    public async Task UpdateCaseDocumentFieldValues_WhenListIsEmpty_ReturnsTrue()
    {
        // Arrange
        const bool resultExpected = true;
        List<UpdateCaseDocumentFieldValueDto> updateDtos = [];
        var ids = updateDtos.Select(c => c.Id).ToArray();
        List<CaseDocumentFieldValue> emptyEntityList = [];
        _mockRepository.Setup(r => r.FindAsync(x => ids.Contains(x.Id))).ReturnsAsync(emptyEntityList);

        // Act
        var response = await _service.UpdateCaseDocumentFieldValues(updateDtos);

        // Assert
        response.Should().NotNull();
        response.Should().Be(resultExpected);

        _mockRepository.Verify(x => x.FindAsync(It.IsAny<Expression<Func<CaseDocumentFieldValue, bool>>>()), Times.Once);
    }

    [Test]
    public async Task UpdateCaseDocumentFieldValues_WhenListEntityIsNotEmpty_ReturnsTrue()
    {
        // Arrange
        const bool resultExpected = true;
        List<UpdateCaseDocumentFieldValueDto> updateDtos = [];
        var ids = updateDtos.Select(c => c.Id).ToArray();
        var entityList = new List<CaseDocumentFieldValue>
        {
            new() {
                CaseProcessDocumentId = 1,
                DocumentTypeId = 1,
                ProcessDocumentTypeId = 1,
                CaseId = 1,
                Id = 1
            }
        };
        _mockRepository.Setup(r => r.FindAsync(x => ids.Contains(x.Id))).ReturnsAsync(entityList);

        // Act
        var response = await _service.UpdateCaseDocumentFieldValues(updateDtos);

        // Assert
        response.Should().NotNull();
        response.Should().Be(resultExpected);

        _mockRepository.Verify(x => x.FindAsync(It.IsAny<Expression<Func<CaseDocumentFieldValue, bool>>>()), Times.Once);
    }

    [Test]
    public async Task UpdateCaseDocumentFieldValues_WhenListDtoIsNotEmpty_ReturnsTrue()
    {
        // Arrange
        const bool resultExpected = true;
        var updateDtos = new List<UpdateCaseDocumentFieldValueDto>
        {
            new() {
                Id = 1,
                FieldValue = "value"
            }
        };
        var entityList = new List<CaseDocumentFieldValue>
        {
            new() {
                CaseProcessDocumentId = 1,
                DocumentTypeId = 1,
                ProcessDocumentTypeId = 1,
                CaseId = 1,
                Id = 1,
                FieldValue = "value"
            }
        };
        _mockRepository.Setup(r => r.FindAsync(It.IsAny<Expression<Func<CaseDocumentFieldValue, bool>>>())).ReturnsAsync(entityList);

        // Act
        var response = await _service.UpdateCaseDocumentFieldValues(updateDtos);

        // Assert
        response.Should().NotNull();
        response.Should().Be(resultExpected);

        _mockRepository.Verify(x => x.FindAsync(It.IsAny<Expression<Func<CaseDocumentFieldValue, bool>>>()), Times.Once);
    }

    [Test]
    public async Task UpdateCaseDocumentFieldValues_WhenTrow_ReturnsFalse()
    {
        // Arrange
        const bool resultExpected = false;
        var updateDtos = new List<UpdateCaseDocumentFieldValueDto>
        {
            new() {
                Id = 1,
                FieldValue = "value"
            }
        };
        var entityList = new List<CaseDocumentFieldValue>
        {
            new() {
                CaseProcessDocumentId = 1,
                DocumentTypeId = 1,
                ProcessDocumentTypeId = 1,
                CaseId = 1,
                Id = 1,
                FieldValue = "value"
            }
        };
        _mockRepository.Setup(r => r.FindAsync(It.IsAny<Expression<Func<CaseDocumentFieldValue, bool>>>())).ReturnsAsync(entityList);
        _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<CaseDocumentFieldValue>())).ThrowsAsync(new Exception());

        // Act
        var response = await _service.UpdateCaseDocumentFieldValues(updateDtos);

        // Assert
        response.Should().NotNull();
        response.Should().Be(resultExpected);

        _mockRepository.Verify(x => x.FindAsync(It.IsAny<Expression<Func<CaseDocumentFieldValue, bool>>>()), Times.Once);
        _mockRepository.Verify(x => x.UpdateAsync(It.IsAny<CaseDocumentFieldValue>()), Times.Once);
    }
}