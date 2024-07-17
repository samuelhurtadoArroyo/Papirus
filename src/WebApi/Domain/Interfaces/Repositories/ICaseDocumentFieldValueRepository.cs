namespace Papirus.WebApi.Domain.Interfaces.Repositories;

public interface ICaseDocumentFieldValueRepository : IRepository<CaseDocumentFieldValue>
{
    public Task<IEnumerable<CaseDocumentFieldValue>> GetByCaseIdAsync(int caseId);

    public Task<IEnumerable<CaseDocumentFieldValue>> GetByCaseIdAndDocumentTypeIdAsync(int caseId, int? documentTypeId);
}