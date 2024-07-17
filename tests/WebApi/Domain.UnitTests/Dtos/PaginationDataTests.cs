namespace Papirus.WebApi.Domain.UnitTests.Dtos;

[ExcludeFromCodeCoverage]
[TestFixture]
public class PaginationDataTests
{
    [Test]
    public void Create_WhenCalled_CreatesPaginationDataSuccessfully()
    {
        // Arrange
        int pageNumber = PaginationConst.DefaultPageNumber;
        int pageSize = PaginationConst.DefaultPageSize;
        int totalCount = CommonConst.MaxCount;
        int totalPages = CommonConst.MaxPages;

        // Act
        var paginationData = new PaginationData()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };

        // Asserts
        paginationData.Should().NotBeNull();
        paginationData.PageNumber.Should().Be(pageNumber);
        paginationData.PageSize.Should().Be(pageSize);
        paginationData.TotalCount.Should().Be(totalCount);
        paginationData.TotalPages.Should().Be(totalPages);
    }

    [Test]
    public void HasPreviousPage_WhenPageNumberIsGreaterThanOne_ReturnsTrue()
    {
        // Arrange
        int pageNumber = 2;
        int pageSize = PaginationConst.DefaultPageSize;
        int totalCount = CommonConst.MaxCount;
        int totalPages = CommonConst.MaxPages;

        // Act
        var paginationData = new PaginationData()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };

        // Asserts
        paginationData.Should().NotBeNull();
        paginationData.HasPreviousPage.Should().BeTrue();
        paginationData.PreviousPage.Should().Be(pageNumber - 1);
    }

    [Test]
    public void HasPreviousPage_WhenPageNumberIsOne_ReturnsFalse()
    {
        // Arrange
        int pageNumber = PaginationConst.DefaultPageNumber;
        int pageSize = PaginationConst.DefaultPageSize;
        int totalCount = CommonConst.MaxCount;
        int totalPages = CommonConst.MaxPages;

        // Act
        var paginationData = new PaginationData()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };

        // Asserts
        paginationData.Should().NotBeNull();
        paginationData.HasPreviousPage.Should().BeFalse();
        paginationData.PreviousPage.Should().Be(pageNumber);
    }

    [Test]
    public void HasNextPage_WhenPageNumberIsLessThanTotalPages_ReturnsTrue()
    {
        // Arrange
        int pageNumber = PaginationConst.DefaultPageNumber;
        int pageSize = PaginationConst.DefaultPageSize;
        int totalCount = CommonConst.MaxCount;
        int totalPages = CommonConst.MaxPages;

        // Act
        var paginationData = new PaginationData()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };

        // Asserts
        paginationData.Should().NotBeNull();
        paginationData.HasNextPage.Should().BeTrue();
        paginationData.NextPage.Should().Be(pageNumber + 1);
    }

    [Test]
    public void HasNextPage_WhenPageNumberIsEqualToTotalPages_ReturnsFalse()
    {
        // Arrange
        int pageNumber = CommonConst.MaxPages;
        int pageSize = PaginationConst.DefaultPageSize;
        int totalCount = CommonConst.MaxCount;
        int totalPages = CommonConst.MaxPages;

        // Act
        var paginationData = new PaginationData()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };

        // Asserts
        paginationData.Should().NotBeNull();
        paginationData.HasNextPage.Should().BeFalse();
        paginationData.NextPage.Should().Be(totalPages);
    }
}