namespace Papirus.WebApi.Domain.Dtos;

public class PaginationData
{
    public int PageNumber { get; init; }

    public int PageSize { get; init; }

    public int TotalPages { get; init; }

    public int TotalCount { get; init; }

    public int PreviousPage
    {
        get => PageNumber == 1 ? 1 : PageNumber - 1;
    }

    public int NextPage
    {
        get => PageNumber == TotalPages ? TotalPages : PageNumber + 1;
    }

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;
}