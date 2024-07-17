namespace Papirus.Tests.Common.Builders;

[ExcludeFromCodeCoverage]
public class PaginationDataBuilder
{
    private int _pageNumber;

    private int _pageSize;

    private int _totalPages;

    private int _totalCount;

    public PaginationDataBuilder()
    {
        _pageNumber = 0;
        _pageSize = 0;
        _totalPages = 0;
        _totalCount = 0;
    }

    public PaginationDataBuilder WithPageNumber(int pageNumber)
    {
        _pageNumber = pageNumber;
        return this;
    }

    public PaginationDataBuilder WithPageSize(int pageSize)
    {
        _pageSize = pageSize;
        return this;
    }

    public PaginationDataBuilder WithTotalPages(int totalPages)
    {
        _totalPages = totalPages;
        return this;
    }

    public PaginationDataBuilder WithTotalCount(int totalCount)
    {
        _totalCount = totalCount;
        return this;
    }

    public PaginationData Build()
    {
        return new PaginationData
        {
            PageNumber = _pageNumber,
            PageSize = _pageSize,
            TotalPages = _totalPages,
            TotalCount = _totalCount
        };
    }
}