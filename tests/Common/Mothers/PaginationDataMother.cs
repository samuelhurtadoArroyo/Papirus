namespace Papirus.Tests.Common.Mothers;

[ExcludeFromCodeCoverage]
public static class PaginationDataMother
{
    public static PaginationData Create(int pageNumber, int pageSise, int totalCount, int totalPages)
    {
        return new PaginationDataBuilder()
               .WithPageNumber(pageNumber)
               .WithPageSize(pageSise)
               .WithTotalCount(totalCount)
               .WithTotalPages(totalPages)
               .Build();
    }

    public static PaginationData DefaultPaginationData()
    {
        return Create(PaginationConst.DefaultPageNumber,
                                    PaginationConst.DefaultPageSize,
                                    CommonConst.MaxCount,
                                    (int)Math.Ceiling(CommonConst.MaxCount / (double)PaginationConst.DefaultPageSize));
    }
}