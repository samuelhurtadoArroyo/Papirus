using Microsoft.AspNetCore.Hosting;

namespace Papirus.WebApi.Api.Controllers.Tests;

[ExcludeFromCodeCoverage]
[Ignore("Due date")]
public class DocumentTemplateProcessControllerTests
{
    //private DocumentTemplateProcessController _documentTemplateProcessControllerController = null!;

    private Mock<IDocumentTemplateProcessService> _mockDocumentTemplateProcessService = null!;

    private Mock<IWebHostEnvironment>? _env;

    [SetUp]
    [Ignore("Due date")]
    public void Setup()
    {
        _mockDocumentTemplateProcessService = new Mock<IDocumentTemplateProcessService>();
        _env = new Mock<IWebHostEnvironment>();
    }

    //[Test]
    //public async Task GetDocumentProcessAsync_WhenCalled_ReturnsFileDownloadName()
    //{
    //    // Arrange
    //    var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "document");
    //    Directory.CreateDirectory(path);
    //    const string fileName = "Doc1.docx";
    //    var fullPath = Path.Combine(path, fileName);
    //    _env!.Object.ContentRootPath = path;
    //    await File.Create(fullPath).DisposeAsync();

    //    var caseResponseExpected = CaseMother.DemandCase();
    //    var caseProcessDocuments = new List<CaseProcessDocument>
    //    {
    //        new() {
    //            DocumentTypeId = 1,
    //            ProcessDocumentTypeId = 1,
    //            CaseId = caseResponseExpected.Id,
    //            FileName = caseResponseExpected.FileName,
    //            FilePath = caseResponseExpected.FilePath,
    //            Case = caseResponseExpected,
    //            Id = 1
    //        }
    //    };
    //    var caseTemplateDictionary = new List<CaseDocumentFieldValueDto>
    //    {
    //        new() { Id = 1, CaseId = caseResponseExpected.Id }
    //    };
    //    var processTemplateExpected = new ProcessTemplate()
    //    {
    //        FirmId = 1,
    //        ProcessTypeId = 1,
    //        ProcessId = 1,
    //        SubProcessId = 1,
    //        FileName = fullPath,
    //        FilePath = path,
    //        Id = 1
    //    };
    //    var caseProcessDocumentDto = new CaseProcessDocumentDto()
    //    {
    //        Id = 1,
    //        DocumentTypeId = 1,
    //        ProcessDocumentTypeId = 1,
    //        CaseId = 1,
    //        FileName = fullPath,
    //        FilePath = path
    //    };

    //    _mockDocumentTemplateProcessService.Setup(x => x.GetDocumentProcessAsync(caseResponseExpected.Id)).ReturnsAsync(caseProcessDocuments);
    //    _mockDocumentTemplateProcessService.Setup(x => x.GetProcessTemplateDocumentAsync(caseResponseExpected.Id, 1)).ReturnsAsync(processTemplateExpected);
    //    _mockDocumentTemplateProcessService.Setup(x => x.GetTemplateDictionaryAsync(caseResponseExpected.Id)).ReturnsAsync(caseTemplateDictionary);
    //    _mockDocumentTemplateProcessService.Setup(x => x.ReplaceTextAsync(caseTemplateDictionary, processTemplateExpected, _env.Object.ContentRootPath, 1, 1)).ReturnsAsync(caseProcessDocumentDto);

    //    // Act
    //    var response = await _documentTemplateProcessControllerController.ProcessDocumentTemplate(caseResponseExpected.Id, 1, 23) as FileStreamResult;

    //    // Asserts
    //    response.Should().NotBeNull();
    //    response!.FileDownloadName.Should().Be(fileName);
    //    response!.ContentType.Should().Be("application/vnd.openxmlformats-officedocument.wordprocessingml.document");

    //    _mockDocumentTemplateProcessService.Verify(x => x.ReplaceTextAsync(caseTemplateDictionary, processTemplateExpected, _env.Object.ContentRootPath, 1, 1), Times.Once());
    //}

    //[Test]
    //public async Task GetDocumentProcessAsync_WhenCalled_ReturnsNotFound()
    //{
    //    // Arrange
    //    var caseResponseExpected = CaseMother.DemandCase();
    //    List<CaseProcessDocument>? caseProcessDocument = null;

    //    _mockDocumentTemplateProcessService.Setup(x => x.GetDocumentProcessAsync(caseResponseExpected.Id)).ReturnsAsync(caseProcessDocument!);

    //    // Act
    //    var response = await _documentTemplateProcessControllerController.ProcessDocumentTemplate(caseResponseExpected.Id, 1, 23) as NotFoundResult;

    //    // Asserts
    //    response.Should().NotBeNull();
    //    response!.StatusCode.Should().Be(StatusCodes.Status404NotFound);

    //    _mockDocumentTemplateProcessService.Verify(x => x.GetDocumentProcessAsync(It.IsAny<int>()), Times.Once());
    //}
}