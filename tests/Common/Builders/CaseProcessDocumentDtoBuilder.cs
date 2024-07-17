namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class CaseProcessDocumentDtoBuilder
{
    private int _id;

    private int _documentTypeId;

    private int _processDocumentTypeId;

    private int _caseId;

    private string _fileName;

    private string _filePath;

    public CaseProcessDocumentDtoBuilder()
    {
        _id = 0;
        _documentTypeId = 0;
        _processDocumentTypeId = 0;
        _caseId = 0;
        _fileName = null!;
        _filePath = null!;
    }

    public CaseProcessDocumentDtoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public CaseProcessDocumentDtoBuilder WithDocumentTypeId(int documentTypeId)
    {
        _documentTypeId = documentTypeId;
        return this;
    }

    public CaseProcessDocumentDtoBuilder WithProcessDocumentTypeId(int processDocumentTypeId)
    {
        _processDocumentTypeId = processDocumentTypeId;
        return this;
    }

    public CaseProcessDocumentDtoBuilder WithCaseId(int caseId)
    {
        _caseId = caseId;
        return this;
    }

    public CaseProcessDocumentDtoBuilder WithFileName(string fileName)
    {
        _fileName = fileName;
        return this;
    }

    public CaseProcessDocumentDtoBuilder WithFilePath(string filePath)
    {
        _filePath = filePath;
        return this;
    }

    public CaseProcessDocumentDto Build()
    {
        return new CaseProcessDocumentDto
        {
            Id = _id,
            DocumentTypeId = _documentTypeId,
            ProcessDocumentTypeId = _processDocumentTypeId,
            CaseId = _caseId,
            FileName = _fileName,
            FilePath = _filePath
        };
    }
}