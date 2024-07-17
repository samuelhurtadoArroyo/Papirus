using AutoMapper;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Papirus.WebApi.Application.Dtos;
using Papirus.WebApi.Application.Interfaces;

namespace Papirus.WebApi.Application.Services.Tests;

[ExcludeFromCodeCoverage]
[TestFixture]
public class DocumentTemplateProcessServiceTests
{
    //private DocumentTemplateProcessService _documentTemplateProcessService = null!;

    
    //private Mock<IProcessTemplatesService> _mockProcessTemplatesService = null!;

    //private Mock<ICaseDocumentFieldValueService> _mockCaseDocumentFieldValueService = null!;

    //private Mock<ICaseService> _mockCaseService = null!;

    //private Mock<IDataManagerService> _mockDataService = null!;

    //private Mock<IMapper> _mockMapper = null!;

    //private Mock<IBusinessLineService> _mockBusinessLineService = new();

    //private Mock<BusinessLineDto> _mockBusinessLineDto = null!;

    //private Mock<IBusinessLineRepository> _mockBusinessLineRepository = new();

    //[SetUp]
    //public void Setup()
    //{
    //    _mockProcessTemplatesService = new Mock<IProcessTemplatesService>();
    //    _mockCaseDocumentFieldValueService = new Mock<ICaseDocumentFieldValueService>();
    //    _mockCaseService = new Mock<ICaseService>();
    //    _mockMapper = new Mock<IMapper>();
    //    _mockBusinessLineService = new Mock<IBusinessLineService>();
    //    _mockDataService = new Mock<IDataManagerService>();
    //    _mockBusinessLineDto = new Mock<BusinessLineDto>();
        

    //    _documentTemplateProcessService = new DocumentTemplateProcessServiceTests(
    //        _mockProcessTemplatesService.Object,
    //        _mockCaseDocumentFieldValueService.Object,
    //        _mockCaseService.Object, 
    //        _mockDataService.Object,
    //        _mockMapper.Object,
    //        _mockBusinessLineService.Object
    //        );
    //}

    //[Test]
    //public async Task GetTemplateDictionaryAsync_WhenCaseIdIsValid_ReturnsCaseTemplateDictionary()
    //{
    //    // Arrange
    //    const int caseId = 1;
    //    var caseTemplateDictionary = new List<CaseDocumentFieldValueDto>
    //    {
    //        new() { Id = 1, CaseId = caseId }
    //    };

    //    _mockCaseDocumentFieldValueService.Setup(x => x.GetByCaseIdAsync(caseId)).ReturnsAsync(caseTemplateDictionary);

    //    // Act
    //    var caseResult = await _documentTemplateProcessService.GetTemplateDictionaryAsync(caseId);

    //    // Asserts
    //    caseResult.Should().NotBeNull();
    //    caseResult.Should().BeEquivalentTo(caseTemplateDictionary);

    //    _mockCaseDocumentFieldValueService.Verify(x => x.GetByCaseIdAsync(It.IsAny<int>()), Times.Once);
    //}

    //[Test]
    //public async Task GetDocumentProcessAsync_WhenCaseIsValid_ReturnsCreatedCase()
    //{
    //    // Arrange
    //    const int caseId = 1;
    //    var caseProcessDocument = new List<CaseProcessDocument>
    //    {
    //        new() { Id = 1, CaseId = caseId, DocumentTypeId = 1, ProcessDocumentTypeId = 1 }
    //    };

    //    _mockCaseProcessDocumentService.Setup(x => x.GetByCaseId(caseId)).ReturnsAsync(caseProcessDocument);

    //    // Act
    //    var caseResult = await _documentTemplateProcessService.GetDocumentProcessAsync(caseId);

    //    // Asserts
    //    caseResult.Should().NotBeNull();
    //    caseResult.Should().BeEquivalentTo(caseProcessDocument);

    //    _mockCaseProcessDocumentService.Verify(x => x.GetByCaseId(It.IsAny<int>()), Times.Once);
    //}

    //[Test]
    //public async Task ReplaceTextAsync_WhenProcesstemplateIsValid_ReturnsCaseProcessDocument()
    //{
    //    // Arrange
    //    var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "document");
    //    Directory.CreateDirectory(path);
    //    var processTemplate = new ProcessTemplate()
    //    {
    //        FirmId = 1,
    //        ProcessTypeId = 2,
    //        ProcessId = 2,
    //        SubProcessId = null,
    //        FileName = "Doc1.docx",
    //        FilePath = path,
    //        Id = 2
    //    };
    //    var fullPath = Path.Combine(path, processTemplate.FileName);

