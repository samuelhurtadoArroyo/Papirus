namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class CaseProcessDocumentBuilder
{
    private int _id;

    private int _documentTypeId;

    private int _processDocumentTypeId;

    private int _caseId;

    private string _fileName;

    private string _filePath;

    public CaseProcessDocumentBuilder()
    {
        _id = 0;
        _documentTypeId = 0;
        _processDocumentTypeId = 0;
        _caseId = 0;
        _fileName = null!;
        _filePath = null!;
    }

    public CaseProcessDocumentBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public CaseProcessDocumentBuilder WithDocumentTypeId(int documentTypeId)
    {
        _documentTypeId = documentTypeId;
        return this;
    }

    public CaseProcessDocumentBuilder WithProcessDocumentTypeId(int processDocumentTypeId)
    {
        _processDocumentTypeId = processDocumentTypeId;
        return this;
    }

    public CaseProcessDocumentBuilder WithCaseId(int caseId)
    {
        _caseId = caseId;
        return this;
    }

    public CaseProcessDocumentBuilder WithFileName(string fileName)
    {
        _fileName = fileName;
        return this;
    }

    public CaseProcessDocumentBuilder WithFilePath(string filePath)
    {
        _filePath = filePath;
        return this;
    }

    public CaseProcessDocument Build()
    {
        return new CaseProcessDocument
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