namespace Papirus.WebApi.Application.Interfaces;

public interface ICaseProcessDocumentService : IService<CaseProcessDocument>
{
    public Task<IEnumerable<CaseProcessDocument>> GetByCaseId(int caseId);
}