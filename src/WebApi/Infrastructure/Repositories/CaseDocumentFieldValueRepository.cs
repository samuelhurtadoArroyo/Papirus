namespace Papirus.WebApi.Infrastructure.Repositories;

public class CaseDocumentFieldValueRepository : Repository<CaseDocumentFieldValue>, ICaseDocumentFieldValueRepository
{
    private readonly AppDbContext _context;

    public CaseDocumentFieldValueRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves case document field values filtered by CaseId and DocumentTypeId.
    /// </summary>
    /// <param name="caseId">The identifier of the case.</param>
    /// <param name="documentTypeId">The identifier of the document type.</param>
    /// <returns>A list of case document field values.</returns>
    public async Task<IEnumerable<CaseDocumentFieldValue>> GetByCaseIdAndDocumentTypeIdAsync(int caseId, int? documentTypeId)
    {
        IQueryable<CaseDocumentFieldValue> query = _context.CaseDocumentFieldValues.Where(x => x.CaseId == caseId);

        if (documentTypeId.HasValue)
        {
            query = query.Where(x => x.DocumentTypeId == documentTypeId.Value);
        }

        return await query.Include(x => x.DocumentType).ToListAsync();
    }

    /// <summary>
    /// Retrieves case document field values filtered by CaseId.
    /// </summary>
    /// <param name="caseId">The identifier of the case.</param>
    /// <returns>A list of case document field values.</returns>
    public async Task<IEnumerable<CaseDocumentFieldValue>> GetByCaseIdAsync(int caseId)
    {
        return await _context.CaseDocumentFieldValues
            .Where(x => x.CaseId == caseId)
            .Include(x => x.DocumentType)
            .ToListAsync();
    }
}