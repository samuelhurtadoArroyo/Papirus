namespace Papirus.WebApi.Application.Services;

public class CaseDocumentFieldValueService : ICaseDocumentFieldValueService
{
    private readonly ICaseDocumentFieldValueRepository _repository;

    private readonly IMapper _mapper;

    public CaseDocumentFieldValueService(ICaseDocumentFieldValueRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CaseDocumentFieldValueDto>> GetByCaseIdAndDocumentTypeIdAsync(int caseId, int? documentTypeId)
    {
        var entities = await _repository.GetByCaseIdAndDocumentTypeIdAsync(caseId, documentTypeId);
        return _mapper.Map<IEnumerable<CaseDocumentFieldValueDto>>(entities);
    }

    /// <summary>
    /// Retrieves case document field values filtered by CaseId.
    /// </summary>
    /// <param name="caseId">The identifier of the case.</param>
    /// <returns>A list of case document field value DTOs.</returns>
    public async Task<IEnumerable<CaseDocumentFieldValueDto>> GetByCaseIdAsync(int caseId)
    {
        var entities = await _repository.GetByCaseIdAsync(caseId);
        return _mapper.Map<IEnumerable<CaseDocumentFieldValueDto>>(entities);
    }

    public async Task<bool> UpdateCaseDocumentFieldValues(List<UpdateCaseDocumentFieldValueDto> updateDtos)
    {
        bool allUpdatesSuccessful = true;
        var ids = updateDtos.Select(c => c.Id).ToArray();
        var records = await _repository.FindAsync(c => ids.Contains(c.Id));
        foreach (var record in records)
        {
            var dto = updateDtos.Find(c => c.Id == record.Id);
            if (dto != null)
            {
                try
                {
                    record.FieldValue = dto.FieldValue;
                    await _repository.UpdateAsync(record);
                }
                catch (Exception)
                {
                    allUpdatesSuccessful = false;
                }
            }
        }
        return allUpdatesSuccessful;
    }
}