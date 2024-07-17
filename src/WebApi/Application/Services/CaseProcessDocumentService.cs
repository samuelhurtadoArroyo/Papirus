namespace Papirus.WebApi.Application.Services;

public class CaseProcessDocumentService : ICaseProcessDocumentService
{
    private readonly ICaseProcessDocumentRepository _caseProcessDocumentRepository;

    public CaseProcessDocumentService(ICaseProcessDocumentRepository caseProcessDocumentRepository)
    {
        _caseProcessDocumentRepository = caseProcessDocumentRepository;
    }

    public async Task<CaseProcessDocument> Create(CaseProcessDocument model)
    {
        return await _caseProcessDocumentRepository.AddAsync(model);
    }

    public async Task Delete(int id)
    {
        var existingDocument = await _caseProcessDocumentRepository.GetByIdAsync(id);

        if (existingDocument is not null)
        {
            await _caseProcessDocumentRepository.RemoveAsync(existingDocument);
            return;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<CaseProcessDocument> Edit(CaseProcessDocument model)
    {
        var id = model.Id;
        var existingDocument = await _caseProcessDocumentRepository.GetByIdAsync(id);

        if (existingDocument is not null)
        {
            return await _caseProcessDocumentRepository.UpdateAsync(model);
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<IEnumerable<CaseProcessDocument>> GetAll()
    {
        return await _caseProcessDocumentRepository.GetAllAsync();
    }

    public async Task<CaseProcessDocument> GetById(int id)
    {
        var retrievedDocument = await _caseProcessDocumentRepository.GetByIdAsync(id);

        if (retrievedDocument is not null)
        {
            return retrievedDocument;
        }

        throw new NotFoundException($"The Id={id} Not Found");
    }

    public async Task<IEnumerable<CaseProcessDocument>> GetByCaseId(int caseId)
    {
        return await _caseProcessDocumentRepository.FindAsync(x => x.CaseId.Equals(caseId));
    }

    public async Task<QueryResult<CaseProcessDocument>> GetByQueryRequestAsync(QueryRequest queryRequest)
    {
        return await _caseProcessDocumentRepository.GetByQueryRequestAsync(queryRequest);
    }
}