namespace Papirus.WebApi.Application.Interfaces;

public interface IDocumentTemplateProcessService
{
    Task<CaseProcessDocumentDto> ReplaceTextAsync
        (IEnumerable<CaseDocumentFieldValueDto> caseDocumentFieldValues, ProcessTemplate processtemplate, int caseId, int documentType, string rootDirectory);

    Task<ProcessTemplate> GetProcessTemplateDocumentAsync(int caseId, int templateId);

    Task<IEnumerable<CaseDocumentFieldValueDto>> GetTemplateDictionaryAsync(int caseId);

    Task<IEnumerable<CaseProcessDocument>> GetDocumentProcessAsync(int caseId);

    Task<CaseProcessDocument> PostDocumentProcessAsync(CaseProcessDocument caseProcessDocument);

    public string MoveTemplate(string fileNameOrigin, byte[] contentResult);

    public void DeleteTempFiles();
}