namespace Papirus.WebApi.Application.Interfaces;

public interface ICaseDocumentFieldValueService
{
    public Task<IEnumerable<CaseDocumentFieldValueDto>> GetByCaseIdAsync(int caseId);

    public Task<IEnumerable<CaseDocumentFieldValueDto>> GetByCaseIdAndDocumentTypeIdAsync(int caseId, int? documentTypeId);

    public Task<bool> UpdateCaseDocumentFieldValues(List<UpdateCaseDocumentFieldValueDto> updateDtos);
}