    //    using (var document = WordprocessingDocument.Create(fullPath, WordprocessingDocumentType.Document))
    //    {
    //        MainDocumentPart mainPart = document.AddMainDocumentPart();
    //        mainPart.Document = new Document();
    //        mainPart.Document.AppendChild(new Body());

    //        mainPart.Document.Save();
    //    }

    //    var fileNameExpected = Path.Combine(path, processTemplate.FileName);

    //    var caseTemplateDictionary = new List<CaseDocumentFieldValueDto>
    //    {
    //        new() { Id = 1, CaseId = 1 }
    //    };
    //    var caseProcessDocumentDtoExpected = new CaseProcessDocumentDto
    //    {
    //        FileName = fileNameExpected
    //    };

    //    // Act
    //    var result = await _documentTemplateProcessService.ReplaceTextAsync(caseTemplateDictionary, processTemplate, path, 2, 23);

    //    // Asserts
    //    result.Should().NotBeNull();
    //    result.Should().BeEquivalentTo(caseProcessDocumentDtoExpected);
    //    result.FileName.Should().Be(fileNameExpected);
    //}

    //[Test]
    //public async Task ReplaceTextAsync_WhenDocumentHasBody_ReturnsCaseProcessDocument()
    //{
    //    // Arrange
    //    var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "document");
    //    Directory.CreateDirectory(path);
    //    var processTemplate = new ProcessTemplate()
    //    {
    //        FirmId = 1,
    //        ProcessTypeId = 2,
    //        ProcessId = 2,
    //        SubProcessId = null,
    //        FileName = "Doc1.docx",
    //        FilePath = path,
    //        Id = 2
    //    };
    //    var fullPath = Path.Combine(path, processTemplate.FileName);

    //    using (var document = WordprocessingDocument.Create(fullPath, WordprocessingDocumentType.Document))
    //    {
    //        MainDocumentPart mainPart = document.AddMainDocumentPart();
    //        mainPart.Document = new Document();

    //        Body body = mainPart.Document.AppendChild(new Body());
    //        Paragraph para = body.AppendChild(new Paragraph());
    //        Run run = para.AppendChild(new Run());
    //        run.AppendChild(new Text("Create text in body - CreateWordprocessingDocument"));

    //        mainPart.Document.Save();
    //    }

    //    var fileNameExpected = Path.Combine(path, processTemplate.FileName);

    //    var caseTemplateDictionary = new List<CaseDocumentFieldValueDto>
    //    {
    //        new() { Id = 1, CaseId = 1, Name = "CreateWordprocessingDocument" }
    //    };
    //    var caseProcessDocumentDtoExpected = new CaseProcessDocumentDto
    //    {
    //        FileName = fileNameExpected
    //    };

    //    // Act
    //    var result = await _documentTemplateProcessService.ReplaceTextAsync(caseTemplateDictionary, processTemplate, TestContext.CurrentContext.TestDirectory, 2, 23);

    //    // Asserts
    //    result.Should().NotBeNull();
    //    result.Should().BeEquivalentTo(caseProcessDocumentDtoExpected);
    //    result.FileName.Should().Be(fileNameExpected);
    //}

    //[Test]
    //public async Task GetProcessTemplateDocumentAsync_WhenCaseIdIsValid_ReturnsProcessTemplate()
    //{
    //    // Arrange
    //    const int caseId = 1;
    //    var caseResponseExpected = CaseMother.DemandCase();
    //    const int templateId = 1;
    //    var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "document");
    //    Directory.CreateDirectory(path);
    //    var processTemplateExpected = new ProcessTemplate()
    //    {
    //        FirmId = 1,
    //        ProcessTypeId = 1,
    //        ProcessId = 1,
    //        SubProcessId = 1,
    //        FileName = "Doc1.docx",
    //        FilePath = path,
    //        Id = 1
    //    };

    //    _mockProcessTemplatesService.Setup(x => x.GetAll()).ReturnsAsync(new List<ProcessTemplate> { processTemplateExpected });
    //    _mockCaseService.Setup(x => x.GetById(caseId)).ReturnsAsync(caseResponseExpected);

    //    // Act
    //    var result = await _documentTemplateProcessService.GetProcessTemplateDocumentAsync(caseId, templateId);

    //    // Asserts
    //    result.Should().NotBeNull();
    //    result.Should().BeEquivalentTo(processTemplateExpected);

    //    _mockProcessTemplatesService.Verify(x => x.GetAll(), Times.Once);
    //    _mockCaseService.Verify(x => x.GetById(It.IsAny<int>()), Times.Once);
    //}
}