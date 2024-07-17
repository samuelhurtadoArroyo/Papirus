namespace Papirus.WebApi.Api.Controllers;

[Authorize]
[Route("api/v1.0/[controller]")]
[ApiController]
public class DocumentTemplateProcessController : ControllerBase
{
    private readonly IDocumentTemplateProcessService _documentTemplateProcessService;

    private readonly IMapper _mapper;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public DocumentTemplateProcessController(IDocumentTemplateProcessService documentTemplateProcessService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
    {
        _documentTemplateProcessService = documentTemplateProcessService;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
    }

    /// <summary>
    /// Processes a document template for a given case.
    /// </summary>
    /// <param name="caseId">The ID of the case.</param>
    /// <param name="templateId">
    /// The ID of the document template.
    /// templateId 1: CONTESTACION SENCILLA TUTELA (Simple Answer to Guardianship)
    /// templateId 2: ESCRITO DE EMERGENCIA (Emergency Document)
    /// </param>
    /// <returns>A file containing the processed document.</returns>
    [HttpPost("{caseId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaseProcessDocumentDto))]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> ProcessDocumentTemplate(int caseId, int templateId, int documenType)
    {
        try
        {
            var documentProcess = await _documentTemplateProcessService.GetDocumentProcessAsync(caseId);
            if (documentProcess != null)
            {
                var documentProcessPath = documentProcess.FirstOrDefault(x => x.DocumentTypeId == documenType);
                if (documentProcessPath != null)
                {
                    return Ok(_mapper.Map<CaseProcessDocument, CaseProcessDocumentDto>(documentProcessPath));
                }
            }

            var processtemplate = await _documentTemplateProcessService.GetProcessTemplateDocumentAsync(caseId, templateId);

            if (processtemplate != null)
            {
                _documentTemplateProcessService.DeleteTempFiles();

                var caseTemplateDictionary = await _documentTemplateProcessService.GetTemplateDictionaryAsync(caseId);

                if (!caseTemplateDictionary.Any())
                    return NotFound();

                var result = await _documentTemplateProcessService.ReplaceTextAsync(caseTemplateDictionary, processtemplate, caseId, documenType, _webHostEnvironment.ContentRootPath);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    NotFound();
                }
            }
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorDetails
            {
                ErrorType = ReasonPhrases.GetReasonPhrase(StatusCodes.Status400BadRequest),
                Errors = new List<string> { ex.Message }
            });
        }
    }

    protected string MoveFile(string fileName, string filePath)
    {
        HttpClient httpClient = new();
        HttpResponseMessage response = httpClient.GetAsync(filePath).Result;

        if (response.IsSuccessStatusCode)
        {
            byte[] documentContent = response.Content.ReadAsByteArrayAsync().Result;

            return _documentTemplateProcessService.MoveTemplate(fileName, documentContent);
        }
        return string.Empty;
    }
}